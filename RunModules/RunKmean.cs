using AutoMLGUI.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AutoMLGUI.RunModules
{
    internal class RunKmean
    {
        public static void Run(Form form, JObject config, Dictionary<string, Control> controlMap)
        {
            var configToGuiMap = new Dictionary<string, string>
{
    // 🔹 General Settings
    { "LOGGING_PATH", "loggerTextBox" },
    { "OUTPUT_FILE_PATH", "clusterOutputFileTextBox" },
    { "MODEL_PKL_FILE_PATH", "saveModelFilePathTextBox" },
    { "PREDICT_CSV_FILE_PATH", "clusterInputFileTextBox" },

    // 🔹 Model Selection
    { "ML_MODEL_NAME", "modelComboBox" },

    // 🔹 Clustering Options
    { "LIMIT_CLUSTERS_NUMBER", "clustersNumberTextBox" },

    // 🔹 Evaluation and Preprocessing
    { "MODEL_EVALUATIONS_FOLDER_PATH", "evaluationsTextBox" },
    { "PREPROCESSED_DATA_FILE_PATH", "preprocessedDataFileTextBox" },

    // 🔹 Supervised Learning
    // 🔹 KMeans Hyperparameters
    { "MODEL_HYPERPARAMETERS.KMeans.init", "kmeansInitComboBox" },
    { "MODEL_HYPERPARAMETERS.KMeans.max_iter", "kmeansMaxIterTextBox" },
    { "MODEL_HYPERPARAMETERS.KMeans.tol", "kmeansTolComboBox" },
};
            if (!ValidateForm(controlMap))
            {
                MessageBox.Show("Fill all the needed fields!");
                return;
            }
            UpdateAndSaveConfig(config, controlMap, configToGuiMap);
            ProcessHelper.RunAutoMLPython("-m -p");

        }

        public static bool ValidateForm(Dictionary<string, Control> controlMap)
        {
            try
            {
                //MessageBox.Show("🔍 Running ValidateForm...");

                if (!controlMap.TryGetValue("clustersNumberTextBox", out Control clustersNumberControl) ||
                    !(clustersNumberControl is TextBox clustersNumberTextBox) ||
                    !int.TryParse(clustersNumberTextBox.Text, out _))
                {
                    MessageBox.Show("❌ Validation failed: Clusters Number is invalid!");
                    MessageBox.Show("Clusters Number must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //MessageBox.Show("✅ Clusters Number Valid");

                if (!controlMap.TryGetValue("clusterInputFileTextBox", out Control inputFileControl) ||
                    !(inputFileControl is TextBox clusterInputFileTextBox) ||
                    string.IsNullOrWhiteSpace(clusterInputFileTextBox.Text) ||
                    !File.Exists(clusterInputFileTextBox.Text))
                {
                    MessageBox.Show("❌ Validation failed: Input file does not exist!");
                    MessageBox.Show("Input file path is invalid or does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //MessageBox.Show("✅ Input File Path Valid");
                //MessageBox.Show(controlMap["outputFileTextBox"].Text);
                if (!controlMap.TryGetValue("clusterOutputFileTextBox", out Control outputFileControl) ||
                    !(outputFileControl is TextBox clusterOutputFileTextBox) ||
                    string.IsNullOrWhiteSpace(clusterOutputFileTextBox.Text))
                {
                    MessageBox.Show("❌ Validation failed: Output folder is missing!");
                    MessageBox.Show("Output folder path is invalid or does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //MessageBox.Show("✅ Output File Path Valid");

                return true; // ✅ All validations passed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"🚨 Unexpected Error in ValidateForm: {ex.Message}");
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void UpdateAndSaveConfig(JObject config, Dictionary<string, Control> controlMap, Dictionary<string, string> configToGuiMap)
        {
            try
            {
                // 🔄 Loop through all mappings and update config with UI values
                foreach (var kvp in configToGuiMap)
                {
                    string jsonKey = kvp.Key;
                    string controlName = kvp.Value;

                    if (!controlMap.TryGetValue(controlName, out Control control) || control == null)
                        continue; // Skip if control not found

                    object newValue = null;

                    if (control is TextBox textBox)
                    {
                        newValue = textBox.Text;
                    }
                    else if (control is ComboBox comboBox)
                    {
                        newValue = comboBox.SelectedItem?.ToString() ?? "";
                    }
                    else if (control is CheckBox checkBox)
                    {
                        newValue = checkBox.Checked; // ✅ Store as Boolean
                    }

                    if (newValue != null)
                    {
                        // ✅ Update JSON value using JsonHelper
                        JsonHelper.UpdateJsonValue(config, jsonKey, newValue);
                    }
                }

                JsonHelper.ConfigChoosenOptions(controlMap, config);

                JsonHelper.SaveJsonConfig(config);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"🚨 Error updating config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
