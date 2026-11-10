using System.Net.Http.Json;

namespace MOERAppMovil.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool Exito, string Mensaje)> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new { Email = email, Contrasenia = password };
                var response = await _httpClient.PostAsJsonAsync("api/Usuarios/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        await SecureStorage.Default.SetAsync("jwt_token", result.Token);

                        // Validamos estrictamente que no haya strings vacíos ("") porque Windows los rechaza
                        string nombreSeguro = string.IsNullOrWhiteSpace(result.Nombre) ? "Usuario" : result.Nombre;
                        await SecureStorage.Default.SetAsync("user_name", nombreSeguro);

                        string rolSeguro = string.IsNullOrWhiteSpace(result.Rol) ? "Cliente" : result.Rol;
                        await SecureStorage.Default.SetAsync("user_role", rolSeguro);

                        // Al convertirse a string, un número siempre tiene longitud (ej. "0" o "5"), por lo que es seguro
                        await SecureStorage.Default.SetAsync("user_id", result.IdCliente.ToString());

                        // Usamos la variable 'email' del parámetro del método si el backend lo devuelve vacío
                        string emailSeguro = string.IsNullOrWhiteSpace(result.Email) ? email : result.Email;
                        await SecureStorage.Default.SetAsync("user_email", emailSeguro);

                        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

                        return (true, "Inicio de sesión exitoso");
                    }
                }
                else
                {
                    var errorResult = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    return (false, errorResult?.Mensaje ?? "Error en las credenciales.");
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
        public int IdCliente { get; set; } // Adaptado al backend
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

    public class ErrorResponse
    {
        public string Mensaje { get; set; } = string.Empty;
    }
}