using Producto.Application.Shared;
using Producto.Domain.Contracts;
using Producto.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Application.Producto.EliminarProducto
{
    public class EliminarProductoCommand
    {
        private readonly IUnitOfWork _UnitOfWork;
        public EliminarProductoCommand(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public Response<EliminarProductoRequest> Handle(EliminarProductoRequest request)
        {
            _UnitOfWork.BeginTransaction();
            if (request is null)
            {
                return new Response<EliminarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "Los datoss de la peticion no puede ser nulo.",
                    HuboError = true
                };
            }

            var proveedor = _UnitOfWork.GenericRepository<Domain.Proveedor>().FindFirstOrDefault(x => x.Id == request.ProveedorId);
            if (proveedor is null)
            {
                return new Response<EliminarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "No se encontró el proveedor en la base de datos.",
                    HuboError = true
                };
            }

            var producto = _UnitOfWork.GenericRepository<Domain.Producto>().FindFirstOrDefault(x => x.Id == request.ProductoId);
            if (producto is null)
            {
                return new Response<EliminarProductoRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "No se encontró el producto en la base de datos.",
                    HuboError = true
                };
            }

            var respuesta = proveedor.InactivarProducto(ref producto);

            if (respuesta.HuboError)
            {
                _UnitOfWork.Rollback();
                return new Response<EliminarProductoRequest>
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
                return new Response<EliminarProductoRequest>
                {
                    Mensaje = HttpResponse.Response200,
                    Data = null,
                    Error = null,
                    HuboError = false
                };
            }
        }
    }
}
