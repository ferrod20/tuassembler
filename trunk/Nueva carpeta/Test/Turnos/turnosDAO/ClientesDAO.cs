using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using db4oDataHelper.DAO;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using Turnos.DTO;

namespace Turnos.DAO
{
    public static class ClientesDAO
    {
        public static Cliente Leer(long Id)
        {
            Cliente ClienteBuscar = new Cliente(Id);
            List<Cliente> lClientes = ClientesDAO.Buscar(ClienteBuscar);
            if (lClientes.Count == 1) return lClientes[0];
            return null;
        }
        static public Cliente Guardar(Cliente objetoAGuardar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                try
                {
                    if (objetoAGuardar.dbId > 0)
                    {
                        //Asigna el ID interno de la clase con el 
                        //de la instancia del objeto  asi evito duplicar objetos
                        Connect.setdbId(db, objetoAGuardar);
                    }
                    db.Store(objetoAGuardar);
                    db.Commit();
                    //Si es un alta, recupero nuevamente el objeto y le seteo el campo Id
                    //Hago esta grasada porque sino no puedo recuperar por el Id!
                    if (objetoAGuardar.Id == 0)
                    {
                        objetoAGuardar.Id = db.Ext().GetID(objetoAGuardar);
                        db.Store(objetoAGuardar);
                        db.Commit();
                    }
                    return objetoAGuardar;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        static public bool Borrar(long Id)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                try
                {
                    Cliente o = new Cliente(Id);
                    Connect.setdbId(db, o);
                    db.Delete(o);
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        static public List<Cliente> Buscar(Cliente ClienteBuscar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                long id = ClienteBuscar.Id;
                List<Cliente> lRes = new List<Cliente>();
                try
                {
                    /*ClienteBuscar.dbId = ClienteBuscar.Id;
                    //db.Ext().Bind(ClienteBuscar, id);
                    Connect.setdbId(db, ClienteBuscar);
                    object o2 = db.Ext().GetByID(id);
                    ClienteBuscar = (Cliente)o2;

                    ClienteBuscar.dbId = ClienteBuscar.Id;*/
                    if (ClienteBuscar.Id>0)
                    {
                        ClienteBuscar.dbId = null;
                    }
                    IObjectSet result = db.QueryByExample(ClienteBuscar);
                    while (result.HasNext())
                    {
                        Cliente o = (Cliente)result.Next();
                        Connect.getdbId(db, (object)o);
                        lRes.Add(o);

                    }

                    return lRes;
                }
                finally
                {
                    db.Close();
                }
            }
            return null;
        }
        static public List<Cliente> BuscarOld(Cliente ClienteBuscar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                List<Cliente> lRes = new List<Cliente>();
                try
                {
                    ClienteBuscar.dbId = ClienteBuscar.Id;
                    if (ClienteBuscar.Id > 0)
                    {
                        ClienteBuscar.dbId = null;
                        //db.Ext().Bind(ClienteBuscar, ClienteBuscar.Id);
                    }
                    IObjectSet result = db.QueryByExample(ClienteBuscar);
                    while (result.HasNext())
                    {
                        Cliente o = (Cliente)result.Next();
                        Connect.getdbId(db, (object)o);
                        lRes.Add(o);

                    }

                    return lRes;
                }
                finally
                {
                    db.Close();
                }
            }
            return null;
        }
        static public List<Cliente> LeerTodos()
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                List<Cliente> lRes = new List<Cliente>();
                try
                {
                    IObjectSet result = db.QueryByExample(typeof(Cliente));
                    while (result.HasNext())
                    {
                        Cliente o = (Cliente)result.Next();
                        Connect.getdbId(db, (object)o);
                        lRes.Add(o);

                    }

                    return lRes;
                }
                finally
                {
                    db.Close();
                }
            }
            return null;
        }
    }
}
