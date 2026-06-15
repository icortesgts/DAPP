<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarDocumento.aspx.vb" Inherits="DAPP.EditarDocumento" %>

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
            document.getElementById("<%=Hdd_id_tercero.ClientID%>").value = id_tercero;
            document.getElementById("<%=txtDocumentoTercero.ClientID%>").value = documento;
            document.getElementById("<%=txtTercero.ClientID%>").value = nombre;
            document.getElementById("<%=txtDocumentoTercero.ClientID%>").Text = documento;
            document.getElementById("<%=txtTercero.ClientID%>").Text = nombre;
        }


      
    </script>

   

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>DOCUMENTO</h3>
                <div class="form-horizontal fom-border">
                    <div class="tabbable" id="Tabs" role="tabpanel"> <!-- Only required for left/right tabs -->
					    <ul class="nav nav-tabs" role="tablist">
					        <li class="active"><a href="#tab1" aria-controls="personal" role="tab" data-toggle="tab">Información General</a></li>
					        <li><a href="#tab2" aria-controls="personal" role="tab" data-toggle="tab">Información Asociada</a></li>
					        <li><a href="#tab3" aria-controls="personal" role="tab" data-toggle="tab">Información Adicional</a></li>
					    </ul>
					    <div class="tab-content">
				    	    <div role="tabpanel" class="tab-pane active" id="tab1">
					    	    <div class="row-fluid">
					    		    <div class="span4">
					    			    <h2>Información General</h2>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento:*" Width="180px" />
                                            </b>
                                            <asp:DropDownList ID="txtTipoDocumento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipoDoc" DataTextField="nombre" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                             <b class="control-label">
                                                <asp:Label ID="Label1" runat="server" Text="Ubicación:*" Width="180px" />
                                            </b>
                                            <asp:DropDownList ID="TxtUbicacion" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlUbicacion" DataTextField="ubicacion" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="lbNumDocumento" runat="server" Text="Nombre Documento:*" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="txtAlias" CssClass="input-xlarge" Width="320px"  /> 
                                            
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="lbNombre" runat="server" Text="Descripcion: *" Width="180px" />
                                            </b>
                                            <asp:TextBox runat="server" ID="txtDescripcion" CssClass="input-xlarge" Width="320px" Height="66px" TextMode="MultiLine" />
                                            <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDescripcion" ErrorMessage=" * El campo 'Descripción' es obligatorio." Width="400px" />
                                        </div>
                                        <div>
                                            <b class="control-label">
                                                <asp:Label ID="Label2" runat="server" Text="Folios:*" Width="180px"  />
                                            </b>
                                            <asp:TextBox runat="server" ID="TxtFolios" CssClass="input-xlarge" Width="320px"  TextMode="Number" >1</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="TxtFolios" ErrorMessage=" * El campo 'Folios' es obligatorio." Width="400px" />                      
                                        </div>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td style="width:10%">
                                                         <b class="control-label">
                                                            <asp:Label ID="Label7" runat="server" Text="Archivo:*" Width="117px" Height="18px" />
                                                        </b>
                                                    </td>
                                                    <td style="width:90%;align-items:flex-start">
                                                        <asp:FileUpload ID="flDocumentos" runat="server" CssClass="input-xlarge" Width="320px" />  
                                                    </td>
                                                </tr>
                                            </table>                                  
                                        </div>
                                        <div>
                                             <b class="control-label">
                                                <asp:Label ID="lbTelefono" runat="server" Text="Versión:*" Width="180px" />
                                            </b>
                                            <ig:WebNumericEditor ID="txtVersion" Width="180px" runat="server" CssClass="input-xlarge" NullValue="1" NullText="1" Nullable="False" BorderStyle="None" DataMode="Int" MinValue="1" ToolTip="Versión">
                                            </ig:WebNumericEditor>
                                            <asp:RequiredFieldValidator ID="RQ_Version" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtVersion" ErrorMessage=" * El campo 'Versión' es obligatorio." Width="400px" />
                                        </div>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td style="width:10%; height:21px;">
                                                        <b class="control-label">
                                                            <asp:Label ID="Label4" runat="server" Text="Fecha Documento: *" Width="180px"  />
                                                        </b>
                                                        <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="input-xlarge" Width="180px" textmode="Date" AutoPostBack="True"  /> 
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaInicio" ErrorMessage=" * El campo 'Fecha Documento' es obligatorio." Width="400px" />  
                                                    </td>
                                                    <td style="width:90%;align-items:flex-start">
                                                        <b class="control-label">
                                                            <asp:Label ID="Label8" runat="server" Text="Fecha A. Gestión:" Width="147px"  />
                                                        </b>
                                                        <asp:TextBox runat="server" ID="FGestion" CssClass="input-xlarge" readonly="true"  Width="180px" textmode="Date"  /> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:10%; height:21px;">
                                                        <b class="control-label">
                                                            <asp:Label ID="Label3" runat="server" Text="Fecha A. Central:" Width="180px"  />
                                                        </b>
                                                        <asp:TextBox runat="server" ID="FCentral" CssClass="input-xlarge" Width="180px" textmode="Date" readonly="true" /> 
                                    
                                                    </td>
                                                    <td style="width:90%;align-items:flex-start">
                                                        <b class="control-label">
                                                            <asp:Label ID="Label9" runat="server" Text="Fecha A. Histórico:" Width="147px" />
                                                        </b>
                                                        <asp:TextBox runat="server" ID="FHistorico" CssClass="input-xlarge" Width="180px" textmode="Date" readonly="true" /> 
                                                    </td>
                                                </tr>
                                            </table>                                  
                                        </div>
                                    </div>
                                </div> 
                            </div>
                            <div role="tabpanel" class="tab-pane" id="tab2">
					    	    <h2>Información Asociada</h2>
                                <div class="cabezote-form">
                                    <span>AREAS ASIGNADAS</span>
                                </div>
                                <div>
                                    <p>&nbsp;</p>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbArea" runat="server" Text="Area:" Width="150px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtArea" Width="180px" CssClass="input-xlarge" ReadOnly="true" ViewStateMode="Enabled"></asp:TextBox>
                                    <asp:Image ID="R1" runat="server" ImageUrl="~/Images/Rec.png" />                                             
                                    <asp:ImageButton ID="bttBuscarArea" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" />
                                    <asp:Image ID="R2" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttAgregarArea" runat="server" ImageUrl="~/Images/icoAdd.png" ImageAlign="Middle" />
                                    <asp:Image ID="R3" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttEliminarArea" runat="server" ImageUrl="~/Images/icoDel.png" ImageAlign="Middle" />
                                </div>
                                <div>
                                    <br />
                                    <br />
                                    <asp:ListBox ID="lstAreasDocumento" CssClass="input-xlarge" runat="server" Width="400px"></asp:ListBox>
                                </div>
                                <br />
                                <br />

                                <div class="cabezote-form">
                                    <span>TERCEROS ASOCIADOS</span>
                                </div>
                                <div>
                                    <p>&nbsp;</p>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDoc" runat="server" Text="Documento Tercero:" Width="150px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDocumentoTercero" Width="180px" CssClass="input-xlarge" ViewStateMode="Enabled" AutoPostBack="True" TextMode="Number"></asp:TextBox>
                                    <asp:Image ID="R4" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <b class="control-label">
                                        <asp:Label ID="lbTercero" runat="server" Text="Tercero:" Width="80px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTercero" CssClass="input-xlarge" Width="180px" ViewStateMode="Enabled" AutoPostBack="True" />
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
                            </div>
                            <div role="tabpanel" class="tab-pane" id="tab3">
					    	    <div>
                                    <h2>Información Adicional</h2>
                                    <div class="cabezote-form">
                                        <span>CAMPOS ADICIONALES</span>
                                    </div>
                                    <div>
                                       <asp:table ID="TblDatos" runat="server" CssClass="Office2007Blue" Width="100%" ViewStateMode="Enabled">

                                        </asp:table>
                                    </div>
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
                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />
                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />                    
                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                    <asp:TextBox runat="server" ID="txtnombre" CssClass="input-xlarge" Width="320px" ReadOnly="false" Visible="false"  />
                    <asp:HiddenField ID="TabName" runat="server" />
                    <br />
                    <br />
                </div>
            </div>
        </div>
         <asp:TextBox runat="server" ID="txtcuantos" Height="70" TextMode="MultiLine" Rows="5" Width="280px" Visible="false"  />
       
    
    </div>
    <asp:SqlDataSource ID="SqlTipoDoc" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [nombre] FROM [AdminTipoDocumento] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [nombre]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlUbicacion" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [ubicacion] FROM [ubicaciones] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [ubicacion]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="Hdd_id_area" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_tercero" runat="server" Value="0" />
</asp:Content>


