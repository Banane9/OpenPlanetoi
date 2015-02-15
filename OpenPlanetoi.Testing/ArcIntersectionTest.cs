using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPlanetoi.CoordinateSystems.Spherical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPlanetoi.Testing
{
    [TestClass]
    public class ArcIntersectionTest
    {
        [TestMethod]
        public void HandlesArcGoingThrough0Azimuthal()
        {
            var start1 = new SphereCoordinate(2, Math.PI / 2, 1.75 * Math.PI);
            var end1 = new SphereCoordinate(2, Math.PI / 2, Math.PI / 4);

            var start2 = new SphereCoordinate(2, Math.PI / 4, 0);
            var end2 = new SphereCoordinate(2, 0.75 * Math.PI, 0);

            var arc1 = new ArcSegment(start1, end1);
            var arc2 = new ArcSegment(start2, end2);

            SphereCoordinate intersection;
            Assert.IsTrue(arc1.Intersects(arc2, out intersection));
            Assert.AreEqual(intersection, new SphereCoordinate(2, Math.PI / 2, 0));
        }
    }
}