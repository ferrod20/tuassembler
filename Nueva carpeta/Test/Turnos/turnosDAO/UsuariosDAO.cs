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
    public static class UsuariosDAO
    {
        public static Usuario Leer(long Id)
        {
            Usuario usuarioBuscar = new Usuario(Id);
            List<Usuario> lUsuarios = UsuariosDAO.Buscar(usuarioBuscar);
            if (lUsuarios.Count == 1) return lUsuarios[0];
            return null;
        }
        static public Usuario Guardar(Usuario objetoAGuardar)
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
                    Usuario o = new Usuario(Id);
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
        static public List<Usuario> Buscar(Usuario usuarioBuscar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                //long id = usuarioBuscar.Id;
                List<Usuario> lRes = new List<Usuario>();
                try
                {
                    
                    
                    //db.Ext().Bind(usuarioBuscar, id);
                    //Connect.setdbId(db, usuarioBuscar);
                    //object o2 = db.Ext().GetByID(id);
                    //usuarioBuscar = (Usuario) o2;
                    //usuarioBuscar.dbId = usuarioBuscar.Id;
                    if (usuarioBuscar.Id>0)
                    {
                        usuarioBuscar.dbId = null;
                    }
                    IObjectSet result = db.QueryByExample(usuarioBuscar);
                    while (result.HasNext())
                    {
                        Usuario o = (Usuario)result.Next();
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
        static public List<Usuario> BuscarOld(Usuario usuarioBuscar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                List<Usuario> lRes = new List<Usuario>();
                try
                {
                    usuarioBuscar.dbId = usuarioBuscar.Id;
                    if (usuarioBuscar.Id > 0)
                    {
                        usuarioBuscar.dbId = null;
                        //db.Ext().Bind(usuarioBuscar, usuarioBuscar.Id);
                    }
                    IObjectSet result = db.QueryByExample(usuarioBuscar);
                    while (result.HasNext())
                    {
                        Usuario o = (Usuario)result.Next();
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
        static public List<Usuario> LeerTodos()
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                List<Usuario> lRes = new List<Usuario>();
                try
                {
                  IObjectSet result = db.QueryByExample(typeof(Usuario));
                    while (result.HasNext())
                    {
                        Usuario o = (Usuario)result.Next();
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
