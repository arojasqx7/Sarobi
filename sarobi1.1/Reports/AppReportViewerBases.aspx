<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppReportViewerBases.aspx.cs" Inherits="sarobi1._1.Reports.AppReportViewerBases" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Bases</title>
     <script runat="server">
         void Page_Load(object sender,EventArgs e){
           //  List<sarobi1._1.Models.Base> bases = null;
             using(sarobi1._1.DAL.SarobiContext dc = new sarobi1._1.DAL.SarobiContext())
             {
                 // bases = dc.Bases.Select(s=>s.Nombre).ToList();
               var bases = (from b in dc.Bases
                         join s in dc.Empleados on b.ID_Supervisor equals s.ID
                         where b.ID_Supervisor == s.ID
                         orderby(b.Nombre)
                         select new {b.Nombre,b.Encargado,b.Telefono,b.Direccion,s.PrimerNombre,s.PrimerApellido,s.SegundoApellido}).ToList();
                 ReportViewerBases.LocalReport.ReportPath = Server.MapPath("~/Reports/Bases.rdlc");
                 ReportViewerBases.LocalReport.DataSources.Clear();
                 ReportDataSource rdc = new ReportDataSource("DataSetBaseSupervisor", bases.ToList());
                 ReportDataSource rdc2 = new ReportDataSource("DataSet1", bases.ToList());
                 ReportViewerBases.LocalReport.DataSources.Add(rdc);
                 ReportViewerBases.LocalReport.DataSources.Add(rdc2);
                 ReportViewerBases.LocalReport.Refresh();
             }
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:1000px; margin:0 auto;">

        <br />
      <asp:ScriptManager ID="ScriptManager3" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewerBases" runat="server" AsyncRendering="false" SizeToReportContent="true"></rsweb:ReportViewer>
    
        <a href="../Reports/Index">Regresar</a>
    </div>
    </form>
</body>
</html>
