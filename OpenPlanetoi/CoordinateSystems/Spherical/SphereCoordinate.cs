using OpenPlanetoi.CoordinateSystems.Cartesian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenPlanetoi.CoordinateSystems.Spherical
{
    /// <summary>
    /// Represents a point on a Sphere centered at the Cartesian Point (0/0/0).
    /// </summary>
    public struct SphereCoordinate
    {
        /// <summary>
        /// The azimuthal angle (rotation away from pointing straight to the front).
        /// </summary>
        public readonly double ϕ;

        /// <summary>
        /// The radius of the Sphere (Euclidian Distance in Carteesian Space).
        /// </summary>
        public readonly double Radius;

        /// <summary>
        /// The polar angle (rotation away from pointing straight up).
        /// </summary>
        public readonly double θ;

        /// <summary>
        /// Creates a new instance of the <see cref="SphereCoordinate"/> struct with the given positions.
        /// </summary>
        /// <param name="r">The radius of the Sphere (Euclidian Distance in Carteesian Space). Will be made positive.</param>
        /// <param name="θ">The polar angle (rotation away from pointing straight up). Will be made to fit into [0, Pi].</param>
        /// <param name="ϕ">The azimuthal angle (rotation away from pointing straight to the front). Will be made to fit with any changes to the polar angle and to fit into [0, 2Pi].</param>
        public SphereCoordinate(double r, double θ, double ϕ)
        {
            Radius = Math.Abs(r);
            this.θ = θ;
            this.ϕ = ϕ;

            if (θ < 0 || θ > Math.PI || ϕ < -Math.PI || ϕ > Math.PI)
                this = (SphereCoordinate)(CartesianVector)this;
        }

        public override string ToString()
        {
            return "Spherical: " + Radius + "/" + θ + "/" + ϕ;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SphereCoordinate))
                return false;

            var coordObj = (SphereCoordinate)obj;

            return coordObj == this;
        }

        public static implicit operator SphereCoordinate(CartesianVector cartesianCoordinates)
        {
            // http://en.wikipedia.org/wiki/Spherical_coordinate_system#Cartesian_coordinates (different xyz because of different directions)

            var r = Math.Sqrt(Math.Pow(cartesianCoordinates.X, 2) + Math.Pow(cartesianCoordinates.Y, 2) + Math.Pow(cartesianCoordinates.Z, 2));
            var θ = Math.Acos(cartesianCoordinates.Y / r);
            var ϕ = Math.Atan2(cartesianCoordinates.X, cartesianCoordinates.Z);

            return new SphereCoordinate(r, θ, ϕ);
        }

        public static bool operator ==(SphereCoordinate left, SphereCoordinate right)
        {
            return (CartesianVector)left == (CartesianVector)right;
        }

        public static bool operator !=(SphereCoordinate left, SphereCoordinate right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return ((CartesianVector)this).GetHashCode();
        }
    }
}