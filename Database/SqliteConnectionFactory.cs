using SQLite;

namespace Anno1800.Database
{
    public class SqliteConnectionFactory
    {
        public SQLiteAsyncConnection Connect()
        {
            // SQLiteOpenFlags.SharedCache - enable multi-threaded database access
            return new SQLiteAsyncConnection(Path.Combine(FileSystem.Current.AppDataDirectory, "anno1800.db3"),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        }
    }
}