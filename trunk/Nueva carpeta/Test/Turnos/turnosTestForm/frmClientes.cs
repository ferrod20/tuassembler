using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Turnos.BO;
using Turnos.DTO;
namespace turnosTestForm
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            long? id = null;
            if (tiId.Text != "") id = long.Parse(tiId.Text);

            Cliente o = Clientes.Guardar(id,
                tiNombre.Text,
                tiDescripcion.Text);
            btnListar_Click(null, null);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            dgDatos.DataSource = Clientes.LeerTodos();
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            tiId.Text = "";
            tiNombre.Text= "";
            tiDescripcion.Text= "";
            btnListar_Click(null, null);
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            long id = 0;
            if (tiId.Text != "") id = long.Parse(tiId.Text);

            Cliente o = Clientes.Leer(id);
            tiNombre.Text = o.Nombre;
            tiDescripcion.Text = o.Descripcion;
            dgDatos.DataSource = null;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            long id = 0;
            if (tiId.Text != "") id = long.Parse(tiId.Text);
            dgDatos.DataSource = Clientes.Buscar(id,
                                                tiNombre.Text,
                                                tiDescripcion.Text);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            long id = long.Parse(tiId.Text);
            Clientes.Borrar(id);
            btnLimpiarCampos_Click(null, null);
            btnListar_Click(null, null);
        }

        private void dgDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgDatos.Rows[e.RowIndex].Cells[1].Value.ToString();
            tiId.Text = dgDatos.Rows[e.RowIndex].Cells[0].Value.ToString();
            tiNombre.Text = dgDatos.Rows[e.RowIndex].Cells[1].Value.ToString();
            tiDescripcion.Text = dgDatos.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            dgDatos.DataSource = Clientes.LeerTodos();
        }
    }
}
