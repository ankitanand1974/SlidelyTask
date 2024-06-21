Imports System.Diagnostics
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Private WithEvents btnToggleStopwatch As Button
    Private WithEvents btnSubmit As Button
    Private lblName, lblEmail, lblPhone, lblGithub, lblStopwatch As Label
    Private WithEvents txtName, txtEmail, txtPhone, txtGithub As TextBox
    Private txtStopwatch As TextBox
    Private stopwatch As New Stopwatch()

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "John Doe, Slidely Task 2 - Create Submission"
        Me.KeyPreview = True

        ' Initialize controls
        InitializeControls()
    End Sub

    Private Sub InitializeControls()
        ' Labels
        lblName = CreateLabel("Name", 10, 10)
        lblEmail = CreateLabel("Email", 10, 40)
        lblPhone = CreateLabel("Phone Num", 10, 70)
        lblGithub = CreateLabel("Github Link For Task 2", 10, 100)
        lblStopwatch = CreateLabel("Stopwatch time", 10, 130)

        ' TextBoxes
        txtName = CreateTextBox(150, 10)
        txtEmail = CreateTextBox(150, 40)
        txtPhone = CreateTextBox(150, 70)
        txtGithub = CreateTextBox(150, 100)
        txtStopwatch = CreateTextBox(150, 130)
        txtStopwatch.ReadOnly = True

        ' Buttons
        btnToggleStopwatch = New Button()
        With btnToggleStopwatch
            .Text = "TOGGLE STOPWATCH (CTRL + T)"
            .Location = New Point(10, 170)
            .Size = New Size(200, 30)
            .BackColor = Color.Yellow
        End With
        Me.Controls.Add(btnToggleStopwatch)

        btnSubmit = New Button()
        With btnSubmit
            .Text = "SUBMIT (CTRL + S)"
            .Location = New Point(10, 210)
            .Size = New Size(340, 30)
            .BackColor = Color.LightBlue
        End With
        Me.Controls.Add(btnSubmit)
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
        End With
        Me.Controls.Add(txt)
        Return txt
    End Function

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        ToggleStopwatch()
    End Sub

    Private Sub ToggleStopwatch()
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            btnToggleStopwatch.Text = "Resume Stopwatch (CTRL + T)"
        Else
            stopwatch.Start()
            btnToggleStopwatch.Text = "Pause Stopwatch (CTRL + T)"
        End If
    End Sub

    Private Sub UpdateStopwatch(sender As Object, e As EventArgs) Handles MyBase.Paint
        If stopwatch.IsRunning Then
            txtStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
        End If
        Me.Invalidate()
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If ValidateForm() Then
            Await SubmitForm()
        End If
    End Sub

    Private Function ValidateForm() As Boolean
        ' Add your validation logic here
        ' For example:
        If String.IsNullOrWhiteSpace(txtName.Text) OrElse
           String.IsNullOrWhiteSpace(txtEmail.Text) OrElse
           String.IsNullOrWhiteSpace(txtPhone.Text) OrElse
           String.IsNullOrWhiteSpace(txtGithub.Text) Then
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Private Async Function SubmitForm() As Task
        Using client As New HttpClient()
            Dim submitData = New With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhone.Text,
            .github_link = txtGithub.Text,
            .stopwatch_time = txtStopwatch.Text
        }

            Dim json As String = JsonConvert.SerializeObject(submitData)
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")

            Try
                Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/submit", content)
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Form submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ResetForm()
                Else
                    MessageBox.Show($"Error submitting form: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Function


    Private Sub ResetForm()
        txtName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtGithub.Clear()
        stopwatch.Reset()
        txtStopwatch.Clear()
        btnToggleStopwatch.Text = "TOGGLE STOPWATCH (CTRL + T)"
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.T
                    ToggleStopwatch()
                Case Keys.S
                    btnSubmit.PerformClick()
            End Select
        End If
    End Sub
End Class