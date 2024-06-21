Imports System.Diagnostics

Public Class CreateSubmissionForm
    ' Declare controls as WithEvents
    Private WithEvents btnStopwatch As Button
    Private WithEvents btnSubmit As Button
    Private WithEvents Timer1 As Timer
    Private WithEvents lblStopwatch As Label
    Private WithEvents txtName As TextBox
    Private WithEvents txtEmail As TextBox
    Private WithEvents txtPhone As TextBox
    Private WithEvents txtGitHub As TextBox

    Private stopwatch As New Stopwatch()
    Private isRunning As Boolean = False

    Private Sub btnStopwatch_Click(sender As Object, e As EventArgs) Handles btnStopwatch.Click
        If isRunning Then
            stopwatch.Stop()
            isRunning = False
        Else
            stopwatch.Start()
            isRunning = True
            Timer1.Start()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim submission As New Submission With {
            .Name = txtName.Text,
            .Email = txtEmail.Text,
            .Phone = txtPhone.Text,
            .GitHubLink = txtGitHub.Text,
            .StopwatchTime = lblStopwatch.Text
        }
        ' Save submission to backend (implement saving logic here)
        MessageBox.Show("Submission Saved!")
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.T Then
            btnStopwatch.PerformClick()
        End If
    End Sub

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Timer1.Interval = 1000 ' 1 second intervals

        ' Initialize controls if they're not created in the designer
        If btnStopwatch Is Nothing Then
            btnStopwatch = New Button()
            btnStopwatch.Text = "Stopwatch"
            Me.Controls.Add(btnStopwatch)
        End If

        If btnSubmit Is Nothing Then
            btnSubmit = New Button()
            btnSubmit.Text = "Submit"
            Me.Controls.Add(btnSubmit)
        End If

        If Timer1 Is Nothing Then
            Timer1 = New Timer()
            Timer1.Interval = 1000
        End If

        If lblStopwatch Is Nothing Then
            lblStopwatch = New Label()
            lblStopwatch.Text = "00:00:00"
            Me.Controls.Add(lblStopwatch)
        End If

        If txtName Is Nothing Then
            txtName = New TextBox()
            Me.Controls.Add(txtName)
        End If

        If txtEmail Is Nothing Then
            txtEmail = New TextBox()
            Me.Controls.Add(txtEmail)
        End If

        If txtPhone Is Nothing Then
            txtPhone = New TextBox()
            Me.Controls.Add(txtPhone)
        End If

        If txtGitHub Is Nothing Then
            txtGitHub = New TextBox()
            Me.Controls.Add(txtGitHub)
        End If
    End Sub
End Class