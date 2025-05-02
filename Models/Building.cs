using SQLite;

namespace Anno1800.Models
{
    public class Building
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Produces { get; set; } // продукт, который производит
        public int WorkforceRequired { get; set; }
        public string WorkforceType { get; set; } // "Farmer", "Worker", и т.д.
    }
}