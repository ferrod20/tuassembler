using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApplication1
{
    public class Regla : NumeroAdivinado
    {
        #region Constructores
        public Regla(int n0, int n1, int n2, int n3, int bien, int regular) : base(n0, n1, n2, n3, bien, regular)
        {
        }

        public Regla(Numero numero, int bien, int regular) : base(numero, bien, regular)
        {
        }
        #endregion

        #region Métodos
        public List<GeneradorDeNumero> Generar()
        {
            var lista = new List<GeneradorDeNumero>();
            switch (Bien)
            {
                case 0:
                    switch (Regular)
                    {
                        case 0:
                            lista.Add(new GeneradorDeNumero(null, null, null, null, new List<int> {n0, n1, n2, n3}));
                            break;
                        case 1:
                            lista.Add(new GeneradorDeNumero(null, n0, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n3, null, new List<int> {n0, n1, n2, n3}));
                            break;
                        case 2:
                            lista.Add(new GeneradorDeNumero(n1, n0, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n0, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n0, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n0, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n0, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n0, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n0, n3, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n1, null, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n0, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n0, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n0, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n1, null, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n1, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n3, n0, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n1, n2, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n3, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, n3, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n2, null, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n1, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n1, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n2, null, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n3, n1, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n2, n3, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n3, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n3, n2, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, n3, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n3, null, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n3, n2, new List<int> {n0, n1, n2, n3}));
                            break;
                        case 3:
                            ///0 1 y 2                                                                      
                            lista.Add(new GeneradorDeNumero(null, n0, n1, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n0, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n0, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n0, null, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n1, n2, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, n0, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n0, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, n0, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(null, n2, n1, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n1, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n2, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n1, n0, new List<int> {n0, n1, n2, n3}));

                            ///0 1 y 3
                            lista.Add(new GeneradorDeNumero(null, n0, n3, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n0, n3, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n0, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n0, null, n1, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n1, n3, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n0, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n0, n1, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n1, n3, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, n3, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n1, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n1, n0, new List<int> {n0, n1, n2, n3}));

                            ///0 2 y 3                                                                      
                            lista.Add(new GeneradorDeNumero(null, n0, n3, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n0, n3, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n0, null, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n3, n2, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n3, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n0, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n0, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(null, n2, n3, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n3, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n3, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n2, null, n0, new List<int> {n0, n1, n2, n3}));

                            ///1 2 y 3                                                                      
                            lista.Add(new GeneradorDeNumero(n1, null, n3, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n2, n3, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n3, null, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n3, n2, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n3, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n1, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n1, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(null, n2, n3, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n3, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n3, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n2, null, n1, new List<int> {n0, n1, n2, n3}));
                            break;
                        case 4:
                            lista.Add(new GeneradorDeNumero(n1, n2, n3, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n0, n3, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n3, n0, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n2, n3, n1, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n2, n1, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n0, n1, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n2, n0, n3, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n3, n0, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n2, n0, n1, new List<int> {n0, n1, n2, n3}));
                            break;
                    }
                    break;
                case 1:
                    switch (Regular)
                    {
                        case 0:
                            lista.Add(new GeneradorDeNumero(n0, null, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, null, n3, new List<int> {n0, n1, n2, n3}));
                            break;
                        case 1:
                            lista.Add(new GeneradorDeNumero(n0, null, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n2, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n3, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, n3, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(null, n1, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n1, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n1, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, n3, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(null, n0, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n2, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n2, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n2, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(null, n0, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n0, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n1, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, null, n3, new List<int> {n0, n1, n2, n3}));

                            break;
                        case 2:
                            lista.Add(new GeneradorDeNumero(n0, n2, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, n1, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n2, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n3, n1, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n3, null, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, n3, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n2, n3, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n3, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, n3, n2, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(null, n1, n0, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n1, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n1, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n1, n0, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n1, null, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, n3, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, n3, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n1, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n1, n3, null, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(n1, n0, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n0, n2, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, n2, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n0, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n2, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n2, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n3, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n3, n2, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, null, n2, n1, new List<int> {n0, n1, n2, n3}));

                            lista.Add(new GeneradorDeNumero(null, n0, n1, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n0, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, n0, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n0, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n0, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, n0, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n2, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, null, n1, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n2, n1, n3, new List<int> {n0, n1, n2, n3}));
                            break;
                        case 3:
                            lista.Add(new GeneradorDeNumero(n0, n2, n3, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n3, n1, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n1, n0, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n1, n3, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n3, n2, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n0, n2, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n2, n0, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n0, n1, n3, new List<int> {n0, n1, n2, n3}));
                            break;
                    }
                    break;
                case 2:
                    switch (Regular)
                    {
                        case 0:
                            lista.Add(new GeneradorDeNumero(n0, n1, null, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, null, n2, n3, new List<int> {n0, n1, n2, n3}));
                            break;
                        case 1:
                            lista.Add(new GeneradorDeNumero(n0, n1, n3, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n1, null, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n3, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, n2, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n2, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, null, n1, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n1, n2, null, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, n2, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n1, n0, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n1, null, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, null, n2, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(null, n0, n2, n3, new List<int> {n0, n1, n2, n3}));
                            break;
                        case 2:
                            lista.Add(new GeneradorDeNumero(n0, n1, n3, n2, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n3, n2, n1, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n0, n2, n1, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n3, n1, n2, n0, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n2, n1, n0, n3, new List<int> {n0, n1, n2, n3}));
                            lista.Add(new GeneradorDeNumero(n1, n0, n2, n3, new List<int> {n0, n1, n2, n3}));
                            break;
                    }
                    break;
                case 3:
                    lista.Add(new GeneradorDeNumero(null, n1, n2, n3, new List<int> {n0, n1, n2, n3}));
                    lista.Add(new GeneradorDeNumero(n0, null, n2, n3, new List<int> {n0, n1, n2, n3}));
                    lista.Add(new GeneradorDeNumero(n0, n1, null, n3, new List<int> {n0, n1, n2, n3}));
                    lista.Add(new GeneradorDeNumero(n0, n1, n2, null, new List<int> {n0, n1, n2, n3}));
                    break;
                case 4:
                    lista.Add(new GeneradorDeNumero(n0, n1, n2, n3, new List<int> {n0, n1, n2, n3}));
                    break;
            }
            return lista;
        }

        public static void GuardarTodasLasOpciones(string path)
        {
            TextWriter t = new StreamWriter(path);
            t.Write(MostrarTodasLasOpciones());
            t.Close();
        }

        private static string Mostrar(List<GeneradorDeNumero> lista)
        {
            return lista.Aggregate(string.Empty, (current, num) => current + (num + "\n"));
        }

        public static string MostrarTodasLasOpciones()
        {
            var salida = string.Empty;
            List<GeneradorDeNumero> lista;

            for (var bien = 0; bien < 5; bien++)
                for (var regular = 0; regular < 5; regular++)
                    if (bien + regular <= 4 && !(bien == 3 && regular == 1))
                    {
                        var r = new Regla(1, 2, 3, 4, bien, regular);
                        lista = r.Generar();
                        salida += r + "\n" + Mostrar(lista) + "\n";
                    }

            return salida;
        }
        #endregion
    }
}