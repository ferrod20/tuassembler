using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{    

    public class Extractor
    {
        public Extractor(string archCobuild, string archSalida, string archDeInformación)
        {
            this.archCobuild = archCobuild;
            this.archSalida = archSalida;

            informaciónDeExtracción = new InformaciónDeExtracción(archDeInformación);
            inferidor = new InferiridorDeEtiquetas();
            tablaDeTraducción = new TraducciónCobuildATreebank();
        }

        #region Constantes
        private const string comienzoDeBloque = "DICTIONARY_ENTRY";
        private const string finDeBloque = "\nDI";
        #endregion

        #region Variables

        private InferiridorDeEtiquetas inferidor;
        private string archCobuild;
        private string archSalida;
        private InformaciónDeExtracción informaciónDeExtracción;
        private ITablaDeTraducción tablaDeTraducción;
        #endregion
        
        #region Métodos
        public void ExtraerLaInformaciónDeCobuild()
        {
            ExtraerYGrabarLaInformaciónDeCobuild();
            informaciónDeExtracción.GrabarEnArchivo();
        }

        /// <summary>
        /// Dada una entrada de Cobuild, extrae la información almacenada (palabras y tags)
        /// </summary>
        public string ExtraerInformaciónDeLaEntrada(string entrada)
        {
            informaciónDeExtracción.CantidadDeEntradas++;
            var líneas = entrada.Split('\n');
            var salida = new StringBuilder();
            
            if (líneas.Length > 2)
            {
                var i = 2;

                var palabra = líneas[1].Split(',')[0].TrimEnd();//Pueden venir cosas del estilo: A, a
                var formasDeLaPalabra = líneas[2].Split(',', ';').Select(forma => forma.Trim()).ToList();//A veces no hay formas de la Palabra en la entrada (ver entrada ABC)
                if (formasDeLaPalabra.Any(f => f.Split().Length > 2 || f.Contains('*') || f.Contains('!')))
                    formasDeLaPalabra.Clear();

                var palabras = formasDeLaPalabra.Unir(palabra);

                var encontréDefinición = false;
                var suméEjemplo = false;
                for (; i < líneas.Length - 1; i++)
                {
                    var línea = líneas[i].Trim();
                    var esEjemploODefinición = EsEjemploODefinición(línea, palabras);

                    if (esEjemploODefinición)
                    {
                        if (encontréDefinición)
                        {
                            if (!suméEjemplo)
                            {
                                informaciónDeExtracción.CantidadDeEntradasConEjemplo++;
                                suméEjemplo = true;
                            }

                            var etiquetaPennTreebank = ObtenerEtiquetaPennTreebank(líneas, i + 1, palabra);
                            //Busca en las próximas líneas la etiqueta Cobuild para la entrada. Es decir, una ejemplo que esté en la tabla de traducción de etiquetas. Luego traduce esa etiqueta Cobuild en la etiqueta Penn Treebak correspondiente.
                            
                            if (!string.IsNullOrEmpty(etiquetaPennTreebank))
                            {
                                inferidor.Palabra = palabra;
                                var etiquetasObtenidas = inferidor.InferirEtiquetasParaLasFormasDeLaPalabra(formasDeLaPalabra,etiquetaPennTreebank);
                                etiquetasObtenidas.AgregarSiNoExiste(palabra.ToLower(), etiquetaPennTreebank);
                                salida.Append(GenerarSalidaEtiquetada(línea, etiquetasObtenidas));
                            }
                        }
                        else
                            encontréDefinición = true;
                    }
                }
            }

            return salida.ToString();
        }

        private void ExtraerYGrabarLaInformaciónDeCobuild()
        {
            TextWriter tw = new StreamWriter(archSalida);
            var texto = File.ReadAllText(archCobuild);
            var indice = 0;
            var informaciónExtraída = string.Empty;
            
            while (indice != -1)
            {
                var entradaCobuild = ObtenerEntrada(texto, ref indice);
                if (entradaCobuild != string.Empty)
                    informaciónExtraída = ExtraerInformaciónDeLaEntrada(entradaCobuild);
                if (!string.IsNullOrEmpty(informaciónExtraída.Trim()))
                {
                    informaciónDeExtracción.CantidadDeEntradasExtraídas++;
                    tw.Write(informaciónExtraída);
                }               
            }

            tw.Close();
        }
        /// <summary>
        /// Obtiene la entrada de COBUILD a partir del indice pasado por parametro
        /// Actualiza el indice apuntando a la próxima entrada
        /// </summary>
        private string ObtenerEntrada(string texto, ref int indice)
        {
            var salida = string.Empty;
            var inicio = texto.IndexOf(comienzoDeBloque, indice);
            if (inicio != -1)
            {
                var fin = texto.IndexOf(finDeBloque, inicio);
                indice = fin;
                if (inicio != -1 && fin != -1)
                    salida = texto.Substring(inicio, fin - inicio + 1);
            }
            else
                indice = -1;

            return salida;
        }
        
        /// <summary>
        /// Indica si la linea es un ejemplo de Cobuild;
        /// "used for the", "is used in the present tense", "Someone or something that is", "If something is", "If you are" son parte de la definición
        /// '/' es utilizado en las pronunciaciones
        /// </summary>
        private bool EsEjemploODefinición(string línea, IEnumerable<string> formasDeLaPalabra)
        {
            var esEjemploODefinición =
                //!línea.EmpiezaConAlgunaDeEstas("Someone or something that is","If something is","If you are") &&
                //!línea.ContieneAlgunaDeEstas("is used in the present tense", "used for the" , "/", "*") &&
                !string.IsNullOrWhiteSpace(línea) && línea.Sum(letra => letra == ',' || letra == ';' ? 1 : 0) <= 3;
            return esEjemploODefinición && formasDeLaPalabra.Any(p => EsEjemploODefinición(línea, p));
        }

        /// <summary>
        /// Indica si la ejemplo es un ejemplo o definición de Cobuild;
        /// si la linea contiene a la Palabra 
        /// y no contiene al caracter '/' o '*' (los caracteres '/' y '*' se encuentran en la explicación de la pronunciación)
        /// y tiene al menos 2 letras más que la Palabra
        /// y tiene al menos 4 palabras
        /// y tiene como máximo 3 comas
        /// y tiene al menos una Palabra que no es la 'Palabra'
        /// y no tiene un espacio ' ' (no es una Palabra compuesta)
        /// </summary>
        private bool EsEjemploODefinición(string línea, string palabra)
        {
            var palabras = línea.Split();
            var cantPalabras = palabras.Count();

            return !palabra.Contains(' ') && línea.Contains(palabra) && línea.Length > palabra.Length + 2 && cantPalabras > 4 && cantPalabras > línea.CantidadDeOcurrencias(palabra) && palabras.All(p => !p.StartsWith("/"));
        }
        
        /// <summary>
        ///   Obtiene una lista con cada Palabra del ejemplo. En la Palabra o formas de Palabra para las que se obtuvieron etiquetas, le asigna la etiqueta correspondiente.
        ///   Tiene en cuenta signos de puntuacion al final y al principio.
        /// </summary>
        private string GenerarSalidaEtiquetada(string ejemplo, Dictionary<string, string> etiquetas)
        {
            var cantPalEtiquetadas = informaciónDeExtracción.CantidadDePalabrasEtiquetadas;
            var palabrasDelEjemplo = ejemplo.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var salida = "";

            foreach (var palabraDelEjemplo in palabrasDelEjemplo)
            {
                var palabra = palabraDelEjemplo;

                if (palabra.StartsWith("..."))
                {
                    palabra = palabra.Substring(3);
                    salida += "...\n";
                    informaciónDeExtracción.CantidadDeSignosDePuntuación++;
                }

                string puntuaciónFinal;
                var palabraSinPuntuación = palabra.HayPuntuaciónFinal(out puntuaciónFinal);

                if (palabraSinPuntuación != string.Empty )
                {
                    salida += palabraSinPuntuación;
                    informaciónDeExtracción.CantidadDePalabras++;        

                    var palabraEnMinúscula = palabraSinPuntuación.ToLower();

                    if (etiquetas.ContainsKey(palabraEnMinúscula))
                    {
                        var etiquetaPennTreebank = etiquetas[palabraEnMinúscula];
                        //var etiquetaInferida = InferirEtiqueta(palabraSinPuntuación, etiquetaPennTreebank, palabrasDelEjemplo, i);

                        salida += "\t" + etiquetaPennTreebank;
                        informaciónDeExtracción.CantidadDePalabrasEtiquetadas++;                        
                    }
                    salida += "\n";
                }

                if (puntuaciónFinal != string.Empty)
                {
                    salida += puntuaciónFinal + "\n";
                    if (puntuaciónFinal == "...")
                        salida += "\n";
                    informaciónDeExtracción.CantidadDeSignosDePuntuación++;
                }
            }

            salida += "\n";
            //Si se asginó alguna etiqueta
            return informaciónDeExtracción.CantidadDePalabrasEtiquetadas > cantPalEtiquetadas ? salida : string.Empty;
        }

        /// <summary>
        /// Busca en la entrada, (en cada renglón) si hay alguna Palabra que sea un tag COBUILD. Es decir; 
        /// si está en la tabla de traducción de etiquetas, y si es así lo traduce con la etiqueta Penn Treebank correspondiente.
        /// </summary>
        private string ObtenerEtiquetaPennTreebank(string[] líneas, int i, string palabra)
        {
            string etiquetaPennTreebankObtenida = null;

            for (; i < líneas.Length && etiquetaPennTreebankObtenida == null; i++)
            {
                var línea = líneas[i];
                if(línea!= string.Empty)
                    etiquetaPennTreebankObtenida = ObtenerEtiquetaPennTreebank(línea, palabra);
            }

            return etiquetaPennTreebankObtenida;
        }
        /// <summary>
        /// Busca en la tabla de traducción de etiquetas si existe alguna etiqueta Penn Treebank para la etiqueta Cobuild posibleEtiquetaCobuild
        /// </summary>
        private string ObtenerEtiquetaPennTreebank(string posibleEtiquetaCobuild, string palabra)
        {
            string etiquetaPennTreebank = null;
            posibleEtiquetaCobuild = posibleEtiquetaCobuild.TrimEnd();
            var posiblesEtiquetasPennTreebank = tablaDeTraducción.ObtenerTraduccionesParaEtiquetaQueEmpiezaCon(posibleEtiquetaCobuild);

            if (tablaDeTraducción.ContieneTraduccionPara(posibleEtiquetaCobuild))
                etiquetaPennTreebank = tablaDeTraducción.ObtenerTraduccionPara(posibleEtiquetaCobuild).First();
            else if (posiblesEtiquetasPennTreebank.Any())
                etiquetaPennTreebank = posiblesEtiquetasPennTreebank.First();
            else if (posibleEtiquetaCobuild.StartsWith("countable noun") || posibleEtiquetaCobuild.StartsWith("uncountable noun"))
                etiquetaPennTreebank = "NN";
            else if (posibleEtiquetaCobuild.StartsWith("verb"))
                etiquetaPennTreebank = "VB";
            else if (posibleEtiquetaCobuild.StartsWith("qualitative adjective") || posibleEtiquetaCobuild.StartsWith("classifying adjective"))
                etiquetaPennTreebank = "JJ";

            if (etiquetaPennTreebank != null)
            {
                informaciónDeExtracción.AgregarEtiquetaCobuild(posibleEtiquetaCobuild, palabra);
                informaciónDeExtracción.AgregarEtiquetaTreebank(etiquetaPennTreebank, palabra);
            }
                
            //if (etiquetaPennTreebank != null && posibleEtiquetaCobuild.Length <= 70)
            //    if (infoEtiquetasCobuild.ContainsKey(posibleEtiquetaCobuild))
            //        infoEtiquetasCobuild[posibleEtiquetaCobuild]++;
            //else
            //        infoEtiquetasCobuild[posibleEtiquetaCobuild] = 1;


            return etiquetaPennTreebank;
        }
        #endregion
    }
}