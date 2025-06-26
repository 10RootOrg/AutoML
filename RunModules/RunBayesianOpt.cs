using AutoMLGUI.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AutoMLGUI.RunModules
{
    internal class RunBayesianOpt
    {
        public static void Run(Form form, JObject config, Dictionary<string, Control> controlMap)
        {
            var configToGuiMap = new Dictionary<string, string>
                {
                    // 🔹 General Settings
                    { "SUPERVISED_GROUP_COLUMN_NAME", "supervisedGroupColumnNameTextBox" },
                    { "SUPERVISED_GROUP_COLUMN_INDEX", "supervisedGroupColumnIndexTextBox" },
                    { "LOGGING_PATH", "loggerTextBox" },
                    { "TRAIN_CSV_FILE_PATH", "classificationTrainInputTextBox" },
                    { "OUTPUT_FILE_PATH", "outputFileTextBox" },
                    { "MODEL_PKL_FILE_PATH", "classificationTrainPKLOutputTextBox" },
                    { "PREDICT_CSV_FILE_PATH", "predictCsvFilePathTextBox" },

                    // 🔹 Model Selection
                    { "ML_MODEL_NAME", "modelComboBox" },

                    // 🔹 Column Removal Options
                    { "COLUMNS_TO_REMOVE_BY_NAME", "removeColumnsByNameRadioBox" },
                    { "COLUMNS_TO_REMOVE_BY_INDEX", "removeColumnsByIndexRadioBox" },
                    { "COLUMNS_TO_REMOVE_USING_REGEX", "removeColumnsByRegexRadioBox" },

                    // 🔹 Evaluation and Preprocessing
                    { "MODEL_EVALUATIONS_FOLDER_PATH", "evaluationsTextBox" },
                    { "PREPROCESSED_DATA_FILE_PATH", "preprocessedDataFileTextBox" },

                    { "BAYESIANOPT_N_ITER", "bayesianNIterTextBox" },
                    { "BAYESIANOPT_N_INITIAL_POINTS", "bayesianNInitialPointsTextBox" },
                    { "BAYESIANOPT_ACQUISITION_FUNCTION", "bayesianAcquisitionFunctionComboBox" },
                    { "BAYESIANOPT_RANDOM_STATE", "bayesianRandomStateTextBox" },
                    { "BAYESIANOPT_N_JOBS", "bayesianNJobsTextBox" },
                    { "BAYESIANOPT_VERBOSE", "bayesianVerboseComboBox" },
                };
            if (!ValidateForm(controlMap))
            {
                MessageBox.Show("Fill all the needed fields!");
                return;
            }

            UpdateAndSaveConfig(config, controlMap, configToGuiMap);

            if ((controlMap["classificationTrainRadioBox"] as RadioButton)?.Checked == true)
            {
                ProcessHelper.RunAutoMLPython("-a -t");
            }
            else
            {
                ProcessHelper.RunAutoMLPython("-a -p");
            }
        }

        public static bool ValidateForm(Dictionary<string, Control> controlMap)
        {
            try
            {
                // Define required integer fields (TextBoxes) for GridSearch and BayesianOpt
                var integerFields = new Dictionary<string, string>
        {
            { "BAYESIANOPT_N_ITER", "bayesianNIterTextBox" },
            { "BAYESIANOPT_N_INITIAL_POINTS", "bayesianNInitialPointsTextBox" },
            { "BAYESIANOPT_RANDOM_STATE", "bayesianRandomStateTextBox" },
            { "BAYESIANOPT_N_JOBS", "bayesianNJobsTextBox" }
        };

                // Define required integer fields (ComboBoxes) for GridSearch and BayesianOpt
                var integerComboBoxFields = new Dictionary<string, string>
        {
            { "BAYESIANOPT_VERBOSE", "bayesianVerboseComboBox" }
        };

                // Define required string fields (must not be empty)
                var stringFields = new Dictionary<string, string>
        {
            { "BAYESIANOPT_ACQUISITION_FUNCTION", "bayesianAcquisitionFunctionComboBox" }
        };


                // Validate numeric integer fields (TextBoxes)
                foreach (var field in integerFields)
                {
                    if (!controlMap.TryGetValue(field.Value, out Control control) || !(control is TextBox textBox) || !int.TryParse(textBox.Text, out _))
                    {
                        MessageBox.Show($"❌ Validation failed: {field.Key} must be a valid integer!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // Validate numeric integer fields (ComboBoxes)
                foreach (var field in integerComboBoxFields)
                {
                    if (!controlMap.TryGetValue(field.Value, out Control control) || !(control is ComboBox comboBox) || !int.TryParse(comboBox.Text, out _))
                    {
                        MessageBox.Show($"❌ Validation failed: {field.Key} must be a valid integer (selected from dropdown)!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // Validate non-empty string fields (ComboBoxes)
                foreach (var field in stringFields)
                {
                    if (!controlMap.TryGetValue(field.Value, out Control control) || !(control is ComboBox comboBox) || string.IsNullOrWhiteSpace(comboBox.Text))
                    {
                        MessageBox.Show($"❌ Validation failed: {field.Key} must be selected!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                return true; // ✅ All validations passed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"🚨 Unexpected Error in ValidateForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void UpdateAndSaveConfig(JObject config, Dictionary<string, Control> controlMap, Dictionary<string, string> configToGuiMap)
        {
            try
            {
                foreach (var kvp in configToGuiMap)
                {
                    string jsonKey = kvp.Key;
                    string controlName = kvp.Value;

                    if (!controlMap.TryGetValue(controlName, out Control control) || control == null)
                        continue;

                    object newValue = null;

                    if (control is TextBox textBox)
                    {
                        if (int.TryParse(textBox.Text, out int intValue))
                        {
                            newValue = intValue; // ✅ Convert to integer if applicable
                        }
                        else
                        {
                            newValue = textBox.Text;
                        }
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
