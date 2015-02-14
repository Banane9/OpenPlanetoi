using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPlanetoi.CoordinateSystems;
using System;

namespace OpenPlanetoi.Testing
{
    [TestClass]
    public class SphereCoordinatesTest
    {
        [TestMethod]
        public void HandlesGreaterTheta()
        {
            var sphereCoord1 = new SphereCoordinates(1, 3 * Math.PI, 0);
            var sphereCoord2 = new SphereCoordinates(1, 4 * Math.PI, 0);

            Assert.AreEqual(new SphereCoordinates(1, Math.PI, 0), sphereCoord1);
            Assert.AreEqual(new SphereCoordinates(1, 0, Math.PI), sphereCoord2);
        }

        [TestMethod]
        public void HandlesSmallerTheta()
        {
            var sphereCoord1 = new SphereCoordinates(1, -Math.PI, 0);
            var sphereCoord2 = new SphereCoordinates(1, -2 * Math.PI, 0);

            Assert.AreEqual(new SphereCoordinates(1, Math.PI, 0), sphereCoord1);
            Assert.AreEqual(new SphereCoordinates(1, 0, Math.PI), sphereCoord2);
        }
    }
}