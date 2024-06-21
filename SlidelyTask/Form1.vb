Public Class Form1
    ' Declare the buttons as WithEvents
    Private WithEvents btnViewSubmissions As Button
    Private WithEvents btnCreateNewSubmission As Button

    ' Rest of your code remains the same
    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateNewSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateNewSubmission.Click
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateNewSubmission.PerformClick()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        ' Initialize the buttons if they're not created in the designer
        If btnViewSubmissions Is Nothing Then
            btnViewSubmissions = New Button()
            btnViewSubmissions.Text = "View Submissions"
            Me.Controls.Add(btnViewSubmissions)
        End If

        If btnCreateNewSubmission Is Nothing Then
            btnCreateNewSubmission = New Button()
            btnCreateNewSubmission.Text = "Create New Submission"
            Me.Controls.Add(btnCreateNewSubmission)
        End If
    End Sub
End Class