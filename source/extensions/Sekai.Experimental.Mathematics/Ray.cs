// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.

// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
//
// -----------------------------------------------------------------------------
// Original code from SlimMath project. http://code.google.com/p/slimmath/
// Greetings to SlimDX Group. Original code published with the following license:
// -----------------------------------------------------------------------------
/*
* Copyright (c) 2007-2011 SlimDX Group
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Sekai.Experimental.Mathematics;
/// <summary>
/// Represents a three dimensional line based on a point in space and a direction.
/// </summary>
[DataContract]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Ray : IEquatable<Ray>, IFormattable
{
    /// <summary>
    /// The position in three dimensional space where the ray starts.
    /// </summary>
    public Vector3 Position;

    /// <summary>
    /// The normalized direction in which the ray points.
    /// </summary>
    public Vector3 Direction;

    /// <summary>
    /// Initializes a new instance of the <see cref="Stride.Core.Mathematics.Ray"/> struct.
    /// </summary>
    /// <param name="position">The position in three dimensional space of the origin of the ray.</param>
    /// <param name="direction">The normalized direction of the ray.</param>
    public Ray(Vector3 position, Vector3 direction)
    {
        this.Position = position;
        this.Direction = direction;
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a point.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Vector3 point)
    {
        return CollisionHelper.RayIntersectsPoint(ref this, ref point);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.Ray"/>.
    /// </summary>
    /// <param name="ray">The ray to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Ray ray)
    {
        return CollisionHelper.RayIntersectsRay(ref this, ref ray, out var point);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.Ray"/>.
    /// </summary>
    /// <param name="ray">The ray to test.</param>
    /// <param name="point">When the method completes, contains the point of intersection,
    /// or <see cref="Stride.Core.Mathematics.Vector3.Zero"/> if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Ray ray, out Vector3 point)
    {
        return CollisionHelper.RayIntersectsRay(ref this, ref ray, out point);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.Plane"/>.
    /// </summary>
    /// <param name="plane">The plane to test</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Plane plane)
    {
        return CollisionHelper.RayIntersectsPlane(ref this, ref plane, out float distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.Plane"/>.
    /// </summary>
    /// <param name="plane">The plane to test.</param>
    /// <param name="distance">When the method completes, contains the distance of the intersection,
    /// or 0 if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Plane plane, out float distance)
    {
        return CollisionHelper.RayIntersectsPlane(ref this, ref plane, out distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.Plane"/>.
    /// </summary>
    /// <param name="plane">The plane to test.</param>
    /// <param name="point">When the method completes, contains the point of intersection,
    /// or <see cref="Stride.Core.Mathematics.Vector3.Zero"/> if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Plane plane, out Vector3 point)
    {
        return CollisionHelper.RayIntersectsPlane(ref this, ref plane, out point);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a triangle.
    /// </summary>
    /// <param name="vertex1">The first vertex of the triangle to test.</param>
    /// <param name="vertex2">The second vertex of the triangle to test.</param>
    /// <param name="vertex3">The third vertex of the triangle to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
    {
        return CollisionHelper.RayIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3, out float distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a triangle.
    /// </summary>
    /// <param name="vertex1">The first vertex of the triangle to test.</param>
    /// <param name="vertex2">The second vertex of the triangle to test.</param>
    /// <param name="vertex3">The third vertex of the triangle to test.</param>
    /// <param name="distance">When the method completes, contains the distance of the intersection,
    /// or 0 if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out float distance)
    {
        return CollisionHelper.RayIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3, out distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a triangle.
    /// </summary>
    /// <param name="vertex1">The first vertex of the triangle to test.</param>
    /// <param name="vertex2">The second vertex of the triangle to test.</param>
    /// <param name="vertex3">The third vertex of the triangle to test.</param>
    /// <param name="point">When the method completes, contains the point of intersection,
    /// or <see cref="Stride.Core.Mathematics.Vector3.Zero"/> if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out Vector3 point)
    {
        return CollisionHelper.RayIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3, out point);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.BoundingBox"/>.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref BoundingBox box)
    {
        return CollisionHelper.RayIntersectsBox(ref this, ref box, out float distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.BoundingBox"/>.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <param name="distance">When the method completes, contains the distance of the intersection,
    /// or 0 if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref BoundingBox box, out float distance)
    {
        return CollisionHelper.RayIntersectsBox(ref this, ref box, out distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.BoundingBox"/>.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <param name="point">When the method completes, contains the point of intersection,
    /// or <see cref="Stride.Core.Mathematics.Vector3.Zero"/> if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref BoundingBox box, out Vector3 point)
    {
        return CollisionHelper.RayIntersectsBox(ref this, ref box, out point);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The sphere to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref BoundingSphere sphere)
    {
        return CollisionHelper.RayIntersectsSphere(ref this, ref sphere, out float distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The sphere to test.</param>
    /// <param name="distance">When the method completes, contains the distance of the intersection,
    /// or 0 if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref BoundingSphere sphere, out float distance)
    {
        return CollisionHelper.RayIntersectsSphere(ref this, ref sphere, out distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Stride.Core.Mathematics.BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The sphere to test.</param>
    /// <param name="point">When the method completes, contains the point of intersection,
    /// or <see cref="Stride.Core.Mathematics.Vector3.Zero"/> if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref BoundingSphere sphere, out Vector3 point)
    {
        return CollisionHelper.RayIntersectsSphere(ref this, ref sphere, out point);
    }

    /// <summary>
    /// Tests for equality between two objects.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Ray left, Ray right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Tests for inequality between two objects.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Ray left, Ray right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return string.Format(CultureInfo.CurrentCulture, "Position:{0} Direction:{1}", Position.ToString(), Direction.ToString());
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public string ToString(string format)
    {
        return string.Format(CultureInfo.CurrentCulture, "Position:{0} Direction:{1}", Position.ToString(format, CultureInfo.CurrentCulture),
            Direction.ToString(format, CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public string ToString(IFormatProvider formatProvider)
    {
        return string.Format(formatProvider, "Position:{0} Direction:{1}", Position.ToString(), Direction.ToString());
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return string.Format(formatProvider, "Position:{0} Direction:{1}", Position.ToString(format ?? string.Empty, formatProvider ?? CultureInfo.CurrentCulture),
            Direction.ToString(format ?? string.Empty, formatProvider ?? CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode()
    {
        return Position.GetHashCode() + Direction.GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified <see cref="Stride.Core.Mathematics.Vector4"/> is equal to this instance.
    /// </summary>
    /// <param name="value">The <see cref="Stride.Core.Mathematics.Vector4"/> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="Stride.Core.Mathematics.Vector4"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Ray value)
    {
        return Position == value.Position && Direction == value.Direction;
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to this instance.
    /// </summary>
    /// <param name="value">The <see cref="object"/> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? value)
    {
        if (value == null)
            return false;

        if (value.GetType() != GetType())
            return false;

        return Equals((Ray)value);
    }
}
