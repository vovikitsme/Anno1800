using SQLite;

namespace Anno1800.Models
{
    public class Need
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string IconPath { get; set; }
        public int UnlockAtPopulation { get; set; } = 0;
        public int PopulationIncrease { get; set; } = 0;
    }
}