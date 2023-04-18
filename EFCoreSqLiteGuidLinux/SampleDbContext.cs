using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace EFCoreSqLiteGuidLinux
{
    public class SampleDbContext : DbContext
    {
        public DbSet<SampleEntity> SampleEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "SampleDatabase.db"
            };

            SqliteConnection connection = new SqliteConnection(connectionStringBuilder.ToString());

            optionsBuilder.UseSqlite(connection);
        }
    }
}
