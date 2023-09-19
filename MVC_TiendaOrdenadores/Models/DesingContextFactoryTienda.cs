using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MVC_TiendaOrdenadores.Models
{
    public class DesingContextFactoryTienda : IDesignTimeDbContextFactory<TiendaDbContext>
    {
        public TiendaDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory() + @"\App_Data");

            var dbContextBuilder = new DbContextOptionsBuilder<TiendaDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            dbContextBuilder.UseSqlServer(connectionString);
            return new TiendaDbContext(dbContextBuilder.Options);
        }
    }
}
