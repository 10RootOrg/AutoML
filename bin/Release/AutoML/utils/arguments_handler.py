import argparse
import sys

def parse_arguments():
    """Set up argument parser for training, prediction, and hyperparameter tuning modes."""
    parser = argparse.ArgumentParser(
        description="Script for model training, prediction, and hyperparameter tuning."
    )
    
    # Main operation mode group
    mode_group = parser.add_mutually_exclusive_group()
    mode_group.add_argument(
        "-t", "--train",
        action="store_true",
        help="Train the model."
    )
    mode_group.add_argument(
        "-p", "--predict",
        action="store_true",
        help="Run predictions using the model."
    )

    # Tuning mode group (only relevant if --train is selected)
    tuning_group = parser.add_mutually_exclusive_group()
    tuning_group.add_argument(
        "-a", "--auto",
        action="store_true",
        help="Use automatic hyperparameter tuning (GridSearch, RandomSearch, Bayesian, or Hyperband)."
    )
    tuning_group.add_argument(
        "-m", "--manual",
        action="store_true",
        help="Use manual hyperparameter settings from configuration file."
    )

    try:
        # Parse arguments
        args = parser.parse_args()
        # If no valid arguments are provided, print help message and quit
        if not any([args.train, args.predict]):
            print("Error: No valid operation mode provided. Use --help to see available options")
            print("\nAvailable options:")
            print(parser.format_help())
            sys.exit(1)

        # If training mode is selected but no tuning option is specified
        if args.train and not any([args.auto, args.manual]):
            print("Error: Training mode requires specifying either -a/--auto or -m/--manual for hyperparameter tuning")
            print("\nAvailable options:")
            print(parser.format_help())
            sys.exit(1)

    except SystemExit:
        # Catch invalid arguments and print help
        print("Error: Invalid argument provided. Use --help to see available options")
        print("\nAvailable options:")
        print(parser.format_help())
        sys.exit(1)

    return args