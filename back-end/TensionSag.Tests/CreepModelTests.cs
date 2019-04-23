using System;
using TensionSag.Api.Models;
using TensionSag.Api.Extensions;
using Xunit;

namespace TensionSag.Tests
{
  public class CreepModelTests
  {
    [Fact]
    public void ValidCreepModel_Success()
    {
      // Setup and Execute
      var creep = new Creep(1.0, 25.0);

      // Assert
      Assert.IsType<Creep>(creep);
    }

    [Fact]
    public void PerformCalculate_Success()
    {
      // Setup
      var expectedResult = 25.0;
      var creep = new Creep(1.0, 25.0);

      // Execute
      var result = creep.Calculate();

      // Assert
      Assert.Equal(expectedResult, result);
    }
  }
}
