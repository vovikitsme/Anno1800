using Anno1800.Models;
using Anno1800.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Anno1800.Views
{
    public partial class FarmersViewModel : BaseViewModel
    {
        private readonly DataService _dataService;


        [ObservableProperty]
        private int farmersCount = 150;

        public ObservableCollection<NeedDisplayDto> BasicNeeds { get; } = new();
        public ObservableCollection<NeedDisplayDto> PremiumNeeds { get; } = new();

        public ICommand ToggleExpandCommand { get; }

        public IRelayCommand CalculateCommand { get; }

        public FarmersViewModel(DataService dataService)
        {
            _dataService = dataService;
            CalculateCommand = new AsyncRelayCommand(CalculateNeedsAsync);
            _ = CalculateNeedsAsync(); // автоматически при запуске

            ToggleExpandCommand = new Command<NeedDisplayDto>(ToggleExpand);
        }

        private void ToggleExpand(NeedDisplayDto need)
        {
            need.IsExpanded = !need.IsExpanded;
            OnPropertyChanged(nameof(BasicNeeds)); // Обновить UI
        }

        private async Task CalculateNeedsAsync()
        {
            BasicNeeds.Clear();
            PremiumNeeds.Clear();

            var needs = await _dataService.GetNeedsByPopulationClassAsync("Farmers");

            foreach (var need in needs)
            {
                // Разделим по IsPremium, как ты предложил ранее
                var targetFarmers = need.IsPremium ? 150 : 100;

                var totalConsumption = targetFarmers * need.ConsumptionPerCapitaPer5Min;
                var productionCount = totalConsumption / need.ProductionOutputPer5Min;
                var farmersPerProduction = productionCount > 0 ? targetFarmers / productionCount : 0;

                var needDisplay = new NeedDisplayDto
                {
                    Name = need.Name,
                    IconPath = need.IconPath,
                    ProductionCount = productionCount,
                    FarmersPerProduction = farmersPerProduction
                };

                if (need.IsPremium)
                    PremiumNeeds.Add(needDisplay);
                else
                    BasicNeeds.Add(needDisplay);
            }
        }
    }
}