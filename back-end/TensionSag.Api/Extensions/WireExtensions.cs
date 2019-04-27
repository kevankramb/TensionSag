using TensionSag.Api.Models;
using System;

namespace TensionSag.Api.Extensions
{
    public static class WireExtensions
    {
        public static double CalculateWireElasticity(this Wire wire)
        {
            return wire.OuterElasticity + wire.CoreElasticity;
        }
        public static double CalculateWireThermalCoefficient(this Wire wire)
        {
            return wire.OuterThermalCoefficient * wire.OuterElasticity / (wire.OuterElasticity + wire.CoreElasticity) + wire.CoreThermalCoefficient * wire.CoreElasticity / (wire.OuterElasticity + wire.CoreElasticity);
        }

        public static double CalculateOriginalLength(this Wire wire, Creep creep)
        {
            if (wire.StartingTensionType == true)
            {
                return CalculateOriginalLengthFromInitialTension(wire);
            }
            else
            {
                return CalculateOriginalLengthFromFinalTension(wire, creep);
            }
        }

        public static double CalculateOriginalLengthFromInitialTension(this Wire wire)
        {

            //calculate the average tension in the wire then find the initial stress
            double startingCatenaryCosntant = wire.StartingTension / wire.InitialWireLinearWeight;

            double LeftVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * wire.StartingTension;
            double LeftTotalTension = Math.Sqrt(Math.Pow(LeftVerticalForce, 2) + Math.Pow(wire.StartingTension, 2));

            double RightVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, -wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * wire.StartingTension;
            double RightTotalTension = Math.Sqrt(Math.Pow(RightVerticalForce, 2) + Math.Pow(wire.StartingTension, 2));

            double averageTension = (LeftTotalTension + RightTotalTension) / 2 - wire.InitialWireLinearWeight * WeatherExtensions.CalculateSag(startingCatenaryCosntant, wire.StartingSpanLength, wire.StartingElevation) / 2;

            double initialStress = averageTension / wire.TotalCrossSection;

            double initialStrain = CalculateStringingStrain(wire, initialStress);

            double initialArcLength = WeatherExtensions.CalculateArcLength(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant);

            return initialArcLength - initialArcLength * initialStrain;
        }

        public static double CalculateStringingStrain(this Wire wire, double stress)
        {
            double strainPercent = 0.1;

            // these also get defined in the weather extension when calculating initial tensions, refactor this to only happen in one place
            double wireStressStrainK0 = wire.OuterStressStrainList[0] + wire.CoreStressStrainList[0];
            double wireStressStrainK1 = wire.OuterStressStrainList[1] + wire.CoreStressStrainList[1];
            double wireStressStrainK2 = wire.OuterStressStrainList[2] + wire.CoreStressStrainList[2];
            double wireStressStrainK3 = wire.OuterStressStrainList[3] + wire.CoreStressStrainList[3];
            double wireStressStrainK4 = wire.OuterStressStrainList[4] + wire.CoreStressStrainList[4];

            double difference = 100;
            while (Math.Abs(difference) > 0.001d)
            {
                double functionX = wireStressStrainK0 + wireStressStrainK1 * strainPercent + wireStressStrainK2 * Math.Pow(strainPercent, 2) + wireStressStrainK3 * Math.Pow(strainPercent, 3) + wireStressStrainK4 * Math.Pow(strainPercent, 4) - stress;
                double functionPrimeX = wireStressStrainK1 + 2 * wireStressStrainK2 * strainPercent + 3 * wireStressStrainK3 * Math.Pow(strainPercent, 2) + 4 * wireStressStrainK4 * Math.Pow(strainPercent, 3);
                difference = functionX / functionPrimeX;
                strainPercent = (strainPercent - difference);
            }

            return strainPercent / 100;

        }

        public static double CalculateOriginalLengthFromFinalTension(this Wire wire, Creep creep)
        {
            double startingCatenaryCosntant = wire.StartingTension / wire.FinalWireLinearWeight;
            double startingArcLength = WeatherExtensions.CalculateArcLength(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant);

            double stressFreeLength = startingArcLength - wire.StartingTension * startingArcLength / (wire.TotalCrossSection * WireExtensions.CalculateWireElasticity(wire));
            double creepStrain = CreepExtensions.CalculateCreepStrain(creep, wire);
            return (stressFreeLength - stressFreeLength * creepStrain);

        }
    }
}