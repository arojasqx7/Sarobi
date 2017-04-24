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
using PagedList;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace sarobi1._1.Controllers
{
    public class BasesController : Controller
    {
        private SarobiContext db = new SarobiContext();
        private ApplicationUserManager _userManager;

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            GetSupervisorName();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var user = User.Identity.GetUserName();

            if (user == "andrey.rojas.quiros@gmail.com")
            {
                var baseee = from b in db.Bases
                             select b;


                if (!String.IsNullOrEmpty(searchString))
                {
                    baseee = baseee.Where(s => s.Nombre.Contains(searchString)
                                            );
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        baseee = baseee.OrderByDescending(s => s.Nombre);
                        break;
                    default:
                        baseee = baseee.OrderBy(s => s.Nombre);
                        break;
                }

                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(baseee.ToPagedList(pageNumber, pageSize));
            }


            else
            {
                var IdSup = db.Empleados.Where(i => i.Username == user).Select(i => i.ID).First();
                var basesFilter = db.Bases.Where(s => s.ID_Supervisor == IdSup);
            
            if (!String.IsNullOrEmpty(searchString))
            {
                basesFilter = basesFilter.Where(s => s.Nombre.Contains(searchString)
                                        );
            }
            switch (sortOrder)
            {
                case "name_desc":
                    basesFilter = basesFilter.OrderByDescending(s => s.Nombre);
                    break;
                default:
                    basesFilter = basesFilter.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(basesFilter.ToPagedList(pageNumber, pageSize));
        }
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
            var basee = new Base();
            basee.Empleado = new List<Empleado>();

            PopulateAssignedEmpleadoData(basee);
            ViewBag.ID_Supervisor = new SelectList(db.Empleados.Where(s => s.Puesto.Equals("Supervisor")).OrderBy(s=>s.PrimerNombre), "Id", "FullName");
            return View();
        }

        // POST: Bases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Encargado,Telefono,Direccion,ID_Supervisor")] Base basee, string[] selectedEmpleados)
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
            ViewBag.ID_Supervisor = new SelectList(db.Empleados.Where(s => s.Puesto.Equals("Supervisor")).OrderBy(s => s.PrimerNombre), "Id", "FullName");
            return View(basee);
        }

        // GET: Bases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Base basee = db.Bases
            .Include(i => i.Empleado)
            .Where(i => i.ID == id)
            .Single();

            PopulateAssignedEmpleadoData(basee);

            if (basee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Supervisor = new SelectList(db.Empleados.Where(s => s.Puesto.Equals("Supervisor")).OrderBy(s=>s.PrimerNombre), "Id", "FullName");
            return View(basee);
        }

        private void PopulateAssignedEmpleadoData(Base basee)
        {
            var allEmpleados = db.Empleados.Where(c => c.Puesto.Equals("Oficial")).OrderBy(c=>c.PrimerNombre);
            var BaseEmpleado = new HashSet<int>(basee.Empleado.Select(c => c.ID));
            var viewModel = new List<AssignedEmpleadoData>();
            foreach (var emp in allEmpleados)
            {
                viewModel.Add(new AssignedEmpleadoData
                {
                    EmpleadoID = emp.ID,
                    Nombre = emp.FullName,
                    Assigned = BaseEmpleado.Contains(emp.ID)
                });
            }
            ViewBag.Empleados = viewModel;
        }

        // POST: Bases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedEmpleados, string[] selectedTracking)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var BaseToUpdate = db.Bases
               .Include(i => i.Empleado)
               .Where(i => i.ID == id)
               .Single();


            if (TryUpdateModel(BaseToUpdate,"",
               new string[] { "Nombre, Encargado, Telefono, Direccion, ID_Supervisor" }))
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
            ViewBag.ID_Supervisor = new SelectList(db.Empleados.Where(s => s.Puesto.Equals("Supervisor")), "Id", "FullName");
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

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //private string GetSupervisorName()
        //{
        //    var user = User.Identity.GetUserName();
        //    var supName = db.Bases.Where(i=>i)
        //}

        public ActionResult GetSupervisorName()
        {
            var sups = db.Bases.Include(p => p.Supervisor);
            return View(sups);
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
