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
            MessageBox.Show( ws.HelloWorld());

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
                var cats = ws.CategoriasYProductos ();

                foreach (ProxyWS.ClaseCategoria  c in cats)
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
    }
}
