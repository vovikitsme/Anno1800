using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800.Models
{
    public class NeedDisplayDto
    {
        public string Name { get; set; }                 // Название потребности, например, "Fish"
        public string NeedTypeName { get; set; }         // Тип потребности: "Basic" или "Luxury"
        public string PopulationClassName { get; set; }  // Название класса населения: "Farmers", "Workers" и т.д.
        public string IconPath { get; set; }
        public double ConsumptionRate { get; set; }
        public int ProductionBuildingsRequired { get; set; }
    }
}