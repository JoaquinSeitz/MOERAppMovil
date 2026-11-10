using System.Net.Http.Json;
using MOERAppMovil.DTOs;

namespace MOERAppMovil.Services
{
    public class PedidoService
    {
        private readonly HttpClient _http;

        public PedidoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PedidoResponseDTO>> GetMisPedidos()
        {
            return await _http.GetFromJsonAsync<List<PedidoResponseDTO>>("api/pedidos/mis-pedidos")
                   ?? new List<PedidoResponseDTO>();
        }

        public async Task<HttpResponseMessage> CrearPedido(PedidoCreateDTO dto)
        {
            return await _http.PostAsJsonAsync("api/pedidos", dto);
        }
        // En PedidoService.cs, agrega estos dos métodos:
        public async Task<List<PedidoResponseDTO>> GetTodosLosPedidos()
        {
            return await _http.GetFromJsonAsync<List<PedidoResponseDTO>>("api/pedidos")
                   ?? new List<PedidoResponseDTO>();
        }

        public async Task<HttpResponseMessage> ActualizarEstadoPedido(int idPedido, PedidoEstadoUpdateDTO dto)
        {
            return await _http.PatchAsync($"api/pedidos/{idPedido}/estado", JsonContent.Create(dto));
        }
    }
}