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
      var totalWireLinearWeight = 15.0;
      var elasticity = 1.0;
      var thermalCoefficient = 4.0;
      var startingTension = 500.0;
      var startingTemp = 15;
      var startingSpanLength = 25;
      var startingElevation = 0.0;
      var material = WireMaterial.Copper;

      // Execute
      var wire = Wire.Create(name, totalCrossSection, totalWireLinearWeight, elasticity, thermalCoefficient, startingTension, startingTemp, startingSpanLength, startingElevation, material);

      // Assert
      Assert.IsType<Wire>(wire);
      Assert.Equal(name, wire.Name);
    }
  }
}
