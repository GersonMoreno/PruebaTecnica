using AutoMapper;
using Producto.Application.Shared;
using Producto.Domain.Contracts;
using Producto.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Application.Proveedor.ConsultarProductos
{
    public class ConsultarProductosQuery
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ConsultarProductosQuery(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public Response<ConsultarProductosResponse> Handle(int proveedorId, int? page)
        {
            var proveedorResponse = new ProveedorResponse();
            var productosResponse = new List<ProductoResponse>();
            const int pageSize = 5;
            int skip = (page ?? 0) * pageSize;
            var proveedor = _UnitOfWork.GenericRepository<Domain.Proveedor>().FindFirstOrDefault(x => x.Id == proveedorId);
            if (proveedor is null)
            {
                return new Response<ConsultarProductosResponse>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "No se encontró el proveedor en la base de datos.",
                    HuboError = true
                };
            }
            
            var productos = _UnitOfWork.GenericRepository<Domain.Producto>()
                .FindBy( x => x.Proveedor.Id == proveedorId)?.Skip(skip).Take(pageSize);
            if (productos.Any())
            {
                productosResponse = productos.Select(x => MapProducto(x)).ToList();
            }

            proveedorResponse = MapProveedor(proveedor);

            var ConsultarResponse = new ConsultarProductosResponse()
            {
                Proveedor = proveedorResponse,
                Productos = productosResponse
            };

            return new Response<ConsultarProductosResponse>
            {
                Mensaje = HttpResponse.Response200,
                Data = ConsultarResponse,
                Error = null,
                HuboError = false
            };
        }
        private ProveedorResponse MapProveedor(Domain.Proveedor proveedor)
        {
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Domain.Proveedor, ProveedorResponse>()
            );
            return new Mapper(config).Map<ProveedorResponse>(proveedor);
        }
        private ProductoResponse MapProducto(Domain.Producto producto)
        {
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Domain.Producto, ProductoResponse>()
            );
            return new Mapper(config).Map<ProductoResponse>(producto);
        }
    }
}
