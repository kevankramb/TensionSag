using System;
using TensionSag.Api.Models;
using TensionSag.Api.Extensions;
using Xunit;

namespace TensionSag.Tests
{
    public class CreepModelTests : IClassFixture<WireFactory>
    {
        private WireFactory WireFactory;

        public CreepModelTests(WireFactory wireFactory)
        {
            WireFactory = wireFactory;
        }

        public static readonly int SigFigs = 6;

        [Fact]
        public void CalculateCreepStrain_Success()
        {
            // Setup
            var expectedResult = 0.000827141144670066;
            var wire = WireFactory.Create();
            var creepRTSPercent = 20;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var result = creep.CalculateCreepStrain(wire);

            // Assert
            Assert.Equal(expectedResult, result, SigFigs);
        }
    }
}
