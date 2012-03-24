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
    public static class RecursosDAO
    {
        public static Recurso Leer(long Id)
        {
            Recurso RecursoBuscar = new Recurso(Id);
            List<Recurso> lRecursos = RecursosDAO.Buscar(RecursoBuscar);
            if (lRecursos.Count == 1) return lRecursos[0];
            return null;
        }
        static public Recurso Guardar(Recurso objetoAGuardar)
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
                    Recurso o = new Recurso(Id);
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
        static public List<Recurso> Buscar(Recurso RecursoBuscar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                long id = RecursoBuscar.Id;
                List<Recurso> lRes = new List<Recurso>();
                try
                {
                    //RecursoBuscar.dbId = RecursoBuscar.Id;
                    //db.Ext().Bind(RecursoBuscar, id);
                    //Connect.setdbId(db, RecursoBuscar);
                    //object o2 = db.Ext().GetByID(id);
                    //RecursoBuscar = (Recurso)o2;
                    //RecursoBuscar.dbId = RecursoBuscar.Id;
                    if (RecursoBuscar.Id>0)
                    {
                        RecursoBuscar.dbId = null;
                    }
                    IObjectSet result = db.QueryByExample(RecursoBuscar);
                    while (result.HasNext())
                    {
                        Recurso o = (Recurso)result.Next();
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
        static public List<Recurso> BuscarOld(Recurso RecursoBuscar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                List<Recurso> lRes = new List<Recurso>();
                try
                {
                    RecursoBuscar.dbId = RecursoBuscar.Id;
                    if (RecursoBuscar.Id > 0)
                    {
                        RecursoBuscar.dbId = null;
                        //db.Ext().Bind(RecursoBuscar, RecursoBuscar.Id);
                    }
                    IObjectSet result = db.QueryByExample(RecursoBuscar);
                    while (result.HasNext())
                    {
                        Recurso o = (Recurso)result.Next();
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
        static public List<Recurso> LeerTodos()
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                List<Recurso> lRes = new List<Recurso>();
                try
                {
                    IObjectSet result = db.QueryByExample(typeof(Recurso));
                    while (result.HasNext())
                    {
                        Recurso o = (Recurso)result.Next();
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
