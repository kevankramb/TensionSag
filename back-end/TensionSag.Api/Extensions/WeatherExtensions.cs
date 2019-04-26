using TensionSag.Api.Models;
using System;

namespace TensionSag.Api.Extensions
{
    public static class WeatherExtensions
    {
        private static readonly double IceDensity = 916.8;
        private static readonly double Gravity = 9.80665;

        //this calculates the final elastic tension
        //currently does not account for plastic elongation that is not already present at the reference tension
        public static double CalculateElasticTension(this Weather weather, Wire wire, Creep creep)
        {
            double orginalLength = WireExtensions.CalculateOriginalLength(wire, creep);
            double StartingWireLength = orginalLength + orginalLength*CreepExtensions.CalculateCreepStrain(creep, wire);
            double psi = StartingWireLength + WireExtensions.CalculateWireThermalCoefficient(wire) * StartingWireLength * (weather.Temperature - wire.StartingTemp) - StartingWireLength;
            double beta = StartingWireLength / (WireExtensions.CalculateWireElasticity(wire) * wire.TotalCrossSection);

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

        //this calculates the 'initial' tension from the stress strain curve.
        //vaguely this calculation goes like this: 1)estimate length at design case 2) calculate strain for that length 3) calculate stress for that strain 4) calculate the average tension for that stress
        //5) find the horizontal tension that results in that average tension 6) calculate the wire length for that horizontal tension from the wire geometry and return to step 2) with the new wire length estimate
        public static double CalculateInitialTensions(this Weather weather, Wire wire, Creep creep)
        {
            
            double originalLength = WireExtensions.CalculateOriginalLength(wire, creep);
            double originalLengthDesignTemp = originalLength + WireExtensions.CalculateWireThermalCoefficient(wire) * originalLength * (weather.Temperature - wire.StartingTemp);

            //this estimate may need to be improved by actually calculating the elastic arc length at weather condition
            double lengthEstimate = originalLengthDesignTemp + originalLengthDesignTemp*0.003;

            //refactor this so wireStressStrains are not calculated both here and in the wireExtensions
            double wireStressStrainK0 = wire.OuterStressStrainK0 + wire.CoreStressStrainK0;
            double wireStressStrainK1 = wire.OuterStressStrainK1 + wire.CoreStressStrainK1;
            double wireStressStrainK2 = wire.OuterStressStrainK2 + wire.CoreStressStrainK2;
            double wireStressStrainK3 = wire.OuterStressStrainK3 + wire.CoreStressStrainK3;
            double wireStressStrainK4 = wire.OuterStressStrainK4 + wire.CoreStressStrainK4;

            double lengthDifference = 100;
            while (Math.Abs(lengthDifference) > 0.0001d )
            {
                double strain = (lengthEstimate - originalLengthDesignTemp) / originalLengthDesignTemp;
                double stress = wireStressStrainK0 + wireStressStrainK1 * strain + wireStressStrainK2 * Math.Pow(strain, 2) + wireStressStrainK3 * Math.Pow(strain, 3) + wireStressStrainK4 * Math.Pow(strain, 4);
                double wireAverageTension = stress * wire.TotalCrossSection;

                double TensionDiff = 1000;
                double horizontalTension = wireAverageTension;
                while (Math.Abs(TensionDiff) > 0.001d)
                {
                    double CatenaryConstantEstimate = horizontalTension / CalculateFinalLinearForce(weather, wire);
                    //refactor this average tension calculation to be a single function that is used here and in the wireExtension
                    double LeftVerticalForce = -MathUtility.Sinh(CalculateXc(weather.FinalSpanLength, weather.FinalElevation, CatenaryConstantEstimate) / CatenaryConstantEstimate) * horizontalTension;
                    double LeftTotalTension = Math.Sqrt(Math.Pow(LeftVerticalForce, 2) + Math.Pow(horizontalTension, 2));

                    double RightVerticalForce = -MathUtility.Sinh(CalculateXc(weather.FinalSpanLength, -weather.FinalElevation, CatenaryConstantEstimate) / CatenaryConstantEstimate) * horizontalTension;
                    double RightTotalTension = Math.Sqrt(Math.Pow(RightVerticalForce, 2) + Math.Pow(horizontalTension, 2));

                    double averageTension = (LeftTotalTension + RightTotalTension) / 2 - CalculateFinalLinearForce(weather, wire) * CalculateSag(CatenaryConstantEstimate, weather.FinalSpanLength, weather.FinalElevation) / 2;

                    //this may not work, for large spans or low tension wires, horizontal tension and average tension maybe inversely related.
                    TensionDiff = averageTension - wireAverageTension;
                    horizontalTension = horizontalTension - TensionDiff;

                }

                double wireLength = CalculateArcLength(weather.FinalSpanLength, weather.FinalElevation, horizontalTension / CalculateFinalLinearForce(weather, wire));

                lengthDifference = wireLength - lengthEstimate;

                lengthEstimate = wireLength;

            }

            double stressStrainTension = horizontalTension;

            return stressStrainTension;
        }

        //calculates sag for any wire geometry
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
            return -catenaryConstant * (MathUtility.Cosh(-Xc / catenaryConstant) - 1);
        }

