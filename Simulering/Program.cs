using Simulering.Services;

internal class Program
{

    private static void Main()
    {
        Console.WriteLine("Welcome to snowsimulator");
        var weatherData = DataAccsess.ReadWeatherFile();

        var waterPotenialList = SnowService.CalculateWaterPotential(weatherData);


        DataAccsess.WriteWaterPotenialToFile(waterPotenialList);
        //Write waterPotenialList to CSV
        /*foreach(var water in waterPotenialList)
        {
            if (water.IceAmount < 0) Console.WriteLine("ice {0}", water.IceAmount);
            if (water.SnowAmount < 0) Console.WriteLine("snow {0}", water.SnowAmount);
            if (water.WaterRunoff < 0) Console.WriteLine("run {0}", water.WaterRunoff);
            if (water.WaterAmount < 0) Console.WriteLine("water {0}", water.WaterAmount);
            if (water.WaterAmount/ water.SnowAmount > 0.6) Console.WriteLine("Too much water {0}", water.WaterAmount);
        }*/
        Console.WriteLine("Simulation has ended");
    }
}