<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarUsuario.aspx.vb" Inherits="DAPP.EditarUsuario" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles_form.css">
   

   

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>USUARIO</h3>
                    <ig:WebExcelExporter ID="ExpGrid" runat="server">
                    </ig:WebExcelExporter>
                    <ig:WebDocumentExporter ID="Exppdf" runat="server">
                    </ig:WebDocumentExporter>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Tercero Asociado:" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txttercero" Width="320px" runat="server" CssClass="newsInputD" DataSourceID="sqltercerosx" DataTextField="nombre" DataValueField="id" AutoPostBack="True"></asp:DropDownList>
                                    
                                    
                                    <asp:SqlDataSource ID="sqltercerosx" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select 0 as id, ' Ninguno' as nombre
union
SELECT [id], [nombre] FROM [Terceros] WHERE ([id_cliente] = @id_cliente) and id_tipo in (select id from admintipotercero where interno=1)
ORDER BY [nombre]">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="id_cliente" SessionField="id_cliente" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbUsuario" runat="server" Text="Usuario:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtUsuario" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Usuario" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtUsuario" ErrorMessage=" * El campo 'Usuario' es obligatorio." Width="400px" />
                                </div>

                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbPassword" runat="server" Text="Password:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="cinput-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Password" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtPassword" ErrorMessage=" * El campo 'Password' es obligatorio." Width="400px" />
                                </div>

                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbConfirmarPassword" runat="server" Text="Confirmar Password:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtConfirmarPassword" TextMode="Password" CssClass="input-xlarge" Width="180px" />
                                    <asp:RequiredFieldValidator ID="RQ_ConfirmarPassword" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtConfirmarPassword" ErrorMessage=" * El campo 'Confirmación de Password' es obligatorio." Width="400px" />
                                </div>

                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Email:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="Txtemail" CssClass="input-xlarge" Width="180px" TextMode="Email" />
                                    <asp:RequiredFieldValidator ID="RQ_Email" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtemail" ErrorMessage=" * El campo 'Email' es obligatorio." Width="400px" />
                                </div>

                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbPerfil" runat="server" Text="Perfil:" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtPerfil" Width="180px" runat="server" CssClass="newsInputD" DataSourceID="Src_Perfiles" DataTextField="nombre" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
                                    <br />
                                    <br />
                                </div>

                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="LbNombre" runat="server" Text="Nombre:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="input-xlarge" Width="180px" />
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNombre" ErrorMessage=" * El campo 'Nombre' es obligatorio." Width="400px" />
                                </div>
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbActivo" runat="server" Text="Bloqueado:" Width="180px" />
                                    </b>
                                    <asp:CheckBox ID="txtEstado" runat="server" Text="" Checked="false" Width="180px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label6" runat="server" Text="Administrador Correspondencia:" Width="180px" />
                                    </b>
                                    <asp:CheckBox ID="ChkCorrespondencia" runat="server" Text="" Checked="false" Width="180px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Todos lo Clientes:" Width="180px" />
                                    </b>
                                    <asp:CheckBox ID="Chkclientes" runat="server" Text="" Checked="false" Width="180px" AutoPostBack="True" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Todos los Terceros:" Width="180px" />
                                    </b>
                                    <asp:CheckBox ID="Chkterceros" runat="server" Text="" Checked="false" Width="180px" AutoPostBack="True" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text="Todas las Areas:" Width="180px" />
                                    </b>
                                    <asp:CheckBox ID="Chkareas" runat="server" Text="" Checked="false" Width="180px" AutoPostBack="True" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label7" runat="server" Text="Rol (WF):" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtRol" Width="180px" runat="server" CssClass="newsInputD" DataSourceID="SRC_Rol" DataValueField="MSYSID" DataTextField="Rol_nombre_" ></asp:DropDownList>
                                    <asp:SqlDataSource ID="SRC_Rol" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnectionWF %>" SelectCommand="SELECT [MSYSID], [Rol_nombre_] FROM [Roles] ORDER BY [Rol_nombre_]"></asp:SqlDataSource>
                                    <br />
                                    <br />
                                </div>
                                <div>
                                    <p>&nbsp;</p>                                    
                                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                                </div>
                                <div>                                    
                                    <p>&nbsp;</p>
                                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />                                  
                                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                                    <p>&nbsp;</p>          
                                    <p>&nbsp;</p>                          
                                </div>
                                <asp:Panel ID="PnClientes" runat="server" Width="100%" Visible="false">
                                    <div class="cabezote-form">                                   
                                        <span>CLIENTES ASOCIADOS</span>
                                    </div>
                                    <div >
                                        <table style="margin: auto; width: 100%;">
                                            <tr>
                                                <td style="text-align: center; width: 40px;">&nbsp;</td>
                                                <td style="text-align: center; width: 40px;">
                                                    <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Cliente" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">
                                                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Usuario" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Cliente" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="8">
                                                    <ig:WebDataGrid ID="GridClientes" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Clientes" EnableAjax="False" DataKeyFields="id">
                                                        <Columns>                                                     
                                                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                                                <Header Text="Cliente">
                                                                </Header>
                                                            </ig:BoundDataField>
                                                            <ig:BoundCheckBoxField DataFieldName="todas" Key="todas" CssClass="HeaderCaptionClass">
                                                                <Header Text="Todas las Sucursales">
                                                                </Header>
                                                            </ig:BoundCheckBoxField> 
                                                        </Columns>
                                                        <Behaviors>
                                                            <ig:Selection CellClickAction="Row" SelectedCellCssClass="SelectedCellClass" RowSelectType="Single">
                                                                <AutoPostBackFlags RowSelectionChanged="True" />
                                                            </ig:Selection>
                                                            <ig:Sorting SortingMode="Single" Enabled="true">
                                                            </ig:Sorting>
                                                            <ig:Filtering>
                                                            </ig:Filtering>
                                                        </Behaviors>
                                                    </ig:WebDataGrid>
                                                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Usuario_Cliente.id,clientes.nombre,todas,Usuario_Cliente.id_cliente FROM Usuario_Cliente
