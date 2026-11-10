using MOERAppMovil.DTOs;

namespace MOERAppMovil.Services
{
    public class CarritoService
    {
        public List<ItemCarrito> Items { get; private set; } = new List<ItemCarrito>();

        public event Action OnChange;

        // Permite a la tienda saber cuánto stock ya está retenido en el carrito
        public int ObtenerCantidadEnCarrito(int idProducto)
        {
            var item = Items.FirstOrDefault(i => i.Producto.IdProducto == idProducto);
            return item?.Cantidad ?? 0;
        }

        public void AgregarProducto(ProductoResponseDTO producto, int cantidad)
        {
            var itemExistente = Items.FirstOrDefault(i => i.Producto.IdProducto == producto.IdProducto);
            int cantidadActual = itemExistente?.Cantidad ?? 0;

            // Seguridad: Bloquea intentos de agregar más del stock físico
            if (cantidadActual + cantidad > producto.StockActual) return;

            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
            }
            else
            {
                Items.Add(new ItemCarrito { Producto = producto, Cantidad = cantidad });
            }
            NotifyStateChanged();
        }

        // Permite modificar las unidades exactas desde la tabla del carrito
        public void ActualizarCantidad(int idProducto, int nuevaCantidad)
        {
            var item = Items.FirstOrDefault(i => i.Producto.IdProducto == idProducto);
            if (item != null)
            {
                if (nuevaCantidad <= 0)
                {
                    Items.Remove(item); // Si baja a 0, se elimina automáticamente
                }
                else if (nuevaCantidad <= item.Producto.StockActual)
                {
                    item.Cantidad = nuevaCantidad;
                }
                NotifyStateChanged();
            }
        }

        public void EliminarProducto(int idProducto)
        {
            var item = Items.FirstOrDefault(i => i.Producto.IdProducto == idProducto);
            if (item != null)
            {
                Items.Remove(item);
                NotifyStateChanged();
            }
        }

        public void VaciarCarrito()
        {
            Items.Clear();
            NotifyStateChanged();
        }

        public decimal ObtenerTotal()
        {
            return Items.Sum(i => i.Producto.Precio * i.Cantidad);
        }

        public PedidoCreateDTO GenerarDTO()
        {
            return new PedidoCreateDTO
            {
                Detalles = Items.Select(i => new DetallePedidoCreateDTO
                {
                    IdProducto = i.Producto.IdProducto,
                    Cantidad = i.Cantidad
                }).ToList()
            };
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class ItemCarrito
    {
        public ProductoResponseDTO Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Producto.Precio * Cantidad;
    }
}