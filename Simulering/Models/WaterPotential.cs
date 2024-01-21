using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulering.Models;

public class WaterPotential
{
    public DateTime Time { get; set; }
    public double WaterRunoff { get; set; }
    public double SnowAmount { get; set; }
    public double WaterAmount { get; set; }
    public double IceAmount { get; set; }
}
