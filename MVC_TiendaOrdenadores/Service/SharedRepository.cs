using MVC_TiendaOrdenadores.Models;

namespace MVC_TiendaOrdenadores.Service
{
    public class SharedRepository : ISharedRepository
    {
        private readonly TiendaDbContext _dbContext;
        private readonly DesingContextFactoryTienda _factoriaComponents = new();

        public SharedRepository()
        {
            string[] args = new string[1];
            _dbContext = _factoriaComponents.CreateDbContext(args);
        }
        public ComponenteViewModel? GetComponente(int id)
        {
            Componente comp = _dbContext.Componente.Find(id)!;

            if (comp != null)
            {
                EnumComponentes tipoEnum = (EnumComponentes)Enum.Parse(typeof(EnumComponentes), comp.Tipo.ToString());

                ComponenteViewModel componentViewModel = new()
                {
                    Id = comp.Id,
                    Serie = comp.Serie,
                    Descripcion = comp.Descripcion,
                    Calor = comp.Calor,
                    Megas = comp.Megas,
                    Cores = comp.Cores,
                    Coste = comp.Coste,
                    OrdenadorId = comp.OrdenadorId,
                    Tipo = tipoEnum,
                    Ordenador = comp.OrdenadorId != null ? _dbContext.Ordenador.Find(comp.OrdenadorId) : null
                };

                return componentViewModel;
            }

            return null;
        }



    }
}
