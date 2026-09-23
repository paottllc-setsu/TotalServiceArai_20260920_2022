<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 工事マスタ編集
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
        Button1 = New Button()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        TextBox3 = New TextBox()
        TextBox4 = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Button2 = New Button()
        TextBox5 = New TextBox()
        Label5 = New Label()
        TextBox6 = New TextBox()
        Label7 = New Label()
        RadioButton1 = New RadioButton()
        RadioButton2 = New RadioButton()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(65, 239)
        Button1.Name = "Button1"
        Button1.Size = New Size(78, 47)
        Button1.TabIndex = 7
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.CausesValidation = False
        TextBox1.Location = New Point(149, 104)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(30, 23)
        TextBox1.TabIndex = 1
        TextBox1.Text = "000"
        ' 
        ' TextBox2
        ' 
        TextBox2.CausesValidation = False
        TextBox2.Location = New Point(149, 146)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(349, 23)
        TextBox2.TabIndex = 3
        TextBox2.Text = "□　□　塗　装　工　事"
        ' 
        ' TextBox3
        ' 
        TextBox3.CausesValidation = False
        TextBox3.Location = New Point(149, 193)
        TextBox3.Name = "TextBox3"
        TextBox3.ReadOnly = True
        TextBox3.Size = New Size(47, 23)
        TextBox3.TabIndex = 4
        TextBox3.Text = "㎡"
        ' 
        ' TextBox4
        ' 
        TextBox4.CausesValidation = False
        TextBox4.Location = New Point(149, 35)
        TextBox4.Name = "TextBox4"
        TextBox4.ReadOnly = True
        TextBox4.Size = New Size(21, 23)
        TextBox4.TabIndex = 5
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(65, 107)
        Label1.Name = "Label1"
        Label1.Size = New Size(47, 15)
        Label1.TabIndex = 5
        Label1.Text = "工事No"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(65, 149)
        Label2.Name = "Label2"
        Label2.Size = New Size(55, 15)
        Label2.TabIndex = 6
        Label2.Text = "工事名称"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(65, 196)
        Label3.Name = "Label3"
        Label3.Size = New Size(31, 15)
        Label3.TabIndex = 7
        Label3.Text = "単位"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(65, 38)
        Label4.Name = "Label4"
        Label4.Size = New Size(32, 15)
        Label4.TabIndex = 8
        Label4.Text = "モード"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(420, 239)
        Button2.Name = "Button2"
        Button2.Size = New Size(78, 47)
        Button2.TabIndex = 7
        Button2.Text = "キャンセル"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox5
        ' 
        TextBox5.CausesValidation = False
        TextBox5.Location = New Point(203, 104)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(30, 23)
        TextBox5.TabIndex = 2
        TextBox5.Text = "000"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(182, 107)
        Label5.Name = "Label5"
        Label5.Size = New Size(19, 15)
        Label5.TabIndex = 11
        Label5.Text = "―"
        ' 
        ' TextBox6
        ' 
        TextBox6.BorderStyle = BorderStyle.None
        TextBox6.Font = New Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox6.ForeColor = Color.Red
        TextBox6.Location = New Point(65, 306)
        TextBox6.Multiline = True
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(433, 69)
        TextBox6.TabIndex = 12
        TextBox6.Visible = False
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(229, 107)
        Label7.Name = "Label7"
        Label7.Size = New Size(103, 15)
        Label7.TabIndex = 14
        Label7.Text = "（枝番自動入力）"
        Label7.Visible = False
        ' 
        ' RadioButton1
        ' 
        RadioButton1.AutoSize = True
        RadioButton1.Checked = True
        RadioButton1.Location = New Point(192, 37)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(61, 19)
        RadioButton1.TabIndex = 15
        RadioButton1.TabStop = True
        RadioButton1.Text = "明細行"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.AutoSize = True
        RadioButton2.Location = New Point(271, 37)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(109, 19)
        RadioButton2.TabIndex = 16
        RadioButton2.TabStop = True
        RadioButton2.Text = "数値入力不可行"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' 工事マスタ編集
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(545, 394)
        Controls.Add(RadioButton2)
        Controls.Add(RadioButton1)
        Controls.Add(TextBox5)
        Controls.Add(Label7)
        Controls.Add(TextBox6)
        Controls.Add(Label5)
        Controls.Add(Button2)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TextBox4)
        Controls.Add(TextBox3)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(Button1)
        Name = "工事マスタ編集"
        Text = "工事マスター"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
End Class
