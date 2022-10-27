using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producto.Application.Producto.EliminarProducto;
using Producto.Application.Producto.GuardarProducto;
using Producto.Application.Producto.ModificarProducto;
using Producto.Domain.Contracts;

namespace Producto.Api.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("guardarproducto")]
        public ActionResult GuardarProducto(GuardarProductoRequest request)
        {
            var respuesta = new GuardarProductoCommand(_unitOfWork).Handle(request);
            if (respuesta.HuboError)
                return BadRequest(respuesta);
            return Ok(respuesta);
        }
        [HttpPost("modificarproducto")]
        public ActionResult ModificarProducto(ModificarProductoRequest request)
        {
            var respuesta = new ModificarProductoCommand(_unitOfWork).Handle(request);
            if (respuesta.HuboError)
                return BadRequest(respuesta);
            return Ok(respuesta);
        }
        [HttpPost("eliminarproducto")]
        public ActionResult EliminarProducto(EliminarProductoRequest request)
        {
            var respuesta = new EliminarProductoCommand(_unitOfWork).Handle(request);
            if (respuesta.HuboError)
                return BadRequest(respuesta);
            return Ok(respuesta);
        }
    }
}
