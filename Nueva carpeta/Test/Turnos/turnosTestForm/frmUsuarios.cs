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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            long? id= null;
            if (tiId.Text != "") id = long.Parse(tiId.Text);

            Usuario o = Usuarios.Guardar(id, 
                tiNombre.Text,
                tiApellido.Text,
                tiMail.Text,
                tiPassword.Text,
                tiCelular.Text);
            btnListar_Click(null, null);
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            long id = 0;
            if (tiId.Text != "") id = long.Parse(tiId.Text);
            
            Usuario o = Usuarios.Leer(id);
            tiNombre.Text = o.Nombre;
            tiApellido.Text = o.Apellido;
            tiPassword.Text = o.Password;
            tiMail.Text = o.Mail;
            tiCelular.Text = o.Celular;
            dgDatos.DataSource = null;

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            dgDatos.DataSource = Usuarios.LeerTodos();
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            tiId.Text = "";
            tiNombre.Text = "";
            tiApellido.Text = "";
            tiMail.Text = "";
            tiCelular.Text = "";
            tiPassword.Text = "";
            dgDatos.DataSource = Usuarios.LeerTodos();
        }



        private void dgDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgDatos.Rows[e.RowIndex].Cells[1].Value.ToString();
            tiId.Text = dgDatos.Rows[e.RowIndex].Cells[0].Value.ToString();
            tiNombre.Text = dgDatos.Rows[e.RowIndex].Cells[1].Value.ToString();
            tiApellido.Text = dgDatos.Rows[e.RowIndex].Cells[2].Value.ToString();
            tiMail.Text = dgDatos.Rows[e.RowIndex].Cells[3].Value.ToString();
            tiPassword.Text = dgDatos.Rows[e.RowIndex].Cells[4].Value.ToString();
            tiCelular.Text = dgDatos.Rows[e.RowIndex].Cells[5].Value.ToString();
            
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            long id = long.Parse(tiId.Text);
            Usuarios.Borrar(id);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            long id = 0;
            if (tiId.Text != "") id = long.Parse(tiId.Text);
            dgDatos.DataSource = Usuarios.Buscar(id,
                                                tiNombre.Text,
                                                tiApellido.Text,
                                                tiMail.Text,
                                                tiPassword.Text,
                                                tiCelular.Text);
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            dgDatos.DataSource = Usuarios.LeerTodos();
        }
    }
}
