using Anno1800.Database;
using Anno1800.Models;
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
    public class FarmersViewModel : BaseViewModel
    {
        private readonly ISQLiteAsyncConnection _db;

        public ObservableCollection<Need> FarmerNeeds { get; } = new();

        public ObservableCollection<NeedGroup> GroupedFarmerNeeds { get; } = new();

        public FarmersViewModel(SqliteConnectionFactory sqliteConnectionFactory)
        {
            _db = sqliteConnectionFactory.Connect();
            LoadNeedsCommand = new Command(async () => await LoadNeedsAsync());
            LoadNeedsCommand.Execute(null);
        }

        public ICommand LoadNeedsCommand { get; }

        private async Task LoadNeedsAsync()
        {

           //n.NeedTypeId — внешний ключ.

          //needTypes.FirstOrDefault(nt => nt.Id == n.NeedTypeId)?.Name — находим имя типа.

          //Если вдруг что - то пойдёт не так — будет "Other".

            GroupedFarmerNeeds.Clear();

            var farmersClass = await _db.Table<PopulationClass>().FirstOrDefaultAsync(p => p.Name == "Farmers");
            if (farmersClass == null) return;

            // Получаем все нужды фермеров
            var needs = await _db.Table<Need>()
                .Where(n => n.PopulationClassId == farmersClass.Id)
                .ToListAsync();

            // Получаем все типы нужд
            var needTypes = await _db.Table<NeedType>().ToListAsync();

            // Подключаем названия типов через Id
            var grouped = needs
                .GroupBy(n => needTypes.FirstOrDefault(nt => nt.Id == n.NeedTypeId)?.Name ?? "Other")
                .Select(g => new NeedGroup(g.Key, g))
                .ToList();

            foreach (var group in grouped)
                GroupedFarmerNeeds.Add(group);
        }
    }
}
