﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    public class ServicioNormal : IService1, IService2 
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
            if (IdCategoria <= 0)
                throw new FaultException<ClaseError>(new ClaseError()
                {
                    Error = enumTipoError.CategoriaErronea,
                    Mensaje = "La categoría no puede ser negativa.",
                    //No se puede mandar Datos como un objeto de excepción
                    //Lo hemos cambiado por una cadena y mando el mensage del error
                    Datos = (new ArgumentOutOfRangeException("IdCategoria",
                                                            IdCategoria,
                                                            "Id negativo")).Message 
                });

            using (northwindEntities ne = new northwindEntities())
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
                    throw new FaultException<string>("No existe la categoría " + IdCategoria,
                                                     "Categoría no encontrada.");
            }
        }


        public Category CategoriaPorIDconPausa(int IdCategoria, int segundos)
        {
            //Añado una pausa
            System.Threading.Thread.Sleep(segundos * 1000);

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

        //Metodo que incrementa uno a uno una variable definida al instanciar el servicio
        int Contador = 0;

        public int IncrementaContador()
        {
            Contador++;
            return Contador;
        }

        //Me ha implementado solo este porque los otros ya los había implementado con el IService1
        public DateTime Hora()
        {
            return DateTime.Now ;
        }

        //Implementado
        public List<object[]> StockProductos(int IdCategoria)
        {
            using (northwindEntities ne = new northwindEntities())
            {
                ne.ContextOptions.LazyLoadingEnabled = false;

                string cad = "SELECT p.ProductName, p.UnitsInStock " + 
                             "FROM northwindEntities.Products as p " +
                             "WHERE p.CategoryID = @cat";

                System.Data.Objects.ObjectQuery<System.Data.Common.DbDataRecord> consulta;
                consulta = new System.Data.Objects.ObjectQuery<System.Data.Common.DbDataRecord>
                                (cad, ne, System.Data.Objects.MergeOption.NoTracking);
                consulta.Parameters.Add(new System.Data.Objects.ObjectParameter("cat", IdCategoria));
                
                List<object[]> lista = new List<object[]>();
                if (consulta.Count()> 0)
                {
                    object[] reg;

                    //Vamos a pasar los nombres de los campos, como primer registro
                    foreach(var x in consulta)
                    {
                        //Si todavía no he añadido ningún registro a la lista pongo la cabecera
                        if (lista.Count == 0)
                        {
                            //Añadimos los nombres de los campos
                            reg = new object[x.FieldCount];
                            for (int i = 0; i < x.FieldCount; i++)
                                reg[i] = x.GetName(i);
                            lista.Add(reg);
                        }
                        reg = new object[x.FieldCount];
                        //Esto se puede hacer más fácilmente
                        //for (int i = 0; i < x.FieldCount; i++)
                        //    reg[i] = x[i];
                        x.GetValues(reg);

                        lista.Add(reg);
                    }
                }
                
                return lista;
            }
        }
    }
}
