<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenerarSticker.aspx.vb" Inherits="DAPP.GenerarSticker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .divppal {
            text-align: right;
            width: 70%;
        }
        .tabla {
            width: 400px;
        }

        .a1 {
            width: 80px;
            text-align: left;
        }
        .a2 {
            width: 320px;
            text-align: left;
        }       
    </style>
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">
</head>
<body>
    <form id="form1" runat="server">
    <div class="divppal">
        <div style="width:400px;margin-right: 100px;float:right;">
            <table align="right" style="text-align: left; border: 1px solid #808080; padding: 2px; margin: 2px" class="tabla">               
                <tr>
                    <td class="a1" colspan="2">
                        <asp:Label ID="lblTitulo" runat="server" Width="360px" CssClass="texto-encab-campos" Font-Bold="True" Font-Size="Larger"/>
                    </td>
                </tr>
                <tr>
                    <td class="a1">
                        <asp:Label ID="lb1" runat="server" Text="Fecha Hora:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="lbFechaHora" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="lb2" runat="server" Text="Ciudad:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="lbCiudad" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="lb3" runat="server" Text="Sucursal:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="lbSucursal" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="lb4" runat="server" Text="Remitente:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="lbEmpresa" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="lb5" runat="server" Text="Numero Radicación:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="lbNumeroRadicacion" runat="server" Text=""  CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="lb6" runat="server" Text="Destinatario:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="lbDestinatario" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="Label2" runat="server" Text="Responsable:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="LblResponsable" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="lb7" runat="server" Text="Numero de Folios:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="lbNumeroFolios" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="Label1" runat="server" Text="Numero de Paquetes:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="LblPaquete" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 <tr>
                    <td class="a1">
                        <asp:Label ID="lb8" runat="server" Text="Referencia:" Width="180px" CssClass="texto-encab-campos"/>
                    </td>
                    <td class="a2">
                        <asp:Label ID="lbReferencia" runat="server" Text="" CssClass="texto-campos"/>
                    </td>
                </tr>
                 
                                
            </table>
        <div>
    </div>
    </form>
</body>
</html>
