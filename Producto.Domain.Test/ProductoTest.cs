using NUnit.Framework;

namespace Producto.Domain.Test
{
    public class ProductoTest
    {
        Proveedor proveedor;
        [SetUp]
        public void Setup()
        {
            var proveedorDto = new ProveedorDto()
            {
                Descripcion = "Proveedor",
                Telefono = "12345"

            };
            proveedor = new Proveedor(proveedorDto);
        }

        [Test]
        public void NoSePuedeGuardarProductoConDatosNulos()
        {

            var respuesta = proveedor.GuardarProducto(null);

            Assert.IsTrue(respuesta.HuboError);

            Assert.AreEqual(respuesta.Error,"Los datos a registrar no pueden ser nulos.");

        }
        [Test]
        public void NoSePuedeGuardarProductoConDescripcionNula()
        {
            #region Preparacion
            
            var productoDto = new ProductoDto() { 
            };
            #endregion


            var respuesta = proveedor.GuardarProducto(productoDto);

            Assert.IsTrue(respuesta.HuboError);

            Assert.AreEqual(respuesta.Error, "La descripcion del producto no puede ser nulo.");

        }

        [Test]
        public void NoSePuedeGuardarProductoConFechaDeVencimientoMenorOIgualFechaFabricacion()
        {
            #region Preparacion

            var productoDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 10, 25),
            };
            #endregion


            var respuesta = proveedor.GuardarProducto(productoDto);

            Assert.IsTrue(respuesta.HuboError);

            Assert.AreEqual(respuesta.Error, "La fecha de vencimiento del producto debe ser mayor a la fecha de fabricacion.");

        }
       
        [Test]
        public void SeGuardaProductoCorrectamente()
        {
            #region Preparacion

            var productoDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 12, 26),
                Estado = Estado.Activo
            };
            #endregion


            var respuesta = proveedor.GuardarProducto(productoDto);

            Assert.AreEqual(proveedor.Productos.Count,1);
            Assert.IsFalse(respuesta.HuboError);
            Assert.IsNull(respuesta.Error);

        }
        [Test]
        public void NoSePuedeModificarProductoNulo()
        {
            #region Preparacion

            Producto producto = null;
            #endregion

            var respuesta = proveedor.ModificarProducto(null,ref producto);

            Assert.IsTrue(respuesta.HuboError);

            Assert.AreEqual(respuesta.Error, "El producto que desea modificar no debe ser nulo.");

        }
        [Test]
        public void LosDatosParaModificarProductoNoDebeSerNulo()
        {
            #region Preparacion
            var productoDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 12, 26),
                Estado = Estado.Activo
            };
            Producto producto = new Producto(productoDto, new Proveedor());
            #endregion

            var respuesta = proveedor.ModificarProducto(null, ref producto);

            Assert.IsTrue(respuesta.HuboError);

            Assert.AreEqual(respuesta.Error, "Los datos a modificar no pueden ser nulos.");

        }
        [Test]
        public void NoSePuedeModificarProductoConFechaDeVencimientoMenorOIgualFechaFabricacion()
        {
            #region Preparacion

            var productoModificarDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 10, 26),
                Estado = Estado.Activo
            };
            var productoDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 12, 25),
            };
            Producto producto = new Producto(productoDto, new Proveedor());
            #endregion


            var respuesta = proveedor.ModificarProducto(productoModificarDto, ref producto);

            Assert.IsTrue(respuesta.HuboError);

            Assert.AreEqual(respuesta.Error, "La fecha de vencimiento del producto debe ser mayor a la fecha de fabricacion.");

        }
        [Test]
        public void ModificarProductoCorrectamente()
        {
            #region Preparacion

            var productoModificarDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 11, 26),
                Estado = Estado.Activo
            };
            var productoDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 12, 25),
            };
            Producto producto = new Producto(productoDto, new Proveedor());
            #endregion

            var respuesta = proveedor.ModificarProducto(productoModificarDto, ref producto);

            Assert.IsFalse(respuesta.HuboError);
            Assert.IsNull(respuesta.Error);

        }
        [Test]
        public void ProductoConEstadoInactivoNoSePuedeInactivar()
        {
            #region Preparacion

            var productoDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 12, 26),
                Estado = Estado.Inactivo
            };
            Producto producto = new Producto(productoDto, new Proveedor());
            #endregion


            var respuesta = proveedor.InactivarProducto(ref producto);

            
            Assert.IsTrue(respuesta.HuboError);
            Assert.AreEqual(respuesta.Error, "El producto ya se encuentra inactivo.");

        }
        [Test]
        public void ProductoSePuedeInactivar()
        {
            #region Preparacion

            var productoDto = new ProductoDto()
            {
                Descripcion = "Arroz",
                FechaFabricacion = new System.DateTime(2022, 10, 26),
                FechaVencimiento = new System.DateTime(2022, 12, 26),
                Estado = Estado.Activo
            };
            Producto producto = new Producto(productoDto, new Proveedor());
            #endregion


            var respuesta = proveedor.InactivarProducto(ref producto);


            Assert.IsFalse(respuesta.HuboError);
            Assert.IsNull(respuesta.Error);

        }


    }
}