import pandas as pd
from sklearn.model_selection import train_test_split, StratifiedKFold
from sklearn.metrics import accuracy_score, classification_report, confusion_matrix
import os
import joblib
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from lightgbm import LGBMClassifier
from sklearn.preprocessing import LabelEncoder
import time
from datetime import timedelta
from utils.config_loader import config

class TrainingProgress:
    def __init__(self, total_folds, n_estimators):
        self.start_time = time.time()
        self.total_folds = total_folds
        self.n_estimators_per_fold = n_estimators
        self.total_iterations = total_folds * n_estimators
        self.current_fold = 0
        self.current_iteration = 0
        self.total_completed = 0

    def create_progress_bar(self, current, total, prefix='', length=100):
        """Creates a progress bar string"""
        percent = float(current) * 100 / total
        filled_length = int(length * current // total)
        bar = '█' * filled_length + '░' * (length - filled_length)
        return f'{prefix}: {percent:>3.0f}%|{bar}|'

    def format_time(self, seconds):
        """Formats time in seconds to a readable string"""
        return f"{seconds:.2f}s"

    def create_callback(self, fold):
        """Creates a callback for the current fold"""
        self.current_fold = fold

        def callback(env):
            # Update progress
            previous_iterations = (self.current_fold - 1) * self.n_estimators_per_fold
            self.total_completed = previous_iterations + env.iteration + 1

            # Calculate timing
            elapsed_time = time.time() - self.start_time
            if self.total_completed > 0:
                remaining_time = (elapsed_time / self.total_completed) * (self.total_iterations - self.total_completed)
            else:
                remaining_time = 0

            # Create progress bar
            progress_bar = self.create_progress_bar(
                self.current_fold, 
                self.total_folds,
                prefix='Hyperparameter Tuning LightGBM',
                length=100
            )

            # Add timing information
            timing_info = f" [{self.total_completed}/{self.total_iterations}, "
            timing_info += f"REMAINING={self.format_time(remaining_time)}, "
            timing_info += f"RUNTIME={self.format_time(elapsed_time)}]"

            # Print progress
            print(f'\r{progress_bar}{timing_info}', end='', flush=True)

        return callback

def train_and_save_model(logger):
    try:
        total_start_time = time.time()

        # Load data
        processed_data_path = config.PREPROCESSED_DATA_FILE_PATH
        logger.info(f"Loading processed dataset from {processed_data_path}")
        logger.info("Loading data...")
        data = pd.read_csv(processed_data_path)
        logger.info("Data loaded successfully")

        # Data preparation
        logger.info("Preparing data...")
        label_column_name = config.SUPERVISED_GROUP_COLUMN_NAME.strip().lower().replace(" ", "_")

        if label_column_name != "":
            if label_column_name not in data.columns:
                raise ValueError(f"Label column '{label_column_name}' not found in dataset columns: {data.columns}")
            X = data.drop(columns=[label_column_name])
            y = data[label_column_name]
        else:
            label_column_index = int(config.SUPERVISED_GROUP_COLUMN_INDEX)
            if label_column_index >= len(data.columns):
                raise ValueError(f"Label column index {label_column_index} out of range")
            label_column_name = data.columns[label_column_index]
            X = data.drop(columns=[label_column_name])
            y = data[label_column_name]

        # Encode labels
        le = LabelEncoder()
        y_encoded = le.fit_transform(y)

        # Split data
        X_train, X_test, y_train, y_test = train_test_split(X, y_encoded, test_size=0.2, random_state=42)
        logger.info("Data preparation completed")

        # Model parameters
        base_params = {
            'random_state': 42,
            'num_leaves': getattr(config, 'LIGHTGBM_NUM_LEAVES', 31),
            'max_depth': getattr(config, 'LIGHTGBM_MAX_DEPTH', -1),
            'learning_rate': getattr(config, 'LIGHTGBM_LEARNING_RATE', 0.1),
            'n_estimators': getattr(config, 'LIGHTGBM_N_ESTIMATORS', 100),
            'force_row_wise': True,
            'verbose': -1
        }

        # Initialize progress tracker
        n_folds = 5
        progress_tracker = TrainingProgress(n_folds + 1, base_params['n_estimators'])

        # Cross-validation
        logger.info("Starting cross-validation...")
        kf = StratifiedKFold(n_splits=n_folds, shuffle=True, random_state=42)
        cv_scores = []

        for fold_idx, (train_idx, val_idx) in enumerate(kf.split(X, y_encoded), 1):
            X_train_fold, X_val_fold = X.iloc[train_idx], X.iloc[val_idx]
            y_train_fold, y_val_fold = y_encoded[train_idx], y_encoded[val_idx]

            model_fold = LGBMClassifier(**base_params)
            callback_fn = progress_tracker.create_callback(fold_idx)

            model_fold.fit(
                X_train_fold, 
                y_train_fold,
                callbacks=[callback_fn]
            )

            val_pred = model_fold.predict(X_val_fold)
            fold_score = accuracy_score(y_val_fold, val_pred)
            cv_scores.append(fold_score)
            logger.info(f"Fold {fold_idx} accuracy: {fold_score:.4f}")

        logger.info(f"Cross-validation scores: {cv_scores}")
        logger.info(f"Average cross-validation score: {np.mean(cv_scores):.4f}")

        # Train final model
        logger.info("\nTraining final model...")
        callback_fn = progress_tracker.create_callback(n_folds + 1)
        model = LGBMClassifier(**base_params)
        model.fit(
            X_train, 
            y_train,
            callbacks=[callback_fn]
        )
        logger.info(" ")  # New line after progress bar

        # Evaluation and saving
        logger.info("\nGenerating evaluations...")
        y_pred = model.predict(X_test)
        accuracy = accuracy_score(y_test, y_pred)
        logger.info(f"Model Accuracy: {accuracy:.4f}")

        # Save visualizations
        os.makedirs(config.MODEL_EVALUATIONS_FOLDER_PATH, exist_ok=True)

        # Confusion Matrix
        conf_matrix = confusion_matrix(y_test, y_pred)
        plt.figure(figsize=(8, 6))
        sns.heatmap(conf_matrix, annot=True, fmt="d", cmap="Blues")
        plt.title("Confusion Matrix")
        plt.xlabel("Predicted")
        plt.ylabel("Actual")
        conf_matrix_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "confusion_matrix.png")
        plt.savefig(conf_matrix_path)
        plt.close()

        # Feature Importances
        if hasattr(model, "feature_importances_"):
            feature_importances = pd.DataFrame({
                "Feature": X.columns,
                "Importance": model.feature_importances_
            }).sort_values(by="Importance", ascending=False)

            plt.figure(figsize=(10, 6))
            sns.barplot(x=feature_importances["Importance"], y=feature_importances["Feature"])
            plt.title("Feature Importances")
            plt.tight_layout()
            plot_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "feature_importances.png")
            plt.savefig(plot_path)
            plt.close()

        # Save model
        model_data = {"model": model, "label_encoder": le}
        os.makedirs(os.path.dirname(config.SAVE_MODEL_FILE_PATH), exist_ok=True)
        joblib.dump(model_data, config.SAVE_MODEL_FILE_PATH)

        # Final timing
        total_time = time.time() - total_start_time
        logger.info(f"Total execution time: {str(timedelta(seconds=int(total_time)))}")
        logger.info(f"\nTraining completed in {str(timedelta(seconds=int(total_time)))}")

        return model

    except Exception as e:
        logger.error(f"Error during model training and saving: {e}", exc_info=True)
        raise
