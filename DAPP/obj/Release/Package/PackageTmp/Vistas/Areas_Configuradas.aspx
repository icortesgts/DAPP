<%@ Page Title="" Language="VB" AutoEventWireup="true" CodeBehind="Areas_Configuradas.aspx.vb" Inherits="DAPP.Areas_Configuradas" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Areas Configuradas</title>
    <link rel="stylesheet" type="text/css" href="../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../Styles/styles_form.css">    
    <style type="text/css">
        .altura {
            height: 45px;
        }       
        
    </style>
</head>
<body>
    <form id="form1" runat="server" class="formulario">
        <asp:ScriptManager ID="MGR" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="AJAX_Panel" runat="server">
            <ContentTemplate>

                <div class="container mrgnBotMd" style="width: 100%;">
                    <div class="row noPadding">
                        <div class="col-sm-12 noPadding colHeight">
                            <h3>Areas Configuradas</h3>
                            <div class="form-horizontal fom-border">

                                <table style="width: 100%;height:100%">
                                    <tr>
                                        <td style="text-align: left">
                                            <ig:WebDataTree ID="DocTree" runat="server"  Width="90%" BorderColor="#333333" BorderStyle="Dotted" BorderWidth="1px" SelectionType="Single">                                              
                                            </ig:WebDataTree>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="bttSeleccionar" runat="server" Text="Cerrar" class="btn btn-primary" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
