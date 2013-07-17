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

        //Que nos pida el Cliente y el número de Pedido nos devuelve un Pedido
        public Order PedidoPorCliente(string Cliente, int Pedido)
        {
            using (northwindEntities ne =
                   new northwindEntities())
            {
                ne.ContextOptions.LazyLoadingEnabled = false;
                var ped = ne.Orders.Include("RelOrder_Details.RelProduct")
                               .Where(p => p.CustomerID == Cliente && p.OrderID == Pedido)
                               .SingleOrDefault();
                return ped;
            }
        }

        //Que nos pida el Cliente y devolverá una lista de Pedidos (Sobrecarga)
        public List<Order> PedidoPorCliente(string Cliente)
        {
            using (northwindEntities ne =
                   new northwindEntities())
            {
                ne.ContextOptions.LazyLoadingEnabled = false;
                var pedidos = ne.Orders.Include("RelOrder_Details.RelProduct")
                               .Where(p => p.CustomerID == Cliente);
                return pedidos.ToList();
            }
        }

        //Que nos pida un número y nos devuelva una categoría y si hay error lo indico en el servicio
        public Category CategoriaPorIDConErrores(int IdCategoria)
        {
            using (northwindEntities ne =
                   new northwindEntities())
            {
                ne.ContextOptions.LazyLoadingEnabled = false;
                var cat = ne.Categories
                            .Where(c => c.CategoryID == IdCategoria)
                            .SingleOrDefault();
                if (cat != null)
                    return cat;
                else
                    //throw new ArgumentOutOfRangeException("IdCategoria",
                    //    IdCategoria,
                    //    "Categoría no encontrada");
                    throw new FaultException<string>("No existe la categoría " + IdCategoria, "Categoría no encontrada.");
            }
        }
    }
}
