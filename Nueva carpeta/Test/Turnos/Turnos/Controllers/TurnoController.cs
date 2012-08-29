using System;
using System.Web.Mvc;

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

      
    }
}
