<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarArea.aspx.vb" Inherits="DAPP.EditarArea" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">   

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        function ActualizarSeleccionArea(id_area, nombre)
        {
            document.getElementById("<%=Hdd_id_area.ClientID%>").value = id_area;
            document.getElementById("<%=txtArea.ClientID%>").value = nombre;
            document.getElementById("<%=txtArea.ClientID%>").Text = nombre;
        }       
        
    </script>

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>AREA</h3>               
                    <div class="form-horizontal fom-border" >                        
                                <br />
                                <br />
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Nombre:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNombre" ErrorMessage=" * El campo 'Nombre' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDescripcion" runat="server" Text="Descripción:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDescripcion" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Descripcion" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDescripcion" ErrorMessage=" * El campo 'Descripcion' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text="Prefijo Correspondencia:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtprefijo" CssClass="input-xlarge" Width="180px" />                                    
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Código:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtCodigo" CssClass="input-xlarge" Width="180px" />                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Color:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtcolor" CssClass="input-xlarge" Width="180px" TextMode="Color" />                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbArea" runat="server" Text="Area:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtArea" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:Image ID="R1" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttBuscarArea" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" />  
                                    <asp:Image ID="R2" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttQuitarSeleccion" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" />                                   
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
    <asp:HiddenField ID="Hdd_id_area" runat="server" Value="0" />
</asp:Content>


