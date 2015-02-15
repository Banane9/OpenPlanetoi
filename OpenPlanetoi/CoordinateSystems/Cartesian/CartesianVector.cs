using OpenPlanetoi.CoordinateSystems.Spherical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPlanetoi.CoordinateSystems.Cartesian
{
    /// <summary>
    /// Represents a point in Cartesian Space.
    /// </summary>
    public struct CartesianVector
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
        /// Gets the length of the Vector.
        /// </summary>
        public double Length
        {
            get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)); }
        }

        /// <summary>
        /// Gets this Vector expressed as a unit vector.
        /// </summary>
        public CartesianVector AsUnitVector
        {
            get
            {
                var length = Length;

                return new CartesianVector(X / length, Y / length, Z / length);
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CartesianVector"/> struct with the given positions.
        /// </summary>
        /// <param name="x">The position on the horizontal axis (left to right).</param>
        /// <param name="y">The position on the vertical axis (bottom to top).</param>
        /// <param name="z">The position on the depth axis (far away to close).</param>
        public CartesianVector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #region Operators

        /// <summary>
        /// Calculates the cross product of the two Vectors.
        /// </summary>
        public static CartesianVector operator *(CartesianVector left, CartesianVector right)
        {
            return new CartesianVector(
                x: left.X * right.Y - right.X * left.Y,
                y: right.Z * left.Y - left.Z * right.Y,
                z: left.Z * right.X - right.Z * left.Z);
        }

        public static CartesianVector operator *(CartesianVector left, double right)
        {
            return new CartesianVector(left.X * right, left.Y * right, left.Z * right);
        }

        public static CartesianVector operator *(double left, CartesianVector right)
        {
            return right * left;
        }

        public static CartesianVector operator +(CartesianVector left, CartesianVector right)
        {
            return new CartesianVector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static CartesianVector operator -(CartesianVector left, CartesianVector right)
        {
            return new CartesianVector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static CartesianVector operator -(CartesianVector subject)
        {
            return new CartesianVector(-subject.X, -subject.Y, -subject.Z);
        }

        public static bool operator ==(CartesianVector left, CartesianVector right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public static bool operator !=(CartesianVector left, CartesianVector right)
        {
            return !(left == right);
        }

        public static implicit operator CartesianVector(SphereCoordinate sphereCoordinates)
        {
            // http://en.wikipedia.org/wiki/Spherical_coordinate_system#Cartesian_coordinates (different xyz order because of different directions)

            var x = sphereCoordinates.R * Math.Sin(sphereCoordinates.θ) * Math.Sin(sphereCoordinates.ϕ);
            var y = sphereCoordinates.R * Math.Cos(sphereCoordinates.θ);
            var z = sphereCoordinates.R * Math.Sin(sphereCoordinates.θ) * Math.Cos(sphereCoordinates.ϕ);

            return new CartesianVector(x, y, z);
        }

        #endregion Operators

        public override bool Equals(object obj)
        {
            if (!(obj is CartesianVector))
                return false;

            var coordObj = (CartesianVector)obj;

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