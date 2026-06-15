<%@ Page Title="Prestamo" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarPrestamo.aspx.vb" Inherits="DAPP.Editarprsetamo" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">

    <script type="text/javascript" src="Resources/dynamsoft.webtwain.initiate.js"> </script>
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.config.js"> </script>

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        function ActualizarSeleccionDocumento(id_documento, nombre) {
            document.getElementById("<%=Hdd_id_documento.ClientID%>").value = id_documento;
            document.getElementById("<%=TxtDocumento.ClientID%>").value = nombre;
            document.getElementById("<%=TxtDocumento.ClientID%>").Text = nombre;
        }

        function ActualizarSeleccionTercero(id_tercero, documento, nombre) {
            document.getElementById("<%=Hdd_id_solicitante.ClientID%>").value = id_tercero;
            document.getElementById("<%=txtSolicitante.ClientID%>").value = nombre;
            document.getElementById("<%=txtSolicitante.ClientID%>").Text = nombre;
        }
        function ActualizarSeleccionResponsable(id_tercero, documento, nombre) {
            document.getElementById("<%=Hdd_id_responsable.ClientID%>").value = id_tercero;
            document.getElementById("<%=txtResponsable.ClientID%>").value = nombre;
            document.getElementById("<%=txtResponsable.ClientID%>").Text = nombre;
        }
        function ActualizarSeleccionUbicacion(id_ubicacion, nombre) {
            document.getElementById("<%=Hdd_id_ubicacion.ClientID%>").value = id_ubicacion;
            document.getElementById("<%=TxtUbicacion.ClientID%>").value = nombre;
            document.getElementById("<%=TxtUbicacion.ClientID%>").Text = nombre;
        }

      
    </script>

   

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>PRESTAMO DE DOCUMENTOS</h3>
                
                <div class="form-horizontal fom-border">
                    <br />
                    <br />
                    <div class="cabezote-form">
                        <span>DOCUMENTOS SOLICITADOS</span>
                    </div>
                    <br />
                    <br />
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label8" runat="server" Text="Documento:" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="TxtDocumento" CssClass="input-xlarge" Width="320px"  /> 
                        <asp:Image ID="R1" runat="server" ImageUrl="~/Images/Rec.png" />                                             
                        <asp:ImageButton ID="BttDocumento" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" Enabled="False" />
                        <asp:Image ID="R2" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttAgregarDoc" runat="server" ImageUrl="~/Images/icoAdd.png" ImageAlign="Middle" Enabled="False" />
                        <asp:Image ID="R3" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttEliminarDoc" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" Enabled="False" />
                    </div>
                    <div>
                        <br />
                        <br />
                        <asp:ListBox ID="lstDocumentos" CssClass="input-xlarge" runat="server" Width="400px"></asp:ListBox>
                        <asp:Button ID="BttDetalle" runat="server" Text="Detalle Documentos"  CssClass="btn btn-primary" ValidationGroup="Errores" />
                    </div>
                    <div>
                         <br />
                         <br />
                         <b class="control-label">
                            <asp:Label ID="Label1" runat="server" Text="Carpeta:" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="TxtUbicacion" CssClass="input-xlarge" Width="320px"  /> 
                        <asp:ImageButton ID="BttUbicacion" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="height: 16px" Enabled="False" />
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label9" runat="server" Text="Solicitante: *" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtSolicitante" CssClass="input-xlarge" Width="320px"  /> 
                        <asp:ImageButton ID="BttSolicitante" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" Enabled="False" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtSolicitante" ErrorMessage=" * El campo 'Solicitante' es obligatorio." Width="400px" />                   
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label10" runat="server" Text="Responsable:*" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtResponsable" CssClass="input-xlarge" Width="320px"  /> 
                        <asp:ImageButton ID="BttResponsable" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="height: 16px; width: 16px;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtResponsable" ErrorMessage=" * El campo 'Responsable' es obligatorio." Width="400px" />                   
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label2" runat="server" Text="Duración (Días):*" Width="180px"  />
                        </b>
                        <asp:TextBox runat="server" ID="TxtDuracion" CssClass="input-xlarge" Width="320px" TextMode="Number" ReadOnly="True" >1</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="TxtDuracion" ErrorMessage=" * El campo 'Duracion' es obligatorio." Width="400px" />                      
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNumDocumento" runat="server" Text="Estado:*" Width="180px" />
                        </b> 
                        <asp:DropDownList ID="txtEstado" runat="server" CssClass="input-xlarge" Width="320px" AutoPostBack="True" >
                            <asp:ListItem>Solicitado</asp:ListItem>
                            <asp:ListItem>Prestado</asp:ListItem>
                            <asp:ListItem>Devuelto</asp:ListItem>
                            <asp:ListItem>Negado</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label3" runat="server" Text="Motivo:" Width="180px" />
                        </b> 
                        <asp:TextBox runat="server" ID="Txtmotivo" CssClass="input-xlarge" Width="320px" TextMode="MultiLine" Height="63px" ReadOnly="True" ></asp:TextBox>
                    </div>
                    <div>
                        <table style="width:100%;">
                            <tr>
                                <td style="width:50%;">
                                    <b class="control-label">
                                        <asp:Label ID="Label7" runat="server" Text="Fecha Solicitud: *" Width="180px"  />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="input-xlarge" Width="180px" textmode="Date" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaInicio" ErrorMessage=" * El campo 'Fecha Solicitud' es obligatorio." Width="400px" />  
                                </td>
                                <td style="width:50%;">
                                    <b class="control-label">
                                        <asp:Label ID="Label11" runat="server" Text="Fecha Prestamo:" Width="180px"  />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtFPrestamo" CssClass="input-xlarge"  Width="180px" textmode="Date"  /> 
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width:50%;">
                                    <b class="control-label">
                                        <asp:Label ID="Label12" runat="server" Text="Fecha Estimada Devolución:" Width="180px"  />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtFEdevolucion" CssClass="input-xlarge" Width="180px" textmode="Date"/> 
                                    
                                </td>
                                <td style="width:50%;">
                                    <b class="control-label">
                                        <asp:Label ID="Label13" runat="server" Text="Fecha Devolución:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtFdevolucion" CssClass="input-xlarge" Width="180px" textmode="Date" /> 
                                </td>
                            </tr>
                        </table>                                  
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label4" runat="server" Text="Observaciones Prestamo:" Width="180px" />
                        </b> 
                        <asp:TextBox runat="server" ID="TxtObservaciones" CssClass="input-xlarge" Width="320px" TextMode="MultiLine" Height="63px" ></asp:TextBox>
                    </div>
                    <br />
                    <br />
                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />
                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                    <br />
                    <br />
                    
                    <br />
                    <br />                                                       
                    
                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                    
                </div>
            </div>
        </div>

       
    
    </div>
    
    <asp:HiddenField ID="Hdd_id_documento" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_ubicacion" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_solicitante" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_responsable" runat="server" Value="0" />
</asp:Content>


