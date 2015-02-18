using OpenPlanetoi.CoordinateSystems.Cartesian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPlanetoi.CoordinateSystems.Spherical
{
    public struct GreatCircleSegment
    {
        /// <summary>
        /// Gets the midpoint of the segment.
        /// </summary>
        public CartesianVector Midpoint
        {
            get
            {
                var θ = Math.Min(Start.θ, End.θ) + (Math.Abs(Start.θ - End.θ) / 2);
                var ϕ = Math.Min(Start.ϕ, End.ϕ) + (Math.Abs(Start.ϕ - End.ϕ) / 2);

                var midpoint = (CartesianVector)new SphereCoordinate(Start.R, θ, ϕ);
                if (IsOnArc(midpoint))
                    return midpoint;

                return -midpoint;
            }
        }

        /// <summary>
        /// The <see cref="GreatCircle"/> that this segment is part of.
        /// </summary>
        public readonly GreatCircle BaseCircle;

        /// <summary>
        /// The start coordinate of the segment.
        /// </summary>
        public readonly SphereCoordinate Start;

        /// <summary>
        /// The end coordinate of the segment.
        /// </summary>
        public readonly SphereCoordinate End;

        /// <summary>
        /// The length of the segment.
        /// </summary>
        public readonly double Length;

        /// <summary>
        /// Creates a new instance of the <see cref="GreatCircleSegment"/> struct with the given points defininf it.
        /// </summary>
        /// <param name="start">The start coordinate of the segment.</param>
        /// <param name="end">The end coordinate of the segment.</param>
        public GreatCircleSegment(SphereCoordinate start, SphereCoordinate end)
        {
            Start = start;
            End = end;
            BaseCircle = new GreatCircle(start, end);
            Length = CalculateArcLength(start, end);
        }

        /// <summary>
        /// Checks whether the given <see cref="GreatCircleSegment"/> intersects with this one.
        /// The point of intersection can then be found in the out-Parameter.
        /// </summary>
        /// <param name="other">The arc segment to be checked.</param>
        /// <param name="intersection">The point of intersection, if they intersect.</param>
        /// <returns>Whether the two <see cref="GreatCircleSegment"/>s intersect or not.</returns>
        public bool Intersects(GreatCircleSegment other, out SphereCoordinate intersection)
        {
            // http://www.boeing-727.com/Data/fly%20odds/distance.html

            if (!Start.R.IsAlmostEqualTo(other.Start.R))
                throw new ArgumentOutOfRangeException(Start.R > other.Start.R ? "this" : "other", "Radius is larger than that of the other Sphere Coordinates.");

            intersection = default(SphereCoordinate);

            if (Length.IsAlmostEqualTo(0) || other.Length.IsAlmostEqualTo(0))
                return false;

            CartesianVector possibleIntersection1;
            if (!BaseCircle.Intersects(other.BaseCircle, out possibleIntersection1))
                return false;

            possibleIntersection1 *= Start.R;
            var possibleIntersection2 = -possibleIntersection1;

            if (IsOnArc(possibleIntersection1) && other.IsOnArc(possibleIntersection1))
            {
                intersection = possibleIntersection1;
                return true;
            }

            if (IsOnArc(possibleIntersection2) && other.IsOnArc(possibleIntersection2))
            {
                intersection = possibleIntersection2;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether a given <see cref="SphereCoordinate"/> is on this arc.
        /// </summary>
        /// <param name="sphereCoordinate">The <see cref="SphereCoordinate"/> to test.</param>
        /// <returns>Whether the coordinate is on this arc.</returns>
        public bool IsOnArc(SphereCoordinate sphereCoordinate)
        {
            // http://www.boeing-727.com/Data/fly%20odds/distance.html Step 1.7

            var startToCoord = CalculateArcLength(Start, sphereCoordinate);
            var endToCoord = CalculateArcLength(End, sphereCoordinate);

            return (Length - startToCoord - endToCoord).IsAlmostEqualTo(0);
        }

        /// <summary>
        /// Calculates the length of a Great Circle segment between the two <see cref="SphereCoordinate"/>s.
        /// Coordinates must have the same radius.
        /// </summary>
        /// <param name="start">One point of the arc.</param>
        /// <param name="end">Other point of the arc.</param>
        /// <returns>The length of the Great Circle segment between the two <see cref="SphereCoordinate"/>s.</returns>
        public static double CalculateArcLength(SphereCoordinate start, SphereCoordinate end)
        {
            // http://www.had2know.com/academics/great-circle-distance-sphere-2-points.html

            if (!start.R.IsAlmostEqualTo(end.R))
                throw new ArgumentOutOfRangeException(start.R > end.R ? "start" : "end", "Radius is larger than that of the other Sphere Coordinate.");

            var cartesianLength = ((CartesianVector)start - end).Length;

            return 2 * start.R * Math.Asin(cartesianLength / (2 * start.R));
        }
    }
}