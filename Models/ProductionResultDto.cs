using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800.Models
{
    public class ProductionResultDto
    {
        public string Name { get; set; }
        public string IconPath { get; set; }
        public double ProductionCount { get; set; }
        public int FarmersPerProduction { get; set; } // Новое поле

        public string ProductionCountDisplay { get; set; }

        public string FarmersPerProductionDisplay { get; set; }
    }
}
