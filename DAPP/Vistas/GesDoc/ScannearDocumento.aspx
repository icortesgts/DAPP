<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ScannearDocumento.aspx.vb" Inherits="DAPP.ScannearDocumento" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">

    <script type="text/javascript" src="Resources/dynamsoft.webtwain.initiate.js"> </script>
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.config.js"> </script>

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        


        function AcquireImage()
        {
            var DWObject = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainer');
            DWObject.IfDisableSourceAfterAcquire = true;
            DWObject.SelectSource();
            DWObject.OpenSource();
            DWObject.AcquireImage();
        }

    </script>

   

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>SCANEAR DOCUMENTO</h3>
                <div class="form-horizontal fom-border">
                    <br />
                    <br />
                    <div style="align-content:center; text-align: center;">
                        <br />
                        <br />
                        <div id="dwtcontrolContainer" style="width:25%;margin: auto;">                                                    
                        </div>                        
                        <asp:Button ID="bttScan" runat="server" Text="SCAN" ValidationGroup="Scan" OnClientClick="AcquireImage();" BackColor="White" BorderColor="#666666" Font-Bold="True" />                                                  
                    </div> 
                     
                    <br />
                    <br /> 
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtTipoDocumento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipoDoc" DataTextField="nombre" DataValueField="id">
                        </asp:DropDownList>
                    </div>
                    
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNumDocumento" runat="server" Text="Nombre Documento:*" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtAlias" CssClass="input-xlarge" Width="320px"  /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtAlias" ErrorMessage=" * El campo 'Nombre' es obligatorio." Width="400px" />                      
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNombre" runat="server" Text="Descripcion:/" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtDescripcion" CssClass="input-xlarge" Width="320px" Height="66px" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDescripcion" ErrorMessage=" * El campo 'Descripción' es obligatorio." Width="400px" />
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label4" runat="server" Text="Fecha Documento: *" Width="180px" />
                        </b>
                       
                        <ig:WebDatePicker ID="txtFechaInicio" runat="server" DisplayModeFormat="d" Nullable="False" CssClass="input-xlarge"  Width="180px" SpinOnlyOneField="True" DropDownCalendarID="WMInicio">
                        </ig:WebDatePicker> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaInicio" ErrorMessage=" * El campo 'Fecha Documento' es obligatorio." Width="400px" />  
                        <ig:WebMonthCalendar ID="WMInicio" runat="server">
                        </ig:WebMonthCalendar>
                                                                                        
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label2" runat="server" Text="Folios:*" Width="180px"  />
                        </b>
                        <asp:TextBox runat="server" ID="TxtFolios" CssClass="input-xlarge" Width="320px" ReadOnly="true" TextMode="Number" >1</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="TxtFolios" ErrorMessage=" * El campo 'Folios' es obligatorio." Width="400px" />                      
                    </div>
                    <div>
                         <b class="control-label">
                            <asp:Label ID="lbArchivo" runat="server" Text="Archivo:*" Width="180px" />
                        </b>
                        <div style="margin-left:180px">
                            <asp:FileUpload ID="flDocumentos" runat="server" CssClass="input-xlarge" Width="320px" />                          
                        </div>
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbTelefono" runat="server" Text="Versión:*" Width="180px" />
                        </b>
                        <ig:WebNumericEditor ID="txtVersion" Width="180px" runat="server" CssClass="input-xlarge" NullValue="1" NullText="1" Nullable="False" BorderStyle="None" DataMode="Int" MinValue="1" ToolTip="Versión">
                        </ig:WebNumericEditor>
                        <asp:RequiredFieldValidator ID="RQ_Version" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtVersion" ErrorMessage=" * El campo 'Versión' es obligatorio." Width="400px" />
                    </div>

                    <br />
                    <br />
                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />
                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                    <br />
                    <br />
                    
                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                    <asp:TextBox runat="server" ID="txtnombre" CssClass="input-xlarge" Width="320px" ReadOnly="false" Visible="false"  />
                </div>
            </div>
        </div>

       
    
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


