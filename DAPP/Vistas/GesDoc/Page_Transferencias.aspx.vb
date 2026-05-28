
Imports Infragistics.Web.UI.NavigationControls

Public Class Page_Transferencias
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
            txtFechaInicio.Text = Format(Now.Date(), "yyyy-MM-dd")

            CargarArbol()
            'If id_area <> -1 Then
            '    CallRecursive(DocTree, id_area)
            'End If
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
            DocTreeD.Nodes.Add(FolderNode)
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

        Dim id_nodo As Long = e.Node.Value
        FileTree.Nodes.Clear()
        Dim Files = From fl In db.Documentos
                    Where fl.id_ubicacion = id_nodo
                    Select fl

        For Each dc In Files
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = dc.alias
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

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim ids As Long
        If txtFechaInicio.Text = "" Then
            Utilidades.Mensaje("Debe Registrar una Fecha de Transferencia", Me)
        Else
            If Session("id_sucursal") IsNot Nothing Then
                ids = Session("id_sucursal")
            Else
                ids = 0
            End If
            If DocTree.SelectedNodes.Count > 0 And DocTreeD.SelectedNodes.Count > 0 Then
                If DocTree.SelectedNodes(0).Value <> DocTreeD.SelectedNodes(0).Value Then
                    If FileTree.SelectedNodes.Count > 0 Then
                        For Each nx In FileTree.SelectedNodes
                            Dim id_nodo As Long = nx.Value
                            registro_transferencia(CLng(DocTree.SelectedNodes(0).Value), CLng(DocTreeD.SelectedNodes(0).Value), id_nodo, CDate(txtFechaInicio.Text), User.Identity.Name)
                            Dim dest As Long = DocTreeD.SelectedNodes(0).Value
                            Dim doc = From cl In db.Documentos
                                      Where cl.id = id_nodo
                                      Select cl
                            For Each dc In doc
                                dc.id_ubicacion = dest
                                Dim ubx = (From ub In db.Ubicaciones
                                           Where ub.id = dest
                                           Select ub).FirstOrDefault
                                Utilidades.RegistrarAuditoria("Documentos", dc.id, dc.alias, "Actualización Ubicación - " & txtFechaInicio.Text, User.Identity.Name, ids)
                                If ubx IsNot Nothing Then
                                    If ubx.Archivo = "Archivo Central" And dc.FCentral Is Nothing Then
                                        dc.FCentral = txtFechaInicio.Text
                                    ElseIf ubx.Archivo = "Archivo Histórico" And dc.FHistorico Is Nothing Then
                                        dc.FHistorico = txtFechaInicio.Text
                                    End If
                                End If

                            Next
                            db.SaveChanges()
                            id_nodo = DocTree.SelectedNodes(0).Value
                            FileTree.Nodes.Clear()
                            Dim Files = From fl In db.Documentos
                                        Where fl.id_ubicacion = id_nodo
                                        Select fl

                            For Each dc In Files
                                Dim FolderNode As New DataTreeNode()
                                FolderNode.Text = dc.alias
                                FolderNode.Value = dc.id
                                FileTree.Nodes.Add(FolderNode)
                            Next
                            FileTreeD.Nodes.Clear()
                            Dim FilesD = From fl In db.Documentos
                                         Where fl.id_ubicacion = dest
                                         Select fl

                            For Each dc In FilesD
                                Dim FolderNode As New DataTreeNode()
                                FolderNode.Text = dc.alias
                                FolderNode.Value = dc.id
                                FileTreeD.Nodes.Add(FolderNode)
                            Next
                        Next
                    Else
                        Dim id_nodo As Long = DocTree.SelectedNodes(0).Value
                        Dim dest As Long = DocTreeD.SelectedNodes(0).Value
                        Dim doc = From cl In db.Documentos
                                  Where cl.id_ubicacion = id_nodo
                                  Select cl
                        For Each dc In doc
                            dc.id_ubicacion = dest
                            registro_transferencia(id_nodo, dest, dc.id, CDate(txtFechaInicio.Text), User.Identity.Name)

                            Dim ubx = (From ub In db.Ubicaciones
                                       Where ub.id = dest
                                       Select ub).FirstOrDefault
                            Utilidades.RegistrarAuditoria("Documentos", dc.id, dc.alias, "Actualización Ubicación - " & txtFechaInicio.Text, User.Identity.Name, ids)
                            If ubx IsNot Nothing Then
                                If ubx.Archivo = "Archivo Central" And dc.FCentral Is Nothing Then
                                    dc.FCentral = txtFechaInicio.Text
                                ElseIf ubx.Archivo = "Archivo Histórico" And dc.FHistorico Is Nothing Then
                                    dc.FHistorico = txtFechaInicio.Text
                                End If
                            End If
                        Next
                        db.SaveChanges()
                        Utilidades.ActualizaCapacidadUbicacion(id_nodo)
                        Utilidades.ActualizaCapacidadUbicacion(dest)
                        FileTree.Nodes.Clear()
                        Dim Files = From fl In db.Documentos
                                    Where fl.id_ubicacion = id_nodo
                                    Select fl

                        For Each dc In Files
                            Dim FolderNode As New DataTreeNode()
                            FolderNode.Text = dc.alias
                            FolderNode.Value = dc.id
                            FileTree.Nodes.Add(FolderNode)
                        Next
                        FileTreeD.Nodes.Clear()
                        Dim FilesD = From fl In db.Documentos
                                     Where fl.id_ubicacion = dest
                                     Select fl

                        For Each dc In FilesD
                            Dim FolderNode As New DataTreeNode()
                            FolderNode.Text = dc.alias
                            FolderNode.Value = dc.id
                            FileTreeD.Nodes.Add(FolderNode)
                        Next
                    End If

                Else
                    Utilidades.Mensaje("Origen y Destino, tienen que ser diferentes", Me)
                End If
            Else
                If DocTree.SelectedNodes.Count > 0 Then
                    Utilidades.Mensaje("Tiene que Escoger el Destino", Me)
                Else
                    Utilidades.Mensaje("Tiene que Escoger el Origen", Me)
                End If
            End If
        End If

    End Sub

    Protected Sub DocTreeD_NodeClick(sender As Object, e As DataTreeNodeClickEventArgs) Handles DocTreeD.NodeClick

        Dim id_nodo As Long = e.Node.Value
        FileTreeD.Nodes.Clear()
        Dim Files = From fl In db.Documentos
                    Where fl.id_ubicacion = id_nodo
                    Select fl

        For Each dc In Files
            Dim FolderNode As New DataTreeNode()
            FolderNode.Text = dc.alias
            FolderNode.Value = dc.id
            FileTreeD.Nodes.Add(FolderNode)
        Next
    End Sub

    Protected Sub FileTreeD_NodeClick(sender As Object, e As DataTreeNodeClickEventArgs) Handles FileTreeD.NodeClick
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
    Protected Sub registro_transferencia(id_origen As Long, id_destino As Long, id_documento As Long, fecha As DateTime, usuario As String)
        Dim trx As New Transferencias
        trx.id_origen = id_origen
        trx.id_destino = id_destino
        trx.id_documento = id_documento
        trx.fecha = fecha
        trx.usuario = usuario
        db.Transferencias.Add(trx)
    End Sub
End Class