﻿// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         AutoEvent
//    Project:          AutoEvent
//    FileName:         DebugLogger.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/05/2023 7:30 PM
//    Created Date:     09/05/2023 7:30 PM
// -----------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using PluginAPI.Core;
using PluginAPI.Helpers;
using UnityEngine;

namespace AutoEvent;

public class DebugLogger
{
    public static DebugLogger Singleton;

    public DebugLogger(bool writeDirectly)
    {
        Singleton = this;
        WriteDirectly = writeDirectly;
        _debugLogs = new List<string>();
        _loaded = true;

        if (!Directory.Exists(AutoEvent.BaseConfigPath))
        {
            Directory.CreateDirectory(AutoEvent.BaseConfigPath);
        }

        try
        {

            _filePath = Path.Combine(AutoEvent.BaseConfigPath, "debug-output.log");
            if (WriteDirectly)
            {
                DebugLogger.LogDebug($"Writing debug output directly to \"{_filePath}\"");
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }

                File.Create(_filePath).Close();
            }
        }
        catch (Exception e)
        {
            DebugLogger.LogDebug($"An error has occured while trying to create a debug log.", LogLevel.Warn, true);
            DebugLogger.LogDebug($"{e}");
        }
}

    private string _filePath;
    private static bool _loaded = false;
    public static bool Debug = false;
    public static bool AntiEnd = false;
    public static bool WriteDirectly = false;
    private List<string> _debugLogs;
    public static void LogDebug(string input, LogLevel level = LogLevel.Debug, bool outputIfNotDebug = false)
    {
        if (_loaded)
        {
            string log = $"[{level.ToString()}] {(!outputIfNotDebug ? "[Hidden] ": "")}" + input;
            if (!WriteDirectly)
                Singleton._debugLogs.Add(log);
            else
                File.AppendAllText(Singleton._filePath, "\n" + log);
        }
            
        if (outputIfNotDebug || AutoEvent.Debug)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    Log.Debug(input);
                    break;
                case LogLevel.Error:
                    Log.Error(input);
                    break;
                case LogLevel.Info:
                    Log.Info(input);
                    break;
                case LogLevel.Warn:
                    Log.Warning(input);
                    break;
            }
        }
    }

    public static void WriteOutput()
    {
        if (WriteDirectly)
            return;
        if (_loaded)
            return;
        
        if (File.Exists(Singleton._filePath))
        {
            File.Delete(Singleton._filePath);
        }
        File.WriteAllLines(Singleton._filePath, Singleton._debugLogs);
    }
}

public enum LogLevel
{
    Info,
    Debug,
    Warn,
    Error,
}