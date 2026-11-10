using System.Net.Http.Json;
using MOERAppMovil.DTOs;

// ¡Esta es la línea clave que elimina el error CS0234!
namespace MOERAppMovil.Services;

public class CatalogoService
{
    private readonly HttpClient _http;

    public CatalogoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ServicioResponseDTO>> GetServiciosAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<List<ServicioResponseDTO>>("api/servicios") ?? new List<ServicioResponseDTO>();
        }
        catch
        {
            return new List<ServicioResponseDTO>();
        }
    }

    public async Task<List<ProductoResponseDTO>> GetProductosActivosAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<List<ProductoResponseDTO>>("api/productos") ?? new List<ProductoResponseDTO>();
        }
        catch
        {
            return new List<ProductoResponseDTO>();
        }
    }
}