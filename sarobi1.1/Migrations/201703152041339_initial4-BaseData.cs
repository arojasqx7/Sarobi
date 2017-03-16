namespace sarobi1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4BaseData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Base", "Encargado", c => c.String(nullable: false));
            AddColumn("dbo.Base", "Telefono", c => c.String());
            AddColumn("dbo.Base", "Direccion", c => c.String(nullable: false));
            AddColumn("dbo.Base", "Supervisor_ID", c => c.Int());
            AlterColumn("dbo.Base", "Nombre", c => c.String(nullable: false));
            CreateIndex("dbo.Base", "Supervisor_ID");
            AddForeignKey("dbo.Base", "Supervisor_ID", "dbo.Empleado", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Base", "Supervisor_ID", "dbo.Empleado");
            DropIndex("dbo.Base", new[] { "Supervisor_ID" });
            AlterColumn("dbo.Base", "Nombre", c => c.String());
            DropColumn("dbo.Base", "Supervisor_ID");
            DropColumn("dbo.Base", "Direccion");
            DropColumn("dbo.Base", "Telefono");
            DropColumn("dbo.Base", "Encargado");
        }
    }
}
