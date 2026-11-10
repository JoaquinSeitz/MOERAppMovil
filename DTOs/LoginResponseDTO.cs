using System.Text.Json.Serialization;

namespace MOERAppMovil.DTOs;

public class LoginResponseDTO
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("rol")]
    public string Rol { get; set; } = string.Empty;
}