using Anno1800.Models;
using Anno1800.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Anno1800.Views
{
    public partial class FarmersViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        [ObservableProperty]
        private int farmersCount = 150;

        [ObservableProperty]
        private ObservableCollection<NeedWithSubNeeds> baseNeeds = new();

        [ObservableProperty]
        private ObservableCollection<NeedWithSubNeeds> premiumNeeds = new();

        [ObservableProperty]
        private string calculationResult = string.Empty;

        public FarmersViewModel(DataService dataService)
        {
            _dataService = dataService;
            _ = LoadDefaultAsync();
        }

        private async Task LoadDefaultAsync()
        {
            await CalculateAsync();
        }

        [RelayCommand]
        private async Task CalculateAsync()
        {
            BaseNeeds.Clear();
            PremiumNeeds.Clear();

            var allNeeds = await _dataService.GetNeedsWithSubNeedsAsync(FarmersCount);

            foreach (var item in allNeeds)
            {
                if (item.Need.UnlockAtPopulation <= 100)
                    BaseNeeds.Add(item);
                else if (item.Need.UnlockAtPopulation <= 150)
                    PremiumNeeds.Add(item);
            }

            CalculateStructures(FarmersCount);
        }

        private void CalculateStructures(int totalFarmers)
        {
            const double baseGrowthPerHouse = 8.0;  // рынок + рыба
            const double premiumGrowthPerHouse = 9.0; // рынок + рыба + одежда

            int baseHouses = (int)Math.Ceiling(100 / baseGrowthPerHouse);
            int premiumHouses = (int)Math.Ceiling(150 / premiumGrowthPerHouse);

            var sb = new System.Text.StringBuilder();

            sb.AppendLine("Базовые потребности (на 100 фермеров):");
            sb.AppendLine($"- {baseHouses} домов фермеров");
            sb.AppendLine($"- 1 рынок");
            sb.AppendLine($"- 1 рыбная гавань");
            sb.AppendLine($"- 1 хлопковое поле");
            sb.AppendLine($"- 1 ткацкая мастерская");
            sb.AppendLine();

            sb.AppendLine("Премиум потребности (на 150 фермеров):");
            sb.AppendLine($"- {premiumHouses} домов фермеров");
            sb.AppendLine($"- 1 картофельное поле");
            sb.AppendLine($"- 1 завод шнапса");
            sb.AppendLine($"- 1 паб");

            CalculationResult = sb.ToString();
        }
    }
}
