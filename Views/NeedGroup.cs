using Anno1800.Models;
using System.Collections.ObjectModel;

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