<%@ Page Title="Areas" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Areas.aspx.vb" Inherits="DAPP.Page_Areas" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css"> 
      
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE AREAS </span>
         <ig:WebExcelExporter ID="ExpGrid" runat="server">
        </ig:WebExcelExporter>
        <ig:WebDocumentExporter ID="Exppdf" runat="server">
        </ig:WebDocumentExporter>
    </div>
    <br />
    <br />
    <div class="DivFormulario">
        <div>
            &nbsp;
            <asp:Button ID="bttSeleccionar" runat="server" Text="Ver Areas Configuradas" class="btn btn-primary" />
            &nbsp;
            &nbsp;
            <br />
            <br />
        </div>
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nueva Area" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Area" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Area" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de Realizar esta Operación?');" />
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
                    <ig:WebDataGrid ID="GridAreas" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Areas" DataKeyFields="id" EnableAjax="False">
                        <Columns>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre" CssClass="HeaderCaptionClass">
                                <Header Text="Area">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="descripcion" Key="descripcion" CssClass="HeaderCaptionClass">
                                <Header Text="Descripcion">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Area_Asociada" Key="Area_Asociada" CssClass="HeaderCaptionClass">
                                <Header Text="Area Asociada">
                                </Header>
                            </ig:BoundDataField>
                            <ig:TemplateDataField Key="Color" >
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" TextMode="Color" Width="100%" readonly="true" enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                <Header Text="Color">
                                </Header>
                            </ig:TemplateDataField>
                            
                            <ig:BoundDataField DataFieldName="codigo" Key="codigo" CssClass="HeaderCaptionClass">
                                <Header Text="Código">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundCheckBoxField DataFieldName="Activo" Key="Activo">
                                <Header Text="Activo">
                                </Header>
                            </ig:BoundCheckBoxField>                                                             
                            <ig:BoundDataField DataFieldName="color" Key="color" CssClass="HeaderCaptionClass" Hidden="true" >
                                <Header Text="Color">
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
                        <Templates>
                            <ig:ItemTemplate ID="GridAreasTemplate1" runat="server" TemplateID="TextColor">
                            </ig:ItemTemplate>
                        </Templates>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="Src_Areas" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT Areas.nombre,Areas.descripcion,isnull(Area_Asociada.nombre,'') as Area_Asociada,  Areas.Activo, Areas.id, Areas.color, Areas.codigo FROM Areas LEFT JOIN Areas as Area_Asociada ON Areas.id_area = Area_Asociada.id WHERE Areas.id_sucursal = @sucursal ORDER BY Areas.nombre">
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



