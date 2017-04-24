using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sarobi1._1.DAL;
using sarobi1._1.Models;
using PagedList;
using Microsoft.AspNet.Identity;

namespace sarobi1._1.Controllers
{
    public class TrackingsController : Controller
    {
        private SarobiContext db = new SarobiContext();

        // GET: Trackings

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, string Filtros)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                Filtros = currentFilter;
            }

            ViewBag.CurrentFilter = Filtros;

            var tracks = from s in db.Trackings
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                tracks = tracks.Where(s => s.Base.Nombre.Equals(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //var track = (from t in db.Trackings select t).OrderBy(t => t.Base.Nombre).ThenBy(t => t.Fecha);
            //return View(track.ToPagedList(pageNumber, pageSize));
            return View(tracks.OrderBy(t => t.Base.Nombre).ThenBy(t => t.Fecha).ToPagedList(pageNumber, pageSize));
        }

        public JsonResult GetBases()
        {
            var user = User.Identity.GetUserName();
            if (user == "andrey.rojas.quiros@gmail.com")
            {
                var basesFilter1 = from i in db.Bases
                                   orderby (i.Nombre)
                                   select new { i.ID, i.Nombre };
                return Json(basesFilter1.ToList(), JsonRequestBehavior.AllowGet);
            }

            else {    
            var IdSup = db.Empleados.Where(i => i.Username == user).Select(i => i.ID).First();
            var basesFilter2 = db.Bases.Where(s => s.ID_Supervisor == IdSup).Select(i => new { i.ID, i.Nombre }).OrderBy(i=>i.Nombre);
            return Json(basesFilter2.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEmpleados(int id_base)
        {
            var empleados = from a in db.EmpleadosBases1 where a.BaseID == id_base orderby (a.Empleado.PrimerNombre) select new { a.Empleado.ID, FullName2= a.Empleado.PrimerNombre + " " + a.Empleado.PrimerApellido + " " + a.Empleado.SegundoApellido};
            return Json(empleados.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Trackings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            return View(tracking);
        }

        // GET: Trackings/Create
        public ActionResult Create()
        {


            ViewBag.ID_Base= new SelectList(db.Bases, "Id", "Nombre");
            ViewBag.ID_Empleado = new SelectList(db.Empleados, "Id", "PrimerNombre");
            return View();
        }

        // POST: Trackings/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Horas,Fecha,ID_Base,ID_Empleado")] Tracking tracking)
        {
            if (ModelState.IsValid)
            {
                db.Trackings.Add(tracking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Base = new SelectList(db.Bases, "Id", "Nombre", tracking.ID_Base);
            ViewBag.ID_Empleado = new SelectList(db.Empleados, "Id", "PrimerNombre", tracking.ID_Empleado);
            return View(tracking);
        }

        // GET: Trackings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            return View(tracking);
        }

        // POST: Trackings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Horas,Fecha,ID_Base,ID_Empleado")] Tracking tracking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tracking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tracking);
        }

        // GET: Trackings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            return View(tracking);
        }

        // POST: Trackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tracking tracking = db.Trackings.Find(id);
            db.Trackings.Remove(tracking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
