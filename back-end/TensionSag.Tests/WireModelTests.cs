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
      var length = 15.0;
      var temperature = 15.0;
      var material = WireMaterial.Copper;

      // Execute
      var wire = new Wire(name, length, temperature, material);

      // Assert
      Assert.IsType<Wire>(wire);
      Assert.Equal(name, wire.Name);
    }

    [Fact]
    public void CalculateTension_Success()
    {
      // Setup
      var name = "Tension Wire Test";
      var length = 15.0;
      var temperature = 15.0;
      var material = WireMaterial.Copper;
      var expectedResult = length * temperature;
      var wire = new Wire(name, length, temperature, material);

      // Execute
      var tension = wire.CalculateTension();

      // Assert
      Assert.Equal(expectedResult, tension);
    }

    [Fact]
    public void CalculateSag_Success()
    {
      // Setup
      var name = "Sag Wire Test";
      var length = 15.0;
      var temperature = 15.0;
      var material = WireMaterial.Copper;
      var expectedResult = length * 25.0;
      var wire = new Wire(name, length, temperature, material);

      // Execute
      var tension = wire.CalculateSag();

      // Assert
      Assert.Equal(expectedResult, tension);
    }
  }
}
