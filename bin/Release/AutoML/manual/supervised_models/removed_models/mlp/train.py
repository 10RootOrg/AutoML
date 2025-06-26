import pandas as pd
from sklearn.model_selection import train_test_split, StratifiedKFold
from sklearn.metrics import accuracy_score, classification_report, confusion_matrix
import os
import joblib
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from utils.config_loader import config
from sklearn.neural_network import MLPClassifier
from sklearn.preprocessing import LabelEncoder

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

        # Encode labels to start at 0
        le = LabelEncoder()
        y_encoded = le.fit_transform(y)
        logger.info(f"Original classes: {list(y.unique())}")
        logger.info(f"Encoded classes: {list(le.classes_)}")
        logger.info(f"Transformed classes start at 0 and go to {len(le.classes_) - 1}")

        # Split data into training and testing sets
        X_train, X_test, y_train, y_test = train_test_split(X, y_encoded, test_size=0.2, random_state=42)
        logger.info(f"Training set size: {len(X_train)}, Testing set size: {len(X_test)}")

        # Retrieve hyperparameters from the config file
        hidden_layer_sizes = getattr(config, 'MLP_HIDDEN_LAYER_SIZES', (100,))
        activation = getattr(config, 'MLP_ACTIVATION', 'relu')
        solver = getattr(config, 'MLP_SOLVER', 'adam')
        alpha = getattr(config, 'MLP_ALPHA', 0.0001)
        learning_rate = getattr(config, 'MLP_LEARNING_RATE', 'constant')

        # Validate hyperparameters
        if not isinstance(hidden_layer_sizes, tuple) or not all(isinstance(i, int) and i > 0 for i in hidden_layer_sizes):
            logger.warning("Invalid hidden_layer_sizes. Using default value: (100,).")
            hidden_layer_sizes = (100,)
        if activation not in ['identity', 'logistic', 'tanh', 'relu']:
            logger.warning("Invalid activation. Using default value: 'relu'.")
            activation = 'relu'
        if solver not in ['lbfgs', 'sgd', 'adam']:
            logger.warning("Invalid solver. Using default value: 'adam'.")
            solver = 'adam'
        if not isinstance(alpha, (float, int)) or alpha <= 0:
            logger.warning("Invalid alpha. Using default value: 0.0001.")
            alpha = 0.0001
        if learning_rate not in ['constant', 'invscaling', 'adaptive']:
            logger.warning("Invalid learning_rate. Using default value: 'constant'.")
            learning_rate = 'constant'

        # Initialize MLPClassifier
        model = MLPClassifier(
            random_state=42,
            hidden_layer_sizes=hidden_layer_sizes,
            activation=activation,
            solver=solver,
            alpha=alpha,
            learning_rate=learning_rate,
            max_iter=300
        )

        # Manual Cross-validation for MLP
        logger.info("Performing manual cross-validation...")
        kf = StratifiedKFold(n_splits=5, shuffle=True, random_state=42)
        cv_scores = []

        for train_idx, val_idx in kf.split(X, y_encoded):
            X_train_fold, X_val_fold = X.iloc[train_idx], X.iloc[val_idx]
            y_train_fold, y_val_fold = y_encoded[train_idx], y_encoded[val_idx]

            model_fold = MLPClassifier(
                random_state=42,
                hidden_layer_sizes=hidden_layer_sizes,
                activation=activation,
                solver=solver,
                alpha=alpha,
                learning_rate=learning_rate,
                max_iter=300
            )
            model_fold.fit(X_train_fold, y_train_fold)
            val_pred = model_fold.predict(X_val_fold)
            score = accuracy_score(y_val_fold, val_pred)
            cv_scores.append(score)

        logger.info(f"Manual cross-validation scores: {cv_scores}")
        logger.info(f"Average cross-validation score: {np.mean(cv_scores):.2f}")

        # Train the final model on the full training data
        model.fit(X_train, y_train)
        logger.info("Model training completed.")

        # Predictions
        y_pred = model.predict(X_test)

        # Inverse transform predictions to original classes
        y_pred_original = le.inverse_transform(y_pred)
        y_test_original = le.inverse_transform(y_test)

        # Metrics
        accuracy = accuracy_score(y_test, y_pred)
        logger.info(f"Model Accuracy: {accuracy:.2f}")
        logger.info("\nClassification Report:\n" + classification_report(y_test_original, y_pred_original))

        os.makedirs(config.MODEL_EVALUATIONS_FOLDER_PATH, exist_ok=True)

        # Confusion Matrix
        conf_matrix = confusion_matrix(y_test_original, y_pred_original, labels=le.classes_)
        logger.info(f"Confusion Matrix:\n{conf_matrix}")
        plt.figure(figsize=(8, 6))
        sns.heatmap(conf_matrix, annot=True, fmt="d", cmap="Blues", xticklabels=le.classes_, yticklabels=le.classes_)
        plt.title("Confusion Matrix")
        plt.xlabel("Predicted")
        plt.ylabel("Actual")
        plt.tight_layout()
        conf_matrix_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "confusion_matrix.png")
        plt.savefig(conf_matrix_path)
        logger.info(f"Confusion matrix plot saved to {conf_matrix_path}")
        plt.close()

        # Correlation Analysis
        corr_matrix = X.corr()
        plt.figure(figsize=(12, 10))
        sns.heatmap(corr_matrix, annot=False, cmap="coolwarm", cbar=True)
        plt.title("Feature Correlation Matrix")
        corr_matrix_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "correlation_matrix.png")
        plt.savefig(corr_matrix_path)
        logger.info(f"Correlation matrix plot saved to {corr_matrix_path}")
        plt.close()

        # Save model and LabelEncoder
        model_data = {
            'model': model,
            'label_encoder': le
        }

        os.makedirs(os.path.dirname(config.SAVE_MODEL_FILE_PATH), exist_ok=True)
        joblib.dump(model_data, config.SAVE_MODEL_FILE_PATH)
        logger.info(f"Model and label encoder saved to {config.SAVE_MODEL_FILE_PATH}")

        return model

    except Exception as e:
        logger.error(f"Error during model training and saving: {e}", exc_info=True)
        raise
