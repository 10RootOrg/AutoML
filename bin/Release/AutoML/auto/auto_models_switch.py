from utils.config_loader import config
from sklearn.model_selection import GridSearchCV
from skopt import BayesSearchCV
import os
import time
import pandas as pd
from datetime import datetime
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.metrics import confusion_matrix, classification_report
from sklearn.model_selection import cross_val_score, learning_curve
import numpy as np
from preprocess import preprocess_dataset
import utils.data_handler
from tqdm import tqdm
import warnings
import joblib
import traceback

# Suppress warnings
def suppress_warnings():
    warnings.filterwarnings("ignore", category=FutureWarning)
    warnings.filterwarnings("ignore", category=UserWarning)
    warnings.filterwarnings("ignore", message=".*define the `__sklearn_tags__` method.*")
suppress_warnings()

def plot_and_save_feature_importance(model, X, evaluation_path, logger):
    """Plot and save the feature importance (if supported by the model)."""
    try:
        if hasattr(model, 'feature_importances_'):
            importance_df = pd.DataFrame({
                'Feature': X.columns,
                'Importance': model.feature_importances_
            }).sort_values('Importance', ascending=False)
            importance_df.to_csv(os.path.join(evaluation_path, 'feature_importance.csv'), index=False)
            logger.info("Saved feature importance analysis")

            plt.figure(figsize=(10, 6))
            plt.bar(importance_df['Feature'], importance_df['Importance'])
            plt.xticks(rotation=45, ha='right')
            plt.title('Feature Importance')
            plt.tight_layout()
            plt.savefig(os.path.join(evaluation_path, 'feature_importance.png'))
            plt.close()
    except Exception as e:
        logger.warning(f"Could not generate feature importance: {str(e)}")
        logger.error(traceback.format_exc())  # Logs the full traceback

def compute_and_save_correlation_matrix(X, evaluation_path, logger):
    """Compute and save the correlation matrix of features."""
    try:
        correlation_matrix = X.corr()
        plt.figure(figsize=(12, 8))
        sns.heatmap(correlation_matrix, annot=True, cmap='coolwarm', center=0)
        plt.title('Feature Correlation Matrix')
        plt.tight_layout()
        plt.savefig(os.path.join(evaluation_path, 'correlation_matrix.png'))
        plt.close()

        correlation_matrix.to_csv(os.path.join(evaluation_path, 'correlation_matrix.csv'))
        logger.info("Saved correlation analysis")
    except Exception as e:
        logger.warning(f"Could not generate correlation matrix: {str(e)}")
        logger.error(traceback.format_exc())  # Logs the full traceback

def compute_and_save_cross_validation_scores(model, X, y, evaluation_path, logger):
    """Compute cross-validation scores and save them to CSV."""
    try:
        cv_scores = cross_val_score(model, X, y, cv=5)
        cv_results = pd.DataFrame({
            'Fold': range(1, len(cv_scores) + 1),
            'Score': cv_scores
        })
        cv_results.to_csv(os.path.join(evaluation_path, 'cross_validation_scores.csv'), index=False)
        logger.info("Saved cross-validation scores")
    except Exception as e:
        logger.warning(f"Could not generate cross-validation scores: {str(e)}")
        logger.error(traceback.format_exc())  # Logs the full traceback

def plot_and_save_confusion_matrix(model, X, y, evaluation_path, logger):
    """Plot and save the confusion matrix (for classification tasks)."""
    try:
        y_pred = model.predict(X)
        cm = confusion_matrix(y, y_pred)
        plt.figure(figsize=(8, 6))
        sns.heatmap(cm, annot=True, fmt='d', cmap='Blues')
        plt.title('Confusion Matrix')
        plt.ylabel('True Label')
        plt.xlabel('Predicted Label')
        plt.tight_layout()
        plt.savefig(os.path.join(evaluation_path, 'confusion_matrix.png'))
        plt.close()

        class_report = classification_report(y, y_pred, output_dict=True)
        pd.DataFrame(class_report).transpose().to_csv(
            os.path.join(evaluation_path, 'classification_report.csv')
        )
        logger.info("Saved classification metrics")
    except Exception as e:
        logger.warning(f"Could not generate classification metrics: {str(e)}")
        logger.error(traceback.format_exc())  # Logs the full traceback

