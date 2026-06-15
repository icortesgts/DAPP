<%@ Page Title="Edición Clase Documento" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarPermiso.aspx.vb" Inherits="DAPP.EditarPermiso" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

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
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Funcionalidad:*" Width="180px" />
                                    </b>
                                   
                                    <asp:DropDownList ID="txtfuncionalidad" runat="server" CssClass="input-xlarge" Width="360px" DataSourceID="SqlMenu" DataTextField="Alias" DataValueField="id">
                                    </asp:DropDownList>
                                    
                                    <asp:SqlDataSource ID="SqlMenu" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Admin_menu.id
	  ,Admin_AliasMenu.Alias
  FROM Admin_menu
  inner join Admin_AliasMenu
  on Admin_AliasMenu.id_menu=Admin_menu.id and Admin_AliasMenu.id_cliente=@cliente
  where Admin_menu.id not in (select id_menu from perfil_menu where id_perfil=@perfil)
order by Admin_AliasMenu.Alias
">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                                            <asp:QueryStringParameter DefaultValue="0" Name="perfil" QueryStringField="id_perfil" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    
                                </div>
                                
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Adición:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="Chkactivo" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
                                </div>
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Edición:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="Chkedit" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
                                </div>
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Eliminación:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="Chkdel" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
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


