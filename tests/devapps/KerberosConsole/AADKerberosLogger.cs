﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Identity.Client
{
    /// <summary>
    /// Helper to provide internal tracking log for debugging.
    /// </summary>
    public static class AADKerberosLogger
    {
        private static int _lastFileIndex = 0;

        /// <summary>
        /// Name of log file.
        /// </summary>
        private static string _logFileName = "";

        /// <summary>
        /// Filename to save log data.
        /// </summary>
        internal static string LogFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_logFileName))
                {
                    do
                    {
                        _lastFileIndex++;

                        _logFileName = String.Format(
                            CultureInfo.InvariantCulture,
                            @"C:\Users\yoko\Documents\AzureAD-MSAL-{0}-{1}.log",
                            DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                            _lastFileIndex);
                    } while (File.Exists(_logFileName));
                }
                return _logFileName;
            }
        }

        /// <summary>
        /// Save the given formatted log text to log file.
        /// </summary>
        /// <param name="format">The message formatter.</param>
        /// <param name="args">The message format variables.</param>
        public static void Format(string format, params object[] args)
        {
            var entry = string.Format(CultureInfo.InvariantCulture, format, args);
            Save(entry);
        }

        /// <summary>
        /// Save the given log text to log file.
        /// </summary>
        /// <param name="logText">Log message to be saved.</param>
        public static void Save(string logText)
        {
            Console.WriteLine("[AADKerberos] " + logText);
            File.AppendAllText(LogFileName, logText + Environment.NewLine);
        }

        /// <summary>
        /// Save the given log text to log file with timestamp.
        /// </summary>
        /// <param name="logText">Log message to be saved.</param>
        public static void SaveWithTimestamp(string logText)
        {
            string logMessage = DateTime.Now.ToString("s", CultureInfo.InvariantCulture) + ": " + logText;

            Console.WriteLine("[AADKerberos] " + logMessage);
            File.AppendAllText(
                LogFileName,
                Environment.NewLine + logMessage + Environment.NewLine);
        }

        /// <summary>
        /// Convert given string table as Loggable text.
        /// </summary>
        /// <param name="dict">String table to be converted.</param>
        /// <returns>Loggable test string.</returns>
        public static string ToString(IDictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in dict.Keys)
            {
                sb.Append("\n        " + key)
                    .Append(": ")
                    .Append(dict[key]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Convert given string list as Loggable text.
        /// </summary>
        /// <param name="list">String list to be converted.</param>
        /// <returns>Loggable test string.</returns>
        public static string ToString(IEnumerable<string> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in list)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(str);
            }

            return sb.ToString();
        }
    }
}
