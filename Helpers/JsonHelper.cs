using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoMLGUI.Helpers
{
    internal class JsonHelper
    {
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "AutoML", "config.json");

        /// <summary>
        /// ✅ Loads JSON configuration from file
        /// </summary>
        public static JObject LoadJsonConfig()
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Config file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new JObject(); // Return an empty JSON object if missing
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                return JObject.Parse(jsonContent); // ✅ Parse JSON safely
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new JObject();
            }
        }

        /// <summary>
        /// ✅ Saves a new JSON configuration to file (overwrites existing)
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
                File.WriteAllText(filePath, formattedJson); // ✅ Overwrite the config file
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void NormalizePaths(JToken token)
        {
            if (token is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    NormalizePaths(property.Value); // Recursively process child elements
                }
            }
            else if (token is JArray array)
            {
                foreach (var item in array)
                {
                    NormalizePaths(item); // Process array elements
                }
            }
            else if (token.Type == JTokenType.String)
            {
                string value = token.ToString();
                if (value.Contains("\\"))
                {
                    ((JValue)token).Value = value.Replace("\\", "/"); // Replace only in strings
                }
            }
        }


        public static void UpdateJsonValue(JObject config, string jsonPath, object newValue)
        {
            try
            {
                JToken token = config.SelectToken(jsonPath);
                if (token != null)
                {
                    token.Replace(JToken.FromObject(newValue)); // ✅ Replace with correct type
                }
                else
                {
                    // 🔥 Handle nested properties manually if SelectToken fails
                    string[] parts = jsonPath.Split('.');
                    JObject current = config;

                    for (int i = 0; i < parts.Length - 1; i++)
                    {
                        if (!current.ContainsKey(parts[i]))
                            current[parts[i]] = new JObject();

                        current = (JObject)current[parts[i]];
                    }

                    current[parts[parts.Length -1]] = JToken.FromObject(newValue); // ✅ Assign final value
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error updating JSON key: {jsonPath}, Error: {ex.Message}");
            }
        }

        public static void ConfigChoosenOptions(Dictionary<string, Control> controlMap, JObject config)
        {
            // ✅ Check if classification mode is selected
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
