Imports System.IO

Public Class Form1

    Private currentAccount As Account
    Private ReadOnly filePath As String = "account.txt"

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

        currentAccount = New Account() With {
            .FilePath = filePath
        }

        If currentAccount.GetData(txtAccountNum.Text) Then
            lblAccountName.Text = currentAccount.AccountName
            lblBalance.Text = currentAccount.Balance.ToString("c")
            btnDeposit.Enabled = True
            btnWithdraw.Enabled = True

        Else
            MessageBox.Show(currentAccount.ErrorMsg, "Error")
            Clear()
        End If
    End Sub

    Private Sub Clear()

        lblAccountName.Text = String.Empty
        lblBalance.Text = String.Empty
        btnDeposit.Enabled = False
        btnWithdraw.Enabled = False

    End Sub

    Private Sub btnDeposit_Click(sender As Object, e As EventArgs) Handles btnDeposit.Click

        Try

            currentAccount.Deposit(CDbl(txtAmount.Text))
            lblBalance.Text = currentAccount.Balance.ToString("c")

        Catch ex As Exception

            MessageBox.Show("Please enter a numeric deposit amount", "Error")

        End Try
    End Sub

    Private Sub btnWithdraw_Click(sender As Object, e As EventArgs) Handles btnWithdraw.Click

        Try

            If currentAccount.Withdraw(CDbl(txtAmount.Text)) Then

                lblBalance.Text = currentAccount.Balance.ToString("c")

            Else

                MessageBox.Show(currentAccount.ErrorMsg, "Error")

            End If
        Catch ex As Exception

            MessageBox.Show("Please enter a numeric withdrawal amount", "Error")

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Close()

    End Sub

    Private Sub btnTotals_Click(sender As Object, e As EventArgs) Handles btnTotals.Click

        MessageBox.Show($"Total deposits = {currentAccount.TotalDeposits:C}," &
                        $"Total withdrawals = {currentAccount.TotalWidrawals:C}")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If currentAccount.GetData() Then

            Using writer As New StreamWriter(filePath, False)

                For i As Integer = 0 To currentAccount.Records.Count - 1

                    If currentAccount.AccountID = currentAccount.Records(i).AccountID Then

                        writer.WriteLine(currentAccount.AccountID & "," &
                                         currentAccount.AccountName & "," &
                                         currentAccount.Balance.ToString())

                    Else

                        writer.WriteLine(currentAccount.Records(i).AccountID & "," &
                                         currentAccount.Records(i).AccountName & "," &
                                         currentAccount.Records(i).Balance.ToString())

                    End If

                Next

            End Using

        Else
            MessageBox.Show(currentAccount.ErrorMsg, "Error")
            Clear()
        End If

    End Sub

End Class
