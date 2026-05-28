<%@ Page Title="Edición Tipo Terceros" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarTipo.aspx.vb" Inherits="DAPP.EditarTipo" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">
    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        
        function ActualizarSeleccionLista(id_documento, nombre) {
            document.getElementById("<%=Hdd_id_lista.ClientID%>").value = id_documento;
            document.getElementById("<%=TxtDocumento.ClientID%>").value = nombre;
            document.getElementById("<%=TxtDocumento.ClientID%>").Text = nombre;
        }
    </script>

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>TIPOS DE TERCEROS</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Tipo:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTipoDocumento" CssClass="input-xlarge" Width="360px" />                                                                    
                                    <asp:RequiredFieldValidator ID="RQ_TipoDocumento" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtTipoDocumento" ErrorMessage=" * El campo 'Tipo' es obligatorio." Width="400px" />  
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
                                        <asp:Label ID="Label2" runat="server" Text="Recibe Correspondencia Interna:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="chkinterno" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
                                </div>
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Tipo Proveedor:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="chkProveedor" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
                                </div>
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Activo:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="Chkactivo" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
                                </div>
                                <br />
                                <br />
                                <div class="cabezote-form">
                                    <span>LISTAS DE CHEQUEO ASOCIADAS</span>
                                </div>
                                <br />
                                <br />
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Lista:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtDocumento" CssClass="input-xlarge" Width="320px"  /> 
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Rec.png" />                                             
                                    <asp:ImageButton ID="BttDocumento" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="width: 16px; height: 16px;" />
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttAgregarDoc" runat="server" ImageUrl="~/Images/icoAdd.png" ImageAlign="Middle" />
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttEliminarDoc" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" />
                                </div>
                                <div>
                                    <br />
                                    <br />
                                    <asp:ListBox ID="lstlista" CssClass="input-xlarge" runat="server" Width="400px"></asp:ListBox>
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
    <asp:HiddenField ID="Hdd_id_lista" runat="server" Value="0" />
</asp:Content>


