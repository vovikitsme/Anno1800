using Anno1800.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace Anno1800.Views
{
    public partial class NeedsViewModel : BaseViewModel
    {
        private readonly ISQLiteAsyncConnection _connection;

        public ObservableCollection<NeedDisplayDto> Needs { get; } = new();
        public ObservableCollection<PopulationClass> PopulationClasses { get; } = new();

        private PopulationClass _selectedPopulationClass;

        public PopulationClass SelectedPopulationClass
        {
            get => _selectedPopulationClass;
            set
            {
                if (SetProperty(ref _selectedPopulationClass, value))
                {
                    _ = LoadNeedsAsync();
                }
            }
        }

        public NeedsViewModel(ISQLiteAsyncConnection connection)
        {
            _connection = connection;
            _ = LoadPopulationClassesAsync();
        }

        private async Task LoadPopulationClassesAsync()
        {
            var classes = await _connection.Table<PopulationClass>().ToListAsync();
            PopulationClasses.Clear();
            foreach (var c in classes)
                PopulationClasses.Add(c);

            if (PopulationClasses.Any())
                SelectedPopulationClass = PopulationClasses.First();
        }

        public async Task LoadNeedsAsync()
        {
            Needs.Clear();

            // Загружаем все потребности, типы и классы населения заранее
            var allNeeds = await _connection.Table<Need>().ToListAsync();
            var allTypes = await _connection.Table<NeedType>().ToListAsync();
            var allPopulations = await _connection.Table<PopulationClass>().ToListAsync();

            // Преобразуем в словари для быстрого доступа по ID
            var typeDict = allTypes.ToDictionary(t => t.Id, t => t.Name);
            var populationDict = allPopulations.ToDictionary(p => p.Id, p => p.Name);

            foreach (var need in allNeeds)
            {
                var typeName = typeDict.TryGetValue(need.NeedTypeId, out var tn) ? tn : "?";
                var populationName = populationDict.TryGetValue(need.PopulationClassId, out var pn) ? pn : "?";

                Needs.Add(new NeedDisplayDto
                {
                    Name = need.Name,
                    NeedTypeName = typeName,
                    PopulationClassName = populationName
                });
            }
        }
    }
}