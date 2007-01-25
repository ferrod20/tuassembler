using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TUAssembler.Definicion;
using TUAssembler.JuegoDePrueba;
using Parametro=TUAssembler.Definicion.Parametro;

namespace TUAssembler.Generacion
{
    class Generador
    {
        #region Variables miembro
        private string archivoDefinicion;
        private string archivoPrueba;
        private DefinicionFuncion definicion;
        private Prueba prueba;
        #endregion

        #region Propiedades
        public string ArchivoDefinicion
        {
            get
            {
                return archivoDefinicion;
            }
            set
            {
                archivoDefinicion = value;
            }
        }

        public string ArchivoPrueba
        {
            get
            {
                return archivoPrueba;
            }
            set
            {
                archivoPrueba = value;
            }
        }

        public DefinicionFuncion Definicion
        {
            get
            {
                return definicion;
            }
            set
            {
                definicion = value;
            }
        }

        public Prueba Prueba
        {
            get
            {
                if (prueba == null)
                    prueba = new Prueba();                
                return prueba;
            }
            set
            {
                prueba = value;
            }
        }
        #endregion

        #region Métodos
        public void LeerDefinicion()
        {
            Definicion = DefinicionFuncion.Leer( archivoDefinicion );
        }
        public void LeerPrueba()
        {
            StreamReader lector = new StreamReader( archivoPrueba );
            LeerSalida( lector );
            LeerEntrada(lector );            
        }
        private void LeerEntrada( StreamReader lector )
        {
            Parametro[] defParametros;
            string linea;
            int i = 0;
            
            defParametros = Definicion.ParametrosEntrada;
            Prueba.Entrada = new JuegoDePrueba.Parametro[defParametros.Length];
            
            foreach (Parametro defParametro in defParametros)
            {
                linea = lector.ReadLine();
                if (linea == string.Empty)
                    throw new Exception( Mensajes.CantidadParametrosEntradaNoCoincideConDefinicion );
                Prueba.Entrada[i] = ObtenerParametro(linea, defParametro );
                    i++;
            }            
        }
        private void LeerSalida(StreamReader lector )
        {
            Parametro[] defParametros, defParametrosEntrada;
            string linea;
            int i, cuantos;            
        
            //-----------------------Obtengo parametros de salida y los de ES o S
            cuantos = Definicion.CuantosParametrosESoS();
            defParametros = new Parametro[cuantos + 1];
            defParametros[0] = Definicion.ParametroSalida;
            defParametrosEntrada = Definicion.ObtenerParametrosESoS();
            
            for (i = 1; i < cuantos + 1; i++ )            
                defParametros[i] = defParametrosEntrada[i];
            //-----------------------Obtengo parametros de salida y los de ES o S

            
            Prueba.Salida = new JuegoDePrueba.Parametro[defParametros.Length];
            
            foreach (Parametro defParametro in defParametros)
            {
                linea = lector.ReadLine();
                if (linea == string.Empty)
                    throw new Exception(Mensajes.CantidadParametrosEntradaNoCoincideConDefinicion);
                Prueba.Salida[i] = ObtenerParametro(linea, defParametro);
                i++;
            }             
        }
        private JuegoDePrueba.Parametro ObtenerParametro( string linea, Parametro defParam )
        {
            Matriz matriz;
            Vector vector;
            string[] parametros;
            JuegoDePrueba.Parametro salida = null;
            if (defParam.EsMatriz)
            {
                matriz = new Matriz(defParam.CantFilas, defParam.CantColumnas);
                matriz.Leer(linea, defParam.Tipo);//Lee la salida y verifica que los parametros sean del tipo valido.
                salida = matriz;
            }
            if (defParam.EsVector)
            {
                vector = new Vector(defParam.Longitud);
                vector.Leer(linea, defParam.Tipo);//Lee la salida y verifica que los parametros sean del tipo valido.
                salida = vector;
            }
            if (defParam.EsElemento)
            {
                parametros = linea.Split(' ');
                if (parametros.Length != 1)
                    throw new Exception(Mensajes.CantidadDeParametrosNoCoincidenConDefinicion);
                salida = new Elem(parametros[0]);
            }
            return salida;
        }
        #endregion

        #region Constructor
        public Generador( string archivoDefinicion, string archivoPrueba )
        {
            this.archivoDefinicion = archivoDefinicion;
            this.archivoPrueba = archivoPrueba;
        }
        #endregion
    }
}
