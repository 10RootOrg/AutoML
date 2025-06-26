import pandas as pd
import joblib
from utils.config_loader import config
import os

def predict_from_csv(data, processed_data, logger):
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

        # Load the model data
        logger.info("Loading saved model...")
        model_data = joblib.load(model_path)

        if isinstance(model_data, dict):
            model = model_data.get('model')
            scaler = model_data.get('scaler')
            if not model or not scaler:
                raise ValueError("Loaded model data does not contain required 'model' and 'scaler' keys.")
        else:
            model = model_data
            scaler = None
            logger.warning("Scaler not found; proceeding without scaling.")

        # Use the preprocessed data for prediction but map predictions back to the original data
        logger.info(f"Using preprocessed data for prediction with shape: {processed_data.shape}")

        # Scale features
        if scaler:
            logger.info("Scaling features...")
            try:
                X_scaled = scaler.transform(processed_data)
                logger.info("Features scaled successfully")
            except ValueError as e:
                logger.error(f"Error during feature scaling: {e}")
                raise
        else:
            logger.warning("No scaler found. Proceeding with raw features.")
            X_scaled = processed_data

        # Make predictions
        logger.info("Making predictions...")
        predictions = model.predict(X_scaled)

        # Get confidence scores
        confidences = model.predict_proba(X_scaled).max(axis=1) * 100  # Convert to percentages

        logger.info(f"Generated predictions for {len(predictions)} samples")

        # Add predictions and confidences to the original dataset
        result_data = data.copy()
        result_data['Predicted_Group'] = predictions
        result_data['Prediction_Confidence_Percentage'] = confidences

        # Log prediction distribution
        value_counts = pd.Series(predictions).value_counts()
        logger.info("\nPrediction distribution:")
        for group, count in value_counts.items():
            percentage = (count / len(predictions)) * 100
            logger.info(f"Group {group}: {count} samples ({percentage:.2f}%)")

        # Save results
        os.makedirs(os.path.dirname(output_csv_path), exist_ok=True)
        result_data.to_csv(output_csv_path, index=False)
        logger.info(f"Results saved to {output_csv_path}")

        return predictions

    except Exception as e:
        logger.error(f"Error during prediction process: {e}", exc_info=True)
        raise
