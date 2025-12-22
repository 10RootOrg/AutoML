using System;
using System.IO;

namespace AutoMLGUI.Helpers
{
    internal class PathHelper
    {
        /// <summary>
        /// Gets the AutoML directory (where main.py and config.json are located)
        /// </summary>
        public static string AutoMLDirectory => Path.Combine(AppDirectory, "AutoML");

        /// <summary>
        /// Gets the application directory (where the exe is located)
        /// </summary>
        public static string AppDirectory => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Converts an absolute path to a relative path (relative to AutoML directory)
        /// Returns the original path if it's already relative or can't be made relative
        /// </summary>
        public static string ToRelativePath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return absolutePath;

            // Already a relative path
            if (!Path.IsPathRooted(absolutePath))
                return absolutePath;

            string autoMLDir = AutoMLDirectory.Replace("\\", "/").TrimEnd('/') + "/";
            string normalizedPath = absolutePath.Replace("\\", "/");

            // Check if path is under the AutoML directory
            if (normalizedPath.StartsWith(autoMLDir, StringComparison.OrdinalIgnoreCase))
            {
                // Return relative path without ./ prefix
                string relativePath = normalizedPath.Substring(autoMLDir.Length);
                return relativePath;
            }

            // Path is outside AutoML directory, keep absolute
            return normalizedPath;
        }

        /// <summary>
        /// Converts a relative path to an absolute path (relative to AutoML directory)
        /// Supports: "path", "./path", "/path", "../path"
        /// Returns the original path if it's already absolute (e.g., C:/...)
        /// </summary>
        public static string ToAbsolutePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return relativePath;

            string cleanPath = relativePath.Replace("\\", "/");

            // Check if it's a Windows absolute path (C:/ or D:/ etc.)
            if (cleanPath.Length > 2 && cleanPath[1] == ':')
                return cleanPath;

            // Remove leading ./ or .\ if present
            if (cleanPath.StartsWith("./"))
                cleanPath = cleanPath.Substring(2);

            // Remove leading / if present (treat /path as relative to AutoML)
            if (cleanPath.StartsWith("/") && (cleanPath.Length < 2 || cleanPath[1] != '/'))
                cleanPath = cleanPath.Substring(1);

            // Combine with AutoML directory
            string absolutePath = Path.Combine(AutoMLDirectory, cleanPath);
            return absolutePath.Replace("\\", "/");
        }

        /// <summary>
        /// Checks if a string looks like a file path
        /// </summary>
        public static bool LooksLikePath(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            // Check for common path indicators
            return value.Contains("/") ||
                   value.Contains("\\") ||
                   value.StartsWith("./") ||
                   value.StartsWith("../") ||
                   (value.Length > 2 && value[1] == ':') || // Windows drive letter
                   value.EndsWith(".csv") ||
                   value.EndsWith(".pkl") ||
                   value.EndsWith(".log") ||
                   value.EndsWith(".json") ||
                   value.EndsWith(".txt");
        }
    }
}
