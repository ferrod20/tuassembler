using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnos.DAO;
using Turnos.DTO;

namespace Turnos.BO
{
    static public class Clientes
    {
        static public Cliente Leer(long Id)
        {
            return ClientesDAO.Leer(Id);
        }
        static public Cliente Guardar(long? Id,
            string Nombre,
            string Descripcion)
        {
            Cliente nuevo = new Cliente();
            if (Id.HasValue)
            {
                nuevo.Id = Id.Value;
                nuevo.dbId = Id.Value;
            }
            nuevo.Nombre = Nombre;
            nuevo.Descripcion = Descripcion;
            return ClientesDAO.Guardar(nuevo);
        }
        static public bool Borrar(long Id)
        {

            return ClientesDAO.Borrar(Id);
        }
        static public List<Cliente> LeerTodos()
        {
            return ClientesDAO.LeerTodos();
        }
        static public List<Cliente> Buscar(long Id,
                    string Nombre,
                    string Descripcion)
        {
            Cliente oBuscar = new Cliente(true);
            oBuscar.Id = long.Parse(Id.ToString());
            if (!string.IsNullOrEmpty(Nombre)) oBuscar.Nombre = Nombre;
            if (!string.IsNullOrEmpty(Descripcion)) oBuscar.Descripcion = Descripcion;
            return ClientesDAO.Buscar(oBuscar);
        }
    }
}
