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
                "dbo.EmpleadoBase",
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
            DropForeignKey("dbo.EmpleadoBase", "BaseID", "dbo.Base");
            DropForeignKey("dbo.EmpleadoBase", "EmpleadoID", "dbo.Empleado");
            DropIndex("dbo.EmpleadoBase", new[] { "BaseID" });
            DropIndex("dbo.EmpleadoBase", new[] { "EmpleadoID" });
            DropTable("dbo.EmpleadoBase");
            DropTable("dbo.Empleado");
            DropTable("dbo.Base");
        }
    }
}
