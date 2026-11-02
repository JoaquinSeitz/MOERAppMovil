using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace TrabajoPracticoProgramacionMovilN6
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // Inyección obligatoria de MudBlazor
            builder.Services.AddMudServices();

            // --- CONFIGURACIÓN DEL HTTP CLIENT PARA PRODUCCIÓN ---
            // Asignamos directamente la URL de MonsterASP para todas las plataformas
            string urlApi = "https://tp5-joaco-alex.runasp.net/";

            // 1. Registramos el interceptor global de errores
            builder.Services.AddScoped<Services.GlobalHttpInterceptor>();

            // 2. Configuramos el HttpClient para que use el interceptor
            builder.Services.AddScoped(sp =>
            {
                // Al tener un SSL real, quitamos el bypass (ServerCertificateCustomValidationCallback)
                var handler = new HttpClientHandler();

                // Enganchamos el interceptor al handler base
                var interceptor = sp.GetRequiredService<Services.GlobalHttpInterceptor>();
                interceptor.InnerHandler = handler;

                return new HttpClient(interceptor)
                {
                    BaseAddress = new Uri(urlApi)
                };
            });

            // Habilitar la seguridad y autorización en Blazor
            builder.Services.AddAuthorizationCore();

            // Inyectar nuestros servicios personalizados
            builder.Services.AddScoped<Services.AuthService>();
            builder.Services.AddScoped<Services.CarritoService>();
            builder.Services.AddScoped<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider, Services.CustomAuthStateProvider>();

            return builder.Build();
        }
    }
}