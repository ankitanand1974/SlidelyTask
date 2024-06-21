Public Class ViewSubmissionsForm
    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0

    ' Declare controls
    Private WithEvents btnPrevious As Button
    Private WithEvents btnNext As Button
    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhone As TextBox
    Private txtGitHubLink As TextBox
    Private txtStopwatchTime As TextBox

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        InitializeControls()
        submissions = GetSubmissions()
        DisplaySubmission()
    End Sub

    Private Sub InitializeControls()
        ' Initialize controls if they're not created in the designer
        If btnPrevious Is Nothing Then
            btnPrevious = New Button()
            btnPrevious.Text = "Previous"
            Me.Controls.Add(btnPrevious)
        End If

        If btnNext Is Nothing Then
            btnNext = New Button()
            btnNext.Text = "Next"
            Me.Controls.Add(btnNext)
        End If

        ' Initialize TextBox controls
        txtName = New TextBox()
        txtEmail = New TextBox()
        txtPhone = New TextBox()
        txtGitHubLink = New TextBox()
        txtStopwatchTime = New TextBox()

        ' Add TextBox controls to the form
        Me.Controls.Add(txtName)
        Me.Controls.Add(txtEmail)
        Me.Controls.Add(txtPhone)
        Me.Controls.Add(txtGitHubLink)
        Me.Controls.Add(txtStopwatchTime)
    End Sub

    Private Function GetSubmissions() As List(Of Submission)
        ' Dummy data for now. Replace this with actual API call to backend
        Return New List(Of Submission) From {
            New Submission With {.Name = "John Doe", .Email = "john@example.com", .Phone = "1234567890", .GitHubLink = "http://github.com/johndoe", .StopwatchTime = "00:01:19"},
            New Submission With {.Name = "Jane Doe", .Email = "jane@example.com", .Phone = "0987654321", .GitHubLink = "http://github.com/janedoe", .StopwatchTime = "00:02:45"}
        }
    End Function

    Private Sub DisplaySubmission()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhone.Text = submission.Phone
            txtGitHubLink.Text = submission.GitHubLink
            txtStopwatchTime.Text = submission.StopwatchTime
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        End If
    End Sub
End Class

Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GitHubLink As String
    Public Property StopwatchTime As String
End Class