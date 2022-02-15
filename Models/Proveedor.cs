using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdProveedor { get; set; }
        public string NitProveedor { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
