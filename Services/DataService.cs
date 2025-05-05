using Anno1800.Database;
using Anno1800.Models;
using SQLite;

namespace Anno1800.Services;

public class DataService
{
    private readonly ISQLiteAsyncConnection _database;


    public DataService(SqliteConnectionFactory sqliteConnectionFactory)
    {
        _database = sqliteConnectionFactory.Connect(); ;
    }

    // Получить все потребности
    public async Task<List<Need>> GetAllNeedsAsync()
    {
        return await _database.Table<Need>().ToListAsync();
    }

    // Получить потребности, которые открываются при населении <= заданного значения
    public async Task<List<Need>> GetNeedsByPopulationAsync(int maxPopulation)
    {
        return await _database.Table<Need>()
            .Where(n => n.UnlockAtPopulation <= maxPopulation)
            .ToListAsync();
    }

    // Получить SubNeeds по ID родительской потребности
    public async Task<List<SubNeed>> GetSubNeedsByParentIdAsync(int parentNeedId)
    {
        return await _database.Table<SubNeed>()
            .Where(sn => sn.ParentNeedId == parentNeedId)
            .ToListAsync();
    }

    // Получить все Need с их SubNeed
    public async Task<List<NeedWithSubNeeds>> GetNeedsWithSubNeedsAsync(int maxPopulation = int.MaxValue)
    {
        var needs = await GetNeedsByPopulationAsync(maxPopulation);
        var result = new List<NeedWithSubNeeds>();

        foreach (var need in needs)
        {
            var subNeeds = await GetSubNeedsByParentIdAsync(need.Id);
            result.Add(new NeedWithSubNeeds
            {
                Need = need,
                SubNeeds = subNeeds
            });
        }

        return result;
    }
}