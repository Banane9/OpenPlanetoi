using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPlanetoi.CoordinateSystems.Spherical
{
    public class Polygon
    {
        private readonly List<GreatCircleSegment> sides = new List<GreatCircleSegment>();
        private readonly Dictionary<SphereCoordinate, Tuple<GreatCircleSegment, GreatCircleSegment>> corners = new Dictionary<SphereCoordinate, Tuple<GreatCircleSegment, GreatCircleSegment>>();

        public double Area
        {
            get
            {
                // http://mathworld.wolfram.com/SphericalPolygon.html

                var interiorAngleSum = corners.Keys.Select(corner => GetCornerAngle(corner));

                return (interiorAngleSum.Sum() - ((corners.Keys.Count - 2) * Math.PI)) * Math.Pow(corners.Keys.First().Radius, 2);
            }
        }

        public Polygon(params SphereCoordinate[] points)
        {
            if (points.Length < 2)
                throw new ArgumentOutOfRangeException("points", "There has to be at least two points to make a polygon on a sphere.");

            if (points.Length > 2)
                for (var i = 0; i < points.Length - 1; ++i)
                    sides.Add(new GreatCircleSegment(points[i], points[i + 1]));

            sides.Add(new GreatCircleSegment(points[points.Length - 1], points[0]));

            corners.Add(sides[0].Start, new Tuple<GreatCircleSegment, GreatCircleSegment>(sides[0], sides[sides.Count - 1]));

            for (var i = 1; i < sides.Count; ++i)
                corners.Add(sides[i].Start, new Tuple<GreatCircleSegment, GreatCircleSegment>(sides[i], sides[i - 1]));
        }

        private double GetCornerAngle(SphereCoordinate corner)
        {
            var segments = corners[corner];

            var tangentVector1 = segments.Item1.BaseCircle.GetTangentAt(corner, corner != segments.Item1.Start ? segments.Item1.Start : segments.Item1.End);
            var tangentVector2 = segments.Item2.BaseCircle.GetTangentAt(corner, corner != segments.Item2.Start ? segments.Item2.Start : segments.Item2.End);

            return Math.Abs(Math.Acos(tangentVector1.DotProduct(tangentVector2)));
        }
    }
}