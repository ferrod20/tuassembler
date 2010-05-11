using System;
using System.Collections.Generic;

namespace _4dig
{
	public class Regla
	{
		#region Variables de instancia
		private int bien;
		private int regular;
		private Numero numero;
		#endregion

		public List<NumeroGenerado> Generar()
		{
			var lista = new List<NumeroGenerado>();
			switch(bien)
			{
				case 0:					
					break;					
				case 1:
					lista.Add(new NumeroGenerado(numero[0], null, null, null));
					lista.Add(new NumeroGenerado(null, numero[1], null, null));
					lista.Add(new NumeroGenerado(null, null, numero[2], null));
					lista.Add(new NumeroGenerado(null, null, null, numero[3]));
					break;					
				case 2:
					switch(regular)
					{
						case 0:
							lista.Add(new NumeroGenerado(numero[0], numero[1], null, null));
							lista.Add(new NumeroGenerado(numero[0], null, numero[2], null));
							lista.Add(new NumeroGenerado(numero[0], null, null, numero[3]));
							lista.Add(new NumeroGenerado(null, numero[1], numero[2], null));
							lista.Add(new NumeroGenerado(null, numero[1], null, numero[3]));
							lista.Add(new NumeroGenerado(null, null, numero[2], numero[3]));
							break;
						case 1:
							lista.Add(new NumeroGenerado(numero[0], numero[1], numero[3], null));
							lista.Add(new NumeroGenerado(numero[0], numero[1], null, numero[2]));
							lista.Add(new NumeroGenerado(numero[0], numero[3], numero[2], null));
							lista.Add(new NumeroGenerado(numero[0], null, numero[2], numero[1]));
							lista.Add(new NumeroGenerado(numero[0], numero[2], null, numero[3]));
							lista.Add(new NumeroGenerado(numero[0], null, numero[1], numero[3]));
							lista.Add(new NumeroGenerado(numero[3], numero[1], numero[2], null));
							lista.Add(new NumeroGenerado(null, numero[1], numero[2], numero[1]));
							lista.Add(new NumeroGenerado(null, numero[1], numero[0], numero[3]));
							lista.Add(new NumeroGenerado(numero[2], numero[1], null, numero[3]));
							lista.Add(new NumeroGenerado(numero[1], null, numero[2], numero[3]));
							lista.Add(new NumeroGenerado(null, numero[0], numero[2], numero[3]));
							break;
						case 2:
							lista.Add(new NumeroGenerado(numero[0], numero[1], numero[3], numero[2]));
							lista.Add(new NumeroGenerado(numero[0], numero[3], numero[2], numero[1]));
							lista.Add(new NumeroGenerado(numero[0], numero[2], numero[1], numero[3]));
							lista.Add(new NumeroGenerado(numero[3], numero[1], numero[2], numero[0]));
							lista.Add(new NumeroGenerado(numero[2], numero[1], numero[0], numero[3]));
							lista.Add(new NumeroGenerado(numero[1], numero[0], numero[2], numero[3]));
							break;

					}
					break;
				case 3:
					lista.Add(new NumeroGenerado(null, numero[1], numero[2], numero[3]));
					lista.Add(new NumeroGenerado(numero[0], null, numero[2], numero[3]));
					lista.Add(new NumeroGenerado(numero[0], numero[1], null, numero[3]));
					lista.Add(new NumeroGenerado(numero[0], numero[1], numero[2], null));
					break;
				case 4:
					 lista.Add(new NumeroGenerado( numero ));
					break;
					
			}
			return lista;
			//for(var i=0; i<4-bien;i++)
			//{
			//    for(var j=i+1; j<4-bien-1;j++)
			//    {
					
			//    }
			//    var num = new Numero();
			//    num[i] = numero[i];
			//}

			
		}

		public override string ToString()
		{
			return numero + " " +bien + " BIEN " + regular + " REGULAR" ;
		}
	}

	public class NumeroGenerado
	{
		public Numero Numero;
		public List<int> DigitosExcluidos;
		public NumeroGenerado(Numero numero)
		{
			Numero = numero;
		}
		public NumeroGenerado(int? a, int? b, int? c, int? d)
		{
				Numero = new Numero(a,b,c,d);
		}
	}

	public class Numero
	{
		#region Variables de instancia
		private int?[] num = new int?[4];
		#endregion

		#region Métodos
		public bool EsUnificable(Numero numero)
		{
			var esUnificable = true;
			for (var i = 0; i < 4; i++)
				esUnificable &= numero.num[i] == null || num[i] == null;
			return esUnificable;
		}
		public Numero()
		{
		}
		public Numero(int? a, int? b, int? c, int? d)
		{
			num[0] = a;
			num[1] = b;
			num[2] = c;
			num[3] = d;
		}
		public Numero Unificar(Numero numero)
		{
			var numUnificado = new Numero();
			for (var i = 0; i < 4; i++)
				if (numero.num[i] == null)
					numUnificado.num[i] = num[i];
				else
					numUnificado.num[i] = numero.num[i];


			return numUnificado;
		}
		#endregion

		public override string ToString()
		{
			var numero = string.Empty;
			numero += num[0] == null ? "_" : num[0].ToString();
			numero += num[1] == null ? "_" : num[1].ToString();
			numero += num[2] == null ? "_" : num[2].ToString();
			numero += num[3] == null ? "_" : num[3].ToString();
			
			return numero;
		}
		public int? this[int i]
		{
			get
			{
				return num[i];
			}
		}
	}


	internal class Program
	{
		#region Métodos
		private static void Main(string[] args)
		{
		}
		#endregion
	}
}