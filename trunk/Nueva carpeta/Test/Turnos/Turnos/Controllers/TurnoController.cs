using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turnos.BO;
using Turnos.DTO;

namespace Turnos.Controllers
{
    public class TurnoController : Controller
    {
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ObtenerRecursos(long id)
        {
            var recursos = new [] {
            new { id = 1, nombre = "Sofia", especialidad = "peluquera", habilitado = true, foto = "", email = "sofia@peluque.com"},
            new { id = 2, nombre = "Carla", especialidad = "manos", habilitado = true, foto = "", email = "carla@peluque.com"},
            new { id = 3, nombre = "Jenifer", especialidad = "pedicura", habilitado = true, foto = "", email = "jeni@peluque.com"},
            new { id = 4, nombre = "Juan Carlos", especialidad = "coiffeur", habilitado = true, foto = "", email = "juanca@peluque.com"}
            };

            return Json(recursos);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GrabarRecurso(long id, string nombre, string especialidad, bool habilitado, string foto, string email)
        {
            return Json(new { id = "5", nombre = "Cecilia", especialidad = "pedicu", habilitado = true, foto = "", email = "ceci@peluque.com" });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EliminarRecurso(long id)
        {
            return Json("");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ReservarTurno(long clienteId, DateTime fechaI, DateTime fechaF, string telefono, string mail)
        {
            return Json("a");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ObtenerTurnosLibres(long clienteId, DateTime fechaI, DateTime fechaF)
        {
            //Marian: Defintivamente hay que hacer una sola llamada que devuelva los recursos con sus turnos disponibles
            //Y en el futuro proximo agregar a esa llamada los turnos tomados
            
            var result = ObtenerTurnosLibres();//Uso esto porque las llamadas a DB4O no me devolvian ningun dato.
            return Json(result);
        }

        private object ObtenerTurnosLibres()
        {
//freebusys: [

            //    { "start": new Date(year, month, day + 1, 08), "end": new Date(year, month, day + 1, 15), "free": true, userId: [0, 1, 2, 3] },
            //    {"start": new Date(year, month, day+0, 08), "end": new Date(year, month, day+0, 18, 00), "free": true, userId: [0,1,2,3]},
            //    {"start": new Date(year, month, day+1, 08), "end": new Date(year, month, day+1, 18, 00), "free": true, userId: [0,3]},
            //    {"start": new Date(year, month, day+2, 14), "end": new Date(year, month, day+2, 18, 00), "free": true, userId: 1}
            //],

            //creo la disponibilidad
            var disponibilidad = new List<disponibilidad>();

            var ahora = DateTime.Now;
            var dispo1 = new disponibilidad(ahora.AddDays(1).AddHours(8), ahora.AddDays(1).AddHours(15), true, new List<int> {0, 1, 2, 3});
            var dispo2 = new disponibilidad(ahora.AddHours(8), ahora.AddHours(18), true, new List<int> {0, 1, 2, 3});
            var dispo3 = new disponibilidad(ahora.AddDays(1).AddHours(8), ahora.AddDays(1).AddHours(18), true, new List<int> {0, 3});
            var dispo4 = new disponibilidad(ahora.AddDays(2).AddHours(14), ahora.AddDays(2).AddHours(18), true, new List<int> {1});

            disponibilidad.Add(dispo1);
            disponibilidad.Add(dispo2);
            disponibilidad.Add(dispo3);
            disponibilidad.Add(dispo4);

            //creo los recursos
            var recursos = new List<string> {"Mimiaaaaa", "Ulises", "Colorista", "Pepe Soriano", "Juan Domingo"};

            //Devuelvo un objeto con 2 propiedades: recursos y disponibilidad
            return new {recursos, disponibilidad};
        }
    }
}
