Public Class CSQLiteDB
    Inherits CSQLite

    Public Overrides Function CreateDatabase(sFile As String, Optional bKeepOpen As Boolean = False) As Boolean
        Dim bResult As Boolean = False

        MyBase.CreateDatabase(sFile, True)

        bResult = Me.Query("CREATE TABLE IF NOT EXISTS Entry (Name VARCHAR(255) NOT NULL, PRIMARY KEY(Name))").Length = 0
        bResult = Me.Query("ALTER TABLE Entry ADD LastSeen INT NOT NULL DEFAULT 0").Length = 0
        bResult = Me.Query("ALTER TABLE Entry ADD Count INT NOT NULL DEFAULT 0").Length = 0
        bResult = Me.Query("ALTER TABLE Entry ADD Ranking INT NOT NULL DEFAULT 0").Length = 0

        If bKeepOpen = False Then
            Me.CloseDatabase()
        End If

        Return bResult
    End Function

    Public Function addEntry(sEntry As String, iLastSeen As Integer, iCount As Integer, iRanking As Integer) As Boolean
        Return Me.Query("INSERT INTO Entry (Name, LastSeen, Count, Ranking) VALUES (""" & sEntry & """, " & iLastSeen.ToString & ", " & iCount.ToString & ", " & iRanking.ToString & ")").Length = 0
    End Function
End Class
