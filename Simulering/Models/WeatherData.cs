using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulering.Models;

public class WeatherData
{
    public DateTime Time { get; set; }
    public double Precipitation { get; set; }
    public double Temperature { get; set; }
}
