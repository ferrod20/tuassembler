using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
  
namespace ConsoleApplication1
{
	internal class Program
	{
		private static Dictionary<string, string> tiposs;															

		#region Variables de clase
		private static string comienzoDeBloque = "DICTIONARY_ENTRY";
		private static string finDeBloque = "\nDI";
		private static List<string> tipos = new List<string> {"phrasal verb", "adverb", "other", "phrase", "verb", "adjective", "noun"};
		#endregion

		#region Métodos
		private static bool EsEjemplo(string parte, string palabra)
		{
			return parte.Contains(palabra) && parte.Length > palabra.Length +2 && parte.Split().Count() > 4 && parte.Sum(letra => letra == ',' ? 1 : 0) <= 3;
		}
		private static bool EsTipo(string tipo, out string tipoAsociado)
		{
			tipoAsociado = string.Empty;
			var tipos2 = tiposs.Where(t => tipo.StartsWith(t.Key));
			if (tipos2.Count() > 0)
				tipoAsociado = tipos2.First().Value;

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
				if(bloque != string.Empty)
				{
					datos = ExtraerDatosDelBloque(bloque);
					salida.WriteLine(datos);	
				}
			}
		}
		private static string ExtraerDatosDelBloque(string bloque)
		{
			var partes = bloque.Split('\n');
			var palabra = partes[1].TrimEnd();

			var salida = palabra + "\n";
			string tip;
			for (var i = 2; i < partes.Length - 1; i++)
			{
				var parte = partes[i].TrimEnd();

				if (EsEjemplo(parte, palabra))
					salida += parte + "\n";
				else if (EsTipo(parte, out tip))
					salida += parte + "\n";
			}

			partes = bloque.Split('\n');

			salida = palabra + "\n";

			for (var i = 2; i < partes.Length - 1; i++)
			{
			    var parte = partes[i].TrimEnd();

			    if (EsEjemplo(parte, palabra))
			    {
			        var tipo = ObtenerTipo(partes, i + 1);
			        if( tipo!=string.Empty)
			        {


			        	Regex.Replace(parte, "", "");
						///parte.R
						var partesDelEjemplo = parte.Replace("","").Split(' ');
			            foreach (var p in partesDelEjemplo)
			            {
			                salida += p;
			                if (p == palabra)
			                    salida += "\t" + tipo;

			                salida += "\n";
			            }	
			        }

			    }									
			}

			return salida + "------------------------";
			return salida;
		}
		private static string ObtenerTipo(string[] partes, int i)
		{
			var tipo = string.Empty;
			for(;i<partes.Length;i++)
			{
				if( EsTipo(partes[i], out tipo))
					break;
			}
			return tipo;
		}
		private static void Main(string[] args)
		{
			tiposs = new Dictionary<string, string> {
	
    {"phrasal verb", ""}																																																																						,
    {"adverb", "RB"}                                                                                                                                                ,
    {"other", ""}                                                                                                                                                   ,
    {"phrase", ""}                                                                                                                                                  ,
    {"verb", "VB"}                                                                                                                                                  ,
    {"adjective", "JJ"}                                                                                                                                             ,
    {"noun", "NN"}					                                                                                                                                        ,
    
    {"countable noun", "NN"}                                                                                                                                        ,
    {"uncountable noun", "NN"}                                                                                                                                      ,
    {"classifying adjective", "JJ"}                                                                                                                                 ,
    {"qualitative adjective", "JJ"}                                                                                                                                 ,
    {"verb + object", "VB"}                                                                                                                                         ,
    {"adverb with verb", "RB"}                                                                                                                                      ,
    {"noun singular", "NN"}                                                                                                                                         ,
    {"noun plural", "NNS"}                                                                                                                                          ,
    {"countable or uncountable noun", "NN"}                                                                                                                         ,
    {"preposition", "IN"}                                                                                                                                           ,
    {"countable noun with supporter", "NN"}                                                                                                                         ,
    {"uncountable or countable noun", "NN"}                                                                                                                         ,
    {"convention", ""}                                                                                                                                              ,
    {"noun singular with determiner", "NN"}                                                                                                                         ,
    {"verb or verb + object", "VB"}                                                                                                                                 ,
    {"ergative verb", "VB"}                                                                                                                                         ,
    {"verb + adjunct", "VB"}                                                                                                                                        ,
    {"verb + object + adjunct", "VB"}                                                                                                                               ,
    {"adverb after verb", "RB"}                                                                                                                                     ,
    {"mass noun", "NN"}                                                                                                                                             ,
    {"sentence adverb", "RB"}                                                                                                                                       ,
    {"proper noun", "NNP"}                                                                                                                                          ,
    {"uncountable noun with supporter", "NN"}                                                                                                                       ,
    {"partitive noun", "NN"}                                                                                                                                        ,
    {"adverb + adjective or adverb", "RB"}                                                                                                                          ,
    {"combining form", ""}                                                                                                                                          ,
    {"modal", "MD"}                                                                                                                                                 ,
    {"subordinating conjunction", "IN"}                                                                                                                             ,
    {"exclamation", "UH"}                                                                                                                                           ,
    {"adjective colour", "JJ"}                                                                                                                                      ,
    {"noun singular with determiner with supporter", "NN"}                                                                                                          ,
    {"prefix", ""}                                                                                                                                                  ,
    {"noun before noun", "NN"}                                                                                                                                      ,
    {"verb + object (noun group or reflexive)", "VB"}                                                                                                               ,
    {"verb + object or reporting clause", "VB"}                                                                                                                     ,
    {"countable noun + ^i{of^i}", "NN"}                                                                                                                             ,
    {"suffix", ""}                                                                                                                                                  ,
    {"wh", ""}                                                                                                                                                      ,
    {"verb + object (reflexive)", "VB"}                                                                                                                             ,
    {"verb + adjunct (^i{to^i})", "VB"}                                                                                                                             ,
    {"phrase + noun group", ""}                                                                                                                                     ,
    {"countable noun, or ^i{by^i} + noun", "NN"}                                                                                                                    ,
    {"countable noun or partitive noun", "NN"}                                                                                                                      ,
    {"pronoun", "PRP"}                                                                                                                                              ,
    {"verb + object, or phrasal verb", "VB"}                                                                                                                        ,
    {"count or uncountable noun", "NN"}                                                                                                                             ,
    {"ordinal", "JJ"}                                                                                                                                               ,
    {"countable noun or vocative", "NN"}                                                                                                                            ,
    {"partitive noun + uncountable noun", "NN"}                                                                                                                     ,
    {"noun singular with determiner + ^i{of^i}", "NN"}                                                                                                              ,
    {"verb + ^i{to^i}-infinitive", "VB"}                                                                                                                            ,
    {"adverb + adjective", "RB"}                                                                                                                                    ,
    {"verb or verb + adjunct (^i{with)", "VB"}                                                                                                                      ,
    {"noun in titles", "NN"}                                                                                                                                        ,
    {"coordinating conjunction", "CC"}                                                                                                                              ,
    {"number", "CD"}                                                                                                                                                ,
    {"verb + object, verb + object + object, or verb + object + adjunct (^i{to^i})", "VB"}                                                                          ,
    {"ergative verb + adjunct", "VB"}                                                                                                                               ,
    {"verb + object + adjunct (^i{to^i})", "VB"}                                                                                                                    ,
    {"verb + object, or verb + adjunct", "VB"}                                                                                                                      ,
    {"preposition, or adverb after verb", "IN"}                                                                                                                     ,
    {"verb + object + adjunct (^i{with^i})", "VB"}                                                                                                                  ,
    {"verb + adjunct (^i{with^i})", "VB"}                                                                                                                           ,
    {"verb + complement", "VB"}                                                                                                                                     ,
    {"noun vocative", "NN"}                                                                                                                                         ,
    {"uncountable noun + ^i{of^i}", "NN"}                                                                                                                           ,
    {"indefinite pronoun", "NN"}                                                                                                                                    ,
    {"determiner", "DT"}                                                                                                                                            ,
    {"uncountable noun, or noun singular", "NN"}                                                                                                                    ,
    {"adjective after noun", "JJ"}                                                                                                                                  ,
    {"countable noun, or ^i{in^i} + noun", "NN"}                                                                                                                    ,
    {"noun in names", "NN"}                                                                                                                                         ,
    {"noun plural with supporter", "NN"}                                                                                                                            ,
    {"verb + object, or verb", "VB"}                                                                                                                                ,
    {"partitive noun + noun in plural", "NN"}                                                                                                                       ,
    {"preposition or adverb", "RB"}                                                                                                                                 ,
    {"verb + object + ^i{to^i}-infinitive", "VB"}                                                                                                                   ,
    {"verb + reporting clause", "VB"}                                                                                                                               ,
    {"exclam", "UH"}                                                                                                                                                ,
    {"proper noun or vocative", "NNP"}                                                                                                                              ,
    {"phrase after noun", ""}                                                                                                                                       ,
    {"adverb after verb, or classifying adjective", "RB"}                                                                                                           ,
    {"determiner + countable noun in singular", "DT"}                                                                                                               ,
    {"preposition after noun", "IN"}                                                                                                                                ,
    {"verb or ergative verb", "VB"}                                                                                                                                 ,
    {"countable or uncountable noun with supporter", "NN"}                                                                                                          ,
    {"phrase + reporting clause", ""}                                                                                                                               ,
    {"adverb or sentence adverb", "RB"}                                                                                                                             ,
    {"uncountable noun, or noun before noun", "NN"}                                                                                                                 ,
    {"adverb with verb, or sentence adverb", "RB"}                                                                                                                  ,
    {"verb + adjunct (^i{from^i})", "VB"}                                                                                                                           ,
    {"uncountable or countable noun with supporter", "NN"}                                                                                                          ,
    {"verb + object, verb + object + object, or verb + object + adjunct (^i{for^i})", "VB"}                                                                         


	} ;

			TextReader archivo = new StreamReader(@"C:\Users\frodriguez\Desktop\palla\data\pruebacobuild.3.txt");
			TextWriter salida = new StreamWriter(@"C:\Users\frodriguez\Desktop\palla\data\ExtraccionDeDatos.txt");

			var texto = archivo.ReadToEnd();
			ExtraerDatos(texto, salida);
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
		#endregion
	}
}