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

        public Polygon(params SphereCoordinate[] points)
        {
            if (points.Length < 2)
                throw new ArgumentOutOfRangeException("points", "There has to be at least two points to make a polygon on a sphere.");

            sides.Add(new GreatCircleSegment(points[0], points[points.Length - 1]));

            if (points.Length > 2)
                for (var i = 1; i < points.Length - 1; ++i)
                    sides.Add(new GreatCircleSegment(points[i], points[i - 1]));
        }
    }
}