using System;
using System.ComponentModel.DataAnnotations;

namespace MOERAppMovil.DTOs
{
    public class TurnoCreateDTO
    {
        [Required(ErrorMessage = "El servicio es obligatorio.")]
        public int IdServicio { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public TimeSpan? HoraInicio { get; set; }
    }
}