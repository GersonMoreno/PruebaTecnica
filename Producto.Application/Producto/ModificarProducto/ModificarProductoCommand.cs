using AutoMapper;
using Producto.Application.Shared;
using Producto.Domain;
using Producto.Domain.Contracts;
using Producto.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Application.Producto.ModificarProducto
{
    public class ModificarProductoCommand
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ModificarProductoCommand(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public Response<ModificarProductoRequest> Handle(ModificarProductoRequest request)
        {
            _UnitOfWork.BeginTransaction();
            if (request is null)
            {
                return new Response<ModificarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "Los datos del producto no puede ser nulo.",
                    HuboError = true
                };
            }

            var proveedor = _UnitOfWork.GenericRepository<Domain.Proveedor>().FindFirstOrDefault(x => x.Id == request.ProveedorId);
            if (proveedor is null)
            {
                return new Response<ModificarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data =null,
                    Error = "No se encontró el proveedor en la base de datos.",
                    HuboError = true
                };
            }

            var producto = _UnitOfWork.GenericRepository<Domain.Producto>().FindFirstOrDefault(x => x.Id == request.ProductoId);
            if (producto is null)
            {
                return new Response<ModificarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data=null,
                    Error = "No se encontró el producto en la base de datos.",
                    HuboError = true
                };
            }
            if (request.Estado < 0 || request.Estado > 1)
            {
                return new Response<ModificarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "El estado no es correcto.",
                    HuboError = true
                };
            }
            var respuesta = proveedor.ModificarProducto(Map(request), ref producto);

            if (respuesta.HuboError)
            {
                _UnitOfWork.Rollback();
                return new Response<ModificarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = respuesta.Error,
                    HuboError = true
                };
            }
            else
            {
                _UnitOfWork.GenericRepository<Domain.Producto>().Update(producto);
                _UnitOfWork.Commit();
                return new Response<ModificarProductoRequest>
                {
                    Mensaje = HttpResponse.Response200,
                    Data = null,
                    Error = null,
                    HuboError = false
                };
            }
        }
        private ProductoDto Map(ModificarProductoRequest request)
        {
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<ModificarProductoRequest, ProductoDto>()
            );
            return new Mapper(config).Map<ProductoDto>(request);
        }
    }
}
