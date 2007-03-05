using System;
using System.CodeDom.Compiler;
using System.IO;

namespace TUAssembler.Compilacion
{
    public class Compilador
    {
        #region Variables miembro
        private string directorio;
        private string nombre;
        private string archivoSalida;
        private string archivoError;
        private string error;
        private string salida;
        private TempFileCollection archivosTemporales;
        #endregion

        #region Propiedades
        public string Salida
        {
            get
            {
                if( salida==null )
                    ObtenerSalida();
                return error;
            }
        }

        public string Error
        {
            get
            {
                if( error==null )
                    ObtenerError();
                return error;
            }
        }

        public bool HuboError
        {
            get
            {
                return Error.Length > 0;
            }
        }

        public TempFileCollection ArchivosTemporales
        {
            get
            {
                return archivosTemporales;
            }
        }

        public string Directorio
        {
            get
            {
                return directorio;
            }
            set
            {
                directorio = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        #endregion

        #region Métodos
        public void Enlazar( string ejecutable, params string[] archivosObjeto )
        {
            string comando;
            comando = "-o " + ejecutable;
            foreach( string archivo in archivosObjeto )
                comando += " " + archivo;

            Compilar( comando );
        }
        public void Compilar( string comando )
        {
            string rutaCompleta;
            string cmd = string.Empty;
            int salida = 0;

            try
            {
                rutaCompleta = Path.Combine( directorio, nombre );
                // cmd = "\"" + rutaCompleta + "\" " + comando;
                cmd = rutaCompleta + " " + comando;
                BorrarArchivosSalidaYError();
                salida = Executor.ExecWaitWithCapture( cmd, archivosTemporales, ref archivoSalida, ref archivoError );
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorAlCompilar( this, cmd, e ) );
            }
            if( salida!=0 )
                throw new Exception( Mensajes.ErrorAlCompilar( this, cmd, Error ) );
        }
        private void ObtenerError()
        {
            StreamReader sr = new StreamReader( archivoError );
            error = sr.ReadToEnd();
            sr.Close();
        }
        private void ObtenerSalida()
        {
            StreamReader sr = new StreamReader( archivoSalida );
            salida = sr.ReadToEnd();
            sr.Close();
        }
        public void BorrarArchivosSalidaYError()
        {
            File.Delete( archivoSalida );
            File.Delete( archivoError );
        }
        #endregion

        #region Constructor
        public Compilador( string directorio, string nombre, string archivoSalida, string archivoError )
        {
            archivosTemporales = new TempFileCollection();
            this.directorio = directorio;
            this.nombre = nombre;
            this.archivoSalida = archivoSalida;
            this.archivoError = archivoError;
        }
        #endregion
    }
}