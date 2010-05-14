using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	/// <summary>
	/// Metodos auxiliares
	/// </summary>
	public class MA
	{
		/// <summary>
		/// Establece la propiedad Enabled de cada uno de los controles al valor de habilitar.
		/// </summary>
		public static void HabilitarControles(bool habilitar, params Control[] controles)
		{
			foreach (var control in controles)
				control.Enabled = habilitar;
		}

		/// <summary>
		/// Establece la propiedad Visible de cada uno de los controles al valor de mostrar.
		/// </summary>
		public static void MostrarControles(bool mostrar, params Control[] controles)
		{
			foreach (var control in controles)
				control.Visible = mostrar;
		}

		/// <summary>
		/// Establece la propiedad Text de cada uno de los controles a string.Empty
		/// </summary>
		public static void LimpiarTextBoxs(params Control[] controles)
		{
			foreach (var control in controles)
				control.Text = string.Empty;
		}
	}
}
