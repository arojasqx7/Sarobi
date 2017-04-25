<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppReportViewerSupervisores.aspx.cs" Inherits="sarobi1._1.Reports.AppReportViewerSupervisores" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes</title>
        <script runat="server">
        void Page_Load(object sender,EventArgs e){
            List<sarobi1._1.Models.Empleado> empleados = null;
            using(sarobi1._1.DAL.SarobiContext dc = new sarobi1._1.DAL.SarobiContext())
            {
                empleados = dc.Empleados.Where(p=>p.Puesto.Equals("Supervisor")).OrderBy(a => a.PrimerNombre).ToList();
                ReportViewerSupervisores.LocalReport.ReportPath = Server.MapPath("~/Reports/Supervisores.rdlc");
                ReportViewerSupervisores.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSetEmpleados", empleados);
                ReportViewerSupervisores.LocalReport.DataSources.Add(rdc);
                ReportViewerSupervisores.LocalReport.Refresh();
            }
        }
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <div style="width:800px; margin:0 auto;">

        <br />
      <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewerSupervisores" runat="server" AsyncRendering="false" SizeToReportContent="true"></rsweb:ReportViewer>
    
        <a href="../Reports/Index">Regresar</a>
    </div>
    </form>
</body>
</html>
