<%@ Page Title="Documentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_EstadoArchivo.aspx.vb" Inherits="DAPP.Page_EstadoArchivo" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ESTADO ARCHIVO</span>
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
                            <ig:BoundDataField DataFieldName="alias" Key="alias">
                                <Header Text="Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="tipodoc" Key="tipodoc">
                                <Header Text="Tipo Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fechadoc" Key="fechadoc">
                                <Header Text="Fecha Focumento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fgestion" Key="fgestion">
                                <Header Text="Fecha A. Gestión">
                                </Header>
                            </ig:BoundDataField>                           
                            <ig:BoundDataField DataFieldName="fcentral" Key="fcentral">
                                <Header Text="Fecha A. Central">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fhistorico" Key="fhistorico">
                                <Header Text="Fecha A. Histórico">
                                </Header>
                            </ig:BoundDataField> 
                            <ig:BoundDataField DataFieldName="ubicacion" Key="ubicacion">
                                <Header Text="Ubicación">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="archivo" Key="archivo">
                                <Header Text="Archivo">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="gest" Key="gest">
                                <Header Text="Tiempo A. Gestión">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="central" Key="central">
                                <Header Text="Tiempo A. Central">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="hist" Key="hist">
                                <Header Text="Tiempo A. Histórico">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="DifGestion" Key="DifGestion">
                                <Header Text="Permanecia Adicional A. Gestión">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="DifCentral" Key="DifCentral">
                                <Header Text="Permanecia Adicional A. Central">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="DifHistorico" Key="DifHistorico">
                                <Header Text="Permanecia Adicional A. Histórico">
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
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select documentos.alias,documentos.fechadoc,documentos.fgestion,isnull(documentos.fcentral,'') as fcentral,isnull(documentos.fhistorico,'') as fhistorico,
  ubicaciones.ubicacion,ubicaciones.archivo,
  convert(varchar(10),gestion) + ' ' + tgestion as gest,
  convert(varchar(10),central) + ' ' + tcentral as central,
  convert(varchar(10),historico) + ' ' + thistorico as hist,
  admintipodocumento.nombre as tipodoc,
  gestion-iif(tgestion='Años',DATEDIFF(year ,documentos.fechadoc,getdate()),DATEDIFF(month , documentos.fechadoc,getdate())) DifGestion,
  central-iif(tcentral='Años',DATEDIFF(year , isnull(documentos.fgestion,getdate()),getdate()),DATEDIFF(month , isnull(documentos.fgestion,getdate()),getdate())) DifCentral,
  historico-iif(thistorico='Años',DATEDIFF(year , isnull(documentos.fcentral,getdate()),getdate()),DATEDIFF(month , isnull(documentos.fcentral,getdate()),getdate())) DifHistorico
  from tipoDoc_TablaRetencion
  inner join admintipodocumento
  on tipoDoc_TablaRetencion.id_tipo=admintipodocumento.id
  inner join documentos
  on documentos.id_tipo=admintipodocumento.id 
  inner join ubicaciones
  on documentos.id_ubicacion=ubicaciones.id
  where gestion-iif(tgestion='Años',DATEDIFF(year ,documentos.fechadoc,getdate()),DATEDIFF(month , documentos.fechadoc,getdate())) &lt;= 0 or
  central-iif(tcentral='Años',DATEDIFF(year , isnull(documentos.fgestion,getdate()),getdate()),DATEDIFF(month , isnull(documentos.fgestion,getdate()),getdate())) &lt;=0 or
  historico-iif(thistorico='Años',DATEDIFF(year , isnull(documentos.fcentral,getdate()),getdate()),DATEDIFF(month , isnull(documentos.fcentral,getdate()),getdate())) &lt;=0
and documentos.id_sucursal=@sucursal">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="sucursal" SessionField="id_sucursal" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                
            </tr>
        </table>
    </div>

</asp:Content>



