﻿using System;
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
using System.Windows.Shapes;

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        string cad = "";

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyPorLlamada.ServicioPorLlamadaClient  s =
                   new ProxyPorLlamada.ServicioPorLlamadaClient())
            {
                cad = "";
                for (int i = 1; i <= 3; i++)
                {
                    cad += "\n" + i + " -> contador: " +
                           s.IncrementaContador(int.Parse(textBox1.Text));
                }

                MessageBox.Show(cad);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            using (ProxyPorSesion.ServicioPorSesionClient  s =
                   new ProxyPorSesion.ServicioPorSesionClient())
            {
                cad = "";
                for (int i = 1; i <= 3; i++)
                {
                    cad += "\n" + i + " -> contador: " +
                           s.IncrementaContador(int.Parse(textBox1.Text));
                }

                MessageBox.Show(cad);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
