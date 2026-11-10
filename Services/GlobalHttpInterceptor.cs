using System.Net;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MOERAppMovil.Services
{
    public class GlobalHttpInterceptor : DelegatingHandler
    {
        private readonly ISnackbar _snackbar;
        private readonly NavigationManager _navigationManager;

        public GlobalHttpInterceptor(ISnackbar snackbar, NavigationManager navigationManager)
        {
            _snackbar = snackbar;
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. PRIMERO inyectamos el token ANTES de enviar la petición
                var token = await SecureStorage.Default.GetAsync("jwt_token");
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                // 2. DESPUÉS dejamos que la petición viaje al backend
                var response = await base.SendAsync(request, cancellationToken);

                // Verificamos si la petición fue dirigida a los endpoints de autenticación
                bool esEndpointDeAuth = request.RequestUri != null &&
                                        (request.RequestUri.AbsolutePath.Contains("/login", StringComparison.OrdinalIgnoreCase) ||
                                         request.RequestUri.AbsolutePath.Contains("/registro", StringComparison.OrdinalIgnoreCase));

                // 3. Si hay error Y NO es una petición de Auth, mostramos los mensajes globales
                if (!response.IsSuccessStatusCode && !esEndpointDeAuth)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized: // 401
                            _snackbar.Add("Sesión expirada o inválida. Volvé a ingresar.", Severity.Error);
                            _navigationManager.NavigateTo("/logout");
                            break;
                        case HttpStatusCode.Forbidden: // 403
                            _snackbar.Add("No tenés permisos para esta acción.", Severity.Warning);
                            break;
                        case HttpStatusCode.BadRequest: // 400
                            _snackbar.Add("Los datos enviados no son válidos.", Severity.Warning);
                            break;
                        case HttpStatusCode.InternalServerError: // 500
                            _snackbar.Add("Error interno en el servidor.", Severity.Error);
                            break;
                        case HttpStatusCode.NotFound: // 404
                            _snackbar.Add("El recurso o endpoint no existe.", Severity.Info);
                            break;
                    }
                }

                return response;
            }
            catch (TaskCanceledException)
            {
                // Atrapamos la cancelación generada al cambiar de página o cerrar sesión abruptamente
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (HttpRequestException)
            {
                // Si el emulador no tiene internet o tu API en localhost está apagada
                _snackbar.Add("Error de red: No se pudo conectar con el servidor.", Severity.Error);
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}