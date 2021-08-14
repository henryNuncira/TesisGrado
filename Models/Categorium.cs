using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public class Categorium
    {
        public Categorium()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
