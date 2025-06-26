
import pandas as pd
import csv
import os

import pandas as pd

def detect_delimiter(file_path):
    with open(file_path, 'r') as file:
        first_line = file.readline()
        if ',' in first_line:
            return ','
        elif '\t' in first_line:
            return '\t'
        else:
            return ','  # Default to comma

def load_csv(file_path: str, logger) -> pd.DataFrame:
    try:
        logger.info(f"Attempting to load CSV file from: {file_path}")

        delimiter = detect_delimiter(file_path)
        logger.info(f"Detected delimiter: '{delimiter}'")

        data = pd.read_csv(file_path, delimiter=delimiter)
        logger.info(f"Successfully loaded CSV file with shape: {data.shape} and columns: {list(data.columns)}")
        return data

    except Exception as e:
        logger.error(f"Error loading CSV file from {file_path}: {e}", exc_info=True)
        raise




def save_csv(data, file_path, logger):
    logger.info(f"Attempting to save DataFrame to: {file_path}")
    
    # Ensure the directory exists
    directory = os.path.dirname(file_path)
    if directory and not os.path.exists(directory):
        os.makedirs(directory, exist_ok=True)

    try:
        data.to_csv(file_path, index=False)
        logger.info(f"DataFrame successfully saved to: {file_path}")
    except Exception as e:
        logger.error(f"Error saving DataFrame to {file_path}: {e}", exc_info=True)
        raise

def infer_column_types(data: pd.DataFrame, logger) -> dict:
    """
    Infer the types of columns in the dataset.
    """
    try:
        logger.info("Inferring column types for the dataset.")
        column_types = {}
        for column in data.columns:
            if pd.api.types.is_numeric_dtype(data[column]):
                column_types[column] = "numeric"
                logger.info(f"Column '{column}' inferred as numeric.")
            elif pd.api.types.is_datetime64_any_dtype(data[column]):
                column_types[column] = "datetime"
                logger.info(f"Column '{column}' inferred as datetime.")
            else:
                column_types[column] = "string"
                logger.info(f"Column '{column}' inferred as string.")
        logger.info("Column type inference completed.")
        return column_types
    except Exception as e:
        logger.error("Error inferring column types: {e}", exc_info=True)
        raise
