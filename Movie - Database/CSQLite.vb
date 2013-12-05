Imports System.Data.SQLite

Public Class CSQLite
    ' Ver. 1.0.5

    Private myDatabase As SQLiteConnection
    Private lstColumns As List(Of String)
    Private sSQLMessage As String = ""
    Private iAffectedRows As Integer = 0

#Region "Properties"
    Public ReadOnly Property ColumnNames As List(Of String)
        Get
            If lstColumns Is Nothing Then lstColumns = New List(Of String)

            Return lstColumns
        End Get
    End Property

    Public ReadOnly Property SQLMessage() As String
        Get
            Return sSQLMessage
        End Get
    End Property

    Public ReadOnly Property isOpen As Data.ConnectionState
        Get
            Return myDatabase.State
        End Get
    End Property

    Public ReadOnly Property AffectedRows As Integer
        Get
            Return iAffectedRows
        End Get
    End Property

#End Region

#Region "Database Information"
    Public Function getViews() As List(Of String)
        Dim aList As List(Of List(Of String)) = Nothing
        Dim myList As New List(Of String)

        Query("SELECT tbl_name FROM sqlite_master WHERE type=""view"" ORDER BY tbl_name;", , aList)

        For Each Entry In aList
            myList.Add(Entry.Item(0))
        Next

        Return myList
    End Function

    Public Function getTables() As List(Of String)
        Dim aList As List(Of List(Of String)) = Nothing
        Dim myList As New List(Of String)

        Query("SELECT tbl_name FROM sqlite_master WHERE type=""table"" ORDER BY tbl_name;", , aList)

        If aList Is Nothing Then Return myList

        For Each Entry In aList
            myList.Add(Entry.Item(0))
        Next

        Return myList
    End Function

    Public Function getColumns(ByVal sTable As String) As List(Of String)
        Me.Query("SELECT * FROM " + sTable + " LIMIT 1")

        Return lstColumns
    End Function
#End Region

#Region "Database Query"
    Public Function Query(ByVal sQuery As String, Optional ByRef iAffectedRows As Integer = 0, Optional ByRef newList As List(Of List(Of String)) = Nothing) As String
        Try
            Dim SQLreader As SQLiteDataReader
            Dim iCount As Integer = 0
            Dim tmpList As New List(Of List(Of String))

            iAffectedRows = 0
            Me.iAffectedRows = iAffectedRows

            Dim SQLCommand As SQLiteCommand = myDatabase.CreateCommand()

            With SQLCommand
                .CommandText = sQuery.Trim
                If .CommandText.EndsWith(";") = False Then .CommandText += ";"

                If .CommandText.ToUpper.StartsWith("SELECT ") = True Then
                    SQLreader = .ExecuteReader()

                    lstColumns = New List(Of String)
                    For iFieldCount As Integer = 1 To SQLreader.FieldCount
                        lstColumns.Add(SQLreader.GetName(iFieldCount - 1))
                    Next

                    While SQLreader.Read()
                        tmpList.Add(New List(Of String))

                        For iFieldCount As Integer = 1 To SQLreader.FieldCount
                            If SQLreader(iFieldCount - 1).GetType.ToString = "System.DBNull" Then
                                tmpList(iCount).Add("")
                            Else
                                tmpList(iCount).Add(SQLreader(iFieldCount - 1))
                            End If
                        Next

                        iCount += 1
                    End While

                    SQLreader.Close()
                    iAffectedRows = iCount
                    Me.iAffectedRows = iAffectedRows
                    newList = tmpList
                Else
                    iAffectedRows = .ExecuteNonQuery()
                    Me.iAffectedRows = iAffectedRows
                End If

                .Dispose()
            End With

            sSQLMessage = ""

        Catch ex As Exception
            sSQLMessage = ex.Message

        End Try

        sSQLMessage = sSQLMessage.Replace(vbNewLine, " : ")

        Return sSQLMessage
    End Function
#End Region

#Region "Open Database"
    Public Function OpenDatabase(ByVal sDBName As String, ByVal sFolder As String) As Boolean
        Return Me.OpenDatabase(sFolder + "\" + sDBName + ".db")
    End Function

    Public Function OpenDatabase(ByVal sFile As String) As String
        Try
            Me.CloseDatabase()

            If System.IO.File.Exists(sFile) = False Then Return "database does not exists"

            myDatabase = New SQLiteConnection
            myDatabase.ConnectionString = "Data Source=" + sFile + ";"
            myDatabase.Open()

            Return ""

        Catch ex As Exception
            Return ex.Message.Replace(vbNewLine, " : ")

        End Try
    End Function
#End Region

#Region "Close Database"
    Public Function CloseDatabase() As Boolean
        Try
            If Not myDatabase Is Nothing Then
                myDatabase.Close()
                myDatabase.Dispose()
            End If

            Return True
        Catch
            Return False
        End Try
    End Function
#End Region

#Region "Create Database"
    Public Overridable Function CreateDatabase(ByVal sFile As String, Optional ByVal bKeepOpen As Boolean = False) As Boolean
        Dim SQLconnect As New SQLiteConnection()
        myDatabase = New SQLiteConnection

        Try
            myDatabase.ConnectionString = "Data Source=" + sFile + ";"
            myDatabase.Open()

            If bKeepOpen = False Then
                Me.CloseDatabase()
            End If

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Function CreateDatabase(ByVal sDBName As String, ByVal sFolder As String, Optional ByVal bKeepOpen As Boolean = False) As Boolean
        Return Me.CreateDatabase(sFolder + "\" + sDBName + ".db", bKeepOpen)
    End Function
#End Region

End Class
