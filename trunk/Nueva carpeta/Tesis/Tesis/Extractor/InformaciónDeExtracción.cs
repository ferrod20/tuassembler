using System.IO;

namespace ConsoleApplication1
{
    public class InformaciónDeExtracción
    {
        public int CantidadDeSignosDePuntuación;
        public int CantidadDePalabras;
        public int CantidadDePalabrasEtiquetadas;
        public int CantidadDeEntradas;
        public int CantidadDeEntradasExtraídas;
        public int CantidadDeEntradasConEjemplo;
        private InformaciónDeEtiquetas infoEtiquetasCobuild;
        private InformaciónDeEtiquetas infoEtiquetasTreebank;
        private TextWriter writer;

        public InformaciónDeExtracción(string archDeInformación)
        {
            writer = new StreamWriter(archDeInformación);            
            infoEtiquetasCobuild = new InformaciónDeEtiquetas();
            infoEtiquetasTreebank = new InformaciónDeEtiquetas();
            CantidadDeEntradas =  CantidadDeEntradasExtraídas =  CantidadDeEntradasConEjemplo = CantidadDeSignosDePuntuación = CantidadDePalabras = CantidadDePalabrasEtiquetadas = 0;
        }

        public InformaciónDeExtracción GrabarEnArchivo()
        {
            writer.WriteLine("Cantidad de palabras y signos puntuación: " + CantidadDePalabrasYSignosDePuntuación);
            writer.WriteLine("Cantidad de signos de puntuación: " + CantidadDeSignosDePuntuación);
            writer.WriteLine("Cantidad de palabras: " + CantidadDePalabras);
            writer.Write("Cantidad de palabras etiquetadas: " + CantidadDePalabrasEtiquetadas);
            EscribirPorcentaje(CantidadDePalabrasEtiquetadas, CantidadDePalabras);
            
            writer.WriteLine();
            writer.WriteLine("Cantidad de entradas: " + CantidadDeEntradas);
            writer.Write("Cantidad de entradas con ejemplos: " + CantidadDeEntradasConEjemplo);
            EscribirPorcentaje(CantidadDeEntradasConEjemplo , CantidadDeEntradas);
            writer.WriteLine();
            writer.Write("Cantidad de entradas extraídas de entradas con ejemplos: " + CantidadDeEntradasExtraídas );
            EscribirPorcentaje(CantidadDeEntradasExtraídas, CantidadDeEntradasConEjemplo);

            GrabarInfoEtiquetasCobuild();
            GrabarInfoEtiquetasTreebank();
           
            writer.Close();
            return this;
        }

        private void EscribirPorcentaje(double parte, double total)
        {
            writer.WriteLine("\t({0:0.00}%)", parte * 100 / total);            
        }

        private void GrabarInfoEtiquetasTreebank()
        {
            writer.WriteLine();
            writer.WriteLine("Distribución de etiquetas Penn Treebank");
            writer.WriteLine();
            foreach (var etiquetaPalabras in infoEtiquetasTreebank.ObtenerDistribuciónDeEtiquetas(8, 3))
            {
                var infoEtiqueta = etiquetaPalabras.infoEtiqueta;
                writer.Write(infoEtiqueta.etiqueta + "\t" + infoEtiqueta.cantApariciones);
                EscribirPorcentaje(infoEtiqueta.cantApariciones, CantidadDePalabrasEtiquetadas);
                foreach (var palabra in etiquetaPalabras.palabras)
                {
                    writer.Write(palabra.Key + "\t" + palabra.Value);
                    EscribirPorcentaje(palabra.Value, infoEtiqueta.cantApariciones);
                }

                writer.WriteLine();
            }
        }

        private void GrabarInfoEtiquetasCobuild()
        {
            writer.WriteLine();
            writer.WriteLine("Distribución de etiquetas COBUILD");
            writer.WriteLine();
            foreach (var etiquetaPalabras in infoEtiquetasCobuild.ObtenerDistribuciónDeEtiquetas(8, 3))
            {
                var infoEtiqueta = etiquetaPalabras.infoEtiqueta;
                writer.Write(infoEtiqueta.etiqueta + "\t" + infoEtiqueta.cantApariciones);
                EscribirPorcentaje(infoEtiqueta.cantApariciones,CantidadDePalabrasEtiquetadas);
                foreach (var palabra in etiquetaPalabras.palabras)
                {
                    writer.Write(palabra.Key + "\t" + palabra.Value);
                    EscribirPorcentaje(palabra.Value, infoEtiqueta.cantApariciones);
                }
                    
                writer.WriteLine();
            }
        }

        public int CantidadDePalabrasYSignosDePuntuación
        {
            get
            {
                return CantidadDePalabras + CantidadDeSignosDePuntuación;    
            }
        }

        public void AgregarEtiquetaTreebank(string etiqueta, string palabra)
        {
            infoEtiquetasTreebank.Agregar(etiqueta, palabra);
        }

        public void AgregarEtiquetaCobuild(string etiquetaCobuild, string palabra)
        {
            infoEtiquetasCobuild.Agregar(etiquetaCobuild, palabra);
        }
    }
}