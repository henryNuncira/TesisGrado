using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCCCactualizado.Dto
{
    public class BodyDetalleVenta
    {
      
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Descuento { get; set; }

    }
}
