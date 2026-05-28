<%@ Page Title="SOLICITUD / PRESTAMO DE DOCUMENTOS" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Prestamos.aspx.vb" Inherits="DAPP.Page_Prestamos" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">PRESTAMO DE DOCUMENTOS</span>
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
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Solicitud" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                 <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttAbrir" ToolTip="Abrir Documento" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoOpen.png" BackColor="Transparent" Width="26" Height="26" />
                </td>                
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridDocs" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Clientes" EnableAjax="False" DataKeyFields="id">
                        <Columns>
                            <ig:BoundDataField DataFieldName="id" Key="id">
                                <Header Text="Nro Solicitud">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Fecha" Key="Fecha">
                                <Header Text="Fecha Solicitud">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Fecha_prestamo" Key="Fecha_prestamo">
                                <Header Text="Fecha Prestamo">
                                </Header>
                            </ig:BoundDataField>                           
                            <ig:BoundDataField DataFieldName="Fecha_estimada" Key="Fecha_estimada">
                                <Header Text="Fecha Estimada">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fecha_devolucion" Key="fecha_devolucion">
                                <Header Text="Fecha Devolucion">
                                </Header>
                            </ig:BoundDataField> 
                            <ig:BoundDataField DataFieldName="Documento" Key="Documento">
                                <Header Text="Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Carpeta" Key="Carpeta">
                                <Header Text="Carpeta">
                                </Header>
                            </ig:BoundDataField>                                                                             
                            <ig:BoundDataField DataFieldName="Solicitante" Key="Solicitante">
                                <Header Text="Solicitante">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Responsable" Key="Responsable">
                                <Header Text="Responsable">
                                </Header>
                            </ig:BoundDataField>  
                            <ig:BoundDataField DataFieldName="duracion" Key="duracion">
                                <Header Text="Duracion">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Estado" Key="Estado">
                                <Header Text="Estado">
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
                            <ig:Paging PageSize="10">
                            </ig:Paging>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Solicitudes].[id]
      ,[Solicitudes].[Fecha]
      ,[Solicitudes].[Fecha_prestamo]
      ,[Solicitudes].[Fecha_estimada]
      ,[Solicitudes].[fecha_devolucion]
      ,isnull([documentos_solicitud].[id_documento],0) as id_documento
      ,[Solicitudes].[id_ubicacion]
      ,[Solicitudes].[id_solicitante]
      ,[Solicitudes].[id_responsable]
      ,[Solicitudes].[duracion]
      ,[Solicitudes].[Estado]
      ,[Solicitudes].[id_sucursal]
	  ,terceros.nombre as Solicitante
	  ,tx.nombre as Responsable
	  ,isnull(Documentos.alias,'') as Documento
	  ,isnull(ubicaciones.ubicacion,'') as Carpeta
  FROM [Solicitudes]
  left join documentos_solicitud
  on [documentos_solicitud].[id_solicitud]=solicitudes.id 
  left join documentos
  on [documentos_solicitud].[id_documento]=documentos.id
  left join ubicaciones
  on [Solicitudes].[id_ubicacion]=ubicaciones.id 
  inner join terceros
  on [Solicitudes].[id_solicitante]=terceros.id
  inner join terceros as tx
  on [Solicitudes].[id_solicitante]=tx.id
WHERE [Solicitudes].id_sucursal=@sucursal
ORDER BY [Solicitudes].[Fecha] desc">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="sucursal" SessionField="id_sucursal" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                
            </tr>
        </table>
    </div>

</asp:Content>



