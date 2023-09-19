using Microsoft.EntityFrameworkCore;
using MVC_TiendaOrdenadores.Models;

namespace MVC_TiendaOrdenadores.Service
{
    public class OrdenadorRepository:IOrdenadorRepository
    {
        private readonly TiendaDbContext _dbContext;
        private readonly DesingContextFactoryTienda _factoriaComponents = new();
        private readonly IComponenteRepository _componenteRepository;

        public OrdenadorRepository(IComponenteRepository componenteRepository)
        {
            string[] args = new string[1];
            _dbContext = _factoriaComponents.CreateDbContext(args);
            _componenteRepository = componenteRepository;   
        }

        public void AddOrdenador(Ordenador? ordenador)
        {
            
            if (ordenador != null)
            {
                Ordenador orde = new()
                {
                    Name = ordenador.Name,
                };
                _dbContext.Ordenador.Add(orde);
                _dbContext.SaveChanges();

                int nuevoOrdenadorId = orde.Id;

                var componentesToUpdate = ordenador.ComponentesLIst!
                .Select(comp =>
                {
                    ComponenteViewModel componente = _componenteRepository.GetComponente(comp.Id)!;
                    if (componente != null)
                    {
                        componente.OrdenadorId = nuevoOrdenadorId;
                        return new Models.Componente
                        {
                            Id = componente.Id,
                            Serie = componente.Serie,
                            Descripcion = componente.Descripcion,
                            Calor = componente.Calor,
                            Megas = componente.Megas,
                            Cores = componente.Cores,
                            Coste = componente.Coste,
                            Tipo = (int)componente.Tipo,
                            OrdenadorId = nuevoOrdenadorId
                        };
                    }
                    return null;
                })
                .Where(componente => componente != null)
                .ToList();

                _dbContext.Componente.UpdateRange(componentesToUpdate!);
                _dbContext.SaveChanges();
            }
        }

        public List<OrdenadorViewModel> AllOrdenador()
        {
         var ordenadoresViewModel = _dbContext.Ordenador.Include(o => o.ComponentesLIst)
        .Select(ordenador => new OrdenadorViewModel
        {
            Id = ordenador.Id,
            Name = ordenador.Name,
            ComponentesLIst = ordenador.ComponentesLIst!
                .OrderBy(c => c.Tipo == (int)EnumComponentes.Procesador ? 1 : c.Tipo == (int)EnumComponentes.Memoria ? 2 : 3)
                .ToList(),
            Coste = ordenador.ComponentesLIst != null ? ordenador.ComponentesLIst.Sum(c => c.Coste) : null,
            Calor = ordenador.ComponentesLIst != null ? ordenador.ComponentesLIst.Sum(c => c.Calor) : null
        })
        .ToList();

            return ordenadoresViewModel;
        }

        public void Delete(int id)
        {
            Ordenador ordenador = _dbContext.Ordenador
                .Include(o => o.ComponentesLIst)
                .FirstOrDefault(o => o.Id == id)!;
            if (ordenador != null)
            {
                if (ordenador.ComponentesLIst != null)
                {
                    foreach (var componente in ordenador.ComponentesLIst)
                    {
                        componente.OrdenadorId = null;
                    }
                    _dbContext.Componente.UpdateRange(ordenador.ComponentesLIst);
                }

            
                _dbContext.Ordenador.Remove(ordenador);
                _dbContext.SaveChanges();
            }
        }

        public OrdenadorViewModel GetOrdenadorViewModel(int id)
        {
            Ordenador ordenador = _dbContext.Ordenador.Include(o => o.ComponentesLIst).FirstOrDefault(o => o.Id == id)!;

            if (ordenador == null)
            {
                return null!;
            }

            int? totalCoste = ordenador.ComponentesLIst?.Sum(c => c.Coste);
            int? totalCalor = ordenador.ComponentesLIst?.Sum(c => c.Calor);

            OrdenadorViewModel ordenadorViewModel = new()
            {
                Id = ordenador.Id,
                Name = ordenador.Name,
                ComponentesLIst = ordenador.ComponentesLIst!,
                Coste = totalCoste,
                Calor = totalCalor
            };
            ordenadorViewModel.ComponentesLIst = ordenadorViewModel.ComponentesLIst?
                .OrderBy(c =>
                {
                    if (c.Tipo != (int)EnumComponentes.Memoria)
                    {
                        return c.Tipo == (int)EnumComponentes.Procesador ? 1 : 3;
                    }
                    else
                    {
                        return c.Tipo == (int)EnumComponentes.Procesador ? 1 : 2;
                    }
                })
                .ToList();

            return ordenadorViewModel;
        }

        public void Edit(OrdenadorViewModel ordenador)
        {
          Ordenador compu = _dbContext.Ordenador.Include(o => o.ComponentesLIst).FirstOrDefault(o => o.Id == ordenador.Id)!;

            foreach (var componenteExistente in compu.ComponentesLIst!)
            {
                componenteExistente.OrdenadorId = null;
            }

            var componentIdsToUpdate = ordenador.ComponentesLIst!.Select(comp => comp.Id).ToList();
            var componentesActualizar = _dbContext.Componente.Where(c => componentIdsToUpdate.Contains(c.Id)).ToList();

            foreach (var componente in componentesActualizar)
            {
                componente.OrdenadorId = ordenador.Id;
            }

            _dbContext.Componente.UpdateRange(componentesActualizar);

            compu.Name = ordenador.Name;

            _dbContext.SaveChanges();

        }


        public List<Models.Componente> GetComponentesPorTipo(EnumComponentes tipo)
        {
            return _dbContext.Componente.Where(c => c.Tipo == (int)tipo && c.OrdenadorId == null).ToList();
        }

        public bool TieneComponentesDeCadaTipo(Ordenador ordenador) => ordenador.ComponentesLIst != null
                   && Enumerable.Any(ordenador.ComponentesLIst, c => c.Tipo == (int)EnumComponentes.Procesador)
                   && Enumerable.Any(ordenador.ComponentesLIst, c => c.Tipo == (int)EnumComponentes.Memoria)
                   && Enumerable.Any(ordenador.ComponentesLIst, c => c.Tipo == (int)EnumComponentes.DiscoDuro);
    }


}
