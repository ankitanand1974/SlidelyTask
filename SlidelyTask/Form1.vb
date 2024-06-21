Public Class Form1
    Private WithEvents btnViewSubmissions As Button
    Private WithEvents btnCreateNewSubmission As Button

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Slidely Task 2 - Slidely Form App"
        Me.KeyPreview = True

        btnViewSubmissions = New Button()
        With btnViewSubmissions
            .Text = "VIEW SUBMISSIONS (CTRL + V)"
            .Location = New Point(10, 10)
            .Size = New Size(250, 30)
            .BackColor = Color.Yellow
        End With
        Me.Controls.Add(btnViewSubmissions)

        btnCreateNewSubmission = New Button()
        With btnCreateNewSubmission
            .Text = "CREATE NEW SUBMISSION (CTRL + N)"
            .Location = New Point(10, 50)
            .Size = New Size(250, 30)
            .BackColor = Color.LightBlue
        End With
        Me.Controls.Add(btnCreateNewSubmission)
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateNewSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateNewSubmission.Click
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.V
                    btnViewSubmissions.PerformClick()
                Case Keys.N
                    btnCreateNewSubmission.PerformClick()
            End Select
        End If
    End Sub
End Class