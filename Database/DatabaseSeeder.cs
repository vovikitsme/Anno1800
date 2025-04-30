using Anno1800.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800.Database
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(SQLiteAsyncConnection connection)
        {
            // Создание таблиц, если ещё не созданы
            await connection.CreateTableAsync<PopulationClass>();
            await connection.CreateTableAsync<NeedType>();
            await connection.CreateTableAsync<Need>();

            // Проверим, есть ли уже данные
            var hasClasses = await connection.Table<PopulationClass>().CountAsync() > 0;
            if (hasClasses) return;

            // Классы населения
            var farmers = new PopulationClass { Name = "Farmers" };
            var workers = new PopulationClass { Name = "Workers" };

            await connection.InsertAllAsync(new[] { farmers, workers });

            // Типы потребностей
            var basic = new NeedType { Name = "Basic" };
            var luxury = new NeedType { Name = "Luxury" };

            await connection.InsertAllAsync(new[] { basic, luxury });

            // Повторно получаем классы и типы с Id
            farmers = await connection.Table<PopulationClass>().Where(p => p.Name == "Farmers").FirstAsync();
            workers = await connection.Table<PopulationClass>().Where(p => p.Name == "Workers").FirstAsync();
            basic = await connection.Table<NeedType>().Where(n => n.Name == "Basic").FirstAsync();
            luxury = await connection.Table<NeedType>().Where(n => n.Name == "Luxury").FirstAsync();

            // Потребности
            var needs = new List<Need>
        {
            // Farmers
            new Need { Name = "Fish", PopulationClassId = farmers.Id, NeedTypeId = basic.Id },
            new Need { Name = "Work Clothes", PopulationClassId = farmers.Id, NeedTypeId = basic.Id },
            new Need { Name = "Schnapps", PopulationClassId = farmers.Id, NeedTypeId = luxury.Id },
            new Need { Name = "Pub", PopulationClassId = farmers.Id, NeedTypeId = luxury.Id },

            // Workers
            new Need { Name = "Sausages", PopulationClassId = workers.Id, NeedTypeId = basic.Id },
            new Need { Name = "Bread", PopulationClassId = workers.Id, NeedTypeId = basic.Id },
            new Need { Name = "Soap", PopulationClassId = workers.Id, NeedTypeId = basic.Id },
            new Need { Name = "Beer", PopulationClassId = workers.Id, NeedTypeId = luxury.Id },
            new Need { Name = "Church", PopulationClassId = workers.Id, NeedTypeId = luxury.Id },
        };

            await connection.InsertAllAsync(needs);
        }
    }
}
