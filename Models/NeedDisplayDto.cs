namespace Anno1800.Models
{
    public class NeedDisplayDto
    {
        public string Name { get; set; }                 // Название потребности, например, "Fish"
        public string NeedTypeName { get; set; }         // Тип потребности: "Basic" или "Luxury"
        public string PopulationClassName { get; set; }  // Название класса населения: "Farmers", "Workers" и т.д.
        public string IconPath { get; set; }
        public double ConsumptionRate { get; set; }
        public double ProductionOutputPerDay { get; set; } //
        public int ProductionBuildingsRequired { get; set; }
        public double ConsumptionPerCapita { get; set; } // как 0.0004166667
        public double FarmersPerProduction { get; set; }

        public double ProductionCount { get; set; }

        public double ConsumptionPerCapitaPer5Min { get; set; } // Потребление на человека за 5 минут
        public double ProductionOutputPer5Min { get; set; }     // Производство за 5 минут

        public bool IsPremium { get; set; }
		
		 public string ProductionCountDisplay =>
            ProductionCount < 0.01 ? "< 0.01 производств" : $"{ProductionCount:F2} производств";

        public string FarmersPerProductionDisplay =>
            FarmersPerProduction > 0 ? $"1 производство покрывает: {Math.Floor(FarmersPerProduction):N0} фермеров" : "Недостаточно данных";


        public bool IsExpanded { get; set; }  // для управления раскрытием

        public List<NeedDisplayDto> SubProductions { get; set; } = new();
    }
    }