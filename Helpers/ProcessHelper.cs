using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AutoMLGUI.Helpers
{
    internal class ProcessHelper
    {
        public static void RunAutoMLPython(string additionalArgs)
        {
            try
            {
                string autoMLDirectory = PathHelper.AutoMLDirectory;
                string pythonPath = Path.Combine(autoMLDirectory, "automl_env", "python.exe");
                string scriptPath = Path.Combine(autoMLDirectory, "main.py");

                if (!File.Exists(pythonPath))
                {
                    MessageBox.Show("Error: Python executable not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!File.Exists(scriptPath))
                {
                    MessageBox.Show("Error: Python script (main.py) not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ensure required output directories exist
                EnsureDirectoriesExist(autoMLDirectory);

                // Start the Python process with UTF-8 encoding
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    WorkingDirectory = autoMLDirectory,
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\" {additionalArgs}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                // Set PYTHONIOENCODING environment variable to force UTF-8
                psi.EnvironmentVariables["PYTHONIOENCODING"] = "utf-8";
                psi.EnvironmentVariables["PYTHONUTF8"] = "1";

                using (Process process = new Process { StartInfo = psi })
                {
                    StringBuilder outputBuilder = new StringBuilder();
                    StringBuilder errorBuilder = new StringBuilder();

                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data != null)
                            outputBuilder.AppendLine(e.Data);
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data != null)
                            errorBuilder.AppendLine(e.Data);
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();

                    string output = outputBuilder.ToString();
                    string error = errorBuilder.ToString();

                    // Check exit code to determine if it was actually an error
                    // Python logging writes to stderr by default, so we can't assume stderr = error
                    if (process.ExitCode != 0)
                    {
                        string errorMsg = !string.IsNullOrWhiteSpace(error) ? error : output;
                        MessageBox.Show($"Python Error (Exit Code {process.ExitCode}):\n{errorMsg.Substring(0, Math.Min(errorMsg.Length, 2000))}",
                            "Python Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Process completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error running Python:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ensures all required output directories exist before running Python
        /// </summary>
        private static void EnsureDirectoriesExist(string autoMLDirectory)
        {
            string[] requiredFolders = new string[]
            {
                Path.Combine(autoMLDirectory, "Results"),
                Path.Combine(autoMLDirectory, "Results", "logs"),
                Path.Combine(autoMLDirectory, "Results", "evaluations"),
                Path.Combine(autoMLDirectory, "Results", "saved_models")
            };

            foreach (string folder in requiredFolders)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
        }
    }
}
