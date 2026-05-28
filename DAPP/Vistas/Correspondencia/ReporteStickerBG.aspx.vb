Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms

Public Class ReporteStickerBG
    Inherits System.Web.UI.Page

    Private ReadOnly Property id_correspondencia As Long
        Get
            If Me.ClientQueryString.Contains("id_correspondencia") Then
                Return CLng(Request.QueryString.Get("id_correspondencia"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            VisorReporte.ProcessingMode = ProcessingMode.Local
            VisorReporte.LocalReport.ReportPath = Server.MapPath("StickerBG.rdlc")

            Dim ds As New DS_Stickers
            Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString())

            cnn.Open()

            Dim adp As New SqlDataAdapter("", cnn)
            adp.SelectCommand.CommandText = "GetStickerCorrespondencia"
            adp.SelectCommand.CommandType = CommandType.StoredProcedure

            'Asignacion de parámetros
            adp.SelectCommand.Parameters.Add("@id_correspondencia", SqlDbType.BigInt).Value = id_correspondencia


            ' Captura de tablas
            adp.TableMappings.Add("Table", "StickerCorrespondencia")

            ' Execute
            adp.Fill(ds)

            cnn.Close()

            If ds.Tables.Count > 0 Then
                Dim ReporteDataSource As ReportDataSource = New ReportDataSource("DS_Sticker", ds.Tables("StickerCorrespondencia"))
                VisorReporte.LocalReport.DataSources.Clear()
                VisorReporte.LocalReport.DataSources.Add(ReporteDataSource)
            End If


        End If
    End Sub

End Class