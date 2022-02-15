using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public partial class DetalleIngreso
    {
        public int IdDetalleIngreso { get; set; }
        public int? IdIngreso { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }

        public virtual Ingreso IdIngresoNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
