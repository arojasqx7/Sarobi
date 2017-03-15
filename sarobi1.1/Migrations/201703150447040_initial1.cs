namespace sarobi1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Base",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Empleado",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PrimerNombre = c.String(),
                        PrimerApellido = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tracking",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Horas = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        ID_Base = c.Int(nullable: false),
                        ID_Empleado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Base", t => t.ID_Base, cascadeDelete: true)
                .ForeignKey("dbo.Empleado", t => t.ID_Empleado, cascadeDelete: true)
                .Index(t => t.ID_Base)
                .Index(t => t.ID_Empleado);
            
            CreateTable(
                "dbo.Empleado_Base",
                c => new
                    {
                        EmpleadoID = c.Int(nullable: false),
                        BaseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmpleadoID, t.BaseID })
                .ForeignKey("dbo.Empleado", t => t.EmpleadoID, cascadeDelete: true)
                .ForeignKey("dbo.Base", t => t.BaseID, cascadeDelete: true)
                .Index(t => t.EmpleadoID)
                .Index(t => t.BaseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracking", "ID_Empleado", "dbo.Empleado");
            DropForeignKey("dbo.Tracking", "ID_Base", "dbo.Base");
            DropForeignKey("dbo.Empleado_Base", "BaseID", "dbo.Base");
            DropForeignKey("dbo.Empleado_Base", "EmpleadoID", "dbo.Empleado");
            DropIndex("dbo.Empleado_Base", new[] { "BaseID" });
            DropIndex("dbo.Empleado_Base", new[] { "EmpleadoID" });
            DropIndex("dbo.Tracking", new[] { "ID_Empleado" });
            DropIndex("dbo.Tracking", new[] { "ID_Base" });
            DropTable("dbo.Empleado_Base");
            DropTable("dbo.Tracking");
            DropTable("dbo.Empleado");
            DropTable("dbo.Base");
        }
    }
}
