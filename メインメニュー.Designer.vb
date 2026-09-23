<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class メインメニュー
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
        Label1 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        Button5 = New Button()
        Button6 = New Button()
        Button7 = New Button()
        Label2 = New Label()
        Label3 = New Label()
        Button8 = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = SystemColors.Control
        Label1.Font = New Font("ＭＳ Ｐゴシック", 11.25F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.Location = New Point(26, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(0, 19)
        Label1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.LightGray
        Button1.Font = New Font("Yu Gothic UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point)
        Button1.Location = New Point(26, 96)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(221, 76)
        Button1.TabIndex = 1
        Button1.Text = "見積書"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.LightGray
        Button2.Font = New Font("Yu Gothic UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point)
        Button2.Location = New Point(26, 204)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(221, 76)
        Button2.TabIndex = 2
        Button2.Text = "請求書"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button3
        ' 
        Button3.Font = New Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Button3.Location = New Point(665, 96)
        Button3.Margin = New Padding(3, 4, 3, 4)
        Button3.Name = "Button3"
        Button3.Size = New Size(221, 76)
        Button3.TabIndex = 3
        Button3.Text = "顧客マスター編集"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Font = New Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Button4.Location = New Point(665, 204)
        Button4.Margin = New Padding(3, 4, 3, 4)
        Button4.Name = "Button4"
        Button4.Size = New Size(221, 76)
        Button4.TabIndex = 4
        Button4.Text = "工事マスター編集"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Font = New Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Button5.Location = New Point(665, 461)
        Button5.Margin = New Padding(3, 4, 3, 4)
        Button5.Name = "Button5"
        Button5.Size = New Size(221, 76)
        Button5.TabIndex = 5
        Button5.Text = "年間請求一覧　出力"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(695, 341)
        Button6.Margin = New Padding(3, 4, 3, 4)
        Button6.Name = "Button6"
        Button6.Size = New Size(163, 51)
        Button6.TabIndex = 6
        Button6.Text = "SQLite_test"
        Button6.UseVisualStyleBackColor = True
        Button6.Visible = False
        ' 
        ' Button7
        ' 
        Button7.BackColor = Color.LightGreen
        Button7.Font = New Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        Button7.Location = New Point(26, 463)
        Button7.Margin = New Padding(3, 4, 3, 4)
        Button7.Name = "Button7"
        Button7.Size = New Size(251, 76)
        Button7.TabIndex = 7
        Button7.Text = "請求書修正・削除"
        Button7.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(285, 409)
        Label2.Name = "Label2"
        Label2.Size = New Size(110, 20)
        Label2.TabIndex = 8
        Label2.Text = "※削除はここから"
        Label2.Visible = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(285, 519)
        Label3.Name = "Label3"
        Label3.Size = New Size(110, 20)
        Label3.TabIndex = 9
        Label3.Text = "※削除はここから"
        Label3.Visible = False
        ' 
        ' Button8
        ' 
        Button8.BackColor = Color.LightBlue
        Button8.Font = New Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        Button8.Location = New Point(26, 353)
        Button8.Margin = New Padding(3, 4, 3, 4)
        Button8.Name = "Button8"
        Button8.Size = New Size(251, 76)
        Button8.TabIndex = 10
        Button8.Text = "見積書修正・削除"
        Button8.UseVisualStyleBackColor = False
        ' 
        ' メインメニュー
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        ClientSize = New Size(914, 600)
        Controls.Add(Button8)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Button7)
        Controls.Add(Button6)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "メインメニュー"
        Text = "メインメニュー"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button8 As Button
End Class
