using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public  class Ventum
    {
        public Ventum()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IdVenta { get; set; }
        public int? IdCliente { get; set; }
        public string TipoComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Total { get; set; }
        public int? IdVendedor { get; set; }



        public  Cliente IdClienteNavigation { get; set; }
        public Vendedor IdVendedorNavigation { get; set; }
        public  ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
