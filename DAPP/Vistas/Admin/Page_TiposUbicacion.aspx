<%@ Page Title="Tipos de Ubicación" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_TiposUbicacion.aspx.vb" Inherits="DAPP.Page_TiposUbicacion" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE TIPOS DE UBICACIÓN </span>
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
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nueva Tipo" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Tipo" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Tipo" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
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
                    <ig:WebDataGrid ID="GridTipos" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Tipos" DataKeyFields="id">
                        <Columns>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                <Header Text="Tipo Ubicación">
                                </Header>
                            </ig:BoundDataField>                                                                                              
                            <ig:BoundDataField DataFieldName="Clase" Key="Clase">
                                <Header Text="Clase">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Largo" Key="Largo">
                                <Header Text="Profundidad">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Ancho" Key="Ancho">
                                <Header Text="Ancho">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Alto" Key="Alto">
                                <Header Text="Alto">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Capacidad" Key="Capacidad">
                                <Header Text="Capacidad">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Almacena" Key="Almacena">
                                <Header Text="Contiene">
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
                    <asp:SqlDataSource ID="Src_Tipos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [AdminTipoUbicacion].[nombre]
      ,[AdminTipoUbicacion].[id_cliente]
      ,[AdminTipoUbicacion].[id]
      ,[AdminTipoUbicacion].[Activo]
      ,[AdminTipoUbicacion].[Clase]
      ,[AdminTipoUbicacion].[Largo]
      ,[AdminTipoUbicacion].[Ancho]
      ,[AdminTipoUbicacion].[Alto]
      ,[AdminTipoUbicacion].[Capacidad]
      ,isnull(tiporef.nombre,'Documentos')  Almacena
  FROM [AdminTipoUbicacion] left join
  [AdminTipoUbicacion] as tiporef
  on [AdminTipoUbicacion].[id_tipoCapacidad]=tiporef.id 
  where [AdminTipoUbicacion].[id_sucursal]=@sucursal
ORDER BY nombre">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="sucursal" SessionField="id_sucursal" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="HCliente" runat="server" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>



