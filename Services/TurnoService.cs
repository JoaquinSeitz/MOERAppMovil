using System.Net.Http.Json;
using MOERAppMovil.DTOs;

namespace MOERAppMovil.Services
{
    public class TurnoService
    {
        private readonly HttpClient _http;

        public TurnoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TurnoResponseDTO>> GetMisTurnos()
        {
            return await _http.GetFromJsonAsync<List<TurnoResponseDTO>>("api/turnos/mis-turnos")
                   ?? new List<TurnoResponseDTO>();
        }

        public async Task<HttpResponseMessage> CrearTurno(TurnoCreateDTO dto)
        {
            return await _http.PostAsJsonAsync("api/turnos", dto);
        }

        public async Task<HttpResponseMessage> CancelarTurno(int idTurno)
        {
            return await _http.PatchAsync($"api/turnos/{idTurno}/cancelar", null);
        }
        public async Task<List<TurnoResponseDTO>> GetAgenda(DateTime? fecha = null)
        {
            // Si se pasa una fecha, la adjuntamos como query string; si no, el backend usará el día de hoy.
            var url = fecha.HasValue ? $"api/turnos/agenda?fecha={fecha.Value:yyyy-MM-dd}" : "api/turnos/agenda";
            return await _http.GetFromJsonAsync<List<TurnoResponseDTO>>(url)
                   ?? new List<TurnoResponseDTO>();
        }

        public async Task<HttpResponseMessage> ActualizarEstadoTurno(int idTurno, TurnoEstadoUpdateDTO dto)
        {
            // Usamos JsonContent.Create para asegurar la compatibilidad del método PATCH en Blazor WebAssembly
            return await _http.PatchAsync($"api/turnos/{idTurno}/estado", JsonContent.Create(dto));
        }
    }
}