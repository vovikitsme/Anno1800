using SQLite;

namespace Anno1800.Models
{
    public class NeedType
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } // "Basic" or "Luxury"
    }
}