def plot_and_save_learning_curves(model, X, y, evaluation_path, logger):
    """Plot and save the learning curves of the model."""
    try:
        train_sizes, train_scores, test_scores = learning_curve(
            model, X, y, cv=5, n_jobs=-1,
            train_sizes=np.linspace(0.1, 1.0, 10)
        )

        plt.figure(figsize=(10, 6))
        plt.plot(train_sizes, np.mean(train_scores, axis=1), label='Training score')
        plt.plot(train_sizes, np.mean(test_scores, axis=1), label='Cross-validation score')
        plt.xlabel('Training Size')
        plt.ylabel('Score')
        plt.title('Learning Curves')
        plt.legend(loc='best')
        plt.grid(True)
        plt.tight_layout()
        plt.savefig(os.path.join(evaluation_path, 'learning_curves.png'))
        plt.close()
        logger.info("Saved learning curves")
    except Exception as e:
        logger.warning(f"Could not generate learning curves: {str(e)}")
        logger.error(traceback.format_exc())  # Logs the full traceback

def get_base_model(model_name, logger):
    base_params = {
        'random_state': 42,
        'num_leaves': 31,
        'max_depth': -1,
        'learning_rate': 0.1,
        'n_estimators': 100,
        'force_row_wise': True,
        'verbosity': -1
    }
    try:
        if model_name == "DecisionTree":
            from sklearn.tree import DecisionTreeClassifier
            return DecisionTreeClassifier()
        elif model_name == "LightGBM":
            from lightgbm import LGBMClassifier
            return LGBMClassifier(**base_params)
        else:
            raise ValueError(f"Unsupported model: {model_name}")
    except Exception as e:
        logger.error(f"Error initializing base model: {e}")
        logger.error(traceback.format_exc())  # Logs the full traceback
        raise


from tqdm import tqdm

# Custom progress bar format
tqdm_format = "{desc}: {percentage:3.0f}%|{bar}| {n_fmt}/{total_fmt} [{postfix}]"

def models_switch(args, logger):
    suppress_warnings()
    try:
        if args.train:
            data = utils.data_handler.load_csv(config.TRAIN_CSV_FILE_PATH, logger)
            processed_data = preprocess_dataset(data, logger)
            logger.info("Starting automatic model evaluation and hyperparameter tuning")

            model_results = {}
            start_time_total = time.time()

            if config.SUPERVISED_GROUP_COLUMN_NAME:
                y = data[config.SUPERVISED_GROUP_COLUMN_NAME]
                X = processed_data.drop(columns=[config.SUPERVISED_GROUP_COLUMN_NAME])
            else:
                y = data.iloc[:, int(config.SUPERVISED_GROUP_COLUMN_INDEX)]
                X = processed_data.drop(columns=[processed_data.columns[int(config.SUPERVISED_GROUP_COLUMN_INDEX)]])

            for model_name in config.MODELS_TO_TRY:
                logger.info(f"Starting evaluation of {model_name}")
                model_start_time = time.time()

                try:
                    # Define parameter grids for each model type separately
                    if model_name == "DecisionTree":
                        param_grid = {
                            'max_depth': config.MODEL_HYPERPARAMETERS.DecisionTree.max_depth,
                            'min_samples_split': config.MODEL_HYPERPARAMETERS.DecisionTree.min_samples_split,
                            'min_samples_leaf': config.MODEL_HYPERPARAMETERS.DecisionTree.min_samples_leaf,
                            'criterion': config.MODEL_HYPERPARAMETERS.DecisionTree.criterion
                        }
                    elif model_name == "LightGBM":
                        param_grid = {
                            'num_leaves': config.MODEL_HYPERPARAMETERS.LightGBM.num_leaves,
                            'max_depth': config.MODEL_HYPERPARAMETERS.LightGBM.max_depth,
                            'learning_rate': config.MODEL_HYPERPARAMETERS.LightGBM.learning_rate,
                            'n_estimators': config.MODEL_HYPERPARAMETERS.LightGBM.n_estimators
                        }
                    else:
                        raise ValueError(f"Unsupported model: {model_name}")

                    # Convert single values to lists for grid search
                    param_grid = {k: [v] if not isinstance(v, (list, tuple)) else v 
                                for k, v in param_grid.items()}

                    base_model = get_base_model(model_name, logger)

                    search = None
                    if config.SEARCH_STRATEGY == "GridSearchCV":
                        search = GridSearchCV(
                            estimator=base_model,
                            param_grid=[param_grid],  # Wrap in list as expected by GridSearchCV
                            cv=config.GRIDSEARCHCV_CV,
                            scoring=config.SCORING_METRIC,
                            n_jobs=config.GRIDSEARCHCV_N_JOBS,
                            verbose=0,
                            refit=config.REFIT_BEST_MODEL
                        )
                    elif config.SEARCH_STRATEGY == "BayesianOptimization":
                        search = BayesSearchCV(
                            estimator=base_model,
                            search_spaces=param_grid,
                            n_iter=config.BAYESIANOPT_N_ITER,
                            cv=5,
                            n_jobs=config.BAYESIANOPT_N_JOBS,
                            random_state=config.BAYESIANOPT_RANDOM_STATE,
                            n_points=config.BAYESIANOPT_N_INITIAL_POINTS,
                            optimizer_kwargs={'acq_func': config.BAYESIANOPT_ACQUISITION_FUNCTION},
                            verbose=0
                        )

                    if search is None:
                        raise ValueError(f"Search strategy for {model_name} is not properly configured or unsupported.")

                    total_combinations = (len(param_grid) if config.SEARCH_STRATEGY == "GridSearchCV" 
                                       else config.BAYESIANOPT_N_ITER)

                    with tqdm(total=total_combinations, desc=f"Hyperparameter Tuning {model_name}", 
                            unit="comb", bar_format=tqdm_format) as pbar:
                        search.fit(X, y)
                        pbar.update(total_combinations)  # Update progress bar to completion

                    model_results[model_name] = {
                        'best_score': search.best_score_,
                        'best_params': search.best_params_,
                        'model': search.best_estimator_,
                        'training_time': time.time() - model_start_time
                    }

                    logger.info(f"Best parameters for {model_name}: {search.best_params_}")
                    logger.info(f"Best score for {model_name}: {search.best_score_}")

                except Exception as model_error:
                    logger.error(f"Error during {model_name} evaluation: {str(model_error)}")
                    logger.error(traceback.format_exc())
                    continue

                if (time.time() - start_time_total) / 60 > config.MAXIMUM_TIME_PER_MODEL:
                    logger.warning("Maximum time reached, stopping further model evaluations.")
                    break

            if model_results:
                best_model_name = max(model_results.items(), key=lambda x: x[1]['best_score'])[0]
                best_model = model_results[best_model_name]['model']

                logger.info(f"Best performing model: {best_model_name} with score: {model_results[best_model_name]['best_score']}")

                # Save the best model
                model_save_path = config.SAVE_MODEL_FILE_PATH
                os.makedirs(os.path.dirname(model_save_path), exist_ok=True)
                joblib.dump(best_model, model_save_path)
                logger.info(f"Best model saved to {model_save_path}")

        elif args.predict:
            # Prediction logic remains unchanged
            logger.info("Prediction process initiated.")
            model_path = config.MODEL_PKL_FILE_PATH
            input_data_path = config.PREDICT_CSV_FILE_PATH
            os.makedirs(os.path.dirname(config.OUTPUT_FILE_PATH), exist_ok=True)
            output_path = config.OUTPUT_FILE_PATH

            try:
                predict_from_csv(model_path, input_data_path, output_path, logger)
                logger.info("Prediction completed successfully.")
            except Exception as e:
                logger.error(f"Prediction failed: {e}")
                logger.error(traceback.format_exc())

    except Exception as e:
        logger.error(f"An error occurred: {e}", exc_info=True)
        logger.error(traceback.format_exc())
        raise


