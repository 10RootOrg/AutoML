# AutoML GUI

> Automated Machine Learning Made Simple - No coding required

[![Platform](https://img.shields.io/badge/platform-Windows-blue.svg)]()
[![Framework](https://img.shields.io/badge/.NET-4.7.2-purple.svg)]()
[![Python](https://img.shields.io/badge/python-3.x-green.svg)]()
[![License](https://img.shields.io/badge/license-Multiple-orange.svg)](#license)

## 📋 Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Data Preparation](#data-preparation)
  - [Launching the Application](#launching-the-application)
- [Interface Overview](#interface-overview)
- [Clustering Analysis](#clustering-analysis)
- [Classification Tasks](#classification-tasks)
  - [Training Models](#training-models)
  - [Making Predictions](#making-predictions)
- [Model Options](#model-options)
- [Parameter Configuration](#parameter-configuration)
- [Data Preprocessing](#data-preprocessing)
- [Running Your Analysis](#running-your-analysis)
- [Understanding Results](#understanding-results)
- [Troubleshooting](#troubleshooting)
- [License](#license)

---

## Introduction

AutoML GUI makes machine learning accessible to everyone by providing a simple, intuitive interface for training and using machine learning models. No coding or algorithm knowledge required!

### What Can You Do?

- **Cluster Analysis**: Group similar data points together to discover patterns
- **Classification**: Train models to predict categories or outcomes
- **Automatic Optimization**: Let the system find the best model settings
- **Data Preprocessing**: Automatically clean and prepare your data
- **Easy Predictions**: Use trained models to analyze new data

### Who Is This For?

This tool is designed for users who want to apply machine learning to their data without writing code or understanding complex algorithms. Whether you're analyzing customer data, scientific measurements, or any other dataset, AutoML GUI guides you through the process step by step.

---

## Getting Started

### Prerequisites

**System Requirements:**
- Windows Operating System
- .NET Framework 4.7.2 or higher
- Python 3.x environment
- Sufficient disk space for data and models

**Data Requirements:**
- CSV file format (Excel-compatible)
- Column headers in the first row
- Numerical or text data
- For Classification: One column with target categories

### Data Preparation

Before using AutoML GUI, ensure your data is:

✅ Saved as a CSV file  
✅ Contains column headers in the first row  
✅ Has numerical or text data in columns  
✅ For Classification: Includes a target column with categories to predict  

### Launching the Application

Simply double-click the AutoML GUI application icon to start.

---

## Interface Overview

The AutoML GUI is organized into clear sections for step-by-step configuration.

### Main Sections

| Section | Location | Purpose |
|---------|----------|---------|
| **Mode Selection** | Top | Choose between Clustering or Classification |
| **Input/Output Settings** | Left Side | Specify file locations and model options |
| **Parameters** | Right Side | Fine-tune model settings (defaults work well) |
| **General Settings** | Middle | Configure preprocessing and debug options |
| **Run Button** | Bottom | Start the analysis |

### Mode Selection

**Clustering Mode:**
- Use when you want to discover groups or patterns in your data
- No specific prediction target needed
- Finds natural groupings automatically

**Classification Mode:**
- Use when you want to predict specific categories or outcomes
- Requires labeled training data
- Can make predictions on new data

---

## Clustering Analysis

Clustering is useful for discovering natural groups in your data, such as customer segments or measurement patterns.

### Step-by-Step: Running Clustering

**Step 1: Select Clustering Mode**
- Click the "Clustering" radio button at the top

**Step 2: Choose Your Model**
Select from the dropdown:
- **KMeans**: Fast and simple (recommended for beginners)
- **DecisionTree**: Tree-based for complex patterns

**Step 3: Specify Number of Clusters**
- Enter how many groups you expect (try 2-5 if unsure)
- Type "DDC" to let the system decide automatically

**Step 4: Select Input File**
- Enter path to your CSV data file or browse

**Step 5: Choose Output Location**
- Specify where to save results

**Step 6: Click Run**
- Process data and generate cluster assignments

### Clustering Results

Your output CSV file will contain:
- All original data
- New column showing cluster assignment (0, 1, 2, etc.)
- Open in Excel to explore the groups

**What to Look For:**
- Sort by cluster number to see group members
- Identify patterns that make each cluster unique
- Calculate statistics for each cluster
- Use for further analysis or visualization

---

## Classification Tasks

Classification predicts outcomes based on your data, such as spam detection, customer behavior prediction, or category assignment.

### Two Modes

| Mode | Purpose | When to Use |
|------|---------|-------------|
| **Train** | Teach the model using known data | First step - build your model |
| **Predict** | Use trained model on new data | After training - make predictions |

### Training Models

**Step 1: Select Classification Mode**
- Click "Classification" radio button at top

**Step 2: Select Train Mode**
- Ensure "Train" radio button is selected

**Step 3: Choose Your Model**

| Model | Best For | Skill Level |
|-------|----------|-------------|
| DecisionTree | Simple, interpretable predictions | Beginner |
| LightGBM | High accuracy, large datasets | Intermediate |
| GridSearch | Automatic model comparison | Automatic |
| BayesianOpt | Smart optimization, best results | Automatic |

**Step 4: Select Training Data**
- Specify path to training CSV file
- Must contain data with known correct answers

**Step 5: Specify Target Column**

Choose one method:
- **Option A**: Enter column name in "Supervised name" field
- **Option B**: Enter column index in "Supervised index" field (0 = first column)

**Step 6: Choose Model Save Location**
- Specify path for trained model (.pkl file)
- You'll need this file for making predictions

**Step 7: Click Run**
- Training may take a few minutes depending on data size

### Making Predictions

**Step 1: Select Classification and Predict Mode**
- Ensure "Classification" is selected
- Click "Predict" radio button

**Step 2: Load Your Trained Model**
- Specify location of your trained .pkl file

**Step 3: Select Data to Predict**
- Specify CSV file with new data
- Must have same columns as training data (except target)

**Step 4: Choose Output Location**
- Specify where to save predictions

**Step 5: Click Run**
- Output includes original data plus predictions and confidence levels

### Prediction Results

Your output CSV will contain:
- All original input data
- **Predicted_Label**: Model's predictions
- **Prediction_Confidence**: Confidence percentage (0-100%)

**Confidence Interpretation:**

| Range | Meaning | Reliability |
|-------|---------|-------------|
| 90-100% | Very confident | Very likely correct |
| 70-89% | Confident | Probably correct |
| 50-69% | Moderate | Uncertain |
| Below 50% | Low confidence | May be incorrect |

---

## Model Options

### For Clustering

| Model | Best For | Speed |
|-------|----------|-------|
| **KMeans** | Finding distinct groups, customer segmentation | Fast |
| **DecisionTree** | Complex patterns, hierarchical grouping | Fast |

### For Classification

| Model | Best For | Complexity |
|-------|----------|------------|
| **DecisionTree** | Simple predictions, interpretable results | Beginner |
| **LightGBM** | High accuracy, large datasets | Intermediate |
| **GridSearch** | Testing multiple models automatically | Automatic |
| **BayesianOpt** | Smart optimization, best overall results | Automatic |

> 💡 **Tip**: If unsure which model to use, try GridSearch or BayesianOpt - they'll automatically test different options and pick the best one!

---

## Parameter Configuration

The right side of the interface shows adjustable parameters. Default values work well for most cases.

### Decision Tree Parameters

| Parameter | Description | Default |
|-----------|-------------|---------|
| **Max depth** | How deep the tree grows (larger = more complex) | 10 |
| **Min samples split** | Minimum data points needed to create a branch | 2 |
| **Min samples leaf** | Minimum data points in each end branch | 3 |
| **Criterion** | How to measure quality of splits | gini |
| **Splitter** | Strategy for choosing splits | best |
| **Max features** | Number of features to consider | sqrt |
| **Min impurity decrease** | Threshold for creating new branches | 0 |
| **Random state** | Seed for reproducible results | 42 |

### LightGBM Parameters

| Parameter | Description | Default |
|-----------|-------------|---------|
| **Num leaves** | Number of leaves in the tree | 31 |
| **Max depth** | Maximum tree depth | 6 |
| **Learning rate** | How fast the model learns | 0.1 |
| **N estimators** | Number of trees to build | 100 |
| **Subsample** | Fraction of data used per tree | 0.8 |

### KMeans Parameters

| Parameter | Description | Default |
|-----------|-------------|---------|
| **Max iteration** | Maximum attempts to find best clusters | 300 |
| **Init** | How to initialize cluster centers | k-means++ |
| **Tolerance** | Precision for stopping criteria | 1e-3 |

### GridSearch Parameters

| Parameter | Description | Default |
|-----------|-------------|---------|
| **Scoring metric** | How to measure model quality | accuracy |
| **CV** | Number of validation rounds | 5 |
| **N Jobs** | CPU cores to use (-1 = all) | -1 |
| **Verbose** | Amount of progress information shown | 1 |
| **Refit best model** | Train final model on all data | Checked |

### Bayesian Optimization Parameters

| Parameter | Description | Default |
|-----------|-------------|---------|
| **N Iter** | Number of optimization attempts | 50 |
| **N Initial Points** | Random starting points to try | 5 |
| **Acquisition Function** | Strategy for exploring options | gp_hedge |
| **Random State** | Seed for reproducibility | 42 |
| **N Jobs** | CPU cores to use (-1 = all) | -1 |

> ⚠️ **Important**: Default parameter values work perfectly fine for most users. Only adjust if you're familiar with machine learning or want to experiment.

---

## Data Preprocessing

The "General Settings" section helps prepare your data before analysis.

### Removing Columns

Sometimes your data contains columns to exclude from analysis (ID numbers, dates, etc.).

**By Name:**
1. Select "Name" radio button
2. In "Values" field, type column names separated by commas
   ```
   Example: Measurement_Index, Date, ID
   ```

**By Index:**
1. Select "Index" radio button
2. In "Values" field, type column numbers (0 = first column)
   ```
   Example: 0, 5, 8
   ```

**By Pattern (Regex):**
1. Select "Regex" radio button
2. Enter pattern to match column names
   ```
   Example: _id$ (removes all columns ending with "_id")
   ```

### Debug Options

**Logger:**
- Specify file path for detailed logs
- Useful for troubleshooting if something goes wrong
- Tracks all processing steps and errors

**Evaluations:**
- Specify folder path for evaluation reports and charts
- Shows model performance metrics
- Generates visualizations and statistics

---

## Running Your Analysis

### Pre-Run Checklist

Before clicking Run, verify:

- ✅ Correct mode selected (Clustering or Classification)
- ✅ Input file path is correct and file exists
- ✅ Output file path is valid
- ✅ For Classification Training: Target column specified
- ✅ For Classification Prediction: Trained model file loaded

### What Happens When You Click Run

The application will:

1. Load your data file
2. Clean and prepare data automatically
3. Remove specified columns
4. Handle missing values
5. Train model or make predictions
6. Save results to output location
7. Generate logs and reports (if configured)

### Expected Processing Times

| Dataset Size | Typical Time |
|--------------|--------------|
| Small (< 1,000 rows) | 10-30 seconds |
| Medium (1,000-10,000 rows) | 1-5 minutes |
| Large (> 10,000 rows) | 5-30 minutes |
| GridSearch/BayesianOpt | Add 5-30 minutes |

Processing time depends on:
- Dataset size (rows and columns)
- Model chosen
- Computer processing power

---

## Understanding Results

### Clustering Results

**Output Contains:**
- All original data
- Additional column with cluster assignments (0, 1, 2, etc.)

**Analysis Steps:**
1. Open file in Excel
2. Sort by cluster number to group members
3. Look for patterns - what makes each cluster unique?
4. Calculate averages or statistics per cluster
5. Use cluster labels for further analysis or visualization

### Classification Training Results

**You'll Receive:**
- Model file (.pkl) for making predictions
- Log files showing training progress and accuracy
- Evaluation reports (if configured) with performance metrics
- Charts and graphs (if evaluations folder specified)

### Classification Prediction Results

**Output Contains:**
- All original input data
- **Predicted_Label** column: Model's predictions
- **Prediction_Confidence** column: Confidence percentage (0-100%)

### Evaluation Reports (Advanced)

If evaluations folder specified, you'll find:

| Report | Information |
|--------|-------------|
| **Feature Importance** | Which columns had biggest impact on predictions |
| **Confusion Matrix** | Detailed breakdown of correct/incorrect predictions |
| **Learning Curves** | How model performance improved during training |
| **Cross-Validation Scores** | Model reliability across different data subsets |

---

## Troubleshooting

### Common Issues and Solutions

| Problem | Solution |
|---------|----------|
| **File path errors** | Verify paths are correct and files exist |
| **File locked** | Close CSV file in Excel or other programs |
| **Missing headers** | Ensure data file has column names in first row |
| **Errors during run** | Check log file for detailed error messages |
| **Classification errors** | Verify target column name/index is correct |
| **Out of memory** | Try simpler model or reduce dataset size |
| **Disk space errors** | Ensure sufficient disk space for output files |

### Getting Help

If you encounter persistent issues:

1. Check the log file (if specified) for detailed error messages
2. Verify all file paths and data formatting
3. Try using default parameters first
4. Test with a smaller subset of your data
5. Ensure all prerequisites are installed correctly

---

## License

This project incorporates various third-party libraries and components, each with their respective licenses.

### Python Dependencies

| Package | License |
|---------|---------|
| pandas | BSD 3-Clause License |
| scikit-learn (sklearn) | BSD 3-Clause License |
| numpy | BSD License (modified) |
| matplotlib | PSF-based License (BSD-compatible) |
| seaborn | BSD 3-Clause License |
| lightgbm | MIT License |
| scikit-optimize (skopt) | BSD 3-Clause License |
| tqdm | MPL-2.0 AND MIT License (dual licensed) |
| joblib | BSD 3-Clause License |

### C#/.NET Dependencies

| Package | Version | License |
|---------|---------|---------|
| Newtonsoft.Json | 13.0.3 | MIT License |

### Standard .NET Framework 4.7.2 References

The following standard .NET libraries are covered by the .NET Framework License:
- System
- System.Core
- System.Xml.Linq
- System.Data.DataSetExtensions
- Microsoft.CSharp
- System.Data
- System.Deployment
- System.Drawing
- System.Net.Http
- System.Windows.Forms
- System.Xml

### License Summary

This project uses components under the following licenses:
- **MIT License**: 2 packages
- **BSD Licenses**: 7 packages (various BSD variants)
- **Dual License (MPL-2.0/MIT)**: 1 package
- **PSF-based License**: 1 package
- **.NET Framework License**: Standard framework libraries

All third-party licenses are compatible with open-source distribution. Please refer to individual package documentation for complete license texts.

---

**Last Updated**: 2025  
**Version**: 1.0  
**Framework**: .NET 4.7.2 | Python 3.x

---

## Contributing

Contributions are welcome! Please ensure all changes maintain compatibility with existing data formats and workflows.

## Support

For questions, issues, or feature requests, please refer to the troubleshooting section or contact support.
