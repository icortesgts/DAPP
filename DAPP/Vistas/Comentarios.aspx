<%@ Page Title="Contactos" Language="VB" AutoEventWireup="true" CodeBehind="Comentarios.aspx.vb" Inherits="DAPP.Comentarios" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>COMENTARIOS CORRESPONDENCIA</title>
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

                <div class="container mrgnBotMd" style="width: 100%;">
                    <div class="row noPadding">
                        <div class="col-sm-12 noPadding colHeight">
                            <h3><asp:Label ID="lbTitulo" runat="server" Text="Contactos"  /></h3>
                            <ig:WebExcelExporter ID="ExpGrid" runat="server">
                            </ig:WebExcelExporter>
                            <ig:WebDocumentExporter ID="Exppdf" runat="server">
                            </ig:WebDocumentExporter>
                             <div class="DivFormulario">
                                <table style="margin: auto; width: 100%;">
                                    <tr>
                                        <td style="text-align: center; width: 40px;">&nbsp;</td>
                                        <td style="text-align: center; width: 40px;">
                                            <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Contacto" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="26" Height="26" />
                                        </td>
                                        <td style="text-align: center; width: 40px;">
                                            <asp:ImageButton ID="bttEditar" ToolTip="Editar Contacto" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="26" Height="26" />
                                        </td>
                                        <td style="text-align: center; width: 40px;">&nbsp;
                                            <asp:ImageButton ID="bttEliminar" ToolTip="Eliminar Contacto" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoDelete.png" BackColor="Transparent" Width="26" Height="26" OnClientClick="return confirm('Esta Seguro de Realizar esta Operación?');" />
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
                                            <ig:WebDataGrid ID="GridContactos" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Contactos" EnableAjax="False" DataKeyFields="id">
                                                <Columns>
                                                    <ig:BoundDataField DataFieldName="numero_radicado" Key="numero_radicado">
                                                        <Header Text="Número Radicado">
                                                        </Header>
                                                    </ig:BoundDataField>
                                               
                                                    <ig:BoundDataField DataFieldName="Fecha" Key="Fecha">
                                                        <Header Text="Fecha Comentario">
                                                        </Header>
                                                    </ig:BoundDataField>
                                                    <ig:BoundDataField DataFieldName="Comentario" Key="Comentario">
                                                        <Header Text="Comentario">
                                                        </Header>
                                                    </ig:BoundDataField>
                   
                                                    <ig:BoundDataField DataFieldName="nombre" Key="nombre">
                                                        <Header Text="Autor">
                                                        </Header>
                                                    </ig:BoundDataField>
                                                    
                                                </Columns>
                                                <Behaviors>
                                                    <ig:Selection CellClickAction="Row" SelectedCellCssClass="SelectedCellClass" RowSelectType="Single">
                                                        <AutoPostBackFlags RowSelectionChanged="True" />
                                                    </ig:Selection>
                                                    <ig:Sorting SortingMode="Single" Enabled="true">
                                                    </ig:Sorting>
                                                    <ig:Filtering>
                                                    </ig:Filtering>
                                                </Behaviors>
                                            </ig:WebDataGrid>
                                            <asp:SqlDataSource ID="Src_Contactos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Comentarios_Correspondencia.id
      ,id_correspondencia
      ,Fecha
      ,Comentario
      ,id_tercero
	  ,terceros.nombre
	  ,correspondencia.numero_radicado
  FROM Comentarios_Correspondencia inner join terceros
  on Comentarios_Correspondencia.id_tercero=terceros.id
  inner join correspondencia 
  on Comentarios_Correspondencia.id_correspondencia=correspondencia.id and correspondencia.id=@correspondencia
order by fecha desc">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter DefaultValue="0" Name="correspondencia" QueryStringField="id_Correspondencia" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:HiddenField ID="HCliente" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
