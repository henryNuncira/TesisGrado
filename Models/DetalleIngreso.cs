using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public class DetalleIngreso
    {
        public int IdDetalleIngreso { get; set; }
        public int? IdIngreso { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }

        public Ingreso IdIngresoNavigation { get; set; }
        public Producto IdProductoNavigation { get; set; }
    }
}
