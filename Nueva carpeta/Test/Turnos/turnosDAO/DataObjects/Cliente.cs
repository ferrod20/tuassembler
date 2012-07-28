using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnosLib
{

    public class Cliente 
    {
        public long Id { get; set; }                       //Id del Cliente
        public string Nombre { get; set; }                 //Nombre de fantasia para ser mostrado
        public string Descripcion { get; set; }            //Descripcion del cliente
        public bool Activo { get; set; }
        public string Foto { get; set; }
        public string Url { get; set; }
        public bool baja { get; set; }  
        public Cliente()
        {
            Id = 0;
            Nombre = "";
            Descripcion = "";
            Activo = false;
            Foto = "";
            Url = "";
            baja = false;
        }

        public Cliente(string nombre, string descripcion, bool activo, string foto, string url)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Activo = activo;
            Foto = foto;
            Url = url;
        }

    }
    
}
