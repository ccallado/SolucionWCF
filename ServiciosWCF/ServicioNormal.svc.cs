using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    public class ServicioNormal : IService1
    {
        public string GetData(int value)
        {
            return string.Format("Has introducido: {0}", value);
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}


        //Método para devolver una categoria y pide un int
        public Category CategoriaPorID(int IdCategoria)
        {
            using (northwindEntities ne =
                   new northwindEntities())
            {
                ne.ContextOptions.LazyLoadingEnabled = false;
                var cat = ne.Categories
                            .Where(c => c.CategoryID == IdCategoria)
                            .SingleOrDefault();
                return cat;
            }
        }

        //Método para devolver una lista de productos y pide un int
        public List<Product> ProductosPorCategoria(int IdCategoria)
        {
            using (northwindEntities ne =
                   new northwindEntities())
            {
                ne.ContextOptions.LazyLoadingEnabled = false;
                var prod = ne.Products 
                            .Where(p => p.CategoryID == IdCategoria);
                return prod.ToList();
            }
        }

        //Método para devolver una lista de productos y pide un int
        public Category CategoriaYProductosPorId(int IdCategoria)
        {
            using (northwindEntities ne =
                   new northwindEntities())
            {
                ne.ContextOptions.LazyLoadingEnabled = false;
                var cat = ne.Categories.Include("RelProducts")
                            .Where(c => c.CategoryID == IdCategoria)
                            .SingleOrDefault();
                return cat;
            }
        }
    }
}
