using System.ComponentModel.DataAnnotations;

namespace MOERAppMovil.DTOs;

public class ProductoCreateDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Precio { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int StockActual { get; set; }

    public string? Imagen { get; set; }

    [Required(ErrorMessage = "Debes seleccionar una categoría.")]
    public int IdCategoriaProducto { get; set; }
}