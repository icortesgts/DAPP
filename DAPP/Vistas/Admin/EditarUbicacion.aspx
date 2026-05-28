<%@ Page Title="Edición Ubicaciones" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarUbicacion.aspx.vb" Inherits="DAPP.EditarUbicacion" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">   

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        function ActualizarSeleccionUbicacion(id_ubicacion, nombre)
        {
            document.getElementById("<%=Hdd_id_ubicacion.ClientID%>").value = id_ubicacion;
            document.getElementById("<%=txtUbicacion.ClientID%>").value = nombre;
            document.getElementById("<%=txtUbicacion.ClientID%>").Text = nombre;
        }       
        
    </script>

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>UBICACION</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Nombre:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNombre" ErrorMessage=" * El campo 'Nombre' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDescripcion" runat="server" Text="Descripción:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDescripcion" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Descripcion" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDescripcion" ErrorMessage=" * El campo 'Descripcion' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Archivo Vínculado:" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtArchivo" runat="server" Font-Overline="False" Height="22px" Width="320px" CssClass="newsInputD">
                                        <asp:ListItem>Archivo de Gestión</asp:ListItem>
                                        <asp:ListItem>Archivo Central</asp:ListItem>
                                        <asp:ListItem>Archivo Histórico</asp:ListItem>
                                    </asp:DropDownList>                                  
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Tipo Ubicación:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txttipo" runat="server" Font-Overline="False" Height="22px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipoU" DataTextField="nombre" DataValueField="id" AutoPostBack="True">
                                    </asp:DropDownList>                                  
                                    
                                    <asp:SqlDataSource ID="SqlTipoU" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [nombre] FROM [AdminTipoUbicacion] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [nombre]">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" Type="Int64" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label6" runat="server" Text="Capacidad:*" Width="180px" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtcapacidad" Width="180px" runat="server" CssClass="input-xlarge" NullValue="0" NullText="0" Nullable="False" Enabled="false"></ig:WebNumericEditor>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtcapacidad" ErrorMessage=" * El campo 'Capcidad' es obligatorio." Width="400px" />                                                                    
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbUbicacion" runat="server" Text="Ubicacion:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtUbicacion" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:Image ID="R1" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttBuscarUbicacion" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" />  
                                    <asp:Image ID="R2" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttQuitarSeleccion" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" />                                   
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
    <asp:HiddenField ID="Hdd_id_ubicacion" runat="server" Value="0" />
</asp:Content>


