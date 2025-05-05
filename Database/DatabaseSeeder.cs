using Anno1800.Models;
using SQLite;

namespace Anno1800.Database
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(SQLiteAsyncConnection database)
        {
            await database.DropTableAsync<Need>();
            await database.CreateTableAsync<Need>();
            await database.DropTableAsync<SubNeed>();
            await database.CreateTableAsync<SubNeed>();

            var needs = new List<Need>
        {
            new Need { Name = "Рынок", IconPath = "market.png", UnlockAtPopulation = 0, PopulationIncrease = 5 },
            new Need { Name = "Рыба", IconPath = "fish.png", UnlockAtPopulation = 50, PopulationIncrease = 3 },
            new Need { Name = "Рабочая одежда", IconPath = "work_clothes.png", UnlockAtPopulation = 100, PopulationIncrease = 2 },
            new Need { Name = "Шнапс", IconPath = "schnapps.png", UnlockAtPopulation = 100 },
            new Need { Name = "Паб", IconPath = "pub.png", UnlockAtPopulation = 150 }
        };

            // Очищаем и заполняем заново
            await database.DeleteAllAsync<Need>();
            await database.InsertAllAsync(needs);

            var insertedNeeds = await database.Table<Need>().ToListAsync();

            var subNeeds = new List<SubNeed>();

            var fish = insertedNeeds.FirstOrDefault(n => n.Name == "Рыба");
            if (fish != null)
            {
                subNeeds.Add(new SubNeed
                {
                    ParentNeedId = fish.Id,
                    Name = "Рыболовецкая гавань",
                    IconPath = "fishing_harbour.png"
                });
            }

            var clothes = insertedNeeds.FirstOrDefault(n => n.Name == "Рабочая одежда");
            if (clothes != null)
            {
                subNeeds.AddRange(new[]
                {
                new SubNeed
                {
                    ParentNeedId = clothes.Id,
                    Name = "Овцеферма",
                    IconPath = "sheep_farm.png"
                },
                new SubNeed
                {
                    ParentNeedId = clothes.Id,
                    Name = "Ткацкая мастерская",
                    IconPath = "framework_knitters.png"
                }
            });
            }

            var schnapps = insertedNeeds.FirstOrDefault(n => n.Name == "Шнапс");
            if (schnapps != null)
            {
                subNeeds.AddRange(new[]
                {
                new SubNeed
                {
                    ParentNeedId = schnapps.Id,
                    Name = "Картофельное поле",
                    IconPath = "potato_farm.png"
                },
                new SubNeed
                {
                    ParentNeedId = schnapps.Id,
                    Name = "Завод шнапса",
                    IconPath = "schnapps_distillery.png"
                }
            });
            }

            await database.DeleteAllAsync<SubNeed>();
            await database.InsertAllAsync(subNeeds);
        }
    }

}