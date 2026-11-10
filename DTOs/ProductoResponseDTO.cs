namespace MOERAppMovil.DTOs;

public class ProductoResponseDTO
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? Imagen { get; set; }
    public decimal Precio { get; set; }
    public int StockActual { get; set; }
    public int IdCategoriaProducto { get; set; }
    public string CategoriaNombre { get; set; } = string.Empty;
    public bool Activo { get; set; }
}