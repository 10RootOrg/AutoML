import logging
from utils.config_loader import config
from utils.config_loader import config
import os

class IgnoreSpecificMessagesFilter(logging.Filter):
    def filter(self, record):
        # Ignore LightGBM warnings about "No further splits with positive gain"
        ignore_phrases = [
            "No further splits with positive gain",
            "_get_tags",
        ]
        return not any(phrase in record.getMessage() for phrase in ignore_phrases)

def setup_logging(mode):
    # Ensure logs directory exists
    logging_path = config.LOGGING_PATH
    if mode == "manual":
        logging_path = config.LOGGING_PATH

    directory = os.path.dirname(logging_path)
    # Create directory if not exists
    os.makedirs(directory, exist_ok=True)
    
    # Configure the logger
    logger = logging.getLogger()
    logger.setLevel(logging.INFO)

    # File handler
    file_handler = logging.FileHandler(logging_path, mode="a")
    file_handler.setLevel(logging.INFO)
    file_handler.setFormatter(logging.Formatter("%(asctime)s - %(levelname)s - %(message)s"))
    
    # Console handler
    console_handler = logging.StreamHandler()
    console_handler.setLevel(logging.INFO)
    console_handler.setFormatter(logging.Formatter("%(asctime)s - %(levelname)s - %(message)s"))
    
    # Add custom filter to ignore specific messages
    ignore_filter = IgnoreSpecificMessagesFilter()
    file_handler.addFilter(ignore_filter)
    console_handler.addFilter(ignore_filter)
    
    # Add handlers to logger
    logger.addHandler(file_handler)
    logger.addHandler(console_handler)
    
    return logger
