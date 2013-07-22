using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Para ignorar la serialización de algun parametro
using System.Runtime.Serialization;

namespace ServiciosWCF
{
    //Si ponemos esta label se serializará solo aquellos que lleven [DataMember]
    [DataContract]
    public class ClaseDatos
    {
        [DataMember]
        //Aunque sea local tambien lo serializa
        int IdLocal { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }

        //Si quiero que alguna no quiero que se serialice
        [IgnoreDataMember]
        public DateTime Creacion { get; set; }
    }
}