using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800.Models
{
    public class NeedWithSubNeeds
    {
        public Need Need { get; set; }
        public List<SubNeed> SubNeeds { get; set; } = new();
    }
}
