using Microsoft.EntityFrameworkCore;

using MVC_TiendaOrdenadores.Models;
using TiendaOrdenadoresA.Componentes;
using TiendaOrdenadoresA.Componentes.Builder;

namespace MVC_TiendaOrdenadores.Service
{
    public class FakeRepositorioComponentes : IComponenteRepository
    {

        readonly List<ComponenteViewModel> componentesList = new();

        public FakeRepositorioComponentes()
        {
            var miBuilder = new BuilderComponente();

            var procesador = miBuilder.DameComponente(EnumComponente.ProcesadorInteli7_789_XCT);
            var discoDuro = miBuilder.DameComponente(EnumComponente.DiscoDuroSanDisk_789_XX);
            var memoria = miBuilder.DameComponente(EnumComponente.BancoDeMemoriaSDRAM_879FH);

            componentesList.Add(ConvertirAViewModel(procesador!, 1));
            componentesList.Add(ConvertirAViewModel(discoDuro!,2));
            componentesList.Add(ConvertirAViewModel(memoria!,3));


        }
        private static ComponenteViewModel ConvertirAViewModel(TiendaOrdenadoresA.Componentes.Componente componente, int id)
        {
            return new ComponenteViewModel
            {
                Id = id,
                Serie = componente.NumeroSerie,
                Descripcion = componente.Descripcion,
                Calor = componente.Calor,
                Megas = componente.Megas,
                Cores = componente.Cores,
                Coste = (int)componente.Coste,
                Tipo = (EnumComponentes)componente.TipoComponente,
            };
        }
        public void AddComponents(ComponenteViewModel? comp)
        {
            componentesList.Add(comp!);
        }

        public List<ComponenteViewModel> AllComponents()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            ComponenteViewModel? componentToRemove = componentesList.FirstOrDefault(comp => comp.Id == id);
            if (componentToRemove != null)
            {
                componentesList.Remove(componentToRemove);
            }
        }

        public void Edit(ComponenteViewModel comp)
        {
            var existingComp = componentesList.FirstOrDefault(compo => compo.Id == comp.Id);
            if (existingComp != null)
            {
                existingComp.Serie = comp.Serie;
                existingComp.Descripcion = comp.Descripcion;
                existingComp.Calor = comp.Calor;
                existingComp.Megas = comp.Megas;
                existingComp.Cores = comp.Cores;
                existingComp.Coste = comp.Coste;
                existingComp.Tipo = comp.Tipo;
            }
        }

        public ComponenteViewModel? GetComponente(int id)
        {
            throw new NotImplementedException();
        }

        public bool HayOrdenadoresEnSistema()
        {
            throw new NotImplementedException();
        }

    }
}
