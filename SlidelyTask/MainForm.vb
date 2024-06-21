Public Class MainForm
    ' Declare the buttons as WithEvents
    Private WithEvents btnViewSubmissions As Button
    Private WithEvents btnCreateNewSubmission As Button

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateNewSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateNewSubmission.Click
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateNewSubmission.PerformClick()
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        ' Initialize the buttons if they're not created in the designer
        If btnViewSubmissions Is Nothing Then
            btnViewSubmissions = New Button()
            btnViewSubmissions.Text = "VIEW SUBMISSIONS (CTRL + V)"
            btnViewSubmissions.BackColor = Color.Yellow
            Me.Controls.Add(btnViewSubmissions)
        End If

        If btnCreateNewSubmission Is Nothing Then
            btnCreateNewSubmission = New Button()
            btnCreateNewSubmission.Text = "CREATE NEW SUBMISSION (CTRL + N)"
            btnCreateNewSubmission.BackColor = Color.LightBlue
            Me.Controls.Add(btnCreateNewSubmission)
        End If
    End Sub
End Class