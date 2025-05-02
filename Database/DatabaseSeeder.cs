using Anno1800.Models;
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


            #region Объяснения по переменным

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

            #endregion

            // Потребности

            var needs = new List<Need>
        {
            // Farmers
            new Need { Name = "Жильё фермеров", PopulationClassId = farmers.Id, NeedTypeId = basic.Id , IconPath = "farmer_residence.png", ConsumptionPerCapitaPer5Min = 0.2083,  ProductionOutputPer5Min = 1, GainsInhabitants = 0 ,IsPremium = false},
             new Need { Name = "Рынок", PopulationClassId = farmers.Id, NeedTypeId = basic.Id , IconPath = "marketplace.png", ConsumptionPerCapitaPer5Min = 0.2083,  ProductionOutputPer5Min = 1, GainsInhabitants = 5, IsPremium = false},
            new Need { Name = "Рыб.гавань", PopulationClassId = farmers.Id, NeedTypeId = basic.Id , IconPath = "fish.png", ConsumptionPerCapitaPer5Min = 0.2083,  ProductionOutputPer5Min = 1,IsPremium = false},
            new Need { Name = "Овцеферма", PopulationClassId = farmers.Id, NeedTypeId = basic.Id , IconPath = "wool.png",ConsumptionPerCapitaPer5Min = 0.2083, ProductionOutputPer5Min = 1,IsPremium = false },
            new Need { Name = "Ткацкий завод", PopulationClassId = farmers.Id, NeedTypeId = basic.Id , IconPath = "work_clothes.png",ConsumptionPerCapitaPer5Min = 0.2083, ProductionOutputPer5Min = 1,IsPremium = false },
            new Need { Name = "Картофельное поле", PopulationClassId = farmers.Id, NeedTypeId = luxury.Id , IconPath = "potato.png" , ConsumptionPerCapitaPer5Min = 0.2083 , ProductionOutputPer5Min = 1, IsPremium = true},
             new Need { Name = "Шнапс завод", PopulationClassId = farmers.Id, NeedTypeId = luxury.Id , IconPath = "schnapps.png" , ConsumptionPerCapitaPer5Min = 0.2083 , ProductionOutputPer5Min = 1, IsPremium = true},
            new Need { Name = "Паб", PopulationClassId = farmers.Id, NeedTypeId = luxury.Id, IconPath = "pub.png" ,ConsumptionPerCapitaPer5Min = 0.1667, ProductionOutputPer5Min = 1, IsPremium = true},


            // Workersвавававава
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