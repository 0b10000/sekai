// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.

namespace Sekai.Framework.Windowing;

/// <summary>
/// Allows querying of the underlying native window of a given <see cref="IView"/>.
/// </summary>
public interface INativeWindowSource
{
    /// <summary>
    /// Native access to the window.
    /// </summary>
    public INativeWindow Native { get; }
}
