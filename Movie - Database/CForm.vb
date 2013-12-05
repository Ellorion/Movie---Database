Imports System.Runtime.InteropServices

Public Class CForm
    Inherits Form

    ' Ver. 1.0.1

    Private bFadeIn As Boolean = False
    Private bClickThrough As Boolean = False
    Private bWasTopMost As Boolean = Me.TopMost
    Private bLoaded As Boolean = False
    Private bGlassEffect As Boolean = False

    Private iInitialStyle As Integer
    Private iWindowAlphaActive As Integer = 100
    Private iWindowAlphaTransparent As Integer = 50

    Private WithEvents myTimerCreated As New Timer

    Public Event Loaded(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property GlassEffect As Boolean
        Get
            Return bGlassEffect
        End Get
        Set(ByVal value As Boolean)
            bGlassEffect = value

            Dim myMargins As User32Wrappers.MARGINS

            If bGlassEffect Then
                myMargins.LeftWidth = -1
                myMargins.RightWidth = -1
                myMargins.TopHeight = -1
                myMargins.Buttomheight = -1
                Me.BackColor = Color.FromKnownColor(KnownColor.WindowText)
            Else
                myMargins.LeftWidth = 0
                myMargins.RightWidth = 0
                myMargins.TopHeight = 0
                myMargins.Buttomheight = 0
                Me.BackColor = Color.FromKnownColor(KnownColor.Control)
            End If

            User32Wrappers.DwmExtendFrameIntoClientArea(Me.Handle, myMargins)
        End Set
    End Property

    Public Property WindowAlphaTransparent As Integer
        Get
            Return iWindowAlphaTransparent
        End Get
        Set(ByVal value As Integer)
            If value < 10 Then value = 10
            If value > 100 Then value = 100

            iWindowAlphaTransparent = value
        End Set
    End Property

    Public Property WindowAlphaActive As Integer
        Get
            Return iWindowAlphaActive
        End Get
        Set(ByVal value As Integer)
            If value < 10 Then value = 10
            If value > 100 Then value = 100

            iWindowAlphaActive = value
        End Set
    End Property

    Public Property ClickThrough As Boolean
        Get
            Return bClickThrough
        End Get
        Set(ByVal value As Boolean)
            bClickThrough = value

            If bClickThrough Then
                bWasTopMost = Me.TopMost

                User32Wrappers.SetWindowLong(Me.Handle, User32Wrappers.GWL.ExStyle, iInitialStyle Or User32Wrappers.WS_EX.Layered Or User32Wrappers.WS_EX.Transparent)
                User32Wrappers.SetLayeredWindowAttributes(Me.Handle, 0, 255 * (WindowAlphaTransparent / 100), User32Wrappers.LWA.Alpha)

                Me.TopMost = True
            Else
                User32Wrappers.SetWindowLong(Me.Handle, User32Wrappers.GWL.ExStyle, iInitialStyle Or User32Wrappers.WS_EX.Layered)
                User32Wrappers.SetLayeredWindowAttributes(Me.Handle, 0, 255 * (WindowAlphaActive / 100), User32Wrappers.LWA.Alpha)

                Me.TopMost = bWasTopMost
            End If
        End Set
    End Property

    Public Property FadeIn As Boolean
        Get
            Return bFadeIn
        End Get
        Set(ByVal value As Boolean)
            bFadeIn = value
        End Set
    End Property

    Sub New()
        Me.Hide()

        Me.DoubleBuffered = True
        myTimerCreated.Interval = 10

        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub myTimerCreated_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles myTimerCreated.Tick
        If Me.Created Then
            If Not bFadeIn Then
                myTimerCreated.Stop()
                myTimerCreated.Dispose()

                RaiseEvent Loaded(sender, e)
                Exit Sub
            End If

            For iCount As Double = 0.0F To 1.01F Step 0.01F
                Me.Opacity = iCount
                Me.Refresh()
                Threading.Thread.Sleep(5)
            Next

            myTimerCreated.Dispose()
            RaiseEvent Loaded(sender, e)
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'CForm
        '
        Me.ClientSize = New System.Drawing.Size(300, 162)
        Me.Name = "CForm"
        Me.ResumeLayout(False)

    End Sub

    Private Sub CForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        iInitialStyle = User32Wrappers.GetWindowLong(Me.Handle, User32Wrappers.GWL.ExStyle)

        If bFadeIn Then
            Me.Opacity = 0
        End If

        myTimerCreated.Start()
    End Sub
End Class

Public Class User32Wrappers
    Public Enum GWL As Integer
        ExStyle = -20
    End Enum

    Public Enum WS_EX As Integer
        Transparent = &H20
        Layered = &H80000
    End Enum

    Public Enum LWA As Integer
        ColorKey = &H1
        Alpha = &H2
    End Enum

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MARGINS
        Public LeftWidth As Integer
        Public RightWidth As Integer
        Public TopHeight As Integer
        Public Buttomheight As Integer
    End Structure

    <DllImport("dwmapi.dll")> _
    Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarinset As MARGINS) As Integer
    End Function

    <DllImport("user32", EntryPoint:="GetWindowLong")> _
    Public Shared Function GetWindowLong( _
            ByVal hWnd As IntPtr, _
            ByVal nIndex As GWL _
                ) As Integer
    End Function

    <DllImport("user32", EntryPoint:="SetWindowLong")> _
    Public Shared Function SetWindowLong( _
            ByVal hWnd As IntPtr, _
            ByVal nIndex As GWL, _
            ByVal dsNewLong As WS_EX _
                ) As Integer
    End Function

    <DllImport("user32.dll", EntryPoint:="SetLayeredWindowAttributes")> _
    Public Shared Function SetLayeredWindowAttributes( _
            ByVal hWnd As IntPtr, _
            ByVal crKey As Integer, _
            ByVal alpha As Byte, _
            ByVal dwFlags As LWA _
                ) As Boolean
    End Function
End Class