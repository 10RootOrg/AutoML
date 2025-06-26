import pandas as pd
from sklearn.preprocessing import LabelEncoder
from utils.config_loader import config
from utils.data_handler import load_csv, save_csv
import os
import re

def remove_columns(df, logger):
    """
    Remove columns from the DataFrame based on configurations in config.py.
    """
    try:
        # Standardize column names before any operation
        logger.info("Standardizing column names before column removal...")
        df.columns = df.columns.str.strip().str.lower().str.replace(" ", "_", regex=False)
        logger.info(f"Standardized column names: {list(df.columns)}")

        initial_columns = list(df.columns)  # Keep track of initial columns
        logger.info(f"Initial columns in DataFrame: {initial_columns}")

        # Step 1: Remove columns by index
        if config.COLUMNS_TO_REMOVE_BY_INDEX:
            valid_indices = [i for i in config.COLUMNS_TO_REMOVE_BY_INDEX if 0 <= i < len(df.columns)]
            if valid_indices:
                logger.info(f"Removing columns by index: {valid_indices}")
                df = df.drop(df.columns[valid_indices], axis=1)
            else:
                logger.warning("No valid indices found in COLUMNS_TO_REMOVE_BY_INDEX.")

        # Step 2: Remove columns by name
        if config.COLUMNS_TO_REMOVE_BY_NAME:
            # Standardize the column names in the configuration
            standardized_columns_to_remove = [
                col.strip().lower().replace(" ", "_") for col in config.COLUMNS_TO_REMOVE_BY_NAME
            ]
            columns_to_remove = [col for col in standardized_columns_to_remove if col in df.columns]
            if columns_to_remove:
                logger.info(f"Removing columns by name: {columns_to_remove}")
                df = df.drop(columns=columns_to_remove)
            else:
                logger.warning("No matching columns found in COLUMNS_TO_REMOVE_BY_NAME.")

        # Step 3: Remove columns matching regex
        if hasattr(config, 'COLUMNS_TO_REMOVE_USING_REGEX') and config.COLUMNS_TO_REMOVE_USING_REGEX:
            pattern = re.compile(config.COLUMNS_TO_REMOVE_USING_REGEX, re.IGNORECASE)
            columns_to_drop = [col for col in df.columns if pattern.search(col)]
            if columns_to_drop:
                logger.info(f"Removing columns matching regex '{config.COLUMNS_TO_REMOVE_USING_REGEX}': {columns_to_drop}")
                df = df.drop(columns=columns_to_drop)
            else:
                logger.warning("No columns matched the regex in COLUMNS_TO_REMOVE_USING_REGEX.")

        # Check if all columns were removed
        if df.empty or len(df.columns) == 0:
            raise ValueError("All columns have been removed. Review the removal criteria.")

        logger.info(f"Final columns in DataFrame: {list(df.columns)}")
        return df

    except Exception as e:
        logger.error(f"Error during column removal: {e}", exc_info=True)
        raise


def standardize_column_names(df, logger):
    logger.info("Standardizing column names...")
    df.columns = df.columns.str.strip().str.lower().str.replace(" ", "_", regex=False)
    logger.info(f"Column names after standardization: {list(df.columns)}")
    return df


def preprocess_dataset(data, logger):
    try:
        data = data.copy()  # Create a hard copy for preprocessing
        logger.info(f"Initial dataset shape: {data.shape}")

        # Standardize column names
        data = standardize_column_names(data, logger)

        # Remove unnecessary columns
        data = remove_columns(data, logger)
        logger.info(f"Shape after column removal: {data.shape}")

        # List of columns explicitly intended to be dates
        date_columns = getattr(config, "DATE_COLUMNS", [])

        # Process each column
        logger.info("Processing individual columns...")
        new_columns = {}
        columns_to_drop = []

        for column in data.columns:
            logger.info(f"Processing column: {column}")
            col_dtype = data[column].dtype

            if column in date_columns:
                # Parse explicitly listed date columns
                try:
                    parsed_dates = pd.to_datetime(data[column], errors="coerce")
                    logger.info(f"Column {column} parsed as dates.")
                    new_columns[f"{column}_year"] = parsed_dates.dt.year
                    new_columns[f"{column}_month"] = parsed_dates.dt.month
                    new_columns[f"{column}_day"] = parsed_dates.dt.day
                    columns_to_drop.append(column)
                    continue
                except Exception as e:
                    logger.warning(f"Failed to parse {column} as dates: {e}")
            elif col_dtype == "object":
                # Clean string column
                logger.info(f"Cleaning and encoding string column: {column}")
                data[column] = (
                    data[column]
                    .fillna("missing")
                    .str.lower()
                    .str.replace(r"[^a-z0-9\s]", "", regex=True)
                    .str.strip()
                )

                # Encode string column
                if data[column].nunique() <= 10:  # Low cardinality
                    mapping = {val: idx + 1 for idx, val in enumerate(data[column].unique())}
                    data[column] = data[column].map(mapping)
                else:  # High cardinality
                    le = LabelEncoder()
                    data[column] = le.fit_transform(data[column])
            elif pd.api.types.is_numeric_dtype(col_dtype):
                logger.info(f"Filling missing values in numeric column: {column}")
                data.loc[:, column] = data[column].fillna(data[column].mean())
            elif pd.api.types.is_datetime64_any_dtype(col_dtype):
                logger.info(f"Splitting datetime column: {column}")
                new_columns[f"{column}_year"] = data[column].dt.year
                new_columns[f"{column}_month"] = data[column].dt.month
                new_columns[f"{column}_day"] = data[column].dt.day
                columns_to_drop.append(column)

        # Add new columns
        for new_col, values in new_columns.items():
            data[new_col] = values

        # Drop processed columns
        if columns_to_drop:
            logger.info(f"Dropping processed columns: {columns_to_drop}")
            data.drop(columns=columns_to_drop, inplace=True)

        logger.info(f"Final dataset shape: {data.shape}")
        save_csv(data, config.PREPROCESSED_DATA_FILE_PATH or "processed_dataset.csv", logger)
        return data

    except Exception as e:
        logger.error(f"Error during dataset preprocessing: {e}", exc_info=True)
        raise
