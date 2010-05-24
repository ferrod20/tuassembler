using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApplication1
{
    public enum EstadoDelJuego
    {
        GanoLaCompu,
        GanoElJugador,
        Empate,
        Jugando
    }

    public class Juego
    {
        #region Variables de instancia
        public int CantidadDeOpciones;
        public EstadoDelJuego Estado;
        public Numero NumeroAAdivinarPorElJugador;
        public Numero NumeroAAdivinarPorLaCompu;
        public Numero NumeroAdivinadoPorLaCompu;
        private int cantJugadasCompu;
        private int cantJugadasJugador;
        private List<NumeroAdivinado> jugadasDeLaCompu;
        private List<NumeroAdivinado> jugadasDelJugador;
        private List<Regla> reglasDeLaCompu = new List<Regla>();
        #endregion

        #region Constructores
        public Juego()
        {
            Estado = EstadoDelJuego.Jugando;
            jugadasDelJugador = new List<NumeroAdivinado>();
            jugadasDeLaCompu = new List<NumeroAdivinado>();
        }
        #endregion

        #region Propiedades
        public bool NumeroAAdivinarPorLaCompuIngresado
        {
            get
            {
                return NumeroAdivinadoPorLaCompu != null;
            }
        }
        #endregion

        #region Métodos
        public void Adivinar()
        {
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
                    nums = nums.Count == 0 ? new List<NumeroGenerado>(numeros) : Unificar(nums, numeros);
                }

                if (nums.Count > 0)
                {
                    foreach (var r in reglasDeLaCompu)
                        if (nums.Contains(r.ConvertirEnNumeroGenerado))
                            nums.Remove(r.ConvertirEnNumeroGenerado);
                    var num = nums.First();

                    CantidadDeOpciones = CalcularOpciones(nums);

                    if (!num.Completar())
                        NumeroAdivinadoPorLaCompu = null;
                    else
                        NumeroAdivinadoPorLaCompu = new Numero(num);
                }
            }
        }
        public Regla AgregarRegla(int a, int b, int c, int d, int bien, int regular)
        {
            var regla = new Regla(a, b, c, d, bien, regular);
            reglasDeLaCompu.Add(regla);
            return regla;
        }
        public Regla AgregarRegla(Numero n, int bien, int regular)
        {
            var regla = new Regla(n, bien, regular);
            reglasDeLaCompu.Add(regla);
            return regla;
        }
        public Regla AgregarReglaAlNumeroAdivinado(int bien, int regular)
        {
            return AgregarRegla(NumeroAdivinadoPorLaCompu, bien, regular);
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
        public void CalificarElNumeroDelJugador(string numero, out int bien, out int regular)
        {
            NumeroAAdivinarPorElJugador.Calificar(numero, out bien, out regular);
        }
        private void EstablecerEstadoDelJuego()
        {
            if(jugadasDelJugador.Count  > 0 && jugadasDelJugador.Count == jugadasDeLaCompu.Count )
            {
                var jugadorAdivinó = jugadasDelJugador.Last().Bien == 4;
                var compuAdivinó = jugadasDeLaCompu.Last().Bien == 4;

                if (jugadorAdivinó)
                    Estado = compuAdivinó ? EstadoDelJuego.Empate : EstadoDelJuego.GanoElJugador;
                else if (compuAdivinó)
                    Estado = EstadoDelJuego.GanoLaCompu;
            }
                        
        }
        public void GenerarNumeroAAdivinar()
        {
            NumeroAAdivinarPorElJugador = NumeroGenerado.GenerarNumeroAlAzar();
        }
        public NumeroAdivinado JuegaElJugador(string numero, out int bien, out int regular)
        {
            CalificarElNumeroDelJugador(numero, out bien, out regular);
            var n = new NumeroAdivinado(numero, bien, regular);
            jugadasDelJugador.Add(n);
            EstablecerEstadoDelJuego();
            return n;

        }
        public Regla JuegaLaCompu()
        {
            int bien, regular;
            Adivinar();
            NumeroAAdivinarPorLaCompu.Calificar(NumeroAdivinadoPorLaCompu, out bien, out regular);
            jugadasDeLaCompu.Add(new NumeroAdivinado(NumeroAdivinadoPorLaCompu, bien, regular));
            EstablecerEstadoDelJuego();

            return AgregarReglaAlNumeroAdivinado(bien, regular);
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