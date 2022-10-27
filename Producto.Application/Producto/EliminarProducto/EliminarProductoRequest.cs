using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Application.Producto.EliminarProducto
{
    public class EliminarProductoRequest
    {
        public int ProveedorId { get; set; }
        public int ProductoId { get; set; }
    }
}
