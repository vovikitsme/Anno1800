using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Anno1800.Views
{
    public partial class MainViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task NavigateToFarmers()
        {
            await Shell.Current.GoToAsync("//FarmersView");
        }
    }
}
