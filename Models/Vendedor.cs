using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public  class Vendedor
    {
        public Vendedor()
        {
            Venta = new HashSet<Ventum>();
        }

        public int IdVendedor { get; set; }
        public string Nit { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }

        public ICollection<Ventum> Venta { get; set; }
    }
}
