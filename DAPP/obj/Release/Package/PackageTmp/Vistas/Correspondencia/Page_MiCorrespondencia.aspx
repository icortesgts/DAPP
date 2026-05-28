<%@ Page Title="Correspondencia" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_MiCorrespondencia.aspx.vb" Inherits="DAPP.Page_MiCorrespondencia" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">MI CORRESPONDENCIA</span>
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
                    <asp:ImageButton ID="bttEditar" ToolTip="Editar Correspondencia" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Correspondencia" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" />
                </td>                
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="BttComentarios" ToolTip="Comentarios Correspondencia" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/fileopen.png" BackColor="Transparent" Width="26" Height="26" />
                </td>   
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridCorrespondencia" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataKeyFields="id"  DataSourceID="Src_Correspondencia">
                        <Columns>
                            <ig:BoundDataField DataFieldName="tipo_correspondencia" Key="tipo_correspondencia">
                                <Header Text="Tipo Correspondencia">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="numero_radicado" Key="numero_radicado">
                                <Header Text="Radicado">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fecha_documento" Key="fecha_documento">
                                <Header Text="Fecha Correspondencia">
                                </Header>
                            </ig:BoundDataField>                            
                            <ig:BoundDataField DataFieldName="asunto" Key="asunto">
                                <Header Text="Asunto">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Documento" Key="Documento">
                                <Header Text="Documento">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Destinatario" Key="Destinatario">
                                <Header Text="Destinatario">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Remitente1" Key="Remitente1">
                                <Header Text="Remitente">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="estado" Key="estado">
                                <Header Text="Estado">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="observaciones" Key="observaciones">
                                <Header Text="Observaciones">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Responsable" Key="Responsable">
                                <Header Text="Responsable">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="tipo_envio" Key="tipo_envio">
                                <Header Text="Envio / Recibo">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Mensajeria" Key="Mensajeria">
                                <Header Text="Mensajeria">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="guia" Key="guia">
                                <Header Text="Guia">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="usuario" Key="usuario">
                                <Header Text="Usuario Registro">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="fecha_sistema" Key="fecha_sistema">
                                <Header Text="Fecha Registro">
                                </Header>
                            </ig:BoundDataField>                            
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
                    <asp:SqlDataSource ID="Src_Correspondencia" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT numero_radicado
      ,fecha_documento
      ,fecha_sistema
      ,ciudad_remitente
      ,tipo_ingreso
      ,tipo_correspondencia
      ,remitente
      ,dependencia_remitente
      ,asunto
      ,sticker
      ,Correspondencia.id_sucursal
      ,Correspondencia.id
      ,tipo_envio
      ,guia
      ,Correspondencia.usuario
      ,observaciones
	  ,id_mensajeria
	  ,id_responsable
      ,Correspondencia.estado as estado
	  ,isnull(Documentos.alias,'Pendiente') as Documento
	  ,isnull(terceros.nombre,ciudad_remitente) as Destinatario
	  ,isnull(Rems.nombre,ciudad_remitente) as Remitente
	  ,Resp.nombre as Responsable
	  ,Msg.nombre as Mensajeria
      ,AdminTipoDocumento.nombre As TipoDocumento
  FROM Correspondencia
  left join terceros_correspondencia 
  on terceros_correspondencia.id_correspondencia=Correspondencia.id and terceros_correspondencia.tipo='Destinatario'
  left join terceros
  on terceros_correspondencia.id_tercero=terceros.id
  left join terceros_correspondencia as tcr
  on tcr.id_correspondencia=Correspondencia.id and tcr.tipo='Remitente'
  left join terceros as Rems
  on tcr.id_tercero=Rems.id
  left join Documentos_Correspondencia
  on Documentos_Correspondencia.id_correspondencia=Correspondencia.id 
  left join Documentos
  on Documentos_Correspondencia.id_documento=Documentos.id 
  inner join Terceros as Resp
  on correspondencia.id_responsable = Resp.id
  left join Terceros as Msg
  on correspondencia.id_mensajeria = Msg.id
  inner join AdminTipoDocumento
  on correspondencia.id_tipodoc = AdminTipoDocumento.id                       
WHERE Correspondencia.id_sucursal = @Sucursal and (correspondencia.id_responsable=@responsable or terceros_correspondencia.id_tercero=@responsable or tcr.id_tercero=@responsable)
ORDER BY fecha_documento desc">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="sucursal" SessionField="id_sucursal" />
                            <asp:ControlParameter ControlID="Hdd_id_responsable" DefaultValue="0" Name="responsable" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="Hdd_id_responsable" runat="server" Value="0" />
</asp:Content>



