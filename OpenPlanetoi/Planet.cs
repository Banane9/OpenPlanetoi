using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPlanetoi
{
    public sealed class Planet
    {
        private readonly Random random;

        public int Seed { get; private set; }

        public SphericalVoronoi Tiling { get; private set; }

        public Planet(int seed)
        {
            Seed = seed;
            random = new Random(seed);

            Tiling = new SphericalVoronoi();
        }
    }
}