using Anno1800.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800.Views
{
    public class NeedGroup : ObservableCollection<Need>
    {
        public string Title { get; }

        public NeedGroup(string title, IEnumerable<Need> needs) : base(needs)
        {
            Title = title;
        }
    }
}
