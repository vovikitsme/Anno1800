using CommunityToolkit.Mvvm.ComponentModel;

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