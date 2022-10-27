using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Application.Shared
{
    public static class HttpResponse
    {
        public static string Response200 => "Ok-Success";
        public static string Response400 => "Bad Request";
        public static string Response500 => "Error Internal Server";
    }
}
