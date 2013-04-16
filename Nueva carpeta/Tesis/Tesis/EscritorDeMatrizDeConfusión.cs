using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class EscritorDeMatrizDeConfusión
    {
        private string archivoDeSalida;
        private MatrizDeConfusión matrizDeConfusión;

        public EscritorDeMatrizDeConfusión(string archivoDeSalida, MatrizDeConfusión matriz)
        {
            this.archivoDeSalida = archivoDeSalida;
            matrizDeConfusión = matriz;
        }

        public void EscribirTablaDeTraducciónDeEtiquetas()
        {
            var incluidos = new List<string>();
            TextWriter salida = new StreamWriter(archivoDeSalida);

            matrizDeConfusión.OrdenarPorMayorError();

            foreach (var tags in matrizDeConfusión)
            {
                if (!incluidos.Contains(tags.TagGoldStandard))
                {
                    salida.WriteLine(tags.TagGoldStandard + " " + tags.TagDePrueba + " " + tags.TotalDePalabras);
                    incluidos.Add(tags.TagGoldStandard);
                }
            }

            salida.Close();
        }
        public void EscribirMatrizDeConfusión(string titulo, string tituloFila, string tituloColumna)
        {
            TextWriter salida = new StreamWriter(archivoDeSalida);
            salida.WriteLine(titulo);
            salida.WriteLine();
            EscribirEncabezado(salida);

            salida.WriteLine();

            salida.WriteLine("Errores");
            salida.WriteLine(tituloFila + "|\t" + tituloColumna + "|\tCantidadDeErrores");

            matrizDeConfusión.OrdenarPorMayorError();

            foreach (var celda in matrizDeConfusión)
            {
                salida.WriteLine(celda.TagGoldStandard + " " + celda.TagDePrueba + " " + celda.TotalDePalabras);
                foreach (var palabra in celda.TomarPalabrasDeMayorError(40))
                    salida.WriteLine("\t" + palabra.Key + " " + palabra.Value);
            }

            salida.Close();
        }
        public void EscribirMatrizDeConfParaLatex(string titulo, string tituloFila, string tituloColumna)
        {
            TextWriter salida = new StreamWriter(archivoDeSalida);
            var tags = matrizDeConfusión.TomarLosDeMayorError(10);
            var tagsFila = tags.Item1;
            var tagsColumna = tags.Item2;
            var errores = matrizDeConfusión.ObtenerErrores(10);

            EscribirEncabezado(salida);
            EscribirEncabezadoLatex(titulo, tituloFila, tituloColumna, tagsColumna, salida);

            var erroresTotales = 0;
            foreach (var tagFila in tagsFila)
            {
                salida.Write(@"\textbf{" + tagFila + "}");
                foreach (var tagCol in tagsColumna)
                {
                    salida.Write(" & ");
                    var error = matrizDeConfusión.ObtenerCantidadDeErrores(tagCol, tagFila);//tag columna es tag de prueba, tagFila es tag gold standard
                    erroresTotales += error;
                    if (error == 0)
                        salida.Write("-");
                    else
                    {
                        if (errores.Contains(error))
                            salida.Write("\\textbf{" + error + "}");//negrita
                        else
                            salida.Write(error);
                    }

                }
                salida.WriteLine(@"\\");
            }


            salida.Write(@"\hline
\end{tabular}
\end{table}
\end{center}");

            salida.WriteLine();
            salida.WriteLine();
            salida.WriteLine("%Sumatoria de errores: " + erroresTotales);
            EscribirPorcentajeDeAciertos(salida);
            salida.Close();
        }

        private void EscribirPorcentajeDeAciertos(TextWriter salida)
        {
            var cantidadDeErrores = matrizDeConfusión.CantidadDeErrores;
            var aciertos = matrizDeConfusión.CantidadDeAciertos;
            var porcentajeDeAciertos = matrizDeConfusión.PorcentajeDeAciertos; 
            var porcentajeDeErrores = matrizDeConfusión.PorcentajeDeErrores;  
            salida.WriteLine("\\noindent Aciertos: {0:n0} ({1:0.00}\\%)\\\\", aciertos, porcentajeDeAciertos);
            salida.WriteLine("\\noindent Errores: {0:n0} ({1:0.00}\\%)\\\\", cantidadDeErrores, porcentajeDeErrores);
        }
        private void EscribirEncabezado(TextWriter salida)
        {
            var cantidadDeErrores = matrizDeConfusión.CantidadDeErrores;
            var aciertos = matrizDeConfusión.CantidadDeAciertos;
            var porcentajeDeAciertos = matrizDeConfusión.PorcentajeDeAciertos; 
            salida.WriteLine("%Aciertos: " + aciertos + " ( " + porcentajeDeAciertos + "% )");
            salida.WriteLine("%Errores: " + cantidadDeErrores);
            salida.WriteLine("%Cantidad de tags: " + matrizDeConfusión.CantidadDeEtiquetas);
        }

        private static void EscribirEncabezadoLatex(string titulo, string tituloFila, string tituloColumna, IEnumerable<string> tagsColumna, TextWriter salida)
        {
            salida.Write(
                @"\begin{center}
\begin{table}[H]
\caption{" + titulo + @"}\\	
\begin{tabular}{| l | ");
            for (var i = 0; i < tagsColumna.Count(); i++)
                salida.Write("c | ");

            salida.Write(
                @"}
\hline
 \backslashbox{\scriptsize{" + tituloFila + @"}\kern-1em}{\kern-1em \scriptsize{" +
                tituloColumna + @"}}  &	");

            foreach (var tag in tagsColumna)
                salida.Write("\\textbf{" + tag + "}	&   ");
            salida.Write(@"\hline");
        }
    }
}