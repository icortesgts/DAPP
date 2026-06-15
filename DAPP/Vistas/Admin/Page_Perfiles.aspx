<%@ Page Title="Usuarios" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Perfiles.aspx.vb" Inherits="DAPP.Page_PErfiles" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE PERFILES DE USUARIOS </span>
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
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Usuario" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Usuario" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Usuario" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
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
                    <ig:WebDataGrid ID="GridUsuarios" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Usuarios" DataKeyFields="id">
                        <Columns>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                <Header Text="Perfil">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Descripcion" Key="Descripcion">
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
                            </ig:Selection>
                            <ig:Sorting SortingMode="Single" Enabled="true">
                            </ig:Sorting>
                            <ig:Filtering>
                            </ig:Filtering>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="Src_Usuarios" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id],[nombre], [Descripcion],[Activo] 
FROM [AdminPerfiles] 
WHERE ([id_cliente] is null and (select count(*) from usuarios where usuario=@usuario and id_perfil=1)=1) 
union all
SELECT [id],[nombre], [Descripcion], [Activo] 
FROM [AdminPerfiles] 
WHERE ([id_cliente] = @id_cliente) 
ORDER BY [nombre]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Hdd_id_usuario" Name="usuario" PropertyName="Value" />
                            <asp:SessionParameter DefaultValue="0" Name="id_cliente" SessionField="id_cliente" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="Hdd_id_usuario" runat="server" Value="" />
    </div>

</asp:Content>



