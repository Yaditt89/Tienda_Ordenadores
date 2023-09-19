using MVC_TiendaOrdenadores.Models;
using MVC_TiendaOrdenadores.Service;
using Newtonsoft.Json;
using System.Text;

namespace MVC_TiendaOrdenadores.Consume_API
{
    public class RepositoryComponenteApi : IComponenteRepository
    {
        private readonly HttpClient _httpClient;

        public RepositoryComponenteApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyHttpClient");
        }

        public void AddComponents(ComponenteViewModel? compViewModel)
        {
            if (compViewModel != null)
            {
                var componente = ConvertirToComponente(compViewModel);
                string url = "http://localhost:5115/api/Componente";
                var componenteJson = JsonConvert.SerializeObject(componente);
                var content = new StringContent(componenteJson, Encoding.UTF8, "application/json");
                _httpClient.PostAsync(url, content).Wait();
            }
        }

        public List<ComponenteViewModel> AllComponents()
        {
            string url = "http://localhost:5115/api/Componente";
            var callResponse = _httpClient.GetAsync(url).Result;
            var response = callResponse.Content.ReadAsStringAsync().Result;
            var lista = JsonConvert.DeserializeObject<List<Componente>>(response);
            List<ComponenteViewModel> listaComponentesViewModel = ConvertToComponenteViewModelList(lista!);
            if (lista == null) return new();
            return listaComponentesViewModel;
        }

        public void Delete(int id)
        {
            string url = $"http://localhost:5115/api/Componente/{id}";
            _httpClient.DeleteAsync(url).Wait();
        }

        public void Edit(ComponenteViewModel compViewModel)
        {
            if (compViewModel != null)
            {
            var componente = ConvertirToComponente(compViewModel);
            componente.Id = compViewModel.Id;
            string url = $"http://localhost:5115/api/Componente";
            var componenteJson = JsonConvert.SerializeObject(componente);
            var content = new StringContent(componenteJson, Encoding.UTF8, "application/json");
            _httpClient.PutAsync(url, content).Wait();
                }
        }

        public bool HayOrdenadoresEnSistema()
        {
            throw new NotImplementedException();
        }

        public ComponenteViewModel? GetComponente(int id)
        {
            string url = $"http://localhost:5115/api/Componente/{id}";
            var callResponse = _httpClient.GetAsync(url).Result;
            if (callResponse.IsSuccessStatusCode)
            {
                var response = callResponse.Content.ReadAsStringAsync().Result;
                var componente = JsonConvert.DeserializeObject<Componente>(response);
                return ConvertToComponenteViewModel(componente!);
            }
            else
            {
                return null;
            }
        }

        public List<Componente> GetComponentesPorTipo(EnumComponentes tipo)
        {
            string url = $"http://localhost:5115/api/Componente/tipo/{(int)tipo}";
            var callResponse = _httpClient.GetAsync(url).Result;

            if (callResponse.IsSuccessStatusCode)
            {
                var response = callResponse.Content.ReadAsStringAsync().Result;
                var lista = JsonConvert.DeserializeObject<List<Componente>>(response);
                return lista;
            }
            else
            {
                return new List<Componente>();
            }
        }

        public List<ComponenteViewModel> ConvertToComponenteViewModelList(List<Componente> componentes)
        {
            List<ComponenteViewModel> componentesViewModel = componentes.Select(c => ConvertToComponenteViewModel(c)).ToList();
            return componentesViewModel;
        }

        public ComponenteViewModel ConvertToComponenteViewModel(Componente componente)
        {
            return new ComponenteViewModel
            {
                Id = componente.Id,
                Serie = componente.Serie,
                Descripcion = componente.Descripcion,
                Calor = componente.Calor,
                Megas = componente.Megas,
                Cores = componente.Cores,
                Coste = componente.Coste,
                Tipo = (EnumComponentes)componente.Tipo,
                OrdenadorId = componente.OrdenadorId,
                Ordenador = componente.Ordenador
            };
        }

        public Componente ConvertirToComponente(ComponenteViewModel compViewModel)
        {
            return new Componente
            {
                Serie = compViewModel.Serie,
                Descripcion = compViewModel.Descripcion,
                Calor = compViewModel.Calor,
                Megas = compViewModel.Megas,
                Cores = compViewModel.Cores,
                Coste = compViewModel.Coste,
                Tipo = (int)compViewModel.Tipo,
                OrdenadorId = compViewModel.OrdenadorId
            };
        }

        
        }
    }

