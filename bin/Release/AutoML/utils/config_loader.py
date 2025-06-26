import json
import os
import sys

try:
    import json5  # Try importing json5
    JSON5_AVAILABLE = True
except ImportError:
    JSON5_AVAILABLE = False  # Fallback to standard JSON

class ConfigNamespace:
    """Helper class to allow dot notation access to dictionary keys."""
    def __init__(self, dictionary):
        for key, value in dictionary.items():
            if isinstance(value, dict):
                value = ConfigNamespace(value)  # Recursively convert sub-dictionaries
            setattr(self, key, value)

    def __getitem__(self, key):
        return getattr(self, key)

def load_config():
    """Load the configuration file, preferring JSON5 if available, otherwise falling back to JSON."""
    json5_path = "config.json5"
    json_path = "config.json"

    # ✅ Try loading config.json5 first (only if the file exists)
    if JSON5_AVAILABLE and os.path.exists(json5_path):
        try:
            with open(json5_path, 'r', encoding="utf-8") as f:
                config_dict = json5.load(f)
            print("Loaded configuration from config.json5")
            return ConfigNamespace(config_dict)  # ✅ Allows dot notation
        except Exception as e:
            print(f"Warning: Error loading JSON5 file: {e}. Falling back to JSON.")

    # ✅ Try loading config.json if JSON5 fails or doesn't exist
    if os.path.exists(json_path):
        try:
            with open(json_path, 'r', encoding="utf-8") as f:
                config_dict = json.load(f)
            print("Loaded configuration from config.json")
            return ConfigNamespace(config_dict)  # ✅ Allows dot notation
        except Exception as e:
            raise Exception(f"Error loading JSON file ({json_path}): {e}")

    # 🚨 If both files are missing, raise an error
    raise FileNotFoundError("No configuration file found (expected 'config.json5' or 'config.json').")

# ✅ Load configuration globally
try:
    config = load_config()
except Exception as e:
    print(f"Critical error: {e}", file=sys.stderr)
