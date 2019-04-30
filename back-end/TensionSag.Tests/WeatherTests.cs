using System;
using TensionSag.Api.Models;
using TensionSag.Api.Extensions;
using Xunit;
using System.Collections.Generic;

namespace TensionSag.Tests
{
    public class WeatherTests : IClassFixture<WireFactory>
    {
        private WireFactory WireFactory;

        public WeatherTests (WireFactory wireFactory)
        {
            WireFactory = wireFactory;
        }

        public static readonly int SigFigs = 6;

        [Fact]
        public void CalculateInitialTension_Success()
        {
            // Setup
            var expectedResult = 29066.4212528342;
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
            var startingTensionType = true;
            var material = WireMaterial.ACSR;
            var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
                outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
            var temperature = 0.0;
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
            Assert.Equal(expectedResult, tension, SigFigs);
        }

        [Fact]
        public void CalculateElasticTension_Success()
        {
            // Setup
            var expectedResult = 27057.9633822329;
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
            var startingTensionType = true;
            var material = WireMaterial.ACSR;
            var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
                outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
            var temperature = 0.0;
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
            Assert.Equal(expectedResult, tension, SigFigs);
        }

        [Fact]
        public void CalculateSag_Success()
        {
            // Setup
            var expectedResult = 0.223220217279341;
            var catenaryConstant = 1400.0;
            var spanLength = 50;
            var spanElevation = 0.0;
        
            // Execute
            var sag = WeatherExtensions.CalculateSag(catenaryConstant, spanLength, spanElevation);

            // Assert
            Assert.Equal(expectedResult, sag, SigFigs);
        }

        [Fact]
        public void CalculateXc_Success()
        {
            // Setup
            var expectedResult = 25;
            var catenaryConstant = 1400.0;
            var spanLength = 50;
            var spanElevation = 0.0;

            // Execute
            var Xc = WeatherExtensions.CalculateXc(spanLength, spanElevation, catenaryConstant);

            // Assert
            Assert.Equal(expectedResult, Xc, SigFigs);
        }

        [Fact]
        public void CalculateYc_Success()
        {
            // Setup
            var expectedResult = -0.223220217279341;
            var catenaryConstant = 1400.0;
            var Xc = 25;

            // Execute
            var Yc = WeatherExtensions.CalculateYc(catenaryConstant, Xc);

            // Assert
            Assert.Equal(expectedResult, Yc, SigFigs);
        }

        [Fact]
        public void CalculateXd_Success()
        {
            // Setup
            var expectedResult = 25;
            var catenaryConstant = 1400.0;
            var Xc = 25;
            var spanLength = 50;
            var spanElevation = 0.0;

            // Execute
            var Xd = WeatherExtensions.CalculateXd(Xc, catenaryConstant, spanElevation, spanLength);

            // Assert
            Assert.Equal(expectedResult, Xd, SigFigs);
        }

        [Fact]
        public void CalculateArcLength_Success()
        {
            // Setup
            var expectedResult = 50.0026573552933;
            var catenaryConstant = 1400.0;
            var spanLength = 50;
            var spanElevation = 0.0;

            // Execute
            var ArcLength = WeatherExtensions.CalculateArcLength(spanLength, spanElevation, catenaryConstant);

            // Assert
            Assert.Equal(expectedResult, ArcLength, SigFigs);
        }

        [Fact]
        public void CalculateFinalLinearForce_Success()
        {
            // Setup
            var expectedResult = 43.5274780001651;
            var wire = WireFactory.Create(795);
            var temperature = 0.0;
            var iceRadius = 0.02;
            var windPressure = 200.0;
            var finalSpanLength = 50;
            var finalElevation = 0;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);

            // Execute
            var linearForce = WeatherExtensions.CalculateFinalLinearForce(weather, wire);

            // Assert
            Assert.Equal(expectedResult, linearForce, SigFigs);
        }

        [Fact]
        public void Calculate556InitialWireTest_Success()
        {
            // Setup
            var expectedResult = 3542.03457045459;
            var wire = WireFactory.Create(556);
            var temperature = 0.0;
            var iceRadius = 0.0;
            var windPressure = 0.0;
            var finalSpanLength = 50;
            var finalElevation = 0;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);
            var creepRTSPercent = 5;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var tension = WeatherExtensions.CalculateInitialTensions(weather, wire, creep);

            // Assert
            Assert.Equal(expectedResult, tension, SigFigs);
        }

        [Fact]
        public void Calculate556ElasticTension_Success()
        {
            // Setup
            var expectedResult = 2819.01124957566;
            var wire = WireFactory.Create(556);
            var temperature = 0.0;
            var iceRadius = 0.0;
            var windPressure = 0.0;
            var finalSpanLength = 50;
            var finalElevation = 0;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);
            var creepRTSPercent = 5;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var tension = WeatherExtensions.CalculateElasticTension(weather, wire, creep);

            // Assert
            Assert.Equal(expectedResult, tension, SigFigs);
        }

        [Fact]
        public void Calculate556SagTest_Success()
        {
            // Setup
            var expectedResult = 0.660679754095347;
            var wire = WireFactory.Create(556);
            var temperature = 0.0;
            var iceRadius = 0.0;
            var windPressure = 0.0;
            var finalSpanLength = 50;
            var finalElevation = 0;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);
            var creepRTSPercent = 5;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var sag = WeatherExtensions.CalculateSag(3600 / WeatherExtensions.CalculateFinalLinearForce(weather, wire), weather.FinalSpanLength, weather.FinalElevation);

            // Assert
            Assert.Equal(expectedResult, sag, SigFigs);
        }
    }
}
