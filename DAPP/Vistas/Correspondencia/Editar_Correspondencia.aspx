<%@ Page Title="CORRESPONDENCIA" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Editar_Correspondencia.aspx.vb" Inherits="DAPP.Editar_Correspondencia" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">

    <script type="text/javascript" src="Resources/dynamsoft.webtwain.initiate.js"> </script>
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.config.js"> </script>

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        
         function ActualizarSeleccionArea(id_area, nombre) {
            document.getElementById("<%=Hdd_id_area.ClientID%>").value = id_area;
            document.getElementById("<%=txtArea.ClientID%>").value = nombre;
            document.getElementById("<%=txtArea.ClientID%>").Text = nombre;
        }
        function ActualizarSeleccionTercero(id_tercero, documento, nombre) {
            document.getElementById("<%=Hdd_id_remitente.ClientID%>").value = id_tercero;
            document.getElementById("<%=txtDocumentoTercero.ClientID%>").value = documento;
            document.getElementById("<%=txtTercero.ClientID%>").value = nombre;
            document.getElementById("<%=txtDocumentoTercero.ClientID%>").Text = documento;
            document.getElementById("<%=txtTercero.ClientID%>").Text = nombre;
        }
        function ActualizarSeleccionDestinatario(id_tercero, documento, nombre) {
            document.getElementById("<%=Hdd_id_destinatario.ClientID%>").value = id_tercero;
            document.getElementById("<%=TxtDocDestinatario.ClientID%>").value = documento;
            document.getElementById("<%=TxtDestinatario.ClientID%>").value = nombre;
            document.getElementById("<%=TxtDocDestinatario.ClientID%>").Text = documento;
            document.getElementById("<%=txtTercero.ClientID%>").Text = nombre;
        }
        function ActualizarSeleccionResponsable(id_tercero, documento, nombre) {
            document.getElementById("<%=Hdd_id_responsable.ClientID%>").value = id_tercero;
            document.getElementById("<%=txtResponsable.ClientID%>").value = nombre;
            document.getElementById("<%=txtResponsable.ClientID%>").Text = nombre;
        }
        function ActualizarSeleccionMensajeria(id_tercero, documento, nombre) {
            document.getElementById("<%=Hdd_id_mensajeria.ClientID%>").value = id_tercero;
            document.getElementById("<%=txtResponsable.ClientID%>").value = nombre;
            document.getElementById("<%=txtResponsable.ClientID%>").Text = nombre;
        }
        function ActualizarSeleccionDocumento(id_documento, nombre) {
            document.getElementById("<%=Hdd_id_documento.ClientID%>").value = id_documento;
            document.getElementById("<%=TxtDocumento.ClientID%>").value = nombre;
            document.getElementById("<%=TxtDocumento.ClientID%>").Text = nombre;
        }
    </script>
    
    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>CORRESPONDENCIA</h3>
                <div class="form-horizontal fom-border">
                    <%--<div class="panel panel-default" style="width: 500px; padding: 10px; margin: 10px">
                        <div id="TabXs" role="tabpanel">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li><a href="#personal" aria-controls="personal" role="tab" data-toggle="tab">Personal
                                </a></li>
                                <li><a href="#employment" aria-controls="employment" role="tab" data-toggle="tab">Employment</a></li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content" style="padding-top: 20px">
                                <div role="tabpanel" class="tab-pane active" id="personal">
                                    This is Personal Information Tab
                                </div>
                                <div role="tabpanel" class="tab-pane" id="employment">
                                    This is Employment Information Tab
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="Button1" Text="Submit" runat="server" CssClass="btn btn-primary" />
                        <asp:HiddenField ID="TabName" runat="server" />
                    </div>--%>
                    
                    <div class="tabbable" id="Tabs" role="tabpanel"> <!-- Only required for left/right tabs -->
					    <ul class="nav nav-tabs" role="tablist">
					        <li class="active"><a href="#tab1" aria-controls="personal" role="tab" data-toggle="tab">Información General</a></li>
					        <li><a href="#tab2" aria-controls="personal" role="tab" data-toggle="tab">Información Complementaria</a></li>
					        <li><a href="#tab3" aria-controls="personal" role="tab" data-toggle="tab">Información Envío/Recibo</a></li>
					      </ul>
					    <div class="tab-content">
				    	    <div role="tabpanel" class="tab-pane active" id="tab1">
					    	    <div class="row-fluid">
					    		    <div class="span4">
					    			    <h2>Información General</h2>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label10" runat="server" Text="Tipo Correspondencia:*" Width="180px" />
                                            </b>
                                            <asp:DropDownList ID="txttipoc" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" AutoPostBack="True">
                                                <asp:ListItem>Externa Enviada</asp:ListItem>
                                                <asp:ListItem>Externa Recibida</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label6" runat="server" Text="Radicado Documento:" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="TxtRadicado" CssClass="input-xlarge" Width="180px" ReadOnly="true"  /> 
                                             <b class="control-label">
                                                <asp:Label ID="Label11" runat="server" Text="Fecha Correspondencia:" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="TxtfechaC" CssClass="input-xlarge" Width="180px" TextMode="Date"  /> 
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label2" runat="server" Text="Asunto:*" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="TxtAsunto" CssClass="input-xlarge" Width="320px" Height="66px" TextMode="MultiLine" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="TxtAsunto" ErrorMessage=" * El campo 'Asunto' es obligatorio." Width="400px" />
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label3" runat="server" Text="Estado:*" Width="180px" />
                                            </b>
                                            <asp:DropDownList ID="txtestado" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD">
                                                <asp:ListItem>Registrada</asp:ListItem>
                                                <asp:ListItem>Recibida</asp:ListItem>
                                                <asp:ListItem>Atendida</asp:ListItem>
                                                <asp:ListItem>Archivada</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento:*" Width="180px" />
                                            </b>
                                            <asp:DropDownList ID="txtTipoDocumento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipoDoc" DataTextField="nombre" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label15" runat="server" Text="Responsable:*" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="txtResponsable" CssClass="input-xlarge" Width="320px"  /> 
                                            <asp:ImageButton ID="BttResponsable" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" Width="16px" />                   
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="lblRem" runat="server" Text="Destinatario:*" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="TxtRemitenteAdd" CssClass="input-xlarge" Width="320px"  />  
                                            <asp:RequiredFieldValidator ID="ValidaRem" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="TxtRemitenteAdd" ErrorMessage=" * El campo 'Remitente' es obligatorio." Width="400px" />                  
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label19" runat="server" Text="Area Asociada" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="Txtarea" CssClass="input-xlarge" Width="320px" ReadOnly="true"   />
                                            <asp:ImageButton ID="BttArea" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" Width="16px" /> 
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label4" runat="server" Text="Nro de Folios:*" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="TxtFolios" CssClass="input-xlarge" Width="180px" TextMode="Number" Text="1"  /> 
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label7" runat="server" Text="Nro de Carpetas o Paquetes:*" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="txtPaquetes" CssClass="input-xlarge" Width="180px" TextMode="Number" Text="0"  /> 
                                        </div>
                                        <asp:Panel ID="PRespuesta" runat="server">
                                            <h2>Respuesta Correspondencia:</h2>
                                            <div>
                                                <b class="control-label">
                                                    <asp:Label ID="Label18" runat="server" Text="Correspondencia Asociada:" Width="180px" />
                                                </b>
                                                <asp:DropDownList ID="txtCorrespondencia" runat="server" Font-Overline="False" Height="20px" Width="180px" CssClass="newsInputD" DataSourceID="SqlCorrespondencia" DataTextField="numero_radicado" DataValueField="id">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlCorrespondencia" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="Select ' Ninguna' as numero_radicado,0 as id
