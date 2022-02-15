using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public bool? Activo { get; set; }
        public int? IdRol { get; set; }
        public string Sal { get; set; }
        public string Token { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
    }
}
