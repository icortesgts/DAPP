<%@ Page Title="Usuarios" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Usuarios.aspx.vb" Inherits="DAPP.Page_Usuarios" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE USUARIOS </span>
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
                            <ig:BoundDataField DataFieldName="Usuario" Key="usuario" CssClass="HeaderCaptionClass">
                                <Header Text="Usuario">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Nombre" Key="nombre" CssClass="HeaderCaptionClass">
                                <Header Text="Nombre">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Perfil" Key="perfil" CssClass="HeaderCaptionClass">
                                <Header Text="Perfil">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Email" Key="email" CssClass="HeaderCaptionClass">
                                <Header Text="email">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundCheckBoxField DataFieldName="Bloqueado" Key="bloqueado" CssClass="HeaderCaptionClass">
                                <Header Text="Bloqueado">
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
                    <asp:SqlDataSource ID="Src_Usuarios" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [usuario], AdminPerfiles.nombre as perfil, [Usuarios].[nombre], [email], [bloqueado], [Usuarios].[id] FROM [Usuarios] INNER JOIN AdminPerfiles ON [Usuarios].id_perfil = AdminPerfiles.Id inner join usuario_cliente on usuario_cliente.id_usuario=usuarios.id and usuario_cliente.id_cliente=@cliente and  [Usuarios].id_perfil<>1 and  [Usuarios].todos <>1
union all
SELECT DISTINCT [usuario], AdminPerfiles.nombre as perfil, [Usuarios].[nombre], [email], [bloqueado], [Usuarios].[id] FROM [Usuarios] INNER JOIN AdminPerfiles ON [Usuarios].id_perfil = AdminPerfiles.Id where todos=1
and 1=(select count(*) from usuarios where id=@usuario and id_perfil=1)
ORDER BY [Usuario]">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                            <asp:ControlParameter ControlID="Hdd_id_usuario" DefaultValue="1" Name="usuario" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="Hdd_id_usuario" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>



