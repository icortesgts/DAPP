<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="DAPP._Default"%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        document.addEventListener("DOMContentLoaded", inicio('~/Images/bg_01.fw.png'), false);

      function inicio(fuente)
      {
       document.getElementById("<%=imginicio.ClientID%>").src = fuente;
      }

    </script>
    <div>        
        <%--<div align="center"><asp:Image ID="ImagePPal" ImageAlign="Middle" runat="server" ImageUrl="~/Images/procesos cng.jpg" /></div> --%>
        <div align="center">
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Image id="imginicio" ImageUrl="~/Images/procesos cng.jpg" runat="server" alt="" usemap="#map1453733293958" />
                    <map id="map1453733293958" name="map1453733293958">
                        <area shape="rect" coords="21,67,263,202" title="GESTION ADMINISTRATIVA FINANCIERA" alt="GESTION ADMINISTRATIVA FINANCIERA" href=<%=ResolveClientUrl("~/Vistas/GesDoc/Page_MisDocumentos?id_area=13")%> target="_self">
                        <area shape="rect" coords="164,214,339,387" title="GESTION ADMINISTRATIVA FINANCIERA" alt="GESTION ADMINISTRATIVA FINANCIERA" href=<%=ResolveClientUrl("~/Vistas/GesDoc/Page_MisDocumentos?id_area=13")%> target="_self">
                        <area shape="rect" coords="318,77,484,243" title="GESTION DIRECTIVA" alt="GESTION DIRECTIVA" href=<%=ResolveClientUrl("~/Vistas/GesDoc/Page_MisDocumentos?id_area=8")%> target="_self">
                        <area shape="rect" coords="537,76,772,198" title="GESTION DIRECTIVA" alt="GESTION DIRECTIVA" href=<%=ResolveClientUrl("~/Vistas/GesDoc/Page_MisDocumentos?id_area=8")%> target="_self">
                        <area shape="rect" coords="469,212,606,382" title="GESTION ACADEMICA" alt="GESTION ACADEMICA" href=<%=ResolveClientUrl("~/Vistas/GesDoc/Page_MisDocumentos?id_area=11")%> target="_self">
                        <area shape="rect" coords="538,399,774,524" title="GESTION ACADEMICA" alt="GESTION ACADEMICA" href=<%=ResolveClientUrl("~/Vistas/GesDoc/Page_MisDocumentos?id_area=11")%> target="_self">
                        <area shape="rect" coords="314,349,485,537" title="GESTION DE LA COMUNIDAD" alt="GESTION DE LA COMUNIDAD" href=<%=ResolveClientUrl("~/Vistas/GesDoc/Page_MisDocumentos?id_area=12")%> target="_self">
                        <area shape="rect" coords="23,398,263,524" title="GESTION DE LA COMUNIDAD" alt="GESTION DE LA COMUNIDAD" href=<%=ResolveClientUrl("~/Vistas/GesDoc/Page_MisDocumentos?id_area=12")%> target="_self">
                    </map>
                    <asp:HiddenField ID="HCliente" runat="server" />
                </ContentTemplate> 
                
            </asp:UpdatePanel>
            
        </div> 
    </div>    

</asp:Content>
