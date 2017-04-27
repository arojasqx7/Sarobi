using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sarobi1._1.ViewModels;
using sarobi1._1.DAL;


namespace sarobi1._1.Controllers
{
    public class HomeController : Controller
    {
        private SarobiContext db = new SarobiContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }

        public JsonResult GetCountOficiales()
        {
            //string query = "select count(*) AS OficialesCount from Empleado where Puesto='Oficial'";
            //IEnumerable<DashboardStatistics> data = db.Database.SqlQuery<DashboardStatistics>(query);

            IQueryable<DashboardOficial> data1 = from o in db.Empleados
                                                   where o.Puesto.Equals("Oficial")
                                                   group o by o.Puesto into OfiGroup
                                                   select new DashboardOficial()
                                                   {
                                                       OficialesCount = OfiGroup.Count()
                                                   };

            return Json(data1.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountSupervisores()
        {
            IQueryable<DashboardSupervisor> data2 = from o in db.Empleados
                                                   where o.Puesto.Equals("Supervisor")
                                                   group o by o.Puesto into SupGroup
                                                   select new DashboardSupervisor()
                                                   {
                                                       SupervisoresCount = SupGroup.Count()
                                                   };

            return Json(data2.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountBases()
        {
            IQueryable<DashboardBase> data3 =  from b in db.Bases
                                               join e in db.Empleados on b.ID_Supervisor equals e.ID
                                               where b.ID_Supervisor == e.ID
                                               group b by e.Puesto into BaseGroup
                                               select new DashboardBase()
                                               {
                                                 BasesCount = BaseGroup.Count()
                                               };
            return Json(data3.ToList(), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetBasesActivas()
        //{
        //    IQueryable<DashboardBasesActivas> basesActivasVar = from b in db.Bases
        //                                                        group b by b.Nombre into ListBases
        //                                                          select new DashboardBasesActivas()
        //                                                          {
        //                                                              BasesActivas = ListBases.ToList()
        //                                                          };
        //    return Json(basesActivasVar, JsonRequestBehavior.AllowGet);
        //}

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
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