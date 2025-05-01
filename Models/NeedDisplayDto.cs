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
        public double ProductionOutputPerDay { get; set; } // Новое поле
        public int ProductionBuildingsRequired { get; set; }
        public double ConsumptionPerCapita { get; set; } // как 0.0004166667
        public int FarmersPerProduction { get; set; } // Новое поле

        public double ConsumptionPerCapitaPer5Min { get; set; } // Потребление на человека за 5 минут
        public double ProductionOutputPer5Min { get; set; }     // Производство за 5 минут


    }
}