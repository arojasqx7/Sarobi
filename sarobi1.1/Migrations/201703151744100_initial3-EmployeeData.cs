namespace sarobi1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3EmployeeData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empleado", "SegundoApellido", c => c.String(maxLength: 20));
            AddColumn("dbo.Empleado", "FechaNacimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Empleado", "Sexo", c => c.String(nullable: false));
            AddColumn("dbo.Empleado", "TipoIdentificacion", c => c.String(nullable: false));
            AddColumn("dbo.Empleado", "NumeroIdentificacion", c => c.String(nullable: false));
            AddColumn("dbo.Empleado", "Nacionalidad", c => c.String());
            AddColumn("dbo.Empleado", "Telefono1", c => c.String(nullable: false));
            AddColumn("dbo.Empleado", "Telefono2", c => c.String());
            AddColumn("dbo.Empleado", "Direccion", c => c.String(nullable: false));
            AddColumn("dbo.Empleado", "Puesto", c => c.String(nullable: false));
            AddColumn("dbo.Empleado", "TipoEmpleado", c => c.String());
            AddColumn("dbo.Empleado", "FechaContratacion", c => c.DateTime(nullable: false));
            AddColumn("dbo.Empleado", "Recomendaciones", c => c.String());
            AddColumn("dbo.Empleado", "Foto", c => c.String());
            AddColumn("dbo.Empleado", "AntecedentesPenales", c => c.String());
            AlterColumn("dbo.Empleado", "PrimerNombre", c => c.String(maxLength: 20));
            AlterColumn("dbo.Empleado", "PrimerApellido", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Empleado", "PrimerApellido", c => c.String());
            AlterColumn("dbo.Empleado", "PrimerNombre", c => c.String());
            DropColumn("dbo.Empleado", "AntecedentesPenales");
            DropColumn("dbo.Empleado", "Foto");
            DropColumn("dbo.Empleado", "Recomendaciones");
            DropColumn("dbo.Empleado", "FechaContratacion");
            DropColumn("dbo.Empleado", "TipoEmpleado");
            DropColumn("dbo.Empleado", "Puesto");
            DropColumn("dbo.Empleado", "Direccion");
            DropColumn("dbo.Empleado", "Telefono2");
            DropColumn("dbo.Empleado", "Telefono1");
            DropColumn("dbo.Empleado", "Nacionalidad");
            DropColumn("dbo.Empleado", "NumeroIdentificacion");
            DropColumn("dbo.Empleado", "TipoIdentificacion");
            DropColumn("dbo.Empleado", "Sexo");
            DropColumn("dbo.Empleado", "FechaNacimiento");
            DropColumn("dbo.Empleado", "SegundoApellido");
        }
    }
}
