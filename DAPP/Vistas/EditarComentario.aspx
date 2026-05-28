<%@ Page Title="Comentarios" Language="vb" AutoEventWireup="false" CodeBehind="EditarComentario.aspx.vb" Inherits="DAPP.EditarComentario" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>EDITAR CONTACTOS</title>
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
 


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>COMENTARIOS</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label7" runat="server" Text="Fecha:*" Width="180px" />
                                    </b>
                                    <b class="control-label">
                                        <asp:Label ID="lblfecha" runat="server" Text="" Width="180px" />
                                    </b>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Comentario:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="input-xlarge" Width="320px" TextMode="MultiLine" Height="66px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNombre" ErrorMessage=" * El campo 'Comentario' es obligatorio." Width="400px" />
                                </div>
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDireccion" runat="server" Text="Autor:*" Width="180px" />
                                    </b>
                                    <b class="control-label">
                                        <asp:Label ID="lblAutor" runat="server" Text="" Width="180px" />
                                    </b>
                                </div>
                                
                                
                                <br />
                                <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />                                  
                                <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                                <br />
                                <br />
                                <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                            </div>                      
            </div>
        </div>
    </div>
    </form>
</body>
</html>

