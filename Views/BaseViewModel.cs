using Anno1800.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800.Views
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
        }

        [ObservableProperty]
        private string farmersCount;

        [ObservableProperty]
        private string selectedResource;

        [ObservableProperty]
        private bool isPlanksAvailable;

        [ObservableProperty]
        private string calculationResult;
    }
}