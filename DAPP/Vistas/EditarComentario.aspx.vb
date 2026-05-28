Public Class EditarComentario
    Inherits System.Web.UI.Page


    Private ContactoEditado As Comentarios_Correspondencia
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_correspondencia As Long
        Get
            If Me.ClientQueryString.Contains("id_correspondencia") Then
                Return CLng(Request.QueryString.Get("id_correspondencia"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_comentario As Long
        Get
            If Me.ClientQueryString.Contains("id_comentario") Then
                Return CLng(Request.QueryString.Get("id_comentario"))
            Else
                Return -1
            End If
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If id_correspondencia = -1 Then
                Utilidades.Mensaje("Información no Asociada", Me)
                Response.Redirect("../Vistas/Comentarios.aspx?id_correspondencia=-1")
            Else
                Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
                If Usuario Is Nothing Then
                    Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
                End If

                If id_comentario <> -1 Then

                    ContactoEditado = (From tr In db.Comentarios_Correspondencia.Include("Terceros")
                                       Where tr.id = id_comentario
                                       Select tr).FirstOrDefault

                    If ContactoEditado IsNot Nothing Then
                        BoundTercero(ContactoEditado)
                    End If
                Else
                    lblfecha.Text = Format(Now, "dd/MM/yyyy hh:mm:ss tt")
                    lblAutor.Text = Utilidades.ConsultarUsuario(User.Identity.Name).nombre
                End If
            End If
        End If
    End Sub

    Private Sub BoundTercero(ContactoEditado As Comentarios_Correspondencia)


        lblfecha.Text = ContactoEditado.Fecha
        lblAutor.Text = ContactoEditado.Terceros.nombre
        txtNombre.Text = ContactoEditado.Comentario

    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click


        If id_comentario = -1 Then
            ContactoEditado = New Comentarios_Correspondencia
            ContactoEditado.id_correspondencia = id_correspondencia
            ContactoEditado.Fecha = Now()
            db.Comentarios_Correspondencia.Add(ContactoEditado)
        Else
            ContactoEditado = (From tr In db.Comentarios_Correspondencia
                               Where tr.id = id_comentario
                               Select tr).FirstOrDefault
        End If





        If ContactoEditado IsNot Nothing Then



            ContactoEditado.Comentario = txtNombre.Text
            ContactoEditado.id_tercero = Utilidades.ConsultarUsuario(User.Identity.Name).id_tercero
        End If

        db.SaveChanges()
        Utilidades.Mensaje("Comentario Guardado", Me)
        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        If id_comentario = -1 Then
            Utilidades.RegistrarAuditoria("Comentarios", ContactoEditado.id, ContactoEditado.Comentario, "Nuevo Registro", User.Identity.Name, ids)
        Else
            Utilidades.RegistrarAuditoria("Comentarios", ContactoEditado.id, ContactoEditado.Comentario, "Actualización Registro", User.Identity.Name, ids)
        End If

        Response.Redirect("../Vistas/Comentarios.aspx?id_correspondencia=" & id_correspondencia)






    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Vistas/Comentarios.aspx?id_correspondencia=" & id_correspondencia)
    End Sub

End Class