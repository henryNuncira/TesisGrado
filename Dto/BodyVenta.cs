using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCCCactualizado.Dto
{
    public class BodyVenta
    {
        [Required]
        [Range(1,Double.MaxValue,ErrorMessage ="El valor del idCliente debe ser mayor a 0")]
        [ExisteCliente(ErrorMessage ="El cliente no existe")]
        public int IdCliente { get; set; }

        public DateTime? Fecha { get; set; }
      
        public string NumeroComprobante { get; set; }
        public decimal? Impuesto { get; set; }
        public string TipoComprobante { get; set; }
        public int? IdVendedor { get; set; }
       
        [Required]
        [MinLength(1, ErrorMessage ="deben existir un detalle de la venta")]
        public List<Detalle>   Detalles { get; set; }
        public BodyVenta ()
        {
            this.Detalles = new List<Detalle>();
        }

    }
    public class Detalle
    {
        public int? idProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Descuento { get; set; }
    }
    #region validaciones
    public class ExisteClienteAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int idCliente = (int)value;
            using(var db = new Models.CCCVEntasContext())
            {
                if (db.Clientes.Find(idCliente) == null) return false;
            }
            return true;
        }
    }
    #endregion
}
