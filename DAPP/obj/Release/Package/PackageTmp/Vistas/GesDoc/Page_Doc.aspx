<%@ Page Title="Documentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Doc.aspx.vb" Inherits="DAPP.Page_Doc" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE DOCUMENTOS </span>
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
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Documento" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Documento" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Documento" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
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
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttScan" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoscaner.jpg" BackColor="Transparent" Width="26" Height="26" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridDocs" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Clientes" DataKeyFields="id" EnableAjax="False">
                        <Columns>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                <Header Text="Archivo">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="alias" Key="alias">
                                <Header Text="Nombre">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="descripcion" Key="descripcion">
                                <Header Text="Descripcion">
                                </Header>
                            </ig:BoundDataField>                           
                            <ig:BoundDataField DataFieldName="version" Key="version">
                                <Header Text="Version">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="folios" Key="folios">
                                <Header Text="Folios">
                                </Header>
                            </ig:BoundDataField> 
                            <ig:BoundDataField DataFieldName="fechadoc" Key="fechadoc">
                                <Header Text="Fecha Documento">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="fecha_creacion" Key="fecha_creacion">
                                <Header Text="Fecha Creacion">
                                </Header>
                            </ig:BoundDataField>  
                            <ig:BoundDataField DataFieldName="fecha_modificacion" Key="fecha_modificacion">
                                <Header Text="Fecha Modificacion">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="usuario" Key="usuario">
                                <Header Text="Usuario">
                                </Header>
                            </ig:BoundDataField>                                                      
                            <ig:BoundDataField DataFieldName="ubicacion" Key="ubicacion">
                                <Header Text="Ubicacion">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="tipodoc" Key="tipodoc">
                                <Header Text="Tipo Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Area" Key="Area">
                                <Header Text="Area">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Tercero" Key="Tercero">
                                <Header Text="Tercero">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="estado" Key="estado">
                                <Header Text="Estado">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Areaid" Key="Areaid">
                                <Header Text="ID Area">
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
                    <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT distinct Documentos.nombre
  ,Documentos.descripcion
  ,Documentos.version
  ,Documentos.fechadoc
  ,Documentos.fecha_creacion
  ,Documentos.fecha_modificacion
  ,Documentos.usuario
  ,Documentos.id_tipo
  ,Documentos.id_ubicacion
  ,Documentos.id_cliente
  ,Documentos.id
  ,Documentos.alias
  ,Documentos.folios
  ,Documentos.estado
  ,isnull(ubicaciones.ubicacion,'Pendiente') as ubicacion
  ,AdminTipoDocumento.nombre as tipodoc
  ,isnull(Areas.nombre,'') Area
  ,isnull(Areas.id,'') Areaid
  ,isnull(Terceros.nombre,'') Tercero
  ,isnull(usuarios.id_tercero,0) as idtx
  FROM Documentos
  left join ubicaciones
  on Documentos.id_ubicacion=ubicaciones.id
  inner join AdminTipoDocumento
  on Documentos.id_tipo=AdminTipoDocumento.id
  left join documentos_area
  on documentos.id=documentos_area.id_documento
  left join Areas
  on Documentos_Area.id_area=Areas.id
  left join Documentos_Terceros
  on Documentos.id=Documentos_Terceros.id_documento
  left join Terceros
  on Documentos_Terceros.id_tercero=Terceros.id
  inner join usuarios
  on documentos.usuario=usuarios.usuario
  left join Terceros as trx
  on usuarios.id_tercero=trx.id
  where Documentos.id_ubicacion is null and Documentos.id_sucursal=@sucursal and 
  (
   (1=(select 1 from usuarios where usuario=@usuario and id_perfil=-1)) 
   or 
   (usuarios.usuario=@usuario or (isnull(usuarios.id_tercero,0) <>0 and documentos.id in (select id_documento from documentos_area where id_area=trx.id_area)))
  )">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="sucursal" SessionField="id_sucursal" />
                            <asp:SessionParameter DefaultValue="" Name="usuario" SessionField="Usuario" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                
            </tr>
        </table>
    </div>

</asp:Content>



