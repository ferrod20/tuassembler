using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class InformaciónDeEtiquetas 
    {
        protected Dictionary<string, Dictionary<string, int>> etiquetasPalabras;
        public InformaciónDeEtiquetas Agregar(string etiqueta, string palabra)
        {
            if (!etiquetasPalabras.ContainsKey(etiqueta))
                etiquetasPalabras[etiqueta] = new Dictionary<string, int>();

            if (etiquetasPalabras[etiqueta].ContainsKey(palabra))
                etiquetasPalabras[etiqueta][palabra]++;
            else
                etiquetasPalabras[etiqueta][palabra] = 1;

            return this;
        }

        public InformaciónDeEtiquetas()
        {
            etiquetasPalabras = new Dictionary<string, Dictionary<string, int>>();
        }
        
        public dynamic ObtenerDistribuciónDeEtiquetas(int cantidadMáximaDeEtiquetas, int cantidadMáximaDePalabras)
        {
            var etiquetasOrdenadas =
                etiquetasPalabras
                .Select(etiquetaPalabras => new 
                { 
                    infoEtiqueta = new { etiqueta = etiquetaPalabras.Key, cantApariciones = etiquetaPalabras.Value.Sum(palabraCant => palabraCant.Value) }, 
                    palabras = etiquetaPalabras.Value.OrderByDescending(palabraCant => palabraCant.Value).Take(cantidadMáximaDePalabras)
                })
                .OrderByDescending(etiquetaPalabras => etiquetaPalabras.infoEtiqueta.cantApariciones)
                .Take(cantidadMáximaDeEtiquetas);

            return etiquetasOrdenadas;
        }         
    } 
}