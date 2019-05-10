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
            var wire = WireFactory.Create(795);
            var temperature = 0.0;
            var iceRadius = 0.0;
            var windPressure = 0.0;
            var finalSpanLength = wire.StartingSpanLength;
            var finalElevation = wire.StartingElevation;
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
            var expectedResult = 27057.963390129;
            var wire = WireFactory.Create(795);
            var temperature = 0.0;
            var iceRadius = 0.0;
            var windPressure = 0.0;
            var finalSpanLength = wire.StartingSpanLength;
            var finalElevation = wire.StartingElevation;
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
        public void Calculate556InitialWireTestCold_Success()
        {
            // Setup
            var expectedResult = 5691.34812333956;
            var wire = WireFactory.Create(556);
            var temperature = -20.0;
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
        public void Calculate556ElasticTensionHot_Success()
        {
            // Setup
            var expectedResult = 1339.4659607512;
            var wire = WireFactory.Create(556);
            var temperature = 100.0;
            var iceRadius = 0.0;
            var windPressure = 0.0;
            var finalSpanLength = 50;
            var finalElevation = 0;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);
            var creepRTSPercent = 10;
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

        [Fact]
        public void CalculateFinalLinearForceInclined_Success()
        {
            // Setup
            var expectedResult = 43.5274780001651;
            var wire = WireFactory.Create(1);
            var temperature = 0.0;
            var iceRadius = 0.0125;
            var windPressure = 400.0;
            var finalSpanLength = 36;
            var finalElevation = 1.26;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);

            // Execute
            var linearForce = WeatherExtensions.CalculateFinalLinearForce(weather, wire);

            // Assert
            Assert.Equal(expectedResult, linearForce, SigFigs);
        }

        [Fact]
        public void CalculateXcInclinded_Success()
        {
            // Setup
            var expectedResult = 25;
            var catenaryConstant = 183.972911;
            var spanLength = 35.97;
            var spanElevation = 1.256;

            // Execute
            var Xc = WeatherExtensions.CalculateXc(spanLength, spanElevation, catenaryConstant);

            // Assert
            Assert.Equal(expectedResult, Xc, SigFigs);
        }

        [Fact]
        public void CalculateYcInclined_Success()
        {
            // Setup
            var expectedResult = -0.223220217279341;
            var catenaryConstant = 183.972911;
            var Xc = 11.5725554426295;

            // Execute
            var Yc = WeatherExtensions.CalculateYc(catenaryConstant, Xc);

            // Assert
            Assert.Equal(expectedResult, Yc, SigFigs);
        }

        [Fact]
        public void CalculateXdInclined_Success()
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
    }
}
