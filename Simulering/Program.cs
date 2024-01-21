using Simulering.Services;

internal class Program
{

    private static void Main()
    {
        Console.WriteLine("Welcome to snowsimulator");
        var weatherData = DataAccsess.ReadWeatherFile();

        var waterPotenialList = SnowService.CalculateWaterPotential(weatherData);


        DataAccsess.WriteWaterPotenialToFile(waterPotenialList);
        
        Console.WriteLine("Simulation has ended");
    }
}