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
                "KM",
                "M",
                "UNIT"
            };
        }
        public static class Weight
        {
            public static List<string> weightUnitTypes = new List<string>()
            {
                "KG",
                "G",
            };
        }
    }
}
