namespace sarobi1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial41BaseData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Base", "ID_Supervisor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Base", "ID_Supervisor");
        }
    }
}
