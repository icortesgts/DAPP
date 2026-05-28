<%@ Page Title="Archivo Virtual" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Archivo.aspx.vb" Inherits="DAPP.Page_Archivo" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ARCHIVO VIRTUAL</span>
    </div>
    <br />
    <br />
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">           
            <tr style="width: 100%;">
                <td style="text-align: Left; width: 50%;">
                   <ig:WebDataTree ID="DocTree" runat="server"  Width="95%" BorderColor="#333333" BorderStyle="Dotted" BorderWidth="1px" ToolTip="Carpetas" Font-Names="Arial" ForeColor="Black">
                       <AutoPostBackFlags NodeClick="On" />
                    </ig:WebDataTree>
                </td>  
                <td style="text-align: Left;width: 50%;">
                   <ig:WebDataTree ID="FileTree" runat="server"  Width="95%" BorderColor="#333333" BorderStyle="Dotted" BorderWidth="1px" ToolTip="Archivos">           
                       <AutoPostBackFlags NodeClick="On" />    
                    </ig:WebDataTree>
                </td>                
            </tr>
        </table>
    </div>

</asp:Content>



