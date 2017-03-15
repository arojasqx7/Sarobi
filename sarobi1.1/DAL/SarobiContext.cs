using sarobi1._1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace sarobi1._1.DAL
{
    public class SarobiContext : DbContext
    {
       

        public SarobiContext() : base("SarobiContext")
        {
        }
        
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Base> Bases { get; set; }
        public DbSet<EmpleadoBase1> EmpleadosBases1 { get; set; }
        public DbSet<Tracking> Trackings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Empleado>()
             .HasMany(c => c.Bases).WithMany(i => i.Empleado)
             .Map(t => t.MapLeftKey("EmpleadoID")
                 .MapRightKey("BaseID")
                 .ToTable("Empleado_Base"));
            base.OnModelCreating(modelBuilder);
        }
    }
}