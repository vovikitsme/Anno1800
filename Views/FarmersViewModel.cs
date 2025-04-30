using Anno1800.Database;
using Anno1800.Models;
using Anno1800.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<string> AvailableResources { get; } = new();

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
            if (!int.TryParse(FarmersCount, out int count) || count <= 0 || Needs.Count == 0)
            {
                CalculationResult = "Введите корректное количество фермеров.";
                return;
            }

            var sb = new StringBuilder();

            foreach (var need in Needs)
            {
                if (need.ConsumptionPerCapita <= 0 || need.ProductionOutputPerDay <= 0) continue;

                double totalConsumption = Convert.ToDouble(FarmersCount) * need.ConsumptionPerCapita;
                double productionCount = totalConsumption / need.ProductionOutputPerDay;

                string productionText = productionCount < 0.01 ? "< 0.01 производств" : $"{productionCount:F2} производств";

                sb.AppendLine($"{need.Name}: {productionText}");
            }

            CalculationResult = sb.ToString();
        }
    }
}
