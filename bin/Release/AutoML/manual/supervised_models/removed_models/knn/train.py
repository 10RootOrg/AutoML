import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split, cross_val_score
from sklearn.neighbors import KNeighborsClassifier
from sklearn.preprocessing import StandardScaler
from sklearn.metrics import accuracy_score, classification_report, confusion_matrix
import os
import joblib
from utils.config_loader import config
import matplotlib.pyplot as plt
import seaborn as sns

def train_and_save_model(logger):
    try:
        processed_data_path = "saved_processed_datasets/processed_dataset.csv"
        if config.PREPROCESSED_DATA_FILE_PATH != "":
            processed_data_path = config.PREPROCESSED_DATA_FILE_PATH

        logger.info(f"Loading processed dataset from {processed_data_path}")
        data = pd.read_csv(processed_data_path)
        logger.info(f"Dataset successfully loaded with {len(data)} rows and {len(data.columns)} columns.")

        label_column_name = config.SUPERVISED_GROUP_COLUMN_NAME.strip().lower().replace(" ", "_")

        # Determine the label column
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

        # Create pipeline with feature names support
        scaler = StandardScaler().set_output(transform="pandas")
        X_scaled = scaler.fit_transform(X)

        # Split data into training and testing sets
        X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)
        logger.info(f"Training set size: {len(X_train)}, Testing set size: {len(X_test)}")

        # Retrieve hyperparameters from the config file
        n_neighbors = getattr(config, 'KNN_N_NEIGHBORS', 5)
        weights = getattr(config, 'KNN_WEIGHTS', 'uniform')
        algorithm = getattr(config, 'KNN_ALGORITHM', 'auto')

        # Validate hyperparameters
        if not isinstance(n_neighbors, int) or n_neighbors <= 0:
            logger.warning("Invalid n_neighbors. Using default value: 5.")
            n_neighbors = 5
        if weights not in ['uniform', 'distance']:
            logger.warning("Invalid weights. Using default value: 'uniform'.")
            weights = 'uniform'
        if algorithm not in ['auto', 'ball_tree', 'kd_tree', 'brute']:
            logger.warning("Invalid algorithm. Using default value: 'auto'.")
            algorithm = 'auto'

        # Initialize the KNN classifier
        model = KNeighborsClassifier(
            n_neighbors=n_neighbors,
            weights=weights,
            algorithm=algorithm
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

        # Feature Correlation Matrix
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

        # Save the model and scaler together
        model_data = {
            'model': model,
            'scaler': scaler
        }

        os.makedirs(os.path.dirname(config.SAVE_MODEL_FILE_PATH), exist_ok=True)
        joblib.dump(model_data, config.SAVE_MODEL_FILE_PATH)
        logger.info(f"Model and scaler saved to {config.SAVE_MODEL_FILE_PATH}")

        return model

    except Exception as e:
        logger.error(f"Error during model training and saving: {e}", exc_info=True)
        raise
