using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoMLGUI.Helpers
{
    internal class JsonHelper
    {
        private static readonly string filePath = Path.Combine(PathHelper.AutoMLDirectory, "config.json");

        /// <summary>
        /// Loads JSON configuration from file
        /// Paths are kept as-is (relative or absolute)
        /// </summary>
        public static JObject LoadJsonConfig()
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Config file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new JObject();
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                return JObject.Parse(jsonContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new JObject();
            }
        }

        /// <summary>
        /// Saves a new JSON configuration to file (overwrites existing)
        /// </summary>
        public static bool SaveJsonConfig(JObject newConfig)
        {
            try
            {
                // Normalize paths inside the JSON object
                NormalizePaths(newConfig);

                // Convert JSON to a formatted string
                string formattedJson = JsonConvert.SerializeObject(newConfig, Formatting.Indented);

                // Save the updated JSON string
                File.WriteAllText(filePath, formattedJson);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void NormalizePaths(JToken token, bool convertToRelative = true)
        {
            if (token is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    NormalizePaths(property.Value, convertToRelative);
                }
            }
            else if (token is JArray array)
            {
                foreach (var item in array)
                {
                    NormalizePaths(item, convertToRelative);
                }
            }
            else if (token.Type == JTokenType.String)
            {
                string value = token.ToString();
                string sanitized = SanitizePathString(value);

                // Convert absolute paths to relative for storage
                if (convertToRelative && PathHelper.LooksLikePath(sanitized))
                {
                    sanitized = PathHelper.ToRelativePath(sanitized);
                }

                if (value != sanitized)
                {
                    ((JValue)token).Value = sanitized;
                }
            }
        }

        /// <summary>
        /// Sanitizes a string for safe use with Python by:
        /// - Converting backslashes to forward slashes
        /// - Removing problematic Unicode characters
        /// - Removing control characters
        /// - Normalizing quotes
        /// </summary>
        public static string SanitizePathString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Step 1: Replace backslashes with forward slashes (for path compatibility)
            string result = input.Replace("\\", "/");

            // Step 2: Remove or replace problematic Unicode characters
            // Keep only ASCII printable characters (32-126) plus common extended chars
            StringBuilder sb = new StringBuilder();
            foreach (char c in result)
            {
                if (c >= 32 && c <= 126)
                {
                    // Standard ASCII printable characters - keep as is
                    sb.Append(c);
                }
                else if (c == '\t' || c == '\n' || c == '\r')
                {
                    // Keep common whitespace characters
                    sb.Append(c);
                }
                else if (c >= 128 && c <= 255)
                {
                    // Extended ASCII - try to keep common accented chars but skip problematic ones
                    // Skip chars that commonly cause cp1252 encoding issues
                    if (!IsProblematicUnicodeChar(c))
                    {
                        sb.Append(c);
                    }
                }
                // Skip any other Unicode characters (emojis, special symbols, etc.)
            }

            result = sb.ToString();

            // Step 3: Normalize quotes - replace smart quotes with regular quotes
            // Left double quote, Right double quote
            result = result.Replace('\u201C', '"').Replace('\u201D', '"');
            // Left single quote, Right single quote
            result = result.Replace('\u2018', '\'').Replace('\u2019', '\'');

            // Step 4: Remove any double forward slashes (except for protocol like http://)
            result = Regex.Replace(result, @"(?<!:)/{2,}", "/");

            return result;
        }

        /// <summary>
        /// Checks if a Unicode character is problematic for Python's default encoding
        /// </summary>
        private static bool IsProblematicUnicodeChar(char c)
        {
            // Characters that commonly cause UnicodeEncodeError with cp1252
            // Block characters, special Unicode symbols, etc.
            int code = (int)c;

            // Block drawing characters (used in progress bars)
            if (code >= 0x2580 && code <= 0x259F) return true;

            // Box drawing characters
            if (code >= 0x2500 && code <= 0x257F) return true;

            // Geometric shapes
            if (code >= 0x25A0 && code <= 0x25FF) return true;

            // Miscellaneous symbols
            if (code >= 0x2600 && code <= 0x26FF) return true;

            // Dingbats
            if (code >= 0x2700 && code <= 0x27BF) return true;

            // Emoji range
            if (code >= 0x1F300 && code <= 0x1F9FF) return true;

            return false;
        }


        public static void UpdateJsonValue(JObject config, string jsonPath, object newValue)
        {
            try
            {
                JToken token = config.SelectToken(jsonPath);
                if (token != null)
                {
                    token.Replace(JToken.FromObject(newValue));
                }
                else
                {
                    // Handle nested properties manually if SelectToken fails
                    string[] parts = jsonPath.Split('.');
                    JObject current = config;

                    for (int i = 0; i < parts.Length - 1; i++)
                    {
                        if (!current.ContainsKey(parts[i]))
                            current[parts[i]] = new JObject();

                        current = (JObject)current[parts[i]];
                    }

                    current[parts[parts.Length -1]] = JToken.FromObject(newValue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error updating JSON key: {jsonPath}, Error: {ex.Message}");
            }
        }

        public static void ConfigChoosenOptions(Dictionary<string, Control> controlMap, JObject config)
        {
            // Check if classification mode is selected
            if (controlMap.TryGetValue("classificationRadioBox", out Control classificationControl) &&
                classificationControl is RadioButton classificationRadioButton)
            {
                config["SUPERVISED_MODEL"] = classificationRadioButton.Checked;
            }
            else
            {
                config["SUPERVISED_MODEL"] = false; // Default to false if not found
            }

            // Check which columns to remove
            config["COLUMNS_TO_REMOVE_BY_INDEX"] = new JArray(); // Empty list
            config["COLUMNS_TO_REMOVE_BY_NAME"] = "";
            config["COLUMNS_TO_REMOVE_USING_REGEX"] = "";
            if ((controlMap["removeColumnsByIndexRadioBox"] as RadioButton)?.Checked == true)
            {
                var textInput = (controlMap["removeColumnsTextBox"] as TextBox)?.Text ?? "";

                // Debug output to check raw input
                Console.WriteLine($"Raw input: {textInput}");

                // Parse the input into a list of integers
                var indexList = textInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(s => s.Trim())  // Remove extra spaces
                                         .Where(s => int.TryParse(s, out _))  // Filter valid integers
                                         .Select(s => int.Parse(s))  // Convert to int
                                         .ToList();

                // Debug output to confirm correct parsing
                Console.WriteLine($"Parsed List: {string.Join(", ", indexList)}");

                // Store as a JSON array
                config["COLUMNS_TO_REMOVE_BY_INDEX"] = JArray.FromObject(indexList);
            }
            else if ((controlMap["removeColumnsByNameRadioBox"] as RadioButton)?.Checked == true)
            {
                var textInput = (controlMap["removeColumnsTextBox"] as TextBox)?.Text ?? "";
                config["COLUMNS_TO_REMOVE_BY_NAME"] = textInput; // Store as a string
            }
            else if ((controlMap["removeColumnsByRegexRadioBox"] as RadioButton)?.Checked == true)
            {
                var textInput = (controlMap["removeColumnsTextBox"] as TextBox)?.Text ?? "";
                config["COLUMNS_TO_REMOVE_USING_REGEX"] = textInput; // Store as a string
            }


        }

    }
}
