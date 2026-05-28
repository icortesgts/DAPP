<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarContrato.aspx.vb" Inherits="DAPP.EditarContrato" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>CLIENTE</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Número Cotnrato:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtNro" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNro" ErrorMessage=" * El campo 'Número de Contrato' es obligatorio." Width="400px" />
                                </div>                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTipoDocumento" runat="server" Text="Fecha Inicio:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtFecha" CssClass="input-xlarge" Width="180px" TextMode="Date"  />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFecha" ErrorMessage=" * El campo 'Fecha Inicio' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Fecha Fin:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtFechaFin" CssClass="input-xlarge" Width="180px" TextMode="Date"  />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="TxtFechaFin" ErrorMessage=" * El campo 'Fecha Fin' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Tipo Pago:*" Width="180px" />
                                    </b>
                                    
                                    
                                    <asp:DropDownList ID="TxtTipo" runat="server" Width="320px">
                                        <asp:ListItem>Mensual</asp:ListItem>
                                        <asp:ListItem>Anual</asp:ListItem>
                                        <asp:ListItem>Único PAgo</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Estado:*" Width="180px" />
                                    </b>
                                    
                                    
                                    <asp:DropDownList ID="txtEstado" runat="server" Width="320px">
                                        <asp:ListItem>Activo</asp:ListItem>
                                        <asp:ListItem>En Mora</asp:ListItem>
                                        <asp:ListItem>Caducado</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text="Licencia:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtLicencia" CssClass="input-xlarge" Width="320px" ReadOnly="True" />                                    
                                    
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


