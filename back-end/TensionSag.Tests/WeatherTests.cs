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
      var name = "Tension Wire Test";
      var totalCrossSection = 15.0;
      var totalLinearWeight = 15.0;
      var elasticity = 1.0;
      var thermalCoefficient = 4.0;
      var startingTension = 500.0;
      var startingTemp = 15;
      var startingSpanLength = 25;
      var startingElevation = 0.0;
      var material = WireMaterial.Copper;
      var wire = Wire.Create(name, totalCrossSection, totalLinearWeight, elasticity, thermalCoefficient, startingTension, startingTemp, startingSpanLength, startingElevation, material);
      var temperature = 25.0;
      var iceRadius = 2.0;
      var windPressure = 3.0;
      var weather = new Weather(temperature, iceRadius, windPressure);

      // Execute
      var tension = weather.CalculateTension(wire);

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
      var totalLinearWeight = 15.0;
      var elasticity = 1.0;
      var thermalCoefficient = 4.0;
      var startingTension = 500.0;
      var startingTemp = 15;
      var startingSpanLength = 25;
      var startingElevation = 0.0;
      var material = WireMaterial.Copper;
      var wire = Wire.Create(name, totalCrossSection, totalLinearWeight, elasticity, thermalCoefficient, startingTension, startingTemp, startingSpanLength, startingElevation, material);
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
