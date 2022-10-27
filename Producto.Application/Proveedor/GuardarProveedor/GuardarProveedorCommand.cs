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

namespace Producto.Application.Proveedor.GuardarProveedor
{
    public class GuardarProveedorCommand
    {
        private readonly IUnitOfWork _UnitOfWork;
        public GuardarProveedorCommand(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public Response<GuardarProveedorRequest> Handle(GuardarProveedorRequest request)
        {

            _UnitOfWork.BeginTransaction();

            if(request is null)
            {
                return new Response<GuardarProveedorRequest>
                {
                    Mensaje = HttpResponse.Response400,
                    Data = null,
                    Error = "Los datos del proveedor no puede ser nulo.",
                    HuboError = true
                };
            }
            try
            {
                var proveedor = new Domain.Proveedor(Map(request));
                _UnitOfWork.GenericRepository<Domain.Proveedor>().Add(proveedor);
                _UnitOfWork.Commit();
                return new Response<GuardarProveedorRequest>
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
                return new Response<GuardarProveedorRequest>
                {
                    Mensaje = HttpResponse.Response500,
                    Data = null,
                    Error = e.Message,
                    HuboError = true
                };
            }
            
        }
        private ProveedorDto Map(GuardarProveedorRequest request)
        {
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<GuardarProveedorRequest, ProveedorDto>()
            );
            return new Mapper(config).Map<ProveedorDto>(request);
        }
    }
}
