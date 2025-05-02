using SQLite;

namespace Anno1800.Models
{
    public class PopulationClass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } // e.g., "Farmers", "Workers", etc.
    }
}