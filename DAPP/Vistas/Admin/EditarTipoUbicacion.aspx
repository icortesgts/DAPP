<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarTipoUbicacion.aspx.vb" Inherits="DAPP.EditarTipoUbicacion" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>TIPO UBICACIÓN</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Tipo Ubicación:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTipoDocumento" CssClass="input-xlarge" Width="320px" />                                                                    
                                    <asp:RequiredFieldValidator ID="RQ_TipoDocumento" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtTipoDocumento" ErrorMessage=" * El campo 'Tipo Ubicación' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTipoDocumento" runat="server" Text="Clase:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtClase" runat="server" Font-Overline="False" Height="22px" Width="320px" CssClass="newsInputD">
                                        <asp:ListItem>A-Z CARTA</asp:ListItem>
                                        <asp:ListItem>A-Z OFICIO</asp:ListItem>
                                        <asp:ListItem>CAJA</asp:ListItem>
                                        <asp:ListItem>CARPETA</asp:ListItem>
                                        <asp:ListItem>CONTENEDOR</asp:ListItem>
                                        <asp:ListItem>EDIFICACIÓN</asp:ListItem>
                                        <asp:ListItem>LEGAJO</asp:ListItem>
                                    </asp:DropDownList>                                  
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Profundidad (cm):*" Width="180px" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtlargo" Width="180px" runat="server" CssClass="input-xlarge" NullValue="12" NullText="12" Nullable="False"></ig:WebNumericEditor>                                                                  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtlargo" ErrorMessage=" * El campo 'Largo' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Ancho (cm):*" Width="180px" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtAncho" Width="180px" runat="server" CssClass="input-xlarge" NullValue="22" NullText="22" Nullable="False"></ig:WebNumericEditor>                                                                  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtAncho" ErrorMessage=" * El campo 'Ancho' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text="Alto (cm):*" Width="180px" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtAlto" Width="180px" runat="server" CssClass="input-xlarge" NullValue="28" NullText="28" Nullable="False"></ig:WebNumericEditor>                                                                  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtAlto" ErrorMessage=" * El campo 'Alto' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Contiene:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtcontiene" runat="server" Font-Overline="False" Height="22px" Width="360px" CssClass="newsInputD" DataSourceID="SqlTipoU" DataTextField="nombre" DataValueField="id">
                                    </asp:DropDownList>                                  
                                    
                                    <asp:SqlDataSource ID="SqlTipoU" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT 0 as [id], ' Documentos' as [nombre] union all SELECT [id], [nombre] FROM [AdminTipoUbicacion] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [nombre]">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" Type="Int64" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                         <asp:Label ID="Label6" runat="server" Text="Capacidad:*" Width="180px" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtcapacidad" Width="180px" runat="server" CssClass="input-xlarge" NullValue="0" NullText="0" Nullable="False"></ig:WebNumericEditor>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtcapacidad" ErrorMessage=" * El campo 'Capcidad' es obligatorio." Width="400px" />   
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                         <asp:Label ID="Lblcalculo" runat="server" Text="Método de Cálculo de Capacidad" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtmetodo" runat="server" Font-Overline="False" Height="22px" Width="360px" CssClass="newsInputD" ToolTip="Método de Cálculo de Capacidad basado en Profundidad, Ancho o Alto">
                                        <asp:ListItem>Profundidad</asp:ListItem>
                                        <asp:ListItem>Ancho</asp:ListItem>
                                        <asp:ListItem>Alto</asp:ListItem>
                                    </asp:DropDownList>      
                                </div>
                                
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Activo:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="Chkactivo" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
                                </div> 
                                <br />
                                <br />
                                <asp:Button ID="BttCapacidad" runat="server" Text="Calcular" OnClick="BttCapacidad_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />
                                <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />                                  
                                <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary" />
                                <br />
                                <br />
                                <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                            </div>                      
            </div>
        </div>
    </div>
</asp:Content>


