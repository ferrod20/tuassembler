using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turnos.DTO
{
    public class Usuario : db4oDataHelper.Domain.DomainObject
    {
        public long Id { get; set; }                 
        public string Nombre { get; set; }                 //Nombre del usuario
        public string Apellido { get; set; }               //Apellido del usuario
        public string Mail { get; set; }                   //Email
        public string Password { get; set; }               //Password que indica (opcional)
        public string Celular { get; set; }                //Celular indicado para el registro
        public  Usuario()
        {
            Id = 0;
            Nombre = "";
            Apellido = "";
            Mail = "";
            Password = "";
            Celular = "";
        }
        public Usuario(bool vacio)
        {
            if (!vacio)
            {
                Id = 0;
                Nombre = "";
                Apellido = "";
                Mail = "";
                Password = "";
                Celular = "";
            }
        }
        public Usuario(long Id )
        {
            this.Id  = Id;
            this.dbId = Id;
        }
        public Usuario(Int32 Id, string Nombre, string Apellido, string Mail, string Password, string Celular)
        {
            this.Id = 0;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Mail = Mail;
            this.Password = Password;
            this.Celular = Celular;
        }

    }
}
