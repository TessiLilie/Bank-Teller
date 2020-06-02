
Imports System.IO

Public Class Account

        Public Property AccountID As String
        Public Property AccountName As String
        Public Property Balance As Decimal
        Public Property FilePath As String
        Public Property ErrorMsg As String
        Public Property Records As List(Of Account)

        Private _totalDeposits As Decimal
        Private _totalWitdrawals As Decimal

        Public ReadOnly Property TotalDeposits As Decimal

            Get
                Return _totalDeposits
            End Get

        End Property

        Public ReadOnly Property TotalWidrawals As Decimal

            Get
                Return _totalWitdrawals
            End Get

        End Property

        Public Sub New()

            AccountName = String.Empty
            Balance = 0D
            Records = New List(Of Account)

        End Sub

        ''' <summary>
        ''' Open the file and read it
        ''' </summary>
        ''' <param name="acctId">user pass account ID</param>
        ''' <returns>Boolean value if account existis</returns>
        Public Function GetData(acctId As String) As Boolean

            Dim infile As StreamReader = Nothing
            ErrorMsg = String.Empty

            Try
                infile = File.OpenText(FilePath)

                While Not infile.EndOfStream
                    Dim line As String = infile.ReadLine()
                    Dim fields() As String = line.Split(","c)

                    If fields(0) = acctId Then
                        AccountID = fields(0)
                        AccountName = fields(1)
                        Balance = CDec(fields(2))

                        _totalDeposits = 0D
                        _totalWitdrawals = 0D
                        Return True
                    End If

                End While

                ErrorMsg = "Account " & AccountID & "Not Found"
                Return False

            Catch ex As Exception

                ErrorMsg = ex.Message
                Return False

            Finally

                If infile IsNot Nothing Then infile.Close()

            End Try
        End Function

        ''' <summary>
        ''' Saving data to the memory
        ''' </summary>
        ''' <returns>Boolean value if account existis</returns>
        Public Function GetData() As Boolean

            Dim infile As StreamReader = Nothing
            ErrorMsg = String.Empty
            Dim fields() As String

            Try
                infile = File.OpenText(FilePath)

                While Not infile.EndOfStream

                Dim line As String = infile.ReadLine()
                fields = line.Split(","c)

                Dim acct As Account = New Account()
                    acct.AccountID = fields(0)
                    acct.AccountName = fields(1)
                acct.Balance = CDec(Trim(fields(2)))

                Records.Add(acct)

                End While

                If Records IsNot Nothing Then Return True

            Catch ex As Exception

                ErrorMsg = ex.Message
                Return False

            Finally

                If infile IsNot Nothing Then infile.Close()

            End Try

            Return False

        End Function

        Public Sub Deposit(amount As Decimal)

            Balance += amount
            _totalDeposits += amount

        End Sub

        Public Function Withdraw(amount As Decimal) As Boolean

            If amount <= Balance Then

                Balance -= amount
                _totalWitdrawals -= amount
                Return True

            Else

                ErrorMsg = "Balance is too low to withdraw the requested amount"
                Return False

            End If

        End Function


    End Class

