using MVC_TiendaOrdenadores.Service;
using MVC_TiendaOrdenadores.ViewComponents;
using Polly.Extensions.Http;
using Polly;
using MVC_TiendaOrdenadores.Consume_API;

namespace MVC_TiendaOrdenadores
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IComponenteRepository, ComponenteRepository>();
            builder.Services.AddScoped<IComponenteRepository, RepositoryComponenteApi>();
            //builder.Services.AddScoped<IOrdenadorRepository, OrdenadorRepository>();
            builder.Services.AddScoped<IOrdenadorRepository, RepositoryOrdenadorApi>();
            builder.Services.AddScoped<ISharedRepository, SharedRepository>();
            builder.Services.AddScoped<ComponenteViewComponent>();
            builder.Services.AddHttpClient("MyHttpClient")
               .AddPolicyHandler(GetCircuitBreakerPolicy());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Componente}/{action=Index}/{id?}");

            app.Run();
        }
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );
        }
    }
}