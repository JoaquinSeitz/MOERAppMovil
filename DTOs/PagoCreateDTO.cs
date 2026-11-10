using System.ComponentModel.DataAnnotations;

namespace MOERAppMovil.DTOs
{
    public class PagoCreateDTO
    {
        public int? IdPedido { get; set; }
        public int? IdTurno { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        public int IdMetodoPago { get; set; }
    }
}