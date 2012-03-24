using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace db4oDataHelper.Domain
{
    [Serializable]
    public abstract class DomainObject
    {
        #region Dispose

        private bool disposing;
        public void Dispose()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }

        protected virtual void Dispose(bool b)
        {
            // Si no se esta destruyendo ya…
            if (!disposing)
            {
                // La marco como desechada ó desechandose,
                // de forma que no se puede ejecutar este código
                // dos veces.
                disposing = true;

                // Indico al GC que no llame al destructor
                // de esta clase al recolectarla.
                GC.SuppressFinalize(this);

                // … libero los recursos… 
            }
        }

        ~DomainObject()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        #region Clone
        public object Clone()
        {
            // return this.MemberwiseClone(); OJO lo anterior es shallow copy ver en site:moco$oft.com
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                object a = this;
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                DomainObject cb = (DomainObject)formatter.Deserialize(stream);
                cb.dbId = null;
                return (object) cb;
            }

        }
        #endregion

        #region Errores de Clase
        public class ClaseBaseException : System.ApplicationException
        {
            public ClaseBaseException(string mensaje)
                : base(mensaje)
            {
            }
        }
        #endregion

        #region Db4o        
        public long? dbId { get; set; }
        #endregion

        /*
        #region ObjID
        public long? _ID { get; set; }
        #endregion 
         */
    }
}
