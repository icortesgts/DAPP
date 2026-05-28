<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReporteStickerBG.aspx.vb" Inherits="DAPP.ReporteStickerBG" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<script src="<%=Page.ResolveUrl("~/Scripts/jquery-1.10.2.min.js") %>" type="text/javascript"></script>
  <%--  <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            if ($.browser.mozilla ) {
                try {
                    var ControlName = 'VisorReporte';
                    var innerScript = '<scr' + 'ipt type="text/javascript">document.getElementById("' + ControlName + '_print").Controller = new ReportViewerHoverButton("' + ControlName + '_print", false, "", "", "", "#ECE9D8", "#DDEEF7", "#99BBE2", "1px #ECE9D8 Solid", "1px #336699 Solid", "1px #336699 Solid");</scr' + 'ipt>';
                    var innerTbody = '<tbody><tr><td><input type="image" style="border-width: 0px; padding: 2px; height:16px; width: 16px;" alt="Print" src="/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=9.0.30729.1&amp;Name=Microsoft.Reporting.WebForms.Icons.Print.gif" title="Print"></td></tr></tbody>';
                    var innerTable = '<table title="Print" onmouseout="this.Controller.OnNormal();" onmouseover="this.Controller.OnHover();" onclick="PrintFunc(\'' + ControlName + '\'); return false;" id="' + ControlName + '_print" style="border: 1px solid rgb(236, 233, 216); background-color: rgb(236, 233, 216); cursor: default;">' + innerScript + innerTbody + '</table>'
                    var outerScript = '<scr' + 'ipt type="text/javascript">document.getElementById("' + ControlName + '_print").Controller.OnNormal();</scr' + 'ipt>';                  
                    var outerDiv = '<div style="display: inline; font-size: 8pt; height: 30px;" class=" "> <table cellspacing="0" cellpadding="0" style="display: inline;"><tbody><tr><td height="28px">' + innerTable + outerScript + '</td></tr></tbody></table></div>';
                    $("#" + ControlName + " > div > div").append(outerDiv);
                }
                catch (e) { alert(e); }
            }
        }); 

        function PrintFunc(ControlName) {
            setTimeout('ReportFrame' + ControlName + '.print();', 100);
        }
    </script>--%>
    <script type="text/javascript">
    $(document).ready(function () {
        $("#BtnImprimir").click(imprimirDiv);    //Asociando la función "imprimirDiv" al clic del botón para Imprimir Reporte
    });
 
    function imprimirDiv()
    {
        var divImprimir = $("div[id$='ReportDiv']").parent();        //Obteniendo el padre del DIV que contiene al reporte
        var estilos = $("head style[id$='ReportControl_styles']");    //Obteniendo los estilos del reporte
        newWin= window.open("");        //Abriendo una nueva ventana
 
        //Construyendo el HTML de la nueva ventana, con los estilos del reporte y el div que contiene el reporte
        newWin.document.write('<html xmlns="http://www.w3.org/1999/xhtml"><head><style type="text/css">' + estilos.html() + '</style></head><body>' + divImprimir.html() + '</body>');
        newWin.document.close();        //Finalizando la escritura
        newWin.print();        //Imprimir contenido de nueva ventana
        newWin.close();        //Cerrar nueva ventana
    }
    </script>

<title></title>     
     
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM_Reportes" runat="server">
        </asp:ScriptManager>
        <asp:ImageButton ID="BtnImprimir" runat="server" OnClientClick="return false;" UseSubmitBehavior="False" ImageUrl="~/Images/icoPrint.gif" />
        <%--<asp:Button ID="BtnImprimir" runat="server" Text="Imprimir" CausesValidation="False" OnClientClick="return false;" UseSubmitBehavior="False" />--%>
        <br />
        <rsweb:ReportViewer id="VisorReporte"  runat="server" Height="400px" Width="800px" ShowFindControls="False" ShowPrintButton="true"  ShowBackButton="False" SizeToReportContent="True" BackColor="#BBD2FD"></rsweb:ReportViewer>        
    </form>
</body>
</html>
