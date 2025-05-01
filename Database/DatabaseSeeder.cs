using Anno1800.Models;
using Microsoft.Maui;
using SQLite;


namespace Anno1800.Database
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(SQLiteAsyncConnection connection)
        {
            // Создание таблиц, если ещё не созданы
            await connection.DropTableAsync<PopulationClass>();
            await connection.CreateTableAsync<PopulationClass>();
            await connection.DropTableAsync<NeedType>();
            await connection.CreateTableAsync<NeedType>();
            await connection.DropTableAsync<Need>();
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


            //ProductionOutputPerDay = 2880  // 2 тонны/мин * 60 мин/час * 24 часа
            //ProductionOutputPerDay = 1440  // 1 тонна/мин * 60 мин/час * 24 часа


            /* Work Clothes
             Производственная цепочка: Sheep Farm → Weaving Mill

             Время производства: каждая стадия по 30 секунд

             Выход продукции: 1 единица Work Clothes за 30 секунд

             Потребление: 1 единица на 24 минуты на жителя

             Рассчитанные значения:

            ConsumptionPerCapitaPer5Min = 0.2083(1 / 24 * 5)

             ProductionOutputPer5Min = 10(5 минут / 0.5 минуты на единицу)*/



            /* Schnapps
             Производственная цепочка: Potato Farm → Schnapps Distillery

             Время производства: каждая стадия по 30 секунд

            Выход продукции: 1 единица Schnapps за 30 секунд

            Потребление: 1 единица на 24 минуты на жителя

             Рассчитанные значения:

            ConsumptionPerCapitaPer5Min = 0.2083

            ProductionOutputPer5Min = 10 */


           /* Pub
             Здание: Pub

            Время обслуживания: 1 единица обслуживания за 60 секунд

             Потребление: 1 единица на 30 минут на жителя

             Рассчитанные значения:

            ConsumptionPerCapitaPer5Min = 0.1667(1 / 30 * 5)

            ProductionOutputPer5Min = 5(5 минут / 1 минута на единицу) */


            // Потребности
            var needs = new List<Need>
        {
            // Farmers
            new Need { Name = "Fish", PopulationClassId = farmers.Id, NeedTypeId = basic.Id , ConsumptionPerCapita = 0.0004166667 , ConsumptionRate =  0.0004166667 , IncomeModifier =  1.25, ProductionOutputPerDay = 2880,  UnlockCondition = "Доступно сразу", IconPath = "fish.png", ConsumptionPerCapitaPer5Min = 0.2083,  ProductionOutputPer5Min = 10},
            new Need { Name = "Work Clothes", PopulationClassId = farmers.Id, NeedTypeId = basic.Id , ConsumptionPerCapita = 0.000512821 ,ConsumptionRate =  0.000512821 , IncomeModifier =  3.75,  ProductionOutputPerDay = 2880, UnlockCondition = "Доступно сразу", IconPath = "work_clothes.png",ConsumptionPerCapitaPer5Min = 0.2083, ProductionOutputPer5Min = 10 },
            new Need { Name = "Schnapps", PopulationClassId = farmers.Id, NeedTypeId = luxury.Id , ConsumptionPerCapita = 0.000555556 ,ConsumptionRate = 0.000555556 , IncomeModifier =  3.75,  ProductionOutputPerDay = 2880, UnlockCondition = "Доступно сразу", IconPath = "schnapps.png" , ConsumptionPerCapitaPer5Min = 0.2083 , ProductionOutputPer5Min = 10},
            new Need { Name = "Pub", PopulationClassId = farmers.Id, ProductionOutputPerDay = 1440, NeedTypeId = luxury.Id,ConsumptionPerCapitaPer5Min = 0.1667, ProductionOutputPer5Min = 5},

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