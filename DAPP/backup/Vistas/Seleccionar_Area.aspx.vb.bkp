Imports Infragistics.Web.UI.NavigationControls

Public Class Seleccionar_Area
    Inherits Page

    Private db As New DAPP_BDEntities


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Me.Page.IsPostBack Then
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            CargarArbol()
        End If
    End Sub

    Private Sub CargarArbol()
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        Dim Raices = From rz In db.Areas
                     Where rz.id_area Is Nothing And rz.id_sucursal = ids
                     Select rz

        For Each Raiz In Raices
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = Raiz.nombre
            FolderNode.Value = Raiz.id
            FolderNode.Expanded = True
            DocTree.Nodes.Add(FolderNode)
            AdjuntarHijos(FolderNode)
        Next

    End Sub


    Private Sub AdjuntarHijos(Node As DataTreeNode)

        Dim Hijos = (From hj In db.Areas
                     Where hj.id_area = Node.Value
                     Select hj)

        For Each Hijo In Hijos
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = Hijo.nombre
            FolderNode.Value = Hijo.id
            FolderNode.Expanded = True
            Node.Nodes.Add(FolderNode)
            AdjuntarHijos(FolderNode)
        Next

    End Sub

    Protected Sub bttSeleccionar_Click(sender As Object, e As EventArgs) Handles bttSeleccionar.Click
        If DocTree.SelectedNodes.Count > 0 Then
            Dim Node As DataTreeNode = DocTree.SelectedNodes(0)
            Dim CallbackFunction As String = "ActualizarSeleccionArea"
            Utilidades.RegistrarScript(String.Format("parent.window.opener.{0}({1},'{2}'); window.close();", CallbackFunction, Node.Value, Node.Text), Me)
        Else
            Utilidades.Mensaje("Debe seleccionar un Area", Me)
        End If
    End Sub
End Class