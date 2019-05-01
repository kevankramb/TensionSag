using TensionSag.Api.Models;
using System.Collections.Generic;

namespace TensionSag.Tests
{
    public class WireFactory
    {
        public WireFactory() { }

        public Wire Create(int wireType)
        {
            if (wireType == 795)
            {
                var name = "Tension Wire Test Drake 795 ACSR";
                var totalCrossSection = 0.00046844;
                var initialWireDiameter = 0.0281432;
                var finalWireDiameter = 0.0281432;
                var initialWireLinearWeight = 15.9657;
                var finalWireLinearWeight = 15.9657;
                var maxRatedStrength = 68000;
                var outerElasticity = 44126400000;
                var outerThermalCoefficient = 0.00002304;
                var coreElasticity = 25510600000;
                var coreThermalCoefficient = 0.00001152;
                var outerStressStrainList = new List<double> { -8363341, 305493595.6, -96556939.03, -2.59367e8, 2.11504e8 };
                var outerCreepList = new List<double> { -3756263.8, 147732585.6, -129912395.9, 3.7887e7, 0 };
                var coreStressStrainList = new List<double> { -477806.7, 2.66338e8, 27565929.1, -3.1518e8, 1.92309e8 };
                var coreCreepList = new List<double> { 324743.1, 249668124.8, 84125691.63, -4.99125e8, 3.19489e8 };
                var startingTension = 22500.0;
                var startingTemp = 15;
                var startingSpanLength = 50;
                var startingElevation = 0.0;
                var startingTensionType = true;
                var material = WireMaterial.ACSR;
                var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                    outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
                    outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                    startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
                return wire;
            }

            if (wireType == 556)
            {
                var name = "Tension Wire Test 556 AAC";
                var totalCrossSection = 0.0002818704;
                var initialWireDiameter = 0.0217424;
                var finalWireDiameter = 0.0217424;
                var initialWireLinearWeight = 7.60926;
                var finalWireLinearWeight = 7.60926;
                var maxRatedStrength = 43370;
                var outerElasticity = 61605437000;
                var outerThermalCoefficient = 0.00002304;
                var coreElasticity = 0;
                var coreThermalCoefficient = 0;
                var outerStressStrainList = new List<double> { 0, 483543118, -3.58196e+8, -2.29416e+8, 2.30271e+8 };
                var outerCreepList = new List<double> { 0, 202416285, -14775465, -144390007, 1.63682e+8 };
                var coreStressStrainList = new List<double> { 0, 0, 0, 0, 0 };
                var coreCreepList = new List<double> { 0, 0, 0, 0, 0 };
                var startingTension = 3000.0;
                var startingTemp = 10.0;
                var startingSpanLength = 50;
                var startingElevation = 0.0;
                var startingTensionType = true;
                var material = WireMaterial.ACSR;
                var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                    outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
                    outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                    startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
                return wire;
            }

            else
            {
                var name = "General Wire Data";
                var totalCrossSection = 0.00046844;
                var initialWireDiameter = 0.0281432;
                var finalWireDiameter = 0.0281432;
                var initialWireLinearWeight = 15.9657;
                var finalWireLinearWeight = 15.9657;
                var maxRatedStrength = 68000;
                var outerElasticity = 44126400000;
                var outerThermalCoefficient = 0.00002304;
                var coreElasticity = 25510600000;
                var coreThermalCoefficient = 0.00001152;
                var outerStressStrainList = new List<double> { -8363341, 305493595.6, -96556939.03, -2.59367e8, 2.11504e8 };
                var outerCreepList = new List<double> { -3756263.8, 147732585.6, -129912395.9, 3.7887e7, 0 };
                var coreStressStrainList = new List<double> { -477806.7, 2.66338e8, 27565929.1, -3.1518e8, 1.92309e8 };
                var coreCreepList = new List<double> { 324743.1, 249668124.8, 84125691.63, -4.99125e8, 3.19489e8 };
                var startingTension = 22500.0;
                var startingTemp = 15;
                var startingSpanLength = 50;
                var startingElevation = 0.0;
                var startingTensionType = true;
                var material = WireMaterial.ACSR;
                var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                    outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
                    outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                    startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
                return wire;
            }
        }
    }
}