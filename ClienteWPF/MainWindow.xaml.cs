using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //Instanciamos el servicio web
            ProxyWS.ServicioCurso ws = new ProxyWS.ServicioCurso();

            //Llamamos al método
            MessageBox.Show(ws.HelloWorld());

            //Llamamos al nuevo método HelloWord con sobrecarga de NOMBRE
            MessageBox.Show(ws.HelloWorld(textBox1.Text));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyWS.ServicioCurso ws = new ProxyWS.ServicioCurso())
            {
                string cad = "";
                var cats = ws.Categorias();

                foreach (ProxyWS.Category c in cats)
                {
                    cad += c.CategoryID + " - " +
                           c.CategoryName + "\n";
                }

                MessageBox.Show(cad);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyWS.ServicioCurso ws = new ProxyWS.ServicioCurso())
            {
                string cad = "";
                var cats = ws.CategoriasYProductos();

                foreach (ProxyWS.ClaseCategoria c in cats)
                {
                    cad += c.Categoria.CategoryID + " - " +
                           c.Categoria.CategoryName + " (Productos: " +
                           c.Productos.Count() + ")\n";

                    foreach (ProxyWS.Product p in c.Productos)
                    {
                        cad += "\n - " + p.ProductName + " - " +
                               p.UnitPrice.Value.ToString("c");
                    }

                    cad += "\n\n";
                }

                MessageBox.Show(cad);
            }
        }

        //GetData (WCF)
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyWCFNormal.ServicioNormalClient s =
                   new ProxyWCFNormal.ServicioNormalClient())
            {
                MessageBox.Show(s.GetData(int.Parse(textBox2.Text)));
            }
        }

        //CategoriaPorID
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyWCFNormal.ServicioNormalClient s =
                     new ProxyWCFNormal.ServicioNormalClient())
            {
                ProxyWCFNormal.Category cat = s.CategoriaPorID(int.Parse(textBox3.Text));
                if (cat != null)
                    MessageBox.Show(cat.CategoryName, "Categoría " + cat.CategoryID);
            }
        }

        //ProductosPorCategoria
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyWCFNormal.ServicioNormalClient s =
                     new ProxyWCFNormal.ServicioNormalClient())
            {
                var prods = s.ProductosPorCategoria(int.Parse(textBox3.Text));

                if (prods.Count() > 0)
                {
                    string cad = "";

                    foreach (ProxyWCFNormal.Product p in prods)
                        cad += p.ProductID + " - " +
                               p.ProductName + "\n";

                    MessageBox.Show(cad, "Productos " + prods.Count());
                }
            }
        }

        //Categoria y productos
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyWCFNormal.ServicioNormalClient s =
                     new ProxyWCFNormal.ServicioNormalClient())
            {
                ProxyWCFNormal.Category cat = s.CategoriaYProductosPorId(int.Parse(textBox3.Text));
                if (cat != null)
                {
                    string cad = cat.CategoryID + " - " +
                        cat.CategoryName + "\n";

                    foreach (var p in cat.RelProducts)
                        cad += "\n\t" + p.ProductID + " - " +
                               p.ProductName;
                    MessageBox.Show(cad, "Categoría " + cat.Description);
                }
            }
        }

        //Pedido por Cliente (WCF)
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyWCFNormal.ServicioNormalClient s =
                     new ProxyWCFNormal.ServicioNormalClient())
            {
                //Me creo la variable pedidos tipo Array de .....
                ProxyWCFNormal.Order[] pedidos;

                if (textBox5.Text != "")
                {
                    ProxyWCFNormal.Order ped = s.PedidoPorCliente(textBox4.Text, int.Parse(textBox5.Text));
                    pedidos = new ProxyWCFNormal.Order[] { ped };
                }
                else
                {
                    pedidos = s.PedidosPorCliente(textBox4.Text);
                }

                string cad = "Cantidad: " + pedidos.Count() + "\n";

                foreach (var p in pedidos)
                {
                    cad += p.OrderID + " - " +
                           p.OrderDate.Value.ToShortDateString() + "\n";

                    foreach (var d in p.RelOrder_Details)
                        cad += "\n\t" + d.Quantity + "x" +
                               d.RelProduct.ProductName + " (" +
                               d.UnitPrice.ToString("c") + ")";

                    cad += "\n\n";
                }
                MessageBox.Show(cad);
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyWCFNormal.ServicioNormalClient s =
                     new ProxyWCFNormal.ServicioNormalClient())
            {
                try
                {
                    ProxyWCFNormal.Category cat = s.CategoriaPorIDConErrores(int.Parse(textBox6.Text));
                    MessageBox.Show(cat.Description);
                }
                //Excepcion de SOAP
                catch (System.ServiceModel.FaultException<string> ex)
                {
                    MessageBox.Show(ex.Detail + "\n" + ex.Reason,
                                    "Tipo de error: " + ex.GetType());
                }
                //Excepcion de SOAP con la clase CLASEERROR
                catch (System.ServiceModel.FaultException<ProxyWCFNormal.ClaseError> ex)
                {
                    string cad = "";
                    //El Detail es un ClaseError
                    ProxyWCFNormal.ClaseError detalle = ex.Detail as ProxyWCFNormal.ClaseError;
                    cad += detalle.Mensaje + "\n" +
                        //La enumeración es pública en el proxy también
                           (detalle.Error == ProxyWCFNormal.enumTipoError.CategoriaErronea ?
                           "Categoría Inexistente..." :
                           detalle.Error.ToString()) + "\n";
                    if (detalle.Datos != null)
                    {
                        cad += "Excepción: " + detalle.Datos.GetType();
                        cad += "\n" + detalle.Datos;
                    }
                    MessageBox.Show(cad);
                }
            }
        }

        DateTime Entrada;

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            Entrada = DateTime.Now;

            //Crear objeto de contexto
            using (ProxyWCFNormal.ServicioNormalClient s =
                    new ProxyWCFNormal.ServicioNormalClient())
            {
                if (!checkBox1.IsChecked.Value)
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        ProxyWCFNormal.Category c = s.CategoriaPorIDconPausa(i, 2);
                        listBox1.Items.Add(i.ToString() + " - " +
                                           c.CategoryName + " - segundos: " +
                                           DateTime.Now.Subtract(Entrada).TotalSeconds);
                    }
                }
                else
                {
                    //Manejador de evento
                    s.CategoriaPorIDconPausaCompleted += new EventHandler<ProxyWCFNormal.CategoriaPorIDconPausaCompletedEventArgs>(s_CategoriaPorIDconPausaCompleted);
                    for (int i = 1; i <= 8; i++)
                    {
                        //El método es void
                        //El parametro de número de IdCategoria lo puedo pasar en UserState
                        s.CategoriaPorIDconPausaAsync(i, 5, i);
                    }
                }
            }
        }

        void s_CategoriaPorIDconPausaCompleted(object sender, ProxyWCFNormal.CategoriaPorIDconPausaCompletedEventArgs e)
        {
            //Esto se ejecuta en el hilo principal y por lo tanto tengo acceso a los controles
            ProxyWCFNormal.Category c = e.Result;

            //El parametro de número de IdCategoria lo puedo pasar en UserState
            listBox1.Items.Add(e.UserState.ToString() + " - " +
                               c.CategoryName + " - segundos: " +
                               DateTime.Now.Subtract(Entrada).TotalSeconds);
            //Application.
        }

        //Incrementar contador en servicio WCF
        private void button11_Click(object sender, RoutedEventArgs e)
        {
            //Crear objeto de contexto
            using (ProxyWCFNormal.ServicioNormalClient s =
                    new ProxyWCFNormal.ServicioNormalClient())
            {
                string cad = "";
                for (int i = 1; i <= 5; i++)
                {
                    cad += "Llamada " + i + " - Contador: " +
                           s.IncrementaContador() + "\n";
                }

                MessageBox.Show(cad);
            }
        }

        //Stock Productos (WCF)
        private void button12_Click(object sender, RoutedEventArgs e)
        {
            //Crear objeto de contexto
            using (ProxyWCFNormal.ServicioNormalClient s =
                    new ProxyWCFNormal.ServicioNormalClient())
            {
                var datos = s.StockProductos(int.Parse(textBox7.Text));

                string cad = "";
                foreach (var x in datos)
                { 
                    cad += "\n";
                    for (int i = 0; i < x.Count(); i++)
                    {
                        cad += x[i].ToString() + "\t";
                    }
                }

                MessageBox.Show(cad);
            }
        }

    }
}