inner join clientes
on usuario_cliente.id_cliente=clientes.id
where usuario_cliente.id_usuario=@usuario
ORDER BY clientes.nombre">
                                                        <SelectParameters>
                                                            <asp:QueryStringParameter DefaultValue="0" Name="usuario" QueryStringField="id_usuario" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="cabezote-form">                                   
                                        <span>SUCURSALES ASOCIADAS</span>
                                    </div>
                                    <div >
                                        <table style="margin: auto; width: 100%;">
                                            <tr>
                                                <td style="text-align: center; width: 40px;">&nbsp;</td>
                                                <td style="text-align: center; width: 40px;">
                                                    <asp:ImageButton ID="BttN1" ToolTip="Nuevo Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>

                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton10" ToolTip="Eliminar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton11" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton12" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="8">
                                                    <ig:WebDataGrid ID="GridSucursal" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="SqlSucursales" EnableAjax="False" DataKeyFields="id">
                                                        <Columns>                                                     
                                                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                                                <Header Text="Sucursal">
                                                                </Header>
                                                            </ig:BoundDataField>                           
                                                        </Columns>
                                                        <Behaviors>
                                                            <ig:Selection CellClickAction="Row" SelectedCellCssClass="SelectedCellClass" RowSelectType="Single">
                                                                <AutoPostBackFlags RowSelectionChanged="True" />
                                                            </ig:Selection>
                                                            <ig:Sorting SortingMode="Single" Enabled="true">
                                                            </ig:Sorting>
                                                            <ig:Filtering>
                                                            </ig:Filtering>
                                                        </Behaviors>
                                                    </ig:WebDataGrid>
                                                    <asp:SqlDataSource ID="SqlSucursales" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Usuario_Sucursal.id,AdminSucursal.sucursal as nombre FROM Usuario_Sucursal
