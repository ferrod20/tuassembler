//#define HacerLegible
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//-extraer "Datos/Extraccion/Cobuild.original.legible" "Datos/Extraccion/Cobuild.extracted" "Datos/Extraccion/Cobuild.extracted.info"
//-comparar "Datos/Extraccion/Cobuild.tagged" "Datos/Extraccion/Cobuild.extracted" "Datos/Extraccion/Cobuild.mconf"
namespace ConsoleApplication1
{    
    internal class Program
    {
        #region Constantes
        private const string cobuildOriginal = @"Datos\Extraccion\Cobuild.original";
        #endregion

        #region Variables de clase
        private static readonly List<string> verbosIrregulares = new List<string>
        {
         "abide",
"abode",
"abided",
"abode",
"abided",
"abidden",
"abides",
"abiding",
"alight",
"alit",
"alighted",
"alit",
"alighted",
"alights",
"alighting",
"arise",
"arose",
"arisen",
"arises",
"arising",
"awake",
"awoke",
"awoken",
"awakes",
"awaking",
"be",
"was",
"were",
"been",
"is",
"being",
"bear",
"bore",
"born",
"borne",
"bears",
"bearing",
"beat",
"beat",
"beaten",
"beats",
"beating",
"become",
"became",
"become",
"becomes",
"becoming",
"begin",
"began",
"begun",
"begins",
"beginning",
"behold",
"beheld",
"beheld",
"beholds",
"beholding",
"bend",
"bent",
"bent",
"bends",
"bending",
"bet",
"bet",
"bet",
"bets",
"betting",
"bid",
"bade",
"bidden",
"bids",
"bidding",
"bid",
"bid",
"bid",
"bids",
"bidding",
"bind",
"bound",
"bound",
"binds",
"binding",
"bite",
"bit",
"bitten",
"bites",
"biting",
"bleed",
"bled",
"bled",
"bleeds",
"bleeding",
"blow",
"blew",
"blown",
"blows",
"blowing",
"break",
"broke",
"broken",
"breaks",
"breaking",
"breed",
"bred",
"bred",
"breeds",
"breeding",
"bring",
"brought",
"brought",
"brings",
"bringing",
"broadcast",
"broadcast",
"broadcasted",
"broadcast",
"broadcasted",
"broadcasts",
"broadcasting",
"build",
"built",
"built",
"builds",
"building",
"burn",
"burnt",
"burned",
"burnt",
"burned",
"burns",
"burning",
"burst",
"burst",
"burst",
"bursts",
"bursting",
"bust",
"bust",
"bust",
"busts",
"busting",
"buy",
"bought",
"bought",
"buys",
"buying",
"cast",
"cast",
"cast",
"casts",
"casting",
"catch",
"caught",
"caught",
"catches",
"catching",
"choose",
"chose",
"chosen",
"chooses",
"choosing",
"clap",
"clapped",
"clapt",
"clapped",
"clapt",
"claps",
"clapping",
"cling",
"clung",
"clung",
"clings",
"clinging",
"clothe",
"clad",
"clothed",
"clad",
"clothed",
"clothes",
"clothing",
"come",
"came",
"come",
"comes",
"coming",
"cost",
"cost",
"cost",
"costs",
"costing",
"creep",
"crept",
"crept",
"creeps",
"creeping",
"cut",
"cut",
"cut",
"cuts",
"cutting",
"dare",
"dared",
"durst",
"dared",
"dares",
"daring",
"deal",
"dealt",
"dealt",
"deals",
"dealing",
"dig",
"dug",
"dug",
"digs",
"digging",
"dive",
"dived",
"dove",
"dived",
"dives",
"diving",
"do",
"did",
"done",
"does",
"doing",
"draw",
"drew",
"drawn",
"draws",
"drawing",
"dream",
"dreamt",
"dreamed",
"dreamt",
"dreamed",
"dreams",
"dreaming",
"drink",
"drank",
"drunk",
"drinks",
"drinking",
"drive",
"drove",
"driven",
"drives",
"driving",
"dwell",
"dwelt",
"dwelt",
"dwells",
"dwelling",
"eat",
"ate",
"eaten",
"eats",
"eating",
"fall",
"fell",
"fallen",
"falls",
"falling",
"feed",
"fed",
"fed",
"feeds",
"feeding",
"feel",
"felt",
"felt",
"feels",
"feeling",
"fight",
"fought",
"fought",
"fights",
"fighting",
"find",
"found",
"found",
"finds",
"finding",
"fit",
"fit",
"fitted",
"fit",
"fitted",
"fits",
"fitting",
"flee",
"fled",
"fled",
"flees",
"fleeing",
"fling",
"flung",
"flung",
"flings",
"flinging",
"fly",
"flew",
"flown",
"flies",
"flying",
"forbid",
"forbade",
"forbad",
"forbidden",
"forbids",
"forbidding",
"forecast",
"forecast",
"forecasted",
"forecast",
"forecasted",
"forecasts",
"forecasting",
"foresee",
"foresaw",
"foreseen",
"foresees",
"foreseeing",
"foretell",
"foretold",
"foretold",
"foretells",
"foretelling",
"forget",
"forgot",
"forgotten",
"forgets",
"foregetting",
"forgive",
"forgave",
"forgiven",
"forgives",
"forgiving",
"forsake",
"forsook",
"forsaken",
"forsakes",
"forsaking",
"freeze",
"froze",
"frozen",
"freezes",
"freezing",
"frostbite",
"frostbit",
"frostbitten",
"frostbites",
"frostbiting",
"get",
"got",
"got",
"gotten",
"gets",
"getting",
"give",
"gave",
"given",
"gives",
"giving",
"go",
"went",
"gone",
"been",
"goes",
"going",
"grind",
"ground",
"ground",
"grinds",
"grinding",
"grow",
"grew",
"grown",
"grows",
"growing",
"handwrite",
"handwrote",
"handwritten",
"handwrites",
"handwriting",
"hang",
"hung",
"hanged",
"hung",
"hanged",
"hangs",
"hanging",
"have",
"had",
"had",
"has",
"having",
"hear",
"heard",
"heard",
"hears",
"hearing",
"hide",
"hid",
"hidden",
"hides",
"hiding",
"hit",
"hit",
"hit",
"hits",
"hitting",
"hold",
"held",
"held",
"holds",
"holding",
"hurt",
"hurt",
"hurt",
"hurts",
"hurting",
"inlay",
"inlaid",
"inlaid",
"inlays",
"inlaying",
"input",
"input",
"inputted",
"input",
"inputted",
"inputs",
"inputting",
"interlay",
"interlaid",
"interlaid",
"interlays",
"interlaying",
"keep",
"kept",
"kept",
"keeps",
"keeping",
"kneel",
"knelt",
"kneeled",
"knelt",
"kneeled",
"kneels",
"kneeling",
"knit",
"knit",
"knitted",
"knit",
"knitted",
"knits",
"knitting",
"know",
"knew",
"known",
"knows",
"knowing",
"lay",
"laid",
"laid",
"lays",
"laying",
"lead",
"led",
"led",
"leads",
"leading",
"lean",
"leant",
"leaned",
"leant",
"leaned",
"leans",
"leaning",
"leap",
"leapt",
"leaped",
"leapt",
"leaped",
"leaps",
"leaping",
"learn",
"learnt",
"learned",
"learnt",
"learned",
"learns",
"learning",
"leave",
"left",
"left",
"leaves",
"leaving",
"lend",
"lent",
"lent",
"lends",
"lending",
"let",
"let",
"let",
"lets",
"letting",
"lie",
"lay",
"lain",
"lies",
"lying",
"light",
"lit",
"lit",
"lights",
"lighting",
"lose",
"lost",
"lost",
"loses",
"losing",
"make",
"made",
"made",
"makes",
"making",
"mean",
"meant",
"meant",
"means",
"meaning",
"meet",
"met",
"met",
"meets",
"meeting",
"melt",
"melted",
"molten",
"melted",
"melts",
"melting",
"mislead",
"misled",
"misled",
"misleads",
"misleading",
"mistake",
"mistook",
"mistaken",
"mistakes",
"mistaking",
"misunderstand",
"misunderstood",
"misunderstood",
"misunderstands",
"misunderstanding",
"miswed",
"miswed",
"miswedded",
"miswed",
"miswedded",
"misweds",
"miswedding",
"mow",
"mowed",
"mown",
"mows",
"mowing",
"overdraw",
"overdrew",
"overdrawn",
"overdraws",
"overdrawing",
"overhear",
"overheard",
"overheard",
"overhears",
"overhearing",
"overtake",
"overtook",
"overtaken",
"overtakes",
"overtaking",
"pay",
"paid",
"paid",
"pays",
"paying",
"preset",
"preset",
"preset",
"presets",
"presetting",
"prove",
"proved",
"proven",
"proved",
"proves",
"proving",
"put",
"put",
"put",
"puts",
"putting",
"quit",
"quit",
"quit",
"quits",
"quitting",
"re-prove",
"re-proved",
"re-proven",
"re-proved",
"re-proves",
"re-proving",
"read",
"read",
"read",
"reads",
"reading",
"rid",
"rid",
"ridded",
"rid",
"ridded",
"rids",
"ridding",
"ride",
"rode",
"ridden",
"rides",
"riding",
"ring",
"rang",
"rung",
"rings",
"ringing",
"rise",
"rose",
"risen",
"rises",
"rising",
"rive",
"rived",
"riven",
"rived",
"rives",
"riving",
"run",
"ran",
"run",
"runs",
"running",
"saw",
"sawed",
"sawn",
"sawed",
"saws",
"sawing",
"say",
"said",
"said",
"says",
"saying",
"see",
"saw",
"seen",
"sees",
"seeing",
"seek",
"sought",
"sought",
"seeks",
"seeking",
"sell",
"sold",
"sold",
"sells",
"selling",
"send",
"sent",
"sent",
"sends",
"sending",
"set",
"set",
"set",
"sets",
"setting",
"sew",
"sewed",
"sewn",
"sewed",
"sews",
"sewing",
"shake",
"shook",
"shaken",
"shakes",
"shaking",
"shave",
"shaved",
"shaven",
"shaved",
"shaves",
"shaving",
"shear",
"shore",
"sheared",
"shorn",
"sheared",
"shears",
"shearing",
"shed",
"shed",
"shed",
"sheds",
"shedding",
"shine",
"shone",
"shone",
"shines",
"shining",
"shoe",
"shod",
"shod",
"shoes",
"shoeing",
"shoot",
"shot",
"shot",
"shoots",
"shooting",
"show",
"showed",
"shown",
"shows",
"showing",
"shrink",
"shrank",
"shrunk",
"shrinks",
"shrinking",
"shut",
"shut",
"shut",
"shuts",
"shutting",
"sing",
"sang",
"sung",
"sings",
"singing",
"sink",
"sank",
"sunk",
"sinks",
"sinking",
"sit",
"sat",
"sat",
"sits",
"sitting",
"slay",
"slew",
"slain",
"slays",
"slaying",
"sleep",
"slept",
"slept",
"sleeps",
"sleeping",
"slide",
"slid",
"slid",
"slidden",
"slides",
"sliding",
"sling",
"slung",
"slung",
"slings",
"slinging",
"slink",
"slunk",
"slunk",
"slinks",
"slinking",
"slit",
"slit",
"slit",
"slits",
"slitting",
"smell",
"smelt",
"smelled",
"smelt",
"smelled",
"smells",
"smelling",
"sneak",
"sneaked",
"snuck",
"sneaked",
"snuck",
"sneaks",
"sneaking",
"soothsay",
"soothsaid",
"soothsaid",
"soothsays",
"soothsaying",
"sow",
"sowed",
"sown",
"sows",
"sowing",
"speak",
"spoke",
"spoken",
"speaks",
"speaking",
"speed",
"sped",
"speeded",
"sped",
"speeded",
"speeds",
"speeding",
"spell",
"spelt",
"spelled",
"spelt",
"spelled",
"spells",
"spelling",
"spend",
"spent",
"spent",
"spends",
"spending",
"spill",
"spilt",
"spilled",
"spilt",
"spilled",
"spills",
"spilling",
"spin",
"span",
"spun",
"spun",
"spins",
"spinning",
"spit",
"spat",
"spit",
"spat",
"spit",
"spits",
"spitting",
"split",
"split",
"split",
"splits",
"splitting",
"spoil",
"spoilt",
"spoiled",
"spoilt",
"spoiled",
"spoils",
"spoiling",
"spread",
"spread",
"spread",
"spreads",
"spreading",
"spring",
"sprang",
"sprung",
"springs",
"springing",
"stand",
"stood",
"stood",
"stands",
"standing",
"steal",
"stole",
"stolen",
"steals",
"stealing",
"stick",
"stuck",
"stuck",
"sticks",
"sticking",
"sting",
"stung",
"stung",
"stings",
"stinging",
"stink",
"stank",
"stunk",
"stinks",
"stinking",
"stride",
"strode",
"strided",
"stridden",
"strides",
"striding",
"strike",
"struck",
"struck",
"stricken",
"strikes",
"striking",
"string",
"strung",
"strung",
"strings",
"stringing",
"strip",
"stript",
"stripped",
"stript",
"stripped",
"strips",
"stripping",
"strive",
"strove",
"striven",
"strives",
"striving",
"sublet",
"sublet",
"sublet",
"sublets",
"subletting",
"sunburn",
"sunburned",
"sunburnt",
"sunburned",
"sunburnt",
"sunburns",
"sunburning",
"swear",
"swore",
"sworn",
"swears",
"swearing",
"sweat",
"sweat",
"sweated",
"sweat",
"sweated",
"sweats",
"sweating",
"sweep",
"swept",
"sweeped",
"swept",
"sweeped",
"sweeps",
"sweeping",
"swell",
"swelled",
"swollen",
"swells",
"swelling",
"swim",
"swam",
"swum",
"swims",
"swimming",
"swing",
"swung",
"swung",
"swings",
"swinging",
"take",
"took",
"taken",
"takes",
"taking",
"teach",
"taught",
"taught",
"teaches",
"teaching",
"tear",
"tore",
"torn",
"tears",
"tearing",
"tell",
"told",
"told",
"tells",
"telling",
"think",
"thought",
"thought",
"thinks",
"thinking",
"thrive",
"throve",
"thrived",
"thriven",
"thrived",
"thrives",
"thriving",
"throw",
"threw",
"thrown",
"throws",
"throwing",
"thrust",
"thrust",
"thrust",
"thrusts",
"thrusting",
"tread",
"trod",
"trodden",
"treads",
"treading",
"undergo",
"underwent",
"undergone",
"undergoes",
"undergoing",
"understand",
"understood",
"understood",
"understands",
"understanding",
"undertake",
"undertook",
"undertaken",
"undertakes",
"undertaking",
"upset",
"upset",
"upset",
"upsets",
"upsetting",
"vex",
"vext",
"vexed",
"vext",
"vexed",
"vexes",
"vexing",
"wake",
"woke",
"woken",
"wakes",
"waking",
"wear",
"wore",
"worn",
"wears",
"wearing",
"weave",
"wove",
"woven",
"weaves",
"weaving",
"wed",
"wed",
"wedded",
"wed",
"wedded",
"weds",
"wedding",
"weep",
"wept",
"wept",
"weeps",
"weeping",
"wend",
"wended",
"went",
"wended",
"went",
"wends",
"wending",
"wet",
"wet",
"wetted",
"wet",
"wetted",
"wets",
"wetting",
"win",
"won",
"won",
"wins",
"winning",
"wind",
"wound",
"wound",
"winds",
"winding",
"withdraw",
"withdrew",
"withdrawn",
"withdraws",
"withdrawing",
"withhold",
"withheld",
"withheld",
"withholds",
"withholding",
"withstand",
"withstood",
"withstood",
"withstands",
"withstanding",
"wring",
"wrung",
"wrung",
"wrings",
"wringing",
"write",
"wrote",
"written",
"writes",
"writing",
"zinc",
"zinced",
"zincked",
"zinced",
"zincked",
"zincs",
"zincking"                                                                
        };
        private static string cobuildOriginalLegible = @"Datos\Extraccion\Cobuild.original.legible";
        #endregion

