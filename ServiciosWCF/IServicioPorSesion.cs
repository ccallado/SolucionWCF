using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiciosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioPorSesion" en el código y en el archivo de configuración a la vez.
    //Hay que configurar los binding para que esto funcione
    [ServiceContract(SessionMode=SessionMode.Allowed)]
    public interface IServicioPorSesion : IServicioGeneral
    {
        //[OperationContract]
        //int IncrementaContador(int segundosParada);
    }
}
