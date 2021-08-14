using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCCCactualizado.Dto
{
    public class BodyProducto
    {
        public string NombreCompleto { get; set; }
        public string NitCodigo { get; set; }
        public int? Precio { get; set; }
        public int? Stock { get; set; }
        public string Descripcion { get; set; }
    
    }
}
