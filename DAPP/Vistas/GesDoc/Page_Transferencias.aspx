<%@ Page Title="Transferencia de Documentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Transferencias.aspx.vb" Inherits="DAPP.Page_Transferencias" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">TRANSFERENCIA DE DOCUMENTOS </span>
    </div>
    <br />
    <br />
    
    <div class="DivFormulario">
        <b class="control-label">
            <asp:Label ID="Label4" runat="server" Text="Fecha Documento: *" Width="180px"  />
        </b>
        <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="input-xlarge" Width="180px" textmode="Date" /> 
    </div>
    <div class="DivFormulario">
        <asp:Button ID="btOk" runat="server" Text="Transferir Archivos" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" OnClientClick="return confirm('Esta Seguro de Transferir los Documentos?');" /> 
    </div>
    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ORIGEN</span>
    </div>
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">           
            <tr>
                <td style="text-align: Left; width: 461px;">
                   <ig:WebDataTree ID="DocTree" runat="server" Height="350px" Width="440px" BorderColor="#333333" BorderStyle="Dotted" BorderWidth="1px" ToolTip="Carpetas" Font-Names="Arial" ForeColor="Black" SelectionType="Single">
                       <AutoPostBackFlags NodeClick="On" />
                    </ig:WebDataTree>
                </td>  
                <td style="text-align: Left;">
                   <ig:WebDataTree ID="FileTree" runat="server" Height="350px" Width="440px" BorderColor="#333333" BorderStyle="Dotted" BorderWidth="1px" ToolTip="Archivos" SelectionType="Multiple">           
                       <AutoPostBackFlags NodeClick="On" />    
                    </ig:WebDataTree>
                </td>                
            </tr>
        </table>
    </div>
    <div class="titular-into-form">
        <span class="texto-titulo-form">DESTINO</span>
    </div>
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">           
            <tr>
                <td style="text-align: Left; width: 461px;">
                   <ig:WebDataTree ID="DocTreeD" runat="server" Height="350px" Width="440px" BorderColor="#333333" BorderStyle="Dotted" BorderWidth="1px" ToolTip="Carpetas" Font-Names="Arial" ForeColor="Black" SelectionType="Single">
                       <AutoPostBackFlags NodeClick="On" />
                    </ig:WebDataTree>
                </td>  
                <td style="text-align: Left;">
                   <ig:WebDataTree ID="FileTreeD" runat="server" Height="350px" Width="440px" BorderColor="#333333" BorderStyle="Dotted" BorderWidth="1px" ToolTip="Archivos">           
                       <AutoPostBackFlags NodeClick="On" />    
                    </ig:WebDataTree>
                </td>                
            </tr>
        </table>
    </div>

</asp:Content>



