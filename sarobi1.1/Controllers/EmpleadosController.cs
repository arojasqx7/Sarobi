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

namespace sarobi1._1.Controllers
{
    public class EmpleadosController : Controller
    {
        private SarobiContext db = new SarobiContext();

        // GET: Empleados
        [Authorize]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PuestoSortParm = sortOrder == "Puesto" ? "puesto_desc" : "Puesto";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var empleados = from s in db.Empleados
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                empleados = empleados.Where(s => s.PrimerNombre.Contains(searchString)
                                       || s.PrimerApellido.Contains(searchString)
                                       || s.SegundoApellido.Contains(searchString)
                                       || s.Puesto.Contains(searchString)
                                       || s.NumeroIdentificacion.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    empleados = empleados.OrderByDescending(s => s.PrimerNombre);
                    break;
                case "puesto_desc":
                    empleados = empleados.OrderByDescending(s => s.Puesto);
                    break;
                default:
                    empleados = empleados.OrderBy(s => s.PrimerNombre);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(empleados.ToPagedList(pageNumber, pageSize));
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PrimerNombre,PrimerApellido,SegundoApellido,FechaNacimiento,Sexo,TipoIdentificacion,NumeroIdentificacion,Nacionalidad,Telefono1,Telefono2,Direccion,Puesto,TipoEmpleado,FechaContratacion,Recomendaciones,Foto,AntecedentesPenales")] Empleado empleado, HttpPostedFileBase file, HttpPostedFileBase file2)
        {
            if (ModelState.IsValid)
            {
                if (file != null || file2!=null)
                {
                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Fotos/" + ImageName);
                    string DocName = System.IO.Path.GetFileName(file2.FileName);
                    string physicalPath2 = Server.MapPath("~/AntecedentesPenales/" + DocName);


                    // save image in folder
                    file.SaveAs(physicalPath);
                    file2.SaveAs(physicalPath2);

                    empleado.Foto = ImageName;
                    empleado.AntecedentesPenales = DocName;
                    db.Empleados.Add(empleado);
                    db.SaveChanges();                 
                }
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, PrimerNombre, PrimerApellido, SegundoApellido, FechaNacimiento, Sexo, TipoIdentificacion, NumeroIdentificacion, Nacionalidad, Telefono1, Telefono2, Direccion, Puesto, TipoEmpleado, FechaContratacion, Recomendaciones")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
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
