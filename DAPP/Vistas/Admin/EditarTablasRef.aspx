<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarTablasRef.aspx.vb" Inherits="DAPP.EditarTablasRef" %>

<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        function ActualizarSeleccionArea(id_area, nombre) {
            document.getElementById("<%=Hdd_id_area.ClientID%>").value = id_area;
            document.getElementById("<%=txtArea.ClientID%>").value = nombre;
            document.getElementById("<%=txtArea.ClientID%>").Text = nombre;
        }
      
    </script>


    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>ADMINISTRACIÓN TABLAS DE RETENCIÓN</h3>               
                    <div class="form-horizontal fom-border">                        
                                <br />
                                <br />
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label3" runat="server" Text="Oficina:*" Width="180px" />
                                    </b>                                   
                                    <asp:DropDownList ID="txtOficina" runat="server" CssClass="input-xlarge" Width="320px" DataSourceID="SqlOficina" DataTextField="nombre" DataValueField="nombre">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlOficina" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="Select nombre as nombre from clientes
where id=@cliente
union all
select clientes.nombre + ' - ' +  adminsucursal.sucursal as nombre from adminsucursal
inner join clientes
on adminsucursal.id_cliente=clientes.id
where adminsucursal.id_cliente=@cliente
order by nombre">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text="Entidad:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtArea" Width="320px" CssClass="input-xlarge" ReadOnly="true" ViewStateMode="Enabled"></asp:TextBox>
                                    <asp:Image ID="R1" runat="server" ImageUrl="~/Images/Rec.png" />                                             
                                    <asp:ImageButton ID="bttBuscarArea" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" />                          
                                    <asp:HiddenField ID="Hdd_id_area" runat="server" Value="0" />                                         
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtArea" ErrorMessage=" * El campo 'Entidad' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="Label5" runat="server" Text="Responsable:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtResponsable" CssClass="input-xlarge" Width="180px" />                                                                  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtResponsable" ErrorMessage=" * El campo 'Responsable' es obligatorio." Width="400px" />  
                                </div>                                
                                <div>
                                    <b class="control-label">
                                        <asp:Label ID="lbNumDocumento" runat="server" Text="Código:*" Width="180px" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtCodigo" CssClass="input-xlarge" Width="180px" />                                                                   
                                    <asp:RequiredFieldValidator ID="RQ_Codigo" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCodigo" ErrorMessage=" * El campo 'Código' es obligatorio." Width="400px" />  
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 50%;">
                                                <b class="control-label">
                                                    <asp:Label ID="Label2" runat="server" Text="Fecha Inicio:*" Width="180px" />
                                                </b>
                                                <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="input-xlarge" Width="180px" textmode="Date" AutoPostBack="True"  /> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaInicio" ErrorMessage=" * El campo 'Fecha Inicio' es obligatorio." Width="400px" />  
                                            </td>
                                            <td  style="text-align: left; width: 50%;">
                                                <b class="control-label">
                                                    <asp:Label ID="Label7" runat="server" Text="Fecha Fin:*" Width="180px" />
                                                </b>
                                                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="input-xlarge" Width="180px" textmode="Date" AutoPostBack="True"  /> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaFin" ErrorMessage=" * El campo 'Fecha Fin' es obligatorio." Width="400px" />  
                                            </td>                                            
                                        </tr>                            
                                    </table>
                                    
                                                                                                   
                                    
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
</asp:Content>


