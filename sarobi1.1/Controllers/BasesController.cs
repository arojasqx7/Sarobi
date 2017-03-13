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
using System.Data.Entity.Infrastructure;

namespace sarobi1._1.Controllers
{
    public class BasesController : Controller
    {
        private SarobiContext db = new SarobiContext();

        // GET: Bases
        public ActionResult Index()
        {
            return View(db.Bases.ToList());
        }

        // GET: Bases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Base @base = db.Bases.Find(id);
            if (@base == null)
            {
                return HttpNotFound();
            }
            return View(@base);
        }

        // GET: Bases/Create
        public ActionResult Create()
        {
            var baseee = new Base();
            baseee.Empleado = new List<Empleado>();
            PopulateAssignedEmpleadoData(baseee);
            return View();
        }

        // POST: Bases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre")] Base basee, string[] selectedEmpleados)
        {
            if (selectedEmpleados != null)
            {
                basee.Empleado = new List<Empleado>();
                foreach (var emp in selectedEmpleados)
                {
                    var empToAdd = db.Empleados.Find(int.Parse(emp));
                    basee.Empleado.Add(empToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Bases.Add(basee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedEmpleadoData(basee);
            return View(basee);
        }

        // GET: Bases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Base @base = db.Bases.Find(id);
            Base basee = db.Bases
            .Include(i => i.Empleado)
            .Where(i => i.ID == id)
            .Single();
            PopulateAssignedEmpleadoData(basee);

            if (basee == null)
            {
                return HttpNotFound();
            }
            return View(basee);
        }

        private void PopulateAssignedEmpleadoData(Base basee)
        {
            var allEmpleados = db.Empleados;
            var BaseEmpleado = new HashSet<int>(basee.Empleado.Select(c => c.ID));
            var viewModel = new List<AssignedEmpleadoData>();
            foreach (var emp in allEmpleados)
            {
                viewModel.Add(new AssignedEmpleadoData
                {
                    EmpleadoID = emp.ID,
                    Nombre = emp.PrimerNombre,
                    Assigned = BaseEmpleado.Contains(emp.ID)
                });
            }
            ViewBag.Empleados = viewModel;
        }

        // POST: Bases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedEmpleados)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var BaseToUpdate = db.Bases
               .Include(i => i.Empleado)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(BaseToUpdate, "",
               new string[] { "Nombre" }))
            {
                try
                {
                    UpdateBaseEmpleados(selectedEmpleados, BaseToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedEmpleadoData(BaseToUpdate);
            return View(BaseToUpdate);
        }

        private void UpdateBaseEmpleados(string[] selectedEmpleados, Base BaseToUpdate)
        {
            if (selectedEmpleados == null)
            {
                BaseToUpdate.Empleado = new List<Empleado>();
                return;
            }

            var selectedEmpleadoHS = new HashSet<string>(selectedEmpleados);
            var baseEmpleados = new HashSet<int>
                (BaseToUpdate.Empleado.Select(c => c.ID));

            foreach (var emp in db.Empleados)
            {
                if (selectedEmpleadoHS.Contains(emp.ID.ToString()))
                {
                    if (!baseEmpleados.Contains(emp.ID))
                    {
                        BaseToUpdate.Empleado.Add(emp);
                    }
                }
                else
                {
                    if (baseEmpleados.Contains(emp.ID))
                    {
                        BaseToUpdate.Empleado.Remove(emp);
                    }
                }
            }
        }

        // GET: Bases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Base @base = db.Bases.Find(id);
            if (@base == null)
            {
                return HttpNotFound();
            }
            return View(@base);
        }

        // POST: Bases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Base @base = db.Bases.Find(id);
            db.Bases.Remove(@base);
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
