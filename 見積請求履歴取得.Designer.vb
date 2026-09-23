<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 見積請求履歴取得
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        ListBox1 = New ListBox()
        Button1 = New Button()
        Button2 = New Button()
        ListBox2 = New ListBox()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(12, 12)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(368, 424)
        ListBox1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(410, 328)
        Button1.Name = "Button1"
        Button1.Size = New Size(91, 41)
        Button1.TabIndex = 1
        Button1.Text = "決定"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(410, 395)
        Button2.Name = "Button2"
        Button2.Size = New Size(91, 41)
        Button2.TabIndex = 2
        Button2.Text = "キャンセル"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' ListBox2
        ' 
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(396, 12)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(120, 289)
        ListBox2.TabIndex = 3
        ' 
        ' 見積請求履歴取得
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(528, 450)
        Controls.Add(ListBox2)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(ListBox1)
        Name = "見積請求履歴取得"
        Text = "見積履歴一覧"
        ResumeLayout(False)
    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ListBox2 As ListBox
End Class