def predict_from_csv(model_path, input_data_path, output_path, logger):
    try:
        logger.info("Starting prediction process...")

        # Load the saved model
        logger.info(f"Loading model from: {model_path}")
        best_model = joblib.load(model_path)

        # Load input data
        logger.info(f"Loading input data from: {input_data_path}")
        raw_data = pd.read_csv(input_data_path)

        # Preprocess the data
        logger.info("Preprocessing input data...")
        processed_data = preprocess_dataset(raw_data, logger)

        # Ensure correct features
        if hasattr(best_model, 'feature_names_in_'):
            expected_features = best_model.feature_names_in_
            logger.info(f"Model expects features: {expected_features}")
        else:
            expected_features = processed_data.columns.to_list()
            logger.warning("Model does not have feature names. Using dataset columns.")

        # Handle missing features
        missing_features = [f for f in expected_features if f not in processed_data.columns]
        if missing_features:
            logger.warning(f"Adding missing features with default values: {missing_features}")
            for missing_feature in missing_features:
                processed_data[missing_feature] = 0
        processed_data = processed_data[expected_features]

        # Ensure all columns are numeric
        processed_data = processed_data.apply(pd.to_numeric, errors='coerce')

        # Make predictions
        logger.info("Making predictions...")
        predictions = best_model.predict(processed_data)
        probabilities = best_model.predict_proba(processed_data).max(axis=1) * 100

        # Save results
        logger.info(f"Saving predictions to: {output_path}")
        raw_data['Predicted_Label'] = predictions
        raw_data['Prediction_Confidence'] = probabilities
        raw_data.to_csv(output_path, index=False)
        logger.info("Predictions saved successfully.")

    except Exception as e:
        logger.error(f"Prediction failed: {str(e)}")
        logger.error(traceback.format_exc())  # Logs the full traceback
        raise