        #region Métodos
        private static string ConvertirAAscii(string texto)
        {
            var sb = new StringBuilder();

            foreach (var car in texto)
            {
                if (0 < car && car < 255)
                    sb.Append(car);
                if (0 == car)
                    sb.Append(" ");
            }

            return sb.ToString();
        }

        private static string HacerLegiblElBloque(string bloque)
        {
            var partes = bloque.Split('\n');
            var palabra = partes[1].TrimEnd();
            var salida = palabra;
            KeyValuePair<string, string> tip;

            for (var i = 2; i < partes.Length - 1; i++)
            {
                var parte = partes[i].TrimEnd();
                salida += parte + "\n";

                //if (EsEjemplo(ejemplo, palabra, palabrasAnteriores))
                //else if (EsTipo(ejemplo, out tip))
                //  salida += ejemplo + "\n";                                    
            }
            return salida;
        }

        private static void Main(string[] args)
        {
            if(args.Length == 0)
                Console.Write("-help para obtener información de los comandos disponibles");
            else
            {
                var comando = args[0];
                Console.WriteLine();
                args = args.Select(a => a.Replace("\"", "")).ToArray();
                switch (comando)
                {
                    
                    case "-extraer":
                        Extraer(args);
                        break;
                    case "-2pasada":
                        SegundaPasada(args);
                        break;
                    case "-unir":
                        Unir(args);
                        break;
                    case "-comparar":
                        Comparar(args);
                        break;
                    case "-help":
                        Help();
                        break;
                    default:
                        Console.WriteLine("-help para obtener información de los comandos disponibles");
                        break;
                }
            }
        }

