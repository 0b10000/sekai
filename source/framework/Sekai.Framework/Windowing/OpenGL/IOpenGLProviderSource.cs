// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.

namespace Sekai.Framework.Windowing.OpenGL;

/// <summary>
/// An interface for windowing systems capable of providing OpenGL.
/// </summary>
public interface IOpenGLProviderSource
{
    /// <summary>
    /// The OpenGL provider.
    /// </summary>
    IOpenGLProvider GL { get; }
}
