#define HacerLegible
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
	internal class Program
	{

		#region Variables de clase
		private static string comienzoDeBloque = "DICTIONARY_ENTRY";
		private static string finDeBloque = "\nDI";
		private static List<string> tipos = new List<string> {"phrasal verb", "adverb", "other", "phrase", "verb", "adjective", "noun"};

		private static Dictionary<string, string> tiposs = new Dictionary<string, string>
		                                                   	{
		                                                   		{"phrasal verb", ""},
		                                                   		{"adverb", "RB"},
		                                                   		{"other", ""},
		                                                   		{"phrase", ""},
		                                                   		{"verb", "VB"},
		                                                   		{"adjective", "JJ"},
		                                                   		{"noun", "NN"},
		                                                   		{"countable noun", "NN"},
		                                                   		{"uncountable noun", "NN"},
		                                                   		{"classifying adjective", "JJ"},
		                                                   		{"qualitative adjective", "JJ"},
		                                                   		{"verb + object", "VB"},
		                                                   		{"adverb with verb", "RB"},
		                                                   		{"noun singular", "NN"},
		                                                   		{"noun plural", "NNS"},
		                                                   		{"countable or uncountable noun", "NN"},
		                                                   		{"preposition", "IN"},
		                                                   		{"countable noun with supporter", "NN"},
		                                                   		{"uncountable or countable noun", "NN"},
		                                                   		{"convention", ""},
		                                                   		{"noun singular with determiner", "NN"},
		                                                   		{"verb or verb + object", "VB"},
		                                                   		{"ergative verb", "VB"},
		                                                   		{"verb + adjunct", "VB"},
		                                                   		{"verb + object + adjunct", "VB"},
		                                                   		{"adverb after verb", "RB"},
		                                                   		{"mass noun", "NN"},
		                                                   		{"sentence adverb", "RB"},
		                                                   		{"proper noun", "NNP"},
		                                                   		{"uncountable noun with supporter", "NN"},
		                                                   		{"partitive noun", "NN"},
		                                                   		{"adverb + adjective or adverb", "RB"},
		                                                   		{"combining form", ""},
		                                                   		{"modal", "MD"},
		                                                   		{"subordinating conjunction", "IN"},
		                                                   		{"exclamation", "UH"},
		                                                   		{"adjective colour", "JJ"},
		                                                   		{"noun singular with determiner with supporter", "NN"},
		                                                   		{"prefix", ""},
		                                                   		{"noun before noun", "NN"},
		                                                   		{"verb + object (noun group or reflexive)", "VB"},
		                                                   		{"verb + object or reporting clause", "VB"},
		                                                   		{"countable noun + ^i{of^i}", "NN"},
		                                                   		{"suffix", ""},
		                                                   		{"wh", ""},
		                                                   		{"verb + object (reflexive)", "VB"},
		                                                   		{"verb + adjunct (^i{to^i})", "VB"},
		                                                   		{"phrase + noun group", ""},
		                                                   		{"countable noun, or ^i{by^i} + noun", "NN"},
		                                                   		{"countable noun or partitive noun", "NN"},
		                                                   		{"pronoun", "PRP"},
		                                                   		{"verb + object, or phrasal verb", "VB"},
		                                                   		{"count or uncountable noun", "NN"},
		                                                   		{"ordinal", "JJ"},
		                                                   		{"countable noun or vocative", "NN"},
		                                                   		{"partitive noun + uncountable noun", "NN"},
		                                                   		{"noun singular with determiner + ^i{of^i}", "NN"},
		                                                   		{"verb + ^i{to^i}-infinitive", "VB"},
		                                                   		{"adverb + adjective", "RB"},
		                                                   		{"verb or verb + adjunct (^i{with)", "VB"},
		                                                   		{"noun in titles", "NN"},
		                                                   		{"coordinating conjunction", "CC"},
		                                                   		{"number", "CD"},
		                                                   		{"verb + object, verb + object + object, or verb + object + adjunct (^i{to^i})", "VB"},
		                                                   		{"ergative verb + adjunct", "VB"},
		                                                   		{"verb + object + adjunct (^i{to^i})", "VB"},
		                                                   		{"verb + object, or verb + adjunct", "VB"},
		                                                   		{"preposition, or adverb after verb", "IN"},
		                                                   		{"verb + object + adjunct (^i{with^i})", "VB"},
		                                                   		{"verb + adjunct (^i{with^i})", "VB"},
		                                                   		{"verb + complement", "VB"},
		                                                   		{"noun vocative", "NN"},
		                                                   		{"uncountable noun + ^i{of^i}", "NN"},
		                                                   		{"indefinite pronoun", "NN"},
		                                                   		{"determiner", "DT"},
		                                                   		{"uncountable noun, or noun singular", "NN"},
		                                                   		{"adjective after noun", "JJ"},
		                                                   		{"countable noun, or ^i{in^i} + noun", "NN"},
		                                                   		{"noun in names", "NN"},
		                                                   		{"noun plural with supporter", "NN"},
		                                                   		{"verb + object, or verb", "VB"},
		                                                   		{"partitive noun + noun in plural", "NN"},
		                                                   		{"preposition or adverb", "RB"},
		                                                   		{"verb + object + ^i{to^i}-infinitive", "VB"},
		                                                   		{"verb + reporting clause", "VB"},
		                                                   		{"exclam", "UH"},
		                                                   		{"proper noun or vocative", "NNP"},
		                                                   		{"phrase after noun", ""},
		                                                   		{"adverb after verb, or classifying adjective", "RB"},
		                                                   		{"determiner + countable noun in singular", "DT"},
		                                                   		{"preposition after noun", "IN"},
		                                                   		{"verb or ergative verb", "VB"},
		                                                   		{"countable or uncountable noun with supporter", "NN"},
		                                                   		{"phrase + reporting clause", ""},
		                                                   		{"adverb or sentence adverb", "RB"},
		                                                   		{"uncountable noun, or noun before noun", "NN"},
		                                                   		{"adverb with verb, or sentence adverb", "RB"},
		                                                   		{"verb + adjunct (^i{from^i})", "VB"},
		                                                   		{"uncountable or countable noun with supporter", "NN"},
		                                                   		{"verb + object, verb + object + object, or verb + object + adjunct (^i{for^i})", "VB"}
		                                                   	};
		#endregion

		#region Métodos
		private static decimal CantidadDeOcurrencias(string parte, string palabra)
		{
			var cantOc = 0;
			var ind = parte.IndexOf(palabra);

			while (ind != -1)
			{
				cantOc++;
				ind = parte.IndexOf(palabra, ind + 1);
			}
			return cantOc;
		}
		private static bool EsEjemplo(string parte, string palabra)
		{
			var cantPalabras = parte.Split().Count();
			return parte.Contains(palabra) && parte.Length > palabra.Length + 2 && cantPalabras > 4 && parte.Sum(letra => letra == ',' ? 1 : 0) <= 3 && cantPalabras > CantidadDeOcurrencias(parte, palabra);
		}
		private static bool EsTipo(string tipo, out KeyValuePair<string, string> parDeTipos)
		{
			parDeTipos  = new KeyValuePair<string, string>("", "");
			var tipos2 = tiposs.Where(t => t.Key.StartsWith( tipo.TrimEnd()) );
			if (tipos2.Count() > 0)
				parDeTipos = tipos2.First();

			return tipos.Any(tipo.StartsWith);
		}
		private static void ExtraerDatos(string texto, TextWriter salida)
		{
			var indice = 0;
			var bloque = string.Empty;
			string datos;
			while (indice != -1)
			{
				bloque = ObtenerBloque(texto, ref indice);
				if (bloque != string.Empty)
				{
                    #if HacerLegible
                    	datos = HacerLegiblElBloque(bloque);
                    #else
					    datos = ExtraerDatosDelBloque(bloque);
                    #endif
					salida.WriteLine(datos);
				}
			}
		}
        private static string HacerLegiblElBloque(string bloque)
        {
            var partes = bloque.Split('\n');
            var palabra = partes[1].TrimEnd();

            var salida = palabra;
            KeyValuePair<string, string> tip;
            for (var i = 2; i < partes.Length - 1; i++)
            {
                var parte = partes[i].TrimEnd();

                if (EsEjemplo(parte, palabra))
                    salida += parte + "\n";
                else if (EsTipo(parte, out tip))
                    salida += parte + "\n";
            }
            return salida;
        }
	    private static string ExtraerDatosDelBloque(string bloque)
		{
			var partes = bloque.Split('\n');
			var palabra = partes[1].TrimEnd();



			partes = bloque.Split('\n');
			var salida = palabra ;
			bool escribirTipo = true;
			string obtenido;
			for (var i = 2; i < partes.Length - 1; i++)
			{
				var parte = partes[i].TrimEnd();

				if (EsEjemplo(parte, palabra))
				{
					var tipo = ObtenerTipo(partes, i + 1, out obtenido);
					if (tipo.Value != string.Empty)
					{
						if(escribirTipo )
							salida += " | " + obtenido.TrimEnd() +"-->" + tipo + "\n";
						escribirTipo = false;
						if (palabra.Contains(' '))
							parte = ReemplazarEspacio(ref palabra, parte);

						var partesDelEjemplo = parte.Split(' ');

						foreach (var p in partesDelEjemplo)
						{
							string ultimo;
							var p2 = QuitarMagia(p);
							string p3;
							var hayPuntuacion = false;
							if (p2.StartsWith("..."))
							{
								p2 = p2.Substring(3);
								salida += "...\n";
							}

							if (p2.EndsWith("..."))
							{
								ultimo = "...";
								p3 = p2.Substring(0, p2.Length - 3);
								hayPuntuacion = true;
							}
							else
							{
								ultimo = (p2 == string.Empty ? ' ' : p2.Last()).ToString();
								if (char.IsPunctuation(ultimo[0]))
								{
									hayPuntuacion = true;
									p3 = p2.Substring(0, p2.Length - 1);
								}
								else
									p3 = p2;
							}

							salida += p3;

							if (p3.ToLower() == palabra.ToLower())
								salida += "\t" + tipo.Value;

							salida += "\n";

							if (hayPuntuacion)
								salida += ultimo + "\n";
						}
					}
				}
			}

			return salida + "------------------------";
		}
		private static void Main(string[] args)
		{
		    PonerSaltosDeLinea();
          //  ExtraerDatos();
			
		}
        private static string textoOriginal1 = @"Datos\COBUILD.DAt";
	    private static string textoOriginal2 = @"Datos\cobuildSinSaltosDeLinea.txt";
	    private static string textoOriginal3 = @"Datos\ExtraccionDeDatos.txt";

	    private static void PonerSaltosDeLinea()
	    {

            TextReader archivo = new StreamReader(textoOriginal1, Encoding.Default);
            var texto = archivo.ReadToEnd();
            archivo.Close();

            TextWriter salida = new StreamWriter(@"Datos\cosa.txt", false, Encoding.Default);            
            
            PonerSaltosDeLinea(texto,salida);	                    
            salida.Close();
	    }
	    private static void PonerSaltosDeLinea(string texto, TextWriter salida)
	    {                                    	        
            //var texto2 = texto.Replace("\0\0", Environment.NewLine).Replace("\0\0", Environment.NewLine);
	        //var texto2 = texto.Replace("^b{", "").Replace("^b}", "").Replace("^i{", "").Replace("^i}", "");
            var texto2 = ConvertirAAscii(texto);
	        salida.Write(texto2);
	    }
	    private static string ConvertirAAscii(string texto)
	    {
            var sb = new StringBuilder();

            foreach (var car in texto)
                if (1 < car && car < 127)
                    sb.Append(car);

	        return sb.ToString();

            //var unicode = Encoding.BigEndianUnicode;            
            //var ascii = Encoding.ASCII;
            //var bytes = unicode.GetBytes(texto3);
            //var asciiBytes = Encoding.Convert(unicode, ascii, bytes);

            //var asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            //ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            //return new string(asciiChars);
	    }
	    private static void ExtraerDatos()
	    {
            TextReader archivo = new StreamReader(textoOriginal2, Encoding.Default);
            TextWriter salida = new StreamWriter(textoOriginal3);

            var texto = archivo.ReadToEnd();
            archivo.Close();
            ExtraerDatos(texto, salida);
            salida.Close();
	    }

	    private static string ObtenerBloque(string texto, ref int indice)
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
		private static KeyValuePair<string, string> ObtenerTipo(string[] partes, int i, out string obtenido)
		{
			obtenido = string.Empty;
			var tipo = new KeyValuePair<string, string>();

			for (; i < partes.Length; i++)
			{
				obtenido = partes[i];
				if (EsTipo(obtenido, out tipo))
					break;
			}
				
			return tipo;
		}
		private static string QuitarMagia(string p)
		{
			return p.Replace(@"\_/", " ");
		}
		private static string ReemplazarEspacio(ref string palabra, string parte)
		{
			var palabra2 = palabra.Replace(" ", @"\_/");
			parte = parte.Replace(palabra, palabra2);
			palabra = palabra2;
			return parte;
		}
		#endregion
	}
}