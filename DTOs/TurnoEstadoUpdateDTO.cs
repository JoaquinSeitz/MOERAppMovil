using System.ComponentModel.DataAnnotations;

namespace MOERAppMovil.DTOs
{
    public class TurnoEstadoUpdateDTO
    {
        [Required(ErrorMessage = "El estado del turno es obligatorio.")]
        public int IdEstadoTurno { get; set; }
    }
}