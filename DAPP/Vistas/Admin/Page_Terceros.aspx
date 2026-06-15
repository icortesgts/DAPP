<%@ Page Title="Terceros" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Terceros.aspx.vb" Inherits="DAPP.Page_Terceros" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">ADMINISTRACION DE TERCEROS </span>



                <ig:WebExcelExporter ID="ExpGrid" runat="server">
        </ig:WebExcelExporter>
        <ig:WebDocumentExporter ID="Exppdf" runat="server">
        </ig:WebDocumentExporter>


    </div>
    <br />
    <br />
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Tercero" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Tercero" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Tercero" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttContacto" ToolTip="Contactos Tercero" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/user.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridTerceros" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Terceros" DataKeyFields="id" EnableAjax="False">
                        <Columns>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                <Header Text="Nombre">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="tipo_documento" Key="tipo_documento">
                                <Header Text="Tipo Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="numero_documento" Key="numero_documento">
                                <Header Text="Número Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="sector" Key="sector">
                                <Header Text="Sector">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Actividad" Key="Actividad">
                                <Header Text="Actividad">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Tipo" Key="Tipo">
                                <Header Text="Tipo Tercero">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="direccion" Key="direccion">
                                <Header Text="Dirección">
                                </Header>
                            </ig:BoundDataField>                                                 
                            <ig:BoundDataField DataFieldName="telefono" Key="telefono">
                                <Header Text="Teléfono">
                                </Header>
                            </ig:BoundDataField>  
                            <ig:BoundDataField DataFieldName="celular" Key="celular">
                                <Header Text="Celular">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="correo_electronico" Key="correo_electronico">
                                <Header Text="Correo Electrónico">
                                </Header>
                            </ig:BoundDataField>
                            
                            <ig:BoundCheckBoxField DataFieldName="Activo" Key="Activo">
                                <Header Text="Activo">
                                </Header>
                            </ig:BoundCheckBoxField>
                            
                            
                        </Columns>
                        <Behaviors>
                            <ig:Selection CellClickAction="Row" SelectedCellCssClass="SelectedCellClass" RowSelectType="Single">
                                <AutoPostBackFlags RowSelectionChanged="True" />
                            </ig:Selection>
                            <ig:Sorting SortingMode="Single" Enabled="true">
                            </ig:Sorting>
                            <ig:Filtering>
                            </ig:Filtering>
                            <ig:Paging PageSize="50">
                            </ig:Paging>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="Src_Terceros" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [nombre]
      ,[tipo_documento]
      ,[numero_documento]
      ,[direccion]
      ,[telefono]
      ,[celular]
      ,[correo_electronico]
      ,[fecha_creacion]
      ,[usuario]
      ,[Terceros].[id_cliente]
      ,[Terceros].[id]
      ,[Terceros].[id_sucursal]
      ,[Terceros].[Activo]
      ,[id_tipo]
      ,[id_actividad]
	  ,sector
	  ,AdminActividad.Actividad
	  ,AdminTipoTercero.Tipo
  FROM [Terceros] inner join AdminActividad 
  on terceros.id_actividad=AdminActividad.id
  inner join AdminTipoTercero
  on terceros.id_tipo=AdminTipoTercero.id
  where [Terceros].[id_cliente]=@cliente
  order by nombre">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="cliente" SessionField="id_cliente" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="HCliente" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
     <div class="titular-into-form">
        <span class="texto-titulo-form">SUCURSALES</span>
    </div>
    <br />
    <br />
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="BttNew" ToolTip="Nuevo Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">
                    <asp:ImageButton ID="BttEdit" ToolTip="Editar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttDelete" ToolTip="Eliminar Registro" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de realizar esta operación?');" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttContact" ToolTip="Contactos Sucursal" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/user.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttExc1" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="Bttpd1" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridSucursales" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Sucursales" DataKeyFields="id">
                        <Columns>
                            <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                <Header Text="Nombre">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="direccion" Key="direccion">
                                <Header Text="Dirección">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="telefono" Key="telefono">
                                <Header Text="Teléfono">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="correo_electronico" Key="correo_electronico">
                                <Header Text="Correo Electrónico">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundCheckBoxField DataFieldName="Activo" Key="Activo">
                                <Header Text="Activo">
                                </Header>
                            </ig:BoundCheckBoxField>
                            
                            
                        </Columns>
                        <Behaviors>
                            <ig:Selection CellClickAction="Row" SelectedCellCssClass="SelectedCellClass" RowSelectType="Single">
                            </ig:Selection>
                            <ig:Sorting SortingMode="Single" Enabled="true">
                            </ig:Sorting>
                            <ig:Filtering>
                            </ig:Filtering>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="Src_Sucursales" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="  SELECT [nombre]
      ,[direccion]
      ,[telefono]
      ,[correo_electronico]
      ,[id]
      ,[id_tercero]
      ,[Activo]
  FROM [SucursalTerceros]
where id_tercero=@tercero
order by nombre">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HiddenField1" DefaultValue="0" Name="tercero" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
        </table>
    </div>    



</asp:Content>



