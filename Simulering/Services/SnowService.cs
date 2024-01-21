using Simulering.Models;

namespace Simulering.Services;

public class SnowService
{
    private const double IceToWaterConstant = 0.01;
    private const double SnowToWaterConstant = 0.015;
    private const double WaterToIceConstant = 0.012;

    public static List<WaterPotential> CalculateWaterPotential(List<WeatherData> weatherData)
    {
        var waterPotentialList = InitializeWaterPotentialList(weatherData[0]);

        for (var i = 1; i < weatherData.Count; i++)
        {

            var currentWeatherData = weatherData[i];
            var previousWaterPotenial = waterPotentialList[i - 1];
            var currentWaterPotenial = new WaterPotential()
            {
                Time = currentWeatherData.Time,
                SnowAmount = previousWaterPotenial.SnowAmount,
                WaterAmount = previousWaterPotenial.WaterAmount,
                IceAmount = previousWaterPotenial.IceAmount
            };

            if (currentWeatherData.Temperature >= 0)
            {
                currentWaterPotenial = CalculateRain(currentWaterPotenial, currentWeatherData.Precipitation);
                currentWaterPotenial = CalculateMelt(currentWaterPotenial, currentWeatherData.Temperature);
            }
            else
            {
                currentWaterPotenial = CalculateSnow(currentWaterPotenial, currentWeatherData.Precipitation);
                currentWaterPotenial = CalculateFreeze(currentWaterPotenial, currentWeatherData.Temperature);
            }

            waterPotentialList.Add(currentWaterPotenial);

        }

        return waterPotentialList;
    }

    public static WaterPotential AddWaterToSystem(WaterPotential waterPotential, double waterAmount)
    {
        var waterAsPercentageOfSnow = waterPotential.WaterAmount / waterPotential.SnowAmount;

        if (waterPotential.SnowAmount == 0)
        {
            waterPotential.WaterRunoff = waterAmount;
            return waterPotential;
        }

        if (waterAsPercentageOfSnow > 0.6)
        {
            waterPotential.WaterRunoff = waterAmount;
            return waterPotential;
        }

        var incomingWaterAsPercentageOfSnow = waterAmount / waterPotential.SnowAmount;

        if (incomingWaterAsPercentageOfSnow + waterAsPercentageOfSnow > 0.6)
        {
            waterPotential.WaterRunoff = (incomingWaterAsPercentageOfSnow + waterAsPercentageOfSnow - 0.6) * waterPotential.SnowAmount;
            waterPotential.WaterAmount = 0.6 * waterPotential.SnowAmount;
            return waterPotential;
        }

        waterPotential.WaterAmount = waterAmount + waterPotential.WaterAmount;

        return waterPotential;
    }

    private static WaterPotential CalculateRain(WaterPotential currentWaterPotenial, double precipitation)
    {
        currentWaterPotenial = AddWaterToSystem(currentWaterPotenial, precipitation);

        return currentWaterPotenial;
    }

    private static WaterPotential CalculateMelt(WaterPotential currentWaterPotenial, double temperature)
    {
        var iceMelt = currentWaterPotenial.IceAmount * temperature * IceToWaterConstant;
        currentWaterPotenial.IceAmount -= iceMelt;

        var snowMelt = currentWaterPotenial.SnowAmount * temperature * SnowToWaterConstant;
        currentWaterPotenial.SnowAmount -= snowMelt;

        var waterReleaseFromSnow = 0.0;
        if(currentWaterPotenial.WaterAmount /currentWaterPotenial.SnowAmount > 0.6)
        {
            waterReleaseFromSnow = currentWaterPotenial.WaterAmount - (0.6 * currentWaterPotenial.SnowAmount);
            currentWaterPotenial.WaterAmount = 0.6 * currentWaterPotenial.SnowAmount;
        }

        var toatlMelt = iceMelt + snowMelt + waterReleaseFromSnow;

        currentWaterPotenial = AddWaterToSystem(currentWaterPotenial, toatlMelt);

        return currentWaterPotenial;
    }

    private static WaterPotential CalculateSnow(WaterPotential currentWaterPotenial, double precipitation)
    {
        currentWaterPotenial.SnowAmount += precipitation;

        return currentWaterPotenial;
    }

    private static WaterPotential CalculateFreeze(WaterPotential currentWaterPotenial, double temperature)
    {
        var waterFrozenToIce = currentWaterPotenial.WaterAmount * Math.Abs(temperature) * WaterToIceConstant;

        currentWaterPotenial.IceAmount += waterFrozenToIce;
        currentWaterPotenial.WaterAmount -= waterFrozenToIce;

        return currentWaterPotenial;
    }

    private static List<WaterPotential> InitializeWaterPotentialList(WeatherData firstWeatherData)
    {
        var waterPotenialList = new List<WaterPotential>();

        var firstWaterPotenial = new WaterPotential()
        {
            Time = firstWeatherData.Time,
            WaterRunoff = 0,
            SnowAmount = 0,
            WaterAmount = 0,
            IceAmount = 0
        };

        if (firstWeatherData.Temperature > 0)
        {
            firstWaterPotenial.WaterRunoff = firstWeatherData.Precipitation;
        }
        else
        {
            firstWaterPotenial.SnowAmount = firstWeatherData.Precipitation;
        }

        waterPotenialList.Add(firstWaterPotenial);

        return waterPotenialList;
    }

}
