<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppReportViewerEmpleados.aspx.cs" Inherits="sarobi1._1.Reports.AppReportViewerEmpleados" %>

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
                empleados = dc.Empleados.Where(p=>p.Puesto.Equals("Oficial")).OrderBy(a => a.PrimerNombre).ToList();
                ReportViewerEmpleados.LocalReport.ReportPath = Server.MapPath("~/Reports/Oficiales.rdlc");
                ReportViewerEmpleados.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSetEmpleados", empleados);
                ReportViewerEmpleados.LocalReport.DataSources.Add(rdc);
                ReportViewerEmpleados.LocalReport.Refresh();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:800px; margin:0 auto;">

        <br />
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewerEmpleados" runat="server" AsyncRendering="false" SizeToReportContent="true"></rsweb:ReportViewer>
    
        <a href="../Reports/Index">Regresar</a>
    </div>
    </form>
</body>
</html>
