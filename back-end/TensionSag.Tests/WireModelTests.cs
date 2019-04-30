using System;
using TensionSag.Api.Models;
using TensionSag.Api.Extensions;
using Xunit;
using System.Collections.Generic;

namespace TensionSag.Tests
{
    public class WireModelTests : IClassFixture<WireFactory>
    {
        private WireFactory WireFactory;

        public WireModelTests(WireFactory wireFactory)
        {
            WireFactory = wireFactory;
        }

        public static readonly int SigFigs = 6;

        [Fact]
        public void ValidWireModel_Success()
        {
            // Setup
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
            var startingTemp = 15.0;
            var startingSpanLength = 50.0;
            var startingElevation = 0.0;
            var startingTensionType = true;
            var material = WireMaterial.ACSR;

            // Execute
            var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient, 
                outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);

            // Assert
            Assert.IsType<Wire>(wire);
            Assert.Equal(name, wire.Name);
        }

        [Fact]
        public void CalculateOriginalLength_Success()
        {
            // Setup
            var expectedLength = 49.9518250626855;
            var wire = WireFactory.Create(795);
            var creepRTSPercent = 30;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var actualLength = wire.CalculateOriginalLength(creep);

            // Assert
            Assert.Equal(expectedLength, actualLength, SigFigs);
        }

        [Fact]
        public void CalculateOriginalLength556_Success()
        {
            // Setup
            var expectedLength = 50.0223040579414;
            var wire = WireFactory.Create(556);
            var creepRTSPercent = 5;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var actualLength = wire.CalculateOriginalLength(creep);

            // Assert
            Assert.Equal(expectedLength, actualLength, SigFigs);
        }

        [Fact]
        public void CalculateWireThermalCoefficient_Success()
        {
            // Setup
            var expectedLength = 1.88197993595359E-05;
            var wire = WireFactory.Create(795);

            // Execute
            var actualLength = wire.CalculateWireThermalCoefficient();

            // Assert
            Assert.Equal(expectedLength, actualLength, SigFigs);
        }

        [Fact]
        public void CalculateStringingStrain_Success()
        {
            // Setup
            var expectedLength = 0.00101685891255513;
            var wire = WireFactory.Create(795);
            var stress = wire.StartingTension/wire.TotalCrossSection;
 

            // Execute
            var actualLength = wire.CalculateStringingStrain(stress);

            // Assert
            Assert.Equal(expectedLength, actualLength, SigFigs);
        }
    }
}
