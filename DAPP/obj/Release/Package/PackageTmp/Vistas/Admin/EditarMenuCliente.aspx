<%@ Page Title="Edita Menú" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarMenuCliente.aspx.vb" Inherits="DAPP.EditarMenuCliente" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">   

   
    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>Menú Aplicación</h3>               
                    <div class="form-horizontal fom-border" >                        
                                <br />
                                <br />
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Opción Menú:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtLabel" CssClass="input-xlarge" Width="320px" ReadOnly="true"  />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtLabel" ErrorMessage=" * El campo 'Opción Menú' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Alías:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtAlias" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtAlias" ErrorMessage=" * El campo 'Alías' es obligatorio." Width="400px" />
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
    <asp:HiddenField ID="Hdd_id_area" runat="server" Value="0" />
</asp:Content>


