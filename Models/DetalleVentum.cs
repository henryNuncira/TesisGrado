using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public class DetalleVentum
    {
        public int IdDetalleVenta { get; set; }
        public int? IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Descuento { get; set; }

        public Producto IdProductoNavigation { get; set; }
        public Ventum IdVentaNavigation { get; set; }
    }
}
