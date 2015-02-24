<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LED
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pbxLED = New System.Windows.Forms.PictureBox
        CType(Me.pbxLED, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbxLED
        '
        Me.pbxLED.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbxLED.Location = New System.Drawing.Point(0, 0)
        Me.pbxLED.Name = "pbxLED"
        Me.pbxLED.Size = New System.Drawing.Size(40, 60)
        Me.pbxLED.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxLED.TabIndex = 0
        Me.pbxLED.TabStop = False
        '
        'LED
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pbxLED)
        Me.Name = "LED"
        Me.Size = New System.Drawing.Size(40, 60)
        CType(Me.pbxLED, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbxLED As System.Windows.Forms.PictureBox

End Class
