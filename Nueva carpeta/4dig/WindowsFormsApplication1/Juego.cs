using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApplication1
{
    public class Juego
    {
        #region Variables de instancia
        public int CantidadDeOpciones;
        public bool GanoLaCompu;
        public NumeroGenerado NumeroAAdivinarPorElJugador;
        public NumeroGenerado NumeroAdivinadoPorLaCompu;
        private List<Regla> reglasDeLaCompu = new List<Regla>();
        
        #endregion

        #region Métodos
        public void Adivinar()
        {
            var n = new NumeroGenerado(8,0,4,2);
            var n2 = new NumeroGenerado(null, 0, 4, 2);
            NumeroAdivinadoPorLaCompu = null;
            if (reglasDeLaCompu.Count == 0)
            {
                NumeroAdivinadoPorLaCompu = NumeroGenerado.GenerarNumeroAlAzar();
                CantidadDeOpciones = 120960; //10.9.8.7   4.3.2
            }

            else
            {
                var nums = new List<NumeroGenerado>();
                foreach (var regla in reglasDeLaCompu)
                {
                    var numeros = regla.Generar();
                    var f = numeros.Contains(n);
                    f = numeros.Contains(n2);
                    nums = nums.Count == 0 ? new List<NumeroGenerado>(numeros) : Unificar(nums, numeros);
                    f = nums.Contains(n);
                }

                if (nums.Count > 0)
                {
                    foreach (var r in reglasDeLaCompu)
                        if (nums.Contains(r.Numero))
                            nums.Remove(r.Numero);
                    NumeroAdivinadoPorLaCompu = nums.First();
                    CantidadDeOpciones = CalcularOpciones(nums);

                    if (!NumeroAdivinadoPorLaCompu.Completar())
                        NumeroAdivinadoPorLaCompu = null;
                }
            }
        }
        public Regla AgregarRegla(int a, int b, int c, int d, int bien, int regular)
        {
            var regla = new Regla(a, b, c, d, bien, regular);
            reglasDeLaCompu.Add(regla);
            return regla;
        }
        public Regla AgregarReglaAlNumeroAdivinado(int bien, int regular)
        {
            return AgregarRegla(NumeroAdivinadoPorLaCompu[0].Value, NumeroAdivinadoPorLaCompu[1].Value, NumeroAdivinadoPorLaCompu[2].Value, NumeroAdivinadoPorLaCompu[3].Value, bien, regular);
        }
        private int CalcularOpciones(List<NumeroGenerado> numerosGenerados)
        {
            var cantDeOpciones = 0;
            foreach (var num in numerosGenerados)
            {
                var posibles = 10 - num.DigitosExcluidos.Count;
                switch (num.CantidadDeNulls)
                {
                    case 0:
                        cantDeOpciones += 1;
                        break;
                    case 1:
                        cantDeOpciones += posibles;
                        break;
                    case 2:
                        cantDeOpciones += posibles*(posibles - 1)*2;
                        break;
                    case 3:
                        cantDeOpciones += posibles*(posibles - 1)*(posibles - 2)*6;
                        break;
                    case 4:
                        cantDeOpciones += posibles*(posibles - 1)*(posibles - 2)*(posibles - 3)*48;
                        break;
                }
            }
            return cantDeOpciones;
        }
        public void Calificar(string numero, out int bien, out int regular)
        {
            NumeroAAdivinarPorElJugador.Calificar(numero, out bien, out regular);
        }
        public void GenerarNumeroAAdivinar()
        {
            NumeroAAdivinarPorElJugador = NumeroGenerado.GenerarNumeroAlAzar();
        }
        private List<NumeroGenerado> Unificar(List<NumeroGenerado> nums, List<NumeroGenerado> numeros)
        {
            var numsUnificados = new List<NumeroGenerado>();
            foreach (var numeroGenerado in numeros)
                foreach (var num in nums)
                    if (num.EsUnificableCon(numeroGenerado))
                    {
                        var numGenerado = num.UnificarCon(numeroGenerado);
                        if (!numsUnificados.Contains(numGenerado))
                            numsUnificados.Add(numGenerado);
                    }
            return numsUnificados;
        }
        #endregion
    }
}