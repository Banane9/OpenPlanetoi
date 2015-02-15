using OpenPlanetoi.CoordinateSystems.Cartesian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPlanetoi.CoordinateSystems.Spherical
{
    public struct ArcSegment
    {
        public readonly SphereCoordinate Start;

        public readonly SphereCoordinate End;

        public readonly double Length;

        public ArcSegment(SphereCoordinate start, SphereCoordinate end)
        {
            Start = start;
            End = end;
            Length = CalculateArcLength(start, end);
        }

        /// <summary>
        /// Checks whether the given <see cref="ArcSegment"/> intersects with this one.
        /// The point of intersection can then be found the out-Parameter.
        /// </summary>
        /// <param name="other">The arc segment to be checked.</param>
        /// <param name="intersection">The point of intersection, if any.</param>
        /// <returns>Whether or not the two <see cref="ArcSegment"/>s intersect.</returns>
        public bool Intersects(ArcSegment other, out SphereCoordinate intersection)
        {
            // http://www.boeing-727.com/Data/fly%20odds/distance.html

            if (!Start.R.IsAlmostEqualTo(other.Start.R))
                throw new ArgumentOutOfRangeException(Start.R > other.Start.R ? "this" : "other", "Radius is larger than that of the other Sphere Coordinates.");

            intersection = default(SphereCoordinate);

            if (Length.IsAlmostEqualTo(0) || other.Length.IsAlmostEqualTo(0))
                return false;

            var planeUnitVector1 = ((CartesianVector)Start * End).AsUnitVector;
            var planeUnitVector2 = ((CartesianVector)other.Start * other.End).AsUnitVector;

            if (planeUnitVector1 == planeUnitVector2)
                return false;

            var unitVectorDirector = (planeUnitVector1 * planeUnitVector2).AsUnitVector;

            var possibleIntersection1 = unitVectorDirector * Start.R;
            var possibleIntersection2 = -unitVectorDirector * Start.R;

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
        /// Calculates the length of a Great Circle arc between the two <see cref="SphereCoordinate"/>s.
        /// Coordinates must have the same radius.
        /// </summary>
        /// <param name="start">One point of the arc.</param>
        /// <param name="end">Other point of the arc.</param>
        /// <returns>The length of the Great Circle arc between the two <see cref="SphereCoordinate"/>s.</returns>
        public static double CalculateArcLength(SphereCoordinate start, SphereCoordinate end)
        {
            // http://www.had2know.com/academics/great-circle-distance-sphere-2-points.html

            if (!start.R.IsAlmostEqualTo(end.R))
                throw new ArgumentOutOfRangeException(start.R > end.R ? "start" : "end", "Radius is larger than that of the other Sphere Coordinates.");

            var cartesianLength = ((CartesianVector)start - end).Length;

            return 2 * start.R * Math.Asin(cartesianLength / (2 * start.R));
        }
    }
}