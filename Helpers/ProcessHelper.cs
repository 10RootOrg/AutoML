using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AutoMLGUI.Helpers
{
    internal class ProcessHelper
    {
        public static void RunAutoMLPython(string additionalArgs)
        {
            try
            {
                string autoMLDirectory = Path.Combine(Directory.GetCurrentDirectory(), "AutoML");
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

                // 🔥 Start the Python process
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    WorkingDirectory = autoMLDirectory,
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\" {additionalArgs}", // ✅ Pass main.py and extra args
                    //RedirectStandardOutput = true,
                    //RedirectStandardError = true,
                    UseShellExecute = false, // ❌ Don't use system shell (allows capturing output)
                    CreateNoWindow = true // ✅ Run in the background
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();
                    process.WaitForExit(); // ⏳ Wait for the process to finish
                }
                MessageBox.Show("Process completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error running Python:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
