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
    public partial class frmRecursos : Form
    {
        protected Recurso recursoSeleccionado = new Recurso();
        public frmRecursos()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmRecursos_Load(object sender, EventArgs e)
        {
            cmbClientes.ValueMember = "Id";
            cmbClientes.DisplayMember = "Nombre";
            cmbClientes.DataSource = Clientes.LeerTodos();
            cmbClientes.SelectedIndex = -1;

            cmbClienteBuscarTurno.ValueMember = "Id";
            cmbClienteBuscarTurno.DisplayMember = "Nombre";
            cmbClienteBuscarTurno.DataSource = Clientes.LeerTodos();
            cmbClienteBuscarTurno.SelectedIndex = -1;
            btnListar_Click(null, null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            long id = 0;
            if (tiId.Text != "") id = long.Parse(tiId.Text);
            Cliente clienteDelRecurso = (Cliente)cmbClientes.SelectedItem;
            List<DisponibilidadSemanal> lDis = new List<DisponibilidadSemanal>();
            List<Intervalo> lDiasAdicionales = new List<Intervalo>();
            List<Intervalo> lDiasNoDisponible = new List<Intervalo>();

            Recurso recurso = new Recurso(id);
            recurso.IdCliente = clienteDelRecurso.Id;
            recurso.Nombre = tiNombre.Text;
            recurso.Activo = true;

            //Completos los dias disponibles
            foreach (object oDA in lstDisponibilidades.Items)
            {
                lDis.Add((DisponibilidadSemanal)oDA);
            }
            //Completo los dias disponibles adicionales
            foreach (object oDA in lstDiasAdicionales.Items)
            {
                lDiasAdicionales.Add((Intervalo)oDA);
            }
            //Completo los dias NO disponibles
            foreach (object oDA in lstNoDisponible.Items)
            {
                lDiasNoDisponible.Add((Intervalo)oDA);
            }
            recurso.Disponibilidad = lDis;
            recurso.DiasDisponibles = lDiasAdicionales;
            recurso.DiasNoDisponibles = lDiasNoDisponible;
            Recursos.Guardar(recurso);
            btnListar_Click(null, null);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            dgDatos.DataSource = Recursos.LeerTodos();
        }

        private void btn_AgregarNoDisponible_Click(object sender, EventArgs e)
        {
            int hI = int.Parse(tiNoDisponiblelHI.Text.Split(':')[0]);
            int mI = int.Parse(tiNoDisponiblelHI.Text.Split(':')[1]);
            int hF = int.Parse(tiNoDisponiblelHF.Text.Split(':')[0]);
            int mF = int.Parse(tiNoDisponiblelHF.Text.Split(':')[1]);

            DateTime diaI = new DateTime(dtpDisponible.Value.Year,
                                                    dtpDisponible.Value.Month,
                                                    dtpDisponible.Value.Day,
                                                    hI, mI, 0);
            DateTime diaF = new DateTime(dtpDisponible.Value.Year,
                                                    dtpDisponible.Value.Month,
                                                    dtpDisponible.Value.Day,
                                                    hF, mF, 0);
            TimeSpan duracion = diaF - diaI;
            Intervalo intervalo = new Intervalo { Fecha = diaI, Duracion = duracion };
            recursoSeleccionado.DiasNoDisponibles.Add(intervalo);
            CompletarCampos();
        }

        private void btn_AgregarDiaAdicional_Click(object sender, EventArgs e)
        {
            int hI = int.Parse(tiAdicionalHI.Text.Split(':')[0]);
            int mI = int.Parse(tiAdicionalHI.Text.Split(':')[1]);
            int hF = int.Parse(tiAdicionalHF.Text.Split(':')[0]);
            int mF = int.Parse(tiAdicionalHF.Text.Split(':')[1]);

            DateTime diaI = new DateTime(dtpDisponible.Value.Year,
                                        dtpDisponible.Value.Month,
                                        dtpDisponible.Value.Day,
                                        hI, mI,0);
            DateTime diaF = new DateTime(dtpDisponible.Value.Year,
                                                    dtpDisponible.Value.Month,
                                                    dtpDisponible.Value.Day,
                                                    hF, mF, 0);
            TimeSpan duracion = diaF - diaI;   
            Intervalo intervalo = new Intervalo { Fecha = diaI, Duracion = duracion };
            recursoSeleccionado.DiasDisponibles.Add(intervalo);
            CompletarCampos();
        }

        private void btn_AgregarDisponibilidad_Click(object sender, EventArgs e)
        {
            int hI = int.Parse(tiHoraInicial.Text.Split(':')[0]);
            int mI = int.Parse(tiHoraInicial.Text.Split(':')[1]);
            int hF = int.Parse(tiHoraFinal.Text.Split(':')[0]);
            int mF = int.Parse(tiHoraFinal.Text.Split(':')[1]);
            DisponibilidadSemanal o = new DisponibilidadSemanal(true);
            TimeSpan  horaInicial = new TimeSpan(hI,mI,0);
            TimeSpan horaFinal = new TimeSpan(hF, mF, 0);
            NumeroDeLaSemana dia = (NumeroDeLaSemana)Enum.Parse(typeof(NumeroDeLaSemana), lstDias.SelectedItem.ToString(), true);
            o.DiaDeLaSemana = dia;
            o.HoraInicial = horaInicial;
            o.HoraFinal = horaFinal;

            recursoSeleccionado.Disponibilidad.Add(o);
            CompletarCampos();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            long id = long.Parse(tiId.Text);
            Recursos.Borrar(id);
            btnLimpiarCampos_Click(null, null);
            btnListar_Click(null, null);
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            //lstDias.Items.Clear();
            lstDiasAdicionales.DataSource = null;
            lstDisponibilidades.DataSource = null;
            lstNoDisponible.DataSource = null;
            tiNombre.Text = "";
            tiId.Text = "";
            cmbClientes.SelectedIndex = -1;
            //tiHoraInicial.Text = "";
            //tiHoraFinal.Text= "";
        }

        private void dgDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long idRecurso = long.Parse(dgDatos.Rows[e.RowIndex].Cells[0].Value.ToString());
            recursoSeleccionado = Recursos.Leer(idRecurso);
            CompletarCampos();
        }
        protected void CompletarCampos()
        {
            
            tiId.Text = recursoSeleccionado.Id.ToString();
            tiNombre.Text = recursoSeleccionado.Nombre;

            for (int n = 0; n < cmbClientes.Items.Count; n++)
            {
                if (((Cliente)cmbClientes.Items[n]).Id ==  recursoSeleccionado.IdCliente)
                {
                    cmbClientes.SelectedIndex = n;
                }
            }
            lstDisponibilidades.DataSource = null;
            lstDiasAdicionales.DataSource = null;
            lstNoDisponible.DataSource = null;
            lstDisponibilidades.DataSource = recursoSeleccionado.Disponibilidad;
            lstDiasAdicionales.DataSource = recursoSeleccionado.DiasDisponibles;
            lstNoDisponible.DataSource = recursoSeleccionado.DiasNoDisponibles;
        }
        protected void turnos(DateTime dI, DateTime dF)
        {
            //long idCliente = ((Cliente)cmbClientes.SelectedItem).Id;
            ////NumeroDeLaSemana dia = (NumeroDeLaSemana)Enum.Parse(typeof(NumeroDeLaSemana), lstDias.SelectedItem.ToString(), true);
            //List<TurnoLibre> lResultado = new List<TurnoLibre>();
            //DateTime diaBase ;
            
            //TimeSpan ticksIniciales = new TimeSpan(dI.Ticks);
            //TimeSpan ticksFinales = new TimeSpan(dF.Ticks);
            //TimeSpan duracionTurno = new TimeSpan(0,30,0);


            ////Traigo los recursos activos para ese cliente
            //List<Recurso> lRecursos = Recursos.Buscar(idCliente,true);
            //if (lRecursos.Count == 0) return ;
            ////Recorro todos los recursos encontrados
            //foreach (Recurso oR in lRecursos)
            //{
            //    bool libre = true;
            //    //Dia base sobre el que construyo los turnos disponibles del dia
            //    diaBase = new DateTime(dI.Year, dI.Month, dI.Day, dI.Hour, dI.Minute, 0);

            //    for (TimeSpan horario = ticksIniciales ;
            //                 horario < ticksFinales; horario += duracionTurno)
            //    {
            //        libre = oR.Disponible(diaBase, duracionTurno);  
            //        lResultado.Add(new TurnoLibre(oR.Nombre, diaBase, libre));
            //        diaBase += duracionTurno;
            //    }
            //}
 

        }
        // Busca los turnos libres para la fecha y horas indicadas en el control 
        // y para el cliente seleccionado 
        private void btnBuscarLibres_Click(object sender, EventArgs e)
        {

                int hI = int.Parse(tiHoraITurnoLibre.Text.Split(':')[0]);
                int mI = int.Parse(tiHoraITurnoLibre.Text.Split(':')[1]);
                int hF = int.Parse(tiHoraFTurnoLibre.Text.Split(':')[0]);
                int mF = int.Parse(tiHoraFTurnoLibre.Text.Split(':')[1]);
                int duracion = int.Parse(tiDuracion.Text);

                if (cmbClienteBuscarTurno.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar algun cliente en el combo clientes");
                    return;
                }
                long idCliente = ((Cliente)cmbClienteBuscarTurno.SelectedItem).Id;
            
                //DisponibilidadSemanal o = new DisponibilidadSemanal(true);
                TimeSpan horaInicial = new TimeSpan(hI, mI, 0);
                TimeSpan horaFinal = new TimeSpan(hF, mF, 0);

                DateTime fechaI = new DateTime(dpDiaTurnosLibres.Value.Year ,
                                            dpDiaTurnosLibres.Value.Month,
                                            dpDiaTurnosLibres.Value.Day, hI, mI, 0, 0);
                DateTime fechaF = new DateTime(dpDiaTurnosLibres.Value.Year,
                                            dpDiaTurnosLibres.Value.Month,
                                            dpDiaTurnosLibres.Value.Day, hF, mF, 0, 0);
                lstTurnosLibres.DataSource = null;
                lstTurnosLibres.DataSource = Recursos.ObtenerDisponibilidad(idCliente, fechaI, fechaF, duracion);
                return;
        }
        /*Busca turnos para el cliente, fecha y horas seleccionada y lo regresa en formato json
         Esto lo hace pidiendo los recursos disponibles para ese dia y pivoteando la tabla para tener 
         como clave el horario del turno y una lista de los id's disponibles para ese horario         */
        private void btnCalendarioDisponibilidadTest_Click(object sender, EventArgs e)
        {
            #region Lectura parametros de los campos y obtiene los recursos disponibles en ese horario
                int hI = int.Parse(tiHoraITurnoLibre.Text.Split(':')[0]);
                int mI = int.Parse(tiHoraITurnoLibre.Text.Split(':')[1]);
                int hF = int.Parse(tiHoraFTurnoLibre.Text.Split(':')[0]);
                int mF = int.Parse(tiHoraFTurnoLibre.Text.Split(':')[1]);
                int duracion = int.Parse(tiDuracion.Text);

                if (cmbClienteBuscarTurno.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar algun cliente en el combo clientes");
                    return;
                }
                long idCliente = ((Cliente)cmbClienteBuscarTurno.SelectedItem).Id;
                TimeSpan horaInicial = new TimeSpan(hI, mI, 0);
                TimeSpan horaFinal = new TimeSpan(hF, mF, 0);

                DateTime fechaI = new DateTime(dpDiaTurnosLibres.Value.Year ,
                                            dpDiaTurnosLibres.Value.Month,
                                            dpDiaTurnosLibres.Value.Day, hI, mI, 0, 0);
                DateTime fechaF = new DateTime(dpDiaTurnosLibres.Value.Year,
                                            dpDiaTurnosLibres.Value.Month,
                                            dpDiaTurnosLibres.Value.Day, hF, mF, 0, 0);
                List<TurnoLibre> lTL = Recursos.ObtenerDisponibilidad(idCliente, fechaI, fechaF, 60);
            #endregion

            List<Turnos.BO.disponibilidad> lResultado = new List<disponibilidad>();
            Dictionary<long, int> dicIDSecuenciales = new Dictionary<long, int>();
            int idRecursoCalendario = 0;
            long idRecursoActual ;
            idRecursoActual = lTL[0].Id;
            // Recorro todos los turnos libres que vienen en forma de una lista ordenada de NombreRecurso, Hora y si esta libre o no,
            // la transformo en una lista de horarios con un array de los id's que estan libres en ese horario
            // los id's tienen que ser comenzando con 0, 1, etc.
            foreach (TurnoLibre tl in lTL)
            {
                if(idRecursoActual == tl.Id)
                {
                    //Busco todos los recursos que estan libres para esa fecha/hora
                    var idRecursos = from o in lTL
                                     where o.Fecha == tl.Fecha && o.Libre
                                     select obtenerIDSecuencial(o.Id, ref dicIDSecuenciales);

                    lResultado.Add( new disponibilidad{ 
                        userId= idRecursos.ToList<int>(),
                        start = tl.Fecha,
                        end = tl.Fecha.AddMinutes(duracion), 
                        free = tl.Libre } 
                        );
                }
                else
                {
                    idRecursoActual = tl.Id;
                    idRecursoCalendario++;
                }
            }
            //Transforma el resultado en un string jSon para que lo tome el calendario javascript
            /* { "start": new Date(year, month, day + 1, 08), "end": new Date(year, month, day + 1, 15), "free": true, userId: [0, 1, 2, 3] },
				{"start": new Date(year, month, day+0, 08), "end": new Date(year, month, day+0, 18, 00), "free": true, userId: [0,1,2,3]},
				{"start": new Date(year, month, day+1, 08), "end": new Date(year, month, day+1, 18, 00), "free": true, userId: [0,3]},
				{"start": new Date(year, month, day+2, 14), "end": new Date(year, month, day+2, 18, 00), "free": true, userId: 1}
			],*/
            StringBuilder jResultado = new StringBuilder();
            jResultado.Append("[");
            foreach (disponibilidad oHorario in lResultado)
            {
                string sIds = "";
                oHorario.userId.ForEach( x=> sIds+= x.ToString() + "," );
                sIds = sIds.Remove(sIds.Length - 1, 1);
                // Recordar que el nro de mes en Javascript comienza con 0!
                jResultado.Append("{");
                jResultado.AppendFormat("\"start\":new Date({0},{1},{2},{3},{4})", oHorario.start.Year, oHorario.start.Month-1, oHorario.start.Day, oHorario.start.Hour, oHorario.start.Minute);
                jResultado.AppendFormat(",\"end\":new Date({0},{1},{2},{3},{4})", oHorario.end.Year, oHorario.end.Month-1, oHorario.end.Day, oHorario.end.Hour, oHorario.end.Minute);
                jResultado.Append(",\"free\": true, userId: ["+ sIds +"]");
                jResultado.Append("},");
            }
            jResultado.Remove(jResultado.Length - 1, 1);
            jResultado.Append("];");
            tiJSon.Text = jResultado.ToString();
        }
        //Dado el id de un recurso y un diccionario se fija que numero secuencial le corresponde
        // esto es porque el control calendario precisa una lista de id de recursos secuenciales
        private int obtenerIDSecuencial(long id, ref Dictionary<long, int> dicIDUsados)
        {
            if (dicIDUsados.ContainsKey(id))
            {
                return dicIDUsados[id];
            }
            else
            {
                int nuevoIdSecuencial = dicIDUsados.Count;
                dicIDUsados.Add(id, nuevoIdSecuencial);
                return nuevoIdSecuencial;
            }
        }
    }
}
