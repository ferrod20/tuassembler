using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class TraducciónCobuildATreebank: ITablaDeTraducción
    {

        /// <summary>
        /// Tabla para traducir etiquetas COBUILD en etiquetas Penn TreeBank
        /// </summary>
        private readonly Dictionary<string, string> traducciónCobuildATreebank = new Dictionary<string, string> {
        {"coordinating conjunction", "CC"}, 
        {"number", "CD"}, 
        {"determiner", "DT"}, 
        {"determiner + countable noun in singular", "DT"}, 
        {"preposition", "IN"}, 
        {"subordinating conjunction", "IN"}, 
        {"preposition, or adverb after verb", "IN"}, 
        {"preposition after noun", "IN"}, 
        {"adjective", "JJ"}, 
        {"classifying adjective", "JJ"}, 
        {"qualitative adjective", "JJ"}, 
        {"classifying adjective: attributive", "JJ"}, 
        {"classifying adjective: ussually attributive", "JJ"}, 
        {"qualitative adjective: attributive", "JJ"}, 
        {"adjective colour", "JJ"}, 
        {"ordinal", "JJ"}, 
        {"adjective after noun", "JJ"}, 
        {"modal", "MD"}, 
        {"adverb", "RB"}, 
        {"noun", "NN"}, 
        {"countable noun", "NN"}, 
        {"uncountable noun", "NN"}, 
        {"noun singular", "NN"}, 
        {"countable or uncountable noun", "NN"}, 
        {"countable noun with supporter", "NN"}, 
        {"countable noun: ussually singular", "NN"}, 
        {"countable noun: if + preposition then of", "NN"}, 
        {"countable noun: ussually plural", "NNS"}, 
        {"noun plural: the + noun", "NNS"}, 
        {"countable noun: usually with supporter", "NN"}, 
        {"noun singular: a + noun", "NN"}, 
        {"noun singular: the + noun", "NN"}, 
        {"uncountable or countable noun", "NN"}, 
        {"noun singular with determiner", "NN"}, 
        {"mass noun", "NN"}, 
        {"uncountable noun with supporter", "NN"}, 
        {"partitive noun", "NN"}, 
        {"noun singular with determiner with supporter", "NN"}, 
        {"countable noun + of", "NN"}, 
        {"countable noun, or by + noun", "NN"}, 
        {"countable noun or partitive noun", "NN"}, 
        {"count or uncountable noun", "NN"}, 
        {"countable noun or vocative", "NN"}, 
        {"partitive noun + uncountable noun", "NN"}, 
        {"noun singular with determiner + of", "NN"}, 
        {"noun in titles", "NN"}, 
        {"noun vocative", "NN"}, 
        {"uncountable noun + of", "NN"}, 
        {"indefinite pronoun", "NN"}, 
        {"uncountable noun, or noun singular", "NN"}, 
        {"countable noun, or in + noun", "NN"}, 
        {"partitive noun + noun in plural", "NN"}, 
        {"countable or uncountable noun with supporter", "NN"}, 
        {"uncountable noun, or noun before noun", "NN"}, 
        {"uncountable or countable noun with supporter", "NN"}, 
        {"noun before noun", "NN"}, 
        {"noun plural with supporter", "NNS"}, 
        {"noun in names", "NNP"}, 
        {"proper noun or vocative", "NNP"}, 
        {"proper noun", "NNP"}, 
        {"noun plural", "NNS"}, 
        {"predeterminer", "PDT"}, //si empieza con predeterminer....
        {"pronoun", "PP"}, //Ver.....me parece q no va
        {"possessive", "PPS"}, //si empieza con possessive....
        {"adverb with verb", "RB"}, 
        {"adverb after verb", "RB"}, 
        {"sentence adverb", "RB"}, 
        {"adverb + adjective or adverb", "RB"}, 
        {"adverb + adjective", "RB"}, 
        {"preposition or adverb", "RB"}, 
        {"adverb after verb, or classifying adjective", "RB"}, 
        {"adverb or sentence adverb", "RB"}, 
        {"adverb with verb, or sentence adverb", "RB"}, 
        {"exclamation", "UH"}, 
        {"exclam", "UH"}, 
        {"verb", "VB"}, 
        {"verb + object", "VB"}, 
        {"verb or verb + object", "VB"}, 
        {"ergative verb", "VB"}, 
        {"verb + adjunct", "VB"}, 
        {"verb: usually + adjunct", "VB"}, 
        {"verb + object + adjunct", "VB"}, 
        {"verb + object (noun group or reflexive)", "VB"}, 
        {"verb + object or reporting clause", "VB"}, 
        {"verb + object (reflexive)", "VB"}, 
        {"verb + adjunct (^i{to^i})", "VB"}, 
        {"verb + object, or phrasal verb", "VB"}, 
        {"verb + to-infinitive", "VB"}, 
        {"verb or verb + adjunct (^i{with)", "VB"}, 
        {"verb + object, verb + object + object, or verb + object + adjunct (to)", "VB"}, 
        {"ergative verb + adjunct", "VB"}, 
        {"verb + object + adjunct (to)", "VB"}, 
        {"verb + object, or verb + adjunct", "VB"}, 
        {"verb + object + adjunct (with)", "VB"}, 
        {"verb + adjunct (with)", "VB"}, 
        {"verb + complement", "VB"}, 
        {"verb + object, or verb", "VB"}, 
        {"verb + object + to-infinitive", "VB"}, 
        {"verb + reporting clause", "VB"}, 
        {"verb or ergative verb", "VB"}, 
        {"verb + adjunct (from)", "VB"}, 
        {"verb + object, verb + object + object, or verb + object + adjunct (for)", "VB"}, 
        {"wh: used as determiner", "WDT"}, 
        {"wh: used as relative pronoun", "WP"}, 
        {"wh: used as pronoun", "WP"}, 
        {"wh: used as adverb", "WRB"}};


        public List<string> ObtenerTraduccionPara(string tag)
        {
            return new List<string>{traducciónCobuildATreebank[tag]};
        }

        public bool ContieneTraduccionPara(string tag)
        {
            return traducciónCobuildATreebank.ContainsKey(tag);
        }

        public string ObtenerTraduccionInversaPara(string tag)
        {
            var resultado = tag;
            var traducción = traducciónCobuildATreebank.FirstOrDefault(trad => trad.Value == tag);

            if( !string.IsNullOrWhiteSpace(traducción.Key))
                resultado = traducción.Key;
            
            return resultado;
        }

        public IEnumerable<string> ObtenerTraduccionesParaEtiquetaQueEmpiezaCon(string posibleEtiqueta)
        {
            return traducciónCobuildATreebank.Where(t => t.Key.StartsWith(posibleEtiqueta)).Select(trad=>trad.Value);
        }
    }        
}