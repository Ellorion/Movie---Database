Imports System.IO

Public Class CIniFile
    ' Ver. 1.0.0

    Private sFileName As String
    Private lstContent As New Dictionary(Of String, Dictionary(Of String, String))

    Sub New(sFileName As String)
        Me.sFileName = sFileName
        Me.LoadFile()
    End Sub

    Public Sub SetValue(sSection As String, sKey As String, sValue As String)
        If lstContent.ContainsKey(sSection) Then
            If lstContent.Item(sSection).ContainsKey(sKey) Then
                lstContent.Item(sSection).Item(sKey) = sValue
                Me.SaveFile()
                Return
            End If
        End If

        Dim myPair As New Dictionary(Of String, String)
        myPair.Add(sKey, sValue)
        lstContent.Add(sSection, myPair)

        Me.SaveFile()
    End Sub


    Public Function GetValue(sSection As String, sKey As String, default_value As String) As String
        If Not lstContent.ContainsKey(sSection) Then Return default_value
        If Not lstContent.Item(sSection).ContainsKey(sKey) Then Return default_value

        Return lstContent.Item(sSection).Item(sKey)
    End Function

    Private Sub LoadFile()
        Dim sr As StreamReader
        Dim sLine As String
        Dim sSectionName As String = ""

        If Not File.Exists(Me.sFileName) Then Return

        sr = New StreamReader(Me.sFileName)

        While Not sr.EndOfStream
            sLine = sr.ReadLine.Trim

            If sLine.Length = 0 Then Continue While

            If sLine.StartsWith("[") And sLine.EndsWith("]") Then
                sSectionName = sLine.Substring(1, sLine.Length - 2)
            End If

            If sSectionName.Length = 0 Then Continue While

            If sLine.Contains("=") Then
                Dim sKey As String = sLine.Substring(0, Strings.InStr(sLine, "=") - 1)
                Dim sValue As String = sLine.Substring(sKey.Length + 1)

                Dim myPair As New Dictionary(Of String, String)
                myPair.Add(sKey, sValue)

                lstContent.Add(sSectionName, myPair)
            End If

        End While

        sr.Close()
    End Sub

    Private Sub SaveFile()
        Dim sw As New StreamWriter(Me.sFileName, False)

        For Each dict As KeyValuePair(Of String, Dictionary(Of String, String)) In lstContent
            sw.WriteLine("[" + dict.Key + "]")

            For Each pair As KeyValuePair(Of String, String) In dict.Value
                sw.WriteLine(pair.Key + "=" + pair.Value)
            Next

            sw.WriteLine(vbNewLine)
        Next

        sw.Close()
    End Sub
End Class
