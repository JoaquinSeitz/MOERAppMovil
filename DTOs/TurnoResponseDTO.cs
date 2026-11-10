using System;

namespace MOERAppMovil.DTOs
{
    public class TurnoResponseDTO
    {
        public int IdTurno { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public string Estado { get; set; }
        public string Servicio { get; set; }
        public decimal Precio { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteTelefono { get; set; }
    }
}