using MVC_TiendaOrdenadores.Models;
using MVC_TiendaOrdenadores.Service;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MVC_TiendaOrdenadores.Consume_API
{
    public class RepositoryOrdenadorApi : IOrdenadorRepository
    {

        private readonly HttpClient _httpClient;
        private readonly RepositoryComponenteApi _componenteRepositoryApi;

        public RepositoryOrdenadorApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyHttpClient");
            _componenteRepositoryApi = new RepositoryComponenteApi(httpClientFactory);
        }
        public void AddOrdenador(Ordenador? ordenador)
        {
            if (ordenador != null)
            {
                string url = "http://localhost:5115/api/Ordenador";
                var ordenadorJson = JsonConvert.SerializeObject(ordenador);
                var content = new StringContent(ordenadorJson, Encoding.UTF8, "application/json");
                _httpClient.PostAsync(url, content).Wait();
            }
        }

        public List<OrdenadorViewModel> AllOrdenador()
        {
            string url = "http://localhost:5115/api/Ordenador";
            var callResponse = _httpClient.GetAsync(url).Result;
            var response = callResponse.Content.ReadAsStringAsync().Result;
            var listaOrdenadores = JsonConvert.DeserializeObject<List<Ordenador>>(response);

            if (listaOrdenadores == null)
            {
                return new List<OrdenadorViewModel>();
            }

            var listaOrdenadoresViewModel = new List<OrdenadorViewModel>();

            foreach (var ordenador in listaOrdenadores)
            {
                var ordenadorViewModel = new OrdenadorViewModel
                {
                    Id = ordenador.Id,
                    Name = ordenador.Name,
                    ComponentesLIst = ordenador.ComponentesLIst
                };

                if (ordenador.ComponentesLIst != null)
                {
                    ordenadorViewModel.Coste = ordenador.ComponentesLIst.Sum(c => c.Coste);
                }

                if (ordenador.ComponentesLIst != null)
                {
                    ordenadorViewModel.Calor = ordenador.ComponentesLIst.Sum(c => c.Calor);
                }

                listaOrdenadoresViewModel.Add(ordenadorViewModel);
            }

            return listaOrdenadoresViewModel;
        }

        public void Delete(int id)
        {
            string url = $"http://localhost:5115/api/Ordenador/{id}";
            _httpClient.DeleteAsync(url).Wait();
        }

        public void Edit(OrdenadorViewModel ordenadorViewModel)
        {
            if (ordenadorViewModel != null)
            {
                var ordenador = new Ordenador
                {
                    Id = ordenadorViewModel.Id,
                    Name = ordenadorViewModel.Name,
                    ComponentesLIst = ordenadorViewModel.ComponentesLIst
                };

                string url = $"http://localhost:5115/api/Ordenador";
                var ordenadorJson = JsonConvert.SerializeObject(ordenador);
                var content = new StringContent(ordenadorJson, Encoding.UTF8, "application/json");
                _httpClient.PutAsync(url, content).Wait();
            }
        }

        
            public List<Componente> GetComponentesPorTipo(EnumComponentes tipo)
            {
            List<Componente> lista = _componenteRepositoryApi.GetComponentesPorTipo(tipo);
            return lista;
        }
        

        public OrdenadorViewModel GetOrdenadorViewModel(int id)
        {
            string url = $"http://localhost:5115/api/Ordenador/{id}";
            var callResponse = _httpClient.GetAsync(url).Result;
            if (callResponse.IsSuccessStatusCode)
            {
                var response = callResponse.Content.ReadAsStringAsync().Result;
                var ordenador = JsonConvert.DeserializeObject<Ordenador>(response);

                if (ordenador != null)
                {
                    var ordenadorViewModel = new OrdenadorViewModel
                    {
                        Id = ordenador.Id,
                        Name = ordenador.Name,
                        ComponentesLIst = ordenador.ComponentesLIst
                    };

                    if (ordenador.ComponentesLIst != null)
                    {
                        ordenadorViewModel.Coste = ordenador.ComponentesLIst.Sum(c => c.Coste);
                        ordenadorViewModel.Calor = ordenador.ComponentesLIst.Sum(c => c.Calor);
                    }

                    return ordenadorViewModel;
                }
            }

            return new OrdenadorViewModel();
        }
    }
}
