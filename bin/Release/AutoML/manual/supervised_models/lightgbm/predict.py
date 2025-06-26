import pandas as pd
import joblib
from utils.config_loader import config
import os

def predict_from_csv(data, processed_data, logger):
    """
    Predict from a dataset using a pre-trained LightGBM model and save results with confidence scores.
    """
    try:
        logger.info("Starting prediction process...")

        # Define paths
        output_csv_path = config.OUTPUT_FILE_PATH
        model_path = config.SAVE_MODEL_FILE_PATH

        logger.info(f"Using paths:")
        logger.info(f"- Output CSV: {output_csv_path}")
        logger.info(f"- Model path: {model_path}")

        # Check model file existence
        if not os.path.exists(model_path):
            logger.error(f"Model file not found at {model_path}")
            raise FileNotFoundError(f"Model file not found at {model_path}")

        # Load the saved model data
        logger.info("Loading saved model...")
        loaded_data = joblib.load(model_path)

        # Extract model and label encoder
        if not isinstance(loaded_data, dict):
            raise ValueError(f"Expected dict, got {type(loaded_data)}")

        model = loaded_data.get('model')
        le = loaded_data.get('label_encoder')

        if model is None:
            raise ValueError("No model found in loaded data")
        if le is None:
            raise ValueError("No label encoder found in loaded data")

        # Get feature names from the model
        if hasattr(model, 'feature_names_in_'):
            training_features = model.feature_names_in_.tolist()
            logger.info(f"Model loaded successfully with {len(training_features)} features")
            logger.info(f"Required features: {', '.join(training_features)}")
        else:
            logger.warning("Model does not have feature names information. Attempting to use provided data columns.")
            training_features = processed_data.columns.tolist()
            logger.info(f"Assuming features: {', '.join(training_features)}")

        # Ensure correct features are present in the data
        logger.info("Checking for missing features...")
        all_columns = processed_data.columns.union(data.columns)
        missing_features = [f for f in training_features if f not in all_columns]
        if missing_features:
            logger.error(f"Missing features in data: {missing_features}")
            raise ValueError(f"Missing features in data: {missing_features}")

        # Combine data and preprocess
        logger.info("Combining and preprocessing data...")
        combined_data = pd.concat([data, processed_data], axis=0, ignore_index=True)
        for missing_feature in training_features:
            if missing_feature not in combined_data.columns:
                combined_data[missing_feature] = 0  # Default value for missing features
        combined_data = combined_data[training_features]

        # Handle non-numeric columns
        logger.info("Converting non-numeric columns to numeric...")
        for col in combined_data.select_dtypes(include=['object']).columns:
            combined_data[col], _ = pd.factorize(combined_data[col])
        logger.info(f"Data converted successfully. Shape: {combined_data.shape}")

        # Select the required features for prediction
        prediction_data = combined_data.iloc[:len(data), :]  # Use only the original data rows

        # Make predictions
        logger.info("Making predictions...")
        raw_predictions = model.predict(prediction_data)
        probabilities = model.predict_proba(prediction_data).max(axis=1) * 100  # Confidence percentages
        predictions = le.inverse_transform(raw_predictions)
        logger.info(f"Generated {len(predictions)} predictions")

        # Add predictions and confidences to the original dataset
        logger.info("Adding predictions and confidences to dataset")
        data['Predicted_Group'] = predictions
        data['Prediction_Confidence_Percentage'] = probabilities

        # Log prediction distribution
        value_counts = pd.Series(predictions).value_counts()
        logger.info("\nPrediction distribution:")
        for group, count in value_counts.items():
            percentage = (count / len(predictions)) * 100
            logger.info(f"Group {group}: {count} samples ({percentage:.2f}%)")

        # Create output directory if it doesn't exist
        os.makedirs(os.path.dirname(output_csv_path), exist_ok=True)

        # Save results
        logger.info(f"Saving predictions to {output_csv_path}")
        data.to_csv(output_csv_path, index=False)
        logger.info("Predictions saved successfully")

        logger.info("Prediction process completed successfully")
        return predictions

    except Exception as e:
        logger.error(f"Error during prediction process: {str(e)}")
        raise