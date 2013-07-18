using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWCF
{
    public class ClaseServicios
    {
        int Contador = 0;

        internal int IncrementaContador(int segundosParada)
        {
            System.Threading.Thread.Sleep(segundosParada * 1000);
            return ++Contador;
        }
    }
}