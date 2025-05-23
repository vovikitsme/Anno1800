﻿using Anno1800.Database;
using SQLite;

namespace Anno1800
{
    public partial class App : Application
    {
        private readonly SqliteConnectionFactory _sqliteConnectionFactory;

        public App(SqliteConnectionFactory sqliteConnectionFactory)
        {
            InitializeComponent();

            MainPage = new AppShell();

            _sqliteConnectionFactory = sqliteConnectionFactory;
        }

        protected override async void OnStart()
        {
            try
            {
                SQLiteAsyncConnection connection = _sqliteConnectionFactory.Connect();
                await DatabaseSeeder.SeedAsync(connection);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to initialize database.", "Ok");
            }

            base.OnStart();
        }
    }
}