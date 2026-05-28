<%@ Page Title="Edición Clase Documento" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarPerfil.aspx.vb" Inherits="DAPP.EditarPerfil" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles_form.css">


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>PERFIL DE USUARIO</h3>
                <ig:WebExcelExporter ID="ExpGrid" runat="server">
                </ig:WebExcelExporter>
                <ig:WebDocumentExporter ID="Exppdf" runat="server">
                </ig:WebDocumentExporter>               
                    <div class="form-horizontal fom-border">                        
                    <br />
                    <br />                                
                                
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNumDocumento" runat="server" Text="Perfil:*" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtTipoDocumento" CssClass="input-xlarge" Width="360px" />                                                                    
                        <asp:RequiredFieldValidator ID="RQ_TipoDocumento" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtTipoDocumento" ErrorMessage=" * El campo 'Tipo Documento' es obligatorio." Width="400px" />  
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNombre" runat="server" Text="Descripción:*" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtDescipcion" CssClass="input-xlarge" Width="360px" Height="56px" TextMode="MultiLine" />                                    
                        <asp:RequiredFieldValidator ID="RQ_Descripcion" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDescipcion" ErrorMessage=" * El campo 'Descripcion' es obligatorio." Width="400px" />
                    </div>
                    <div>
                                    
                            <b class="control-label">
                            <asp:Label ID="Label1" runat="server" Text="Activo:*" Width="180px" />
                        </b>                                
                                    
                        <asp:CheckBox ID="Chkactivo" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
                    </div>
                                
                    <br />
                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />                                  
                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                    <br />
                    <br />
                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                    <h3>PERMISOS ASOCIADOS</h3>
                    <div class="DivFormulario">
                        <table style="margin: auto; width: 100%;">
                            <tr>
                                <td style="text-align: center; width: 40px;">
                                    <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                                </td>
                                <td style="text-align: center; width: 40px;">
                                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                                </td>
                                 <td style="text-align: center; width: 40px;">&nbsp;
                                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
                                </td>
                                <td style="text-align: center; width: 40px;">&nbsp;
                                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                                </td>
                                <td style="text-align: center; width: 40px;">&nbsp;
                                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                                </td>                                
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center;" colspan="8">
                                    <ig:WebDataGrid ID="GridTipos" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Tipos" DataKeyFields="id">
                                        <Columns>
                                            <ig:BoundDataField DataFieldName="Alias" Key="Alias">
                                                <Header Text="Funcionalidad">
                                                </Header>
                                            </ig:BoundDataField>
                                            <ig:BoundCheckBoxField DataFieldName="Adicion" Key="Adicion">
                                                <Header Text="Adición">
                                                </Header>
                                            </ig:BoundCheckBoxField>
                                            <ig:BoundCheckBoxField DataFieldName="Edicion" Key="Edicion">
                                                <Header Text="Edición">
                                                </Header>
                                            </ig:BoundCheckBoxField>
                                            <ig:BoundCheckBoxField DataFieldName="Consulta" Key="Consulta">
                                                <Header Text="Eliminación">
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
                                    <asp:SqlDataSource ID="Src_Tipos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Perfil_Menu.id
      ,Perfil_Menu.id_menu
      ,Perfil_Menu.id_perfil
      ,Perfil_Menu.Adicion
      ,Perfil_Menu.Edicion
      ,Perfil_Menu.Consulta
	  ,Admin_menu.label
	  ,Admin_AliasMenu.Alias
  FROM Perfil_Menu
  inner join Admin_menu
  on Perfil_Menu.id_menu=Admin_menu.id
  inner join Admin_AliasMenu
  on Admin_AliasMenu.id_menu=Admin_menu.id and Admin_AliasMenu.id_cliente=@cliente
  where Perfil_Menu.id_perfil=@perfil
 order by Admin_AliasMenu.Alias">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                                            <asp:QueryStringParameter DefaultValue="0" Name="perfil" QueryStringField="id_perfil" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>                      
            </div>
        </div>
    </div>
</asp:Content>


