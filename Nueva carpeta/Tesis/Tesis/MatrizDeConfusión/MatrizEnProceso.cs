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
            celdas = new List<Celda>();
        }

        protected List<Celda> celdas;        
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

            if (celdas.Contains(tags))
            {
                var i = celdas.IndexOf(tags);
                celdas[i].AgregarPalabra(palabra);
            }
            else
                celdas.Add(tags);
        }
        
        public void SumarEtiqueta()
        {
            cantidadDeEtiquetas++;
        }

        private int CantidadDeErrores()
        {
            return celdas.Sum(s => s.TotalDePalabras);
        }        
        
        public MatrizDeConfusión FinalizarProceso()
        {
            var cantidadDeErrores = CantidadDeErrores();
            return new MatrizDeConfusión(celdas, cantidadDeEtiquetas, cantidadDeErrores);
        }
    }
}