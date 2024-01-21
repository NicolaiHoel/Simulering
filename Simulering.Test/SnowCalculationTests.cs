using Xunit;
using Simulering.Models;
using Simulering.Services;

namespace Simulering.Test
{
    public class SnowCalculationTests
    {
        [Fact]
        public void CalculateWaterPotential_WhenWeatherIsColdThenWarm_SnowBecomesWater()
        {
            //Arrange
            var firstWeatherData = new WeatherData()
            {
                Temperature = -5,
                Precipitation = 2
            };
            var secondWeatherData = new WeatherData()
            {
                Temperature = 4,
                Precipitation = 2
            };
            var weatherData = new List<WeatherData> { firstWeatherData, secondWeatherData };

            //Act
            var waterPotenialList = SnowService.CalculateWaterPotential(weatherData);

            //Assert
            Assert.True(waterPotenialList[1].WaterAmount > waterPotenialList[0].WaterAmount);
        }

        [Fact]
        public void AddWaterToSystem_WhenSystemContainsNoSnow_AllWaterBecomesRunoff()
        {
            //Arrange
            var waterPotenial = new WaterPotential()
            {
                SnowAmount = 0
            };
            var waterAmount = 4.5;

            //Act
            waterPotenial = SnowService.AddWaterToSystem(waterPotenial, waterAmount);

            //Assert
            Assert.Equal(waterAmount, waterPotenial.WaterRunoff);
        }

        [Fact]
        public void AddWaterToSystem_WhenSystemContainsSnowButNoWater_WaterAmountIncreases()
        {
            //Arrange
            var oldWaterAmount = 0;
            var waterPotenial = new WaterPotential()
            {
                SnowAmount = 4,
                WaterAmount = oldWaterAmount
            };
            var waterAmount = 4.5;

            //Act
            waterPotenial = SnowService.AddWaterToSystem(waterPotenial, waterAmount);

            //Assert
            Assert.True(waterPotenial.WaterAmount > oldWaterAmount);
        }
    }
}