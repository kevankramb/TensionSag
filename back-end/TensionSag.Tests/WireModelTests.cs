using System;
using TensionSag.Api.Models;
using TensionSag.Api.Extensions;
using Xunit;

namespace TensionSag.Tests
{
  public class WireModelTests
  {
    [Fact]
    public void ValidWireModel_Success()
    {
      // Setup
      var name = "Test Wire";
      var totalCrossSection = 15.0;
      var totalLinearWeight = 15.0;
      var elasticity = 1.0;
      var thermalCoefficient = 4.0;
      var material = WireMaterial.Copper;

      // Execute
      var wire = Wire.Create(name, totalCrossSection, totalLinearWeight, elasticity, thermalCoefficient, material);

      // Assert
      Assert.IsType<Wire>(wire);
      Assert.Equal(name, wire.Name);
    }

    [Fact]
    public void CalculateTension_Success()
    {
      // Setup
      var expectedResult = 225.0;
      var name = "Tension Wire Test";
      var totalCrossSection = 15.0;
      var totalLinearWeight = 15.0;
      var elasticity = 1.0;
      var thermalCoefficient = 4.0;
      var material = WireMaterial.Copper;

      // Execute
      var wire = Wire.Create(name, totalCrossSection, totalLinearWeight, elasticity, thermalCoefficient, material);

      // Execute
      var tension = wire.CalculateTension();

      // Assert
      Assert.Equal(expectedResult, tension);
    }

    [Fact]
    public void CalculateSag_Success()
    {
      // Setup
      var expectedResult = 225.0;
      var name = "Sag Wire Test";
      var totalCrossSection = 15.0;
      var totalLinearWeight = 15.0;
      var elasticity = 1.0;
      var thermalCoefficient = 4.0;
      var material = WireMaterial.Copper;

      // Execute
      var wire = Wire.Create(name, totalCrossSection, totalLinearWeight, elasticity, thermalCoefficient, material);

      // Execute
      var tension = wire.CalculateSag();

      // Assert
      Assert.Equal(expectedResult, tension);
    }
  }
}
