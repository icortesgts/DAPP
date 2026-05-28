
Imports Infragistics.Web.UI.NavigationControls

Public Class Page_Archivo
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities
    Private ReadOnly Property id_area As Long
        Get
            If Me.ClientQueryString.Contains("id_area") Then
                Return CLng(Request.QueryString.Get("id_area"))
            Else
                Return -1
            End If
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
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
        Dim Raices = From rz In db.Ubicaciones
                     Where rz.id_ubicacion Is Nothing And rz.id_sucursal = ids
                     Select rz

        For Each Raiz In Raices
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = Raiz.ubicacion
            FolderNode.Value = Raiz.id
            DocTree.Nodes.Add(FolderNode)
            AdjuntarHijos(FolderNode)
        Next

    End Sub

    Private Sub AdjuntarHijos(Node As DataTreeNode)

        Dim Hijos = (From hj In db.Ubicaciones
                     Where hj.id_ubicacion = Node.Value
                     Select hj)

        For Each Hijo In Hijos
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = Hijo.ubicacion
            FolderNode.Value = Hijo.id
            FolderNode.CssClass = Node.CssClass
            Node.Nodes.Add(FolderNode)
            AdjuntarHijos(FolderNode)
        Next

    End Sub

    Protected Sub DocTree_NodeClick(sender As Object, e As DataTreeNodeClickEventArgs) Handles DocTree.NodeClick
        Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
        Dim x As Integer

        Dim id_nodo As Long = e.Node.Value
        FileTree.Nodes.Clear()
        Dim Files = From fl In db.Documentos
                    Where fl.id_ubicacion = id_nodo
                    Select fl

        For Each dc In Files
            If Usuario.id_perfil = 1 Or Usuario.Areas = True Then
                Dim FolderNode As New DataTreeNode()
                If dc.Estado <> "Archivado" Then
                    FolderNode.Text = dc.alias & " (" & dc.Estado & ")"
                Else
                    FolderNode.Text = dc.alias
                End If

                FolderNode.Value = dc.id
                FileTree.Nodes.Add(FolderNode)
            Else
                x = 0
                Dim dx = (From tr In db.Documentos_Area
                          Where tr.id_documento = dc.id
                          Select tr).ToList
                For Each da In dx
                    Dim trx = (From tr In db.Usuarios_Areas
                               Where tr.id_usuario = Usuario.ID And tr.id_area = da.id_area
                               Select tr).FirstOrDefault
                    If trx IsNot Nothing Then
                        x = x + 1
                    End If
                Next
                If x > 0 Then
                    Dim FolderNode As New DataTreeNode()
                    If dc.Estado <> "Archivado" Then
                        FolderNode.Text = dc.alias & " (" & dc.Estado & ")"
                    Else
                        FolderNode.Text = dc.alias
                    End If

                    FolderNode.Value = dc.id
                    FileTree.Nodes.Add(FolderNode)
                End If


            End If
        Next

    End Sub

    Protected Sub FileTree_NodeClick(sender As Object, e As DataTreeNodeClickEventArgs) Handles FileTree.NodeClick
        Dim id_nodo As Long = e.Node.Value

        Dim doc = (From cl In db.Documentos
                   Where cl.id = id_nodo
                   Select cl).FirstOrDefault

        If doc IsNot Nothing Then
            Dim lnk As String
            lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/" & doc.nombre
            Utilidades.RegistrarScript("window.open('" & lnk & "','Documento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
        End If

    End Sub
    ' Visual Basic
    Private Sub PrintRecursive(ByVal n As DataTreeNode, id As Long)

        Dim aNode As DataTreeNode
        For Each aNode In n.Nodes
            If aNode.Value = id Then
                aNode.Selected = True
                aNode.Expanded = True
            Else
                PrintRecursive(aNode, id)
            End If

        Next
    End Sub

    ' Call the procedure using the top nodes of the treeview.
    Private Sub CallRecursive(ByVal aTreeView As WebDataTree, id As Long)
        Dim n As DataTreeNode
        For Each n In aTreeView.Nodes
            If n.Value = id Then
                n.Selected = True
                n.Expanded = True
            Else
                PrintRecursive(n, id)
            End If
        Next
    End Sub

End Class