using Microsoft.EntityFrameworkCore;
using MVC_TiendaOrdenadores.Models;

namespace MVC_TiendaOrdenadores.Models
{
    public class TiendaDbContext: DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options) { }
        public virtual DbSet<Componente> Componente { get; set; }
        public virtual DbSet<Ordenador> Ordenador { get; set; }
    }
}
