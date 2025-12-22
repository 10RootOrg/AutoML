using AutoMLGUI.Helpers;
using AutoMLGUI.RunModules;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AutoMLGUI
{
    public partial class Form1 : Form
    {
        JObject config;
        public Dictionary<string, Control> controlMap = new Dictionary<string, Control>();


        public Form1()
        {
            InitializeComponent();
            ControlMapHelper.FillControlMap(this, controlMap);
            config = Init.LoadConfig(this, controlMap);
            FixPanelsLocation();
            FixLoadObjects();
        }



        private void FixPanelsLocation()
        {
            classificationTrainPanel.Location = clusteringPanel.Location;
            classificationPredictPanel.Location = clusteringPanel.Location;
        }

        private void classificationRadioBox_CheckedChanged(object sender, EventArgs e)
        {
            if (classificationRadioBox.Checked)
            {
                clusteringRadioBox.Checked = false;
                clusteringPanel.Visible = false;
                classificationTrainPanel.Visible = true;
                decisionTreePanel.Visible = true;
                kmeansPanel.Visible = false;
                trainPredictPanel.Visible = true;

                // Update ComboBox
                UpdateModelComboBox(true);

            }
        }

        private void clusteringRadioBox_CheckedChanged(object sender, EventArgs e)
        {
            if (clusteringRadioBox.Checked)
            {
                classificationRadioBox.Checked = false;
                clusteringPanel.Visible = true;
                classificationTrainPanel.Visible = false;
                decisionTreePanel.Visible = true;
                kmeansPanel.Visible = false;
                trainPredictPanel.Visible = false;

                // Update ComboBox
                UpdateModelComboBox(false);

            }
        }


        private void UpdateModelComboBox(bool isClassification)
        {
            List<string> models = new List<string> { "DecisionTree" }; // Always include DecisionTree

            if (isClassification)
            {
                models.Add("LightGBM"); // Add LightGBM for classification
                models.Add("GridSearch");
                models.Add("BayesianOpt");
            }
            else
            {
                models.Add("KMeans"); // Add K-Means for clustering
            }

            modelComboBox.Items.Clear();
            modelComboBox.Items.AddRange(models.ToArray());

            // Ensure a default selection
            modelComboBox.SelectedIndex = 0;
        }

        private void modelComboBox_TextChanged(object sender, EventArgs e)
        {
            if (modelComboBox.Text == "KMeans")
            {
                kmeansPanel.Visible = true;
                decisionTreePanel.Visible = false;
                lightgbmPanel.Visible = false;
                gridSearchPanel.Visible = false;
                bayesianPanel.Visible = false;
            }
            else if (modelComboBox.Text == "DecisionTree")
            {
                kmeansPanel.Visible = false;
                decisionTreePanel.Visible = true;
                lightgbmPanel.Visible = false;
                gridSearchPanel.Visible = false;
                bayesianPanel.Visible = false;
            }
            else if (modelComboBox.Text == "LightGBM")
            {
                kmeansPanel.Visible = false;
                decisionTreePanel.Visible = false;
                lightgbmPanel.Visible = true;
                gridSearchPanel.Visible = false;
                bayesianPanel.Visible = false; bayesianPanel.Visible = false;
            }
            else if (modelComboBox.Text == "GridSearch")
            {
                kmeansPanel.Visible = false;
                decisionTreePanel.Visible = false;
                lightgbmPanel.Visible = false;
                gridSearchPanel.Visible = true;
                bayesianPanel.Visible = false;
            }
            else if (modelComboBox.Text == "BayesianOpt")
            {
                kmeansPanel.Visible = false;
                decisionTreePanel.Visible = false;
                lightgbmPanel.Visible = false;
                gridSearchPanel.Visible = false;
                bayesianPanel.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RunButton_MouseClick(object sender, MouseEventArgs e)
        {
            if(clusteringRadioBox.Checked && modelComboBox.Text == "KMeans")
            {
                RunKmean.Run(this, config, controlMap);
            }
            else if(clusteringRadioBox.Checked && modelComboBox.Text == "DecisionTree")
            {
                RunClusteringDecisionTree.Run(this, config, controlMap);
            }
            else if (classificationRadioBox.Checked && modelComboBox.Text == "DecisionTree")
            {
                RunClassificationDecisionTree.Run(this, config, controlMap);
            }
            else if (classificationRadioBox.Checked && modelComboBox.Text == "LightGBM")
            {
                RunLightGBM.Run(this, config, controlMap);
            }
            else if (classificationRadioBox.Checked && modelComboBox.Text == "GridSearch")
            {
                RunGridSearch.Run(this, config, controlMap);
            }
            else if (classificationRadioBox.Checked && modelComboBox.Text == "BayesianOpt")
            {
                RunBayesianOpt.Run(this, config, controlMap);
            }
        }

        private void classificationPredictRadioBox_CheckedChanged(object sender, EventArgs e)
        {
            if(classificationPredictRadioBox.Checked)
            {
                classificationPredictPanel.Visible = true;
                classificationTrainPanel.Visible = false;
            }
        }

        private void classificationTrainRadioBox_CheckedChanged(object sender, EventArgs e)
        {
            if(classificationTrainRadioBox.Checked)
            {
                classificationPredictPanel.Visible = false;
                classificationTrainPanel.Visible = true;
            }

        }

        private void FixLoadObjects()
        {
            // Input files are independent - clustering uses CLUSTERING_INPUT_FILE_PATH, classification uses PREDICT_CSV_FILE_PATH
            // Only sync output files
            classificationPredictOutputFileTextBox.Text = clusterOutputFileTextBox.Text;
            classificationPredictPKLFileTextBox.Text = classificationTrainPKLOutputTextBox.Text;
        }
    }
}
