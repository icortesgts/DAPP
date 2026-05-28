<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarTipoDoc.aspx.vb" Inherits="DAPP.EditarTipoDoc" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles_form.css">   


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>TIPO DOCUMENTO</h3>    
                <ig:WebExcelExporter ID="ExpGrid" runat="server">
                </ig:WebExcelExporter>
                <ig:WebDocumentExporter ID="Exppdf" runat="server">
                </ig:WebDocumentExporter>           
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Tipo Documento:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTipoDocumento" CssClass="input-xlarge" Width="360px" />                                                                    
                                    <asp:RequiredFieldValidator ID="RQ_TipoDocumento" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtTipoDocumento" ErrorMessage=" * El campo 'Tipo Documento' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Descripción:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDescipcion" CssClass="input-xlarge" Width="360px" Height="56px" TextMode="MultiLine" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Descripcion" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDescipcion" ErrorMessage=" * El campo 'Descripcion' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Clase:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtClase" runat="server" Font-Overline="False" Height="21px" Width="362px" CssClass="newsInputD" DataSourceID="SqlClase" DataTextField="ClaseDoc" DataValueField="id">
                                     </asp:DropDownList>                                  
                                    <asp:SqlDataSource ID="SqlClase" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [ClaseDoc] FROM [AdminClaseDocumento] WHERE (([Activo] = @Activo) AND ([id_cliente] = @id_cliente)) ORDER BY [ClaseDoc]">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="true" Name="Activo" Type="Boolean" />
                                            <asp:SessionParameter DefaultValue="0" Name="id_cliente" SessionField="id_cliente" Type="Int64" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
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
                                <br />
                                <br />
                                <div class="titular-into-form">
                                    <span class="texto-titulo-form">CAMPOS ADICIONALES</span>
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
                                                        <ig:BoundDataField DataFieldName="Campo" Key="Campo">
                                                            <Header Text="Campo">
                                                            </Header>
                                                        </ig:BoundDataField>                                                                      
                                                        <ig:BoundDataField DataFieldName="Descripcion" Key="Descripcion">
                                                            <Header Text="Descripcion">
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
                                                <asp:SqlDataSource ID="Src_DetTablas" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [Campo], [Descripcion] FROM [AdminCamposAdicionales] WHERE ([id_tipo] = @id_tipo) ORDER BY [Campo]" >
                                                    <SelectParameters>
                                                        <asp:QueryStringParameter DefaultValue="0" Name="id_tipo" QueryStringField="id_tipodoc" Type="Int64" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:HiddenField ID="HCliente" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>     
                    </div>                      
            </div>
        </div>
    </div>
</asp:Content>


