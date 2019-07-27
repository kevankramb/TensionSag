using TensionSag.Api.Models;
using System;

namespace TensionSag.Api.Extensions
{
    public static class CreepExtensions
    {
        //this is the creep strain calculations. these are sort of wire properties, but are depenent on the tensioning conditions of the wire, and not depenent on the weather loading conditions, so the are in their own class.
        public static double CalculateCreepStrain(this Creep creep, Wire wire)
        {
            //todo: change this to also calculate the plastic elongation due to high tension with the stress-strain curve and return the higher strain
            //calculate the average tension in the wire then find the initial stress
            //this also needs to be changed to hand small or zero values for creepRTS percents. currently it returns NAN when modeling zero creep while it should just return 0 strain.
            double startingCatenaryCosntant = (creep.CreepRTSPercent / 100) * wire.MaxRatedStrength / wire.FinalWireLinearWeight;

            double LeftVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * (creep.CreepRTSPercent / 100) * wire.MaxRatedStrength;
            double LeftTotalTension = Math.Sqrt(Math.Pow(LeftVerticalForce, 2) + Math.Pow((creep.CreepRTSPercent / 100) * wire.MaxRatedStrength, 2));

            double RightVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, -wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * (creep.CreepRTSPercent / 100) * wire.MaxRatedStrength;
            double RightTotalTension = Math.Sqrt(Math.Pow(RightVerticalForce, 2) + Math.Pow((creep.CreepRTSPercent / 100) * wire.MaxRatedStrength, 2));

            double averageTension = (LeftTotalTension + RightTotalTension) / 2 - wire.InitialWireLinearWeight * WeatherExtensions.CalculateSag(startingCatenaryCosntant, wire.StartingSpanLength, wire.StartingElevation) / 2;

            //refactor these to be calculated and stored in one place
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
            //previous 'wrong' calculation: -(stress - WireExtensions.CalculateWireElasticity(wire) * strainPercent) / WireExtensions.CalculateWireElasticity(wire)
            //the old calculation was the first back of the envolope equation I derived. Its reasonably accurate but the actual equation is easier to justify/derive and more accurate
            double finalCreepStrainPercent = (strainPercent+1)/(1+ averageTension/(WireExtensions.CalculateWireElasticity(wire)* wire.TotalCrossSection))-1;

            //the stress strain curves all compare stress and strain percent. for our engineering calculations we need stain in unit length, so divide by 100 before returning the strain.
            return finalCreepStrainPercent / 100;
        }

        public static double CalculateStartingStrain(this Wire wire)
        {
            //this whole calculation is deprecated for now since there are ways to avoid the starting strain calculation when the input tension is final
            //havent decided if i want to keep this for future stand alone reporting capabilities.

            //still on the fence if the average tension calculations are needed. a potentual edge case worth testing is very low tension wires where the horizontal tension does not dominate the average tension in the wire
            //calculate the average tension in the wire then find the initial stress
            double startingCatenaryCosntant = wire.StartingTension / wire.FinalWireLinearWeight;

            double LeftVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * wire.StartingTension;
            double LeftTotalTension = Math.Sqrt(Math.Pow(LeftVerticalForce, 2) + Math.Pow(wire.StartingTension, 2));

            double RightVerticalForce = -MathUtility.Sinh(WeatherExtensions.CalculateXc(wire.StartingSpanLength, -wire.StartingElevation, startingCatenaryCosntant) / startingCatenaryCosntant) * wire.StartingTension;
            double RightTotalTension = Math.Sqrt(Math.Pow(RightVerticalForce, 2) + Math.Pow(wire.StartingTension, 2));

            double averageTension = (LeftTotalTension + RightTotalTension) / 2 - wire.InitialWireLinearWeight * WeatherExtensions.CalculateSag(startingCatenaryCosntant, wire.StartingSpanLength, wire.StartingElevation) / 2;

            //refactor these to be calculated and stored in one place
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