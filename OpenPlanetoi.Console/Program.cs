using OpenPlanetoi.CoordinateSystems;
using OpenPlanetoi.CoordinateSystems.Cartesian;
using OpenPlanetoi.CoordinateSystems.Spherical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cons = System.Console;

namespace OpenPlanetoi.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var start1 = new SphereCoordinate(2, Math.PI / 2, 1.75 * Math.PI);
            Cons.WriteLine((CartesianVector)start1);

            var end1 = new SphereCoordinate(2, Math.PI / 2, Math.PI / 4);
            Cons.WriteLine((CartesianVector)end1);

            var start2 = new SphereCoordinate(2, Math.PI / 4, 0);
            Cons.WriteLine((CartesianVector)start2);

            var end2 = new SphereCoordinate(2, 0.75 * Math.PI, 0);
            Cons.WriteLine((CartesianVector)end2);

            var arc1 = new ArcSegment(start1, end1);
            var arc2 = new ArcSegment(start2, end2);

            SphereCoordinate intersection;
            Cons.WriteLine(arc1.Intersects(arc2, out intersection) + "   " + intersection + "   " + (CartesianVector)intersection);

            Cons.ReadLine();
        }
    }
}