using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiciosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioSingle" en el código, en svc y en el archivo de configuración a la vez.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServicioSingle : IServicioSingle
    {
        //variable privada
        ClaseServicios cs;
        //objeto de bloqueo no se usa para nada
        object objetoBloqueo = new object();

        public int IncrementaContador(int segundosParada)
        {
            if (cs == null)
                cs = new ClaseServicios();
            //bloqueo bloque de código para evitar concurrencia
            lock (objetoBloqueo)
            { 
                return cs.IncrementaContador(segundosParada);
            }
        }

        public void ReseteaContador()
        {
            if (cs == null)
                cs = new ClaseServicios();
            else
                cs.ReseteaContador();
        }
    }
}
