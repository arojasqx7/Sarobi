namespace sarobi1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial52 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Empleado", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Empleado", "Contrasena", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Empleado", "Contrasena", c => c.String(maxLength: 100));
            AlterColumn("dbo.Empleado", "Username", c => c.String());
        }
    }
}
