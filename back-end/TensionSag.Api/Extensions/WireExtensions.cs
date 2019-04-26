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
            return wire.OuterThermalCoefficient*wire.OuterElasticity/(wire.OuterElasticity + wire.CoreElasticity) + wire.CoreThermalCoefficient*wire.CoreElasticity/(wire.OuterElasticity + wire.CoreElasticity);
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
            double LeftTotalTension = Math.Sqrt(Math.Pow(LeftVerticalForce, 2) + Math.Pow(wire.StartingTension,2));

            double RightVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, -wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * wire.StartingTension;
            double RightTotalTension = Math.Sqrt(Math.Pow(RightVerticalForce, 2) + Math.Pow(wire.StartingTension,2));

            double averageTension = (LeftTotalTension + RightTotalTension) / 2 - wire.InitialWireLinearWeight * WeatherExtensions.CalculateSag(startingCatenaryCosntant, wire.StartingSpanLength, wire.StartingElevation)/2;

            double initialStress = averageTension / wire.TotalCrossSection;
            
            double initialStrain = CalculateStringingStrain(wire, initialStress);

            double initialArcLength = WeatherExtensions.CalculateArcLength(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant);

            return initialArcLength-initialArcLength*initialStrain;
        }

        public static double CalculateStringingStrain(this Wire wire, double stress)
        {
            double strain = 0.001;

            // these also get defined in the weather extension when calculating initial tensions, refactor this to only happen in one place
            double wireStressStrainK0 = wire.OuterStressStrainK0 + wire.CoreStressStrainK0;
            double wireStressStrainK1 = wire.OuterStressStrainK1 + wire.CoreStressStrainK1;
            double wireStressStrainK2 = wire.OuterStressStrainK2 + wire.CoreStressStrainK2;
            double wireStressStrainK3 = wire.OuterStressStrainK3 + wire.CoreStressStrainK3;
            double wireStressStrainK4 = wire.OuterStressStrainK4 + wire.CoreStressStrainK4;

            double difference = 100;
            while (Math.Abs(difference) > 0.001d)
            {
                double functionX = wireStressStrainK0 + wireStressStrainK1 * strain + wireStressStrainK2 * Math.Pow(strain, 2) + wireStressStrainK3 * Math.Pow(strain, 3) + wireStressStrainK4 * Math.Pow(strain, 4) - stress;
                double functionPrimeX = wireStressStrainK1 + 2 * wireStressStrainK2 * strain + 3 * wireStressStrainK3 * Math.Pow(strain, 2) + 4 * wireStressStrainK4 * Math.Pow(strain, 3);
                difference = functionX/functionPrimeX;
                strain = (strain - difference);

            }

            return strain/100;

        }

        public static double CalculateOriginalLengthFromFinalTension(this Wire wire, Creep creep)
        {
            double startingCatenaryCosntant = wire.StartingTension / wire.FinalWireLinearWeight;
            double startingArcLength = WeatherExtensions.CalculateArcLength(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant);

            double stressFreeLength = startingArcLength - wire.StartingTension * startingArcLength / (wire.TotalCrossSection * WireExtensions.CalculateWireElasticity(wire));
            double creepStrain = CreepExtensions.CalculateCreepStrain(creep, wire);
            return (stressFreeLength - stressFreeLength*creepStrain)/100;

        }



    }
}