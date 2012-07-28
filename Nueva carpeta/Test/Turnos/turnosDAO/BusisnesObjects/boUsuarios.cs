using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnos.DAO;
using Turnos.DTO;

namespace Turnos.BO
{
    static public class Usuarios
    {
        static public Usuario Leer(long Id)
        {
            return UsuariosDAO.Leer(Id);
        }
        static public Usuario Guardar(long? Id, 
            string Nombre,
            string Apellido,
            string Mail,
            string Password,
            string Celular)
        {
            Usuario nuevo = new Usuario();
            if (Id.HasValue)
            {
                nuevo.Id = Id.Value;
                nuevo.dbId = Id.Value;
            }
            nuevo.Nombre = Nombre;
            nuevo.Apellido = Apellido;
            nuevo.Password = Password;
            nuevo.Celular = Celular;
            nuevo.Mail = Mail;
            return UsuariosDAO.Guardar(nuevo);
        }
        static public bool Borrar(long Id)
        {
            
            return UsuariosDAO.Borrar(Id);
        }
        static public List<Usuario> LeerTodos()
        {
            return UsuariosDAO.LeerTodos();
        }
        static public List<Usuario> Buscar(long Id,
                    string Nombre,
                    string Apellido,
                    string Mail,
                    string Password,
                    string Celular)
        {
            Usuario oBuscar = new Usuario(true);
            oBuscar.Id = long.Parse(Id.ToString());
            if (!string.IsNullOrEmpty(Nombre)) oBuscar.Nombre = Nombre;
            if (!string.IsNullOrEmpty(Apellido)) oBuscar.Apellido = Apellido;
            if (!string.IsNullOrEmpty(Password)) oBuscar.Password = Password;
            if (!string.IsNullOrEmpty(Celular)) oBuscar.Celular = Celular;
            if (!string.IsNullOrEmpty(Mail)) oBuscar.Mail = Mail;
            return UsuariosDAO.Buscar(oBuscar);
        }
    }
}
