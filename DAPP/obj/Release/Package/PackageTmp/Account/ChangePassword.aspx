<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangePassword.aspx.vb" Inherits="DAPP.ChangePassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cambiar PasswordY</title>
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
            width: 415px;
        }
        .auto-style3 {
            font-family: 'futura_lt_btlight';
            font-size: 16px;
            color: #717171;
            text-align: left;
            width: 312px;
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
                <img src="images/logo-empresa.png" />
            </div>
        </div>        
        <div id="content-login">
            <div class="marco-login">
                <div class="auto-style2">
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td valign="top" class="auto-style3">
                                <asp:Label runat="server" AssociatedControlID="UserName" CssClass="texto-encab-campos">Usuario:</asp:Label>
                                <div class="col-md-10">
                                   <asp:TextBox ID="UserName" runat="server" CssClass="campo-forms-gen" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        CssClass="text-danger" ErrorMessage="El nombre de usuario es requerido."
                                        ToolTip="El nombre de usuario es requerido." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="auto-style3">
                                <asp:Label runat="server" AssociatedControlID="UserName" CssClass="texto-encab-campos">Contraseña Actual:</asp:Label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="CurrentPassword" runat="server" CssClass="campo-forms-gen" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                        CssClass="text-danger" ErrorMessage="La contraseña actual es requerida."
                                        ToolTip="La contraseña actual es requerida." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="auto-style3">
                                <asp:Label runat="server" AssociatedControlID="NewPassword" CssClass="texto-encab-campos">Contraseña Nueva:</asp:Label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="NewPassword" runat="server" CssClass="campo-forms-gen" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                        CssClass="text-danger" ErrorMessage="La contraseña nueva es requerida."
                                        ToolTip="La contraseña nueva es requerida." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                </div>                             
                            </td>
                        </tr>
                         <tr>
                            <td valign="top" class="auto-style1">
                                <asp:Label runat="server" AssociatedControlID="ConfirmNewPassword" CssClass="texto-encab-campos">Repetir Contraseña Nueva:</asp:Label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="campo-forms-gen" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                        CssClass="text-danger" Display="Dynamic" ErrorMessage="La contraseña de confirmación es requerida."
                                        ToolTip="La contraseña de confirmación es requerida." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                        ControlToValidate="ConfirmNewPassword" CssClass="text-danger" Display="Dynamic"
                                        ErrorMessage="La contraseña de confirmación debe conincidir con la nueva contraseña."
                                        ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                                </div>                                
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="auto-style3">
                               <asp:CustomValidator ID="cvLogin" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="LoginUserValidationGroup" Font-Size="XX-Small"></asp:CustomValidator> 
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="auto-style3">
                                <div class="col-md-offset-2 col-md-10">
                                    <asp:Button ID="ButtonOk" runat="server" Text="Cambiar Contraseña" ValidationGroup="ChangeUserPasswordValidationGroup" CssClass="boton-form-home-a" Width="162px" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" Text="Cancelar" CssClass="boton-form-home-a" />
                                </div>
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