        private static void Help()
        {
            Console.WriteLine("-help: este comando de ayuda");
            Console.WriteLine();
            Console.WriteLine("-extraer <CobuildOriginal> <Salida> <SalidaInfo>");
            Console.WriteLine("\t Extrae la informacion gramatical de CobuildOriginal");
            Console.WriteLine("\t CobuildOriginal: nombre del archivo cobuild original");
            Console.WriteLine("\t Salida: nombre del archivo en donde se extraerá la información de cobuild");
            Console.WriteLine(
                "\t SalidaInfo: nombre del archivo en donde se guardará información del archivo extraido: cantidad de palabras, cantidad de etiquetas, etc.");
            Console.WriteLine();
            Console.WriteLine("-unir <CobuildExtraido> <CobuildEtiquetado> <Salida>");
            Console.WriteLine(
                "\t Une la información de etiquetas de cobuild con cobuild etiquetado. Manteniendo las etiquetas de cobuild.");
            Console.WriteLine("\t CobuildExtraido: nombre del archivo extraido de cobuild");
            Console.WriteLine("\t CobuildEtiquetado: nombre del archivo cobuild etiquetado");
            Console.WriteLine("\t Salida: nombre del archivo cobuild etiquetado");
            Console.WriteLine();
            Console.WriteLine("-comparar <GoldStandard> <ArchivoAComparar> <Salida> [-l]");
            Console.WriteLine("\t Compara el archivo GoldStandard contra el ArchivoAComparar generando una matriz de confusion en Salida.");
            Console.WriteLine("\t -l: genera una matriz de confusión para latex");
        }

