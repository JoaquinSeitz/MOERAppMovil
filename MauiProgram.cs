using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using MOERAppMovil;

namespace MOERAppMovil
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
            builder.Services.AddMudServices();

            // 1. Registramos el interceptor global
            builder.Services.AddScoped<Services.GlobalHttpInterceptor>();

            // 2. Configuración del HttpClient dinámico (Android vs Windows) con Bypass SSL
            builder.Services.AddScoped(sp =>
            {
                var handler = new HttpClientHandler();

                // BYPASS SSL OBLIGATORIO PARA DESARROLLO LOCAL EN MAUI
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                var interceptor = sp.GetRequiredService<Services.GlobalHttpInterceptor>();
                interceptor.InnerHandler = handler;

                // 10.0.2.2 es el localhost del emulador de Android
                string urlApi = DeviceInfo.Platform == DevicePlatform.Android
                                ? "https://10.0.2.2:7133/"
                                : "https://localhost:7133/";

                return new HttpClient(interceptor)
                {
                    BaseAddress = new Uri(urlApi)
                };
            });

            builder.Services.AddAuthorizationCore();

            // Inyectar servicios principales
            builder.Services.AddScoped<Services.AuthService>();
            builder.Services.AddScoped<Services.CarritoService>();

            // Añade aquí los servicios que copiaste de la web
            builder.Services.AddScoped<Services.CatalogoService>();
            builder.Services.AddScoped<Services.TurnoService>();
            builder.Services.AddScoped<Services.PedidoService>();

            builder.Services.AddScoped<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider, Services.CustomAuthStateProvider>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}