using System;
using System.Collections.Generic;

namespace MOERAppMovil.DTOs
{
    public class PedidoResponseDTO
    {
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteTelefono { get; set; }
        public List<DetallePedidoResponseDTO> Detalles { get; set; } = new List<DetallePedidoResponseDTO>();
    }

    public class DetallePedidoResponseDTO
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}