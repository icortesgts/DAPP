<%@ Page Title="Edita Menú" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarMenu.aspx.vb" Inherits="DAPP.EditarMenu" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

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
                                        <asp:Label ID="lbNombre" runat="server" Text="Ítem:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtItem" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtItem" ErrorMessage=" * El campo 'Ítem' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text="Link:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtLink" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtLink" ErrorMessage=" * El campo 'Link' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Label:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtLabel" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtLabel" ErrorMessage=" * El campo 'Label' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Nivel:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtNivel" TextMode="Number"  CssClass="input-xlarge" Width="320px" ReadOnly="true"  />                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNivel" ErrorMessage=" * El campo 'Nivel' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbArea" runat="server" Text="Asociado a:" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtMenu" runat="server" CssClass="input-xlarge" Width="320px" AutoPostBack="True" DataSourceID="SqlMenu" DataTextField="label" DataValueField="id">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlMenu" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="Select 0 as id, ' Ninguno'as Label
union all
SELECT [id], [label] FROM [Admin_Menu] WHERE ([id] &lt;&gt; @id) ORDER BY [label]">
                                        <SelectParameters>
                                            <asp:QueryStringParameter DefaultValue="-1" Name="id" QueryStringField="id_menu" Type="Int64" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
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


