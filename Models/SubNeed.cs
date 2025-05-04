using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800.Models
{
    public class SubNeed
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ParentNeedId { get; set; } // внешний ключ на Need

        public string Name { get; set; }
        public string IconPath { get; set; }
    }
}
