<%@ Page Title="Clientes" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Clientes.aspx.vb" Inherits="DAPP.Page_Clientes" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE CLIENTES </span>
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
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Cliente" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Cliente" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Cliente" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de Realizar esta Operación?');" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttContacto" ToolTip="Contactos Cliente" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/user.png" BackColor="Transparent" Width="26" Height="26" />
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
                    <ig:WebDataGrid ID="GridClientes" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Clientes" DataKeyFields="id" EnableAjax="False">
                        <Columns>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre" CssClass="HeaderCaptionClass">
                                <Header Text="Nombre">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="tipo_documento" Key="tipo_documento" CssClass="HeaderCaptionClass">
                                <Header Text="Tipo Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="numero_documento" Key="numero_documento" CssClass="HeaderCaptionClass">
                                <Header Text="Numero Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="celular" Key="celular" CssClass="HeaderCaptionClass">
                                <Header Text="Celular">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="correo_electronico" Key="correo_electronico" CssClass="HeaderCaptionClass">
                                <Header Text="Correo">
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
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [nombre],[tipo_documento],[numero_documento],[celular],[correo_electronico],[id] FROM [Clientes] ORDER BY [nombre]"></asp:SqlDataSource>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div class="titular-into-form">
        <span class="texto-titulo-form">CONTRATOS CLIENTE </span>
    </div>
    <br />
    <br />
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="BttNew" ToolTip="Nuevo Contrato" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="BttEdit" ToolTip="Editar Contrato" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttDelete" ToolTip="Eliminar Contrato" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de Realizar esta Operación?');" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttXls" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttRep" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridContratos" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="SqlContratos" EnableAjax="False" DataKeyFields="id">
                        <Columns>
                            
                            <ig:BoundDataField DataFieldName="Nro_Contrato" Key="Nro_Contrato">
                                <Header Text="Nro Contrato">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Fecha_inicio" Key="Fecha_inicio">
                                <Header Text="Fecha Inicio">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="Fecha_fin" Key="Fecha_fin">
                                <Header Text="Fecha Fin">
                                </Header>
                            </ig:BoundDataField>  
                            <ig:BoundDataField DataFieldName="Tipo_Pago" Key="Tipo_Pago">
                                <Header Text="Tipo Pago">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Estado" Key="Estado">
                                <Header Text="Estado">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Licencia" Key="Licencia">
                                <Header Text="Licencia">
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
                    <asp:SqlDataSource ID="SqlContratos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Contratos_Cliente.id
      ,id_cliente
      ,Nro_Contrato
      ,Fecha_inicio
      ,Fecha_fin
      ,Tipo_Pago
      ,Estado
      ,Licencia
	  ,Clientes.nombre as Cliente
  FROM Contratos_Cliente 
  inner join Clientes
  on Contratos_cliente.id_cliente=Clientes.id and clientes.id=@cliente
order by Fecha_inicio desc">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Hdd_id_cliente" DefaultValue="0" Name="cliente" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="Hdd_id_cliente" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>



