<%@ Page Title="Listas de Chequeo" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_ListaChequeo.aspx.vb" Inherits="DAPP.Page_ListaChequeo" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE LISTAS DE CHEQUEO</span>
        <ig:WebExcelExporter ID="ExpGrid" runat="server">
        </ig:WebExcelExporter>
        <ig:WebDocumentExporter ID="Exppdf" runat="server">
        </ig:WebDocumentExporter>
        <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
    </div>
    <br />
    <br />
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nueva Lista" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Lista" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Lista" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
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
                    <ig:WebDataGrid ID="GridTipos" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" EnableAjax="false" StyleSetName="Office2007Blue" EnableDataViewState="True"  DataKeyFields="id" DataSourceID="Src_Tipos">
                        <Columns>
                            <ig:BoundDataField DataFieldName="proceso" Key="proceso">
                                <Header Text="Lista de Chequeo">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="descripcion" Key="descripcion">
                                <Header Text="Descripcion">
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
                    <asp:SqlDataSource ID="Src_Tipos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [proceso], [descripcion], [id],[Activo] FROM [Procesos] WHERE ([id_sucursal] = @id_sucursal)">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
     <div class="titular-into-form">
        <span class="texto-titulo-form">TIPOS DE DOCUMENTO ASOCIADO</span>
    </div>
    <br />
    <br />
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="BttNew" ToolTip="Nuevo Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="BttEdit" ToolTip="Editar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttDelete" ToolTip="Eliminar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
                </td>                
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttExc1" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="Bttpd1" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridTiposDoc" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_TiposDoc" DataKeyFields="id">
                        <Columns>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                <Header Text="Tipo Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="descripcion" Key="descripcion">
                                <Header Text="Descripcion">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="cantidad" Key="cantidad">
                                <Header Text="cantidad">
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
                    <asp:SqlDataSource ID="Src_TiposDoc" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [cantidad]
      ,[Documentos_Requeridos].[descripcion]
      ,[id_tipo_documento]
      ,[id_proceso]
      ,[Documentos_Requeridos].[id]
	  ,AdminTipoDocumento.nombre
  FROM [Documentos_Requeridos]
  inner join AdminTipoDocumento
  on [Documentos_Requeridos].[id_tipo_documento]=AdminTipoDocumento.id
  where [Documentos_Requeridos].[id_proceso]=@proceso
order by AdminTipoDocumento.nombre">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HiddenField1" DefaultValue="0" Name="proceso" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
        </table>
    </div>    

</asp:Content>



