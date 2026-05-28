<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenerarFirma.aspx.vb" Inherits="DAPP.GenerarFirma" %>

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
            width: 600px;
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
        <div style="width:600px;margin-right: 100px;float:center;">
            <table align="center" style="text-align: left; border: 1px solid #808080; padding: 2px; margin: 2px" class="tabla">               
                <tr>
                    <td class="a1">
                        
                    </td>
                    <td class="a2">
                        <div>
                            
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width:100%;text-align: center;" colspan="2">
                        <div style="width:100%;text-align: center;" >
                            <span style="width:100%; text-align:left;"><asp:Label ID="lb1" runat="server" Text="Contraseña:" Width="180px" CssClass="texto-encab-campos"/>
                            <asp:TextBox ID="txtPwd" CssClass="texto-campos" runat="server" TextMode="Password" Width="180px"></asp:TextBox></span> 
                        </div>
                        <div style="width:100%;text-align: center;" >
                            <span style="width:100%; text-align:center;"><asp:Button ID="btOk" runat="server" Text="Generar Firma"  CssClass="btn btn-primary" /></span> 
                        </div>
                        
                    </td>
                    
                </tr>
                <tr>
                    <td style="width:100%;text-align: center;" colspan="2">
                        <div style="width:100%;text-align: center;" >
                            <span style="width:100%; text-align:center;"><asp:Label ID="Label1" runat="server" Text="Fecha:" Width="180px" CssClass="texto-encab-campos"/>
                            <asp:Label ID="lblfecha" runat="server" Text="Fecha Hora:" Width="320px" CssClass="texto-encab-campos"/></span>
                        </div>
                        <div style="width:100%;text-align: center;">
                            
                            <span style="width:100%; text-align:justify;"><asp:Label ID="Lb3" runat="server" Text="ESTE DOCUMENTO ES FIEL COPIA DEL ORIGINAL" Font-Bold="true"   CssClass="texto-encab-campos"/></span>
                            <span style="width:100%; text-align:center;"><asp:Image ID="imgfirma" runat="server" Width="300" Height="150"  /></span>
                        </div>
                        <div style="width:100%;text-align: center;">
                            <span style="width:100%; text-align:center;"><asp:Label ID="lbltercero" runat="server" Text="" Font-Bold="true"   CssClass="texto-encab-campos"/></span>
                        </div>
                        
                    </td>
                    
                </tr>
                                               
            </table>
        <div>
    </div>
    </form>
</body>
</html>
