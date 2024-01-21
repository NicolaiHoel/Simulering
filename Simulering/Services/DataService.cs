using CsvHelper;
using CsvHelper.Configuration;
using Simulering.Models;
using System.Globalization;

namespace Simulering.Services;

public class DataAccsess
{
    public static List<WeatherData> ReadWeatherFile()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture);
        config.Delimiter = ";";
        config.HeaderValidated = null;
        //Hardcoded path, replace with your own or a dynamic expression
        using var reader = new StreamReader("C:\\Users\\nicolai.hoel\\source\\practice\\Simulering\\Simulering\\CsvDataSource\\weather-geilo.csv");
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<WeatherDataMap>();
        var records = csv.GetRecords<WeatherData>().ToList();
        Console.WriteLine("Data loaded");

        return records;
    }

    public class WeatherDataMap : ClassMap<WeatherData>
    {
        public WeatherDataMap()
        {
            Map(m => m.Time).Index(0);
            Map(m => m.Precipitation).Index(1);
            Map(m => m.Temperature).Index(2);
        }
    }

    public static void WriteWaterPotenialToFile(List<WaterPotential> waterPotentialList)
    {
        using var writer = new StreamWriter("WaterPotential.csv");
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(waterPotentialList);
        Console.WriteLine("Data written to file");
    }
}
