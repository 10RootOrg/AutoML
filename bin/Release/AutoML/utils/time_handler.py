import time
from datetime import datetime

def start_script_logging(logger):
    """
    Logs the start time of the script and returns the start time.

    Args:
        logger: A logger instance for logging the start time.

    Returns:
        float: The start time in seconds since the epoch.
    """
    start_time = time.time()
    logger.info("Script started.")
    return start_time

def end_script_logging(logger, start_time):
    """
    Logs the total duration of the script.

    Args:
        logger: A logger instance for logging the duration.
        start_time: The start time in seconds since the epoch.
    """
    end_time = time.time()
    duration = end_time - start_time
    logger.info(f"Script finished in {duration:.2f} seconds.")
