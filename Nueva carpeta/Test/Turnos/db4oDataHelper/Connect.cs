using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using System.Linq;
using System.Linq.Expressions;
using db4oDataHelper.Domain;

namespace db4oDataHelper.DAO
{

    public static class Connect
    {

        static IObjectContainer rootContainer;

        private static string connString()
        {
            return ConfigurationManager.AppSettings["db4oFile"];
        }
        public static IObjectContainer getDataBase()
        {
            if (rootContainer == null)
                rootContainer = Db4oFactory.OpenFile(Connect.connString());
            return rootContainer.Ext().OpenSession();
        }
        //RECUPERA  EN EL ID DE LA CLASE DomainObject (mia!) Y LO GUARDA EN EL db4o.ID 
        //PARA EL Y SUS PROPIEDADES QUE SEAN DE TIPO CLASE HERENCIA DE DomainObject
        /// <summary>
        /// Toma el ID (dbID) y lo asigna al id interno del objeto db4o.
        /// </summary>
        /// <param name="db">Objeto de la base de datos db4o</param>
        /// <param name="o">Objeto al que se le va a asignar el Id desde si mismo</param>
        public static void setdbId(IObjectContainer db, object o)
        {
            if (typeof(string).IsInstanceOfType(o) || typeof(String).IsInstanceOfType(o) ||
                    typeof(char).IsInstanceOfType(o) || typeof(Char).IsInstanceOfType(o)) return;

            //SI ES UNA INSTANCIA DE "DomainObject" recupero el dbId 
            // Y LO GUARDA EN EL id de la clase "DomainObject"
            if (typeof(DomainObject).IsInstanceOfType(o) && ((DomainObject)o).dbId.HasValue )
            {
                //DomainObject c = (DomainObject)o;
                //c.dbId = db.Ext().GetID(c);
                db.Ext().Bind(o, ((DomainObject)o).dbId.Value);
                
            }

            foreach (System.Reflection.PropertyInfo ao in o.GetType().GetProperties())
            {
                if (ao.PropertyType.Name.ToLower() == "string" ||
                    ao.PropertyType.Name.ToLower() == "char")
                    continue;


                if (typeof(DomainObject).IsInstanceOfType(ao.GetValue(o, null)))
                {
                    getdbId(db, ao.GetValue(o, null));
                }
                else if (ao.PropertyType.IsClass && ao.GetValue(o, null) != null)
                {
                    lstassigndbId(db, ((IEnumerable)ao.GetValue(o, null)).OfType<object>().ToList());
                }
            }

        }
        //RECUPERA EL db4o.ID Y LO GUARDA EN EL ID DE LA CLASE DomainObject (mia!)
        //PARA EL Y SUS PROPIEDADES QUE SEAN DE TIPO CLASE HERENCIA DE DomainObject
        public static void getdbId(IObjectContainer db, object o)
        {
            //Verifica que la el objeto no sea un string
            //EL TEMA ES QUE UN "String" LO TOMABA COMO UN ARRAY DE CHAR
            //POR ESO VERIFICO QUE NO SEA NI "string", "String" NI "Char"
            //workaround 4 string[]
            if (typeof(string).IsInstanceOfType(o) ||
                    typeof(String).IsInstanceOfType(o) ||
                    typeof(char).IsInstanceOfType(o) ||
                    typeof(Char).IsInstanceOfType(o)
                ) return;

            //SI ES UNA INSTANCIA DE "DomainObject" recupero el dbId
            // Y LO GUARDA EN EL id de la clase "DomainObject"
            if (typeof(DomainObject).IsInstanceOfType(o))
            {
                if (db.Ext().GetID(o) > 0)
                {
                    //DomainObject c = (DomainObject)o;
                    ((DomainObject)o).dbId = db.Ext().GetID(o);
                }
            }

            foreach (System.Reflection.PropertyInfo ao in o.GetType().GetProperties())
            {
                if (ao.PropertyType.Name.ToLower() == "string" ||
                    ao.PropertyType.Name.ToLower() == "char")
                    continue;


                if (typeof(DomainObject).IsInstanceOfType(ao.GetValue(o, null)))
                {
                    getdbId(db, ao.GetValue(o, null));
                }
                else if (ao.PropertyType.IsClass && ao.GetValue(o, null) != null)
                {
                    lstAssignId(db, ((IEnumerable)ao.GetValue(o, null)).OfType<object>().ToList());
                }
            }

        }

        public static void lstAssignId(IObjectContainer db, List<object> lo)
        {
            foreach (object o in lo)
                getdbId(db, o);
        }
        public static void lstassigndbId(IObjectContainer db, List<object> lo)
        {
            foreach (object o in lo)
                setdbId(db, o);
        }
        #region BASURA de sebastian
        public static object getMax(IObjectContainer db, object idOb, string idfield, Hashtable constrains)
        {
            IQuery q = db.Query();
            q.Constrain(idOb.GetType());
            foreach (object key in constrains.Keys)
                q.Descend(getBackingField(key.ToString())).Constrain(constrains[key]);

            q.Descend(getBackingField(idfield)).OrderDescending();
            IObjectSet result = q.Execute();
            while (result.HasNext())
            {
                return (object)result.Next();

            }
            return null;

        }

        private static string getBackingField(string field)
        {
            return "<" + field + ">k__BackingField";
        }

        public static List<Object> resultToList(IObjectSet os)
        {
            List<Object> lstO = new List<Object>();
            while (os.HasNext())
            {
                lstO.Add(os.Next());
            }
            return lstO;
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }
        #endregion
    }

}
