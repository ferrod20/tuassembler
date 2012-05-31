using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class MatrizDeConfusión
    {
        private List<Tags> listaDeTags;

        public MatrizDeConfusión()
        {
            this.listaDeTags = new List<Tags>();
        }

        public int CantidadDeEtiquetas{get; set; }

        
        public int ObtenerCantidadDeErrores(string tagDePrueba, string tagGoldStandard)
        {
            foreach (var tags in listaDeTags)
                if (tags.TagDePrueba == tagDePrueba && tags.TagGoldStandard == tagGoldStandard)
                    return tags.TotalDePalabras;

            return 0;
        }

        public void EscribirMatrizDeConfParaLatex(string archivoDeSalida, string titulo, string tituloFila, string tituloColumna)
        {
            TextWriter salida = new StreamWriter(archivoDeSalida);
            EscribirEncabezado(salida);

            var tags = TomarLosDeMayorError(10);
            var errores = ObtenerErrores(10);

            salida.Write(
            @"\begin{center}
\begin{longtable}{| l | ");
            for (var i = 0; i < tags.Count(); i++)
                salida.Write("c | ");

            salida.Write(
            @"}
\caption{" + titulo+ @"}\\	
\hline
 \backslashbox{\scriptsize{"+tituloFila+@"}\kern-1em}{\kern-1em \scriptsize{"+tituloColumna+@"}}  &	");

            foreach (var tag in tags)
                salida.Write("\\textbf{" + tag + "}	&   ");
            salida.Write(@"\hline
\endhead
\hline
\endfoot
\endlastfoot
	\hline
");
            var erroresTotales = 0;
            foreach (var tagCol in tags)
            {
                salida.Write(@"\textbf{" + tagCol + "}");
                foreach (var tagFila in tags)
                {
                    salida.Write(" & ");
                    var error = ObtenerCantidadDeErrores(tagFila, tagCol);//tag columna es tag de prueba, tagFila es tag gold standard
                    erroresTotales += error;
                    if (error == 0)
                        salida.Write("-");
                    else
                    {
                        if(errores.Contains(error) )
                            salida.Write("\\textbf{" + error + "}");//negrita
                        else
                            salida.Write(error);
                    }
                        
                }
                salida.WriteLine(@"\\");
            }


            salida.Write(@"\hline
\end{longtable}
\end{center}");

            salida.WriteLine();
            salida.WriteLine();
            salida.WriteLine("Sumatoria de errores: " + erroresTotales);
            salida.Close();
        }

        private List<int> ObtenerErrores(int cuantos)
        {
            return listaDeTags.Select(tags => tags.TotalDePalabras).Take(cuantos).ToList();
        }

        public IList<string> ObtenerTags()
        {
            var resultado = new List<string>();
            foreach (var tags in listaDeTags)
            {
                if(!resultado.Contains(tags.TagDePrueba))
                    resultado.Add(tags.TagDePrueba);
                if (!resultado.Contains(tags.TagGoldStandard))
                    resultado.Add(tags.TagGoldStandard);
            }

            return resultado;
        }

        private IList<string> TomarLosDeMayorError(int cuantos)
        {
            OrdenarPorMayorError();
            return ObtenerTags().Take(cuantos).ToList();
        }

        private void OrdenarPorMayorError()
        {
            listaDeTags = listaDeTags.OrderByDescending(s => s.TotalDePalabras).ToList();
        }

        public int CantidadDeErrores()
        {
            return listaDeTags.Sum(s => s.TotalDePalabras);
        }

        public void EscribirMatrizDeConfusión(string archivoDeSalida, string titulo, string tituloFila, string tituloColumna)
        {
            TextWriter salida = new StreamWriter(archivoDeSalida);
            salida.WriteLine(titulo);
            salida.WriteLine();
            EscribirEncabezado(salida);

            salida.WriteLine();
            
            salida.WriteLine("Errores");
            salida.WriteLine(tituloFila + "|\t" + tituloColumna + "|\tCantidadDeErrores");

            OrdenarPorMayorError();

            foreach (var tags in listaDeTags)
            {
                salida.WriteLine(tags.TagGoldStandard + " " + tags.TagDePrueba + " " + tags.TotalDePalabras);
                foreach (var palabra in tags.TomarPalabrasDeMayorError(40))
                    salida.WriteLine("\t" + palabra.Key + " " + palabra.Value);
            }

            salida.Close();
        }

        private void EscribirEncabezado(TextWriter salida)
        {
            var cantidadDeErrores = CantidadDeErrores();
            var aciertos = CantidadDeEtiquetas - cantidadDeErrores;
            var porcentajeDeAciertos = aciertos/(double) CantidadDeEtiquetas*100;
            salida.WriteLine("Aciertos: " + aciertos + " ( " + porcentajeDeAciertos + "% )");
            salida.WriteLine("Errores: " + cantidadDeErrores);
            salida.WriteLine("Cantidad de tags: " + CantidadDeEtiquetas);
        }

        public void AgregarError(string tagGoldStandard, string tagDePrueba, string palabra)
        {
            var tags = new Tags(tagGoldStandard, tagDePrueba, palabra);
            if (listaDeTags.Contains(tags))
            {
                var i = listaDeTags.IndexOf(tags);
                listaDeTags[i].AgregarPalabra(palabra);
            }
            else
                listaDeTags.Add(tags);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var tag in listaDeTags)
                sb.AppendLine(tag.ToString());
            return sb.ToString();
        }
    }

    public class Tags : IComparer<Tags>
    {
        #region Variables de instancia
        public string TagDePrueba;
        public string TagGoldStandard;
        public Dictionary<string, int> Palabras;
        #endregion

        #region Constructores
        public Tags(string tagGoldStandard, string tagDePrueba, string palabra)
        {
            TagDePrueba = tagDePrueba;
            TagGoldStandard = tagGoldStandard;
            Palabras = new Dictionary<string, int> {{palabra, 1}};
        }

        public int TotalDePalabras
        {
            get
            {
                return Palabras.Sum(p => p.Value);
            }
        }

        #endregion

        #region Métodos
        public bool Equals(Tags obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return Equals(obj.TagDePrueba, TagDePrueba) && Equals(obj.TagGoldStandard, TagGoldStandard);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (Tags))
                return false;
            return Equals((Tags) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((TagDePrueba != null ? TagDePrueba.GetHashCode() : 0)*397) ^ (TagGoldStandard != null ? TagGoldStandard.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}(gold)-{1}(prueba): {2}", TagGoldStandard, TagDePrueba, TotalDePalabras);
        }

        public int Compare(Tags x, Tags y)
        {
            return 1;
        }

        public void AgregarPalabra(string palabra)
        {
            if(Palabras.AgregarSiNoExiste(palabra, 1))
                Palabras[palabra]++;

        }
        #endregion

        public IEnumerable<KeyValuePair<string, int>> TomarPalabrasDeMayorError(int cuantas)
        {
            return Palabras.OrderByDescending(s => s.Value).Take(cuantas);
        }
    }
}