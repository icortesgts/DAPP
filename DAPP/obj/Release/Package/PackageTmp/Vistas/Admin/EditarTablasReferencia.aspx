<%@ Page Title="Edición Tipos Documentales" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarTablasReferencia.aspx.vb" Inherits="DAPP.EditarTablasReferencia" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>SERIES Y TIPOS DOCUMENTALES</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Código:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="Txtcodigo" CssClass="input-xlarge" Width="180px" />          
                                    <asp:RequiredFieldValidator ID="RQ_Codigo" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCodigo" ErrorMessage=" * El campo 'Código' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento:*" Width="180px" />
                                    </b>                                    
                                    <asp:DropDownList ID="txtTipo" runat="server" CssClass="input-xlarge" Width="320px" DataSourceID="SqlTipo" DataTextField="nombre" DataValueField="id">
                                    </asp:DropDownList>                                    
                                    <asp:SqlDataSource ID="SqlTipo" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [nombre] FROM [AdminTipoDocumento] WHERE (([id_sucursal] = @id_sucursal) AND ([Activo] = @Activo)) ORDER BY [nombre]">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" Type="Int64" />
                                            <asp:Parameter DefaultValue="true" Name="Activo" Type="Boolean" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Archivo de Gestión:*" Width="180px" ToolTip="Tiempo de Conservación es Archivo en Años" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtgestion" Width="90px" runat="server" CssClass="input-xlarge" NullValue="0" NullText="0" Nullable="False"></ig:WebNumericEditor> 
                                    <asp:DropDownList ID="txtTime" runat="server" CssClass="input-xlarge" Width="90px">
                                        <asp:ListItem>Años</asp:ListItem>
                                        <asp:ListItem>Meses</asp:ListItem>
                                    </asp:DropDownList>                                              
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtgestion" ErrorMessage=" * El campo 'Archivo de Gestión' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text="Archivo Central:*" Width="180px" ToolTip="Tiempo de Conservación es Archivo en Años" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtcentral" Width="90px" runat="server" CssClass="input-xlarge" NullValue="0" NullText="0" Nullable="False"></ig:WebNumericEditor> 
                                    <asp:DropDownList ID="txttime2" runat="server" CssClass="input-xlarge" Width="90px">
                                        <asp:ListItem>Años</asp:ListItem>
                                        <asp:ListItem>Meses</asp:ListItem>
                                    </asp:DropDownList>                                                                                                               
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtcentral" ErrorMessage=" * El campo 'Archivo Central' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Archivo Histórico:*" Width="180px" ToolTip="Tiempo de Conservación es Archivo en Años"  />
                                    </b>
                                    <ig:WebNumericEditor ID="txthistorico" Width="90px" runat="server" CssClass="input-xlarge" NullValue="0" NullText="0" Nullable="False"></ig:WebNumericEditor>   
                                    <asp:DropDownList ID="txttime3" runat="server" CssClass="input-xlarge" Width="90px">
                                        <asp:ListItem>Años</asp:ListItem>
                                        <asp:ListItem>Meses</asp:ListItem>
                                    </asp:DropDownList>                                                                                                             
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txthistorico" ErrorMessage=" * El campo 'Archivo Histórico' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Original:" Width="180px" />
                                    </b>
                                    <asp:CheckBox ID="ChkOriginal" runat="server" Text="" CssClass="input-xlarge" ToolTip="Si eldocumento el Original o Copia" />
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Disposición Final:" Width="180px" />
                                    </b>
                                    <asp:CheckBox ID="ChkCT" runat="server" Text="CT" CssClass="input-xlarge" ToolTip="Conservación Total" />
                                    <asp:CheckBox ID="ChkE" runat="server" Text="E" CssClass="input-xlarge"  ToolTip="Eliminación" />
                                    <asp:CheckBox ID="ChkD" runat="server" Text="D" CssClass="input-xlarge" ToolTip="Digitalización" />
                                    <asp:CheckBox ID="ChkM" runat="server" Text="M" CssClass="input-xlarge"  ToolTip="Microfilmación" />
                                    <asp:CheckBox ID="ChkS" runat="server" Text="S" CssClass="input-xlarge"  ToolTip="Selección" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTelefono" runat="server" Text="Procedimientos:" Width="180px" ToolTip="Referencia a los procedimientos de conservación" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtProcedimiento" CssClass="input-xlarge" Width="360px" TextMode="MultiLine" Height="61px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Procedimiento" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtProcedimiento" ErrorMessage=" * El campo 'Procedimientos' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <p>
                                            CONVENCIONES
                                            CT = Conservación Total       M = Microfilmación        D = Digitalización
                                            E  = Eliminación              S = Selección            
                                        </p>
                                    </b>
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


