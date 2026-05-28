<%@ Page Title="Log in" Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="DAPP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link rel="stylesheet" href="css/normalize.css" />
    <link rel="stylesheet" href="css/estilos.css" />
    <style type="text/css">
        .auto-style1
        {
            font-family: 'futura_lt_btlight';
            font-size: 16px;
            color: #717171;
            text-align: left;
            height: 19px;
            width: 312px;
        }
        .auto-style2 {
            float: right;
            margin-right: 70px;
            margin-top: 50px;
            width: 231px;
        }
        .auto-style3 {
            font-family: 'futura_lt_btlight';
            font-size: 16px;
            color: #717171;
            text-align: left;
            width: 312px;
        }
        .auto-style4 {
            width: 762px;
        }
        .auto-style5 {
            border-radius: 21px;
            border: 1px solid #74A5BE;
            background: #fff;
            height: 327px;
            width: 651px;
        }
        .auto-style6 {
            width: 235px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="cabezote">
            <div class="logo-cabezote">
                <img src="images/logo-dapp.png" />
            </div>
            <div class="logo-sirl">
                &nbsp;</div>
        </div>        
        <div id="content-login" class="auto-style4">
            <div class="auto-style5">
                <div class="auto-style2">
                    <table border="0" cellpadding="3" cellspacing="0" class="auto-style6">
                        <tr>
                            <td valign="top" class="auto-style3">Usuario<br>
                                <label for="textfield"></label>
                                 <asp:TextBox ID="UserName" runat="server" CssClass="campo-forms-gen"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"  ErrorMessage="El nombre de usuario es requerido." ToolTip="El nombre de usuario es requerido." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>                                 
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="auto-style3">Clave<br />
                               <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="campo-forms-gen"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"  ErrorMessage="La contraseña es requerida." ToolTip="La contraseña es requerida." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="auto-style3">
                                    <span class="cont-boton">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Ingresar" ValidationGroup="LoginUserValidationGroup" OnClick="LoginButton_Click" CssClass="boton-form-home-a" />
                                    </span>                                
                            </td>
                        </tr>
                         <tr>
                            <td valign="top" class="auto-style1">
                                    <span class="cont-boton">                                        
                                         <asp:CustomValidator ID="cvLogin" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="LoginUserValidationGroup" Font-Size="XX-Small"></asp:CustomValidator>                                        
                                    </span>                                
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="auto-style3">
                               <asp:CheckBox ID="CheckRemember" CssClass="texto-encab-campos" runat="server" Text=" Recordarme en este equipo. " placeholder="Contraseña"></asp:CheckBox>
                            </td>
                        </tr>

                                               
                    </table>
                </div>
                <div foto-login>
                    <img src="images/imagen-login.png" />
                </div>                
            </div>           
        </div>
    </form>
</body>
</html>