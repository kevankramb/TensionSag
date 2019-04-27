using System;
using TensionSag.Api.Models;
using TensionSag.Api.Extensions;
using Xunit;
using System.Collections.Generic;

namespace TensionSag.Tests
{
    public class WeatherTests
    {
        [Fact]
        public void CalculateInitialTension_Success()
        {
            // Setup
            var expectedResult = 0.0;
            var name = "Tension Wire Test Drake 795";
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
            var startingTensionType = false;
            var material = WireMaterial.ACSR;
            var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
                outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
            var temperature = 15.0;
            var iceRadius = 0.0;
            var windPressure = 0.0;
            var finalSpanLength = startingSpanLength;
            var finalElevation = startingElevation;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);
            var creepRTSPercent = 30;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var tension = WeatherExtensions.CalculateInitialTensions(weather, wire, creep);

            // Assert
            Assert.Equal(expectedResult, tension);
        }

        [Fact]
        public void CalculateElasticTension_Success()
        {
            // Setup
            var expectedResult = 0.0;
            var name = "Tension Wire Test Drake 795";
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
            var startingTensionType = false;
            var material = WireMaterial.ACSR;
            var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
                outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
            var temperature = 15.0;
            var iceRadius = 0.0;
            var windPressure = 0.0;
            var finalSpanLength = startingSpanLength;
            var finalElevation = startingElevation;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);
            var creepRTSPercent = 10;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var tension = WeatherExtensions.CalculateElasticTension(weather, wire, creep);

            // Assert
            Assert.Equal(expectedResult, tension);
        }

        //[Fact]
        //public void CalculateSag_Success()
        //{
        //    // Setup
        //    var expectedResult = 0.0;
        //    var name = "Sag Wire Test";
        //    var totalCrossSection = 15.0;
        //    var totalWireLinearWeight = 15.0;
        //    var elasticity = 1.0;
        //    var thermalCoefficient = 4.0;
        //    var startingTension = 500.0;
        //    var startingTemp = 15;
        //    var startingSpanLength = 25;
        //    var startingElevation = 0.0;
        //    var material = WireMaterial.ACSR;
        //    var wire = Wire.Create(name, totalCrossSection, totalWireLinearWeight, elasticity, thermalCoefficient, startingTension, startingTemp, startingSpanLength, startingElevation, material);
        //    var temperature = 25.0;
        //    var iceRadius = 2.0;
        //    var windPressure = 3.0;
        //    var weather = new Weather(temperature, iceRadius, windPressure);

        //    // Execute
        //    var tension = weather.CalculateSag(wire);

        //    // Assert
        //    Assert.Equal(expectedResult, tension);
        //}
    }
}
