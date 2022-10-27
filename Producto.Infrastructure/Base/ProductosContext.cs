using Microsoft.EntityFrameworkCore;
using Producto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Infrastructure.Base
{
    public class ProductosContext : DbContext
    {
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Domain.Producto> Productos { get; set; }
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {

        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Proveedores
            modelBuilder.Entity<Proveedor>().HasKey(x => x.Id);
            modelBuilder.Entity<Proveedor>().HasMany(x => x.Productos);

            //Productos
            modelBuilder.Entity<Domain.Producto>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Producto>().HasOne(x => x.Proveedor);
        }
    }
}
