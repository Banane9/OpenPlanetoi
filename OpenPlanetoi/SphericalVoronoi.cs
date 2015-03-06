using OpenPlanetoi.CoordinateSystems.Spherical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenPlanetoi
{
    public class SphericalVoronoi
    {
        private readonly List<SphereCoordinate> points = new List<SphereCoordinate>();

        public SphericalVoronoi(params SphereCoordinate[] points)
        {
            this.points.AddRange(points);
        }
    }
}