        private static void Comparar(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("No se han definido los archivos <GoldStandard> <ArchivoParaComparar> y <Salida>");
                Console.WriteLine("-help para obtener información de los comandos disponibles");
            }
            else
            {
                var generarMatrizDeConfParaLatex = args.Length > 4 && args[4] == "-l";

                Console.WriteLine( (generarMatrizDeConfParaLatex? "(latex)":"") + "Comparando: " + Path.GetFileName(args[1]) + "(gold standard) contra " +
                                  Path.GetFileName(args[2]));
                Console.WriteLine("Salida: " + Path.GetFileName(args[3]));
                Console.WriteLine();
                
                var titulo = args.Length > 5? args[5]:"";
                var tituloFila = args.Length > 6 ? args[6] : "";
                var tituloColumna = args.Length > 7 ? args[7] : "";


                Comparador.Comparar(args[1], args[2], args[3], generarMatrizDeConfParaLatex, titulo, tituloFila, tituloColumna);
            }
        }

        private static void SegundaPasada(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("No se han definido los archivos <CobuildExtraido> <CobuildEtiquetado> y <Salida>");
                Console.WriteLine("-help para obtener información de los comandos disponibles");
            }
            else
            {

                Console.WriteLine("Generando la 2 pasada: " + Path.GetFileName(args[1]) + " con " + Path.GetFileName(args[2]));
                Console.WriteLine("Salida: " + Path.GetFileName(args[3]));
                Console.WriteLine();

                UnirCobuildExtraidoConCobuildTaggeado(args[1], args[2], args[3], SegundaPasada);
            }
        }

        private static void Unir(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("No se han definido los archivos <CobuildExtraido> <CobuildEtiquetado> y <Salida>");
                Console.WriteLine("-help para obtener información de los comandos disponibles");
            }
            else
            {

                Console.WriteLine("Uniendo: " + Path.GetFileName(args[1]) + " con " + Path.GetFileName(args[2]));
                Console.WriteLine("Salida: " + Path.GetFileName(args[3]));
                Console.WriteLine();

                UnirCobuildExtraidoConCobuildTaggeado(args[1], args[2], args[3], Unir);
            }
        }

        private static void Extraer(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("No se han definido los archivos <CobuildOriginal> <CobuildExtraido> y <CobuildInfoExtraido>");
                Console.WriteLine("-help para obtener información de los comandos disponibles");
            }
            else
            {
                Console.WriteLine("Extrayendo: " + Path.GetFileName(args[1]));
                Console.WriteLine("Salida: " + Path.GetFileName(args[2]));
                Console.WriteLine("Info: " + Path.GetFileName(args[3]));
                Console.WriteLine();

                Extractor.ExtraerLaInformaciónDeCobuild(args[1], args[2], args[3]);
            }
        }

        private static void UnirCobuildExtraidoConCobuildTaggeado(string cobuildExtraido, string cobuildEtiquetado, string cobuildFinal, Func<TextWriter, string, string, string[], bool> Hacer)
        {
            var textoExtraido = new StreamReader(cobuildExtraido);
            var textoEtiquetado = new StreamReader(cobuildEtiquetado);

            TextWriter salida = new StreamWriter(cobuildFinal, false, Encoding.Default);

            var líneaExtraída = textoExtraido.ReadLine();
            var etiquetaEtiquetada = textoEtiquetado.ReadLine();

            while (líneaExtraída != null && etiquetaEtiquetada != null)
            {
                if( líneaExtraída != string.Empty )
                {
                    var partesExtraídas = líneaExtraída.Split();
                    salida.Write(partesExtraídas[0]);                    
                    var etiquetaExtraída = partesExtraídas.Last();
                    Hacer(salida, etiquetaExtraída, etiquetaEtiquetada, partesExtraídas);

                    salida.WriteLine();
                }

                líneaExtraída = textoExtraido.ReadLine();
                etiquetaEtiquetada = textoEtiquetado.ReadLine();    
            }

            salida.Close();
            textoExtraido.Close();
            textoEtiquetado.Close();
        }

        private static bool Unir(TextWriter salida, string etiquetaExtraída, string etiquetaEtiquetada, string[] partesExtraídas)
        {
            salida.Write("\t");
            if (partesExtraídas.Count() > 1 && !string.IsNullOrEmpty(etiquetaExtraída) && etiquetaExtraída != "VBD|VBN")
                salida.Write(etiquetaExtraída);
            else
                salida.Write(etiquetaEtiquetada.Split().Last());

            return true;
        }

        /// <summary>
        /// Si se obtuvo de Cobuild NN 	pero TnT asigno NNS, NNP o NNPS,  asignar tag TnT
        /// Si se obtuvo de Cobuild NNS pero TnT asigno NNPS, asignar NNPS
        /// Si se obtuvo de Cobuild VB  pero TnT asigno VBN, VBD, VBZ, VBP, VBG  asignar tag TnT
        /// Si se obtuvo de Cobuild JJ  pero TnT asigno JJR o JJS  asignar tag TnT
        /// Si se obtuvo de Cobuild RB  pero TnT asigno RBR o RBS  asignar tag TnT
        /// Si se obtuvo de Cobuild WP  pero TnT asigno WP$  asignar tag TnT
        /// Si se obtuvo de Cobuild PRP  pero TnT asigno PRP$  asignar tag TnT
        /// </summary>
        private static bool SegundaPasada(TextWriter salida, string etiquetaExtraída, string etiquetaEtiquetada, string[] partesExtraídas)
        {
            var etiquetado = etiquetaEtiquetada.Split().Last().Trim();
            var palabra = etiquetaEtiquetada.Split().First().Trim();
            if (partesExtraídas.Count() > 1 && !string.IsNullOrEmpty(etiquetaExtraída) )
            {
                if (etiquetado.StartsWith("VB") && verbosIrregulares.Contains(palabra.Trim().ToLower()))
                {
                    salida.Write("\t");
                    salida.Write(etiquetado);
                }
                else                
                    switch (etiquetaExtraída.Trim())
                    {
                        case "NN":
                            salida.Write("\t");
                            salida.Write(etiquetado.EsAlgunaDeEstas("NNS", "NNP", "NNPS") ? etiquetado : etiquetaExtraída);
                            break;
                        case "NNS":
                            salida.Write("\t");
                            salida.Write(etiquetado == "NNPS" ? etiquetado : etiquetaExtraída);
                            break;
                        case "VB":
                            salida.Write("\t");
                            salida.Write(etiquetado.EsAlgunaDeEstas("VBN", "VBD", "VBZ", "VBP", "VBG") ? etiquetado : etiquetaExtraída);                    
                            break;
                        case "JJ":
                            salida.Write("\t");
                            salida.Write(etiquetado.EsAlgunaDeEstas("JJR", "JJS") ? etiquetado : etiquetaExtraída);
                            break;
                        case "RB":
                            salida.Write("\t");
                            salida.Write(etiquetado.EsAlgunaDeEstas("RBR", "RBS") ? etiquetado : etiquetaExtraída);
                            break;
                        case "WP":
                            salida.Write("\t");
                            salida.Write(etiquetado.EsAlgunaDeEstas("WP$") ? etiquetado : etiquetaExtraída);
                            break;
                        case "PRP":
                            salida.Write("\t");
                            salida.Write(etiquetado.EsAlgunaDeEstas("PRP$") ? etiquetado : etiquetaExtraída);
                            break;

                    }
                }

            

            return true;
        }

        private static void HacerLegibleCobuild()
        {
            var texto = File.ReadAllText(cobuildOriginal);

            TextWriter salida = new StreamWriter(cobuildOriginalLegible, false, Encoding.Default);

            PonerSaltosDeLinea(texto, salida);
            salida.Close();
        }

        private static void PonerSaltosDeLinea(string texto, TextWriter salida)
        {
            //An ostrich cannot fly...
            var texto2 = ConvertirAAscii(texto);
            salida.Write(texto2);
        }
        #endregion
    }
}