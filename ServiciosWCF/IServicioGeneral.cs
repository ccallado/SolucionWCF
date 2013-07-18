using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

//Vamos a usar este interface en los tres servicios
namespace ServiciosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioPorLlamada" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioGeneral
    {
        [OperationContract]
        int IncrementaContador(int segundosParada);
    }
}
