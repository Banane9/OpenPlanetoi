using OpenPlanetoi.CoordinateSystems;
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
            for (var i = -5d; i < 5; i = i + 0.25)
            {
                //var test = i * Math.PI;
                ////var halfCircles = test / Math.PI;
                ////var value = halfCircles - Math.Round(halfCircles - 0.5);
                ////value = halfCircles >= 0 ? value : 1 - value;
                //var value = test % (2 * Math.PI);
                //value = test % Math.PI == 0 && value != 0 ? Math.PI : (value < -Math.PI ? value + 2 * Math.PI : (value > Math.PI ? 2 * Math.PI - value : value)) % Math.PI;
                //value = Math.Abs(value);
                //Cons.WriteLine(value + "   :   " + (i * Math.PI));

                //if (i % 1 == 0)
                //    Cons.WriteLine();

                var sphereCoord = new SphereCoordinates(1, i * Math.PI, 0);
                Cons.WriteLine("{0,5}   {1}", i, sphereCoord);
            }

            Cons.ReadLine();
        }
    }
}