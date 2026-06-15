<%@ Page Title="Ubicaciones" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Ubicaciones.aspx.vb" Inherits="DAPP.Page_Ubicaciones" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE UBICACIONES </span>
        <ig:WebExcelExporter ID="ExpGrid" runat="server">
        </ig:WebExcelExporter>
        <ig:WebDocumentExporter ID="Exppdf" runat="server">
        </ig:WebDocumentExporter>
    </div>
    <div>
    
            <asp:Button ID="BttUbicaciones" runat="server" Text="Ver Ubicaciones Configuradas" class="btn btn-primary" />
            <br />
            <br />
    </div>
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nueva Ubicacion" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Ubicacion" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Ubicacion" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttSticker" ToolTip="Generar Sticker" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/sticker.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridUbicaciones" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Ubicaciones" DataKeyFields="id">
                        <Columns>
                            <ig:BoundDataField DataFieldName="ubicacion" Key="ubicacion">
                                <Header Text="Ubicación">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="descripcion" Key="descripcion">
                                <Header Text="Descripcion">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Archivo" Key="Archivo">
                                <Header Text="Archivo Vinculado">
                                </Header>
                            </ig:BoundDataField>                                  
                            <ig:BoundDataField DataFieldName="Capacidadtipo" Key="Capacidadtipo">
                                <Header Text="Capacidad Original">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Capacidad" Key="Capacidad">
                                <Header Text="Capacidad Disponible">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="contenido" Key="contenido">
                                <Header Text="Ubicado En">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="tipo" Key="tipo">
                                <Header Text="Tipo Ubicación">
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
                    <asp:SqlDataSource ID="Src_Ubicaciones" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Ubicaciones].[ubicacion]
      ,[Ubicaciones].[descripcion]
      ,[Ubicaciones].[id_tipo]
      ,[Ubicaciones].[id_sucursal]
      ,[Ubicaciones].[id_ubicacion]
      ,[Ubicaciones].[id]
      ,[Ubicaciones].[Capacidad]
      ,[Ubicaciones].[Archivo]
	  ,ubic.ubicacion as contenido
	  ,AdminTipoUbicacion.nombre as tipo
	  ,AdminTipoUbicacion.Capacidad  as Capacidadtipo
  FROM [Ubicaciones] inner join AdminTipoUbicacion 
  on [Ubicaciones].[id_tipo]=AdminTipoUbicacion.id
  left join [Ubicaciones] as ubic
  on [Ubicaciones].[id_ubicacion]=ubic.id
  where [Ubicaciones].[id_sucursal]=@sucursal">
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



