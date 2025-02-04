// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.

// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace Sekai.Experimental.Mathematics;

/// <summary>
/// Implementation of a "Guillotine" packer.
/// More information at http://clb.demon.fi/files/RectangleBinPack.pdf.
/// </summary>
public class GuillotinePacker
{
    private readonly List<Rectangle> freeRectangles = new();
    private readonly List<Rectangle> tempFreeRectangles = new();

    /// <summary>
    /// A delegate callback used by <see cref="TryInsert"/>
    /// </summary>
    /// <param name="cascadeIndex">The index of the rectangle</param>
    /// <param name="rectangle">The rectangle found</param>
    public delegate void InsertRectangleCallback(int cascadeIndex, ref Rectangle rectangle);

    /// <summary>
    /// Current width used by the packer.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Current height used by the packer.
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// Clears the specified region.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public void Clear(int width, int height)
    {
        freeRectangles.Clear();
        freeRectangles.Add(new Rectangle { X = 0, Y = 0, Width = width, Height = height });

        Width = width;
        Height = height;
    }

    /// <summary>
    /// Clears the whole region.
    /// </summary>
    public virtual void Clear()
    {
        Clear(Width, Height);
    }

    /// <summary>
    /// Frees the specified old rectangle.
    /// </summary>
    /// <param name="oldRectangle">The old rectangle.</param>
    public void Free(ref Rectangle oldRectangle)
    {
        freeRectangles.Add(oldRectangle);
    }

    /// <summary>
    /// Tries to fit a single rectangle with the specified width and height.
    /// </summary>
    /// <param name="width">Width requested.</param>
    /// <param name="height">Height requested</param>
    /// <param name="bestRectangle">Fill with the rectangle if it was successfully inserted.</param>
    /// <returns><c>true</c> if it was successfully inserted.</returns>
    public bool Insert(int width, int height, ref Rectangle bestRectangle)
    {
        return insert(width, height, freeRectangles, ref bestRectangle);
    }

    /// <summary>
    /// Tries to fit multiple rectangle with (width, height).
    /// </summary>
    /// <param name="width">Width requested.</param>
    /// <param name="height">Height requested</param>
    /// <param name="count">The number of rectangle to fit.</param>
    /// <param name="inserted">A callback called for each rectangle successfully fitted.</param>
    /// <returns><c>true</c> if all rectangles were successfully fitted.</returns>
    public bool TryInsert(int width, int height, int count, InsertRectangleCallback inserted)
    {
        var bestRectangle = new Rectangle();
        tempFreeRectangles.Clear();
        foreach (var freeRectangle in freeRectangles)
        {
            tempFreeRectangles.Add(freeRectangle);
        }

        for (int i = 0; i < count; ++i)
        {
            if (!insert(width, height, tempFreeRectangles, ref bestRectangle))
            {
                tempFreeRectangles.Clear();
                return false;
            }

            inserted(i, ref bestRectangle);
        }

        // if the insertion went well, use the new configuration
        freeRectangles.Clear();
        foreach (var tempFreeRectangle in tempFreeRectangles)
        {
            freeRectangles.Add(tempFreeRectangle);
        }
        tempFreeRectangles.Clear();

        return true;
    }

    private static bool insert(int width, int height, List<Rectangle> freeRectanglesList, ref Rectangle bestRectangle)
    {
        // Info on algorithm: http://clb.demon.fi/files/RectangleBinPack.pdf
        int bestScore = int.MaxValue;
        int freeRectangleIndex = -1;

        // Find space for new rectangle
        for (int i = 0; i < freeRectanglesList.Count; ++i)
        {
            var currentFreeRectangle = freeRectanglesList[i];
            if (width == currentFreeRectangle.Width && height == currentFreeRectangle.Height)
            {
                // Perfect fit
                bestRectangle.X = currentFreeRectangle.X;
                bestRectangle.Y = currentFreeRectangle.Y;
                bestRectangle.Width = width;
                bestRectangle.Height = height;
                freeRectangleIndex = i;
                break;
            }
            if (width <= currentFreeRectangle.Width && height <= currentFreeRectangle.Height)
            {
                // Can fit inside
                // Use "BAF" heuristic (best area fit)
                int score = currentFreeRectangle.Width * currentFreeRectangle.Height - width * height;
                if (score < bestScore)
                {
                    bestRectangle.X = currentFreeRectangle.X;
                    bestRectangle.Y = currentFreeRectangle.Y;
                    bestRectangle.Width = width;
                    bestRectangle.Height = height;
                    bestScore = score;
                    freeRectangleIndex = i;
                }
            }
        }

        // No space could be found
        if (freeRectangleIndex == -1)
            return false;

        var freeRectangle = freeRectanglesList[freeRectangleIndex];

        // Choose an axis to split (trying to minimize the smaller area "MINAS")
        int w = freeRectangle.Width - bestRectangle.Width;
        int h = freeRectangle.Height - bestRectangle.Height;
        bool splitHorizontal = (bestRectangle.Width * h > w * bestRectangle.Height);

        // Form the two new rectangles.
        var bottom = new Rectangle { X = freeRectangle.X, Y = freeRectangle.Y + bestRectangle.Height, Width = splitHorizontal ? freeRectangle.Width : bestRectangle.Width, Height = h };
        var right = new Rectangle { X = freeRectangle.X + bestRectangle.Width, Y = freeRectangle.Y, Width = w, Height = splitHorizontal ? bestRectangle.Height : freeRectangle.Height };

        if (bottom.Width > 0 && bottom.Height > 0)
            freeRectanglesList.Add(bottom);
        if (right.Width > 0 && right.Height > 0)
            freeRectanglesList.Add(right);

        // Remove previously selected freeRectangle
        if (freeRectangleIndex != freeRectanglesList.Count - 1)
            freeRectanglesList[freeRectangleIndex] = freeRectanglesList[^1];
        freeRectanglesList.RemoveAt(freeRectanglesList.Count - 1);

        return true;
    }
}
