<%@ Page Title="Edición Copias de Seguridad" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarBackup.aspx.vb" Inherits="DAPP.EditarBackup" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>COPIA DE SEGURIDAD</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Fecha Solicitud:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtFecha" CssClass="input-xlarge" Width="180px"  TextMode="DateTime" ReadOnly="true"  />                                    
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDireccion" runat="server" Text="Usuario:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtUsuario" CssClass="input-xlarge" Width="320px" ReadOnly ="true"  />                                    
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTipoDocumento" runat="server" Text="Cliente:*" Width="180px" />
                                    </b>

                                    <asp:DropDownList ID="txtCliente" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlCliente" DataTextField="nombre" DataValueField="id" AutoPostBack="True">
                                     </asp:DropDownList>                                  
                                    <asp:SqlDataSource ID="SqlCliente" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT clientes.id,clientes.nombre from clientes
inner join usuario_cliente
on usuario_cliente.id_cliente=clientes.id and id_usuario=@usuario
order by clientes.nombre">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="Hdd_id_usuario" Name="usuario" PropertyName="Value" DefaultValue="0" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Sucursal:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtSucursal" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlSucursal" DataTextField="Sucursal" DataValueField="id">
                                     </asp:DropDownList>                                  
                                    <asp:SqlDataSource ID="SqlSucursal" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="Select 0 as id, ' Todas' as Sucursal
union all
SELECT id,sucursal from AdminSucursal
where id_cliente=@Cliente
order by sucursal">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtCliente" Name="Cliente" PropertyName="SelectedValue" DefaultValue="0" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTelefono" runat="server" Text="Estado:" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="TxtEstado" runat="server" Width="320px" Enabled="false" >
                                        <asp:ListItem>Sollicitud</asp:ListItem>
                                        <asp:ListItem>En Proceso</asp:ListItem>
                                        <asp:ListItem>Disponible</asp:ListItem>
                                        <asp:ListItem>Caducado</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Fecha Respuesta:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtFecResp" CssClass="input-xlarge" Width="180px"  TextMode="DateTime" ReadOnly="true"  />                                    
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text="Ruta de Acceso:" Width="180px" />
                                    </b>
                                    <asp:HyperLink ID="TxtRuta" runat="server">Back Up</asp:HyperLink>
                                    
                                </div>
                                
                                
                                <br />
                                <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />                                  
                                <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                                <br />
                                <br />
                                <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                                <asp:HiddenField ID="Hdd_id_usuario" runat="server" Value="0" />
                            </div>                      
            </div>
        </div>
    </div>
</asp:Content>


