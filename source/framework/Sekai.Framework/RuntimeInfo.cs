// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.

using System;
using System.Reflection;

namespace Sekai.Framework;

/// <summary>
/// Provides context of the current runtime environment.
/// </summary>
public static class RuntimeInfo
{
    /// <summary>
    /// Whether execution is currently in debug moe.
    /// </summary>
    public static readonly bool IsDebug = Assembly
        .GetExecutingAssembly()
        .GetCustomAttribute<AssemblyConfigurationAttribute>()
        ?.Configuration.Contains("Debug", StringComparison.InvariantCultureIgnoreCase) ?? false;

    /// <summary>
    /// Gets the operating system currently in execution.
    /// </summary>
    public static Platform OS { get; }

    /// <summary>
    /// Whether the environment is a UNIX system or not.
    /// </summary>
    public static bool IsUnix => OS != Platform.Windows;

    /// <summary>
    /// Whether the environment supports just-in-time compilation or not.
    /// </summary>
    public static bool SupportsJIT => OS != Platform.iOS;

    /// <summary>
    /// Whether the environment is desktop-based or not.
    /// </summary>
    public static bool IsDesktop => OS is <= Platform.Windows or > Platform.iOS;

    /// <summary>
    /// Whether the enivornment is mobile-based or not.
    /// </summary>
    public static bool IsMobile => OS is < Platform.macOS or >= Platform.Android;

    /// <summary>
    /// Whether the environment is Apple-based or not.
    /// </summary>
    public static bool IsApple => OS is Platform.iOS or Platform.macOS;

    static RuntimeInfo()
    {
        if (OperatingSystem.IsWindows())
            OS = Platform.Windows;

        if (OperatingSystem.IsLinux())
            OS = Platform.Linux;

        if (OperatingSystem.IsMacOS())
            OS = Platform.macOS;

        if (OperatingSystem.IsAndroid())
            OS = Platform.Android;

        if (OperatingSystem.IsIOS())
            OS = Platform.iOS;
    }

    public enum Platform
    {
        Windows = 1,
        Linux = 2,
        macOS = 3,
        iOS = 4,
        Android = 5,
    }
}
