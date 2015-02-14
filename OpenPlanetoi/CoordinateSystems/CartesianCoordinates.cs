using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPlanetoi.CoordinateSystems
{
    /// <summary>
    /// Represents a point in Carteesian Space.
    /// </summary>
    public struct CartesianCoordinates
    {
        /// <summary>
        /// The position on the horizontal axis (left to right).
        /// </summary>
        public readonly double X;

        /// <summary>
        /// The position on the vertical axis (bottom to top).
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// The position on the depth axis (far away to close).
        /// </summary>
        public readonly double Z;

        /// <summary>
        /// Creates a new instance of the <see cref="CartesianCoordinates"/> struct with the given positions.
        /// </summary>
        /// <param name="x">The position on the horizontal axis (left to right).</param>
        /// <param name="y">The position on the vertical axis (bottom to top).</param>
        /// <param name="z">The position on the depth axis (far away to close).</param>
        public CartesianCoordinates(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static bool operator ==(CartesianCoordinates left, CartesianCoordinates right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public static bool operator !=(CartesianCoordinates left, CartesianCoordinates right)
        {
            return !(left == right);
        }

        public static implicit operator CartesianCoordinates(SphereCoordinates sphereCoordinates)
        {
            // http://en.wikipedia.org/wiki/Spherical_coordinate_system#Cartesian_coordinates (different xyz order because of different directions)

            var x = sphereCoordinates.R * Math.Sin(sphereCoordinates.θ) * Math.Sin(sphereCoordinates.ϕ);
            var y = sphereCoordinates.R * Math.Cos(sphereCoordinates.θ);
            var z = sphereCoordinates.R * Math.Sin(sphereCoordinates.θ) * Math.Cos(sphereCoordinates.ϕ);

            return new CartesianCoordinates(x, y, z);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CartesianCoordinates))
                return false;

            var coordObj = (CartesianCoordinates)obj;

            return coordObj == this;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return X.GetHashCode() * 13 + Y.GetHashCode() * 13 + Z.GetHashCode();
            }
        }

        public override string ToString()
        {
            return "Cartesian: " + X + "/" + Y + "/" + Z;
        }
    }
}