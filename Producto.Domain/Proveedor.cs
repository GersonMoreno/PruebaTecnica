using Producto.Domain.Base;
using Producto.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Producto.Domain
{
    public class Proveedor: Entity<int>, IAggregateRoot
    {
        public string Descripcion { get; private set; }
        public string Telefono { get; private set; }
        public List<Producto> Productos { get; private set; }
        public Proveedor()
        {
            Productos = new List<Producto>();
        }
        public Proveedor(ProveedorDto dto)
        {
            if (dto is null)
                throw new Exception("Los datos a registrar no pueden ser nulos.");
            if (String.IsNullOrEmpty(dto.Descripcion))
                throw new Exception("La descripcion del proveedor no puede ser nula.");
            if(String.IsNullOrEmpty(dto.Telefono))
                throw new Exception("El telefono del proveedor no puede ser nulo.");

            Descripcion = dto.Descripcion;
            Telefono = dto.Telefono;
            Productos = new List<Producto>();
        }
        public Respuesta GuardarProducto(ProductoDto productoDto)
        {
            try
            {
                var producto = new Producto(productoDto, this);
                Productos.Add(producto);

                return new Respuesta()
                {
                    Error = null,
                    HuboError = false
                };
            }
            catch (Exception e)
            {

                return new Respuesta()
                {
                    Error = e.Message,
                    HuboError = true
                };
            }

        }
        public Respuesta ModificarProducto(ProductoDto productoDto, ref Producto producto)
        {
            if(producto is null)
            {
                return new Respuesta()
                {
                    Error = "El producto que desea modificar no debe ser nulo.",
                    HuboError = true
                };
            }
            
            var respuesta = producto.Modificar(productoDto);
            if(!respuesta.Equals("Se modifico con exito."))
            {
                return new Respuesta()
                {
                    Error = respuesta,
                    HuboError = true
                };
            }
            return new Respuesta()
            {
                Error = null,
                HuboError = false
            };
        }
        public Respuesta InactivarProducto(ref Producto producto)
        {
            var respuesta = producto.Inactivar();
            if(!respuesta.Equals("Se inactivo conrrectamente."))
            {
                return new Respuesta()
                {
                    Error = respuesta,
                    HuboError = true
                };
            }
            return new Respuesta()
            {
                Error = null,
                HuboError = false
            };
        }
    }
    public class ProveedorDto
    {
        public string Descripcion { get;  set; }
        public string Telefono { get;  set; }
    }
}
