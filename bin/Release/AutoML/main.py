import utils.config_loader
import utils.data_handler
import utils.logger_handler
import utils.arguments_handler
import os
import utils.time_handler
utils.config_loader.load_config()
from utils.config_loader import config

def main():
    
    args = utils.arguments_handler.parse_arguments()
    if(args.manual):
        import manual.manual_models_switch
        logger = utils.logger_handler.setup_logging("manual")
        start_time = utils.time_handler.start_script_logging(logger)
        #if not os.path.exists(config.TRAIN_CSV_FILE_PATH):
         #   logger.error("CSV file does not exists!")
          #  quit()
        
        try:
            manual.manual_models_switch.models_switch(args, logger)

        except Exception as e:
            logger.error(f"An error occurred: {e}", exc_info=True)
        finally:
            utils.time_handler.end_script_logging(logger, start_time)
    elif(args.auto):
        import auto.auto_models_switch
        logger = utils.logger_handler.setup_logging("auto")
        start_time = utils.time_handler.start_script_logging(logger)
        if not os.path.exists(config.TRAIN_CSV_FILE_PATH):
            logger.error("CSV file does not exists!")
            quit()
        
        try:
            auto.auto_models_switch.models_switch(args, logger)

        except Exception as e:
            logger.error(f"An error occurred: {e}", exc_info=True)
        finally:
            utils.time_handler.end_script_logging(logger, start_time)

if __name__ == "__main__":
    main()
