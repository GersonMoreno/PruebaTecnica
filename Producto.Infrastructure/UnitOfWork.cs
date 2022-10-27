using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Producto.Domain.Base;
using Producto.Domain.Contracts;
using Producto.Infrastructure.Base;

namespace Vecindad.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductosContext _productoContext;
        public UnitOfWork(ProductosContext productoContext)
        {
            _productoContext = productoContext;
        }
        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_productoContext);
        }
        public void BeginTransaction()
        {
            _productoContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _productoContext.SaveChanges();
            _productoContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _productoContext.Database.RollbackTransaction();
        }

    }
}
