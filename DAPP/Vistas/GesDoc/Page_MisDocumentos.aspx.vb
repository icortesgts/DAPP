
Imports Infragistics.Web.UI.NavigationControls

Public Class Page_MisDocumentos
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
            If Session("id_sucursal") Is Nothing Then
                Session("id_sucursal") = 11
                Session("id_cliente") = 4
            End If
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            CargarArbol()
            If id_area <> -1 Then
                CallRecursive(DocTree, id_area)
            End If
        End If
    End Sub

    Private Sub CargarArbol()
        Dim ids As Long
        Dim Raices As List(Of Areas)
        Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
        ids = CLng(Session("id_sucursal"))
        If Usuario.id_perfil = 1 Or Usuario.Areas = True Then
            If id_area <> -1 Then
                Raices = (From rz In db.Areas
                          Where rz.id_area Is Nothing And rz.id_sucursal = ids And rz.id = id_area
                          Select rz).ToList

            Else
                Raices = (From rz In db.Areas
                          Where rz.id_area Is Nothing And rz.id_sucursal = ids
                          Select rz).ToList

            End If

            For Each Raiz In Raices
                Dim FolderNode As New DataTreeNode()
                FolderNode.Text = Raiz.nombre
                FolderNode.Value = Raiz.id
                DocTree.Nodes.Add(FolderNode)
                If Raiz.Color IsNot Nothing Then
                    FolderNode.Attributes.Add("style", "background-color:" + Raiz.Color + "")
                End If
                AdjuntarHijos(FolderNode)
            Next
        Else
            Dim trx = (From tr In db.Usuarios_Areas
                       Where tr.id_usuario = Usuario.ID
                       Select tr).ToList
            For Each ua In trx
                Raices = (From rz In db.Areas
                          Where rz.id_area Is Nothing And rz.id_sucursal = ids And rz.id = ua.id_area
                          Select rz).ToList
                For Each Raiz In Raices
                    Dim FolderNode As New DataTreeNode()
                    FolderNode.Text = Raiz.nombre
                    FolderNode.Value = Raiz.id
                    DocTree.Nodes.Add(FolderNode)
                    If Raiz.Color IsNot Nothing Then
                        FolderNode.Attributes.Add("style", "background-color:" + Raiz.Color + "")
                    End If
                    AdjuntarHijos(FolderNode)
                Next
            Next
        End If


    End Sub

    Private Sub AdjuntarHijos(Node As DataTreeNode)

        Dim Hijos = (From hj In db.Areas
                     Where hj.id_area = Node.Value
                     Select hj)

        For Each Hijo In Hijos
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = Hijo.nombre
            FolderNode.Value = Hijo.id
            If Hijo.Color IsNot Nothing Then
                FolderNode.Attributes.Add("style", "background-color:" + Hijo.Color + "")
            End If

            Node.Nodes.Add(FolderNode)
            AdjuntarHijos(FolderNode)
        Next

    End Sub

    Protected Sub DocTree_NodeClick(sender As Object, e As DataTreeNodeClickEventArgs) Handles DocTree.NodeClick

        Dim id_nodo As Long = e.Node.Value
        FileTree.Nodes.Clear()
        Dim Files = From fl In db.Documentos_Area.Include("Documentos")
                    Where fl.id_area = id_nodo
                    Select fl.Documentos

        For Each dc In Files

            Dim FolderNode As New DataTreeNode()
            If dc.Estado <> "Archivado" Then
                FolderNode.Text = dc.alias & " (" & dc.Estado & ")"
            Else
                FolderNode.Text = dc.alias
            End If

            FolderNode.Value = dc.id
            FileTree.Nodes.Add(FolderNode)
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