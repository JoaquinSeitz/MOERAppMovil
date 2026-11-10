namespace MOERAppMovil.DTOs;

public class IngresoProductoRequestDTO
{
    public List<DetalleIngresoRequestDTO> Detalles { get; set; } = new();
}

public class DetalleIngresoRequestDTO
{
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioCompraUnitario { get; set; }
}