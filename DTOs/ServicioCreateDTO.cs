using System.ComponentModel.DataAnnotations;

namespace MOERAppMovil.DTOs
{
    public class ServicioCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int IdCategoriaServicio { get; set; }
    }
}