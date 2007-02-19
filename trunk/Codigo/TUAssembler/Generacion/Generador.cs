using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;
using TUAssembler.JuegoDePrueba;

namespace TUAssembler.Generacion
{
    internal class Generador
    {
        #region Variables miembro
        private string archivoDefinicion;
        private string archivoPrueba;
        private DefinicionFuncion definicion;
        private Prueba[] pruebas;
        private int cantPruebas;
        private int pruebaActual;
        #endregion

        #region Propiedades
        public int CantPruebas
        {
            get
            {
                return cantPruebas;
            }
            set
            {
                cantPruebas = value;
            }
        }

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

        public Prueba[] Pruebas
        {
            get
            {
                if( pruebas==null )
                {
                    pruebas = new Prueba[cantPruebas];
                    for( int i = 0; i < cantPruebas; i++ )
                        pruebas[i] = new Prueba();
                }
                return pruebas;
            }
            set
            {
                pruebas = value;
            }
        }

        public Prueba PruebaActual
        {
            get
            {
                return Pruebas[pruebaActual];
            }
            set
            {
                Pruebas[pruebaActual] = value;
            }
        }
        #endregion

        #region Métodos

        #region Generacion de codigo para la prueba
        public void GenerarPrueba( ref EscritorC escritor )
        {
            escritor.WriteLine( "#include <stdio.h>" );
            escritor.WriteLine( "#define bool int" );
            escritor.WriteLine( "#define true 1" );
            escritor.WriteLine( "#define false 0" );
            EscribirReferenciaExternaDeLaFuncion( ref escritor );
            EscribirMallocFreeFunctions( ref escritor );
            EscribirFuncionesDePrueba( ref escritor );
            EscribirMain( ref escritor );
        }
        private void EscribirReferenciaExternaDeLaFuncion( ref EscritorC escritor )
        {
            escritor.WriteLine( "extern " + Definicion.GenerarPrototipo() + ";" );
        }
        private void EscribirMallocFreeFunctions( ref EscritorC escritor )
        {
            //El uso es el siguiente:
            // - La funcion a probar debe llamar a malloc2 con la cantidad de bytes que se quieren pedir.
            // - Cuando ya no necesite la memoria llamara a free2 pasandole de parametro el puntero devuelto por malloc2.
            // - Desde fuera de la funcion se liberara realmente toda la memoria pedida llamando a free2all.
            // Ej:
            // void prueba(){
            //    char* c = malloc2(10);
            //    printf("Hola!");
            //    free2(c);
            // }
            //
            // int main(){
            //    prueba();
            //    free2all();
            //    return 0;
            // }
            escritor.IdentacionActiva = false;
            escritor.WriteLine();
            escritor.WriteLine( "int cantPedidosMemoria = 0;" );
            escritor.WriteLine( "char* pedidos[sizeof(int)*10000];" );
            escritor.WriteLine( "int tamanioPedidos[sizeof(int)*10000];" );
            escritor.WriteLine( "bool fueLiberado[sizeof(bool)*10000];" );
            escritor.WriteLine();
            escritor.WriteLine( "char* malloc2(int cantBytes){" );
            escritor.WriteLine( "   int i;" );
            escritor.WriteLine( "   char* ret_value;" );
            escritor.WriteLine(
                "   ret_value = malloc(cantBytes + 8 + 8);	//8 bytes antes y 8 bytes despues para controlar que no se pase de la longitud del buffer" );
            escritor.WriteLine( "   pedidos[cantPedidosMemoria] = ret_value;" );
            escritor.WriteLine( "   tamanioPedidos[cantPedidosMemoria] = cantBytes;" );
            escritor.WriteLine( "   fueLiberado[cantPedidosMemoria] = false;" );
            escritor.WriteLine( "   for(i=0; i<8; i++){" );
            escritor.WriteLine( "       ((char*)ret_value)[i] = 'A';" );
            escritor.WriteLine( "       ((char*)ret_value)[cantBytes + 8 + i] = 'A';" );
            escritor.WriteLine( "   }" );
            escritor.WriteLine( "   cantPedidosMemoria++;" );
            escritor.WriteLine( "   return ret_value + 8;" );
            escritor.WriteLine( "}" );
            escritor.WriteLine();
            escritor.WriteLine( "void free2(char* punteroABloque)" );
            escritor.WriteLine( "{" );
            escritor.WriteLine( "   int pos, i;" );
            escritor.WriteLine( "   for(pos=0; pos<cantPedidosMemoria && pedidos[pos]!=punteroABloque-8; pos++);" );
            escritor.WriteLine( "       if(pedidos[pos] !=punteroABloque-8)" );
            escritor.WriteLine( "           printf(\"Se intento liberar una posicion de memoria no valida\");" );
            escritor.WriteLine( "       else{" );
            escritor.WriteLine( "           fueLiberado[pos] = true;" );
            escritor.WriteLine( "           for (i = 0; i < 8; i++)" );
            escritor.WriteLine(
                "               if(((char*)punteroABloque-8)[i] != 'A'|| ((char*)punteroABloque)[tamanioPedidos[pos] + i] != 'A'){" );
            escritor.WriteLine( "                   printf(\"Se ha escrito fuera del buffer\");" );
            escritor.WriteLine( "                   break;" );
            escritor.WriteLine( "               }" );
            escritor.WriteLine( "       }" );
            escritor.WriteLine( "}" );
            escritor.WriteLine();
            escritor.WriteLine( "void free2all(){" );
            escritor.WriteLine( "   int i;" );
            escritor.WriteLine( "   int bytesNoLiberados = 0;" );
            escritor.WriteLine( "   for(i=0; i<cantPedidosMemoria; i++){" );
            escritor.WriteLine( "       free(pedidos[i]);" );
            escritor.WriteLine( "       if(fueLiberado[i]== false)" );
            escritor.WriteLine( "           bytesNoLiberados = bytesNoLiberados + tamanioPedidos[i];" );
            escritor.WriteLine( "   }" );
            escritor.WriteLine( "   if(bytesNoLiberados >0)" );
            escritor.WriteLine( "       printf(\"No se han liberado %d bytes de memoria\", bytesNoLiberados);" );
            escritor.WriteLine( "}" );
            escritor.WriteLine();
            escritor.IdentacionActiva = true;
        }
        private void EscribirFuncionesDePrueba( ref EscritorC escritor )
        {
            foreach( Prueba prueba in Pruebas )
            {
                Mensajes.NombreDePrueba = prueba.Nombre;
                EscribirFuncionPrueba( ref escritor );
                pruebaActual++;
            }
        }
        private void EscribirMain( ref EscritorC escritor )
        {
            escritor.WriteLine( "int main()" );
            escritor.AbrirCorchetes();
            escritor.WriteLine( "/*------------Parametros-------------------------*/" );
            escritor.WriteLine( "int cantErrores = 0;" );
            escritor.WriteLine( "/*------------Llamada a pruebas------------------*/" );
            foreach( Prueba prueba in Pruebas )
            {
                escritor.If( "cantErrores == 0" );
                escritor.WriteLine( "cantErrores = " + prueba.Nombre + "();" );
                escritor.FinIf();
            }
            escritor.CerrarCorchetes();
        }
        private void EscribirFuncionPrueba( ref EscritorC escritor )
        {
            escritor.WriteLine( PruebaActual.Prototipo );
            escritor.AbrirCorchetes();
            escritor.WriteLine( "/*------------Parametros-------------------------*/" );
            DeclararParametros( ref escritor );
            escritor.WriteLine( "int cantErrores = 0;" );
            escritor.WriteLine( "/*------------Instanciacion----------------------*/" );
            InstanciarParametros( ref escritor );
            escritor.WriteLine( "/*------------LlamadaFuncion---------------------*/" );
            LlamarFuncionAProbar( ref escritor );
            escritor.WriteLine( "/*------------Comparacion de valores-------------*/" );
            CompararValoresDevueltos( ref escritor );
            escritor.WriteLine();
            escritor.WriteLine( Mensajes.PrintfPruebaConcluida() );
            escritor.WriteLine( "return cantErrores;" );
            escritor.CerrarCorchetes();
        }
        private void CompararValoresDevueltos( ref EscritorC escritor )
        {
            //Comparo los valores de los parametros de salida y los de ES

            foreach( Parametro param in PruebaActual.ParametrosSalida )
                param.CompararValor( ref escritor );
        }
        private void InstanciarParametros( ref EscritorC escritor )
        {
            string instanciacion;

            //Instancio el valor que debe devolver la funcion para compararlo despues
            //instanciacion = Prueba.ParametrosSalida[0].Instanciar();
            //escritor.WriteLine( instanciacion );

            //Instancio el valor de cada uno de los parametros de ParametrosEntrada que son de salida para pasarselo a la funcion.
            foreach( Parametro param in PruebaActual.ParametrosEntrada )
                if( param.Definicion.EntradaSalida!=EntradaSalida.S )
                {
                    instanciacion = param.Instanciar();
                    escritor.WriteLine( instanciacion );
                }
        }
        private void LlamarFuncionAProbar( ref EscritorC escritor )
        {
            string llamada = "";
            if( Definicion.DefParametroSalida!=null )
                llamada = Definicion.DefParametroSalida.Nombre + " = ";
            llamada += Definicion.Nombre + "( ";
            foreach( Parametro param in PruebaActual.ParametrosEntrada )
                llamada += param.Definicion.Nombre + ", ";
            if( PruebaActual.ParametrosEntrada.Length > 0 ) //Si hay parametros de entrada
                llamada = llamada.Remove( llamada.Length - 2, 2 ); //Elimino la última coma.)
            llamada += " );";
            escritor.WriteLine( llamada );
        }
        private void DeclararParametros( ref EscritorC escritor )
        {
            string declaracion;
            declaracion = PruebaActual.ParametrosSalida[0].Declarar();
            escritor.WriteLine( declaracion );
            foreach( Parametro param in PruebaActual.ParametrosEntrada )
            {
                declaracion = param.Declarar();
                escritor.WriteLine( declaracion );
            }
        }
        #endregion

        #region Lectura de parámetros y funcion
        public void LeerDefinicion()
        {
            Definicion = DefinicionFuncion.Leer( archivoDefinicion );
        }
        public void LeerPrueba()
        {
            StreamReader lector = new StreamReader( archivoPrueba );
            PruebaActual.LeerNombre( lector );
            PruebaActual.GenerarParametros( Definicion );
                //Hace un new de cada parametro( Matriz, Vector o Elem ) segun lo que indica Definicion.
            PruebaActual.LeerParametros( lector );
        }
        #endregion

        #endregion

        #region Constructor
        public Generador( string archivoDefinicion, string archivoPrueba )
        {
            this.archivoDefinicion = archivoDefinicion;
            this.archivoPrueba = archivoPrueba;
            pruebaActual = 0;
            cantPruebas = 1;
        }
        #endregion
    }
}