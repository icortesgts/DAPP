<%@ Page Title="Sucursales" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_BackUp.aspx.vb" Inherits="DAPP.Page_BackUp" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE COPIAS DE SEGURIDAD</span>
        <ig:WebExcelExporter ID="ExpGrid" runat="server">
        </ig:WebExcelExporter>
        <ig:WebDocumentExporter ID="Exppdf" runat="server">
        </ig:WebDocumentExporter>
    </div>
    <br />
    <br />
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nueva Solicitud" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Solicitud" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Solicitud" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de Realizar esta Operación?');" />
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
                    <ig:WebDataGrid ID="GridClientes" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Clientes" DataKeyFields="id">
                        <Columns>
                            
                            <ig:BoundDataField DataFieldName="Fecha_solicitud" Key="Fecha_solicitud">
                                <Header Text="Fecha Solicitud">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fecha_respuesta" Key="fecha_respuesta">
                                <Header Text="Fecha Atención">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="Estado" Key="Estado">
                                <Header Text="Estado">
                                </Header>
                            </ig:BoundDataField>  
                            <ig:BoundDataField DataFieldName="Ruta" Key="Ruta">
                                <Header Text="Ruta">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="cliete" Key="cliete">
                                <Header Text="Cliete">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="sucursal" Key="sucursal">
                                <Header Text="Sucursal">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="usuario" Key="usuario">
                                <Header Text="Usuario">
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
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Backups.id
      ,id_usuario
      ,Fecha_solicitud
      ,fecha_respuesta
      ,Backups.Estado
      ,Ruta
	  ,backups.id_cliente
	  ,backups.id_sucursal
	  ,clientes.nombre as cliete
	  ,isnull(adminsucursal.sucursal,'Todas') as sucursal
	  ,usuarios.nombre as usuario
  FROM Backups INNER JOIN USUARIOS
  on backups.id_usuario=usuarios.id and usuarios.usuario=@usuario
  inner join clientes
  on backups.id_cliente = clientes.id and clientes.id=@cliente
  left join AdminSucursal
  on backups.id_sucursal=AdminSucursal.id
order by Fecha_solicitud desc">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Hdd_id_usuario" DefaultValue="" Name="usuario" PropertyName="Value" />
                            <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="Hdd_id_usuario" runat="server" Value="0" />
</asp:Content>



