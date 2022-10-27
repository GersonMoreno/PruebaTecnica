using Producto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Application.Proveedor.ConsultarProductos
{
    public class ConsultarProductosResponse
    {
        public ProveedorResponse Proveedor { get; set; }
        public List<ProductoResponse> Productos { get; set; }
    }
    public class ProveedorResponse
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
    }
    public class ProductoResponse
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Estado Estado { get; set; }
        public DateTime FechaFabricacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
