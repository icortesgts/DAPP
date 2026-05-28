<%@ Page Title="TIPO DOCUMENTO ASOCIADO A LISTAS DE CHEQUEO" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarTipoD.aspx.vb" Inherits="DAPP.EditarTipoD" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">





    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>TIPO DOCUMENTO ASOCIADO A LISTAS DE CHEQUEO</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Tipo Documento:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtClase" runat="server" Font-Overline="False" Height="21px" Width="362px" CssClass="newsInputD" DataSourceID="SqlClase" DataTextField="nombre" DataValueField="id">
                                     </asp:DropDownList>                                  
                                    <asp:SqlDataSource ID="SqlClase" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [nombre], [id] FROM [AdminTipoDocumento] WHERE ([id_sucursal] = @id_sucursal and [id] = (select id_tipo_documento from Documentos_Requeridos where id=@proceso))
union all
SELECT [nombre], [id] FROM [AdminTipoDocumento] WHERE ([id_sucursal] = @id_sucursal and [id] not in (select id_tipo_documento from Documentos_Requeridos where id_proceso=@lista)) ORDER BY [nombre]">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" Type="Int64" />
                                            <asp:QueryStringParameter DefaultValue="0" Name="proceso" QueryStringField="id_tipod" />
                                            <asp:QueryStringParameter DefaultValue="0" Name="lista" QueryStringField="id_lista" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
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
                                        <asp:Label ID="Label2" runat="server" Text="Cantidad:*" Width="180px" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtCantidad" Width="180px" runat="server" CssClass="input-xlarge" NullValue="1" NullText="1" Nullable="False" BorderStyle="None" DataMode="Int" MinValue="1" ToolTip="Versión">
                                    </ig:WebNumericEditor>
                                    <asp:RequiredFieldValidator ID="RQ_Version" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCantidad" ErrorMessage=" * El campo 'Cantidad' es obligatorio." Width="400px" />
                                </div>                                
                                <br />
                                <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />                                  
                                <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                                <br />
                                <br />
                                <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                            </div>                      
            </div>
        </div>
    </div>
</asp:Content>


