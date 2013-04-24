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
            this.archDeInformación = archDeInformación;
            cantidadDeSignosDePuntuación = cantidadDePalabras = cantidadDePalabrasEtiquetadas = 0;
            infoEtiquetasCobuild = new Dictionary<string, int>();
            inferidor = new InferiridorDeEtiquetas();
        }

        #region Constantes
        private const string comienzoDeBloque = "DICTIONARY_ENTRY";
        private const string finDeBloque = "\nDI";
        /// <summary>
        /// Tabla para traducir etiquetas COBUILD en etiquetas Penn TreeBank
        /// </summary>
        private readonly Dictionary<string, string> tablaDeTraducciónDeEtiquetas = new Dictionary<string, string> {
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
        {"classifying adjective: attributive", "JJ"}, 
        {"classifying adjective: ussually attributive", "JJ"}, 
        {"qualitative adjective: attributive", "JJ"}, 
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
        {"countable noun: ussually singular", "NN"}, 
        {"countable noun: if + preposition then of", "NN"}, 
        {"countable noun: ussually plural", "NNS"}, 
        {"noun plural: the + noun", "NNS"}, 
        {"countable noun: usually with supporter", "NN"}, 
        {"noun singular: a + noun", "NN"}, 
        {"noun singular: the + noun", "NN"}, 
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
        {"verb: usually + adjunct", "VB"}, 
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

        #region Variables

        private InferiridorDeEtiquetas inferidor;
        private int cantidadDeSignosDePuntuación = 0;
        private int cantidadDePalabras = 0;
        private int cantidadDePalabrasEtiquetadas = 0;
        
        private string archCobuild;
        private string archSalida;
        private string archDeInformación;

        private Dictionary<string, int> infoEtiquetasCobuild = new Dictionary<string, int>();
        #endregion
        
        #region Métodos
        public void ExtraerLaInformaciónDeCobuild()
        {
            var texto = File.ReadAllText(archCobuild);
            TextWriter tw = new StreamWriter(archSalida);
        
            ExtraerLaInformaciónDeCobuild(texto, tw);
            tw.Close();
            
            var total = cantidadDePalabras + cantidadDeSignosDePuntuación;
            
            TextWriter info = new StreamWriter(archDeInformación);    
            info.WriteLine("Cantidad de palabras y signos puntuación: " + total);
            info.WriteLine("Cantidad de signos de puntuación: " + cantidadDeSignosDePuntuación);
            info.WriteLine("Cantidad de palabras: " + cantidadDePalabras);
            info.WriteLine("Cantidad de palabras etiquetadas: " + cantidadDePalabrasEtiquetadas + "\t( " + ((cantidadDePalabrasEtiquetadas * 100) / (double)cantidadDePalabras) + "% )");

            info.WriteLine();
            info.WriteLine();

            var etiquetasOrdenadas = infoEtiquetasCobuild.OrderByDescending(infoEtiqueta => infoEtiqueta.Value);
            foreach (var etiqueta in etiquetasOrdenadas)
                if (string.IsNullOrEmpty(ObtenerEtiquetaPennTreebank(etiqueta.Key)))
                    info.WriteLine(etiqueta.Key + "\t\t" + etiqueta.Value);   
            info.Close();
        }

        /// <summary>
        /// Dada una entrada de Cobuild, extrae la información almacenada (palabras y tags)
        /// </summary>
        public string ExtraerInformaciónDeLaEntrada(string entrada)
        {
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

                for (; i < líneas.Length - 1; i++)
                {
                    var línea = líneas[i].Trim();
                    var esEjemploODefinición = EsEjemploODefinición(línea, palabras);

                    if (esEjemploODefinición)
                    {
                        if (encontréDefinición)
                        {
                            var etiquetaPennTreebank = ObtenerEtiquetaPennTreebank(líneas, i + 1);
                            //Busca en las próximas líneas la etiqueta Cobuild para la entrada. Es decir, una ejemplo que esté en la tabla de traducción de etiquetas. Luego traduce esa etiqueta Cobuild en la etiqueta Penn Treebak correspondiente.

                            for (var j = i + 1; j < líneas.Length && etiquetaPennTreebank == null; j++)
                            {
                                var etiquetaCobuild = líneas[j].Trim();
                                if (infoEtiquetasCobuild.ContainsKey(etiquetaCobuild))
                                    infoEtiquetasCobuild[etiquetaCobuild]++;
                                else
                                    infoEtiquetasCobuild[etiquetaCobuild] = 1;
                            }

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

        private void ExtraerLaInformaciónDeCobuild(string texto, TextWriter tw)
        {
            var indice = 0;
            string entradaCobuild, informaciónExtraída = string.Empty;
            
            while (indice != -1)
            {
                entradaCobuild = ObtenerEntrada(texto, ref indice);
                if (entradaCobuild != string.Empty)
                    informaciónExtraída = ExtraerInformaciónDeLaEntrada(entradaCobuild);
                if (!string.IsNullOrEmpty(informaciónExtraída.Trim()))
                    tw.Write(informaciónExtraída);
            }

                   
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
            var cantPalEtiquetadas = cantidadDePalabrasEtiquetadas;
            var palabrasDelEjemplo = ejemplo.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var salida = "";

            foreach (var palabraDelEjemplo in palabrasDelEjemplo)
            {
                var palabra = palabraDelEjemplo;

                if (palabra.StartsWith("..."))
                {
                    palabra = palabra.Substring(3);
                    salida += "...\n";
                    cantidadDeSignosDePuntuación++;
                }

                string puntuaciónFinal;
                var palabraSinPuntuación = palabra.HayPuntuaciónFinal(out puntuaciónFinal);

                if (palabraSinPuntuación != string.Empty )
                {
                    salida += palabraSinPuntuación;
                    cantidadDePalabras++;        

                    var palabraEnMinúscula = palabraSinPuntuación.ToLower();

                    if (etiquetas.ContainsKey(palabraEnMinúscula))
                    {
                        var etiquetaPennTreebank = etiquetas[palabraEnMinúscula];
                        //var etiquetaInferida = InferirEtiqueta(palabraSinPuntuación, etiquetaPennTreebank, palabrasDelEjemplo, i);

                        salida += "\t" + etiquetaPennTreebank;
                        cantidadDePalabrasEtiquetadas++;                        
                    }
                    salida += "\n";
                }

                if (puntuaciónFinal != string.Empty)
                {
                    salida += puntuaciónFinal + "\n";
                    if (puntuaciónFinal == "...")
                        salida += "\n";
                    cantidadDeSignosDePuntuación++;
                }
            }

            salida += "\n";
            //Si se asginó alguna etiqueta
            return cantidadDePalabrasEtiquetadas > cantPalEtiquetadas ? salida : string.Empty;
        }

        
        
        /// <summary>
        /// Busca en la entrada, (en cada renglón) si hay alguna Palabra que sea un tag COBUILD. Es decir; 
        /// si está en la tabla de traducción de etiquetas, y si es así lo traduce con la etiqueta Penn Treebank correspondiente.
        /// </summary>
        private string ObtenerEtiquetaPennTreebank(string[] líneas, int i)
        {
            string etiquetaPennTreebankObtenida = null;

            for (; i < líneas.Length && etiquetaPennTreebankObtenida == null; i++)
            {
                var línea = líneas[i];
                if(línea!= string.Empty)
                    etiquetaPennTreebankObtenida = ObtenerEtiquetaPennTreebank(línea);
            }

            return etiquetaPennTreebankObtenida;
        }
        /// <summary>
        /// Busca en la tabla de traducción de etiquetas si existe alguna etiqueta Penn Treebank para la etiqueta Cobuild posibleEtiquetaCobuild
        /// </summary>
        private string ObtenerEtiquetaPennTreebank(string posibleEtiquetaCobuild)
        {
            string etiquetaPennTreebank = null;
            posibleEtiquetaCobuild = posibleEtiquetaCobuild.TrimEnd();
            var posiblesEtiquetasPennTreebank = tablaDeTraducciónDeEtiquetas.Where(t => t.Key.StartsWith(posibleEtiquetaCobuild));

            if (tablaDeTraducciónDeEtiquetas.ContainsKey(posibleEtiquetaCobuild))
                etiquetaPennTreebank = tablaDeTraducciónDeEtiquetas[posibleEtiquetaCobuild];
            else if (posiblesEtiquetasPennTreebank.Any())
                etiquetaPennTreebank = posiblesEtiquetasPennTreebank.First().Value;
            else if (posibleEtiquetaCobuild.StartsWith("countable noun") || posibleEtiquetaCobuild.StartsWith("uncountable noun"))
                etiquetaPennTreebank = "NN";
            else if (posibleEtiquetaCobuild.StartsWith("verb"))
                etiquetaPennTreebank = "VB";
            else if (posibleEtiquetaCobuild.StartsWith("qualitative adjective"))
                etiquetaPennTreebank = "JJ";
            else if (posibleEtiquetaCobuild.StartsWith("classifying adjective"))
                etiquetaPennTreebank = "JJ";

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