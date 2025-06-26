namespace AutoMLGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.clusteringPanel = new System.Windows.Forms.Panel();
            this.clustersNumberTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.clusterInputFileTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.clusterOutputFileTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.kmeansPanel = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.kmeansInitComboBox = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.kmeansTolComboBox = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.kmeansMaxIterTextBox = new System.Windows.Forms.TextBox();
            this.decisionTreePanel = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.decisionTreeRandomStateTextBox = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.decisionTreeMinImpurityDecreaseTextBox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.decisionTreeMaxFeaturesComboBox = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.decisionTreeSplitterComboBox = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.decisionTreeMinSamplesLeafTextBox = new System.Windows.Forms.TextBox();
            this.decisionTreeCriterionComboBox = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.decisionTreeMinSamplesSplitTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.decisionTreeMaxDepthTextBox = new System.Windows.Forms.TextBox();
            this.RunButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.modelComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clusteringRadioBox = new System.Windows.Forms.RadioButton();
            this.classificationRadioBox = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.loggerTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.removeColumnsTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.removeColumnsByNameRadioBox = new System.Windows.Forms.RadioButton();
            this.removeColumnsByIndexRadioBox = new System.Windows.Forms.RadioButton();
            this.removeColumnsByRegexRadioBox = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.evaluationsTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.preprocessingRadioGroup = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.classificationTrainPanel = new System.Windows.Forms.Panel();
            this.classificationTrainSupervisedNameTextBox = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.classificationTrainPKLOutputTextBox = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.classificationTrainInputTextBox = new System.Windows.Forms.TextBox();
            this.classificationTrainSupervisedIndexTextBox = new System.Windows.Forms.TextBox();
            this.classificationPredictRadioBox = new System.Windows.Forms.RadioButton();
            this.classificationTrainRadioBox = new System.Windows.Forms.RadioButton();
            this.classificationPredictPanel = new System.Windows.Forms.Panel();
            this.classificationPredictOutputFileTextBox = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.classificationPredictPKLFileTextBox = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.classificationPredictInputFileTextBox = new System.Windows.Forms.TextBox();
            this.trainPredictPanel = new System.Windows.Forms.Panel();
            this.lightgbmPanel = new System.Windows.Forms.Panel();
            this.lightgbmSubsampleTextBox = new System.Windows.Forms.TextBox();
            this.lightgbmNEstimatorsTextBox = new System.Windows.Forms.TextBox();
            this.lightgbmLearningRateTextBox = new System.Windows.Forms.TextBox();
            this.lightgbmMaxDepthTextBox = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lightgbmNumLeavesTextBox = new System.Windows.Forms.TextBox();
            this.generalSettingsPanel = new System.Windows.Forms.Panel();
            this.gridSearchPanel = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.gridSearchScoringMetricComboBox = new System.Windows.Forms.ComboBox();
            this.gridSearchReFitBestModelCheckBox = new System.Windows.Forms.CheckBox();
            this.gridSearchCVTextBox = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.gridSearchNJobsTextBox = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.gridSearchVerboseComboBox = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.bayesianPanel = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.bayesianNInitialPointsTextBox = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.bayesianNIterTextBox = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.bayesianAcquisitionFunctionComboBox = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.bayesianNJobsTextBox = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.bayesianVerboseComboBox = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.bayesianRandomStateTextBox = new System.Windows.Forms.TextBox();
            this.clusteringPanel.SuspendLayout();
            this.kmeansPanel.SuspendLayout();
            this.decisionTreePanel.SuspendLayout();
            this.preprocessingRadioGroup.SuspendLayout();
            this.classificationTrainPanel.SuspendLayout();
            this.classificationPredictPanel.SuspendLayout();
            this.trainPredictPanel.SuspendLayout();
            this.lightgbmPanel.SuspendLayout();
            this.generalSettingsPanel.SuspendLayout();
            this.gridSearchPanel.SuspendLayout();
            this.bayesianPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "AutoML";
            // 
            // clusteringPanel
            // 
            this.clusteringPanel.Controls.Add(this.clustersNumberTextBox);
            this.clusteringPanel.Controls.Add(this.label10);
            this.clusteringPanel.Controls.Add(this.clusterInputFileTextBox);
            this.clusteringPanel.Controls.Add(this.label11);
            this.clusteringPanel.Controls.Add(this.clusterOutputFileTextBox);
            this.clusteringPanel.Controls.Add(this.label12);
            this.clusteringPanel.Location = new System.Drawing.Point(1, 142);
            this.clusteringPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clusteringPanel.Name = "clusteringPanel";
            this.clusteringPanel.Size = new System.Drawing.Size(820, 151);
            this.clusteringPanel.TabIndex = 1;
            // 
            // clustersNumberTextBox
            // 
            this.clustersNumberTextBox.Location = new System.Drawing.Point(144, 8);
            this.clustersNumberTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clustersNumberTextBox.Name = "clustersNumberTextBox";
            this.clustersNumberTextBox.Size = new System.Drawing.Size(40, 26);
            this.clustersNumberTextBox.TabIndex = 21;
            this.clustersNumberTextBox.Text = "2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "Clusters Number:";
            // 
            // clusterInputFileTextBox
            // 
            this.clusterInputFileTextBox.Location = new System.Drawing.Point(136, 52);
            this.clusterInputFileTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clusterInputFileTextBox.Name = "clusterInputFileTextBox";
            this.clusterInputFileTextBox.Size = new System.Drawing.Size(671, 26);
            this.clusterInputFileTextBox.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 97);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 20);
            this.label11.TabIndex = 22;
            this.label11.Text = "Output file:";
            // 
            // clusterOutputFileTextBox
            // 
            this.clusterOutputFileTextBox.Location = new System.Drawing.Point(136, 97);
            this.clusterOutputFileTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clusterOutputFileTextBox.Name = "clusterOutputFileTextBox";
            this.clusterOutputFileTextBox.Size = new System.Drawing.Size(671, 26);
            this.clusterOutputFileTextBox.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 57);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 20);
            this.label12.TabIndex = 24;
            this.label12.Text = "Input file:";
            // 
            // kmeansPanel
            // 
            this.kmeansPanel.Controls.Add(this.label17);
            this.kmeansPanel.Controls.Add(this.kmeansInitComboBox);
            this.kmeansPanel.Controls.Add(this.label15);
            this.kmeansPanel.Controls.Add(this.kmeansTolComboBox);
            this.kmeansPanel.Controls.Add(this.label16);
            this.kmeansPanel.Controls.Add(this.kmeansMaxIterTextBox);
            this.kmeansPanel.Location = new System.Drawing.Point(832, 96);
            this.kmeansPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.kmeansPanel.Name = "kmeansPanel";
            this.kmeansPanel.Size = new System.Drawing.Size(368, 128);
            this.kmeansPanel.TabIndex = 29;
            this.kmeansPanel.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 92);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 20);
            this.label17.TabIndex = 34;
            this.label17.Text = "Tolerance:";
            // 
            // kmeansInitComboBox
            // 
            this.kmeansInitComboBox.DisplayMember = "0";
            this.kmeansInitComboBox.FormattingEnabled = true;
            this.kmeansInitComboBox.Items.AddRange(new object[] {
            "k-means++",
            "random"});
            this.kmeansInitComboBox.Location = new System.Drawing.Point(148, 46);
            this.kmeansInitComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.kmeansInitComboBox.Name = "kmeansInitComboBox";
            this.kmeansInitComboBox.Size = new System.Drawing.Size(180, 28);
            this.kmeansInitComboBox.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 14);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 20);
            this.label15.TabIndex = 29;
            this.label15.Text = "Max iteration:";
            // 
            // kmeansTolComboBox
            // 
            this.kmeansTolComboBox.DisplayMember = "0";
            this.kmeansTolComboBox.FormattingEnabled = true;
            this.kmeansTolComboBox.Items.AddRange(new object[] {
            "1e-2",
            "1e-3",
            "1e-4",
            "1e-5",
            "1e-6"});
            this.kmeansTolComboBox.Location = new System.Drawing.Point(148, 88);
            this.kmeansTolComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.kmeansTolComboBox.Name = "kmeansTolComboBox";
            this.kmeansTolComboBox.Size = new System.Drawing.Size(180, 28);
            this.kmeansTolComboBox.TabIndex = 33;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 51);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 20);
            this.label16.TabIndex = 31;
            this.label16.Text = "Init:";
            // 
            // kmeansMaxIterTextBox
            // 
            this.kmeansMaxIterTextBox.Location = new System.Drawing.Point(150, 3);
            this.kmeansMaxIterTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.kmeansMaxIterTextBox.Name = "kmeansMaxIterTextBox";
            this.kmeansMaxIterTextBox.Size = new System.Drawing.Size(54, 26);
            this.kmeansMaxIterTextBox.TabIndex = 32;
            this.kmeansMaxIterTextBox.Text = "300";
            // 
            // decisionTreePanel
            // 
            this.decisionTreePanel.Controls.Add(this.label25);
            this.decisionTreePanel.Controls.Add(this.decisionTreeRandomStateTextBox);
            this.decisionTreePanel.Controls.Add(this.label24);
            this.decisionTreePanel.Controls.Add(this.decisionTreeMinImpurityDecreaseTextBox);
            this.decisionTreePanel.Controls.Add(this.label23);
            this.decisionTreePanel.Controls.Add(this.decisionTreeMaxFeaturesComboBox);
            this.decisionTreePanel.Controls.Add(this.label22);
            this.decisionTreePanel.Controls.Add(this.decisionTreeSplitterComboBox);
            this.decisionTreePanel.Controls.Add(this.label21);
            this.decisionTreePanel.Controls.Add(this.decisionTreeMinSamplesLeafTextBox);
            this.decisionTreePanel.Controls.Add(this.decisionTreeCriterionComboBox);
            this.decisionTreePanel.Controls.Add(this.label18);
            this.decisionTreePanel.Controls.Add(this.decisionTreeMinSamplesSplitTextBox);
            this.decisionTreePanel.Controls.Add(this.label19);
            this.decisionTreePanel.Controls.Add(this.label20);
            this.decisionTreePanel.Controls.Add(this.decisionTreeMaxDepthTextBox);
            this.decisionTreePanel.Location = new System.Drawing.Point(831, 94);
            this.decisionTreePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreePanel.Name = "decisionTreePanel";
            this.decisionTreePanel.Size = new System.Drawing.Size(384, 335);
            this.decisionTreePanel.TabIndex = 35;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(10, 291);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(114, 20);
            this.label25.TabIndex = 44;
            this.label25.Text = "Random state:";
            // 
            // decisionTreeRandomStateTextBox
            // 
            this.decisionTreeRandomStateTextBox.Location = new System.Drawing.Point(174, 288);
            this.decisionTreeRandomStateTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreeRandomStateTextBox.Name = "decisionTreeRandomStateTextBox";
            this.decisionTreeRandomStateTextBox.Size = new System.Drawing.Size(62, 26);
            this.decisionTreeRandomStateTextBox.TabIndex = 45;
            this.decisionTreeRandomStateTextBox.Text = "42";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(10, 251);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(166, 20);
            this.label24.TabIndex = 42;
            this.label24.Text = "Min impurity decrease:";
            // 
            // decisionTreeMinImpurityDecreaseTextBox
            // 
            this.decisionTreeMinImpurityDecreaseTextBox.Location = new System.Drawing.Point(174, 248);
            this.decisionTreeMinImpurityDecreaseTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreeMinImpurityDecreaseTextBox.Name = "decisionTreeMinImpurityDecreaseTextBox";
            this.decisionTreeMinImpurityDecreaseTextBox.Size = new System.Drawing.Size(62, 26);
            this.decisionTreeMinImpurityDecreaseTextBox.TabIndex = 43;
            this.decisionTreeMinImpurityDecreaseTextBox.Text = "0.0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 211);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(105, 20);
            this.label23.TabIndex = 41;
            this.label23.Text = "Max features:";
            // 
            // decisionTreeMaxFeaturesComboBox
            // 
            this.decisionTreeMaxFeaturesComboBox.DisplayMember = "0";
            this.decisionTreeMaxFeaturesComboBox.FormattingEnabled = true;
            this.decisionTreeMaxFeaturesComboBox.Items.AddRange(new object[] {
            "sqrt",
            "log2",
            "null"});
            this.decisionTreeMaxFeaturesComboBox.Location = new System.Drawing.Point(174, 206);
            this.decisionTreeMaxFeaturesComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreeMaxFeaturesComboBox.Name = "decisionTreeMaxFeaturesComboBox";
            this.decisionTreeMaxFeaturesComboBox.Size = new System.Drawing.Size(180, 28);
            this.decisionTreeMaxFeaturesComboBox.TabIndex = 40;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 169);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 20);
            this.label22.TabIndex = 39;
            this.label22.Text = "Splitter";
            // 
            // decisionTreeSplitterComboBox
            // 
            this.decisionTreeSplitterComboBox.DisplayMember = "0";
            this.decisionTreeSplitterComboBox.FormattingEnabled = true;
            this.decisionTreeSplitterComboBox.Items.AddRange(new object[] {
            "best",
            "random"});
            this.decisionTreeSplitterComboBox.Location = new System.Drawing.Point(174, 165);
            this.decisionTreeSplitterComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreeSplitterComboBox.Name = "decisionTreeSplitterComboBox";
            this.decisionTreeSplitterComboBox.Size = new System.Drawing.Size(180, 28);
            this.decisionTreeSplitterComboBox.TabIndex = 38;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 128);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 20);
            this.label21.TabIndex = 36;
            this.label21.Text = "Criterion:";
            // 
            // decisionTreeMinSamplesLeafTextBox
            // 
            this.decisionTreeMinSamplesLeafTextBox.Location = new System.Drawing.Point(174, 85);
            this.decisionTreeMinSamplesLeafTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreeMinSamplesLeafTextBox.Name = "decisionTreeMinSamplesLeafTextBox";
            this.decisionTreeMinSamplesLeafTextBox.Size = new System.Drawing.Size(62, 26);
            this.decisionTreeMinSamplesLeafTextBox.TabIndex = 37;
            this.decisionTreeMinSamplesLeafTextBox.Text = "1";
            // 
            // decisionTreeCriterionComboBox
            // 
            this.decisionTreeCriterionComboBox.DisplayMember = "0";
            this.decisionTreeCriterionComboBox.FormattingEnabled = true;
            this.decisionTreeCriterionComboBox.Items.AddRange(new object[] {
            "gini",
            "entropy"});
            this.decisionTreeCriterionComboBox.Location = new System.Drawing.Point(174, 123);
            this.decisionTreeCriterionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreeCriterionComboBox.Name = "decisionTreeCriterionComboBox";
            this.decisionTreeCriterionComboBox.Size = new System.Drawing.Size(180, 28);
            this.decisionTreeCriterionComboBox.TabIndex = 35;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 88);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(131, 20);
            this.label18.TabIndex = 36;
            this.label18.Text = "Min samples leaf:";
            // 
            // decisionTreeMinSamplesSplitTextBox
            // 
            this.decisionTreeMinSamplesSplitTextBox.Location = new System.Drawing.Point(174, 48);
            this.decisionTreeMinSamplesSplitTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreeMinSamplesSplitTextBox.Name = "decisionTreeMinSamplesSplitTextBox";
            this.decisionTreeMinSamplesSplitTextBox.Size = new System.Drawing.Size(62, 26);
            this.decisionTreeMinSamplesSplitTextBox.TabIndex = 35;
            this.decisionTreeMinSamplesSplitTextBox.Text = "2";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 9);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(87, 20);
            this.label19.TabIndex = 29;
            this.label19.Text = "Max depth:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(10, 51);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(133, 20);
            this.label20.TabIndex = 31;
            this.label20.Text = "Min samples split:";
            // 
            // decisionTreeMaxDepthTextBox
            // 
            this.decisionTreeMaxDepthTextBox.Location = new System.Drawing.Point(174, 5);
            this.decisionTreeMaxDepthTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decisionTreeMaxDepthTextBox.Name = "decisionTreeMaxDepthTextBox";
            this.decisionTreeMaxDepthTextBox.Size = new System.Drawing.Size(62, 26);
            this.decisionTreeMaxDepthTextBox.TabIndex = 32;
            this.decisionTreeMaxDepthTextBox.Text = "10";
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(12, 308);
            this.RunButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(112, 35);
            this.RunButton.TabIndex = 36;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RunButton_MouseClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(827, 69);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 20);
            this.label14.TabIndex = 28;
            this.label14.Text = "Parameters:";
            // 
            // modelComboBox
            // 
            this.modelComboBox.DisplayMember = "0";
            this.modelComboBox.FormattingEnabled = true;
            this.modelComboBox.Items.AddRange(new object[] {
            "DecisionTree",
            "KMeans"});
            this.modelComboBox.Location = new System.Drawing.Point(146, 106);
            this.modelComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.modelComboBox.Name = "modelComboBox";
            this.modelComboBox.Size = new System.Drawing.Size(180, 28);
            this.modelComboBox.TabIndex = 27;
            this.modelComboBox.TextChanged += new System.EventHandler(this.modelComboBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 111);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 20);
            this.label13.TabIndex = 26;
            this.label13.Text = "Model:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mode:";
            // 
            // clusteringRadioBox
            // 
            this.clusteringRadioBox.AutoSize = true;
            this.clusteringRadioBox.Checked = true;
            this.clusteringRadioBox.Location = new System.Drawing.Point(83, 69);
            this.clusteringRadioBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clusteringRadioBox.Name = "clusteringRadioBox";
            this.clusteringRadioBox.Size = new System.Drawing.Size(98, 24);
            this.clusteringRadioBox.TabIndex = 4;
            this.clusteringRadioBox.TabStop = true;
            this.clusteringRadioBox.Text = "Clustering";
            this.clusteringRadioBox.UseVisualStyleBackColor = true;
            this.clusteringRadioBox.CheckedChanged += new System.EventHandler(this.clusteringRadioBox_CheckedChanged);
            // 
            // classificationRadioBox
            // 
            this.classificationRadioBox.AutoSize = true;
            this.classificationRadioBox.Location = new System.Drawing.Point(198, 69);
            this.classificationRadioBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationRadioBox.Name = "classificationRadioBox";
            this.classificationRadioBox.Size = new System.Drawing.Size(120, 24);
            this.classificationRadioBox.TabIndex = 5;
            this.classificationRadioBox.Text = "Classification";
            this.classificationRadioBox.UseVisualStyleBackColor = true;
            this.classificationRadioBox.CheckedChanged += new System.EventHandler(this.classificationRadioBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "General Settings:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 217);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Logger:";
            // 
            // loggerTextBox
            // 
            this.loggerTextBox.Location = new System.Drawing.Point(176, 207);
            this.loggerTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loggerTextBox.Name = "loggerTextBox";
            this.loggerTextBox.Size = new System.Drawing.Size(631, 26);
            this.loggerTextBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 133);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Values:";
            // 
            // removeColumnsTextBox
            // 
            this.removeColumnsTextBox.Location = new System.Drawing.Point(176, 122);
            this.removeColumnsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removeColumnsTextBox.Name = "removeColumnsTextBox";
            this.removeColumnsTextBox.Size = new System.Drawing.Size(631, 26);
            this.removeColumnsTextBox.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 90);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Remove columns by:";
            // 
            // removeColumnsByNameRadioBox
            // 
            this.removeColumnsByNameRadioBox.AutoSize = true;
            this.removeColumnsByNameRadioBox.Location = new System.Drawing.Point(9, 15);
            this.removeColumnsByNameRadioBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removeColumnsByNameRadioBox.Name = "removeColumnsByNameRadioBox";
            this.removeColumnsByNameRadioBox.Size = new System.Drawing.Size(69, 24);
            this.removeColumnsByNameRadioBox.TabIndex = 14;
            this.removeColumnsByNameRadioBox.Text = "Name";
            this.removeColumnsByNameRadioBox.UseVisualStyleBackColor = true;
            // 
            // removeColumnsByIndexRadioBox
            // 
            this.removeColumnsByIndexRadioBox.AutoSize = true;
            this.removeColumnsByIndexRadioBox.Location = new System.Drawing.Point(98, 15);
            this.removeColumnsByIndexRadioBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removeColumnsByIndexRadioBox.Name = "removeColumnsByIndexRadioBox";
            this.removeColumnsByIndexRadioBox.Size = new System.Drawing.Size(66, 24);
            this.removeColumnsByIndexRadioBox.TabIndex = 15;
            this.removeColumnsByIndexRadioBox.Text = "Index";
            this.removeColumnsByIndexRadioBox.UseVisualStyleBackColor = true;
            // 
            // removeColumnsByRegexRadioBox
            // 
            this.removeColumnsByRegexRadioBox.AutoSize = true;
            this.removeColumnsByRegexRadioBox.Location = new System.Drawing.Point(183, 15);
            this.removeColumnsByRegexRadioBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removeColumnsByRegexRadioBox.Name = "removeColumnsByRegexRadioBox";
            this.removeColumnsByRegexRadioBox.Size = new System.Drawing.Size(73, 24);
            this.removeColumnsByRegexRadioBox.TabIndex = 16;
            this.removeColumnsByRegexRadioBox.Text = "Regex";
            this.removeColumnsByRegexRadioBox.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 176);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 18);
            this.label9.TabIndex = 17;
            this.label9.Text = "Debug:";
            // 
            // evaluationsTextBox
            // 
            this.evaluationsTextBox.Location = new System.Drawing.Point(176, 257);
            this.evaluationsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.evaluationsTextBox.Name = "evaluationsTextBox";
            this.evaluationsTextBox.Size = new System.Drawing.Size(631, 26);
            this.evaluationsTextBox.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 257);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "Evaluations:";
            // 
            // preprocessingRadioGroup
            // 
            this.preprocessingRadioGroup.Controls.Add(this.removeColumnsByNameRadioBox);
            this.preprocessingRadioGroup.Controls.Add(this.removeColumnsByIndexRadioBox);
            this.preprocessingRadioGroup.Controls.Add(this.removeColumnsByRegexRadioBox);
            this.preprocessingRadioGroup.Location = new System.Drawing.Point(174, 70);
            this.preprocessingRadioGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.preprocessingRadioGroup.Name = "preprocessingRadioGroup";
            this.preprocessingRadioGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.preprocessingRadioGroup.Size = new System.Drawing.Size(279, 45);
            this.preprocessingRadioGroup.TabIndex = 37;
            this.preprocessingRadioGroup.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 50);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "Preprocessing";
            // 
            // classificationTrainPanel
            // 
            this.classificationTrainPanel.Controls.Add(this.classificationTrainSupervisedNameTextBox);
            this.classificationTrainPanel.Controls.Add(this.label29);
            this.classificationTrainPanel.Controls.Add(this.classificationTrainPKLOutputTextBox);
            this.classificationTrainPanel.Controls.Add(this.label30);
            this.classificationTrainPanel.Controls.Add(this.label28);
            this.classificationTrainPanel.Controls.Add(this.label27);
            this.classificationTrainPanel.Controls.Add(this.label26);
            this.classificationTrainPanel.Controls.Add(this.classificationTrainInputTextBox);
            this.classificationTrainPanel.Controls.Add(this.classificationTrainSupervisedIndexTextBox);
            this.classificationTrainPanel.Location = new System.Drawing.Point(1241, 213);
            this.classificationTrainPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationTrainPanel.Name = "classificationTrainPanel";
            this.classificationTrainPanel.Size = new System.Drawing.Size(741, 213);
            this.classificationTrainPanel.TabIndex = 26;
            this.classificationTrainPanel.Visible = false;
            // 
            // classificationTrainSupervisedNameTextBox
            // 
            this.classificationTrainSupervisedNameTextBox.Location = new System.Drawing.Point(136, 162);
            this.classificationTrainSupervisedNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationTrainSupervisedNameTextBox.Name = "classificationTrainSupervisedNameTextBox";
            this.classificationTrainSupervisedNameTextBox.Size = new System.Drawing.Size(180, 26);
            this.classificationTrainSupervisedNameTextBox.TabIndex = 30;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(4, 7);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(109, 20);
            this.label29.TabIndex = 24;
            this.label29.Text = "Input train file:";
            // 
            // classificationTrainPKLOutputTextBox
            // 
            this.classificationTrainPKLOutputTextBox.Location = new System.Drawing.Point(136, 47);
            this.classificationTrainPKLOutputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationTrainPKLOutputTextBox.Name = "classificationTrainPKLOutputTextBox";
            this.classificationTrainPKLOutputTextBox.Size = new System.Drawing.Size(671, 26);
            this.classificationTrainPKLOutputTextBox.TabIndex = 23;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(1, 162);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(136, 20);
            this.label30.TabIndex = 29;
            this.label30.Text = "Supervised name:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(4, 47);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(110, 20);
            this.label28.TabIndex = 22;
            this.label28.Text = "Output pkl file:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(4, 131);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(133, 20);
            this.label27.TabIndex = 26;
            this.label27.Text = "Supervised index:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(4, 92);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(165, 20);
            this.label26.TabIndex = 28;
            this.label26.Text = "Supervised column:";
            // 
            // classificationTrainInputTextBox
            // 
            this.classificationTrainInputTextBox.Location = new System.Drawing.Point(136, 2);
            this.classificationTrainInputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationTrainInputTextBox.Name = "classificationTrainInputTextBox";
            this.classificationTrainInputTextBox.Size = new System.Drawing.Size(671, 26);
            this.classificationTrainInputTextBox.TabIndex = 25;
            // 
            // classificationTrainSupervisedIndexTextBox
            // 
            this.classificationTrainSupervisedIndexTextBox.Location = new System.Drawing.Point(136, 126);
            this.classificationTrainSupervisedIndexTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationTrainSupervisedIndexTextBox.Name = "classificationTrainSupervisedIndexTextBox";
            this.classificationTrainSupervisedIndexTextBox.Size = new System.Drawing.Size(40, 26);
            this.classificationTrainSupervisedIndexTextBox.TabIndex = 27;
            this.classificationTrainSupervisedIndexTextBox.Text = "11";
            // 
            // classificationPredictRadioBox
            // 
            this.classificationPredictRadioBox.AutoSize = true;
            this.classificationPredictRadioBox.Location = new System.Drawing.Point(96, 12);
            this.classificationPredictRadioBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationPredictRadioBox.Name = "classificationPredictRadioBox";
            this.classificationPredictRadioBox.Size = new System.Drawing.Size(76, 24);
            this.classificationPredictRadioBox.TabIndex = 27;
            this.classificationPredictRadioBox.Text = "Predict";
            this.classificationPredictRadioBox.UseVisualStyleBackColor = true;
            this.classificationPredictRadioBox.CheckedChanged += new System.EventHandler(this.classificationPredictRadioBox_CheckedChanged);
            // 
            // classificationTrainRadioBox
            // 
            this.classificationTrainRadioBox.AutoSize = true;
            this.classificationTrainRadioBox.Checked = true;
            this.classificationTrainRadioBox.Location = new System.Drawing.Point(14, 11);
            this.classificationTrainRadioBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationTrainRadioBox.Name = "classificationTrainRadioBox";
            this.classificationTrainRadioBox.Size = new System.Drawing.Size(62, 24);
            this.classificationTrainRadioBox.TabIndex = 26;
            this.classificationTrainRadioBox.TabStop = true;
            this.classificationTrainRadioBox.Text = "Train";
            this.classificationTrainRadioBox.UseVisualStyleBackColor = true;
            this.classificationTrainRadioBox.CheckedChanged += new System.EventHandler(this.classificationTrainRadioBox_CheckedChanged);
            // 
            // classificationPredictPanel
            // 
            this.classificationPredictPanel.Controls.Add(this.classificationPredictOutputFileTextBox);
            this.classificationPredictPanel.Controls.Add(this.label32);
            this.classificationPredictPanel.Controls.Add(this.label31);
            this.classificationPredictPanel.Controls.Add(this.classificationPredictPKLFileTextBox);
            this.classificationPredictPanel.Controls.Add(this.label33);
            this.classificationPredictPanel.Controls.Add(this.classificationPredictInputFileTextBox);
            this.classificationPredictPanel.Location = new System.Drawing.Point(1241, 85);
            this.classificationPredictPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationPredictPanel.Name = "classificationPredictPanel";
            this.classificationPredictPanel.Size = new System.Drawing.Size(747, 123);
            this.classificationPredictPanel.TabIndex = 31;
            this.classificationPredictPanel.Visible = false;
            // 
            // classificationPredictOutputFileTextBox
            // 
            this.classificationPredictOutputFileTextBox.Location = new System.Drawing.Point(136, 92);
            this.classificationPredictOutputFileTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationPredictOutputFileTextBox.Name = "classificationPredictOutputFileTextBox";
            this.classificationPredictOutputFileTextBox.Size = new System.Drawing.Size(671, 26);
            this.classificationPredictOutputFileTextBox.TabIndex = 27;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(4, 92);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(86, 20);
            this.label32.TabIndex = 26;
            this.label32.Text = "Output file:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(4, 9);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(126, 20);
            this.label31.TabIndex = 24;
            this.label31.Text = "Input predict file:";
            // 
            // classificationPredictPKLFileTextBox
            // 
            this.classificationPredictPKLFileTextBox.Location = new System.Drawing.Point(136, 49);
            this.classificationPredictPKLFileTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationPredictPKLFileTextBox.Name = "classificationPredictPKLFileTextBox";
            this.classificationPredictPKLFileTextBox.Size = new System.Drawing.Size(671, 26);
            this.classificationPredictPKLFileTextBox.TabIndex = 23;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(4, 49);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(115, 20);
            this.label33.TabIndex = 22;
            this.label33.Text = "Saved PKL file:";
            // 
            // classificationPredictInputFileTextBox
            // 
            this.classificationPredictInputFileTextBox.Location = new System.Drawing.Point(136, 4);
            this.classificationPredictInputFileTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.classificationPredictInputFileTextBox.Name = "classificationPredictInputFileTextBox";
            this.classificationPredictInputFileTextBox.Size = new System.Drawing.Size(671, 26);
            this.classificationPredictInputFileTextBox.TabIndex = 25;
            // 
            // trainPredictPanel
            // 
            this.trainPredictPanel.Controls.Add(this.classificationPredictRadioBox);
            this.trainPredictPanel.Controls.Add(this.classificationTrainRadioBox);
            this.trainPredictPanel.Location = new System.Drawing.Point(336, 100);
            this.trainPredictPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trainPredictPanel.Name = "trainPredictPanel";
            this.trainPredictPanel.Size = new System.Drawing.Size(188, 42);
            this.trainPredictPanel.TabIndex = 38;
            this.trainPredictPanel.Visible = false;
            // 
            // lightgbmPanel
            // 
            this.lightgbmPanel.Controls.Add(this.lightgbmSubsampleTextBox);
            this.lightgbmPanel.Controls.Add(this.lightgbmNEstimatorsTextBox);
            this.lightgbmPanel.Controls.Add(this.lightgbmLearningRateTextBox);
            this.lightgbmPanel.Controls.Add(this.lightgbmMaxDepthTextBox);
            this.lightgbmPanel.Controls.Add(this.label38);
            this.lightgbmPanel.Controls.Add(this.label37);
            this.lightgbmPanel.Controls.Add(this.label34);
            this.lightgbmPanel.Controls.Add(this.label35);
            this.lightgbmPanel.Controls.Add(this.label36);
            this.lightgbmPanel.Controls.Add(this.lightgbmNumLeavesTextBox);
            this.lightgbmPanel.Location = new System.Drawing.Point(832, 96);
            this.lightgbmPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lightgbmPanel.Name = "lightgbmPanel";
            this.lightgbmPanel.Size = new System.Drawing.Size(368, 212);
            this.lightgbmPanel.TabIndex = 35;
            this.lightgbmPanel.Visible = false;
            // 
            // lightgbmSubsampleTextBox
            // 
            this.lightgbmSubsampleTextBox.Location = new System.Drawing.Point(150, 171);
            this.lightgbmSubsampleTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lightgbmSubsampleTextBox.Name = "lightgbmSubsampleTextBox";
            this.lightgbmSubsampleTextBox.Size = new System.Drawing.Size(54, 26);
            this.lightgbmSubsampleTextBox.TabIndex = 40;
            this.lightgbmSubsampleTextBox.Text = "300";
            // 
            // lightgbmNEstimatorsTextBox
            // 
            this.lightgbmNEstimatorsTextBox.Location = new System.Drawing.Point(150, 131);
            this.lightgbmNEstimatorsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lightgbmNEstimatorsTextBox.Name = "lightgbmNEstimatorsTextBox";
            this.lightgbmNEstimatorsTextBox.Size = new System.Drawing.Size(54, 26);
            this.lightgbmNEstimatorsTextBox.TabIndex = 39;
            this.lightgbmNEstimatorsTextBox.Text = "300";
            // 
            // lightgbmLearningRateTextBox
            // 
            this.lightgbmLearningRateTextBox.Location = new System.Drawing.Point(150, 91);
            this.lightgbmLearningRateTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lightgbmLearningRateTextBox.Name = "lightgbmLearningRateTextBox";
            this.lightgbmLearningRateTextBox.Size = new System.Drawing.Size(54, 26);
            this.lightgbmLearningRateTextBox.TabIndex = 38;
            this.lightgbmLearningRateTextBox.Text = "300";
            // 
            // lightgbmMaxDepthTextBox
            // 
            this.lightgbmMaxDepthTextBox.Location = new System.Drawing.Point(150, 51);
            this.lightgbmMaxDepthTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lightgbmMaxDepthTextBox.Name = "lightgbmMaxDepthTextBox";
            this.lightgbmMaxDepthTextBox.Size = new System.Drawing.Size(54, 26);
            this.lightgbmMaxDepthTextBox.TabIndex = 37;
            this.lightgbmMaxDepthTextBox.Text = "300";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(10, 183);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(93, 20);
            this.label38.TabIndex = 36;
            this.label38.Text = "Subsample:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(10, 132);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(107, 20);
            this.label37.TabIndex = 35;
            this.label37.Text = "N_estimators:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(10, 92);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(107, 20);
            this.label34.TabIndex = 34;
            this.label34.Text = "Learning rate:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(10, 14);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(95, 20);
            this.label35.TabIndex = 29;
            this.label35.Text = "Num leaves:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(10, 51);
            this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(87, 20);
            this.label36.TabIndex = 31;
            this.label36.Text = "Max depth:";
            // 
            // lightgbmNumLeavesTextBox
            // 
            this.lightgbmNumLeavesTextBox.Location = new System.Drawing.Point(150, 12);
            this.lightgbmNumLeavesTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lightgbmNumLeavesTextBox.Name = "lightgbmNumLeavesTextBox";
            this.lightgbmNumLeavesTextBox.Size = new System.Drawing.Size(54, 26);
            this.lightgbmNumLeavesTextBox.TabIndex = 32;
            this.lightgbmNumLeavesTextBox.Text = "300";
            // 
            // generalSettingsPanel
            // 
            this.generalSettingsPanel.Controls.Add(this.label3);
            this.generalSettingsPanel.Controls.Add(this.label7);
            this.generalSettingsPanel.Controls.Add(this.label4);
            this.generalSettingsPanel.Controls.Add(this.loggerTextBox);
            this.generalSettingsPanel.Controls.Add(this.label5);
            this.generalSettingsPanel.Controls.Add(this.RunButton);
            this.generalSettingsPanel.Controls.Add(this.preprocessingRadioGroup);
            this.generalSettingsPanel.Controls.Add(this.removeColumnsTextBox);
            this.generalSettingsPanel.Controls.Add(this.label8);
            this.generalSettingsPanel.Controls.Add(this.label9);
            this.generalSettingsPanel.Controls.Add(this.label6);
            this.generalSettingsPanel.Controls.Add(this.evaluationsTextBox);
            this.generalSettingsPanel.Location = new System.Drawing.Point(1, 366);
            this.generalSettingsPanel.Name = "generalSettingsPanel";
            this.generalSettingsPanel.Size = new System.Drawing.Size(820, 363);
            this.generalSettingsPanel.TabIndex = 39;
            // 
            // gridSearchPanel
            // 
            this.gridSearchPanel.Controls.Add(this.gridSearchVerboseComboBox);
            this.gridSearchPanel.Controls.Add(this.label44);
            this.gridSearchPanel.Controls.Add(this.gridSearchNJobsTextBox);
            this.gridSearchPanel.Controls.Add(this.label43);
            this.gridSearchPanel.Controls.Add(this.gridSearchCVTextBox);
            this.gridSearchPanel.Controls.Add(this.gridSearchReFitBestModelCheckBox);
            this.gridSearchPanel.Controls.Add(this.label42);
            this.gridSearchPanel.Controls.Add(this.gridSearchScoringMetricComboBox);
            this.gridSearchPanel.Controls.Add(this.label40);
            this.gridSearchPanel.Controls.Add(this.label41);
            this.gridSearchPanel.Location = new System.Drawing.Point(832, 96);
            this.gridSearchPanel.Name = "gridSearchPanel";
            this.gridSearchPanel.Size = new System.Drawing.Size(384, 169);
            this.gridSearchPanel.TabIndex = 40;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(4, 4);
            this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(114, 20);
            this.label41.TabIndex = 38;
            this.label41.Text = "Scoring metric:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(4, 40);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(129, 20);
            this.label40.TabIndex = 40;
            this.label40.Text = "Refit best model:";
            // 
            // gridSearchScoringMetricComboBox
            // 
            this.gridSearchScoringMetricComboBox.DisplayMember = "0";
            this.gridSearchScoringMetricComboBox.FormattingEnabled = true;
            this.gridSearchScoringMetricComboBox.Items.AddRange(new object[] {
            "accuracy",
            "f1",
            "precision",
            "recall",
            "roc_auc"});
            this.gridSearchScoringMetricComboBox.Location = new System.Drawing.Point(142, 2);
            this.gridSearchScoringMetricComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridSearchScoringMetricComboBox.Name = "gridSearchScoringMetricComboBox";
            this.gridSearchScoringMetricComboBox.Size = new System.Drawing.Size(180, 28);
            this.gridSearchScoringMetricComboBox.TabIndex = 41;
            // 
            // gridSearchReFitBestModelCheckBox
            // 
            this.gridSearchReFitBestModelCheckBox.AutoSize = true;
            this.gridSearchReFitBestModelCheckBox.Checked = true;
            this.gridSearchReFitBestModelCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridSearchReFitBestModelCheckBox.Location = new System.Drawing.Point(142, 44);
            this.gridSearchReFitBestModelCheckBox.Name = "gridSearchReFitBestModelCheckBox";
            this.gridSearchReFitBestModelCheckBox.Size = new System.Drawing.Size(15, 14);
            this.gridSearchReFitBestModelCheckBox.TabIndex = 42;
            this.gridSearchReFitBestModelCheckBox.UseVisualStyleBackColor = true;
            // 
            // gridSearchCVTextBox
            // 
            this.gridSearchCVTextBox.Location = new System.Drawing.Point(142, 69);
            this.gridSearchCVTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridSearchCVTextBox.Name = "gridSearchCVTextBox";
            this.gridSearchCVTextBox.Size = new System.Drawing.Size(40, 26);
            this.gridSearchCVTextBox.TabIndex = 27;
            this.gridSearchCVTextBox.Text = "2";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(10, 72);
            this.label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(35, 20);
            this.label42.TabIndex = 26;
            this.label42.Text = "CV:";
            // 
            // gridSearchNJobsTextBox
            // 
            this.gridSearchNJobsTextBox.Location = new System.Drawing.Point(142, 104);
            this.gridSearchNJobsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridSearchNJobsTextBox.Name = "gridSearchNJobsTextBox";
            this.gridSearchNJobsTextBox.Size = new System.Drawing.Size(40, 26);
            this.gridSearchNJobsTextBox.TabIndex = 44;
            this.gridSearchNJobsTextBox.Text = "2";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(5, 107);
            this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(62, 20);
            this.label43.TabIndex = 43;
            this.label43.Text = "N_jobs:";
            // 
            // gridSearchVerboseComboBox
            // 
            this.gridSearchVerboseComboBox.DisplayMember = "0";
            this.gridSearchVerboseComboBox.FormattingEnabled = true;
            this.gridSearchVerboseComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.gridSearchVerboseComboBox.Location = new System.Drawing.Point(142, 139);
            this.gridSearchVerboseComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridSearchVerboseComboBox.Name = "gridSearchVerboseComboBox";
            this.gridSearchVerboseComboBox.Size = new System.Drawing.Size(40, 28);
            this.gridSearchVerboseComboBox.TabIndex = 46;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(4, 141);
            this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(73, 20);
            this.label44.TabIndex = 45;
            this.label44.Text = "Verbose:";
            // 
            // bayesianPanel
            // 
            this.bayesianPanel.Controls.Add(this.bayesianRandomStateTextBox);
            this.bayesianPanel.Controls.Add(this.bayesianVerboseComboBox);
            this.bayesianPanel.Controls.Add(this.bayesianNJobsTextBox);
            this.bayesianPanel.Controls.Add(this.label51);
            this.bayesianPanel.Controls.Add(this.label48);
            this.bayesianPanel.Controls.Add(this.label45);
            this.bayesianPanel.Controls.Add(this.bayesianNInitialPointsTextBox);
            this.bayesianPanel.Controls.Add(this.label46);
            this.bayesianPanel.Controls.Add(this.bayesianNIterTextBox);
            this.bayesianPanel.Controls.Add(this.label47);
            this.bayesianPanel.Controls.Add(this.bayesianAcquisitionFunctionComboBox);
            this.bayesianPanel.Controls.Add(this.label49);
            this.bayesianPanel.Location = new System.Drawing.Point(831, 96);
            this.bayesianPanel.Name = "bayesianPanel";
            this.bayesianPanel.Size = new System.Drawing.Size(384, 230);
            this.bayesianPanel.TabIndex = 47;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(4, 115);
            this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(117, 20);
            this.label45.TabIndex = 45;
            this.label45.Text = "Random State:";
            // 
            // bayesianNInitialPointsTextBox
            // 
            this.bayesianNInitialPointsTextBox.Location = new System.Drawing.Point(163, 76);
            this.bayesianNInitialPointsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bayesianNInitialPointsTextBox.Name = "bayesianNInitialPointsTextBox";
            this.bayesianNInitialPointsTextBox.Size = new System.Drawing.Size(68, 26);
            this.bayesianNInitialPointsTextBox.TabIndex = 44;
            this.bayesianNInitialPointsTextBox.Text = "2";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 79);
            this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(123, 20);
            this.label46.TabIndex = 43;
            this.label46.Text = "N_Initial_Points:";
            // 
            // bayesianNIterTextBox
            // 
            this.bayesianNIterTextBox.Location = new System.Drawing.Point(163, 6);
            this.bayesianNIterTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bayesianNIterTextBox.Name = "bayesianNIterTextBox";
            this.bayesianNIterTextBox.Size = new System.Drawing.Size(68, 26);
            this.bayesianNIterTextBox.TabIndex = 27;
            this.bayesianNIterTextBox.Text = "2";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(4, 6);
            this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(53, 20);
            this.label47.TabIndex = 26;
            this.label47.Text = "N_Iter";
            // 
            // bayesianAcquisitionFunctionComboBox
            // 
            this.bayesianAcquisitionFunctionComboBox.DisplayMember = "0";
            this.bayesianAcquisitionFunctionComboBox.FormattingEnabled = true;
            this.bayesianAcquisitionFunctionComboBox.Items.AddRange(new object[] {
            "gp_hedge",
            "ei",
            "pi",
            "ucb"});
            this.bayesianAcquisitionFunctionComboBox.Location = new System.Drawing.Point(163, 38);
            this.bayesianAcquisitionFunctionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bayesianAcquisitionFunctionComboBox.Name = "bayesianAcquisitionFunctionComboBox";
            this.bayesianAcquisitionFunctionComboBox.Size = new System.Drawing.Size(104, 28);
            this.bayesianAcquisitionFunctionComboBox.TabIndex = 41;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(4, 41);
            this.label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(151, 20);
            this.label49.TabIndex = 38;
            this.label49.Text = "Acquisition function:";
            // 
            // bayesianNJobsTextBox
            // 
            this.bayesianNJobsTextBox.Location = new System.Drawing.Point(163, 152);
            this.bayesianNJobsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bayesianNJobsTextBox.Name = "bayesianNJobsTextBox";
            this.bayesianNJobsTextBox.Size = new System.Drawing.Size(68, 26);
            this.bayesianNJobsTextBox.TabIndex = 48;
            this.bayesianNJobsTextBox.Text = "2";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(4, 155);
            this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(62, 20);
            this.label48.TabIndex = 47;
            this.label48.Text = "N_jobs:";
            // 
            // bayesianVerboseComboBox
            // 
            this.bayesianVerboseComboBox.DisplayMember = "0";
            this.bayesianVerboseComboBox.FormattingEnabled = true;
            this.bayesianVerboseComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.bayesianVerboseComboBox.Location = new System.Drawing.Point(163, 189);
            this.bayesianVerboseComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bayesianVerboseComboBox.Name = "bayesianVerboseComboBox";
            this.bayesianVerboseComboBox.Size = new System.Drawing.Size(80, 28);
            this.bayesianVerboseComboBox.TabIndex = 48;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 192);
            this.label51.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(73, 20);
            this.label51.TabIndex = 47;
            this.label51.Text = "Verbose:";
            // 
            // bayesianRandomStateTextBox
            // 
            this.bayesianRandomStateTextBox.Location = new System.Drawing.Point(163, 112);
            this.bayesianRandomStateTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bayesianRandomStateTextBox.Name = "bayesianRandomStateTextBox";
            this.bayesianRandomStateTextBox.Size = new System.Drawing.Size(68, 26);
            this.bayesianRandomStateTextBox.TabIndex = 49;
            this.bayesianRandomStateTextBox.Text = "2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 717);
            this.Controls.Add(this.bayesianPanel);
            this.Controls.Add(this.gridSearchPanel);
            this.Controls.Add(this.generalSettingsPanel);
            this.Controls.Add(this.lightgbmPanel);
            this.Controls.Add(this.trainPredictPanel);
            this.Controls.Add(this.classificationTrainPanel);
            this.Controls.Add(this.classificationPredictPanel);
            this.Controls.Add(this.decisionTreePanel);
            this.Controls.Add(this.kmeansPanel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.modelComboBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.classificationRadioBox);
            this.Controls.Add(this.clusteringRadioBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clusteringPanel);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.clusteringPanel.ResumeLayout(false);
            this.clusteringPanel.PerformLayout();
            this.kmeansPanel.ResumeLayout(false);
            this.kmeansPanel.PerformLayout();
            this.decisionTreePanel.ResumeLayout(false);
            this.decisionTreePanel.PerformLayout();
            this.preprocessingRadioGroup.ResumeLayout(false);
            this.preprocessingRadioGroup.PerformLayout();
            this.classificationTrainPanel.ResumeLayout(false);
            this.classificationTrainPanel.PerformLayout();
            this.classificationPredictPanel.ResumeLayout(false);
            this.classificationPredictPanel.PerformLayout();
            this.trainPredictPanel.ResumeLayout(false);
            this.trainPredictPanel.PerformLayout();
            this.lightgbmPanel.ResumeLayout(false);
            this.lightgbmPanel.PerformLayout();
            this.generalSettingsPanel.ResumeLayout(false);
            this.generalSettingsPanel.PerformLayout();
            this.gridSearchPanel.ResumeLayout(false);
            this.gridSearchPanel.PerformLayout();
            this.bayesianPanel.ResumeLayout(false);
            this.bayesianPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel clusteringPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton clusteringRadioBox;
        private System.Windows.Forms.RadioButton classificationRadioBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox loggerTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox removeColumnsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton removeColumnsByNameRadioBox;
        private System.Windows.Forms.RadioButton removeColumnsByIndexRadioBox;
        private System.Windows.Forms.RadioButton removeColumnsByRegexRadioBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox clustersNumberTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox evaluationsTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox clusterOutputFileTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox clusterInputFileTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox modelComboBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox kmeansMaxIterTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox kmeansInitComboBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox kmeansTolComboBox;
        private System.Windows.Forms.Panel kmeansPanel;
        private System.Windows.Forms.Panel decisionTreePanel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox decisionTreeMaxDepthTextBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox decisionTreeMinSamplesLeafTextBox;
        private System.Windows.Forms.ComboBox decisionTreeCriterionComboBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox decisionTreeMinSamplesSplitTextBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox decisionTreeSplitterComboBox;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox decisionTreeRandomStateTextBox;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox decisionTreeMinImpurityDecreaseTextBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox decisionTreeMaxFeaturesComboBox;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.GroupBox preprocessingRadioGroup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel classificationTrainPanel;
        private System.Windows.Forms.TextBox classificationTrainInputTextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox classificationTrainPKLOutputTextBox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.RadioButton classificationPredictRadioBox;
        private System.Windows.Forms.RadioButton classificationTrainRadioBox;
        private System.Windows.Forms.TextBox classificationTrainSupervisedIndexTextBox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox classificationTrainSupervisedNameTextBox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel classificationPredictPanel;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox classificationPredictPKLFileTextBox;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox classificationPredictInputFileTextBox;
        private System.Windows.Forms.TextBox classificationPredictOutputFileTextBox;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Panel trainPredictPanel;
        private System.Windows.Forms.Panel lightgbmPanel;
        private System.Windows.Forms.TextBox lightgbmSubsampleTextBox;
        private System.Windows.Forms.TextBox lightgbmNEstimatorsTextBox;
        private System.Windows.Forms.TextBox lightgbmLearningRateTextBox;
        private System.Windows.Forms.TextBox lightgbmMaxDepthTextBox;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox lightgbmNumLeavesTextBox;
        private System.Windows.Forms.Panel generalSettingsPanel;
        private System.Windows.Forms.Panel gridSearchPanel;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox gridSearchScoringMetricComboBox;
        private System.Windows.Forms.CheckBox gridSearchReFitBestModelCheckBox;
        private System.Windows.Forms.TextBox gridSearchCVTextBox;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox gridSearchNJobsTextBox;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.ComboBox gridSearchVerboseComboBox;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Panel bayesianPanel;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox bayesianNInitialPointsTextBox;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox bayesianNIterTextBox;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.ComboBox bayesianAcquisitionFunctionComboBox;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox bayesianNJobsTextBox;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ComboBox bayesianVerboseComboBox;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox bayesianRandomStateTextBox;
    }
}

