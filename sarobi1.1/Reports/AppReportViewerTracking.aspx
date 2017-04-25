<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppReportViewerTracking.aspx.cs" Inherits="sarobi1._1.Reports.AppReportViewerTracking" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes</title>
         <script runat="server">
             void Page_Load(object sender,EventArgs e){
                 using(sarobi1._1.DAL.SarobiContext dc = new sarobi1._1.DAL.SarobiContext())
                 {
                     var track = from i in dc.Trackings
                                 orderby i.Base.Nombre,i.Fecha
                                 select new{i.Base.Nombre,i.Empleado.PrimerNombre,i.Empleado.PrimerApellido,i.Empleado.SegundoApellido,i.Fecha,i.Horas};

                     ReportViewerTracking.LocalReport.ReportPath = Server.MapPath("~/Reports/Tracking.rdlc");
                     ReportViewerTracking.LocalReport.DataSources.Clear();
                     ReportDataSource rdc1 = new ReportDataSource("DataSet1", track.ToList());
                     ReportDataSource rdc2 = new ReportDataSource("DataSet2", track.ToList());
                     ReportDataSource rdc3 = new ReportDataSource("DataSetTracking", track.ToList());
                     ReportViewerTracking.LocalReport.DataSources.Add(rdc1);
                     ReportViewerTracking.LocalReport.DataSources.Add(rdc2);
                     ReportViewerTracking.LocalReport.DataSources.Add(rdc3);
                     ReportViewerTracking.LocalReport.Refresh();
                 }
             }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:800px; margin:0 auto;">

        <br />
      <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewerTracking" runat="server" AsyncRendering="false" SizeToReportContent="true"></rsweb:ReportViewer>
        <a href="../Reports/Index">Regresar</a>
    </div>
    </form>
</body>
</html>
