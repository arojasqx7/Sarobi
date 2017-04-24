namespace sarobi1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empleado", "Username", c => c.String());
            AddColumn("dbo.Empleado", "Contrasena", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empleado", "Contrasena");
            DropColumn("dbo.Empleado", "Username");
        }
    }
}
