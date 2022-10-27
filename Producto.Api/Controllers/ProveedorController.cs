using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producto.Application.Proveedor.ConsultarProductos;
using Producto.Application.Proveedor.GuardarProveedor;
using Producto.Domain.Contracts;

namespace Producto.Api.Controllers
{
    [Route("api/proveedor")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProveedorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("guardarproveedor")]
        public ActionResult GuardarProveedor(GuardarProveedorRequest request)
        {
            var respuesta = new GuardarProveedorCommand(_unitOfWork).Handle(request);
            if (respuesta.HuboError)
                return BadRequest(respuesta);
            return Ok(respuesta);
        }
        [HttpPost("consultarproductos")]
        public ActionResult ConsultarProveedor(int ProveedorId, int? page)
        {
            var respuesta = new ConsultarProductosQuery(_unitOfWork).Handle(ProveedorId,page);
            if (respuesta.HuboError)
                return BadRequest(respuesta);
            return Ok(respuesta);
        }
    }
}
