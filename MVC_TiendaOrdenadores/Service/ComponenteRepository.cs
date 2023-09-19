using MVC_TiendaOrdenadores.Models;

namespace MVC_TiendaOrdenadores.Service
{
    public class ComponenteRepository : IComponenteRepository
    {


        private readonly TiendaDbContext _dbContext;
        private readonly DesingContextFactoryTienda _factoriaComponents = new();

        public ComponenteRepository()
        {
            string[] args = new string[1];
            _dbContext = _factoriaComponents.CreateDbContext(args);
        }
        public List<ComponenteViewModel> AllComponents()
        {
            List<ComponenteViewModel> listaComponentes = _dbContext.Componente
                .Select(comp => new ComponenteViewModel
                {
                    Id = comp.Id,
                    Serie = comp.Serie,
                    Descripcion = comp.Descripcion,
                    Calor = comp.Calor,
                    Megas = comp.Megas,
                    Cores = comp.Cores,
                    Coste = comp.Coste,
                    OrdenadorId = comp.OrdenadorId,
                    Tipo = (EnumComponentes)comp.Tipo,
                    Ordenador = comp.OrdenadorId != null ? _dbContext.Ordenador.Find(comp.OrdenadorId) : null
                })
                .ToList();
            return listaComponentes;
        }

        public bool HayOrdenadoresEnSistema()
        {
            return _dbContext.Ordenador.Any();
        }

        public void AddComponents(ComponenteViewModel? comp)
        {
            if (comp != null)
            {
                Componente componente = new()
                {
                    Serie = comp.Serie,
                    Descripcion = comp.Descripcion,
                    Calor = comp.Calor,
                    Megas = comp.Megas,
                    Cores = comp.Cores,
                    Coste = comp.Coste,
                    Tipo = (int)comp.Tipo,
                    OrdenadorId = comp.OrdenadorId,
                };
                _dbContext.Componente.Add(componente);
                _dbContext.SaveChanges();
            }
        }

        public void Edit(ComponenteViewModel? comp)
        {
            if (comp != null)
            {
                Componente componente = _dbContext.Componente.FirstOrDefault(c => c.Id == comp.Id)!;
                if (componente != null)
                {
                    componente.Serie = comp.Serie;
                    componente.Descripcion = comp.Descripcion;
                    componente.Calor = comp.Calor;
                    componente.Megas = comp.Megas;
                    componente.Cores = comp.Cores;
                    componente.Coste = comp.Coste;
                    componente.Tipo = (int)comp.Tipo;
                    componente.OrdenadorId = comp.OrdenadorId;

                    _dbContext.Componente.Update(componente);
                    _dbContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            Componente componente = _dbContext.Componente.FirstOrDefault(c => c.Id == id)!;
            if (componente != null)
            {
                _dbContext.Componente.Remove(componente);
                _dbContext.SaveChanges();
            }
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
