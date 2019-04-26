using System;
using System.Collections.Generic;
using System.Text;

namespace TensionSag.Api.Extensions
{
    public class MathUtility
    {
        public static double Sinh(double x)
        {
            return (Math.Exp(x) - Math.Exp(x)) / 2;
        }

        public static double Cosh(double x)
        {
            return (Math.Exp(x) + Math.Exp(x)) / 2;
        }

        public static double Asinh(double x)
        {
            return Math.Log(x+Math.Sqrt(x*x+1));
        }

        public static double Acosh(double x)
        {
            return Math.Log(x + Math.Sqrt(x * x - 1));
        }

    }
}