union all
SELECT [numero_radicado], [id] FROM [Correspondencia] WHERE (([id_sucursal] = @id_sucursal) AND ([id] &lt;&gt; @id)) ORDER BY [numero_radicado]">
                                                    <SelectParameters>
                                                        <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" Type="Int64" />
                                                        <asp:QueryStringParameter DefaultValue="0" Name="id" QueryStringField="id_correspondencia" Type="Int64" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                            <div>
                                                <b class="control-label">
                                                    <asp:Label ID="Label9" runat="server" Text="Fecha Respuesta:" Width="180px" />
                                                </b>
                                                <asp:TextBox runat="server" ID="TxtFecRespuesta" CssClass="input-xlarge" Width="180px" TextMode="Date" AutoPostBack="True"  /> 
                                            </div>
                                            <div>
                                                <b class="control-label">
                                                    <asp:Label ID="Label8" runat="server" Text="Respuesta:" Width="180px" />
                                                </b>
                                                <asp:TextBox runat="server" ID="TxtRespuesta" CssClass="input-xlarge" Width="320px" Height="66px" TextMode="MultiLine" />
                                                
                                            </div>
                                        </asp:Panel>
                                    </div>
				    		    </div>
				    	    </div>
					        <div role="tabpanel" class="tab-pane" id="tab2">
					    	    <h2>Información Complementaria</h2>
                                <div class="cabezote-form">
                                    <span>DOCUMENTOS ASOCIADOS</span>
                                </div>
                                <div>
                                    <asp:ImageButton ID="BttScan" ToolTip="Scanear Documento" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoscaner.jpg" BackColor="Transparent" Width="26" Height="26" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Documento:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtDocumento" CssClass="input-xlarge" Width="320px"  /> 
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Rec.png" />                                             
                                    <asp:ImageButton ID="BttDocumento" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="width: 16px" />
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttAgregarDoc" runat="server" ImageUrl="~/Images/icoAdd.png" ImageAlign="Middle" />
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttEliminarDoc" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" style="height: 24px" />
                                </div>
                                <div>
                                    <br />
                                    <br />
                                    <asp:ListBox ID="lstDocumentos" CssClass="input-xlarge" runat="server" Width="400px"></asp:ListBox>
                                    <asp:Button ID="BttDetalle" runat="server" Text="Detalle Documentos"  CssClass="btn btn-primary" ValidationGroup="Errores" />
                                </div>                    
                                <div class="cabezote-form">
                                    <span>REMITENTES ASOCIADOS</span>
                                </div>
                                <div>
                                    <p>&nbsp;</p>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDoc" runat="server" Text="Documento Remitente:" Width="150px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDocumentoTercero" Width="180px" CssClass="input-xlarge"  ViewStateMode="Enabled" AutoPostBack="True" TextMode="Number"></asp:TextBox>
                                    <asp:Image ID="R4" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <b class="control-label">
                                        <asp:Label ID="lbTercero" runat="server" Text="Remitente:" Width="80px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTercero" CssClass="input-xlarge" Width="180px" ViewStateMode="Enabled" AutoPostBack="True" />
                                    <asp:Image ID="R5" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttBuscarTercero" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" />
                                    <asp:Image ID="R6" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttAgregarTercero" runat="server" ImageUrl="~/Images/icoAdd.png" ImageAlign="Middle" />
                                    <asp:Image ID="R7" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttEliminarTercero" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" />
                                </div>
                                <div>
                                    <br />
                                    <br />
                                    <asp:ListBox ID="lstRemitentes" CssClass="input-xlarge" runat="server" Width="400px"></asp:ListBox>                       
                                </div>
                                <div>
                                     <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text=" Info Adicional Remitente:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtRemitente" CssClass="input-xlarge" Width="320px"  /> 
                                </div>
                                <div class="cabezote-form">
                                    <span>DESTINATARIOS ASOCIADOS</span>
                                </div>
                                <div>
                                    <p>&nbsp;</p>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label13" runat="server" Text="Documento Destinatario:" Width="150px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtDocDestinatario" Width="180px" CssClass="input-xlarge"  ViewStateMode="Enabled" AutoPostBack="True" TextMode="Number"></asp:TextBox>
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <b class="control-label">
                                        <asp:Label ID="Label14" runat="server" Text="Destinatario:" Width="80px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtDestinatario" CssClass="input-xlarge" Width="180px" ViewStateMode="Enabled" AutoPostBack="True" />
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="width: 16px" />
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icoAdd.png" ImageAlign="Middle" />
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" />
                                </div>
                                <div>
                                    <br />
                                    <br />
                                    <asp:ListBox ID="lstDestinatario" CssClass="input-xlarge" runat="server" Width="400px"></asp:ListBox>                       
                                </div>
                                <div>
                                     <b class="control-label">
                                        <asp:Label ID="Label12" runat="server" Text="Info Adicional Destinatario:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDest" CssClass="input-xlarge" Width="320px"  /> 
                                </div>
					        </div>
					        <div role="tabpanel" class="tab-pane" id="tab3">
					    	    <div>
                                    <h2>Información Envió/Recibo</h2>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Tipo Recibo/Envió:" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtTipoEnvio" runat="server" Font-Overline="False" Height="20px" Width="180px" CssClass="newsInputD">
                                        <asp:ListItem>Interna</asp:ListItem>
                                        <asp:ListItem>Privada</asp:ListItem>
                                        <asp:ListItem>Courrier Mensajería</asp:ListItem>
                                    </asp:DropDownList>
                                    <b class="control-label">
                                        <asp:Label ID="Label16" runat="server" Text="Guía:" Width="90px" />
                                    </b> 
                                    <asp:TextBox runat="server" ID="txtGuia" CssClass="input-xlarge" Width="180px"  /> 
                        
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label17" runat="server" Text="Courrier:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="TxtMensajeria" CssClass="input-xlarge" Width="320px"  /> 
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" Width="16px" />                   
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Observaciones:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDescripcion" CssClass="input-xlarge" Width="320px" Height="66px" TextMode="MultiLine" />
                                </div>
					        </div>
					    </div>
				    </div>
                    <script type="text/javascript">
                        $(function () {
                            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
                            $('#Tabs a[href="#' + tabName + '"]').tab('show');
                            $("#Tabs a").click(function () {
                                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                            });
                        });
                    </script>
                    <br />
                    <br />
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label20" runat="server" Text="Sticker:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtSticker" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" AutoPostBack="True">
                            <asp:ListItem>Pequeño</asp:ListItem>
                            <asp:ListItem>Carta</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />
                    <asp:Button ID="BttStiker" runat="server" Text="Generar Sticker" CssClass="btn btn-primary" ValidationGroup="Errores" />
                    <asp:Button ID="BtComentarios" runat="server" Text="Comentarios"  CssClass="btn btn-primary" ValidationGroup="Errores" />
                    
                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                    <asp:HiddenField ID="TabName" runat="server" />
                    <br />
                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                    <br />
                    <br />
                </div>
                
				
            </div>
        </div>

       
    
    </div>
    
    <asp:SqlDataSource ID="SqlTipoDoc" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [nombre] FROM [AdminTipoDocumento] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [nombre]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:TextBox runat="server" ID="txttab" CssClass="input-xlarge" Width="320px" Visible="false" text="0"  /> 
    <asp:HiddenField ID="Hdd_tab" runat="server" Value="#tab1" />
    <asp:HiddenField ID="Hdd_id_area" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_responsable" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_remitente" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_destinatario" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_mensajeria" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_documento" runat="server" Value="0" />
</asp:Content>


