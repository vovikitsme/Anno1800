using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800.Models
{
    public class PopulationGroup
    {
        public string Name { get; set; } = string.Empty;
        public List<Need> Needs { get; set; } = new();
    }
}
