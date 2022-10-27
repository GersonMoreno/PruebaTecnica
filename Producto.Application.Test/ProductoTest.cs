using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Producto.Application.Producto.GuardarProducto;
using Producto.Infrastructure;
using Producto.Infrastructure.Base;

namespace Producto.Application.Test
{
    public class ProductoTest
    {
        private ProductosContext _context;
        [SetUp]
        public void Setup()
        {
            var optionsInMemory = new DbContextOptionsBuilder<ProductosContext>().UseInMemoryDatabase("ProductosInMemory").Options;
            _context = new ProductosContext(optionsInMemory);
        }

        [Test]
        public void NoSePuedeGuardarProductosConDatosNulos()
        {
            var command = new GuardarProductoCommand(new UnitOfWorkTest(_context));

            var response = command.Handle(null);

            Assert.AreEqual(response.Error, "Los datos del producto no puede ser nulo.");
            Assert.IsTrue(response.HuboError);
        }
        [Test]
        public void NoSePuedeGuardarProductosSinProveedorRegistrado()
        {
            #region Preparacion
            var command = new GuardarProductoCommand(new UnitOfWorkTest(_context));
            var request = new GuardarProductoRequest();
            #endregion


            var response = command.Handle(request);

            Assert.AreEqual(response.Error, "No se encontró el proveedor en la base de datos.");
            Assert.IsTrue(response.HuboError);
        }
        [Test]
        public void NoSePuedeGuardarProductosDatosVacios()
        {
            #region Preparacion
            var command = new GuardarProductoCommand(new UnitOfWorkTest(_context));
            var request = new GuardarProductoRequest();
            #endregion


            var response = command.Handle(request);

            Assert.AreEqual(response.Error, "No se encontró el proveedor en la base de datos.");
            Assert.IsTrue(response.HuboError);
        }
    }
}