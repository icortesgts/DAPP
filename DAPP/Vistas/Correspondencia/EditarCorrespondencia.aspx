<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarCorrespondencia.aspx.vb" Inherits="DAPP.EditarCorrespondencia" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">

    <script type="text/javascript" src="Resources/dynamsoft.webtwain.initiate.js"> </script>
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.config.js"> </script>

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

       function ActualizarSeleccionTercero(id_tercero, documento, nombre)
        {
            document.getElementById("<%=Hdd_id_tercero.ClientID%>").value = id_tercero;
            document.getElementById("<%=txtDocumentoTercero.ClientID%>").value = documento;
            document.getElementById("<%=txtTercero.ClientID%>").value = nombre;
            document.getElementById("<%=txtDocumentoTercero.ClientID%>").Text = documento;
            document.getElementById("<%=txtTercero.ClientID%>").Text = nombre;
        }

        function ActualizarSeleccionDocumento(id_documento, nombre) {
            document.getElementById("<%=Hdd_id_documento.ClientID%>").value = id_documento;
            document.getElementById("<%=txtDocSeleccionado.ClientID%>").value = nombre;
            document.getElementById("<%=txtDocSeleccionado.ClientID%>").Text = nombre;
        }
      
    </script>

   

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>CORRESPONDENCIA</h3>
                <div class="form-horizontal fom-border">
                    <br />
                    <br />
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNumeroRadicado" runat="server" Text="Numero de Radicado:" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtNumeroRadicado" CssClass="input-xlarge" Width="320px" ReadOnly="true"  /> 
                    </div>
                    <div>

                        <table>
                            <tr>
                                <td>
                                       <b class="control-label">
                                        <asp:Label ID="lbFechaDocumento" runat="server" Text="Fecha Documento:*" Width="180px" />
                                       </b>  
                                </td>
                                <td>
                                        <ig:WebDatePicker ID="txtFechaDocumento" runat="server" DisplayModeFormat="d" Nullable="False" CssClass="input-xlarge"  Width="180px" Height="20px" SpinOnlyOneField="True" DropDownCalendarID="WMFechaDocumento">
                                        </ig:WebDatePicker> 
                                        <ig:WebMonthCalendar ID="WMFechaDocumento" runat="server">
                                        </ig:WebMonthCalendar>                         
                                </td>
                                <td>
                                        <asp:RequiredFieldValidator ID="RQ_FechaDocumento" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaDocumento" ErrorMessage=" * El campo 'Fecha Documento' es obligatorio." Width="400px" />                                      
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbDepartamento" runat="server" Text="Departamento:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtDepartamento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlDpto" DataTextField="departamento" DataValueField="departamento" AutoPostBack="True">
                        </asp:DropDownList>                                  
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbCiudad" runat="server" Text="Ciudad:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtCiudad" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlCiudad" DataTextField="ciudad" DataValueField="ciudad">
                        </asp:DropDownList>                                  
                    </div>
                    
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbTipoIngreso" runat="server" Text="Tipo Ingreso:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtTipoIngreso" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD">
                            <asp:ListItem>Interna</asp:ListItem>
                            <asp:ListItem>Externa</asp:ListItem>
                        </asp:DropDownList>                                                                             
                    </div>


                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbTipoCorrespondencia" runat="server" Text="Tipo Correspondencia: *" Width="180px" />
                        </b>
                       <asp:DropDownList ID="txtTipoCorrespondencia" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SRC_TipoCorrespondencia" DataTextField="tipo" DataValueField="tipo">
                        </asp:DropDownList>  
                    </div>

                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento: *" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtTipoDocumento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipoDoc" DataTextField="nombre" DataValueField="id">
                        </asp:DropDownList>  
                    </div>
                     <div>
                        <b class="control-label">
                            <asp:Label ID="lbRemitente" runat="server" Text="Remitente: *" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtRemitente" CssClass="input-xlarge" Width="320px" ReadOnly="true"  /> 
                         <asp:RequiredFieldValidator ID="RQ_Remitente" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtRemitente" ErrorMessage=" * El campo 'Remitente' es obligatorio." Width="400px" />
                    </div>
                     <div>
                        <b class="control-label">
                            <asp:Label ID="lbDependenciaRemitente" runat="server" Text="Dependencia Remitente: *" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtDependenciaRemitente" CssClass="input-xlarge" Width="320px" ReadOnly="true"  /> 
                         <asp:RequiredFieldValidator ID="RQ_DependenciaRemitente" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDependenciaRemitente" ErrorMessage=" * El campo 'Dependencia del Remitente' es obligatorio." Width="400px" />
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbAsunto" runat="server" Text="Asunto: *" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtAsunto" CssClass="input-xlarge" Width="320px" ReadOnly="true"  /> 
                        <asp:RequiredFieldValidator ID="RQ_Asunto" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtAsunto" ErrorMessage=" * El campo 'Asunto' es obligatorio." Width="400px" />
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbSticker" runat="server" Text="Sticker: *" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtSticker" CssClass="input-xlarge" Width="320px" ReadOnly="true"  /> 
                        <asp:RequiredFieldValidator ID="RQ_Sticker" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtSticker" ErrorMessage=" * El campo 'Sticker' es obligatorio." Width="400px" />
                    </div>
                     
                     <div>
                        <b class="control-label">
                            <asp:Label ID="lbGuia" runat="server" Text="Guia: *" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtGuia" CssClass="input-xlarge" Width="320px" ReadOnly="true"  /> 
                        <asp:RequiredFieldValidator ID="RQ_Guia" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtGuia" ErrorMessage=" * El campo 'Guia' es obligatorio." Width="400px" />
                    </div>
                     <div>
                        <b class="control-label">
                            <asp:Label ID="lbObservaciones" runat="server" Text="Observaciones: *" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtObservaciones" CssClass="input-xlarge" Width="320px" Height="66px" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ID="RQ_Observaciones" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtObservaciones" ErrorMessage=" * El campo 'Observaciones' es obligatorio." Width="400px" />
                    </div>

                    <div>
                        <br />
                        <br />
                    </div>

                    <%--DOCUMENTOS--%>

                    <div class="cabezote-form">
                        <span>DOCUMENTOS</span>
                    </div>
                    <div>
                        <p>&nbsp;</p>
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbDOcumentos" runat="server" Text="Documento:" Width="150px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtDocSeleccionado" Width="180px" CssClass="input-xlarge" ReadOnly="true" ViewStateMode="Enabled"></asp:TextBox>
                        <asp:Image ID="RD1" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:Image ID="RD2" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttBuscarDocumento" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="width: 16px" />
                        <asp:Image ID="RD3" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttAgregarDocumento" runat="server" ImageUrl="~/Images/icoAdd.png" ImageAlign="Middle" />
                        <asp:Image ID="RD4" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttEliminarDocumento" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" />
                    </div>
                    <div>
                        <br />
                        <br />
                        <asp:ListBox ID="lstDocumentos" CssClass="input-xlarge" runat="server" Width="400px"></asp:ListBox>                       
                    </div>

                    <%--DOCUMENTOS--%>

                    <div>
                        <br />
                        <br />
                    </div>
                    
                    <%--TERCEROS--%>

                    <div class="cabezote-form">
                        <span>DESTINATARIOS</span>
                    </div>
                    <div>
                        <p>&nbsp;</p>
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbDoc" runat="server" Text="Documento Tercero:" Width="150px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtDocumentoTercero" Width="180px" CssClass="input-xlarge" ReadOnly="true" ViewStateMode="Enabled"></asp:TextBox>
                        <asp:Image ID="R4" runat="server" ImageUrl="~/Images/Rec.png" />
                        <b class="control-label">
                            <asp:Label ID="lbTercero" runat="server" Text="Tercero:" Width="80px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtTercero" CssClass="input-xlarge" Width="180px" ReadOnly="true" ViewStateMode="Enabled" />
                        <asp:Image ID="R5" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttBuscarTercero" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="width: 16px" />
                        <asp:Image ID="R6" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttAgregarTercero" runat="server" ImageUrl="~/Images/icoAdd.png" ImageAlign="Middle" />
                        <asp:Image ID="R7" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttEliminarTercero" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" />
                    </div>
                    <div>
                        <br />
                        <br />
                        <asp:ListBox ID="lstTercerosDocumento" CssClass="input-xlarge" runat="server" Width="400px"></asp:ListBox>                       
                    </div>

                    <%--TERCEROS--%>

                    <br />
                    <br />
                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />
                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                    <br />
                    <br />
                    <br />                                                       
                    
                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                </div>
            </div>
        </div>           
    </div>
    <asp:SqlDataSource ID="SqlDpto" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [departamento] FROM [Admin_Ciudades] ORDER BY [departamento]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SRC_TipoCorrespondencia" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [tipo] FROM [AdminTipoCorrespondencia] ORDER BY [tipo]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCiudad" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [ciudad] FROM [Admin_Ciudades] WHERE ([departamento] = @departamento) ORDER BY [ciudad]">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtDepartamento" Name="departamento" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTipoDoc" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [nombre] FROM [AdminTipoDocumento] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [nombre]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="Hdd_id_documento" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_tercero" runat="server" Value="0" />
</asp:Content>


