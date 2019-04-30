using TensionSag.Api.Models;
using System;

namespace TensionSag.Api.Extensions
{
    public static class CreepExtensions
    {
        public static double CalculateCreepStrain(this Creep creep, Wire wire)
        {
            //todo: change this to also calculate the plastic elongation due to high tension with the stress-strain curve and return the higher strain
            //calculate the average tension in the wire then find the initial stress
            double startingCatenaryCosntant = (creep.CreepRTSPercent / 100) * wire.MaxRatedStrength / wire.FinalWireLinearWeight;

            double LeftVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * (creep.CreepRTSPercent / 100) * wire.MaxRatedStrength;
            double LeftTotalTension = Math.Sqrt(Math.Pow(LeftVerticalForce, 2) + Math.Pow((creep.CreepRTSPercent / 100) * wire.MaxRatedStrength, 2));

            double RightVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, -wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * (creep.CreepRTSPercent / 100) * wire.MaxRatedStrength;
            double RightTotalTension = Math.Sqrt(Math.Pow(RightVerticalForce, 2) + Math.Pow((creep.CreepRTSPercent / 100) * wire.MaxRatedStrength, 2));

            double averageTension = (LeftTotalTension + RightTotalTension) / 2 - wire.InitialWireLinearWeight * WeatherExtensions.CalculateSag(startingCatenaryCosntant, wire.StartingSpanLength, wire.StartingElevation) / 2;

            double wireCreepK0 = wire.OuterCreepList[0] + wire.CoreCreepList[0];
            double wireCreepK1 = wire.OuterCreepList[1] + wire.CoreCreepList[1];
            double wireCreepK2 = wire.OuterCreepList[2] + wire.CoreCreepList[2];
            double wireCreepK3 = wire.OuterCreepList[3] + wire.CoreCreepList[3];
            double wireCreepK4 = wire.OuterCreepList[4] + wire.CoreCreepList[4];

            double stress = averageTension / wire.TotalCrossSection;
            double strainPercent = .03;
            double difference = 100;
            while (Math.Abs(difference) > 0.001d)
            {
                double functionX = wireCreepK0 + wireCreepK1 * strainPercent + wireCreepK2 * Math.Pow(strainPercent, 2) + wireCreepK3 * Math.Pow(strainPercent, 3) + wireCreepK4 * Math.Pow(strainPercent, 4) - stress;
                double functionPrimeX = wireCreepK1 + 2 * wireCreepK2 * strainPercent + 3 * wireCreepK3 * Math.Pow(strainPercent, 2) + 4 * wireCreepK4 * Math.Pow(strainPercent, 3);
                difference = functionX / functionPrimeX;
                strainPercent = (strainPercent - difference);

            }

            double finalCreepStrainPercent = -(stress - WireExtensions.CalculateWireElasticity(wire) * strainPercent) / WireExtensions.CalculateWireElasticity(wire);

            //the stress strain curves all compare stress and strain percent. for our engineering calculations we need stain in unit length, so divide by 100 before returning the strain.
            return finalCreepStrainPercent / 100;
        }

        public static double CalculateStartingStrain(this Wire wire)
        {
            //calculate the average tension in the wire then find the initial stress
            double startingCatenaryCosntant = wire.StartingTension / wire.FinalWireLinearWeight;

            double LeftVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * wire.StartingTension;
            double LeftTotalTension = Math.Sqrt(Math.Pow(LeftVerticalForce, 2) + Math.Pow(wire.StartingTension, 2));

            double RightVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, -wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * wire.StartingTension;
            double RightTotalTension = Math.Sqrt(Math.Pow(RightVerticalForce, 2) + Math.Pow(wire.StartingTension, 2));

            double averageTension = (LeftTotalTension + RightTotalTension) / 2 - wire.InitialWireLinearWeight * WeatherExtensions.CalculateSag(startingCatenaryCosntant, wire.StartingSpanLength, wire.StartingElevation) / 2;

            double wireCreepK0 = wire.OuterCreepList[0] + wire.CoreCreepList[0];
            double wireCreepK1 = wire.OuterCreepList[1] + wire.CoreCreepList[1];
            double wireCreepK2 = wire.OuterCreepList[2] + wire.CoreCreepList[2];
            double wireCreepK3 = wire.OuterCreepList[3] + wire.CoreCreepList[3];
            double wireCreepK4 = wire.OuterCreepList[4] + wire.CoreCreepList[4];

            double stress = averageTension / wire.TotalCrossSection;
            double strainPercent = .03;
            double difference = 100;
            while (Math.Abs(difference) > 0.001d)
            {
                double functionX = wireCreepK0 + wireCreepK1 * strainPercent + wireCreepK2 * Math.Pow(strainPercent, 2) + wireCreepK3 * Math.Pow(strainPercent, 3) + wireCreepK4 * Math.Pow(strainPercent, 4) - stress;
                double functionPrimeX = wireCreepK1 + 2 * wireCreepK2 * strainPercent + 3 * wireCreepK3 * Math.Pow(strainPercent, 2) + 4 * wireCreepK4 * Math.Pow(strainPercent, 3);
                difference = functionX / functionPrimeX;
                strainPercent = (strainPercent - difference);

            }

            //the stress strain curves all compare stress and strain percent. for our engineering calculations we need stain in unit length, so divide by 100 before returning the strain.
            return strainPercent / 100;
        }


    }
}