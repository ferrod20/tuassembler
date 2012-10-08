using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Turnos.Controllers
{
    public class TurnoController : Controller
    {
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ObtenerRecursos(long idCliente)
        {
            //var recursos = new [] {
            //new { id = 1, nombre = "Sofia", especialidad = "peluquera", habilitado = true, foto = "", email = "sofia@peluque.com"},
            //new { id = 2, nombre = "Carla", especialidad = "manos", habilitado = true, foto = "", email = "carla@peluque.com"},
            //new { id = 3, nombre = "Jenifer", especialidad = "pedicura", habilitado = true, foto = "", email = "jeni@peluque.com"},
            //new { id = 4, nombre = "Juan Carlos", especialidad = "coiffeur", habilitado = true, foto = "", email = "juanca@peluque.com"}
            //};

            var recursos = AdministradorDeRecursos.ObtenerRecursos();
            return Json(recursos);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ObtenerRecurso(long idCliente, long idRecurso)
        {
            var recurso = AdministradorDeRecursos.ObtenerRecurso(idRecurso);
            return Json(recurso);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GrabarRecurso(long? id, Recurso recurso)
        {
            if(id.HasValue)
                AdministradorDeRecursos.ModificarRecurso(recurso);
            else
                AdministradorDeRecursos.AgregarRecurso(recurso);
            
            return Json(recurso);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EliminarRecurso(long idCliente, long idRecurso)
        {
            AdministradorDeRecursos.EliminarRecurso(idRecurso);
            return Json("");
        }             
    }

    public static class AdministradorDeRecursos
    {
        private static List<Recurso> recursos;

        public static List<Recurso>  ObtenerRecursos()
        {
            return recursos;
        }

        public static void AgregarRecurso(Recurso recurso)
        {
            recursos.Add(recurso);
        }

        public static Recurso ObtenerRecurso(long id)
        {
            return recursos.FirstOrDefault(r => r.id == id);
        }

        public static void ModificarRecurso(Recurso nuevoValor)
        {
            var recurso = ObtenerRecurso(nuevoValor.id);
            recurso = nuevoValor;
        }

        public static void Inicializar()
        {
            if(recursos == null)
                recursos = new List<Recurso>
                {
                    new Recurso("Sofia", "peluquera", true, "","sofia@peluque.com"),
                    new Recurso("Carla", "manos", true, "","carla@peluque.com"),
                    new Recurso("Jenifer", "pedicura", true, "","jeni@peluque.com"),
                    new Recurso("Juan Carlos", "coiffeur", true, "","juanca@peluque.com"),
                };
        }

        public static void EliminarRecurso(long idRecurso)
        {
            recursos.RemoveAll(r => r.id == idRecurso);
        }
    }

    public class Recurso
    {
        public Recurso()
        {
            disponibilidad = new Disponibilidad();
            excepciones = new List<Excepcion>();
        }

        public Recurso(string nombre, string especialidad, bool habilitado, string foto, string email): this()
        {
            this.nombre = nombre;
            this.especialidad = especialidad;
            this.habilitado = habilitado;
            this.foto = foto;
            this.email = email;
        }

        public long id { get; set; }
        public string nombre { get; set; }
        public string especialidad { get; set; }
        public bool habilitado { get; set; }
        public string foto { get; set; }
        public string email { get; set; }

        public Disponibilidad disponibilidad{ get; set; }
        public List<Excepcion> excepciones { get; set; }
    }

    public class Excepcion
    {
        public DateTime fecha { get; set; }
        public TimeSpan duracion { get; set; }
        public bool todoElDia { get; set; }
    }

    public class Disponibilidad
    {
        private Dictionary<DayOfWeek, List<TimeSpan>> disponibilidad;

        public void AgregarDisponibilidad(DayOfWeek dia, TimeSpan intervalo)
        {
            if(disponibilidad.ContainsKey(dia))
                disponibilidad[dia].Add(intervalo);
            else
                disponibilidad[dia] = new List<TimeSpan>{intervalo};
        }

        public List<TimeSpan> ObtenerDisponibilidad(DayOfWeek dia)
        {
            return disponibilidad[dia];            
        }
    }
}
