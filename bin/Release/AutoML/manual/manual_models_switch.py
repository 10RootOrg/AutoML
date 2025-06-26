from utils.config_loader import config
import manual.supervised_models.decision_tree.train
import manual.supervised_models.decision_tree.predict

import manual.supervised_models.lightgbm.train
import manual.supervised_models.lightgbm.predict

import manual.unsupervised_models.decision_tree.train
import manual.unsupervised_models.k_means.train

from preprocess import preprocess_dataset
import utils.data_handler

import os
import numpy as np


def models_switch(args, logger):
    try:
        if args.train:
            logger.info("Train")
            logger.info("Reading csv into dataframe!")
            data = utils.data_handler.load_csv(config.TRAIN_CSV_FILE_PATH, logger)

            logger.info("Loading and preprocessing the dataset.")
            # Load and preprocess the dataset
            processed_data = preprocess_dataset(data, logger)
            if config.ML_MODEL_NAME == "DecisionTree":
                logger.info("Train supervised decision tree models has been chosen!")
                model = manual.supervised_models.decision_tree.train.train_and_save_model(logger)
            if config.ML_MODEL_NAME == "LightGBM":
                logger.info("Train supervised LightGBM models has been chosen!")
                model = manual.supervised_models.lightgbm.train.train_and_save_model(logger)
                
        if args.predict:
            logger.info("Reading csv into dataframe!")
            data = utils.data_handler.load_csv(config.PREDICT_CSV_FILE_PATH, logger)

            logger.info("Loading and preprocessing the dataset.")
            # Load and preprocess the dataset
            print(data)
            processed_data = preprocess_dataset(data, logger)
            if not config.SUPERVISED_MODEL:
                # Train and save the model
                logger.info("Starting model training...")
                if config.ML_MODEL_NAME == "DecisionTree":
                    logger.info("Unsupervised decision tree models has been chosen!")
                    manual.unsupervised_models.decision_tree.train.train_and_save_model(data, processed_data, logger)
                if config.ML_MODEL_NAME == "KMeans":
                    logger.info("Unsupervised K means models has been chosen!")
                    manual.unsupervised_models.k_means.train.train_and_save_model(data, processed_data, logger)
                logger.info("Model training and saving completed.")
            else:
                if config.ML_MODEL_NAME == "DecisionTree":
                    manual.supervised_models.decision_tree.predict.predict_from_csv(data, processed_data, logger)
                if config.ML_MODEL_NAME == "LightGBM":
                    manual.supervised_models.lightgbm.predict.predict_from_csv(data, processed_data, logger)
                    
    except Exception as e:
        logger.error(f"An error occurred: {e}", exc_info=True)
        raise