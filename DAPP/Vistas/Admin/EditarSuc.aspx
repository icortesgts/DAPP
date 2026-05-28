<%@ Page Title="SucursalTerceros" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarSuc.aspx.vb" Inherits="DAPP.EditarSuc" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>SUCURSAL TERCEROS</h3>               
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
                                        <asp:Label ID="lbDireccion" runat="server" Text="Dirección:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDireccion" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Direccion" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDireccion" ErrorMessage=" * El campo 'Dirección' es obligatorio." Width="400px" />
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
                                        <asp:Label ID="lbTelefono" runat="server" Text="Teléfono:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Telefono" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtTelefono" ErrorMessage=" * El campo 'Telefono' es obligatorio." Width="400px" />
                                </div>
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbCorreo" runat="server" Text="Correo:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtCorreo" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Correo" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCorreo" ErrorMessage=" * El campo 'Correo' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Activo:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="Chkactivo" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
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
</asp:Content>


