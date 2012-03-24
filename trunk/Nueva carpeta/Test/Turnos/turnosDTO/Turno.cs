using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turnos.DTO
{
    public class Turno : db4oDataHelper.Domain.DomainObject
    {
        int Id { get; set; }                        //Id del turno
        Usuario Usuario { get; set; }               //Usuario que realiza el turno
        Cliente Cliente { get; set; }               //Cliente al que se le realiza el turno
        DateTime Fecha { get; set; }                //Fecha del turno
        Recurso Recurso { get; set; }               //Recurso que se desea reservar
        //Lista con los valores de los campos de datos adicionales ingresados
        public Turno(){}    
    }
    public class TurnoLibre
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public bool Libre { get; set; }

        public TurnoLibre(long id,string nombre, DateTime fecha, bool libre)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Fecha = fecha;
            this.Libre = libre;
        }
        public override string ToString()
        {
            return (this.Nombre
                + " - "
                +  Fecha.ToShortDateString().PadLeft(10)
                + " "
                + Fecha.ToShortTimeString().PadLeft(10)
                + " "
                + (this.Libre ? "LIBRE    " : "NO DISPONIBLE")) .PadRight(50);
        }
    }
    /// <summary>
    /// Estructura de los numeros de dias de la semana
    /// </summary>
    public enum NumeroDeLaSemana
    { 
        Domingo, 
        Lunes, 
        Martes, 
        Miercoles, 
        Jueves, 
        Viernes,
        Sabado,
        DiasHabiles,
        Todos
    }




    
}
