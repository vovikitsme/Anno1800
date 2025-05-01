using Anno1800.Database;
using Anno1800.Models;
using Anno1800.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anno1800.Views
{
    public partial class FarmersViewModel : BaseViewModel
    {
        private readonly DataService _dataService;

        private readonly ISQLiteAsyncConnection _db;

        public ObservableCollection<NeedDisplayDto> Needs { get; } = new();
        public ObservableCollection<ProductionResultDto> ProductionResults { get; } = new();

        public FarmersViewModel(DataService dataService)
        {
            _dataService = dataService;
            LoadNeedsAsync();
        }

        private async void LoadNeedsAsync()
        {
            var needs = await _dataService.GetNeedsByPopulationClassAsync("Farmers");

            Needs.Clear();
            foreach (var need in needs)
            {
                Needs.Add(need);
            }
        }

        [RelayCommand]
        private void Calculate()
        {

            ProductionResults.Clear();

            if (!int.TryParse(FarmersCount, out int count) || count <= 0 || Needs.Count == 0)
            {
                CalculationResult = "Введите корректное количество фермеров.";
                return;
            }

            foreach (var need in Needs)
            {
                if (need.ConsumptionPerCapita <= 0 || need.ProductionOutputPerDay <= 0) continue;

                double totalConsumption = Convert.ToDouble(FarmersCount) * need.ConsumptionPerCapitaPer5Min;
                double productionCount = totalConsumption / need.ProductionOutputPer5Min;

                //var farmersPerProduction = (double)(need.ProductionOutputPerDay / need.ConsumptionPerCapita);

                string farmersPerProductionDisplay = $"Для {FarmersCount} необходимо {Math.Floor(productionCount):N0} производств";

                ProductionResults.Add(new ProductionResultDto
                {
                    Name = need.Name,
                    IconPath = need.IconPath,
                    FarmersPerProductionDisplay = farmersPerProductionDisplay
                });
            }
        }
    }
}