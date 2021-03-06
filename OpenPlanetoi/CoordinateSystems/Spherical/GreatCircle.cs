﻿using OpenPlanetoi.CoordinateSystems.Cartesian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPlanetoi.CoordinateSystems.Spherical
{
    /// <summary>
    /// Represents a great circle on the unit sphere.
    /// </summary>
    public struct GreatCircle
    {
        /// <summary>
        /// The vector perpendicular to the plane that produces the great circle (a unit vector).
        /// </summary>
        public readonly CartesianVector DefinitionVector;

        /// <summary>
        /// Creates a new instance of the <see cref="GreatCircle"/> struct given two points defining it.
        /// </summary>
        /// <param name="start">The start coordinate of the segment.</param>
        /// <param name="end">The end coordinate of the segment.</param>
        public GreatCircle(CartesianVector start, CartesianVector end)
            : this(start.CrossProduct(end).AsUnitVector)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GreatCircle"/> struct given a segment of it.
        /// </summary>
        /// <param name="segment">A part of the great circle.</param>
        public GreatCircle(GreatCircleSegment segment)
            : this(segment.Start, segment.End)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="GreatCircle"/> struct with the given definition vector.
        /// </summary>
        /// <param name="definitionVector">The vector perpendicular to the plane that produces the great circle.</param>
        public GreatCircle(CartesianVector definitionVector)
        {
            DefinitionVector = definitionVector.AsUnitVector;
        }

        /// <summary>
        /// Returns one possible tangent vector of this <see cref="GreatCircle"/> in that point, the other is antipodal to it.
        /// </summary>
        /// <param name="point">The point on the <see cref="GreatCircle"/> that the tangent is wanted for.</param>
        /// <returns>One possible tangent vector.</returns>
        public CartesianVector GetTangentAt(CartesianVector point)
        {
            return DefinitionVector.CrossProduct(point).AsUnitVector;
        }

        /// <summary>
        /// Returns the tangent vector that points in the specified direction.
        /// </summary>
        /// <param name="point">The point on the <see cref="GreatCircle"/> that the tangent is wanted for.</param>
        /// <param name="direction">The point specifying in which direction the tangent vector will point.</param>
        /// <returns>The tangent vector pointing in the right direction.</returns>
        public CartesianVector GetTangentAt(CartesianVector point, CartesianVector direction)
        {
            var possibleTangent = GetTangentAt(point);
            var otherPossibleTangent = -possibleTangent;
            var intendedDirection = (direction - point).AsUnitVector;

            // Don't need to divide by the length because the vectors are both unit vectors.
            var possibleAngle = possibleTangent.DotProduct(intendedDirection);
            var otherPossibleAngle = otherPossibleTangent.DotProduct(intendedDirection);

            return possibleAngle < otherPossibleAngle ? possibleTangent : otherPossibleTangent;
        }

        /// <summary>
        /// Checks whether the given <see cref="GreatCircle"/> intersects with this one.
        /// One point of intersection can then be found in the out-Parameter, the other is the antipodal point to it.
        /// </summary>
        /// <param name="other">The other <see cref="GreatCircle"/>.</param>
        /// <param name="intersection">The point of intersection, if they intersect.</param>
        /// <returns>Whether the two <see cref="GreatCircle"/>s intersect or not.</returns>
        public bool Intersects(GreatCircle other, out CartesianVector intersection)
        {
            // http://www.boeing-727.com/Data/fly%20odds/distance.html

            intersection = default(CartesianVector);

            if (this == other)
                return false;

            intersection = DefinitionVector.CrossProduct(other.DefinitionVector).AsUnitVector;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GreatCircle))
                return false;

            return this == (GreatCircle)obj;
        }

        public override int GetHashCode()
        {
            return DefinitionVector.GetHashCode();
        }

        public override string ToString()
        {
            return "Great Circle: " + DefinitionVector.ToString();
        }

        public static bool operator ==(GreatCircle left, GreatCircle right)
        {
            return left.DefinitionVector == right.DefinitionVector;
        }

        public static bool operator !=(GreatCircle left, GreatCircle right)
        {
            return !(left == right);
        }
    }
}