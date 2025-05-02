using SQLite;

namespace Anno1800.Models
{
    public class Need
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }          // e.g., "Fish", "Work Clothes"
        public double ConsumptionPerCapita { get; set; } // как 0.0004166667
        public double IncomeModifier { get; set; }       // e.g., +1.25
        public string UnlockCondition { get; set; }       // текстовое описание условия
        public int PopulationClassId { get; set; }        // FK
        public int NeedTypeId { get; set; }               // FK
        public double ConsumptionRate { get; set; } // Новый параметр
        public double ProductionOutputPerDay { get; set; } // Новое поле
        public string IconPath { get; set; } // Путь к иконке

        public double ConsumptionPerCapitaPer5Min { get; set; } // Потребление на человека за 5 минут
        public double ProductionOutputPer5Min { get; set; }     // Производство за 5 минут

        public int GainsInhabitants { get; set; }
        
        public bool IsPremium { get; set; }
    }
}