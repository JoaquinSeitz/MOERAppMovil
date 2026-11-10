using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOERAppMovil.DTOs
{
    public class PedidoCreateDTO
    {
        [Required(ErrorMessage = "El pedido debe contener al menos un producto.")]
        public List<DetallePedidoCreateDTO> Detalles { get; set; } = new List<DetallePedidoCreateDTO>();
    }

    public class DetallePedidoCreateDTO
    {
        [Required]
        public int IdProducto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
    }
}