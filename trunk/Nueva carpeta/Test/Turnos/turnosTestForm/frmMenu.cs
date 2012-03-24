using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace turnosTestForm
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new frmClientes()).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             (new frmRecursos()).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             (new frmUsuarios()).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (new frmTurnos()).ShowDialog();
        }
    }
}
