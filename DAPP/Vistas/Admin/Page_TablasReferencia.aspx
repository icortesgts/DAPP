<%@ Page Title="Tablas de Retención" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_TablasReferencia.aspx.vb" Inherits="DAPP.Page_TablasReferencia" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles_form.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE TABLAS DE RETENCION </span>
        <ig:WebExcelExporter ID="ExpGrid" runat="server">
        </ig:WebExcelExporter>
        <ig:WebDocumentExporter ID="Exppdf" runat="server">
        </ig:WebDocumentExporter>
    </div>
    
    <div class="DivFormulario">
        <br />
        <br />
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttNew" ToolTip="Nuevo Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEdit" ToolTip="Editar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttDelete" ToolTip="Eliminar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
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
                    <ig:WebDataGrid ID="GridTablas" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Tablas" DataKeyFields ="id" EnableAjax="False">
                        <Columns>
                            <ig:BoundDataField DataFieldName="oficina" Key="oficina">
                                <Header Text="Oficina Productora">
                                </Header>
                            </ig:BoundDataField> 
                            <ig:BoundDataField DataFieldName="entidad" Key="entidad">
                                <Header Text="Entidad Productora">
                                </Header>
                            </ig:BoundDataField>             
                            <ig:BoundDataField DataFieldName="responsable" Key="responsable">
                                <Header Text="responsable">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Codigo" Key="Codigo">
                                <Header Text="Codigo">
                                </Header>
                            </ig:BoundDataField>

                            <ig:BoundDataField DataFieldName="fechainicio" Key="fechainicio">
                                <Header Text="Fecha Inicio">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fechafin" Key="fechafin">
                                <Header Text="Fecha Fin">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundCheckBoxField DataFieldName="Activo" Key="Activo">
                                <Header Text="Activo">
                                </Header>
                            </ig:BoundCheckBoxField>
                                                
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
                    <asp:SqlDataSource ID="Src_Tablas" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [entidad], [oficina], [responsable], [id], [fechainicio], [fechafin], [Activo], [Codigo] FROM [AdminTablasReferencia] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [oficina]" >
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="titular-into-form">
        <span class="texto-titulo-form">SERIES Y TIPOS DOCUMENTALES</span>
    </div>                  
                          
    <div class="DivFormulario">
        <br />
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttExc1" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttpd1" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridDetTablas" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_DetTablas" DataKeyFields="id">
                        <Columns>

                            <ig:BoundDataField DataFieldName="TipoDocumento" Key="TipoDocumento">
                                <Header Text="Tipo Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="codigo" Key="codigo">
                                <Header Text="Código">
                                </Header>
                            </ig:BoundDataField>                                                                      
                            <ig:BoundDataField DataFieldName="gestion" Key="gestion">
                                <Header Text="Archivo Gestión">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="central" Key="central">
                                <Header Text="Archivo Central">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="historico" Key="historico">
                                <Header Text="Archivo Histórico">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Original" Key="Original">
                                <Header Text="O / C">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundCheckBoxField DataFieldName="CT" Key="CT">
                                <Header Text="CT">
                                </Header>
                            </ig:BoundCheckBoxField>
                            <ig:BoundCheckBoxField DataFieldName="E" Key="E">
                                <Header Text="E">
                                </Header>
                            </ig:BoundCheckBoxField>
                            <ig:BoundCheckBoxField DataFieldName="M" Key="M">
                                <Header Text="M">
                                </Header>
                            </ig:BoundCheckBoxField>
                            <ig:BoundCheckBoxField DataFieldName="D" Key="D">
                                <Header Text="D">
                                </Header>
                            </ig:BoundCheckBoxField>
                            <ig:BoundCheckBoxField DataFieldName="S" Key="S">
                                <Header Text="S">
                                </Header>
                            </ig:BoundCheckBoxField>
                            
                            <ig:BoundDataField DataFieldName="procedimientos" Key="procedimientos">                            
                                <Header Text="Procedimiento">
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
                    <asp:SqlDataSource ID="Src_DetTablas" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [codigo], [gestion], [central], [CT], [E], [M], [D], [S], [procedimientos], AdminTipodocumento.nombre as TipoDocumento, [historico], iif([Original]=1,'O','C') [Original],  [TipoDoc_TablaRetencion].[id] FROM [TipoDoc_TablaRetencion] inner join AdminTipodocumento on [TipoDoc_TablaRetencion].id_tipo=AdminTipodocumento.id WHERE ([id_tablaret] = @id_tablaret) ORDER BY [TipoDocumento]" >
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HiddenField1" DefaultValue="0" Name="id_tablaret" PropertyName="Value" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="HCliente" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <b class="control-label">
            <p>
                CONVENCIONES
                CT = Conservación Total       M = Microfilmación        D = Digitalización
                E  = Eliminación              S = Selección             O = Original
                C  = Copia
            </p>

        </b>
    </div>

</asp:Content>



