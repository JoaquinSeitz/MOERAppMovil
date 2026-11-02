using System.Net.Http.Json;

namespace TrabajoPracticoProgramacionMovilN6.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Ahora devuelve una tupla: (bool EsExitoso, string Mensaje)
        public async Task<(bool Exito, string Mensaje)> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new { Email = email, Password = password };
                var response = await _httpClient.PostAsJsonAsync("api/Usuarios/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        await SecureStorage.Default.SetAsync("jwt_token", result.Token);
                        await SecureStorage.Default.SetAsync("user_name", result.Nombre);
                        await SecureStorage.Default.SetAsync("user_role", result.Rol);
                        await SecureStorage.Default.SetAsync("user_id", result.UsuarioId.ToString());
                        await SecureStorage.Default.SetAsync("user_email", result.Email);

                        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

                        return (true, "Inicio de sesión exitoso");
                    }
                }
                else
                {
                    // Si hay error (400 o 401), leemos el mensaje específico que manda tu API
                    var errorResult = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    return (false, errorResult?.Mensaje ?? "Error desconocido en el servidor.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error de conexión: {ex.Message}");
            }

            return (false, "Error inesperado.");
        }

        public void Logout()
        {
            SecureStorage.Default.Remove("jwt_token");
            SecureStorage.Default.Remove("user_name");
            SecureStorage.Default.Remove("user_role");
            SecureStorage.Default.Remove("user_id");
            SecureStorage.Default.Remove("user_email");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

    // Clase para capturar los mensajes de error del backend
    public class ErrorResponse
    {
        public string Mensaje { get; set; } = string.Empty;
    }
}