using System;
using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;

namespace WindowsFormsApplication1
{
    public class Test
    {
        #region Métodos
        public static void TestJuego3B_2B()
        {
            var j = new Juego();
            j.AgregarRegla(5, 8, 1, 0, 0, 1);
            j.AgregarRegla(2, 5, 3, 4, 0, 3);
            j.AgregarRegla(6, 5, 2, 3, 1, 2);
            j.AgregarRegla(6, 3, 5, 4, 1, 1);
            j.AgregarRegla(7, 2, 5, 3, 1, 3);


        }

        public static void TestNumero()
        {
            var n12_7 = new NumeroGenerado(1, 2, null, 7);
            var n__8_ = new NumeroGenerado(null, null, 8, null);
            var n1__3 = new NumeroGenerado(1, null, null, 3);
            var n8__1 = new NumeroGenerado(8, null, null, 1);

            var todoBien = n12_7.EsUnificableCon(n__8_);
            todoBien = !n12_7.EsUnificableCon(n1__3);
            todoBien = n__8_.EsUnificableCon(n1__3);
            todoBien = n1__3.EsUnificableCon(n__8_);
            todoBien = !n8__1.EsUnificableCon(n__8_);

            todoBien = n12_7.UnificarCon(n__8_).ToString() == "1287";
            todoBien = n__8_.UnificarCon(n1__3).ToString() == "1_83";
            todoBien = n1__3.UnificarCon(n__8_).ToString() == "1_83";
        }
        public static void TestNumero2()
        {
            var a = new NumeroGenerado(null, 3, 2, 5);
            var b = new NumeroGenerado(null, 3, 2, 5);

            var iguales = a.Equals(b);
            var unif = a.EsUnificableCon(b);
        }
        #endregion
    }
}