using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleIngresos = new HashSet<DetalleIngreso>();
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IdProducto { get; set; }
        public int? IdCategoria { get; set; }
        public string NitCodigo { get; set; }
        public string Nombre { get; set; }
        public decimal? PrecioVenta { get; set; }
        public int? Stock { get; set; }
        public string Descricion { get; set; }

        public virtual Categorium IdCategoriaNavigation { get; set; }
        public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
