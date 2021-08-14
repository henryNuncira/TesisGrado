using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int? Activo { get; set; }
        public int? Rol { get; set; }
        public string Sal { get; set; }
        public string Token { get; set; }
    }
}
