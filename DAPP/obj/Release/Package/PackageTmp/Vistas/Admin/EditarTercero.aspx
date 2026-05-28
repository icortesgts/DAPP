<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarTercero.aspx.vb" Inherits="DAPP.EditarTercero" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">
    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        function ActualizarSeleccionArea(id_area, nombre) {
            document.getElementById("<%=Hdd_id_area.ClientID%>").value = id_area;
            document.getElementById("<%=txtArea.ClientID%>").value = nombre;
            document.getElementById("<%=txtArea.ClientID%>").Text = nombre;
        }

        function ActualizarSeleccionTercero(id_tercero, documento, nombre) {
            document.getElementById("<%=Hdd_id_tercero.ClientID%>").value = id_tercero;       
            document.getElementById("<%=txtTercero.ClientID%>").value = nombre;
            document.getElementById("<%=txtTercero.ClientID%>").Text = nombre;
        }


      
    </script>

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>TERCERO</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtTipoDocumento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD">
                                        <asp:ListItem>CC</asp:ListItem>
                                        <asp:ListItem>NIT</asp:ListItem>
                                        <asp:ListItem>CE</asp:ListItem>
                                        <asp:ListItem>Pasaporte</asp:ListItem>
                                         <asp:ListItem>NUIP</asp:ListItem>
                                        <asp:ListItem>Otro</asp:ListItem>
                                     </asp:DropDownList>                                  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Número Documento:*" Width="180px" />
                                    </b>
                                    <ig:WebNumericEditor ID="txtNumDocumento" Width="180px" runat="server" CssClass="input-xlarge" NullValue="0" NullText="0" Nullable="False"></ig:WebNumericEditor>                                                                  
                                    <asp:RequiredFieldValidator ID="RQ_NumDocumento" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNumDocumento" ErrorMessage=" * El campo 'Número Documento' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNombre" runat="server" Text="Nombre:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNombre" ErrorMessage=" * El campo 'Nombre' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label1" runat="server" Text="Sector:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtSector" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD">
                                        <asp:ListItem>Privado</asp:ListItem>
                                        <asp:ListItem>Público</asp:ListItem>
                                        <asp:ListItem>Mixto</asp:ListItem>
                                     </asp:DropDownList>                                  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Tipo:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txttipo" runat="server" Font-Overline="False" Height="22px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipo" DataTextField="tipo" DataValueField="id" AutoPostBack="True">
                                    </asp:DropDownList>                                  
                                    
                                    <asp:SqlDataSource ID="SqlTipo" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [tipo] FROM [AdminTipoTercero] WHERE (activo=1 and[id_cliente] = @id_cliente) ORDER BY [tipo]">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="id_cliente" SessionField="id_cliente" Type="Int64" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Actividad:*" Width="180px" />
                                    </b>
                                    <asp:DropDownList ID="txtActividad" runat="server" Font-Overline="False" Height="22px" Width="320px" CssClass="newsInputD" DataSourceID="SqlActividad" DataTextField="actividad" DataValueField="id">
                                    </asp:DropDownList>                                  
                                    
                                    <asp:SqlDataSource ID="SqlActividad" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [actividad] FROM [AdminActividad] WHERE (activo=1 and[id_cliente] = @id_cliente) ORDER BY [actividad]">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="id_cliente" SessionField="id_cliente" Type="Int64" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDireccion" runat="server" Text="Dirección:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtDireccion" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Direccion" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDireccion" ErrorMessage=" * El campo 'Dirección' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="width:33%;">
                                                <div>
                                                    <b class="control-label">
                                                        <asp:Label ID="Label6" runat="server" Text="Pais:*" Width="120px" />
                                                    </b>                                                    
                                                    <asp:DropDownList ID="txtpais" runat="server" Width="200px" DataSourceID="SqlPais" DataTextField="Pais" DataValueField="id" AutoPostBack="True" >
                                                    </asp:DropDownList>

                                                    <asp:SqlDataSource ID="SqlPais" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [Pais] FROM [AdminPais] ORDER BY [Pais]"></asp:SqlDataSource>

                                                </div>
                                            </td>
                                            <td style="width:33%;">
                                                    <b class="control-label">
                                                        <asp:Label ID="Label7" runat="server" Text="Estado:*" Width="120px" />
                                                    </b>                                                    
                                                    <asp:DropDownList ID="txtDpto" runat="server" Width="200px" DataSourceID="SqlDpto" DataTextField="departamento" DataValueField="departamento" AutoPostBack="True" >
                                                    </asp:DropDownList>

                                                    <asp:SqlDataSource ID="SqlDpto" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [departamento] FROM [Admin_Ciudades] WHERE ([id_pais] = @id_pais)">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="txtpais" DefaultValue="0" Name="id_pais" PropertyName="SelectedValue" Type="Int64" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                            </td>
                                            <td style="width:34%;">
                                                    <b class="control-label">
                                                        <asp:Label ID="Label8" runat="server" Text="Ciudad:*" Width="120px" />
                                                    </b>                                                    
                                                    <asp:DropDownList ID="txtCiudad" runat="server" Width="200px" DataSourceID="SqlCiudad" DataTextField="ciudad" DataValueField="codigo" >
                                                    </asp:DropDownList>

                                                    <asp:SqlDataSource ID="SqlCiudad" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [codigo], [ciudad] FROM [Admin_Ciudades] WHERE ([departamento] = @departamento) ORDER BY [ciudad]">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="txtDpto" DefaultValue="" Name="departamento" PropertyName="SelectedValue" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbTelefono" runat="server" Text="Teléfono:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Telefono" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtTelefono" ErrorMessage=" * El campo 'Telefono' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbCelular" runat="server" Text="Celular:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtCelular" CssClass="input-xlarge" Width="180px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Celular" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCelular" ErrorMessage=" * El campo 'Celular' es obligatorio." Width="400px" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbCorreo" runat="server" Text="Correo:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtCorreo" CssClass="input-xlarge" Width="320px" />                                    
                                    <asp:RequiredFieldValidator ID="RQ_Correo" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCorreo" ErrorMessage=" * El campo 'Correo' es obligatorio." Width="400px" />
                                </div>
                                
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="Label2" runat="server" Text="Activo:*" Width="180px" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="Chkactivo" runat="server" CssClass="control-label" Text="    " TextAlign="Left"/>
                                    
                                </div>
                                <div>                              
                                    <table>
                                        <tr>
                                            <td>
                                                <b class="control-label">
                                                    <asp:Label ID="Label4" runat="server" Text=" Maneja Firma:*" Width="111px" />
                                                </b>  
                                            </td>
                                            <td style="width:10%">
                                                <asp:CheckBox ID="ChkFirma" runat="server" CssClass="control-label" Text="    " TextAlign="Left" AutoPostBack="True"/>
                                            </td>
                                            <td style="width:80%">
                                                <b class="control-label">
                                                    <asp:Label ID="Lblvalida" runat="server" Text="Contraseña Firma:*" Width="180px" Visible="false"  />
                                                </b>
                                                <asp:TextBox runat="server" ID="txtvalida" CssClass="input-xlarge" Width="320px" Visible="false" TextMode="Password"  /> 
                                                <asp:Button ID="BttFirma" runat="server" Text="Ver Firma"  CssClass="btn btn-primary" ValidationGroup="Errores" /> 
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td style="width:10%">
                                                 <b class="control-label">
                                                    <asp:Label ID="Lblfirma" runat="server" Text="Archivo Firma:" Width="117px" Height="18px" Visible="false"  />
                                                </b>
                                            </td>
                                            <td style="width:90%;align-items:flex-start">
                                                <asp:FileUpload ID="flDocumentos" runat="server" CssClass="input-xlarge" Width="320px"  Visible="false"  />  
                                            </td>
                                        </tr>
                                    </table>                                  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Lblpwd" runat="server" Text="Contraseña Firma:*" Width="180px" Visible="false"  />
                                    </b>
                                    <asp:TextBox runat="server" ID="Txtpwd" CssClass="input-xlarge" Width="320px" Visible="false" TextMode="Password"  />                                    
                                    
                                </div>
                                <div>
                                    
                                     <b class="control-label">
                                        <asp:Label ID="lblmensajeria" runat="server" Text="Mensajeria:" Width="180px"  Visible="false" />
                                    </b>                                
                                    
                                    <asp:CheckBox ID="Chkmensajeria" runat="server" CssClass="control-label" Text="    " TextAlign="Left" Visible="false" />
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbArea" runat="server" Text="Area:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtArea" Width="320px" CssClass="input-xlarge" ViewStateMode="Enabled"></asp:TextBox>
                                    <asp:Image ID="R1" runat="server" ImageUrl="~/Images/Rec.png" />                                             
                                    <asp:ImageButton ID="bttBuscarArea" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" Visible="False" />
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbDoc" runat="server" Text="Jefe:" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtTercero" CssClass="input-xlarge" Width="320px" ViewStateMode="Enabled" />
                                    <asp:Image ID="R5" runat="server" ImageUrl="~/Images/Rec.png" />
                                    <asp:ImageButton ID="bttBuscarTercero" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="width: 16px" Visible="False" />
                                    
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="LblCargo" runat="server" Text="Cargo:" Width="180px" Visible="false"  />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtcargo" CssClass="input-xlarge" Width="320px" ViewStateMode="Enabled" Visible="false" />
                                    
                                </div>
                                <br />
                                <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />                                  
                                <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                                <br />
                                <br />
                                <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                                <asp:HiddenField ID="Hdd_id_area" runat="server" Value="0" />
                                <asp:HiddenField ID="Hdd_id_tercero" runat="server" Value="0" />
                                <asp:TextBox runat="server" ID="txtcuantos" Height="70" TextMode="MultiLine" Rows="5" Width="280px" Visible="false"  />
                            </div>                      
            </div>
        </div>
    </div>
</asp:Content>


