using TensionSag.Api.Models;
using System;

namespace TensionSag.Api.Extensions
{
    public static class WeatherExtensions
    {
        private static readonly double TheConstant = 0.0;

        public static double CalculateTension(this Weather weather, Wire wire)
        {
            return 0.0;
        }

        public static double CalculateSag(this Weather weather, Wire wire)
        {
            return 0.0;
        }
    
        public static double CalculateXc(double spanLength, double spanElevation, double catenaryConstant)
        {
            double tempZVar = spanElevation * (Math.Pow(Math.Exp(spanLength / catenaryConstant), 0.5d)) / (catenaryConstant * (1 - Math.Exp(spanLength / catenaryConstant)));
            return spanLength / 2.0d + catenaryConstant * Math.Log(tempZVar + Math.Pow(1 + Math.Pow(tempZVar, 2), 0.5d));
        }
    }
}