inner join AdminSucursal
on usuario_sucursal.id_sucursal=AdminSucursal.id and AdminSucursal.id_cliente=@cliente 
where usuario_sucursal.id_usuario=@usuario
ORDER BY AdminSucursal.sucursal">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="Hdd_id_cliente" DefaultValue="0" Name="cliente" PropertyName="Value" />
                                                            <asp:QueryStringParameter DefaultValue="0" Name="usuario" QueryStringField="id_usuario" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <br />
                                    <br />
                                </asp:Panel>
                                  
                                <asp:Panel ID="PnTerceros" runat="server" Width="100%" Visible="false">
                                    <div class="cabezote-form">                                   
                                        <span>TERCEROS ASOCIADOS</span>
                                    </div>
                                    <br />
                                    <br />
                                    <div >
                                        <table style="margin: auto; width: 100%;">
                                            <tr>
                                                <td style="text-align: center; width: 40px;">&nbsp;</td>
                                                <td style="text-align: center; width: 40px;">
                                                    <asp:ImageButton ID="BttN2" ToolTip="Nuevo Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>

                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton2" ToolTip="Eliminar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton3" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton4" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="8">
                                                    <ig:WebDataGrid ID="GridTerceros" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="SqlTerceros" EnableAjax="False" DataKeyFields="id">
                                                        <Columns>                                                         
                                                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                                                <Header Text="Tercero">
                                                                </Header>
                                                            </ig:BoundDataField>                                                            
                                                        </Columns>
                                                        <Behaviors>
                                                            <ig:Selection CellClickAction="Row" SelectedCellCssClass="SelectedCellClass" RowSelectType="Single">
                                                                
                                                            </ig:Selection>
                                                            <ig:Sorting SortingMode="Single" Enabled="true">
                                                            </ig:Sorting>
                                                            <ig:Filtering>
                                                            </ig:Filtering>
                                                        </Behaviors>
                                                    </ig:WebDataGrid>
                                                    <asp:SqlDataSource ID="SqlTerceros" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Usuarios_Terceros.id,Terceros.nombre FROM Usuarios_Terceros
inner join Terceros
on usuarios_terceros.id_tercero=terceros.id
where usuarios_terceros.id_usuario=@usuario
ORDER BY Terceros.nombre">
                                                        <SelectParameters>                                                            
                                                            <asp:QueryStringParameter DefaultValue="0" Name="usuario" QueryStringField="id_usuario" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PnAreas" runat="server" Width="100%" Visible="false">
                                    <div class="cabezote-form">                                   
                                        <span>AREAS ASOCIADAS</span>
                                    </div>
                                    <div >
                                        <table style="margin: auto; width: 100%;">
                                            <tr>
                                                <td style="text-align: center; width: 40px;">&nbsp;</td>
                                                <td style="text-align: center; width: 40px;">
                                                    <asp:ImageButton ID="BttN3" ToolTip="Nuevo Cliente" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>

                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton6" ToolTip="Eliminar Cliente" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton7" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td style="text-align: center; width: 40px;">&nbsp;
                                                    <asp:ImageButton ID="ImageButton8" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="8">
                                                    <ig:WebDataGrid ID="GridAreas" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="SqlAreas" EnableAjax="False" DataKeyFields="id">
                                                        <Columns>                                                            
                                                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                                                <Header Text="nombre">
                                                                </Header>
                                                            </ig:BoundDataField>                                                            
                                                        </Columns>
                                                        <Behaviors>
                                                            <ig:Selection CellClickAction="Row" SelectedCellCssClass="SelectedCellClass" RowSelectType="Single">
                                                                
                                                            </ig:Selection>
                                                            <ig:Sorting SortingMode="Single" Enabled="true">
                                                            </ig:Sorting>
                                                            <ig:Filtering>
                                                            </ig:Filtering>
                                                        </Behaviors>
                                                    </ig:WebDataGrid>
                                                    <asp:SqlDataSource ID="SqlAreas" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Usuarios_Areas.id,Areas.nombre FROM Usuarios_Areas
inner join Areas
on usuarios_areas.id_area=areas.id
where usuarios_areas.id_usuario=@usuario
ORDER BY Areas.nombre">
                                                        <SelectParameters>                                                            
                                                            <asp:QueryStringParameter DefaultValue="0" Name="usuario" QueryStringField="id_usuario" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>                           
                                
                                                              
                                <asp:SqlDataSource ID="Src_Perfiles" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Id], [nombre] FROM [AdminPerfiles] where id_cliente=@cliente
union all
SELECT [Id], [nombre] FROM [AdminPerfiles] 
where id_cliente is null and 1=(select count(*) from usuarios where usuario=@usuario and id_perfil=1)
ORDER BY [nombre]">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                                        <asp:ControlParameter ControlID="Hdd_id_usuario" DefaultValue="" Name="usuario" PropertyName="Value" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:HiddenField ID="Hdd_id_cliente" runat="server" Value="0" />
                                <asp:HiddenField ID="Hdd_id_usuario" runat="server" Value="" />
                            </div>                          
                </div>
            </div>
        </div>
</asp:Content>


