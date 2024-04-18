using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Enums.MetricUnits
{
    public static class MetricUnits
    {
        public static class Measure
        {
            public static List<string> measurementUnitTypes = new List<string>()
            {
                "TON",
                "CX",
                "UNIT",
                "LITRO",
                "PARES",
                "M",
                "M2",
                "M3",
                "MKW/H",
                "QUILT",
                "TML",
                "GRAMA",
                "BUI",
                "KGBR"
            };
        }
    }
}
