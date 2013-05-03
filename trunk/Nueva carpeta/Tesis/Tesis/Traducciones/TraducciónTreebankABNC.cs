using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{    
    public class TraducciónTreebankABNC: ITablaDeTraducción
    {
        private static Dictionary<string, List<string>> traducciónTreebankABNC = new Dictionary<string, List<string>>
                                                                                     {
                                                                                          {"JJ", new List<string>{"AJ0", "ORD"} },
{"JJR", new List<string>{"AJC"} },
{"JJS", new List<string>{"AJS"} },
{"DT", new List<string>{"AT0", "DT0"} },
{"RB", new List<string>{"AV0", "XX0"} }, 
{"RP", new List<string>{"AVP"} }, 
{"WRB", new List<string>{"AVQ"} }, 
{"CC", new List<string>{"CJC"} }, 
{"IN", new List<string>{"AV0", "AVP", "CJS", "CJT", "PRF", "PRP"} }, 
{"CD", new List<string>{"CRD"} }, 
{"PRP$", new List<string>{"DPS", "PNI"} }, 
{"WDT", new List<string>{"DTQ"} },   
{"EX", new List<string>{"EX0"} },   
{"UH", new List<string>{"ITJ"} },     
{"NN", new List<string>{"NN0", "NN1", "PNI"} },     
{"NNS", new List<string>{"NN2"} },      
{"NNP", new List<string>{"NP0"} },        
{"NNPS", new List<string>{"NP0"} },        
{"PRP", new List<string>{"PNX", "PNP"} },        
{"WP", new List<string>{"PNQ", "DTQ"} },        
{"POS", new List<string>{"POS"} },        
{"TO", new List<string>{"TO0", "PRP"} },        
{"VBP", new List<string>{"VVB","VBB", "VDB", "VHB"} },        
{"VBD", new List<string>{"VBD", "VDD", "VHD", "VVD"} },        
{"VBG", new List<string>{"VBG", "VDG", "VHG", "VVG"} },        
{"VB", new List<string>{"VBI", "VDI", "VHI", "VVB", "VVI"} },        
{"VBN", new List<string>{"VBN", "VHN", "VDN", "VVN"} },        
{"VBZ", new List<string>{"VBZ", "VDZ", "VHZ", "VVZ"} },        
{"MD", new List<string>{"VM0"} },        
{"FW", new List<string>{"UNC"} },        
{"LS", new List<string>{"ORD"} },        
{"PDT", new List<string>{"DT0"} },        
{"RBR", new List<string>{"AV0"} },        
{"RBS", new List<string>{"AV0"} },        
{"SYM", new List<string>{"ZZ0"} },        
{"WP$", new List<string>{"PNQ"} },        
 

{"'", new List<string>{"PUQ"} },        
{"''", new List<string>{"PUQ"} },        
{"\"", new List<string>{"PUQ"} },        
{"(", new List<string>{"PUL"} },        
{"[", new List<string>{"PUL"} },        
{")", new List<string>{"PUR"} },        
{"]", new List<string>{"PUR"} },        
{",", new List<string>{"PUN"} },        
{".", new List<string>{"PUN"} },        
{"!", new List<string>{"PUN"} },        
{"?", new List<string>{"PUN"} },        
{":", new List<string>{"PUN"} },        
{";", new List<string>{"PUN"} },        
{"-", new List<string>{"PUN"} },
{"``", new List<string>{"PUQ"} },
{"`", new List<string>{"PUQ"} }
};

        public List<string> ObtenerTraduccionPara(string tag)
        {
            return traducciónTreebankABNC[tag];
        }

        public bool ContieneTraduccionPara(string tag)
        {
            return traducciónTreebankABNC.ContainsKey(tag);
        }

        public string ObtenerTraduccionInversaPara(string tag)
        {
            var resultado = tag;
            var traducción = traducciónTreebankABNC.FirstOrDefault(trad => trad.Value.Contains(tag));

            if( !string.IsNullOrWhiteSpace(traducción.Key))
                resultado = traducción.Key;
            
            return resultado;
        }

        public IEnumerable<string> ObtenerTraduccionesParaEtiquetaQueEmpiezaCon(string posibleEtiqueta)
        {
            return traducciónTreebankABNC.Where(t => t.Key.StartsWith(posibleEtiqueta)).SelectMany(trad => trad.Value);
        }
    }        
}