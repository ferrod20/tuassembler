using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnosLib 
{
    public struct Intervalo 
    {
        public DateTime Fecha { get; set; }
        public TimeSpan Duracion { get; set; }
        public bool EsParteDelIntervalo(DateTime fecha, TimeSpan duracion)
        {
            return (fecha >= this.Fecha && (fecha + duracion) <= (this.Fecha + this.Duracion));
        }
        public override string ToString()
        {
            return Fecha.ToShortDateString() + " " + Fecha.ToShortTimeString() + "-" +
                (Fecha + Duracion).ToShortTimeString();
        }
    }
    public class DisponibilidadSemanal 
    {
        public NumeroDeLaSemana DiaDeLaSemana { get; set; }           //Numero de dia de la semana (0=Domingo, 1=Lunes
        public TimeSpan HoraInicial { get; set; }
        public TimeSpan HoraFinal { get; set; }           
        
        public DisponibilidadSemanal()
        {   
            DiaDeLaSemana = NumeroDeLaSemana.Todos;
            HoraInicial = new TimeSpan(0, 0, 0);  
            HoraFinal = new TimeSpan(24, 0, 0);
        }
        public DisponibilidadSemanal(bool Vacio)
        {
            if(!Vacio){
                DiaDeLaSemana = NumeroDeLaSemana.Todos;
                HoraInicial = new TimeSpan(0, 0, 0); 
                HoraFinal = new TimeSpan(24, 0, 0);
            }
        }
        public DisponibilidadSemanal(
                NumeroDeLaSemana DiaDeLaSemana,
                TimeSpan HoraIncial,
                TimeSpan HoraFinal
                )
        {
            this.DiaDeLaSemana =DiaDeLaSemana;
            this.HoraInicial = HoraIncial;
            this.HoraFinal = HoraFinal;
        }
        public bool Atiende(DateTime fecha, TimeSpan duracion)
        {
            bool disponible = false;
            //Verifica si es el mismo dia de la semana o si esta disponible todos los dias
            if (((int)fecha.DayOfWeek) == ((int)this.DiaDeLaSemana) || ((int)DiaDeLaSemana) == 7)
            {
                TimeSpan HoraPedida = new TimeSpan(fecha.Hour, fecha.Minute, 0);
                disponible = ( HoraPedida >= HoraInicial && (HoraPedida+duracion) <= HoraFinal );
            }
            else
            {
                disponible = false;
            }

            return disponible;
        }
        public override string ToString()
        {
            return DiaDeLaSemana.ToString() + " "
                + HoraInicial.Hours.ToString().PadLeft(2,'0') 
                + ":"
                + HoraInicial.Minutes.ToString().PadLeft(2, '0') 
                + "-" +
                HoraFinal.Hours.ToString().PadLeft(2, '0') 
                + ":"
                + HoraFinal.Minutes.ToString().PadLeft(2, '0');
        }

    
    }
    
}
