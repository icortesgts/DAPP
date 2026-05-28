<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenerarStickerU.aspx.vb" Inherits="DAPP.GenerarStickerU" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .divppal {
            text-align: center;
            width: 90%;
        }
        .tabla {
            width: 700px;
        }

        .a1 {
            width: 180px;
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
        <div style="width:700px;margin-right: 100px;float:left;">
            <table style="align-content:center; text-align: left; border: 1px solid #808080; padding: 2px; margin: 2px" class="tabla">               
                <tr>
                    <td style="width:100%;text-align: center;" colspan="2">
                        <div style="width:100%;text-align: center;" >
                            <span style="text-align:center; width:100%;"><asp:Label ID="LblCliente" runat="server" Text=""  Font-Bold="true"  CssClass="texto-encab-campos"/></span>
                        </div>
                        <div style="width:100%;text-align: center;">
                            <span style="text-align:center; width:100%;"><asp:Label ID="LblPadre" runat="server" Text="" Font-Bold="true"  CssClass="texto-encab-campos"/></span>                            
                        </div>
                        <div style="width:100%;text-align: center;">
                            <span style="text-align:center; width:100%;"><asp:Label ID="LblUbicacion" runat="server" Text="" Font-Bold="true"  CssClass="texto-encab-campos"/></span>
                        </div>
                    </td>
                    
                </tr> 
                
                <tr>
                    
                    <td style="width:100%;text-align: center;" colspan="2">
                        <div style="width:100%;text-align: left;">
                            <asp:Label ID="lb4" runat="server" Text="Folios:" Width="180px" CssClass="texto-encab-campos"/>
                            <asp:Label ID="lbFolios" runat="server" Text="" CssClass="texto-campos"/>
                        </div>
                        
                    </td>
                </tr>
                <tr>
                    
                    <td style="width:100%;text-align: center;" colspan="2">
                        <div style="width:100%;text-align: left;">
                            <asp:Label ID="lb3" runat="server" Text="Contenido:" Width="180px" CssClass="texto-encab-campos"/>

                            <asp:TextBox ID="lstcontenido" TextMode="MultiLine" Width ="100%" Height="220px" Wrap="true"  runat="server" ReadOnly="True"></asp:TextBox>

                        </div>
                        
                    </td>
                </tr>
                
                    
            </table>
        <div>
    </div>
    </form>
</body>
</html>
