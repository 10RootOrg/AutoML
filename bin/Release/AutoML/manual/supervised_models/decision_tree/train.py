import pandas as pd
from sklearn.model_selection import train_test_split, cross_val_score
from sklearn.tree import DecisionTreeClassifier, export_text
from sklearn.metrics import accuracy_score, classification_report, confusion_matrix
import os
import joblib
from datetime import datetime
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn import tree
from utils.config_loader import config

def train_and_save_model(logger):
    try:
        processed_data_path = "saved_processed_datasets/processed_dataset.csv"
        if config.PREPROCESSED_DATA_FILE_PATH != "":
            processed_data_path = config.PREPROCESSED_DATA_FILE_PATH

        logger.info(f"Loading processed dataset from {processed_data_path}")
        data = pd.read_csv(processed_data_path)
        logger.info(f"Dataset successfully loaded with {len(data)} rows and {len(data.columns)} columns.")

        label_column_name = config.SUPERVISED_GROUP_COLUMN_NAME.strip().lower().replace(" ", "_")

        # Determine which column to use as the label:
        if label_column_name != "":
            if label_column_name not in data.columns:
                raise ValueError(f"The label column '{label_column_name}' does not exist in the dataset columns: {data.columns}")
            X = data.drop(columns=[label_column_name])
            y = data[label_column_name]
        else:
            label_column_index = int(config.SUPERVISED_GROUP_COLUMN_INDEX)
            if label_column_index >= len(data.columns):
                raise ValueError(f"The label column index '{label_column_index}' is out of range for the dataset columns.")
            label_column_name = data.columns[label_column_index]
            X = data.drop(columns=[label_column_name])
            y = data[label_column_name]

        # Split data into training and testing sets
        X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)
        logger.info(f"Training set size: {len(X_train)}, Testing set size: {len(X_test)}")

        # Retrieve hyperparameters from the config file
        max_depth = getattr(config, 'DECISION_TREE_MAX_DEPTH', None)
        min_samples_split = getattr(config, 'DECISION_TREE_MIN_SAMPLES_SPLIT', 2)
        min_samples_leaf = getattr(config, 'DECISION_TREE_MIN_SAMPLES_LEAF', 1)
        criterion = getattr(config, 'DECISION_TREE_CRITERION', 'gini')

        # Validate hyperparameters
        if max_depth is not None and (not isinstance(max_depth, int) or max_depth <= 0):
            logger.warning("Invalid max_depth. Using default value: None.")
            max_depth = None
        if not isinstance(min_samples_split, int) or min_samples_split < 2:
            logger.warning("Invalid min_samples_split. Using default value: 2.")
            min_samples_split = 2
        if not isinstance(min_samples_leaf, int) or min_samples_leaf < 1:
            logger.warning("Invalid min_samples_leaf. Using default value: 1.")
            min_samples_leaf = 1
        if criterion not in ['gini', 'entropy']:
            logger.warning("Invalid criterion. Using default value: 'gini'.")
            criterion = 'gini'

        # Initialize the model with hyperparameters
        model = DecisionTreeClassifier(
            random_state=42,
            class_weight="balanced",
            max_depth=max_depth,
            min_samples_split=min_samples_split,
            min_samples_leaf=min_samples_leaf,
            criterion=criterion
        )

        # Train the model
        model.fit(X_train, y_train)
        logger.info("Model training completed.")

        # Cross-validation
        cv_scores = cross_val_score(model, X_train, y_train, cv=5)
        logger.info(f"Training Cross-validation scores: {cv_scores}")
        logger.info(f"Average training cross-validation score: {cv_scores.mean():.2f}")

        # Predictions
        y_pred = model.predict(X_test)

        # Metrics
        accuracy = accuracy_score(y_test, y_pred)
        logger.info(f"Model Accuracy: {accuracy:.2f}")
        logger.info("\nClassification Report:\n" + classification_report(y_test, y_pred))

        os.makedirs(config.MODEL_EVALUATIONS_FOLDER_PATH, exist_ok=True)

        # Confusion Matrix
        conf_matrix = confusion_matrix(y_test, y_pred)
        logger.info(f"Confusion Matrix:\n{conf_matrix}")
        plt.figure(figsize=(8, 6))
        sns.heatmap(conf_matrix, annot=True, fmt="d", cmap="Blues")
        plt.title("Confusion Matrix")
        plt.xlabel("Predicted")
        plt.ylabel("Actual")
        plt.tight_layout()
        conf_matrix_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "confusion_matrix.png")
        plt.savefig(conf_matrix_path)
        logger.info(f"Confusion matrix plot saved to {conf_matrix_path}")
        plt.close()

        # Feature Importances
        if hasattr(model, "feature_importances_"):
            feature_importances = pd.DataFrame({
                'Feature': X.columns,
                'Importance': model.feature_importances_
            }).sort_values(by='Importance', ascending=False)
            logger.info("\n" + feature_importances.to_string(index=False))

            # Cumulative Importance
            cumulative_importance = feature_importances['Importance'].cumsum()
            logger.info(f"Cumulative feature importance:\n{cumulative_importance}")

            # Number of features required for 95% importance
            n_important_features = (cumulative_importance <= 0.95).sum()
            logger.info(f"Number of features required to reach 95% cumulative importance: {n_important_features}")

            # Visualize feature importance
            plt.figure(figsize=(10, 6))
            sns.barplot(x=feature_importances['Importance'], y=feature_importances['Feature'])
            plt.title("Top Feature Importances")
            plt.tight_layout()
            feature_importance_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "feature_importance.png")
            plt.savefig(feature_importance_path)
            logger.info(f"Feature importance plot saved to {feature_importance_path}")
            plt.close()

        # Correlation Matrix
        corr_matrix = X.corr()
        logger.info("Analyzing correlations among features...")
        high_corr_pairs = corr_matrix.abs().stack().reset_index().query("level_0 != level_1 and 0.8 <= 0 < 1").sort_values(0, ascending=False)
        logger.info(f"Highly correlated feature pairs:\n{high_corr_pairs}")

        plt.figure(figsize=(12, 10))
        sns.heatmap(corr_matrix, annot=False, cmap="coolwarm", cbar=True)
        plt.title("Feature Correlation Matrix")
        corr_matrix_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "correlation_matrix.png")
        plt.savefig(corr_matrix_path)
        logger.info(f"Correlation matrix plot saved to {corr_matrix_path}")
        plt.close()

        # Misclassification Analysis
        misclassified = X_test[y_test != y_pred].copy()
        misclassified['Actual'] = y_test[y_test != y_pred]
        misclassified['Predicted'] = y_pred[y_test != y_pred]
        misclassified_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "misclassified_samples.csv")
        misclassified.to_csv(misclassified_path, index=False)
        logger.info(f"Misclassified samples saved to {misclassified_path}")

        # Save the model
        os.makedirs(os.path.dirname(config.SAVE_MODEL_FILE_PATH), exist_ok=True)
        joblib.dump(model, config.SAVE_MODEL_FILE_PATH)
        logger.info(f"Model saved to {config.SAVE_MODEL_FILE_PATH}")

        return model

    except Exception as e:
        logger.error(f"Error during model training and saving: {e}", exc_info=True)
        raise
