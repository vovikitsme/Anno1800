using Anno1800.Database;
using Anno1800.Models;
using SQLite;

namespace Anno1800.Services
{
    public class DataService
    {
        private readonly ISQLiteAsyncConnection _db;

        public DataService(SqliteConnectionFactory sqliteConnectionFactory)
        {
            _db = sqliteConnectionFactory.Connect();
        }

        public async Task<List<NeedDisplayDto>> GetNeedsByPopulationClassAsync(string populationClassName)
        {
            var populationClass = await _db.Table<PopulationClass>()
                .FirstOrDefaultAsync(p => p.Name == populationClassName);

            if (populationClass == null)
                return new List<NeedDisplayDto>();

            var needs = await _db.Table<Need>()
                .Where(n => n.PopulationClassId == populationClass.Id)
                .ToListAsync();

            var needDtos = new List<NeedDisplayDto>();

            foreach (var need in needs)
            {
                var needType = await _db.FindAsync<NeedType>(need.NeedTypeId);

                needDtos.Add(new NeedDisplayDto
                {
                    Name = need.Name,
                    IconPath = need.IconPath,
                    NeedTypeName = needType?.Name ?? "?",
                    PopulationClassName = populationClass.Name,
                    ConsumptionRate = need.ConsumptionRate,
                    ProductionOutputPerDay = need.ProductionOutputPerDay,
                    ConsumptionPerCapitaPer5Min = need.ConsumptionPerCapitaPer5Min,
                    ProductionOutputPer5Min = need.ProductionOutputPer5Min,
                    IsPremium = need.IsPremium
                });
            }

            return needDtos;
        }
    }
}