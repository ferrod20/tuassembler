using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turnos.DTO
{

    public class Cliente : db4oDataHelper.Domain.DomainObject
    {
        public long Id { get; set; }                       //Id del Cliente
        public string Nombre { get; set; }                 //Nombre de fantasia para ser mostrado
        public string Descripcion { get; set; }            //Descripcion del cliente
        public Cliente()
        {
            Id = 0;
            Nombre = "";
            Descripcion = "";
        }
        public Cliente(bool vacio)
        {
            if (!vacio)
            {
                Id = 0;
                Nombre = "";
                Descripcion = "";
            }
        }
        public Cliente(long Id )
        {
            this.Id  = Id;
            this.dbId = Id;
        }
        public Cliente(Int32 Id, string Nombre, string Descripcion)
        {
            this.Id = 0;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
        }

    }
    
}
