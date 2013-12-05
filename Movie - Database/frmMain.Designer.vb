<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits Movie_Database.CForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lvEntry = New Movie_Database.CSQLView()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowallToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowCompleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowIncompleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AlwaysOnTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblLastSeen = New System.Windows.Forms.Label()
        Me.nudLastSeen = New System.Windows.Forms.NumericUpDown()
        Me.nudCount = New System.Windows.Forms.NumericUpDown()
        Me.lblSeperator = New System.Windows.Forms.Label()
        Me.lblRanking = New System.Windows.Forms.Label()
        Me.nudRanking = New System.Windows.Forms.NumericUpDown()
        Me.MainMenu.SuspendLayout()
        CType(Me.nudLastSeen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudRanking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(12, 35)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(38, 13)
        Me.lblName.TabIndex = 1
        Me.lblName.Text = "Name:"
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(56, 32)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(269, 20)
        Me.txtName.TabIndex = 0
        '
        'lvEntry
        '
        Me.lvEntry.AllowQueryReOrdering = True
        Me.lvEntry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvEntry.Database = ""
        Me.lvEntry.Location = New System.Drawing.Point(12, 59)
        Me.lvEntry.MultiSelect = False
        Me.lvEntry.Name = "lvEntry"
        Me.lvEntry.Size = New System.Drawing.Size(523, 142)
        Me.lvEntry.SortColumn = ""
        Me.lvEntry.SQL = ""
        Me.lvEntry.TabIndex = 1
        Me.lvEntry.UseCompatibleStateImageBehavior = False
        '
        'MainMenu
        '
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem})
        Me.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MainMenu.Size = New System.Drawing.Size(545, 24)
        Me.MainMenu.TabIndex = 0
        Me.MainMenu.Text = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterToolStripMenuItem, Me.ToolStripMenuItem1, Me.AlwaysOnTopToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(58, 20)
        Me.SettingsToolStripMenuItem.Text = "&Settings"
        '
        'FilterToolStripMenuItem
        '
        Me.FilterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowallToolStripMenuItem, Me.ShowCompleteToolStripMenuItem, Me.ShowIncompleteToolStripMenuItem})
        Me.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem"
        Me.FilterToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.FilterToolStripMenuItem.Text = "&Filter"
        '
        'ShowallToolStripMenuItem
        '
        Me.ShowallToolStripMenuItem.Checked = True
        Me.ShowallToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowallToolStripMenuItem.Name = "ShowallToolStripMenuItem"
        Me.ShowallToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.D1), System.Windows.Forms.Keys)
        Me.ShowallToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ShowallToolStripMenuItem.Text = "Show &all"
        '
        'ShowCompleteToolStripMenuItem
        '
        Me.ShowCompleteToolStripMenuItem.Name = "ShowCompleteToolStripMenuItem"
        Me.ShowCompleteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.D2), System.Windows.Forms.Keys)
        Me.ShowCompleteToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ShowCompleteToolStripMenuItem.Text = "Show &complete"
        '
        'ShowIncompleteToolStripMenuItem
        '
        Me.ShowIncompleteToolStripMenuItem.Name = "ShowIncompleteToolStripMenuItem"
        Me.ShowIncompleteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.D3), System.Windows.Forms.Keys)
        Me.ShowIncompleteToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ShowIncompleteToolStripMenuItem.Text = "Show &incomplete"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(176, 6)
        '
        'AlwaysOnTopToolStripMenuItem
        '
        Me.AlwaysOnTopToolStripMenuItem.Name = "AlwaysOnTopToolStripMenuItem"
        Me.AlwaysOnTopToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AlwaysOnTopToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.AlwaysOnTopToolStripMenuItem.Text = "Always on &Top"
        '
        'lblLastSeen
        '
        Me.lblLastSeen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLastSeen.AutoSize = True
        Me.lblLastSeen.Location = New System.Drawing.Point(331, 35)
        Me.lblLastSeen.Name = "lblLastSeen"
        Me.lblLastSeen.Size = New System.Drawing.Size(35, 13)
        Me.lblLastSeen.TabIndex = 5
        Me.lblLastSeen.Text = "Seen:"
        '
        'nudLastSeen
        '
        Me.nudLastSeen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudLastSeen.Location = New System.Drawing.Point(365, 32)
        Me.nudLastSeen.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nudLastSeen.Name = "nudLastSeen"
        Me.nudLastSeen.Size = New System.Drawing.Size(42, 20)
        Me.nudLastSeen.TabIndex = 2
        '
        'nudCount
        '
        Me.nudCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudCount.Location = New System.Drawing.Point(423, 32)
        Me.nudCount.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nudCount.Name = "nudCount"
        Me.nudCount.Size = New System.Drawing.Size(42, 20)
        Me.nudCount.TabIndex = 3
        Me.nudCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblSeperator
        '
        Me.lblSeperator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSeperator.AutoSize = True
        Me.lblSeperator.Location = New System.Drawing.Point(408, 35)
        Me.lblSeperator.Name = "lblSeperator"
        Me.lblSeperator.Size = New System.Drawing.Size(12, 13)
        Me.lblSeperator.TabIndex = 8
        Me.lblSeperator.Text = "/"
        '
        'lblRanking
        '
        Me.lblRanking.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRanking.AutoSize = True
        Me.lblRanking.Location = New System.Drawing.Point(470, 35)
        Me.lblRanking.Name = "lblRanking"
        Me.lblRanking.Size = New System.Drawing.Size(36, 13)
        Me.lblRanking.TabIndex = 9
        Me.lblRanking.Text = "Rank:"
        '
        'nudRanking
        '
        Me.nudRanking.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudRanking.Location = New System.Drawing.Point(504, 32)
        Me.nudRanking.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.nudRanking.Name = "nudRanking"
        Me.nudRanking.Size = New System.Drawing.Size(31, 20)
        Me.nudRanking.TabIndex = 4
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 213)
        Me.Controls.Add(Me.nudRanking)
        Me.Controls.Add(Me.lblRanking)
        Me.Controls.Add(Me.lblSeperator)
        Me.Controls.Add(Me.nudCount)
        Me.Controls.Add(Me.nudLastSeen)
        Me.Controls.Add(Me.lblLastSeen)
        Me.Controls.Add(Me.lvEntry)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.MainMenu)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MainMenu
        Me.Name = "frmMain"
        Me.Text = "Movie - Database"
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        CType(Me.nudLastSeen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudRanking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lvEntry As Movie_Database.CSQLView
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlwaysOnTopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblLastSeen As System.Windows.Forms.Label
    Friend WithEvents nudLastSeen As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSeperator As System.Windows.Forms.Label
    Friend WithEvents lblRanking As System.Windows.Forms.Label
    Friend WithEvents nudRanking As System.Windows.Forms.NumericUpDown
    Friend WithEvents FilterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowallToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowCompleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowIncompleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator

End Class
