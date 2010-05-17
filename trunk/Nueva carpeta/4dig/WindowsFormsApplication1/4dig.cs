﻿using System;
using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;

namespace WindowsFormsApplication1
{
	public class Juego
	{
		#region Variables de instancia
		private List<Regla> reglas = new List<Regla>();
	    public int CantidadDeOpciones;
	    #endregion

		#region Métodos
		public NumeroGenerado Adivinar()
		{
			NumeroGenerado n = null;
			if (reglas.Count == 0)
			{
                n = NumeroGenerado.GenerarNumeroAlAzar();
			    CantidadDeOpciones = 120960; //10.9.8.7   4.3.2
			}
				
			else
			{
				var nums = new List<NumeroGenerado>();
				foreach (var regla in reglas)
				{
					var numeros = regla.Generar();
					nums = nums.Count == 0 ? new List<NumeroGenerado>(numeros) : Unificar(nums, numeros);
				}

				if (nums.Count > 0)
				{
					foreach (var r in reglas)
						if (nums.Contains(r.Numero))
							nums.Remove(r.Numero);
					n = nums.First();
                    CantidadDeOpciones = CalcularOpciones( nums);

					if (!n.Completar())
						n = null;
				}
			}

			return n;
		}
	    private int CalcularOpciones(List<NumeroGenerado> numerosGenerados)
	    {
            var cantDeOpciones= 0;
            foreach(var num in numerosGenerados)
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
                        cantDeOpciones += posibles * (posibles-1) *2;
                        break;
                    case 3:
                        cantDeOpciones += posibles * (posibles - 1) * (posibles - 2) * 6;
                        break;
                    case 4:
                        cantDeOpciones += posibles * (posibles - 1) * (posibles - 2) * (posibles - 3) * 48;
                        break;
                }
            }
	        return cantDeOpciones;
	    }
	    public Regla AgregarRegla(int a, int b, int c, int d, int bien, int regular)
		{
			var regla = new Regla(a, b, c, d, bien, regular);
			reglas.Add(regla);
			return regla;
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

	public class Regla
	{
		#region Variables de instancia
		private int bien;
		private int n0, n1, n2, n3;
		private int regular;
		#endregion

		#region Constructores
		public Regla(int n0, int n1, int n2, int n3, int bien, int regular)
		{
			this.bien = bien;
			this.n0 = n0;
			this.n1 = n1;
			this.n2 = n2;
			this.n3 = n3;
			this.regular = regular;
		}
		#endregion

		#region Propiedades
		public NumeroGenerado Numero
		{
			get
			{
				return new NumeroGenerado(n0, n1, n2, n3);
			}
		}
		#endregion

		#region Métodos
		public List<NumeroGenerado> Generar()
		{
			var lista = new List<NumeroGenerado>();
			switch (bien)
			{
				case 0:
					switch (regular)
					{
						case 0:
							lista = NumeroGenerado.GenerarComplementos(new List<int> { n0, n1, n2, n3 });
							break;
						case 1:
							lista.Add(new NumeroGenerado(null, n0, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n3, null, new List<int> { n0, n1, n2, n3 }));
							break;
						case 2:
							lista.Add(new NumeroGenerado(n1, n0, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n0, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n0, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n0, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n0, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n0, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n0, n3, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n1, null, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n0, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n0, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, n0, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n1, null, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n1, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n3, n0, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n1, n2, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n3, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, n3, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n2, null, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n1, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, n1, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n2, null, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n3, n1, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n2, n3, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, n3, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n3, n2, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, n3, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n3, null, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n3, n2, new List<int> { n0, n1, n2, n3 }));
							break;
						case 3:
							///0 1 y 2                                                                      
							lista.Add(new NumeroGenerado(null, n0, n1, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n0, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n0, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n0, null, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n1, n2, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, n0, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, n0, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, n0, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(null, n2, n1, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, n1, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n2, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n1, null, n0, new List<int> { n0, n1, n2, n3 }));

							///0 2 y 3                                                                      
							lista.Add(new NumeroGenerado(null, n0, n3, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n0, n3, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n0, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n0, null, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n3, n2, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n3, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, n0, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, n0, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(null, n2, n3, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, n3, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n3, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n2, null, n0, new List<int> { n0, n1, n2, n3 }));

							///1 2 y 3                                                                      
							lista.Add(new NumeroGenerado(n1, null, n3, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n2, n3, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n3, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n3, null, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n3, n2, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n3, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, n1, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, n1, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(null, n2, n3, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, n3, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n3, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n2, null, n1, new List<int> { n0, n1, n2, n3 }));
							break;
						case 4:
							lista.Add(new NumeroGenerado(n1, n2, n3, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n0, n3, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n3, n0, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n2, n3, n1, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n2, n1, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n0, n1, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n2, n0, n3, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n3, n0, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n2, n0, n1, new List<int> { n0, n1, n2, n3 }));
							break;
					}
					break;
				case 1:
					switch (regular)
					{
						case 0:
							lista.Add(new NumeroGenerado(n0, null, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, null, n3, new List<int> { n0, n1, n2, n3 }));
							break;
						case 1:
							lista.Add(new NumeroGenerado(n0, null, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n2, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n3, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, n3, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(null, n1, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n1, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n1, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, n3, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(null, n0, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n2, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n2, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, n2, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(null, n0, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n0, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n1, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, null, n3, new List<int> { n0, n1, n2, n3 }));

							break;
						case 2:
							lista.Add(new NumeroGenerado(n0, n2, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, n1, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n2, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n3, n1, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n3, null, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, n3, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n2, n3, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n3, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, n3, n2, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(null, n1, n0, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n1, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n1, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n1, n0, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n1, null, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, n3, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, n3, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n1, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n1, n3, null, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(n1, n0, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n0, n2, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, n2, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n0, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, n2, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, n2, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n3, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n3, n2, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, null, n2, n1, new List<int> { n0, n1, n2, n3 }));

							lista.Add(new NumeroGenerado(null, n0, n1, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n0, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, n0, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n0, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, n0, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, n0, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n2, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, null, n1, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n2, n1, n3, new List<int> { n0, n1, n2, n3 }));
							break;
						case 3:
							lista.Add(new NumeroGenerado(n0, n2, n3, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n3, n1, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n1, n1, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n1, n3, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n3, n2, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n0, n2, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n2, n0, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n0, n1, n3, new List<int> { n0, n1, n2, n3 }));
							break;
					}
					break;
				case 2:
					switch (regular)
					{
						case 0:
							lista.Add(new NumeroGenerado(n0, n1, null, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, null, n2, n3, new List<int> { n0, n1, n2, n3 }));
							break;
						case 1:
							lista.Add(new NumeroGenerado(n0, n1, n3, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n1, null, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n3, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, n2, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n2, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, null, n1, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n1, n2, null, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, n2, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n1, n0, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n1, null, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, null, n2, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(null, n0, n2, n3, new List<int> { n0, n1, n2, n3 }));
							break;
						case 2:
							lista.Add(new NumeroGenerado(n0, n1, n3, n2, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n3, n2, n1, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n0, n2, n1, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n3, n1, n2, n0, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n2, n1, n0, n3, new List<int> { n0, n1, n2, n3 }));
							lista.Add(new NumeroGenerado(n1, n0, n2, n3, new List<int> { n0, n1, n2, n3 }));
							break;
					}
					break;
				case 3:
					lista.Add(new NumeroGenerado(null, n1, n2, n3, new List<int> { n0, n1, n2, n3 }));
					lista.Add(new NumeroGenerado(n0, null, n2, n3, new List<int> { n0, n1, n2, n3 }));
					lista.Add(new NumeroGenerado(n0, n1, null, n3, new List<int> { n0, n1, n2, n3 }));
					lista.Add(new NumeroGenerado(n0, n1, n2, null, new List<int> { n0, n1, n2, n3 }));
					break;
				case 4:
					lista.Add(new NumeroGenerado(n0, n1, n2, n3, new List<int> { n0, n1, n2, n3 }));
					break;
			}
			return lista;
			//for(var i=0; i<4-bien;i++)
			//{
			//    for(var j=i+1; j<4-bien-1;j++)
			//    {

			//    }
			//    var num = new Numero();
			//    num[i] = n[i];
			//}
		}
		public override string ToString()
		{
			return n0 + n1.ToString() + n2 + n3 + " " + bien + "B " + regular + "R";
		}
		#endregion
	}

	public class NumeroGenerado
	{
		#region Variables de instancia
		public List<int> DigitosExcluidos = new List<int>();
		public Dictionary<int, double> Probabilidades = new Dictionary<int, double>(); //Indica la suma de probabilidades de q un digito pertenezca al numero	
		private int?[] num = new int?[4];
		#endregion

		#region Constructores
		public NumeroGenerado()
		{
		}
		public NumeroGenerado(int? a, int? b, int? c, int? d, List<int> excluidos)
			: this(a, b, c, d)
		{
			if (excluidos != null)
				DigitosExcluidos.AddRange(excluidos);
		}
		public NumeroGenerado(int? a, int? b, int? c, int? d)
		{
			num[0] = a;
			num[1] = b;
			num[2] = c;
			num[3] = d;

			if (a != null && !DigitosExcluidos.Contains(a.Value))
				DigitosExcluidos.Add(a.Value);
			if (b != null && !DigitosExcluidos.Contains(b.Value))
				DigitosExcluidos.Add(b.Value);
			if (c != null && !DigitosExcluidos.Contains(c.Value))
				DigitosExcluidos.Add(c.Value);
			if (d != null && !DigitosExcluidos.Contains(d.Value))
				DigitosExcluidos.Add(d.Value);
		}
		#endregion

		#region Propiedades
		public int? this[int i]
		{
			get
			{
				return num[i];
			}
		}

		protected int CuantosFaltanCompletar
		{
			get
			{
				var cuantos = 0;
				for (var i = 0; i < 4; i++)
					if (num[i] == null)
						cuantos++;
				return cuantos;
			}
		}

	    public int CantidadDeNulls
	    {
	        get
	        {
                return num.Count(n=>n==null);
	        }	        
	    }
	    #endregion

		#region Métodos
		public bool Completar()
		{
			IList<int> digitos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			var posiblesValores = digitos.Except(DigitosExcluidos).ToList();

			var todoBien = posiblesValores.Count >= CuantosFaltanCompletar;

			if (todoBien)
				for (var i = 0; i < 4; i++)
					if (num[i] == null)
					{
						num[i] = posiblesValores.First();
						posiblesValores.Remove(posiblesValores.First());
					}
			return todoBien;
		}

		public bool Equals(NumeroGenerado other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return other.num[0] == num[0] && other.num[1] == num[1] && other.num[2] == num[2] && other.num[3] == num[3];
		}
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != typeof(NumeroGenerado))
				return false;
			return Equals((NumeroGenerado)obj);
		}
		public bool EsUnificableCon(NumeroGenerado numero)
		{
			var esUnificable = true;
			for (var i = 0; i < 4; i++)
			{
				var n1 = numero.num[i];
				var n2 = num[i];
				var esUnif = true;

				if (n1.HasValue)
					if (n2.HasValue)
						esUnif = n1.Value == n2.Value;
					else
						esUnif = !DigitosExcluidos.Contains(n1.Value) && !EstaElDigito(n1.Value);
				else if (n2.HasValue)
					esUnif = !numero.DigitosExcluidos.Contains(n2.Value) && !numero.EstaElDigito(n2.Value);

				esUnificable &= esUnif;
			}
			return esUnificable;
		}
		private bool EstaElDigito(int? digito)
		{
			var esta = false;

			if (digito != null)
				for (var i = 0; i < 4 && !esta; i++)
					esta = digito == num[i];

			return esta;
		}
		public static NumeroGenerado GenerarNumeroAlAzar()
		{
			IList<int> digitos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			var r = new Random((int)DateTime.Now.Ticks);

			var indice = r.Next(0, 9);
			var a = digitos[indice];
			digitos.Remove(a);

			indice = r.Next(0, 8);
			var b = digitos[indice];
			digitos.Remove(b);

			indice = r.Next(0, 7);
			var c = digitos[indice];
			digitos.Remove(c);

			indice = r.Next(0, 6);
			var d = digitos[indice];
			digitos.Remove(d);

			return new NumeroGenerado(a, b, c, d);
		}
		public override int GetHashCode()
		{
			return (num != null ? num.GetHashCode() : 0);
		}
		public override string ToString()
		{
			var numero = string.Empty;
			numero += num[0] == null ? "_" : num[0].ToString();
			numero += num[1] == null ? "_" : num[1].ToString();
			numero += num[2] == null ? "_" : num[2].ToString();
			numero += num[3] == null ? "_" : num[3].ToString();

			return numero;
		}
		public NumeroGenerado UnificarCon(NumeroGenerado numero)
		{
			var numUnificado = new NumeroGenerado();
			for (var i = 0; i < 4; i++)
				if (numero.num[i] == null)
					numUnificado.num[i] = num[i];
				else
					numUnificado.num[i] = numero.num[i];

			numUnificado.DigitosExcluidos.AddRange(numero.DigitosExcluidos.Union(DigitosExcluidos));
			return numUnificado;
		}
		#endregion

		public static List<NumeroGenerado> GenerarComplementos(List<int> excluidos)
		{
			IList<int> digitos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			var posibles = digitos.Except(excluidos);

			var variaciones = new Variations<int>(posibles, 4);
			return variaciones.Select(variacion => new NumeroGenerado(variacion[0], variacion[1], variacion[2], variacion[3], excluidos.Union(variacion).ToList())).ToList();
		}

		public void Calificar(string numero, out int bien, out int regular)
		{
			bien =0;
			regular = 0;
			var n0 = int.Parse( numero[0].ToString() );
			var n1 = int.Parse(numero[1].ToString());
			var n2 = int.Parse(numero[2].ToString());
			var n3 = int.Parse(numero[3].ToString());

			if (n0 == num[0])
				bien++;
			else if (Contiene(n0))
				regular++;

			if (n1 == num[1])
				bien++;
			else if (Contiene(n1))
				regular++;

			if (n2 == num[2])
				bien++;
			else if (Contiene(n2))
				regular++;

			if (n3 == num[3])
				bien++;
			else if (Contiene(n3))
				regular++;			
		}
		private bool Contiene(int digito)
		{
			return num.Any(n => n == digito);			
		}
		public static bool EsValido(string numero)
		{
			int num;
			var esValido = !string.IsNullOrEmpty(numero) && numero.Length == 4 && int.TryParse(numero, out num) ;
			if( esValido)
			{
				 var n0 = numero[0];
				 var n1 = numero[1];
				 var n2 = numero[2];
				 var n3 = numero[3];

				esValido &= n0 != n1 && n0 != n2 && n0 != n3 && n1 != n2 && n1 != n3 && n2 != n3;
			}
			

			return esValido;
		}
	}

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
            j.AgregarRegla(7,2,5,3, 1, 3);

  

			var n = j.Adivinar();
		}

        public static void TestNumero2()
        {
            var a = new NumeroGenerado(null, 3, 2, 5);
            var b = new NumeroGenerado(null, 3, 2, 5);

            bool iguales = a.Equals(b);
            bool unif = a.EsUnificableCon(b);
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
		#endregion
	}
}