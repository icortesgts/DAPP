<%@ Page Title="Documentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_RepTransferencias.aspx.vb" Inherits="DAPP.Page_RepTransferencias" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

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
                           
                            <ig:BoundDataField DataFieldName="fecha" Key="fecha">
                                <Header Text="Fecha Transferencia">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                <Header Text="Usuario">
                                </Header>
                            </ig:BoundDataField> 
                            <ig:BoundDataField DataFieldName="alias" Key="alias">
                                <Header Text="Documento">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="orig" Key="orig">
                                <Header Text="Origen">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="archorig" Key="archorig">
                                <Header Text="Archivo Origen">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="dest" Key="dest">
                                <Header Text="Destino">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="archdest" Key="archdest">
                                <Header Text="Archivo Destino">
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
                            <ig:Paging PageSize="100">
                            </ig:Paging>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand=" SELECT Transferencias.id
      ,Transferencias.fecha
      ,id_documento
      ,id_origen
      ,id_destino
      ,usuarios.nombre
	  ,documentos.alias
	  ,origen.ubicacion as orig
      ,origen.archivo as archorig
	  ,destino.ubicacion as dest
      ,destino.archivo as archdest
  FROM Transferencias inner join documentos
  on transferencias.id_documento=documentos.id and documentos.id_sucursal=@sucursal
  inner join ubicaciones as origen
  on transferencias.id_origen=origen.id
  inner join ubicaciones as destino
  on transferencias.id_destino=destino.id
  inner join usuarios
  on transferencias.usuario=usuarios.usuario
  order by Transferencias.fecha desc">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="sucursal" SessionField="id_sucursal" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                
            </tr>
        </table>
    </div>

</asp:Content>



