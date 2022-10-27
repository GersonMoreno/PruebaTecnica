using Producto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Application.Producto.ModificarProducto
{
    public class ModificarProductoRequest
    {
        public int ProveedorId { get; set; }
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public DateTime FechaFabricacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
