Public Class frmMain
    Inherits CForm

    Private myDB As New CSQLiteDB
    Private myCFG As New CIniFile("movie-database-config.ini")

    Private sSectionName As String = ""
    Const iLVMargin As Int16 = 4

    Private sDatabasePath As String
    Dim sSQLBase As String = "SELECT Name, LastSeen, Count, Ranking FROM Entry"

    Private Sub frmMain_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            txtName.Text = ""
            searchItem()
        End If
    End Sub

    Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim lstDatabaseFile As String() = IO.Directory.GetFiles(Application.StartupPath, "*DB.db", IO.SearchOption.TopDirectoryOnly)
        Dim iCounter As Integer = 0

        For Each sFile As String In lstDatabaseFile
            sFile = IO.Path.GetFileName(sFile)
            sFile = sFile.Remove(sFile.Length - 5)
            MainMenu.Items.Insert(iCounter, MainMenu.Items.Add(sFile))
            iCounter += 1
        Next

        If MainMenu.Items.Count <= 1 Then
            MessageBox.Show("No Database(s) specified in Filesystem!" & vbNewLine & "Format: <Name>DB.db", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If

        Select Case myCFG.GetValue("filter", "display", "all")
            Case "complete"
                ShowCompleteToolStripMenuItem_Click(sender, e)
            Case "incomplete"
                ShowIncompleteToolStripMenuItem_Click(sender, e)
            Case Else
                ShowallToolStripMenuItem_Click(sender, e)
        End Select

        If selectSystem(myCFG.GetValue("startup", "database", "Anime")) = False Then
            MainMenu.Items(0).PerformClick()
        End If

        'sSectionName = MainMenu.Items(0).Text.Replace("&", "")
        'selectSystem(sSectionName)
        'MainMenu.Items(0).PerformClick()
    End Sub

    Private Function selectSystem(sType As String)
        Dim sDBFile As String = sType + "DB.db"

        If IO.File.Exists(IO.Path.Combine(Application.StartupPath, sDBFile)) = False Then Return False

        If sType = "Settings" Then Return False

        Me.Text = sType + " - Database"
        sSectionName = sType

        sDatabasePath = IO.Path.Combine(Application.StartupPath, sDBFile)

        myDB.CreateDatabase(sDatabasePath, True)

        lvEntry.Database = sDatabasePath
        lvEntry.SortColumn = "Name"
        lvEntry.FullRowSelect = True
        lvEntry.MultiSelect = False

        txtName.Text = ""

        myCFG.SetValue("startup", "database", sType)

        searchItem()

        Return True
    End Function

    Private Sub lvEntry_DoubleClick(sender As Object, e As System.EventArgs) Handles lvEntry.DoubleClick
        If lvEntry.SelectedItems.Count <> 1 Then Exit Sub

        Dim myLVI As ListViewItem = DirectCast(sender, CSQLView).SelectedItems.Item(0)

        txtName.Text = myLVI.SubItems(0).Text
        nudLastSeen.Value = Decimal.Parse(myLVI.SubItems(1).Text)
        nudCount.Value = Decimal.Parse(myLVI.SubItems(2).Text)
        nudRanking.Value = Decimal.Parse(myLVI.SubItems(3).Text)

        txtName.Focus()
    End Sub

    Private Sub lvEntry_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles lvEntry.KeyUp
        If lvEntry.SelectedItems.Count <> 1 Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            lvEntry_DoubleClick(sender, e)
            Exit Sub
        End If

        If e.Control Then
            If e.KeyCode = Keys.Delete Then
                removeItem(lvEntry.SelectedItems.Item(0).SubItems(0).Text)
                Exit Sub
            End If

            If e.KeyCode = Keys.I Then
                If sSectionName = "Anime" Then
                    Dim sName As String = lvEntry.SelectedItems.Item(0).Text
                    openAnimeInfo(sName)
                End If
                Exit Sub
            End If
        End If
    End Sub

    Private Sub openAnimeInfo(sName As String)
        Process.Start("http://anidb.net/perl-bin/animedb.pl?show=animelist&adb.search=" & sName & "&do.search=search")
    End Sub

    Private Sub removeItem(sName As String)
        If lvEntry.SelectedItems.Count > 1 Then Exit Sub

        Dim iCurrentIndex As Integer = -1
        If lvEntry.SelectedItems.Count > 0 Then
            iCurrentIndex = lvEntry.SelectedItems(0).Index
        End If
        If iCurrentIndex >= lvEntry.Items.Count - 1 Then iCurrentIndex = lvEntry.Items.Count - 2

        Dim sSQL As String = "DELETE FROM Entry WHERE Name = """ & sName & """"
        myDB.Query(sSQL)

        lvEntry.executeQuery()

        txtName.Text = ""
        nudLastSeen.Value = 0
        nudCount.Value = 0
        nudRanking.Value = 0

        If iCurrentIndex < 0 Then Exit Sub
        lvEntry.Items(iCurrentIndex).Selected = True
        lvEntry.Items(iCurrentIndex).Focused = True
        lvEntry.EnsureVisible(iCurrentIndex)
    End Sub

    Private Sub addItem()
        Dim sEntry As String = txtName.Text.Trim
        If sEntry.Length = 0 Then Exit Sub

        Dim iLastSeen As Int32 = Integer.Parse(nudLastSeen.Value)
        Dim iCount As Int32 = Integer.Parse(nudCount.Value)
        Dim iRanking As Int32 = Integer.Parse(nudRanking.Value)

        removeItem(sEntry)
        myDB.addEntry(sEntry, iLastSeen, iCount, iRanking)

        txtName.Text = ""
        nudLastSeen.Value = 0
        nudCount.Value = 0
        nudRanking.Value = 0
        searchItem()
        txtName.Focus()
    End Sub

    Private Sub searchItem()
        Dim sSQL As String = sSQLBase
        Dim iColumnRankingWidth = 55
        Dim iColumnValuesWidth = 60

        If ShowCompleteToolStripMenuItem.Checked Then
            sSQL += " WHERE LastSeen = Count AND Count > 0"
        ElseIf ShowIncompleteToolStripMenuItem.Checked Then
            sSQL += " WHERE LastSeen != Count OR Count = 0"
        ElseIf ShowallToolStripMenuItem.Checked Then
            sSQL += " WHERE 1"
        End If

        If txtName.Text.Length > 0 Then sSQL += " AND Name LIKE ""%" & txtName.Text & "%"""

        lvEntry.executeQuery(sSQL)

        If lvEntry.Items.Count = 0 Then
            Exit Sub
        End If

        lvEntry.Columns(0).Width = lvEntry.Width - iColumnRankingWidth - iColumnValuesWidth * 2 - 20

        lvEntry.Columns(1).Width = iColumnValuesWidth
        lvEntry.Columns(1).TextAlign = HorizontalAlignment.Center

        lvEntry.Columns(2).Width = iColumnValuesWidth
        lvEntry.Columns(2).TextAlign = HorizontalAlignment.Center

        lvEntry.Columns(3).Width = iColumnRankingWidth
        lvEntry.Columns(3).TextAlign = HorizontalAlignment.Center
    End Sub

    Private Sub addItem_EnterKey(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles nudCount.KeyUp, nudLastSeen.KeyUp, nudRanking.KeyUp
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Return Then
            addItem()
        End If
    End Sub

    Private Sub txtName_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        addItem_EnterKey(sender, e)

        If e.Control Then
            If e.KeyCode = Keys.V Then
                ' remove tags
                Dim sEntry As String = txtName.Text.Replace("_", " ")

                sEntry = cutText(sEntry, "[", "]")
                sEntry = cutText(sEntry, "(", ")")

                sEntry = sEntry.Replace(".", " ")
                sEntry = sEntry.Trim()
                sEntry = sEntry.Replace("Vol ", "Vol.")
                sEntry = sEntry.Replace("vol ", "Vol.")

                txtName.Text = sEntry
                Exit Sub
            End If

            If e.KeyCode = Keys.I Then
                If sSectionName = "Anime" Then
                    openAnimeInfo(txtName.Text)
                End If
            End If
        End If
    End Sub

    Private Function cutText(sText As String, sDelimStart As String, sDelimEnd As String) As String
        Dim iPos As Integer = Strings.InStr(sText, sDelimStart)

        While iPos > 0
            Dim sFound As String = sText.Substring(iPos - 1)
            sFound = sFound.Substring(0, Strings.InStr(sFound, sDelimEnd))

            sText = sText.Replace(sFound, "")

            iPos = Strings.InStr(sText, sDelimStart)
        End While

        Return sText
    End Function

    Private Sub menuItem_Click(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MainMenu.ItemClicked
        sSectionName = e.ClickedItem.Text.Replace("&", "")

        selectSystem(sSectionName)
    End Sub

    Private Sub txtName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtName.TextChanged
        searchItem()
    End Sub

    Private Sub AlwaysOnTopToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AlwaysOnTopToolStripMenuItem.Click
        Dim myMenu As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)

        myMenu.Checked = Not myMenu.Checked

        Me.TopMost = myMenu.Checked
    End Sub

    Private Sub lblSeperator_DoubleClick(sender As Object, e As System.EventArgs) Handles lblSeperator.DoubleClick
        nudLastSeen.Value = nudCount.Value
    End Sub

    Private Sub lvEntry_PostInsertItem(ByRef lviEntry As Object) Handles lvEntry.PostInsertItem
        If lviEntry.SubItems(1).Text = lviEntry.SubItems(2).Text And lviEntry.SubItems(2).Text <> "0" Then
            lviEntry.ForeColor = Color.Blue
        End If
    End Sub

    Private Sub ShowallToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ShowallToolStripMenuItem.Click
        ShowallToolStripMenuItem.Checked = True
        ShowCompleteToolStripMenuItem.Checked = False
        ShowIncompleteToolStripMenuItem.Checked = False

        myCFG.SetValue("filter", "display", "all")

        searchItem()
    End Sub

    Private Sub ShowCompleteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ShowCompleteToolStripMenuItem.Click
        ShowallToolStripMenuItem.Checked = False
        ShowCompleteToolStripMenuItem.Checked = True
        ShowIncompleteToolStripMenuItem.Checked = False

        myCFG.SetValue("filter", "display", "complete")

        searchItem()
    End Sub

    Private Sub ShowIncompleteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ShowIncompleteToolStripMenuItem.Click
        ShowallToolStripMenuItem.Checked = False
        ShowCompleteToolStripMenuItem.Checked = False
        ShowIncompleteToolStripMenuItem.Checked = True

        myCFG.SetValue("filter", "display", "incomplete")

        searchItem()
    End Sub
End Class
