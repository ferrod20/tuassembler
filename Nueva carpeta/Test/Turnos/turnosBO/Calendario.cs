using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnos.DAO;
using Turnos.DTO;

namespace Turnos.BO
{
    public class disponibilidad
    {
        public disponibilidad()
        {
        }

        public disponibilidad(DateTime start,DateTime end,bool free,List<int> userId)
        {
            this.start = start;
            this.end = end;
            this.free = free;
            this.userId = userId;
        }

        public DateTime start;
        public DateTime end;
        public bool free;
        public List<int> userId;
        public override string ToString()
        {
            string s= this.start.ToLongTimeString() + " " + 
                    this.end.ToLongTimeString() + " " + 
                    this.free+ " (" ;
            foreach (int i in this.userId) { s += i.ToString() + "," ; };
            return s.PadRight(60) ;
        }
    }
    static public class Calendario
    {

        static public List<String> ObtenerRecursosDelCliente(long clienteId)
        {
            var recursos = new List<string>();
            List<Recurso> listaRecursos = Recursos.Buscar(clienteId, true);
            listaRecursos.ForEach(o => recursos.Add(o.Nombre));
            return recursos;
        }
        static public List<Turnos.BO.disponibilidad> ObtenerTurnosDelCliente(long idCliente, DateTime fechaI, DateTime fechaF, int duracion)
        {
            //Agregar validaciones que la fecha I sea menor o igual a la fecha F y asi..
            return obtenerTurnosDelCliente( idCliente,  fechaI,  fechaF,  duracion) ;

        }

        static private List<Turnos.BO.disponibilidad> obtenerTurnosDelCliente(long idCliente, DateTime fechaI, DateTime fechaF, int duracion)
        {
            //Lectura parametros de los campos y obtiene los recursos disponibles en ese horario
            List<TurnoLibre> lTL = Recursos.ObtenerDisponibilidad(idCliente, fechaI, fechaF, duracion);

            //Pivotea la lista de turnos disponibles
            List<Turnos.BO.disponibilidad> lResultado = pivotearDisponibilidad(lTL, fechaI, fechaF, duracion);

            return lResultado;
        }
        //Dada la lista de los recursos y la disponibilidad de cada uno
        //retorna una lissta con cada una de las horas y los recursos que estan disponibles para cada turno
        private static List<Turnos.BO.disponibilidad> pivotearDisponibilidad(List<TurnoLibre> lTL, DateTime fechaI, DateTime fechaF, int duracion)
        {
            //Determino cuantos bloques de horarios tengo
            int cantidadDeBloques = (int)(Math.Round( (fechaF - fechaI).TotalMinutes / duracion));
            
            //Creo un array con todos los bloques que voy a tener  y los lleno con sus horarios
            DateTime fechaDisponibilidad = fechaI;
            List<Turnos.BO.disponibilidad> lResultado = new List<disponibilidad>();

            //Diccionario donde voy a guardar los ID's reales de los recursos y su traduccion al nro de ID secuencial
            Dictionary<long, int> dicIDSecuenciales = new Dictionary<long, int>();

            //Recorro todos los bloques de turnos que tengo y voy generando la disponibilidad
            for (int i = 0; i < cantidadDeBloques; i++)
            {
                //Busco todos los recursos que estan libres para esa fecha/hora
                var idRecursos = from o in lTL
                                 where o.Fecha == fechaDisponibilidad && o.Libre
                                 select obtenerIDSecuencial(o.Id, ref dicIDSecuenciales);

                if (idRecursos.Count() > 0)
                {
                    disponibilidad dis = new disponibilidad();
                    dis.start = fechaDisponibilidad;
                    dis.end = fechaDisponibilidad.AddMinutes(duracion);
                    dis.free = true;
                    dis.userId = idRecursos.ToList<int>();
                    lResultado.Add(dis);
                    fechaDisponibilidad = fechaDisponibilidad.AddMinutes(duracion);
                }
            }

            return lResultado;
        }
        //Dado el id de un recurso y un diccionario se fija que numero secuencial le corresponde
        // esto es porque el control calendario precisa una lista de id de recursos secuenciales
        static private int obtenerIDSecuencial(long id, ref Dictionary<long, int> dicIDUsados)
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
