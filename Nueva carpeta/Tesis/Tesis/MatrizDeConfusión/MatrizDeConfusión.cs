using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class MatrizDeConfusión : Matriz, IEnumerable<Celda>
    {
        public MatrizDeConfusión(List<Celda> celdas, int cantidadDeEtiquetas, int cantidadDeErrores)
        {
            this.celdas = celdas;
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
            foreach (var celda in celdas)
                if (celda.TagDePrueba == tagDePrueba && celda.TagGoldStandard == tagGoldStandard)
                    return celda.TotalDePalabras;

            return 0;
        }

        public List<int> ObtenerErrores(int cuantos)
        {
            return celdas.Select(tags => tags.TotalDePalabras).Take(cuantos).ToList();
        }

        public Tuple<IEnumerable<string>, IEnumerable<string>> ObtenerTags(int cuantos)
        {
            var tagsFila = new List<string>();
            var tagsColumna = new List<string>();
            foreach (var celda in celdas)
            {
                if (!tagsColumna.Contains(celda.TagDePrueba))
                    tagsColumna.Add(celda.TagDePrueba);
                if (!tagsFila.Contains(celda.TagGoldStandard))
                    tagsFila.Add(celda.TagGoldStandard);
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
            celdas = celdas.OrderByDescending(s => s.TotalDePalabras).ToList();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Celda> GetEnumerator()
        {
            return celdas.GetEnumerator();
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var tag in celdas)
                sb.AppendLine(tag.ToString());
            return sb.ToString();
        }        
    }
}