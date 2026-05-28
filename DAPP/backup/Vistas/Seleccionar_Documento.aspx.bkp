<%@ Page Title="" Language="VB" AutoEventWireup="true" CodeBehind="Seleccionar_Documento.aspx.vb" Inherits="DAPP.Seleccionar_Documento" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>SELECCIONAR DOCUMENTO</title>
    <link rel="stylesheet" type="text/css" href="../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../Styles/styles_form.css">    
    <style type="text/css">
        .altura {
            height: 45px;
        }       
        
    </style>
</head>
<body>
    <form id="form1" runat="server" class="formulario">
        <asp:ScriptManager ID="MGR" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="AJAX_Panel" runat="server">
            <ContentTemplate>

                <div class="DivFormulario">
                     <h3>SELECCIONAR DOCUMENTO</h3>
                    <table style="margin: auto; width: 100%;">
                        <tr>
                            <td style="text-align: center;" colspan="8">
                            <div class="cabezote-form">
                                <span>FILTROS</span>
                            </div>
                            <br />
                            <br />
                            <div>
                                <b class="control-label">
                                    <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento:" Width="180px" />
                                </b>
                                <asp:DropDownList ID="txtTipoDocumento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipoDoc" DataTextField="nombre" DataValueField="id">   
                                    </asp:DropDownList>                                  
                            </div>
                            <div>
                                <b class="control-label">
                                    <asp:Label ID="Label1" runat="server" Text="Ubicación:" Width="180px" />
                                </b>
                                <asp:DropDownList ID="TxtUbicacion" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlUbicacion" DataTextField="ubicacion" DataValueField="id">   
                                    </asp:DropDownList>                                  
                            </div>
                            <div>
                                <b class="control-label">
                                    <asp:Label ID="Label2" runat="server" Text="Tercero:" Width="180px" />
                                </b>
                                <asp:TextBox runat="server" ID="txtTercero" CssClass="input-xlarge" Width="320px" />                                   
                            </div>
                            <div>
                                <b class="control-label">
                                    <asp:Label ID="Label3" runat="server" Text="Area:" Width="180px" />
                                </b>
                                <asp:TextBox runat="server" ID="txtArea" CssClass="input-xlarge" Width="320px" />                                   
                            </div>
                            <div>
                                <b class="control-label">
                                    <asp:Label ID="lbNumDocumento" runat="server" Text="Nombre Documento:" Width="180px" />
                                </b>
                                <asp:TextBox runat="server" ID="txtNombre" CssClass="input-xlarge" Width="320px" />                                                              
                            </div>
                            <div>
                                <b class="control-label">
                                    <asp:Label ID="Label4" runat="server" Text="Descripción:" Width="180px" />
                                </b>
                                <asp:TextBox runat="server" ID="txtdescipcion" CssClass="input-xlarge" Width="320px" />                                                              
                            </div>
                
                                

                                
                            <br />
                            <asp:Button ID="btOk" runat="server" Text="Buscar" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />                                  
                            <asp:Button ID="BttSeleccionar" runat="server" Text="Seleccionar Documento" OnClick="BttSeleccionar_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />
                            <br />
                            <asp:SqlDataSource ID="SqlTipoDoc" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT 0 as [id],' Todos' as [nombre] union select [id], [nombre] FROM [AdminTipoDocumento] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [nombre]">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlUbicacion" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT 0 as [id],' Todas' as [ubicacion] union select [id], [ubicacion] FROM [ubicaciones] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [ubicacion]">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" />
                                </SelectParameters>
                            </asp:SqlDataSource>    
                            </td> 
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 40px;">&nbsp;
                                <asp:ImageButton ID="bttAbrir" ToolTip="Abrir Documento" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoOpen.png" BackColor="Transparent" Width="26" Height="26" />
                            </td> 
                            <td style="text-align: center; width: 40px;">
                                <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Documento" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>                
                            <td style="text-align: center">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="8">
                                <ig:WebDataGrid ID="GridDocs" runat="server" Width="100%" AutoGenerateColumns="False" HeaderCaptionCssClass="HeaderCaptionClass"  StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Clientes" DataKeyFields="id" >
                                
                                    <Columns>
                                        <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                            <Header Text="Archivo">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="alias" Key="alias">
                                            <Header Text="Nombre">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="descripcion" Key="descripcion">
                                            <Header Text="Descripcion">
                                            </Header>
                                        </ig:BoundDataField>  
                                        <ig:BoundDataField DataFieldName="folios" Key="folios">
                                            <Header Text="Folios">
                                            </Header>
                                        </ig:BoundDataField>                           
                                        <ig:BoundDataField DataFieldName="version" Key="version">
                                            <Header Text="Version">
                                            </Header>
                                        </ig:BoundDataField>
                                         <ig:BoundDataField DataFieldName="fechadoc" Key="fechadoc">
                                            <Header Text="Fecha Documento">
                                            </Header>
                                        </ig:BoundDataField>                                                  
                                        <ig:BoundDataField DataFieldName="fecha_creacion" Key="fecha_creacion">
                                            <Header Text="Fecha Creacion">
                                            </Header>
                                        </ig:BoundDataField>  
                                        <ig:BoundDataField DataFieldName="fecha_modificacion" Key="fecha_modificacion">
                                            <Header Text="Fecha Modificacion">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="usuario" Key="usuario">
                                            <Header Text="Usuario">
                                            </Header>
                                        </ig:BoundDataField>                                                      
                                        <ig:BoundDataField DataFieldName="ubicacion" Key="ubicacion">
                                            <Header Text="Ubicacion">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="tipodoc" Key="tipodoc">
                                            <Header Text="Tipo Documento">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="Area" Key="Area">
                                            <Header Text="Area">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="Tercero" Key="Tercero">
                                            <Header Text="Tercero">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="Areaid" Key="Areaid">
                                            <Header Text="ID Area">
                                            </Header>
                                        </ig:BoundDataField>
                                    </Columns>
                                    <Behaviors>
                                        <ig:Selection CellClickAction="Row" RowSelectType="Single">
                                            <AutoPostBackFlags RowSelectionChanged="True" />
                                        </ig:Selection>
                                        <ig:Sorting SortingMode="Single" Enabled="true">
                                        </ig:Sorting>
                                        <ig:Filtering>
                                        </ig:Filtering>
                                        <ig:Paging PageSize="10">
                                        </ig:Paging>
                                    </Behaviors>
                                </ig:WebDataGrid>
                                <asp:SqlDataSource ID="Src_Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Documentos.nombre
                  ,Documentos.descripcion
                  ,Documentos.version
                  ,Documentos.fecha_creacion
                  ,Documentos.fecha_modificacion
                  ,Documentos.usuario
                  ,Documentos.id_tipo
                  ,Documentos.id_ubicacion
                  ,Documentos.id_cliente
                  ,Documentos.id as id
                  ,Documentos.alias
                  ,Documentos.folios
	              ,isnull(ubicaciones.ubicacion,'Pendiente') as ubicacion
	              ,AdminTipoDocumento.nombre as tipodoc
	              ,isnull(Areas.nombre,'') Area
                  ,isnull(Areas.id,'') Areaid
	              ,isnull(Terceros.nombre,'') Tercero
              FROM Documentos
              left join ubicaciones
              on Documentos.id_ubicacion=ubicaciones.id
              inner join AdminTipoDocumento
              on Documentos.id_tipo=AdminTipoDocumento.id
              left join documentos_area
              on documentos.id=documentos_area.id_documento
              left join Areas
              on Documentos_Area.id_area=Areas.id
              left join Documentos_Terceros
              on Documentos.id=Documentos_Terceros.id_documento
              left join Terceros
              on Documentos_Terceros.id_tercero=Terceros.id
              where Documentos.id_sucursal=0">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
