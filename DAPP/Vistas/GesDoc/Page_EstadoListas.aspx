<%@ Page Title="Documentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_EstadoListas.aspx.vb" Inherits="DAPP.Page_EstadoListas" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ESTADO LISTAS DE CHEQUEO</span>
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
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                <Header Text="Tercero">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="numero_documento" Key="numero_documento">
                                <Header Text="Nro. Identificacion">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="tipo" Key="tipo">
                                <Header Text="Tipo Tercero">
                                </Header>
                            </ig:BoundDataField>                           
                            <ig:BoundDataField DataFieldName="proceso" Key="proceso">
                                <Header Text="Lista Chequeo">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="nombre1" Key="nombre1">
                                <Header Text="Tipo Documento Requerido">
                                </Header>
                            </ig:BoundDataField> 
                            <ig:BoundDataField DataFieldName="cantidad" Key="cantidad">
                                <Header Text="Cantidad Requerida">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="nro_actual" Key="nro_actual">
                                <Header Text="Nro Documentos Actuales">
                                </Header>
                            </ig:BoundDataField>  
                            <ig:BoundDataField DataFieldName="faltantes" Key="faltantes">
                                <Header Text="Nro Documentos Faltantes">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Estado" Key="Estado">
                                <Header Text="Estado Verificación">
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
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select terceros.nombre,terceros.numero_documento,AdminTipoTercero.tipo,procesos.proceso,
  admintipodocumento.nombre,Documentos_requeridos.cantidad,
  (select count(*) from documentos inner join Documentos_terceros
   on documentos_terceros.id_documento=documentos.id and documentos_terceros.id_tercero=terceros.id
   where id_tipo=admintipodocumento.id) as nro_actual,
  Documentos_requeridos.cantidad-(select count(*) from documentos inner join Documentos_terceros
   on documentos_terceros.id_documento=documentos.id and documentos_terceros.id_tercero=terceros.id
   where id_tipo=admintipodocumento.id) as faltantes,
  iif (Documentos_requeridos.cantidad-(select count(*) from documentos inner join Documentos_terceros
   on documentos_terceros.id_documento=documentos.id and documentos_terceros.id_tercero=terceros.id
   where id_tipo=admintipodocumento.id)&gt;0,'Pendiente','Cumplido') as Estado   
  from terceros
  inner join AdminTipoTercero 
  on terceros.id_tipo=admintipotercero.id
  inner join listas_tipo
  on listas_tipo.id_tipo=admintipotercero.id
  inner join procesos
  on listas_tipo.id_proceso=procesos.id
  inner join Documentos_requeridos
  on Documentos_requeridos.id_proceso=procesos.id
  inner join admintipodocumento
  on Documentos_requeridos.id_tipo_documento=admintipodocumento.id
  where terceros.id_sucursal=@sucursal">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="sucursal" SessionField="id_sucursal" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                
            </tr>
        </table>
    </div>

</asp:Content>



