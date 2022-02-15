using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public partial class Ingreso
    {
        public Ingreso()
        {
            DetalleIngresos = new HashSet<DetalleIngreso>();
        }

        public int IdIngreso { get; set; }
        public int? IdProveedor { get; set; }
        public string TipoComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Total { get; set; }

        public virtual Proveedor IdProveedorNavigation { get; set; }
        public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; }
    }
}
