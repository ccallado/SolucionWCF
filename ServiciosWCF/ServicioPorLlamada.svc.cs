using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiciosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioPorLlamada" en el código, en svc y en el archivo de configuración a la vez.
    //PerCall al ser por defecto no es necesario ponerlo
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class ServicioPorLlamada : IServicioPorLlamada  
    {
        //variable privada
        ClaseServicios cs;

        public int IncrementaContador(int segundosParada)
        {
            if (cs == null)
                cs = new ClaseServicios();

            return cs.IncrementaContador(segundosParada);
        }
    }
}
