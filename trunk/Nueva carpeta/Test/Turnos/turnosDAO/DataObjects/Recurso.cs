using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnosLib
{
    public class Recurso 
    {
        public long Id { get; set; }                                    //Id del recurso a reservar
        public long IdCliente { get; set; }                           //Id del cliente dueño del recurso
        public string Nombre { get; set; }                             //Nombre del Recurso
        public List<DisponibilidadSemanal> Disponibilidad { get; set; }      //Lista con las disponibilidades del recurso
        public List<Intervalo> DiasNoDisponibles { get; set; }          //Lista de fechas de excepciones en las cuales no esta disponible
        public List<Intervalo> DiasDisponibles { get; set; }   //Lista de fechas de execpciones en las cuales SI esta disponible (complementaria a Disponibilidad)
        public bool Activo { get; set; }
        public Recurso()
        {
            Id = 0;
            IdCliente = 0;
            Nombre = "";
            Disponibilidad = new List<DisponibilidadSemanal>();
            DiasNoDisponibles = new List<Intervalo>();
            DiasDisponibles = new List<Intervalo>();
            Activo = true;
        }
        public Recurso(bool Vacio)
        {
            if(!Vacio)
            {
                Id = 0;
                IdCliente = 0;
                Nombre = "";
                Disponibilidad = new List<DisponibilidadSemanal>();
                DiasNoDisponibles = new List<Intervalo>();
                DiasDisponibles = new List<Intervalo>();
                Activo = true;
            }
        }
        public Recurso(long Id)
        {
            this.Id = Id;
            this.dbId = Id;
        }
        public Recurso(long Id, long IDCliente, string Nombre,
            List<DisponibilidadSemanal> lDisponibilidad,
            List<Intervalo> lDiasNoDisponible,
            List<Intervalo> lDiasDisponible,
            bool Activo)
        {
            this.Id = Id;
            this.IdCliente = IdCliente;
            this.Nombre = Nombre;
            this.Disponibilidad = lDisponibilidad;
            this.DiasNoDisponibles = lDiasNoDisponible;
            this.DiasDisponibles = lDiasDisponible;
            this.Activo = Activo;
        }
        public bool TieneTurnoDisponible(DateTime fecha, TimeSpan duracion)
        {
            bool disponible = true;
            //Me fijo disponibilidad en los dias de la semana que tiene cargado
            disponible = (from dis in this.Disponibilidad
                              where dis.Atiende(fecha,duracion)
                              select dis).Count() > 0;
            
            //Busco en la lista de los intervalos que NO esta disponible
            //me fijo que NO este en esa lista
            disponible = disponible && (from dis in this.DiasNoDisponibles
                                                where dis.EsParteDelIntervalo(fecha,duracion)
                                                select dis).Count() == 0;
            //Me fijo si esta en la lista de dias adicionales SI disponibles 
            disponible = disponible || (from dis in this.DiasDisponibles
                                                where dis.EsParteDelIntervalo(fecha,duracion)
                                                select dis).Count() > 0;

            return disponible;
        }
    }
}
