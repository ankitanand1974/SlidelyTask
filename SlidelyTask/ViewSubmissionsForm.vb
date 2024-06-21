Public Class ViewSubmissionsForm
    Private WithEvents btnPrevious As Button
    Private WithEvents btnNext As Button
    Private lblName, lblEmail, lblPhone, lblGithub, lblStopwatch As Label
    Private txtName, txtEmail, txtPhone, txtGithub, txtStopwatch As TextBox
    Private currentIndex As Integer = 0
    Private submissions As List(Of Submission) = New List(Of Submission)

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "John Doe, Slidely Task 2 - View Submissions"
        Me.KeyPreview = True

        ' Initialize controls
        InitializeControls()

        ' Load submissions (replace this with actual data loading)
        LoadDummyData()

        ' Display first submission
        DisplaySubmission()
    End Sub

    Private Sub InitializeControls()
        ' Labels
        lblName = CreateLabel("Name", 10, 10)
        lblEmail = CreateLabel("Email", 10, 40)
        lblPhone = CreateLabel("Phone Num", 10, 70)
        lblGithub = CreateLabel("Github Link For Task 2", 10, 100)
        lblStopwatch = CreateLabel("Stopwatch time", 10, 130)

        ' TextBoxes
        txtName = CreateTextBox(120, 10)
        txtEmail = CreateTextBox(120, 40)
        txtPhone = CreateTextBox(120, 70)
        txtGithub = CreateTextBox(120, 100)
        txtStopwatch = CreateTextBox(120, 130)

        ' Buttons
        btnPrevious = New Button()
        With btnPrevious
            .Text = "PREVIOUS (CTRL + P)"
            .Location = New Point(10, 170)
            .Size = New Size(150, 30)
            .BackColor = Color.Yellow
        End With
        Me.Controls.Add(btnPrevious)

        btnNext = New Button()
        With btnNext
            .Text = "NEXT (CTRL + N)"
            .Location = New Point(170, 170)
            .Size = New Size(150, 30)
            .BackColor = Color.LightBlue
        End With
        Me.Controls.Add(btnNext)
    End Sub

    Private Function CreateLabel(text As String, x As Integer, y As Integer) As Label
        Dim lbl As New Label()
        With lbl
            .Text = text
            .Location = New Point(x, y)
            .AutoSize = True
        End With
        Me.Controls.Add(lbl)
        Return lbl
    End Function

    Private Function CreateTextBox(x As Integer, y As Integer) As TextBox
        Dim txt As New TextBox()
        With txt
            .Location = New Point(x, y)
            .Size = New Size(200, 20)
            .ReadOnly = True
        End With
        Me.Controls.Add(txt)
        Return txt
    End Function

    Private Sub LoadDummyData()
        ' Replace this with actual data loading logic
        submissions.Add(New Submission("John Doe", "johndoe@gmail.com", "9876543210", "https://github.com/john_doe/my_slidely_task/", "00:01:19"))
        submissions.Add(New Submission("Jane Smith", "janesmith@gmail.com", "1234567890", "https://github.com/jane_smith/slidely_project/", "00:02:45"))
    End Sub

    Private Sub DisplaySubmission()
        If submissions.Count > 0 Then
            Dim submission As Submission = submissions(currentIndex)
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhone.Text = submission.Phone
            txtGithub.Text = submission.GitHubLink
            txtStopwatch.Text = submission.StopwatchTime
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
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.P
                    btnPrevious.PerformClick()
                Case Keys.N
                    btnNext.PerformClick()
            End Select
        End If
    End Sub
End Class

Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GitHubLink As String
    Public Property StopwatchTime As String

    Public Sub New(name As String, email As String, phone As String, gitHubLink As String, stopwatchTime As String)
        Me.Name = name
        Me.Email = email
        Me.Phone = phone
        Me.GitHubLink = gitHubLink
        Me.StopwatchTime = stopwatchTime
    End Sub
End Class