<%@ Page Title="SucursalTerceros" Language="vb" AutoEventWireup="false" CodeBehind="EditarContacto.aspx.vb" Inherits="DAPP.EditarContacto" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

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
                <h3>CONTACTOS</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Nombre:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNombre" ErrorMessage=" * El campo 'Nombre' es obligatorio." Width="400px" />
                                </div>
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDireccion" runat="server" Text="Cargo:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDireccion" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Direccion" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDireccion" ErrorMessage=" * El campo 'Cargo' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTelefono" runat="server" Text="Teléfono:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Telefono" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtTelefono" ErrorMessage=" * El campo 'Telefono' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Celular:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtCelular" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCelular" ErrorMessage=" * El campo 'Celular' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Dirección:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtdir" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtdir" ErrorMessage=" * El campo 'Dirección' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="width:33%;">
                                                <div>
                                                    <b class="control-label">
                                                        <asp:Label ID="Label4" runat="server" Text="Pais:*" Width="120px" />
                                                    </b>                                                    
                                                    <asp:DropDownList ID="txtpais" runat="server" Width="200px" DataSourceID="SqlPais" DataTextField="Pais" DataValueField="id" AutoPostBack="True" >
                                                    </asp:DropDownList>

                                                    <asp:SqlDataSource ID="SqlPais" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [Pais] FROM [AdminPais] ORDER BY [Pais]"></asp:SqlDataSource>

                                                </div>
                                            </td>
                                            <td style="width:33%;">
                                                    <b class="control-label">
                                                        <asp:Label ID="Label5" runat="server" Text="Estado:*" Width="120px" />
                                                    </b>                                                    
                                                    <asp:DropDownList ID="txtDpto" runat="server" Width="200px" DataSourceID="SqlDpto" DataTextField="departamento" DataValueField="departamento" AutoPostBack="True" >
                                                    </asp:DropDownList>

                                                    <asp:SqlDataSource ID="SqlDpto" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [departamento] FROM [Admin_Ciudades] WHERE ([id_pais] = @id_pais)">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="txtpais" DefaultValue="0" Name="id_pais" PropertyName="SelectedValue" Type="Int64" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                            </td>
                                            <td style="width:34%;">
                                                    <b class="control-label">
                                                        <asp:Label ID="Label6" runat="server" Text="Ciudad:*" Width="120px" />
                                                    </b>                                                    
                                                    <asp:DropDownList ID="txtCiudad" runat="server" Width="200px" DataSourceID="SqlCiudad" DataTextField="ciudad" DataValueField="codigo" >
                                                    </asp:DropDownList>

                                                    <asp:SqlDataSource ID="SqlCiudad" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [codigo], [ciudad] FROM [Admin_Ciudades] WHERE ([departamento] = @departamento) ORDER BY [ciudad]">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="txtDpto" DefaultValue="" Name="departamento" PropertyName="SelectedValue" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Correo:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="Txtcorreo" CssClass="input-xlarge" Width="320px" TextMode="Email"  />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtcorreo" ErrorMessage=" * El campo 'Correo' es obligatorio." Width="400px" />
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

