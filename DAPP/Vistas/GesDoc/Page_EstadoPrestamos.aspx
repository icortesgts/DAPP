<%@ Page Title="Documentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_EstadoPrestamos.aspx.vb" Inherits="DAPP.Page_EstadoPrestamos" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ESTADO PRESTAMOS</span>
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
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridDocs" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Clientes" EnableAjax="False">
                        <Columns>
                            <ig:BoundDataField DataFieldName="solicitante" Key="solicitante">
                                <Header Text="Solicitante">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="carpeta" Key="carpeta">
                                <Header Text="Carpeta">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="documento" Key="documento">
                                <Header Text="Documento">
                                </Header>
                            </ig:BoundDataField>                           
                            <ig:BoundDataField DataFieldName="fecha_prestamo" Key="fecha_prestamo">
                                <Header Text="Fecha Prestamo">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fecha_estimada" Key="fecha_estimada">
                                <Header Text="Fecha Esperada de Devolución">
                                </Header>
                            </ig:BoundDataField> 
                            <ig:BoundDataField DataFieldName="dias" Key="dias">
                                <Header Text="Días de Retraso">
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
                            <ig:Paging PageSize="10">
                            </ig:Paging>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select terceros.nombre as solicitante, isnull(ubicaciones.ubicacion,'') as carpeta,isnull(documentos.alias,'') as documento,
  fecha_prestamo,fecha_estimada,DATEDIFF (day , getdate(),fecha_estimada) as dias from solicitudes
  inner join terceros
  on solicitudes.id_solicitante=terceros.id
  left join ubicaciones
  on solicitudes.id_ubicacion= ubicaciones.id 
  left join documentos_solicitud
  on documentos_solicitud.id_solicitud=solicitudes.id
  left join documentos
  on documentos_solicitud.id_documento=documentos.id
  where solicitudes.estado='Prestado' and fecha_estimada < getdate()  and terceros.id_cliente=@cliente">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                
            </tr>
        </table>
    </div>

</asp:Content>