        public static double CalculateXd(double Xc, double catenaryConstant, double spanElevation, double spanLength)
        {
            return Xc + catenaryConstant * MathUtility.Asinh(spanElevation / spanLength);
        }

        public static double CalculateXc(double spanLength, double spanElevation, double catenaryConstant)
        {
            double tempZVar = spanElevation * (Math.Pow(Math.Exp(spanLength / catenaryConstant), 0.5d)) / (catenaryConstant * (1 - Math.Exp(spanLength / catenaryConstant)));
            return spanLength / 2.0d + catenaryConstant * Math.Log(tempZVar + Math.Pow(1 + Math.Pow(tempZVar, 2), 0.5d));
        }

        //calculates the total hanging wire length between attachment points
        public static double CalculateArcLength(double spanLength, double spanElevation, double catenaryConstant)
        {
            double Xc = CalculateXc(spanLength, spanElevation, catenaryConstant);
            return catenaryConstant * (MathUtility.Sinh((spanLength - Xc) / catenaryConstant) + MathUtility.Sinh(Xc / catenaryConstant));
        }

        //calculates the final weather loaded linear weight of the wire and bundle. does not account for NESC linear constant yet
        public static double CalculateFinalLinearForce(this Weather weather, Wire wire)
        {
            double WindLinearForce = wire.FinalWireDiameter * weather.WindPressure;
            double WeightLinearForce = -((Math.PI * Math.Pow(wire.FinalWireDiameter / 2 + weather.IceRadius, 2d) - (Math.PI * Math.Pow(wire.FinalWireDiameter / 2, 2d))) * IceDensity * Gravity + wire.FinalWireLinearWeight);

            return Math.Sqrt(Math.Pow(WindLinearForce, 2d) + Math.Pow(WeightLinearForce, 2d));
        }

        //newton raphson method junk for the elastic tension calculation. this basically follows the numerical tension method but very accurately accounts for changes in elevation
        public static double SolveForDifference(double horizontalTension, double finalSpanLength, double linearForce, double finalSpanElevation, double psi, double beta)
        {

            double iota = Math.Exp(finalSpanLength * linearForce / horizontalTension);
            double kappa = Math.Pow(iota, 1.5d) * finalSpanElevation * finalSpanLength * Math.Pow(linearForce, 2d) / (Math.Pow(1d - iota, 2d) * Math.Pow(horizontalTension, 3d));
            double eta = Math.Sqrt(iota) * finalSpanElevation * finalSpanLength * Math.Pow(linearForce, 2d) / (2d * (1d - iota) * Math.Pow(horizontalTension, 3d));
            double mu = Math.Sqrt(iota) * finalSpanElevation * linearForce / ((1d - iota) * Math.Pow(horizontalTension, 2d));
            double nu = linearForce * (Math.Sqrt(1d + (iota * Math.Pow(finalSpanElevation, 2d) * Math.Pow(linearForce, 2d)) / (Math.Pow(1d - iota, 2d) * Math.Pow(horizontalTension, 2d))));
            double xi = MathUtility.Asinh((Math.Sqrt(iota) * finalSpanElevation * linearForce) / ((1d - iota) * horizontalTension)) / linearForce;
            double omikron = linearForce * (-1d * (-kappa - eta - mu) * horizontalTension / nu - xi) / horizontalTension;
            double chi = linearForce * ((-kappa - eta - mu) * horizontalTension / nu + xi) / horizontalTension;
            double arcLength = CalculateArcLength(finalSpanLength, finalSpanElevation, (horizontalTension / linearForce));

            double tau = (horizontalTension / linearForce) * (omikron - (linearForce / Math.Pow(horizontalTension, 2d)) *
                (finalSpanLength / 2d - horizontalTension * xi)) * MathUtility.Cosh((linearForce *
                (finalSpanLength / 2d - horizontalTension * xi)) / horizontalTension) +
                MathUtility.Sinh((linearForce * (finalSpanLength / 2d - horizontalTension * xi)) / horizontalTension) / linearForce;

            double upsilon = (horizontalTension / linearForce) * (chi - (linearForce / Math.Pow(horizontalTension, 2d)) *
                (finalSpanLength / 2d + horizontalTension * xi)) * MathUtility.Cosh((linearForce *
                (finalSpanLength / 2d + horizontalTension * xi)) / horizontalTension) +
                MathUtility.Sinh((linearForce * (finalSpanLength / 2d + horizontalTension * xi)) / horizontalTension) / linearForce;

            return (psi + horizontalTension * beta - arcLength) / (beta - (tau + upsilon));

        }

    }
}