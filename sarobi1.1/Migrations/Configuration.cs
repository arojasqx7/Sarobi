namespace sarobi1._1.Migrations
{
    using DAL;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<sarobi1._1.DAL.SarobiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "sarobi1._1.DAL.SarobiContext";
        }

        protected override void Seed(sarobi1._1.DAL.SarobiContext context)
        {
        //    var empleados = new List<Empleado>
        //    {
        //    new Empleado
        //    { PrimerNombre = "Carson",PrimerApellido="Alexander" }
        //    };

        //    empleados.ForEach(s => context.Empleados.AddOrUpdate(p => p.PrimerApellido, s));
        //    context.SaveChanges();

        //    var bases = new List<Base>
        //    {
        //    new Base
        //    { Nombre = "Sapiens" }
        //    };

        //    bases.ForEach(s => context.Bases.AddOrUpdate(p => p.Nombre, s));
        //    context.SaveChanges();


        //      }


        //void AddOrUpdateBase(SarobiContext context, string empleadoNombre, string baseNombre)
        //{
        //    var emp = context.Empleados.SingleOrDefault(c => c.PrimerNombre == empleadoNombre);
        //    var bas = emp.Bases.SingleOrDefault(i => i.Nombre == baseNombre);
        //    if (bas == null)
        //        emp.Bases.Add(context.Bases.Single(i => i.Nombre == baseNombre));

        //}
    }
}
}