Imports Infragistics.Web.UI.NavigationControls

Public Class Seleccionar_Ubicacion
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
        Dim Raices = From rz In db.Ubicaciones.Include("AdminTipoUbicacion")
                     Where rz.id_ubicacion Is Nothing And rz.id_sucursal = ids
                     Select rz

        For Each Raiz In Raices
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = Raiz.ubicacion
            FolderNode.Value = Raiz.id
            FolderNode.ToolTip = Raiz.AdminTipoUbicacion.nombre
            FolderNode.Expanded = True
            UbTree.Nodes.Add(FolderNode)
            AdjuntarHijos(FolderNode)
        Next

    End Sub


    Private Sub AdjuntarHijos(Node As DataTreeNode)

        Dim Hijos = (From hj In db.Ubicaciones.Include("AdminTipoUbicacion")
                     Where hj.id_ubicacion = Node.Value
                     Select hj)

        For Each Hijo In Hijos
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = Hijo.ubicacion
            FolderNode.Value = Hijo.id
            FolderNode.ToolTip = Hijo.AdminTipoUbicacion.nombre
            FolderNode.Expanded = True
            Node.Nodes.Add(FolderNode)
            AdjuntarHijos(FolderNode)
        Next

    End Sub

    Protected Sub bttSeleccionar_Click(sender As Object, e As EventArgs) Handles bttSeleccionar.Click
        If UbTree.SelectedNodes.Count > 0 Then
            Dim Node As DataTreeNode = UbTree.SelectedNodes(0)
            Dim CallbackFunction As String = "ActualizarSeleccionUbicacion"
            Utilidades.RegistrarScript(String.Format("parent.window.opener.{0}({1},'{2}'); window.close();", CallbackFunction, Node.Value, Node.Text), Me)
        Else
            Utilidades.Mensaje("Debe seleccionar una Ubicacion", Me)
        End If
    End Sub
End Class