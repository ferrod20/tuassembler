using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnos.DAO;
using Turnos.DTO;

namespace Turnos.BO
{
    static public class Recursos
    {
        #region Metodos primitivos
        static public Recurso Leer(long Id)
        {
            return RecursosDAO.Leer(Id);
        }
        static public Recurso Guardar(long? Id,
            long IDCliente,
            string Nombre,
            List<DisponibilidadSemanal> Disponibilidad,
            List<Intervalo> NoDisponible,
            List<Intervalo> DiasDisponible,
            bool Activo)
        {
            Recurso nuevo = new Recurso();
            if (Id.HasValue)
            {
                nuevo.Id = Id.Value;
                nuevo.dbId = Id.Value;
            }
            nuevo.IdCliente = IDCliente;
            nuevo.Nombre = Nombre;
            nuevo.Disponibilidad = Disponibilidad;
            nuevo.DiasNoDisponibles = NoDisponible;
            nuevo.DiasDisponibles = DiasDisponible;
            nuevo.Activo = Activo;
            return Guardar(nuevo);
        }
        static public Recurso Guardar(Recurso nuevo)
        {
            //Recurso nuevo = new Recurso();
            if (nuevo.Id != 0)
            {
                nuevo.dbId = nuevo.Id;
            }
            return RecursosDAO.Guardar(nuevo);
        }
        static public bool Borrar(long Id)
        {

            return RecursosDAO.Borrar(Id);
        }
        static public List<Recurso> LeerTodos()
        {
            return RecursosDAO.LeerTodos();
        }
        static public List<Recurso> Buscar( long IdCliente,  bool Activo)
        {
            Recurso oBuscar = new Recurso(true);
            oBuscar.Id = 0;
            if (IdCliente != 0) oBuscar.IdCliente = IdCliente;
            return RecursosDAO.Buscar(oBuscar);
        }
        #endregion
        #region Metodos de Busqueda de espacios libres
        public static List<TurnoLibre> ObtenerDisponibilidad(long clienteId, DateTime dI, DateTime dF, int minutosDuracionTurno)
        {
            List<TurnoLibre> lResultado = new List<TurnoLibre>();
            DateTime diaBase;

            TimeSpan ticksIniciales = new TimeSpan(dI.Ticks);
            TimeSpan ticksFinales = new TimeSpan(dF.Ticks);
            TimeSpan duracionTurno = new TimeSpan(0, minutosDuracionTurno, 0);


            //Traigo los recursos activos para ese cliente
            List<Recurso> lRecursos = Recursos.Buscar(clienteId, true);
            if (lRecursos.Count == 0) return lResultado;
            //Recorro todos los recursos encontrados
            foreach (Recurso oR in lRecursos)
            {
                bool libre = true;
                //Dia base sobre el que construyo los turnos disponibles del dia
                diaBase = new DateTime(dI.Year, dI.Month, dI.Day, dI.Hour, dI.Minute, 0);

                for (TimeSpan horario = ticksIniciales;
                             horario < ticksFinales; horario += duracionTurno)
                {
                    libre = oR.TieneTurnoDisponible(diaBase, duracionTurno);
                    lResultado.Add(new TurnoLibre(oR.Id, oR.Nombre, diaBase, libre));
                    diaBase += duracionTurno;
                }
            }
            return lResultado;
        }
        #endregion
    }
}
