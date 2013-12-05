Imports System.Data.SQLite

Public Class CSQLView : Inherits ListView
    ' Ver. 1.0.5

    Private mySQL As New CSQLite
    Private sColumnOrder As String = ""
    Private bSortAscending As Boolean = True
    Private sSQLMessage As String = ""

    Private Property SQLBase As String = ""

    Public Property Database As String = ""
    Public Property SQL As String = ""
    Public Property AllowQueryReOrdering As Boolean = True

    Public Event PostInsertItem(ByRef lviEntry)
    
    Public ReadOnly Property SQLMessage As String
        Get
            Return sSQLMessage
        End Get
    End Property

    Public Property SortColumn As String
        Get
            Return sColumnOrder
        End Get
        Set(value As String)
            sColumnOrder = value
        End Set
    End Property

    Event PostColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)
    Event PreColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)

    Public Function executeQuery() As String
        Return executeQuery(Database, SQLBase)
    End Function

    Public Function executeQuery(ByVal sSQL As String) As String
        Return executeQuery(Database, sSQL)
    End Function

    Public Function executeQuery(ByVal sDatabase As String, ByVal sSQL As String) As String
        Dim newList As List(Of List(Of String)) = Nothing
        Dim sMessage As String = ""
        Dim sSQLBaseBuffer As String = ""
        Dim sError As String = ""

        Me.View = View.Details
        Database = sDatabase
        sSQLBaseBuffer = sSQL

        sError = mySQL.OpenDatabase(sDatabase)
        If sError.Length > 0 Then Return sError

        If sSQL <> sSQLBaseBuffer Then
            bSortAscending = True
        End If

        If sColumnOrder.Length > 0 And AllowQueryReOrdering And sSQL.ToUpper.Contains("ORDER BY") = False Then
            sSQL += " ORDER BY "
            sSQL += sColumnOrder
            sSQL += " " + IIf(bSortAscending, "ASC", "DESC")
        End If

        If SQLBase <> sSQL Then
            sMessage = mySQL.Query(sSQL, , newList)
            SQL = sSQL

            mySQL.CloseDatabase()

            If sMessage.Length > 0 Then
                If sColumnOrder.Length > 0 Then
                    sColumnOrder = ""
                    Return executeQuery(sDatabase, sSQLBaseBuffer)
                End If

                Me.Clear()

                SQLBase = sSQLBaseBuffer

                sSQLMessage = sMessage
                Return sMessage
            End If

            Dim aiColumnWidth(mySQL.ColumnNames.Count - 1) As Integer

            Me.BeginUpdate()

            If SQLBase <> sSQLBaseBuffer Then
                Me.Clear()

                Dim iWidth As Integer = (Me.Width / mySQL.ColumnNames.Count) - (20 / mySQL.ColumnNames.Count)

                For Each Entry In mySQL.ColumnNames
                    Me.Columns.Add(Entry).Width = iWidth
                Next

                Me.Scrollable = True

                SQLBase = sSQLBaseBuffer
            End If

            Me.Items.Clear()

            If Not newList Is Nothing Then
                For Each Entry In newList
                    Dim lvi As ListViewItem = Me.Items.Add(Entry.Item(0))
                    
                    For iCount As Integer = 1 To Entry.Count - 1
                        lvi.SubItems.Add(Entry.Item(iCount))
                    Next

                    RaiseEvent PostInsertItem(lvi)
                Next
            End If

            Me.EndUpdate()
        End If

        sMessage = "SQL successful!"

        sSQLMessage = sMessage
        Return sMessage
    End Function

    Private Sub CSQLView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles Me.ColumnClick
        If sColumnOrder.Length = 0 Then
            bSortAscending = Not bSortAscending
        End If

        sColumnOrder = Me.Columns(e.Column).Text

        RaiseEvent PreColumnClick(sender, e)
        Application.DoEvents()

        bSortAscending = Not bSortAscending
        executeQuery()

        RaiseEvent PostColumnClick(sender, e)
    End Sub

    Sub New()
        DoubleBuffered = True
    End Sub
End Class