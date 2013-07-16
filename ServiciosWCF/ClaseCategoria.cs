using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWCF
{
    //Para que esta clase se pueda enviar tiene que ser SERIALIZABLE
    [Serializable()]
    public class ClaseCategoria
    {
        //Este constructor lo necesita para serializar, ya que al haber puesto
        //el nuestro, el vacío deja de verse.
        public ClaseCategoria() { }

        //Constructor para evitar el error
        public ClaseCategoria(Category cat, System.Collections.Generic.List<Product> prods)
        {
            Categoria = cat;
            Productos = prods;
        }

        //public Int32 CategoryID { get; set; }
        //public string CategoryName { get; set; }
        //public string Description { get; set; }
        //public byte[] Picture { get; set; }
        public Category Categoria { get; set; }
        public System.Collections.Generic.List<Product> Productos { get; set; }
    }
}