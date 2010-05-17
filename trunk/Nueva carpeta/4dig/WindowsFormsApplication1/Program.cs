using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    internal static class Program
    {
        #region Métodos
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        #endregion
    }
}