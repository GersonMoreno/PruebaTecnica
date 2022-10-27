using Producto.Domain.Base;
using Producto.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Domain
{
    public class Producto: Entity<int>
    {
        public string Descripcion { get; private set; }
        public Estado Estado { get; private set; }
        public DateTime FechaFabricacion { get; private set; }
        public DateTime FechaVencimiento { get; private set; }
        public Proveedor Proveedor { get; private set; }
        public Producto()
        {

        }
        public Producto(ProductoDto dto, Proveedor proveedor)
        {
            if (dto is null)
                throw new Exception("Los datos a registrar no pueden ser nulos.");
            if(proveedor is null)
                throw new Exception("El proveedor no pueden ser nulo.");
            if (String.IsNullOrEmpty(dto.Descripcion))
                throw new Exception("La descripcion del producto no puede ser nulo.");
            if(dto.FechaVencimiento <= dto.FechaFabricacion)
                throw new Exception("La fecha de vencimiento del producto debe ser mayor a la fecha de fabricacion.");

            Descripcion = dto.Descripcion;
            Estado = dto.Estado;
            FechaFabricacion = dto.FechaFabricacion;
            FechaVencimiento = dto.FechaVencimiento;
            Proveedor = proveedor;
        }
        public string Modificar(ProductoDto dto)
        {
            if (dto is null)
                return  ("Los datos a modificar no pueden ser nulos.");
            if (String.IsNullOrEmpty(dto.Descripcion))
                return ("La descripcion del producto no puede ser nulo.");
            if (dto.FechaVencimiento <= dto.FechaFabricacion)
                return ("La fecha de vencimiento del producto debe ser mayor a la fecha de fabricacion.");

            Descripcion = dto.Descripcion;
            Estado = dto.Estado;
            FechaFabricacion = dto.FechaFabricacion;
            FechaVencimiento = dto.FechaVencimiento;

            return ("Se modifico con exito.");
        }
        public string Inactivar()
        {
            if (Estado == Estado.Inactivo)
                return "El producto ya se encuentra inactivo.";
            Estado = Estado.Inactivo;
            return "Se inactivo conrrectamente.";
        }
    }
    public class ProductoDto
    {
        public string Descripcion { get;  set; }
        public Estado Estado { get;  set; }
        public DateTime FechaFabricacion { get;  set; }
        public DateTime FechaVencimiento { get;  set; }
    }
    public enum Estado
    {
        Inactivo,
        Activo
    }
}
