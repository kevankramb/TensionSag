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
            var expectedResult = 27060.8000272576;
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
            // Test the transform back to the structures coordinate system
            var finaltension = WeatherExtensions.StructureLongitudinalForce(weather, wire, finalElevation, finalSpanLength, tension);

            // Assert
            Assert.Equal(expectedResult, tension, SigFigs);
            Assert.Equal(expectedResult, finaltension, SigFigs);
        }

        [Fact]
        public void CalculateInitialDuplexTension_Success()
        {
            // Setup
            var expectedResult = 4156.73688896795;
            var wire = WireFactory.Create(4);
            var temperature = -40.0;
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
            var expectedResult = 45.2625626876656;
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
            var expectedResult = 1339.50376340508;
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
            var expectedResult = 26.5803407868209;
            var wire = WireFactory.Create(1);
            var temperature = 0.0;
            var iceRadius = 0.0125;
            var windPressure = 400.0;
            var finalSpanLength = 35.97;
            var finalElevation = 1.256;
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
            var expectedResult = 11.5725554426295;
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
            var expectedResult = -0.364097639344555;
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
            var expectedResult = 17.9952144920095;
            var catenaryConstant = 183.972911;
            var Xc = 11.5725554426295;
            var spanLength = 35.97;
            var spanElevation = 1.256;

            // Execute
            var Xd = WeatherExtensions.CalculateXd(Xc, catenaryConstant, spanElevation, spanLength);

            // Assert
            Assert.Equal(expectedResult, Xd, SigFigs);
        }

        [Fact]
        public void CalculateSagInclined_Success()
        {
            // Setup
            var expectedResult = 0.880332532899847;
            var catenaryConstant = 183.972911;
            var spanLength = 35.97;
            var spanElevation = 1.256;

            // Execute
            var Sag = WeatherExtensions.CalculateSag(catenaryConstant, spanLength, spanElevation);

            // Assert
            Assert.Equal(expectedResult, Sag, SigFigs);
        }

        //this is a pretty good example of how the general backend works
        //calculate which ever horizontal tension you need, then input that into the 'structure' force transformations to read out the 
        //actual force exerted on the structure.
        [Fact]
        public void CalculateLargeInclinationElasticTension_Success()
        {
            // Setup
            var expectedTension = 16972.6716275075;
            var expectedLongitudinalTension = 11558.1651525476;
            var expectedVertical = -13933.9733840054; //negative here doesnt make sense but thats how CIMA did the calculation, we correct it for the final vertical force
            var expectedVerticalForce = 18639.808930321;
            var wire = WireFactory.Create(557);
            var temperature = -30.0;
            var iceRadius = 0.02;
            var windPressure = 400.0;
            var finalSpanLength = wire.StartingSpanLength;
            var finalElevation = wire.StartingElevation;
            var weather = new Weather(temperature, iceRadius, windPressure, finalSpanLength, finalElevation);
            var creepRTSPercent = 10;
            var creep = new Creep(creepRTSPercent);

            // Execute
            var tension = WeatherExtensions.CalculateElasticTension(weather, wire, creep);
            var blownspanlength = WeatherExtensions.BlownSpanLength(weather, wire, finalElevation, finalSpanLength); //just an intermediate step for debugging
            var blownelevation = WeatherExtensions.BlownSpanElevation(weather, wire, finalElevation); //just an intermediate step for debugging
            var verticalforce = WeatherExtensions.CalculateVerticalForce(weather, wire, blownspanlength, blownelevation, tension); //just an intermediate step for debugging

            // Test the transform back to the structures coordinate system
            var finallongitudinaltension = WeatherExtensions.StructureLongitudinalForce(weather, wire, finalElevation, finalSpanLength, tension);
            var finalverticaltension = WeatherExtensions.StructureVerticalForce(weather, wire, finalElevation, finalSpanLength, tension);

            // Assert
            Assert.Equal(expectedTension, tension, SigFigs);
            Assert.Equal(expectedLongitudinalTension, finallongitudinaltension, SigFigs);
            Assert.Equal(expectedVertical, verticalforce, SigFigs);
            Assert.Equal(expectedVerticalForce, finalverticaltension, SigFigs);
        }


    }
}
