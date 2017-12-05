<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteProductos.aspx.cs" Inherits="BIOGRAPHY_OF_PETSFAP.Reportes.ReporteProductos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BIOGRAPHY_OF_PETSFAP.Models.Producto> producto = null;
                using (BIOGRAPHY_OF_PETSFAP.Models.VeterinariaEntities db = new BIOGRAPHY_OF_PETSFAP.Models.VeterinariaEntities())
                {
                    producto = db.Producto.OrderBy(a => a.Nombre).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/ReportProductos.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("VeterinariaDataSet",producto);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                
            }   
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" SizeToReportContent="true"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
