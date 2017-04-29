<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppReportViewerBaseEmpleado.aspx.cs" Inherits="sarobi1._1.Reports.AppReportViewerBaseEmpleado" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes</title>
         <script runat="server">
             void Page_Load(object sender,EventArgs e){
                 using(sarobi1._1.DAL.SarobiContext dc = new sarobi1._1.DAL.SarobiContext())
                 {
                       var bases = from i in dc.EmpleadosBases1
                               orderby i.Base.Nombre,i.Empleado.PrimerNombre
                                select new{i.Base.Nombre,i.Empleado.PrimerNombre,i.Empleado.PrimerApellido,i.Empleado.SegundoApellido};

                   //  var baseQuery = "SELECT Base.Nombre,Empleado.PrimerNombre,Empleado.PrimerApellido,Empleado.SegundoApellido FROM Empleado_Base JOIN Empleado ON Empleado_Base.EmpleadoID = Empleado.ID JOIN Base ON Empleado_Base.BaseID = Base.ID";


                     ReportViewerBasesXEmpleado.LocalReport.ReportPath = Server.MapPath("~/Reports/BaseEmpleados.rdlc");
                     ReportViewerBasesXEmpleado.LocalReport.DataSources.Clear();
                     ReportDataSource rdc = new ReportDataSource("DataSet1", bases.ToList());
                     ReportDataSource rdc2 = new ReportDataSource("DataSet2", bases.ToList());
                     ReportViewerBasesXEmpleado.LocalReport.DataSources.Add(rdc);
                     ReportViewerBasesXEmpleado.LocalReport.DataSources.Add(rdc2);
                     ReportViewerBasesXEmpleado.LocalReport.Refresh();
                 }
             }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:800px; margin:0 auto;">

        <br />
      <asp:ScriptManager ID="ScriptManager4" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewerBasesXEmpleado" runat="server" AsyncRendering="false" SizeToReportContent="true"></rsweb:ReportViewer>

        <a href="../Reports/Index">Regresar</a>
    </div>
    </form>
</body>
</html>
