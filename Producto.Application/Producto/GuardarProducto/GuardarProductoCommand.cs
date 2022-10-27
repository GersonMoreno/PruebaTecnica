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

namespace Producto.Application.Producto.GuardarProducto
{
    public class GuardarProductoCommand
    {
        private readonly IUnitOfWork _UnitOfWork;
        public GuardarProductoCommand(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public Response<GuardarProductoRequest> Handle(GuardarProductoRequest request)
        {
            _UnitOfWork.BeginTransaction();

            if (request is null)
            {
                return new Response<GuardarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "Los datos del producto no puede ser nulo.",
                    HuboError = true
                };
            }

            var proveedor = _UnitOfWork.GenericRepository<Domain.Proveedor>().FindFirstOrDefault(x => x.Id == request.ProveedorId);
            if(proveedor is null)
            {
                return new Response<GuardarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "No se encontró el proveedor en la base de datos.",
                    HuboError = true
                };
            }
            if (request.Estado < 0 || request.Estado > 1)
            {
                return new Response<GuardarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "El estado no es correcto.",
                    HuboError = true
                };
            }

            try
            {
                var producto = new Domain.Producto(Map(request),proveedor);
                _UnitOfWork.GenericRepository<Domain.Producto>().Add(producto);
                _UnitOfWork.Commit();
                return new Response<GuardarProductoRequest>
                {
                    Mensaje = HttpResponse.Response200,
                    Data = null,
                    Error = null,
                    HuboError = false
                };
            }
            catch (Exception e)
            {
                _UnitOfWork.Rollback();
                return new Response<GuardarProductoRequest>
                {
                    Mensaje = HttpResponse.Response500,
                    Data = null,
                    Error = e.Message,
                    HuboError = true
                };
            }
        }
        private ProductoDto Map(GuardarProductoRequest request)
        {
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<GuardarProductoRequest, ProductoDto>()
            );
            return new Mapper(config).Map<ProductoDto>(request);
        }
    }
}
