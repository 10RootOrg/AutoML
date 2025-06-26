import pandas as pd
from sklearn.model_selection import train_test_split, StratifiedKFold
from sklearn.metrics import accuracy_score, classification_report, confusion_matrix
import os
import joblib
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from utils.config_loader import config
from xgboost import XGBClassifier
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

        # Encode labels
        le = LabelEncoder()
        y_encoded = le.fit_transform(y)
        logger.info(f"Classes: {list(le.classes_)}")

        # Split data
        X_train, X_test, y_train, y_test = train_test_split(X, y_encoded, test_size=0.2, random_state=42)
        logger.info(f"Training size: {len(X_train)}, Testing size: {len(X_test)}")

        # Hyperparameters
        learning_rate = getattr(config, "XGBOOST_LEARNING_RATE", 0.1)
        max_depth = getattr(config, "XGBOOST_MAX_DEPTH", 6)
        n_estimators = getattr(config, "XGBOOST_N_ESTIMATORS", 100)
        subsample = getattr(config, "XGBOOST_SUBSAMPLE", 1.0)
        objective = getattr(config, "XGBOOST_OBJECTIVE", "multi:softprob")

        # Model initialization
        model = XGBClassifier(
            random_state=42,
            use_label_encoder=False,
            eval_metric="mlogloss",
            learning_rate=learning_rate,
            max_depth=max_depth,
            n_estimators=n_estimators,
            subsample=subsample,
            objective=objective
        )

        # Manual Cross-validation
        logger.info("Performing manual cross-validation...")
        kf = StratifiedKFold(n_splits=5, shuffle=True, random_state=42)
        cv_scores = []
        for train_idx, val_idx in kf.split(X, y_encoded):
            X_train_fold, X_val_fold = X.iloc[train_idx], X.iloc[val_idx]
            y_train_fold, y_val_fold = y_encoded[train_idx], y_encoded[val_idx]

            model_fold = XGBClassifier(
                random_state=42,
                use_label_encoder=False,
                eval_metric="mlogloss",
                learning_rate=learning_rate,
                max_depth=max_depth,
                n_estimators=n_estimators,
                subsample=subsample,
                objective=objective
            )
            model_fold.fit(X_train_fold, y_train_fold)
            val_pred = model_fold.predict(X_val_fold)
            cv_scores.append(accuracy_score(y_val_fold, val_pred))

        logger.info(f"Cross-validation scores: {cv_scores}")
        logger.info(f"Average CV score: {np.mean(cv_scores):.2f}")

        # Train the final model
        model.fit(X_train, y_train)
        logger.info("Training completed.")

        # Predictions
        y_pred = model.predict(X_test)

        # Metrics
        accuracy = accuracy_score(y_test, y_pred)
        logger.info(f"Accuracy: {accuracy:.2f}")
        logger.info("\nClassification Report:\n" + classification_report(y_test, y_pred))

        # Confusion Matrix
        conf_matrix = confusion_matrix(y_test, y_pred)
        plt.figure(figsize=(8, 6))
        sns.heatmap(conf_matrix, annot=True, fmt="d", cmap="Blues")
        plt.title("Confusion Matrix")
        plt.xlabel("Predicted")
        plt.ylabel("Actual")
        plt.tight_layout()
        conf_matrix_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "confusion_matrix.png")
        plt.savefig(conf_matrix_path)
        logger.info(f"Confusion matrix saved to {conf_matrix_path}")
        plt.close()

        # Feature Importance
        if hasattr(model, "feature_importances_"):
            feature_importances = pd.DataFrame({
                "Feature": X.columns,
                "Importance": model.feature_importances_
            }).sort_values(by="Importance", ascending=False)

            logger.info("Feature Importances:\n" + feature_importances.to_string(index=False))
            plt.figure(figsize=(10, 6))
            sns.barplot(x=feature_importances["Importance"], y=feature_importances["Feature"])
            plt.title("Feature Importances")
            plt.tight_layout()
            feature_importance_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "feature_importances.png")
            plt.savefig(feature_importance_path)
            logger.info(f"Feature importances saved to {feature_importance_path}")
            plt.close()

        # Correlation Analysis
        corr_matrix = X.corr()
        plt.figure(figsize=(12, 10))
        sns.heatmap(corr_matrix, annot=False, cmap="coolwarm", cbar=True)
        plt.title("Feature Correlation Matrix")
        correlation_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "correlation_matrix.png")
        plt.savefig(correlation_path)
        logger.info(f"Correlation matrix saved to {correlation_path}")
        plt.close()

        # Save the model
        model_data = {"model": model, "label_encoder": le}
        os.makedirs(os.path.dirname(config.SAVE_MODEL_FILE_PATH), exist_ok=True)
        joblib.dump(model_data, config.SAVE_MODEL_FILE_PATH)
        logger.info(f"Model saved to {config.SAVE_MODEL_FILE_PATH}")

        return model

    except Exception as e:
        logger.error(f"Error during model training and saving: {e}", exc_info=True)
        raise
