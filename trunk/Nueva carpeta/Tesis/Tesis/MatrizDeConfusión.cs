using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Matriz
    {
        public Matriz()
        {
            listaDeTags = new List<Celda>();
        }

        protected List<Celda> listaDeTags;        
    }
    
    public class MatrizEnProceso: Matriz
    {
        public MatrizEnProceso()
        {
            cantidadDeEtiquetas = 0;
        }

        private int cantidadDeEtiquetas;

        public void AgregarError(string tagGoldStandard, string tagDePrueba, string palabra)
        {
            var tags = new Celda(tagGoldStandard, tagDePrueba, palabra);

            if (listaDeTags.Contains(tags))
            {
                var i = listaDeTags.IndexOf(tags);
                listaDeTags[i].AgregarPalabra(palabra);
            }
            else
                listaDeTags.Add(tags);
        }
        
        public void SumarEtiqueta()
        {
            cantidadDeEtiquetas++;
        }

        private int CantidadDeErrores()
        {
            return listaDeTags.Sum(s => s.TotalDePalabras);
        }        
        
        public MatrizDeConfusión FinalizarProceso()
        {
            var cantidadDeErrores = CantidadDeErrores();
            return new MatrizDeConfusión(listaDeTags, cantidadDeEtiquetas, cantidadDeErrores);
        }
    }

    public class MatrizDeConfusión : Matriz, IEnumerable<Celda>
    {
        public MatrizDeConfusión(List<Celda> listaDeTags, int cantidadDeEtiquetas, int cantidadDeErrores)
        {
            this.listaDeTags = listaDeTags;
            CantidadDeEtiquetas = cantidadDeEtiquetas;            
            CantidadDeErrores = cantidadDeErrores;
            CantidadDeAciertos = cantidadDeEtiquetas - cantidadDeErrores;
        }

        public int CantidadDeAciertos { get; private set; }
        public int CantidadDeErrores { get; private set; }
        public int CantidadDeEtiquetas{get; private set; }

        public double PorcentajeDeAciertos
        {
            get { return (double)CantidadDeAciertos / (double)CantidadDeEtiquetas * 100; }            
        }

        public double PorcentajeDeErrores
        {
            get { return (double)CantidadDeErrores / (double)CantidadDeEtiquetas * 100; }            
        }

        public int ObtenerCantidadDeErrores(string tagDePrueba, string tagGoldStandard)
        {
            foreach (var tags in listaDeTags)
                if (tags.TagDePrueba == tagDePrueba && tags.TagGoldStandard == tagGoldStandard)
                    return tags.TotalDePalabras;

            return 0;
        }

        public List<int> ObtenerErrores(int cuantos)
        {
            return listaDeTags.Select(tags => tags.TotalDePalabras).Take(cuantos).ToList();
        }

        public Tuple<IEnumerable<string>, IEnumerable<string>> ObtenerTags(int cuantos)
        {
            var tagsFila = new List<string>();
            var tagsColumna = new List<string>();
            foreach (var tags in listaDeTags)
            {
                if (!tagsColumna.Contains(tags.TagDePrueba))
                    tagsColumna.Add(tags.TagDePrueba);
                if (!tagsFila.Contains(tags.TagGoldStandard))
                    tagsFila.Add(tags.TagGoldStandard);
            }

            return new Tuple<IEnumerable<string>, IEnumerable<string>>(tagsFila.Take(cuantos), tagsColumna.Take(cuantos));
        }

        public Tuple<IEnumerable<string>, IEnumerable<string>> TomarLosDeMayorError(int cuantos)
        {
            OrdenarPorMayorError();
            return ObtenerTags(cuantos);
        }

        public void OrdenarPorMayorError()
        {
            listaDeTags = listaDeTags.OrderByDescending(s => s.TotalDePalabras).ToList();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Celda> GetEnumerator()
        {
            return listaDeTags.GetEnumerator();
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var tag in listaDeTags)
                sb.AppendLine(tag.ToString());
            return sb.ToString();
        }        
    }
}