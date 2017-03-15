namespace sarobi1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmpleadoBase1",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpleadoID = c.Int(nullable: false),
                        BaseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Base", t => t.BaseID, cascadeDelete: true)
                .ForeignKey("dbo.Empleado", t => t.EmpleadoID, cascadeDelete: true)
                .Index(t => t.EmpleadoID)
                .Index(t => t.BaseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmpleadoBase1", "EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.EmpleadoBase1", "BaseID", "dbo.Base");
            DropIndex("dbo.EmpleadoBase1", new[] { "BaseID" });
            DropIndex("dbo.EmpleadoBase1", new[] { "EmpleadoID" });
            DropTable("dbo.EmpleadoBase1");
        }
    }
}
