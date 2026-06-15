<%@ Page Title="Sucursales" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Sucursal.aspx.vb" Inherits="DAPP.Page_sucursal" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE SUCURSALES</span>
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
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nueva Sucursal" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Sucursal" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Sucursal" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttContacto" ToolTip="Contactos Sucursal" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/user.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridClientes" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Clientes" DataKeyFields="Id">
                        <Columns>
                            <ig:BoundDataField DataFieldName="Sucursal" Key="Sucursal">
                                <Header Text="Sucursal">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Direccion" Key="Direccion">
                                <Header Text="Dirección">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Telefono" Key="Telefono">
                                <Header Text="Teléfono">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Departamento" Key="Departamento">
                                <Header Text="Departamento">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="Ciudad" Key="Ciudad">
                                <Header Text="Ciudad">
                                </Header>
                            </ig:BoundDataField>  
                            <ig:BoundCheckBoxField DataFieldName="Activo" Key="Activo">
                                <Header Text="Activo">
                                </Header>
                            </ig:BoundCheckBoxField>
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
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Id]
      ,[Sucursal]
      ,[Direccion]
      ,[Telefono]
      ,[Ciudad]
      ,[Departamento]
      ,[Activo]
  FROM [DAPPBD].[dbo].[AdminSucursal]
where id_cliente=@cliente
order by [Sucursal]">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>



