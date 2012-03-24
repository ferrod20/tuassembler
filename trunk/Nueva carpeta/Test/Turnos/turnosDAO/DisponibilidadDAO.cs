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
    public static class DisponibilidadsDAO
    {
        public static DisponibilidadSemanal Leer(long Id)
        {
            DisponibilidadSemanal DisponibilidadBuscar = new DisponibilidadSemanal(Id);
            List<DisponibilidadSemanal> lDisponibilidads = DisponibilidadsDAO.Buscar(DisponibilidadBuscar);
            if (lDisponibilidads.Count == 1) return lDisponibilidads[0];
            return null;
        }
        static public Disponibilidad Guardar(Disponibilidad objetoAGuardar)
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
                    Disponibilidad o = new Disponibilidad(Id);
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
        static public List<Disponibilidad> Buscar(Disponibilidad DisponibilidadBuscar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                long id = DisponibilidadBuscar.Id;
                List<Disponibilidad> lRes = new List<Disponibilidad>();
                try
                {
                    DisponibilidadBuscar.dbId = DisponibilidadBuscar.Id;
                    //db.Ext().Bind(DisponibilidadBuscar, id);
                    Connect.setdbId(db, DisponibilidadBuscar);
                    object o2 = db.Ext().GetByID(id);
                    DisponibilidadBuscar = (Disponibilidad)o2;

                    /*DisponibilidadBuscar.dbId = DisponibilidadBuscar.Id;
                    if (DisponibilidadBuscar.Id>0)
                    {
                        DisponibilidadBuscar.dbId = null;
                    }*/
                    IObjectSet result = db.QueryByExample(DisponibilidadBuscar);
                    while (result.HasNext())
                    {
                        Disponibilidad o = (Disponibilidad)result.Next();
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
        static public List<Disponibilidad> BuscarOld(Disponibilidad DisponibilidadBuscar)
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                List<Disponibilidad> lRes = new List<Disponibilidad>();
                try
                {
                    DisponibilidadBuscar.dbId = DisponibilidadBuscar.Id;
                    if (DisponibilidadBuscar.Id > 0)
                    {
                        DisponibilidadBuscar.dbId = null;
                        //db.Ext().Bind(DisponibilidadBuscar, DisponibilidadBuscar.Id);
                    }
                    IObjectSet result = db.QueryByExample(DisponibilidadBuscar);
                    while (result.HasNext())
                    {
                        Disponibilidad o = (Disponibilidad)result.Next();
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
        static public List<Disponibilidad> LeerTodos()
        {
            using (IObjectContainer db = Connect.getDataBase())
            {
                List<Disponibilidad> lRes = new List<Disponibilidad>();
                try
                {
                    IObjectSet result = db.QueryByExample(typeof(Disponibilidad));
                    while (result.HasNext())
                    {
                        Disponibilidad o = (Disponibilidad)result.Next();
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
