using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiciosWCF
{
    //Voy ha crear una enumeración para el error
    public enum enumTipoError
    { 
        CategoriaNoEncontrada,
        CategoriaErronea,
        Otros
    }

    public class ClaseError
    {
        public enumTipoError Error { get; set; }
        public string Mensaje { get; set; }
        public string Datos { get; set; }
    }
}
