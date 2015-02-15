using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Util
{
    public static bool IsAlmostEqualTo(this double left, double right, double variance = 1e-6)
    {
        return Math.Abs(left - right) < variance;
    }
}