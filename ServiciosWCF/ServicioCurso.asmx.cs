using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServiciosWCF
{
    /// <summary>
    /// Descripción breve de ServicioCurso
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    //Esta línea se quita a partir de que se pone el MessageName ya que es incompatible
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioCurso : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //Voy a darle un nombre de cara a SOAP (ALIAS)
        [WebMethod(MessageName = "HelloWordConNombre")]
        public string HelloWorld(string nombre)
        {
            return "Hello " + (string.IsNullOrWhiteSpace(nombre) ? "World" : nombre);
        }

        //Devolvemos una lista de tipo Category
        //Con CacheDuration impedimos que se ataque muchas veces a la base de datos
        [WebMethod(CacheDuration=15)]
        public System.Collections.Generic.List<Category> Categorias()
        {
            using (northwindEntities ne = new northwindEntities())
            {
                //No es imprescindible pero si recomendable
                ne.ContextOptions.LazyLoadingEnabled = false;
                return ne.Categories.ToList();
            }
        }

        //Devolvemos una lista de tipo Category con todos sus productos
        [WebMethod]
        public System.Collections.Generic.List<ClaseCategoria> CategoriasYProductos()
        {
            using (northwindEntities ne = new northwindEntities())
            {
                //No es imprescindible pero si recomendable
                ne.ContextOptions.LazyLoadingEnabled = false;

                //Hecho de dos maneras distintas
                //var cats = from c in ne.Categories.Include("Products")
                //           select new ClaseCategoria()
                //           {
                //               Categoria = c,
                //               Productos = c.Products.ToList()
                //           };
                //var cats = ne.Categories
                //    .Include("Products")
                //    .Select(c => new ClaseCategoria()
                //        {
                //            Categoria = c,
                //            Productos = c.Products.ToList()
                //        }
                //    );

                List<ClaseCategoria> cats = new List<ClaseCategoria>();
                foreach (Category c in ne.Categories.Include("RelProducts"))
                    cats.Add(new ClaseCategoria(c, c.RelProducts.ToList()));

                return cats.ToList();
            }
        }
    }
}
