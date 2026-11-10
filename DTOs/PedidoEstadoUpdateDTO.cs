using System.ComponentModel.DataAnnotations;

namespace MOERAppMovil.DTOs
{
    public class PedidoEstadoUpdateDTO
    {
        [Required]
        public int IdEstadoPedido { get; set; }
    }
}