<%@ Page Title="SucursalTerceros" Language="vb" AutoEventWireup="false" CodeBehind="EditarInfo.aspx.vb" Inherits="DAPP.EditarInfo" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>EDITAR INFORMACIÓN USUARIOS</title>
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
                <h3>INFORMACIÓN ASOCIADA</h3>               
                    <div class="form-horizontal fom-border">                        
                    <br />
                    <br />                                
                                
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNombre" runat="server" Text="Nombre:*" Width="180px" />
                        </b>                                   
                                    
                        <asp:DropDownList ID="txtNombre" runat="server" CssClass="input-xlarge" Width="320px" DataSourceID="SqlNombre" DataTextField="nombre" DataValueField="id">
                        </asp:DropDownList>
                                    
                        <asp:SqlDataSource ID="SqlNombre" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select id,nombre from clientes where id not in (select id_cliente from usuario_cliente where id_usuario=@usuario) order by nombre">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="0" Name="usuario" QueryStringField="id_usuario" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                                    
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Lbltodos" runat="server" Text="Todas las Sucursales:" Width="180px" Visible="false"  />
                        </b>
                        <asp:CheckBox ID="Chktodos" runat="server" Text="" Checked="false" Width="180px" visible="false"  />
                    </div>
                                
                                
                    <br />
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

