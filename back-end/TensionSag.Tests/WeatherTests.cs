using System;
using TensionSag.Api.Models;
using TensionSag.Api.Extensions;
using Xunit;

namespace TensionSag.Tests
{
  public class WeatherTests
  {
    [Fact]
    public void CalculateTension_Success()
    {
      // Setup
      var expectedResult = 0.0;
      var name = "Tension Wire Test Drake 795";
      var totalCrossSection = 0.00046844;
      var initialWireDiameter = 0.0281432;
      var finalWireDiameter = 0.0281432;
      var initialWireLinearWeight = 15.9657;
      var finalWireLinearWeight = 15.9657;
      var outerElasticity = 44126400000;
      var outerThermalCoefficient = 0.00002304;
      var coreElasticity = 25510600000;
      var coreThermalCoefficient = 0.00001152;
      var outerStressStrainK0 = -8363341;
      var outerStressStrainK1 = 305493595.6;
      var outerStressStrainK2 = -96556939.03;
      var outerStressStrainK3 = -2.59367e8;
      var outerStressStrainK4 = 2.11504e8;
      var outerCreepK0 = -3756263.8;
      var outerCreepK1 = 147732585.6;
      var outerCreepK2 = -129912395.9;
      var outerCreepK3 = 3.7887e7;
      var outerCreepK4 = 0;
      var coreStressStrainK0 = -477806.7;
      var coreStressStrainK1 = 2.66338e8;
      var coreStressStrainK2 = 27565929.1;
      var coreStressStrainK3 = -3.1518e8;
      var coreStressStrainK4 = 1.92309e8;
      var coreCreepK0 = 324743.1;
      var coreCreepK1 = 249668124.8;
      var coreCreepK2 = 84125691.63;
      var coreCreepK3 = -4.99125e8;
      var coreCreepK4 = 3.19489e8;
      var startingTension = 22500.0;
      var startingTemp = 15;
      var startingSpanLength = 50;
      var startingElevation = 0.0;
      var startingTensionType = true;
      var material = WireMaterial.ACSR;
      var wire = Wire.Create(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, 
          outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
          outerStressStrainK0, outerStressStrainK1, outerStressStrainK2, outerStressStrainK3, outerStressStrainK4,
          outerCreepK0, outerCreepK1, outerCreepK2, outerCreepK3, outerCreepK4,
          coreStressStrainK0, coreStressStrainK1, coreStressStrainK2, coreStressStrainK3, coreStressStrainK4,
          coreCreepK0, coreCreepK1, coreCreepK2, coreCreepK3, coreCreepK4,
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
      Assert.Equal(expectedResult, tension);
    }

    [Fact]
    public void CalculateSag_Success()
    {
      // Setup
      var expectedResult = 0.0;
      var name = "Sag Wire Test";
      var totalCrossSection = 15.0;
      var totalWireLinearWeight = 15.0;
      var elasticity = 1.0;
      var thermalCoefficient = 4.0;
      var startingTension = 500.0;
      var startingTemp = 15;
      var startingSpanLength = 25;
      var startingElevation = 0.0;
      var material = WireMaterial.ACSR;
      var wire = Wire.Create(name, totalCrossSection, totalWireLinearWeight, elasticity, thermalCoefficient, startingTension, startingTemp, startingSpanLength, startingElevation, material);
      var temperature = 25.0;
      var iceRadius = 2.0;
      var windPressure = 3.0;
      var weather = new Weather(temperature, iceRadius, windPressure);

      // Execute
      var tension = weather.CalculateSag(wire);

      // Assert
      Assert.Equal(expectedResult, tension);
    }
  }
}
