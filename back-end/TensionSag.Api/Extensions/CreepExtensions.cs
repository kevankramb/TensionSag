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
            double startingCatenaryCosntant = (creep.CreepRTSPercent / 100) * wire.MaxRateStrength / wire.FinalWireLinearWeight;

            double LeftVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * (creep.CreepRTSPercent / 100) * wire.MaxRateStrength;
            double LeftTotalTension = Math.Sqrt(Math.Pow(LeftVerticalForce, 2) + Math.Pow((creep.CreepRTSPercent / 100) * wire.MaxRateStrength, 2));

            double RightVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, -wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * (creep.CreepRTSPercent / 100) * wire.MaxRateStrength;
            double RightTotalTension = Math.Sqrt(Math.Pow(RightVerticalForce, 2) + Math.Pow((creep.CreepRTSPercent / 100) * wire.MaxRateStrength, 2));

            double averageTension = (LeftTotalTension + RightTotalTension) / 2 - wire.InitialWireLinearWeight * WeatherExtensions.CalculateSag(startingCatenaryCosntant, wire.StartingSpanLength, wire.StartingElevation) / 2;

            double wireCreepK0 = wire.OuterCreepK0 + wire.CoreCreepK0;
            double wireCreepK1 = wire.OuterCreepK1 + wire.CoreCreepK1;
            double wireCreepK2 = wire.OuterCreepK2 + wire.CoreCreepK2;
            double wireCreepK3 = wire.OuterCreepK3 + wire.CoreCreepK3;
            double wireCreepK4 = wire.OuterCreepK4 + wire.CoreCreepK4;

            double stress = averageTension / wire.TotalCrossSection;
            double strain = .03;
            double difference = 100;
            while (Math.Abs(difference) > 0.001d)
            {
                double functionX = wireCreepK0 + wireCreepK1 * strain + wireCreepK2 * Math.Pow(strain, 2) + wireCreepK3 * Math.Pow(strain, 3) + wireCreepK4 * Math.Pow(strain, 4) - stress;
                double functionPrimeX = wireCreepK1 + 2 * wireCreepK2 * strain + 3 * wireCreepK3 * Math.Pow(strain, 2) + 4 * wireCreepK4 * Math.Pow(strain, 3);
                difference = functionX / functionPrimeX;
                strain = (strain - difference);

            }

            double finalCreepStrain = -(stress - WireExtensions.CalculateWireElasticity(wire) * strain) / WireExtensions.CalculateWireElasticity(wire);

            return finalCreepStrain/100;
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

            double wireCreepK0 = wire.OuterCreepK0 + wire.CoreCreepK0;
            double wireCreepK1 = wire.OuterCreepK1 + wire.CoreCreepK1;
            double wireCreepK2 = wire.OuterCreepK2 + wire.CoreCreepK2;
            double wireCreepK3 = wire.OuterCreepK3 + wire.CoreCreepK3;
            double wireCreepK4 = wire.OuterCreepK4 + wire.CoreCreepK4;

            double stress = averageTension / wire.TotalCrossSection;
            double strain = .03;
            double difference = 100;
            while (Math.Abs(difference) > 0.001d)
            {
                double functionX = wireCreepK0 + wireCreepK1 * strain + wireCreepK2 * Math.Pow(strain, 2) + wireCreepK3 * Math.Pow(strain, 3) + wireCreepK4 * Math.Pow(strain, 4) - stress;
                double functionPrimeX = wireCreepK1 + 2 * wireCreepK2 * strain + 3 * wireCreepK3 * Math.Pow(strain, 2) + 4 * wireCreepK4 * Math.Pow(strain, 3);
                difference = functionX / functionPrimeX;
                strain = (strain - difference);

            }

            return strain/100;
        }


    }
}