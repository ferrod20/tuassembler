using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Extractor
    {
        #region Constantes
        private const string comienzoDeBloque = "DICTIONARY_ENTRY";
        private const string finDeBloque = "\nDI";
        #endregion

        #region Variables de clase
        /// <summary>
        /// Tabla para traducir etiquetas COBUILD en etiquetas Penn TreeBank
        /// </summary>
        private static readonly Dictionary<string, string> tablaDeTraducciónDeEtiquetas = new Dictionary<string, string> {
        {"coordinating conjunction", "CC"}, 
        {"number", "CD"}, 
        {"determiner", "DT"}, 
        {"determiner + countable noun in singular", "DT"}, 
        {"preposition", "IN"}, 
        {"subordinating conjunction", "IN"}, 
        {"preposition, or adverb after verb", "IN"}, 
        {"preposition after noun", "IN"}, 
        {"adjective", "JJ"}, //ver como clasificar JJR y JJS
        {"classifying adjective", "JJ"}, 
        {"qualitative adjective", "JJ"}, 
        {"adjective colour", "JJ"}, 
        {"ordinal", "JJ"}, 
        {"adjective after noun", "JJ"}, 
        {"modal", "MD"}, 
        {"adverb", "RB"}, 
        {"noun", "NN"}, //ver q hacer con plurales....
        {"countable noun", "NN"}, 
        {"uncountable noun", "NN"}, 
        {"noun singular", "NN"}, 
        {"countable or uncountable noun", "NN"}, 
        {"countable noun with supporter", "NN"}, 
        {"uncountable or countable noun", "NN"}, 
        {"noun singular with determiner", "NN"}, 
        {"mass noun", "NN"}, 
        {"uncountable noun with supporter", "NN"}, 
        {"partitive noun", "NN"}, 
        {"noun singular with determiner with supporter", "NN"}, 
        {"countable noun + of", "NN"}, 
        {"countable noun, or by + noun", "NN"}, 
        {"countable noun or partitive noun", "NN"}, 
        {"count or uncountable noun", "NN"}, 
        {"countable noun or vocative", "NN"}, 
        {"partitive noun + uncountable noun", "NN"}, 
        {"noun singular with determiner + of", "NN"}, 
        {"noun in titles", "NN"}, //Verificar
        {"noun vocative", "NN"}, 
        {"uncountable noun + of", "NN"}, 
        {"indefinite pronoun", "NN"}, 
        {"uncountable noun, or noun singular", "NN"}, 
        {"countable noun, or in + noun", "NN"}, 
        {"partitive noun + noun in plural", "NN"}, //Ver
        {"countable or uncountable noun with supporter", "NN"}, 
        {"uncountable noun, or noun before noun", "NN"}, 
        {"uncountable or countable noun with supporter", "NN"}, 
        {"noun before noun", "NN"}, 
        {"noun plural with supporter", "NNS"}, 
        {"noun in names", "NNP"}, //Verificar
        {"proper noun or vocative", "NNP"}, 
        {"proper noun", "NNP"}, 
        {"noun plural", "NNS"}, 
        {"predeterminer", "PDT"}, //si empieza con predeterminer....
        {"pronoun", "PP"}, //Ver.....me parece q no va
        {"possessive", "PPS"}, //si empieza con possessive....
        {"adverb with verb", "RB"}, 
        {"adverb after verb", "RB"}, 
        {"sentence adverb", "RB"}, 
        {"adverb + adjective or adverb", "RB"}, 
        {"adverb + adjective", "RB"}, 
        {"preposition or adverb", "RB"}, 
        {"adverb after verb, or classifying adjective", "RB"}, 
        {"adverb or sentence adverb", "RB"}, 
        {"adverb with verb, or sentence adverb", "RB"}, 
        {"exclamation", "UH"}, 
        {"exclam", "UH"}, 
        {"verb", "VB"}, 
        {"verb + object", "VB"}, 
        {"verb or verb + object", "VB"}, 
        {"ergative verb", "VB"}, 
        {"verb + adjunct", "VB"}, 
        {"verb + object + adjunct", "VB"}, 
        {"verb + object (noun group or reflexive)", "VB"}, 
        {"verb + object or reporting clause", "VB"}, 
        {"verb + object (reflexive)", "VB"}, 
        {"verb + adjunct (^i{to^i})", "VB"}, 
        {"verb + object, or phrasal verb", "VB"}, 
        {"verb + to-infinitive", "VB"}, 
        {"verb or verb + adjunct (^i{with)", "VB"}, 
        {"verb + object, verb + object + object, or verb + object + adjunct (to)", "VB"}, 
        {"ergative verb + adjunct", "VB"}, 
        {"verb + object + adjunct (to)", "VB"}, 
        {"verb + object, or verb + adjunct", "VB"}, 
        {"verb + object + adjunct (with)", "VB"}, 
        {"verb + adjunct (with)", "VB"}, 
        {"verb + complement", "VB"}, 
        {"verb + object, or verb", "VB"}, 
        {"verb + object + to-infinitive", "VB"}, 
        {"verb + reporting clause", "VB"}, 
        {"verb or ergative verb", "VB"}, 
        {"verb + adjunct (from)", "VB"}, 
        {"verb + object, verb + object + object, or verb + object + adjunct (for)", "VB"}, 
        {"wh: used as determiner", "WDT"}, 
        {"wh: used as relative pronoun", "WP"}, 
        {"wh: used as pronoun", "WP"}, 
        {"wh: used as adverb", "WRB"}, 
        {"phrase + noun group", ""}, 
        {"convention", ""}, 
        {"combining form", ""}, 
        {"prefix", ""}, 
        {"phrasal verb", ""}, 
        {"other", ""}, 
        {"phrase", ""}, 
        {"suffix", ""}, 
        {"wh", ""}, 
        {"phrase after noun", ""}, 
        {"phrase + reporting clause", ""}};
        #endregion

        private static int CantidadDeSignosDePuntuación = 0;
        private static int CantidadDePalabras = 0;
        private static int CantidadDePalabrasEtiquetadas = 0;
        #region Metodos
        public static void ExtraerLaInformaciónDeCobuild(string archCobuild, string archSalida)
        {
            var texto = File.ReadAllText(archCobuild);
            TextWriter tw = new StreamWriter(archSalida);
            ExtraerLaInformaciónDeCobuild(texto, tw);
            tw.WriteLine("Cantidad de palabras y signos puntuación: " + CantidadDePalabras + CantidadDeSignosDePuntuación);
            tw.WriteLine("Cantidad de signos de puntuación: " + CantidadDeSignosDePuntuación);
            tw.WriteLine("Cantidad de palabras: " + CantidadDePalabras);           
            tw.WriteLine("Cantidad de palabras etiquetadas: " + CantidadDePalabrasEtiquetadas);            
            tw.Close();
        }
        private static void ExtraerLaInformaciónDeCobuild(string texto, TextWriter tw)
        {
            var indice = 0;
            string entradaCobuild, informaciónExtraída = string.Empty;

            while (indice != -1)
            {
                entradaCobuild = ObtenerEntrada(texto, ref indice);
                if (entradaCobuild != string.Empty)
#if HacerLegible
                    	datos = HacerLegiblElBloque(bloque);
#else
                    informaciónExtraída = ExtraerInformaciónDeLaEntrada(entradaCobuild);
                if (!string.IsNullOrEmpty(informaciónExtraída))
                    tw.Write(informaciónExtraída);
#endif
            }
        }
        /// <summary>
        /// Obtiene la entrada de COBUILD a partir del indice pasado por parametro
        /// Actualiza el indice apuntando a la próxima entrada
        /// </summary>
        private static string ObtenerEntrada(string texto, ref int indice)
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
        /// Dada una entrada de Cobuild, extrae la información almacenada (palabras y tags)
        /// </summary>
        private static string ExtraerInformaciónDeLaEntrada(string entrada)
        {
            var líneas = entrada.Split('\n');
            var salida = "";

            if (líneas.Length > 2)
            {
                var palabra = líneas[1].Split(',')[0].TrimEnd();//Pueden venir cosas del estilo: A, a
                var formasDeLaPalabra = líneas[2].Split(',').Select(forma => forma.Trim()).ToList();
                var palabras = formasDeLaPalabra.Unir(palabra);
                
                for (var i = 2; i < líneas.Length - 1; i++)
                {
                    var línea = líneas[i].TrimEnd();

                    if (EsEjemploODefinición(línea, palabras))
                    {
                        var etiquetaPennTreebank = ObtenerEtiquetaPennTreebank(líneas, i + 1);//Busca en las próximas líneas la etiqueta Cobuild para la entrada. Es decir, una línea que esté en la tabla de traducción de etiquetas. Luego traduce esa etiqueta Cobuild en la etiqueta Penn Treebak correspondiente.
                        if (!string.IsNullOrEmpty(etiquetaPennTreebank))
                        {
                            var etiquetasInferidas = InferirEtiquetasParaLasFormasDeLaPalabra(formasDeLaPalabra, palabra, etiquetaPennTreebank);
                            etiquetasInferidas.AgregarSiNoExiste(palabra, etiquetaPennTreebank);
                            salida += GenerarSalidaEtiquetada(línea, etiquetasInferidas);
                        }
                    }
                }
                //salida += "------------------------";
            }

            return salida;
        }
       

        /// <summary>
        /// Indica si la línea es un ejemplo o definición de Cobuild;
        /// </summary>
        private static bool EsEjemploODefinición(string línea, IEnumerable<string> formasDeLaPalabra)
        {
            return formasDeLaPalabra.Any(p => EsEjemploODefinición(línea, p));
        }

        /// <summary>
        /// Indica si la línea es un ejemplo o definición de Cobuild;
        /// si la linea contiene a la palabra 
        /// y tiene al menos 2 letras más que la palabra
        /// y tiene al menos 4 palabras
        /// y tiene como máximo 3 comas
        /// y tiene al menos una palabra que no es la 'palabra'
        /// </summary>
        private static bool EsEjemploODefinición(string línea, string palabra)
        {
            var cantPalabras = línea.Split().Count();
            return línea.Contains(palabra) && línea.Length > palabra.Length + 2 && cantPalabras > 4 && línea.Sum(letra => letra == ',' ? 1 : 0) <= 3 && cantPalabras > línea.CantidadDeOcurrencias(palabra);
        }

        
        /// <summary>
        ///   Obtiene una lista con cada palabra del ejemplo. En la palabra o formas de palabra para las que se obtuvieron etiquetas, le asigna la etiqueta correspondiente.
        ///   Tiene en cuenta signos de puntuacion al final y al principio.
        /// </summary>
        private static string GenerarSalidaEtiquetada(string ejemplo, Dictionary<string, string> etiquetas)
        {
            var salida = string.Empty;

            foreach (var palabraConEtiqueta in etiquetas)
                if (palabraConEtiqueta.Key.Contains(' '))
                {
                    var palabra = palabraConEtiqueta.Key;
                    ejemplo = ejemplo.ReemplazarEspacioPorStringMagico(palabra);
                }

            var palabrasDelEjemplo = ejemplo.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < palabrasDelEjemplo.Length; i++)
            {
                var hayPuntuacion = false;
                var palabra = palabrasDelEjemplo[i].ReemplazarStringMagicoPorEspacio();
                string puntuacionFinal, parteOriginalSinPuntuacion;

                if (palabra.StartsWith("..."))
                {
                    palabra = palabra.Substring(3);
                    salida += "...\n";
                    CantidadDeSignosDePuntuación++;
                }

                if (palabra.EndsWith("..."))
                {
                    puntuacionFinal = "...";
                    parteOriginalSinPuntuacion = palabra.Substring(0, palabra.Length - 3);
                    hayPuntuacion = true;
                }
                else
                {
                    puntuacionFinal = (palabra == string.Empty ? ' ' : palabra.Last()).ToString();
                    if (char.IsPunctuation(puntuacionFinal[0]))
                    {
                        hayPuntuacion = true;
                        parteOriginalSinPuntuacion = palabra.Substring(0, palabra.Length - 1);
                    }
                    else
                        parteOriginalSinPuntuacion = palabra;
                }

                salida += parteOriginalSinPuntuacion;
                CantidadDePalabras++;

                var parteSinPunt = parteOriginalSinPuntuacion.ToLower();

                if (etiquetas.ContainsKey(parteSinPunt))
                {
                    salida += "\t" + etiquetas[parteSinPunt];
                    CantidadDePalabrasEtiquetadas++;
                }
                    

                salida += "\n";

                if (hayPuntuacion)
                {
                    salida += puntuacionFinal + "\n";
                    CantidadDeSignosDePuntuación++;
                }
                    
            }
            return salida;
        }

        /// <summary>
        /// Para cada una de las formas de la palabra infiere su tipo basado en la palabra y su etiqueta PennTreebank
        /// </summary>
        private static Dictionary<string, string> InferirEtiquetasParaLasFormasDeLaPalabra(IEnumerable<string> formasDeLaPalabra, string palabra, string etiquetaPennTreebank)
        {
            var etiquetasInferidas = new Dictionary<string, string>();
            var etiquetaInferida = string.Empty;
            foreach (var forma in formasDeLaPalabra)
            {
                switch (etiquetaPennTreebank)
                {
                    case "JJ":
                        etiquetaInferida = palabra.InferirTipoJJ(forma);
                        break;
                    case "VB":
                        etiquetaInferida = palabra.InferirTipoVB(forma);
                        break;
                    case "RB":
                        etiquetaInferida = palabra.InferirTipoRB(forma);
                        break;
                    case "NN":
                        etiquetaInferida = palabra.InferirTipoNN(forma);
                        break;
                }
                if (etiquetaInferida != string.Empty)
                    etiquetasInferidas.AgregarSiNoExiste(forma.ToLower(), etiquetaInferida);
            }
            return etiquetasInferidas;
        }

        /// <summary>
        /// Busca en la entrada, (en cada renglón) si hay alguna palabra que sea un tag COBUILD. Es decir; 
        /// si está en la tabla de traducción de etiquetas, y si es así lo traduce con la etiqueta Penn Treebank correspondiente.
        /// </summary>
        private static string ObtenerEtiquetaPennTreebank(string[] líneas, int i)
        {
            string líneaDeDondeSeObtuvoLaEtiqueta;
            return ObtenerEtiquetaPennTreebank(líneas, i, out líneaDeDondeSeObtuvoLaEtiqueta);           
        }
        #endregion

        /// <summary>
        /// Busca en la entrada, (en cada renglón) si hay alguna palabra que sea un tag COBUILD. Es decir; 
        /// si está en la tabla de traducción de etiquetas, y si es así lo traduce con la etiqueta Penn Treebank correspondiente.
        /// </summary>
        private static string ObtenerEtiquetaPennTreebank(string[] líneas, int i, out string líneaDeDondeSeObtuvoLaEtiqueta)
        {
            líneaDeDondeSeObtuvoLaEtiqueta = string.Empty;
            string etiquetaPennTreebankObtenida = null;

            for (; i < líneas.Length && etiquetaPennTreebankObtenida == null; i++)
            {
                líneaDeDondeSeObtuvoLaEtiqueta = líneas[i];
                etiquetaPennTreebankObtenida = ObtenerEtiquetaPennTreebank(líneaDeDondeSeObtuvoLaEtiqueta);
            }

            return etiquetaPennTreebankObtenida;
        }
        /// <summary>
        /// Busca en la tabla de traducción de etiquetas si existe alguna etiqueta Penn Treebank para la etiqueta Cobuild posibleEtiquetaCobuild
        /// </summary>
        private static string ObtenerEtiquetaPennTreebank(string posibleEtiquetaCobuild)
        {
            string etiquetaPennTreebank = null;
            posibleEtiquetaCobuild = posibleEtiquetaCobuild.TrimEnd();
            var posiblesEtiquetasPennTreebank = tablaDeTraducciónDeEtiquetas.Where(t => t.Key.StartsWith(posibleEtiquetaCobuild));

            if (tablaDeTraducciónDeEtiquetas.ContainsKey(posibleEtiquetaCobuild))
                etiquetaPennTreebank = tablaDeTraducciónDeEtiquetas[posibleEtiquetaCobuild];
            else if (posiblesEtiquetasPennTreebank.Any())
                etiquetaPennTreebank = posiblesEtiquetasPennTreebank.First().Value;

            return etiquetaPennTreebank;
        }

    }
}