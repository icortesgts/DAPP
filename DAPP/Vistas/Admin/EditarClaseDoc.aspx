<%@ Page Title="Edición Clase Documento" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarClaseDoc.aspx.vb" Inherits="DAPP.EditarClaseDoc" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>CLASE DOCUMENTO</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Clase Documento:*" Width="180px" />
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
                            </div>                      
            </div>
        </div>
    </div>
</asp:Content>


