using TensionSag.Api.Models;
using System;

namespace TensionSag.Api.Extensions
{
    public static class WeatherExtensions
    {
        private static readonly double TheConstant = 0.0;

        public static double CalculateElasticTension(this Weather weather, Wire wire)
        {
            double StartingArcLength = CalculateArcLength(wire.StartingSpanLength, wire.StartingElevation, wire.StartingTension / wire.InitialWireLinearWeight);
            double psi = StartingArcLength + wire.ThermalCoefficient * StartingArcLength * (weather.Temperature - wire.StartingTemp) - StartingArcLength * wire.StartingTension / (wire.Elasticity * wire.TotalCrossSection);
            double beta = StartingArcLength / (wire.Elasticity * wire.TotalCrossSection);

            double lengthEstimate = Math.Sqrt(Math.Pow(weather.FinalSpanLength, 2) + Math.Pow(weather.FinalElevation, 2));
            double horizontalTension = CalculateFinalLinearForce(weather, wire) * (lengthEstimate / 2) * Math.Sqrt(lengthEstimate / (6 * lengthEstimate));

            double difference = 100;
            while (Math.Abs(difference) > 0.001d )
            {
                difference = SolveForDifference(horizontalTension, wire.StartingSpanLength, CalculateFinalLinearForce(weather, wire), wire.StartingElevation, psi, beta);
                horizontalTension = (horizontalTension - difference);

            }

            return horizontalTension;
        }

        public static double CalculateSag(double catenaryConstant, double spanLength, double spanElevation)
        {
            double XcForSag = CalculateXc(spanLength, spanElevation, catenaryConstant);
            double YcForSag = CalculateYc(catenaryConstant, XcForSag);
            double distanceToSagPoint = CalculateXd(XcForSag, catenaryConstant, spanElevation, spanLength);

            double tempSagXc = (spanElevation / spanLength) * distanceToSagPoint;
            double tempSagYc = YcForSag + catenaryConstant * (Math.Sqrt(Math.Pow(spanLength, 2) + Math.Pow(spanElevation, 2)) - spanLength) / spanLength;

            double sag = tempSagXc - tempSagYc;

            return sag;
        }

        public static double CalculateYc(double catenaryConstant, double Xc)
        {
            return -catenaryConstant * (cosh(-Xc / catenaryConstant) - 1);
        }

        public static double CalculateXd(double Xc, double catenaryConstant, double spanElevation, double spanLength)
        {
            return Xc + catenaryConstant * MathUtility.asinh(spanElevation / spanLength);
        }

        public static double CalculateXc(double spanLength, double spanElevation, double catenaryConstant)
        {
            double tempZVar = spanElevation * (Math.Pow(Math.Exp(spanLength / catenaryConstant), 0.5d)) / (catenaryConstant * (1 - Math.Exp(spanLength / catenaryConstant)));
            return spanLength / 2.0d + catenaryConstant * Math.Log(tempZVar + Math.Pow(1 + Math.Pow(tempZVar, 2), 0.5d));
        }

        public static double CalculateArcLength(double spanLength, double spanElevation, double catenaryConstant)
        {
            double Xc = CalculateXc(spanLength, spanElevation, catenaryConstant);
            return catenaryConstant * (sinh((spanLength - Xc) / catenaryConstant) + sinh(Xc / catenaryConstant));
        }

        public static double CalculateFinalLinearForce(this Weather weather, Wire wire)
        {
            double WindLinearForce = wire.FinalWireDiameter * weather.WindPressure;
            double WeightLinearForce = -((Math.PI * Math.Pow(wire.FinalWireDiameter / 2 + weather.IceRadius, 2d) - (Math.PI * Math.Pow(wire.FinalWireDiameter / 2, 2d))) * IceDensity * Gravity + wire.FinalWireLinearWeight);

            return Math.Sqrt(Math.Pow(WindLinearForce, 2d) + Math.Pow(WeightLinearForce, 2d));
        }

        public static double SolveForDifference(double horizontalTension, double finalSpanLength, double linearForce, double finalSpanElevation, double psi, double beta)
        {

            double iota = Math.Exp(finalSpanLength * linearForce / horizontalTension);
            double kappa = Math.Pow(iota, 1.5d) * finalSpanElevation * finalSpanLength * Math.Pow(linearForce, 2d) / (Math.Pow(1d - iota, 2d) * Math.Pow(horizontalTension, 3d));
            double eta = Math.Sqrt(iota) * finalSpanElevation * finalSpanLength * Math.Pow(linearForce, 2d) / (2d * (1d - iota) * Math.Pow(horizontalTension, 3d));
            double mu = Math.Sqrt(iota) * finalSpanElevation * linearForce / ((1d - iota) * Math.Pow(horizontalTension, 2d));
            double nu = linearForce * (Math.Sqrt(1d + (iota * Math.Pow(finalSpanElevation, 2d) * Math.Pow(linearForce, 2d)) / (Math.Pow(1d - iota, 2d) * Math.Pow(horizontalTension, 2d))));
            double xi = MathUtility.asinh((Math.Sqrt(iota) * finalSpanElevation * linearForce) / ((1d - iota) * horizontalTension)) / linearForce;
            double omikron = linearForce * (-1d * (-kappa - eta - mu) * horizontalTension / nu - xi) / horizontalTension;
            double chi = linearForce * ((-kappa - eta - mu) * horizontalTension / nu + xi) / horizontalTension;
            double arcLength = CalculateArcLength(finalSpanLength, finalSpanElevation, (horizontalTension / linearForce));

            double tau = (horizontalTension / linearForce) * (omikron - (linearForce / Math.Pow(horizontalTension, 2d)) *
                (finalSpanLength / 2d - horizontalTension * xi)) * cosh((linearForce *
                (finalSpanLength / 2d - horizontalTension * xi)) / horizontalTension) +
                sinh((linearForce * (finalSpanLength / 2d - horizontalTension * xi)) / horizontalTension) / linearForce;

            double upsilon = (horizontalTension / linearForce) * (chi - (linearForce / Math.Pow(horizontalTension, 2d)) *
                (finalSpanLength / 2d + horizontalTension * xi)) * cosh((linearForce *
                (finalSpanLength / 2d + horizontalTension * xi)) / horizontalTension) +
                sinh((linearForce * (finalSpanLength / 2d + horizontalTension * xi)) / horizontalTension) / linearForce;

        //this is the actual newton-raphson equation used to solve for our final tension, eq 90 in the documentation
            return (psi + horizontalTension * beta - arcLength) / (beta - (tau + upsilon));

        }

    }
}