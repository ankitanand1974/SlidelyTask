Imports System.Net
Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Private currentIndex As Integer = 0
    Private btnPrevious As Button
    Private btnNext As Button
    Private lblName, lblEmail, lblPhone, lblGithub, lblStopwatch As Label
    Private txtName, txtEmail, txtPhone, txtGithub, txtStopwatch As TextBox

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "John Doe, Slidely Task 2 - View Submissions"
        Me.KeyPreview = True

        InitializeControls()
        Await DisplaySubmission()
    End Sub

    Private Async Function DisplaySubmission() As Task
        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync($"http://localhost:3000/read?index={currentIndex}")
                If response.IsSuccessStatusCode Then
                    Dim submissionJson As String = Await response.Content.ReadAsStringAsync()
                    Dim submission As Submission = JsonConvert.DeserializeObject(Of Submission)(submissionJson)

                    UpdateFormFields(submission)
                    UpdateButtonStates()
                ElseIf response.StatusCode = HttpStatusCode.NotFound Then
                    HandleNotFoundResponse()
                Else
                    ShowErrorMessage($"Error retrieving submission: {response.StatusCode}")
                End If
            Catch ex As Exception
                ShowErrorMessage($"An error occurred: {ex.Message}")
            End Try
        End Using
    End Function

    Private Sub UpdateFormFields(submission As Submission)
        txtName.Text = submission.name
        txtEmail.Text = submission.email
        txtPhone.Text = submission.phone
        txtGithub.Text = submission.github_link
        txtStopwatch.Text = submission.stopwatch_time
    End Sub

    Private Sub UpdateButtonStates()
        btnPrevious.Enabled = currentIndex > 0
        btnNext.Enabled = True
    End Sub

    Private Sub HandleNotFoundResponse()
        If currentIndex > 0 Then
            currentIndex -= 1
            btnNext.Enabled = False
        Else
            MessageBox.Show("No submissions found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ShowErrorMessage(message As String)
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Async Sub btnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            Await DisplaySubmission()
        End If
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs)
        currentIndex += 1
        Await DisplaySubmission()
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
        btnPrevious = CreateButton("PREVIOUS (CTRL + P)", 10, 170, Color.Yellow, AddressOf btnPrevious_Click)
        btnNext = CreateButton("NEXT (CTRL + N)", 170, 170, Color.LightBlue, AddressOf btnNext_Click)
    End Sub

    Private Function CreateLabel(text As String, x As Integer, y As Integer) As Label
        Dim lbl As New Label() With {
            .Text = text,
            .Location = New Point(x, y),
            .AutoSize = True
        }
        Me.Controls.Add(lbl)
        Return lbl
    End Function

    Private Function CreateTextBox(x As Integer, y As Integer) As TextBox
        Dim txt As New TextBox() With {
            .Location = New Point(x, y),
            .Size = New Size(200, 20),
            .ReadOnly = True
        }
        Me.Controls.Add(txt)
        Return txt
    End Function

    Private Function CreateButton(text As String, x As Integer, y As Integer, backColor As Color, clickHandler As EventHandler) As Button
        Dim btn As New Button() With {
            .Text = text,
            .Location = New Point(x, y),
            .Size = New Size(150, 30),
            .BackColor = backColor
        }
        AddHandler btn.Click, clickHandler
        Me.Controls.Add(btn)
        Return btn
    End Function

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
    Public Property name As String
    Public Property email As String
    Public Property phone As String
    Public Property github_link As String
    Public Property stopwatch_time As String
End Class