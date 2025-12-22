using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AutoMLGUI.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoMLGUI
{

    internal class Init
    {
        public static JObject LoadConfig(Form form, Dictionary<string, Control> controlMap)
        {
            DisableComboBoxEditing(form); // Apply to all ComboBoxes in the form
            try
            {
                JObject jsonData = JsonHelper.LoadJsonConfig();
                var configToGuiMap = new Dictionary<string, string>
                {
                    // 🔹 General Settings
                    { "SUPERVISED_GROUP_COLUMN_NAME", "classificationTrainSupervisedNameTextBox" },
                    { "SUPERVISED_GROUP_COLUMN_INDEX", "classificationTrainSupervisedIndexTextBox" },
                    { "LOGGING_PATH", "loggerTextBox" },
                    { "TRAIN_CSV_FILE_PATH", "classificationTrainInputTextBox" },
                    { "OUTPUT_FILE_PATH", "clusterOutputFileTextBox" },
                    { "MODEL_PKL_FILE_PATH", "classificationTrainPKLOutputTextBox" },
                    { "PREDICT_CSV_FILE_PATH", "classificationPredictInputFileTextBox" },
                    { "CLUSTERING_INPUT_FILE_PATH", "clusterInputFileTextBox" },
                    // Model Selection
                    { "ML_MODEL_NAME", "modelComboBox" },

                    // 🔹 Clustering Options
                    { "LIMIT_CLUSTERS_NUMBER", "clustersNumberTextBox" },

                    // 🔹 Column Removal Options
                    { "COLUMNS_TO_REMOVE_BY_NAME", "removeColumnsTextBox" },
                    { "COLUMNS_TO_REMOVE_BY_INDEX", "removeColumnsByIndexRadioBox" },
                    { "COLUMNS_TO_REMOVE_USING_REGEX", "removeColumnsByRegexRadioBox" },

                    // 🔹 Evaluation and Preprocessing
                    { "MODEL_EVALUATIONS_FOLDER_PATH", "evaluationsTextBox" },
                    { "PREPROCESSED_DATA_FILE_PATH", "preprocessedDataFileTextBox" },

                    // 🔹 Search Strategy
                    { "SEARCH_STRATEGY", "searchStrategyComboBox" },
                    { "MODELS_TO_TRY", "modelsToTryListBox" },
                    { "MAXIMUM_TIME_PER_MODEL", "maxTimePerModelTextBox" },

                    // 🔹 GridSearchCV Settings
                    { "GRIDSEARCHCV_CV", "gridSearchCVTextBox" },
                    { "SCORING_METRIC", "gridSearchScoringMetricComboBox" },
                    { "GRIDSEARCHCV_N_JOBS", "gridSearchNJobsTextBox" },
                    { "GRIDSEARCHCV_VERBOSE", "gridSearchVerboseComboBox" },
                    { "REFIT_BEST_MODEL", "gridSearchReFitBestModelCheckBox" },

                    // 🔹 Bayesian Optimization Settings
                    { "BAYESIANOPT_N_ITER", "bayesianNIterTextBox" },
                    { "BAYESIANOPT_N_INITIAL_POINTS", "bayesianNInitialPointsTextBox" },
                    { "BAYESIANOPT_ACQUISITION_FUNCTION", "bayesianAcquisitionFunctionComboBox" },
                    { "BAYESIANOPT_RANDOM_STATE", "bayesianRandomStateTextBox" },
                    { "BAYESIANOPT_N_JOBS", "bayesianNJobsTextBox" },
                    { "BAYESIANOPT_VERBOSE", "bayesianVerboseComboBox" },

                    // 🔹 Supervised Learning
                    { "SUPERVISED_MODEL", "supervisedModelCheckBox" },

                    // 🔹 Decision Tree Hyperparameters
                    { "MODEL_HYPERPARAMETERS.DecisionTree.max_depth", "decisionTreeMaxDepthTextBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.min_samples_split", "decisionTreeMinSamplesSplitTextBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.min_samples_leaf", "decisionTreeMinSamplesLeafTextBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.criterion", "decisionTreeCriterionComboBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.splitter", "decisionTreeSplitterComboBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.max_features", "decisionTreeMaxFeaturesComboBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.min_impurity_decrease", "decisionTreeMinImpurityDecreaseTextBox" },
                    { "MODEL_HYPERPARAMETERS.DecisionTree.random_state", "decisionTreeRandomStateTextBox" },

                    // 🔹 KMeans Hyperparameters
                    { "MODEL_HYPERPARAMETERS.KMeans.init", "kmeansInitComboBox" },
                    { "MODEL_HYPERPARAMETERS.KMeans.max_iter", "kmeansMaxIterTextBox" },
                    { "MODEL_HYPERPARAMETERS.KMeans.tol", "kmeansTolComboBox" },

                    // 🔹 Lightgbm Hyperparameters (if needed, update with actual UI elements)
                    { "MODEL_HYPERPARAMETERS.LightGBM.num_leaves", "lightgbmNumLeavesTextBox" },
                    { "MODEL_HYPERPARAMETERS.LightGBM.max_depth", "lightgbmMaxDepthTextBox" },
                    { "MODEL_HYPERPARAMETERS.LightGBM.learning_rate", "lightgbmLearningRateTextBox" },
                    { "MODEL_HYPERPARAMETERS.LightGBM.n_estimators", "lightgbmNEstimatorsTextBox" },
                    { "MODEL_HYPERPARAMETERS.LightGBM.subsample", "lightgbmSubsampleTextBox" },
                };
                form.SuspendLayout(); // ⏳ Optimize UI updates

                foreach (var kvp in configToGuiMap)
                {
                    try
                    {
                        string jsonPath = kvp.Key;  // JSON key path
                        string controlName = kvp.Value;  // UI control name

                        // 🔍 Get JSON value dynamically
                        JToken token = jsonData.SelectToken(jsonPath);

                        // 🔥 Find the control using controlMap
                        if (controlMap.TryGetValue(controlName, out Control control))
                        {
                            SetControlValue(control, token);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing key: {kvp.Key}, Error: {ex.Message}");
                    }
                }

                form.ResumeLayout(); // ✅ Resume UI updates
                return jsonData; // ✅ Return the loaded JSON config
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new JObject();
            }
        }


        public static void SetControlValue(Control control, JToken token)
        {
            if (control == null)
                return; // ✅ Skip if control is not found

            try
            {
                string value = "";

                if (token != null)
                {
                    if (token.Type == JTokenType.Array)
                    {
                        // 🔥 Convert array to comma-separated string
                        value = string.Join(", ", token.ToObject<List<string>>() ?? new List<string>());
                    }
                    else
                    {
                        value = token.ToString();
                    }
                }

                // 🔹 Update control based on its type
                if (control is TextBox textBox)
                {
                    textBox.Text = value; // ✅ Keep empty if null
                }
                else if (control is Label label)
                {
                    label.Text = value; // ✅ Keep empty if null
                }
                else if (control is ComboBox comboBox)
                {
                    // 🔍 Check if the value exists in the ComboBox
                    int index = comboBox.Items.IndexOf(value);
                    if (index >= 0)
                    {
                        comboBox.SelectedIndex = index; // ✅ Set correct index
                    }
                    else if (comboBox.Items.Count > 0)
                    {
                        comboBox.SelectedIndex = 0; // 🚨 Default to first item if not found
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting value for control {control.Name}: {ex.Message}");
            }
        }


        public static Control FindControl(Control parent, string name)
        {
            try
            {
                foreach (Control control in parent.Controls)
                {
                    if (control.Name == name) return control;
                    if (control.HasChildren)
                    {
                        Control found = FindControl(control, name);
                        if (found != null) return found;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding control {name}: {ex.Message}");
            }
            return null;
        }


        public static void DisableComboBoxEditing(Control parent)
        {
            try
            {
                foreach (Control control in parent.Controls)
                {
                    if (control is ComboBox comboBox)
                    {
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // ✅ Prevent manual text input
                    }

                    // 🔄 Recursively apply to nested panels or group boxes
                    if (control.HasChildren)
                    {
                        DisableComboBoxEditing(control);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting ComboBox readonly mode: {ex.Message}");
            }
        }

    }
}
