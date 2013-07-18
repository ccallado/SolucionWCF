using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    //Namespace Es para cuando la información se serializa (Prefijo) para evitar duplicidades
    [ServiceContract(Name="IServicioNormalAux", Namespace="http://com.miempresa2.wwww")]
    public interface IService2
    {

        //Que nos pida un número y nos devuelva una categoría
        [OperationContract]
        Category CategoriaPorID(int cat);

        //Que nos pida una categoría y nos devuelva todos los productos de la categoría
        [OperationContract]
        List<Product> ProductosPorCategoria(int IdCategoria);

        //Que nos da la hora
        [OperationContract]
        DateTime Hora();
    }
}
