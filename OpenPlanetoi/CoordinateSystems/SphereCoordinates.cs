﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenPlanetoi.CoordinateSystems
{
    /// <summary>
    /// Represents a point on a Sphere centered at the Cartesian Point (0/0/0).
    /// </summary>
    public struct SphereCoordinates
    {
        /// <summary>
        /// The azimuthal angle (rotation away from pointing straight to the front).
        /// </summary>
        public readonly double ϕ;

        /// <summary>
        /// The radius of the Sphere (Euclidian Distance in Carteesian Space).
        /// </summary>
        public readonly double R;

        /// <summary>
        /// The polar angle (rotation away from pointing straight up).
        /// </summary>
        public readonly double θ;

        /// <summary>
        /// Creates a new instance of the <see cref="SphereCoordinates"/> struct with the given positions.
        /// </summary>
        /// <param name="r">The radius of the Sphere (Euclidian Distance in Carteesian Space).</param>
        /// <param name="θ">The polar angle (rotation away from pointing straight up).</param>
        /// <param name="ϕ">The azimuthal angle (rotation away from pointing straight to the front).</param>
        public SphereCoordinates(double r, double θ, double ϕ)
        {
            R = Math.Abs(r);

            this.θ = θ;
            this.ϕ = ϕ;
        }

        public override string ToString()
        {
            return "Sphere: " + R + "/" + θ + "/" + ϕ;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SphereCoordinates))
                return false;

            var coordObj = (SphereCoordinates)obj;

            return coordObj == this;
        }

        public static implicit operator SphereCoordinates(CartesianCoordinates cartesianCoordinates)
        {
            // http://en.wikipedia.org/wiki/Spherical_coordinate_system#Cartesian_coordinates (different xyz because of different directions)

            var r = Math.Sqrt(Math.Pow(cartesianCoordinates.X, 2) + Math.Pow(cartesianCoordinates.Y, 2) + Math.Pow(cartesianCoordinates.Z, 2));
            var θ = Math.Acos(cartesianCoordinates.Y / r);
            var ϕ = Math.Atan(cartesianCoordinates.X / cartesianCoordinates.Z);

            return new SphereCoordinates(r, θ, ϕ);
        }

        public static bool operator ==(SphereCoordinates left, SphereCoordinates right)
        {
            return left.R == right.R && left.θ == right.θ && left.ϕ == right.ϕ;
        }

        public static bool operator !=(SphereCoordinates left, SphereCoordinates right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return R.GetHashCode() * 13 + θ.GetHashCode() * 13 + ϕ.GetHashCode();
            }
        }
    }
}