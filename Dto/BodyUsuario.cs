using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCCCactualizado.Dto
{
    public class BodyUsuario
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Sal { get; set; }
        public string Token { get; set; }
        public int Activo { get; set; }
        public int Rol { get; set; }
    }
}
