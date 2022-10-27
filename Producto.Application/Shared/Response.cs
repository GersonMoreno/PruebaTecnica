using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Application.Shared
{
    public class Response<T>
    {
        public string Mensaje { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
        public bool HuboError { get; set; }
    }
}
