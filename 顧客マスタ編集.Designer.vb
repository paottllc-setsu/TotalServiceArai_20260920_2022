<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 顧客マスタ編集
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
        Label1 = New Label()
        Label2 = New Label()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        Label3 = New Label()
        TextBox3 = New TextBox()
        Label4 = New Label()
        TextBox4 = New TextBox()
        Label5 = New Label()
        TextBox5 = New TextBox()
        Button2 = New Button()
        TextBox6 = New TextBox()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(60, 249)
        Button1.Name = "Button1"
        Button1.Size = New Size(86, 46)
        Button1.TabIndex = 6
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(62, 51)
        Label1.Name = "Label1"
        Label1.Size = New Size(55, 15)
        Label1.TabIndex = 100
        Label1.Text = "顧客番号"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(62, 89)
        Label2.Name = "Label2"
        Label2.Size = New Size(43, 15)
        Label2.TabIndex = 100
        Label2.Text = "顧客名"
        ' 
        ' TextBox1
        ' 
        TextBox1.CausesValidation = False
        TextBox1.Location = New Point(151, 48)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(60, 23)
        TextBox1.TabIndex = 1
        TextBox1.Text = "00000"
        ' 
        ' TextBox2
        ' 
        TextBox2.CausesValidation = False
        TextBox2.Location = New Point(151, 86)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(250, 23)
        TextBox2.TabIndex = 2
        TextBox2.Text = "XXXXXXXXXXXX"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(62, 129)
        Label3.Name = "Label3"
        Label3.Size = New Size(31, 15)
        Label3.TabIndex = 100
        Label3.Text = "住所"
        ' 
        ' TextBox3
        ' 
        TextBox3.CausesValidation = False
        TextBox3.Location = New Point(151, 126)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(214, 23)
        TextBox3.TabIndex = 3
        TextBox3.Text = "XXXXXXXXXX XXX"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(62, 168)
        Label4.Name = "Label4"
        Label4.Size = New Size(55, 15)
        Label4.TabIndex = 100
        Label4.Text = "電話番号"
        ' 
        ' TextBox4
        ' 
        TextBox4.CausesValidation = False
        TextBox4.Location = New Point(151, 165)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(90, 23)
        TextBox4.TabIndex = 4
        TextBox4.Text = "123-456-0000"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(62, 207)
        Label5.Name = "Label5"
        Label5.Size = New Size(55, 15)
        Label5.TabIndex = 100
        Label5.Text = "郵便番号"
        ' 
        ' TextBox5
        ' 
        TextBox5.CausesValidation = False
        TextBox5.Location = New Point(151, 204)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(65, 23)
        TextBox5.TabIndex = 5
        TextBox5.Text = "999-1234"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(408, 249)
        Button2.Name = "Button2"
        Button2.Size = New Size(86, 46)
        Button2.TabIndex = 7
        Button2.Text = "キャンセル"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox6
        ' 
        TextBox6.ForeColor = Color.Red
        TextBox6.Location = New Point(60, 314)
        TextBox6.Multiline = True
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(434, 37)
        TextBox6.TabIndex = 101
        TextBox6.Visible = False
        ' 
        ' 顧客マスタ編集
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(545, 370)
        Controls.Add(TextBox6)
        Controls.Add(Button2)
        Controls.Add(TextBox5)
        Controls.Add(Label5)
        Controls.Add(TextBox4)
        Controls.Add(Label4)
        Controls.Add(TextBox3)
        Controls.Add(Label3)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Button1)
        Location = New Point(402, 276)
        Name = "顧客マスタ編集"
        Text = "編集対象"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox6 As TextBox
End Class
