using AutoMLGUI.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AutoMLGUI.RunModules
{
    internal class RunClusteringDecisionTree
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

                    // 🔹 Decision Tree Hyperparameters
                    { "MODEL_HYPERPARAMETERS.DecisionTree.max_depth", "decisionTreeMaxDepthTextBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.min_samples_split", "decisionTreeMinSamplesSplitTextBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.min_samples_leaf", "decisionTreeMinSamplesLeafTextBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.criterion", "decisionTreeCriterionComboBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.splitter", "decisionTreeSplitterComboBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.max_features", "decisionTreeMaxFeaturesComboBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.min_impurity_decrease", "decisionTreeMinImpurityDecreaseTextBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.random_state", "decisionTreeRandomStateTextBox" },
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
                if (!controlMap.TryGetValue("decisionTreeMaxDepthTextBox", out Control maxDepthControl) ||
                    !(maxDepthControl is TextBox maxDepthTextBox) ||
                    !int.TryParse(maxDepthTextBox.Text, out _))
                {
                    MessageBox.Show("❌ Validation failed: Max Depth is invalid!");
                    return false;
                }

                if (!controlMap.TryGetValue("decisionTreeMinSamplesSplitTextBox", out Control minSamplesSplitControl) ||
                    !(minSamplesSplitControl is TextBox minSamplesSplitTextBox) ||
                    !int.TryParse(minSamplesSplitTextBox.Text, out _))
                {
                    MessageBox.Show("❌ Validation failed: Min Samples Split is invalid!");
                    return false;
                }

                if (!controlMap.TryGetValue("decisionTreeMinSamplesLeafTextBox", out Control minSamplesLeafControl) ||
                    !(minSamplesLeafControl is TextBox minSamplesLeafTextBox))
                {
                    MessageBox.Show("❌ Min Samples Leaf text box not found in controlMap!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string minSamplesLeafValue = minSamplesLeafTextBox.Text;
                if (!int.TryParse(minSamplesLeafValue, out _))
                {
                    MessageBox.Show($"❌ Validation failed: Min Samples Leaf is invalid! Value: {minSamplesLeafValue}");
                    return false;
                }

                return true; // ✅ All validations passed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"🚨 Unexpected Error in ValidateForm: {ex.Message}");
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
