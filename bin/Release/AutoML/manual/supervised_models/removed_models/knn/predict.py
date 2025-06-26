import pandas as pd
import joblib
from utils.config_loader import config
import os

def predict_from_csv(data, processed_data, logger):
    """
    Predict from a CSV file using a pre-trained KNN model, adding confidence percentages.
    """
    try:
        logger.info("Starting prediction process...")
        
        # Define paths
        output_csv_path = os.path.join(config.OUTPUT_FILE_PATH, "model_prediction.csv")
        model_path = config.MODEL_PKL_FILE_PATH
        
        logger.info(f"Using paths:")
        logger.info(f"- Output CSV: {output_csv_path}")
        logger.info(f"- Model path: {model_path}")

        # Check model file existence
        if not os.path.exists(model_path):
            logger.error(f"Model file not found at {model_path}")
            raise FileNotFoundError(f"Model file not found at {model_path}")
        
        # Load the model
        logger.info("Loading saved model...")
        model_data = joblib.load(model_path)
        
        # Extract model and feature names from saved data
        if isinstance(model_data, dict):
            model = model_data['model']
            # Get feature names from the model's feature_names_in_ attribute
            if hasattr(model, 'feature_names_in_'):
                training_features = model.feature_names_in_.tolist()
            else:
                logger.error("Model does not have feature names information")
                raise ValueError("Model does not have feature names information")
        else:
            model = model_data
            if hasattr(model, 'feature_names_in_'):
                training_features = model.feature_names_in_.tolist()
            else:
                logger.error("Model does not have feature names information")
                raise ValueError("Model does not have feature names information")
            
        logger.info(f"Model loaded successfully with {len(training_features)} features")
        logger.info(f"Required features: {', '.join(training_features)}")

        # Use the preprocessed data for prediction
        logger.info("Using preprocessed data for prediction...")
        prediction_data = processed_data[training_features]

        # Ensure correct features are present and in correct order
        missing_features = [f for f in training_features if f not in processed_data.columns]
        extra_features = [f for f in processed_data.columns if f not in training_features]
        
        if missing_features:
            logger.error(f"Missing features in prediction data: {missing_features}")
            raise ValueError(f"Missing features in prediction data: {missing_features}")
        
        if extra_features:
            logger.warning(f"Extra features in prediction data (will be ignored): {extra_features}")
        
        # Log the columns in input data
        logger.info(f"Input data columns: {', '.join(data.columns)}")

        # Make predictions and calculate confidences
        logger.info("Making predictions...")
        predictions = model.predict(prediction_data)
        confidences = model.predict_proba(prediction_data).max(axis=1) * 100  # Confidence percentages
        logger.info(f"Generated predictions for {len(predictions)} samples")

        # Add predictions and confidences to the original dataset
        logger.info("Adding predictions and confidences to dataset")
        data['Predicted_Group'] = predictions
        data['Prediction_Confidence_Percentage'] = confidences

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
        logger.error(f"Error during prediction process: {e}", exc_info=True)
        raise
