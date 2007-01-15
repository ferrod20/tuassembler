using System;
using System.CodeDom.Compiler;
using System.IO;

namespace TUAssembler
{
    public class Compilador
    {
        #region Variables miembro
        private string directorio;
        private string nombre;
        private string archivoSalida;
        private string archivoError;
        private TempFileCollection archivosTemporales;
        #endregion

        #region Propiedades
        public bool HuboError
        {
            get
            {
                return ObtenerError().Length > 0;
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
        public void Compilar( string comando )
        {
            string rutaCompleta;
            string cmd;
            int salida = 0;

            rutaCompleta = Path.Combine( directorio, nombre );
            cmd = "\"" + rutaCompleta + "\" " + comando;
            File.Delete( archivoSalida );
            File.Delete( archivoError );

            try
            {
                salida = Executor.ExecWaitWithCapture( cmd, archivosTemporales, ref archivoSalida, ref archivoError );
            }
            catch( Exception e )
            {
                archivoSalida = e.Message;
            }

            if( salida!=0 )
                throw new Exception( ObtenerError() );
        }
        public string ObtenerError()
        {
            StreamReader sr = new StreamReader( archivoError );
            return sr.ReadToEnd();
        }
        public string ObtenerSalida()
        {
            StreamReader sr = new StreamReader( archivoSalida );
            return sr.ReadToEnd();
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