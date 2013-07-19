using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiciosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioSingle" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioSingle : IServicioGeneral
    {
        //IsOneWay, el cliente no tiene que esperar nada
        //El servidor corta la comunicación con el cliente
        //Muy util para metodos VOID
        [OperationContract(IsOneWay = true)]
        void ReseteaContador();
    }
}
