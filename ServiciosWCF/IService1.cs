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
    [ServiceContract(Name="IServicioNormal", Namespace="http://com.miempresa.wwww")]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        //Que nos pida un número y nos devuelva una categoría
        [OperationContract]
        Category CategoriaPorID(int cat);

        //Que nos pida una categoría y nos devuelva todos los productos de la categoría
        [OperationContract]
        List<Product> ProductosPorCategoria(int IdCategoria);

        //Que nos pida un número y nos devuelva una categoría y todos sus productos
        [OperationContract]
        Category CategoriaYProductosPorId(int cat);

        //Que nos pida el Cliente y el número de Pedido nos devuelve un Pedido
        [OperationContract]
        Order PedidoPorCliente(string Cliente, int Pedido);

        //Que nos pida el Cliente y devolverá una lista de Pedidos (Sobrecarga)
        [OperationContract(Name="PedidosPorCliente")]
        List<Order> PedidoPorCliente(string Cliente);

        //Que nos pida un número y nos devuelva una categoría y si hay error lo indico en el servicio
        [OperationContract]
        //Tenemos que poner la etiqueta para indicar que este metodo devuelve en caso de error un string
        [FaultContract(typeof(string))]
        //Ahora exta excepción puede devolver un STRING o una instancia de CLASEERROR clase mia.
        [FaultContract(typeof(ClaseError))]
        Category CategoriaPorIDConErrores(int IdCategoria);

        //Que nos pida un número y nos devuelva una categoría
        [OperationContract]
        Category CategoriaPorIDconPausa(int IdCategoria, int segundos);

    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
