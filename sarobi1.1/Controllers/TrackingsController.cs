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

namespace sarobi1._1.Controllers
{
    public class TrackingsController : Controller
    {
        private SarobiContext db = new SarobiContext();

        // GET: Trackings
        public ActionResult Index()
        {
            return View(db.Trackings.ToList());
        }

        public JsonResult GetBases()
        {
            var bases = from b in db.Bases select new { b.ID, b.Nombre };
            return Json(bases.ToList(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetEmpleados(int id_base)
        {

            var empleados = from a in db.EmpleadosBases1 where a.BaseID == id_base select new { a.Empleado.ID,a.Empleado.PrimerNombre};
            return Json(empleados.ToList(), JsonRequestBehavior.AllowGet);
            Console.Write(empleados.Count());
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
