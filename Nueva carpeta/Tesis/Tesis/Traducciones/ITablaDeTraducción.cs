using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{    
    public interface ITablaDeTraducción
    {
        List<string> ObtenerTraduccionPara(string tag2);
        bool ContieneTraduccionPara(string tag2);
        string ObtenerTraduccionInversaPara(string tagFila);
        IEnumerable<string> ObtenerTraduccionesParaEtiquetaQueEmpiezaCon(string posibleEtiqueta);
    }

    public class TablaDeTraducciónVacía: ITablaDeTraducción
    {
        public List<string> ObtenerTraduccionPara(string tag)
        {
            return new List<string>{tag};
        }

        public bool ContieneTraduccionPara(string tag)
        {
            return true;
        }

        public string ObtenerTraduccionInversaPara(string tagFila)
        {
            return tagFila;
        }

        public IEnumerable<string> ObtenerTraduccionesParaEtiquetaQueEmpiezaCon(string posibleEtiqueta)
        {
            return ObtenerTraduccionPara(posibleEtiqueta);
        }
    }
}