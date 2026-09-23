<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class 見積請求作成修正
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(見積請求作成修正))
        Button1 = New Button()
        Button2 = New Button()
        TextBox1 = New TextBox()
        DataGridView1 = New DataGridView()
        Column7 = New DataGridViewTextBoxColumn()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Column6 = New DataGridViewTextBoxColumn()
        Button3 = New Button()
        Button6 = New Button()
        Label1 = New Label()
        TextBox2 = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        Button4 = New Button()
        ProgressBar1 = New ProgressBar()
        Label4 = New Label()
        Button7 = New Button()
        TextBox3 = New TextBox()
        Label5 = New Label()
        Label6 = New Label()
        Panel1 = New Panel()
        Panel2 = New Panel()
        PictureBox1 = New PictureBox()
        Label7 = New Label()
        Label8 = New Label()
        TextBox4 = New TextBox()
        CheckBox1 = New CheckBox()
        DateTimePicker1 = New DateTimePicker()
        Panel3 = New Panel()
        CheckBox2 = New CheckBox()
        Button8 = New Button()
        CheckBox3 = New CheckBox()
        CheckBox4 = New CheckBox()
        Button10 = New Button()
        Label9 = New Label()
        Label10 = New Label()
        Button11 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(925, 800)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(86, 31)
        Button1.TabIndex = 7
        Button1.Text = "PDF出力"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(1050, 800)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(94, 31)
        Button2.TabIndex = 8
        Button2.Text = "クリア"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.AcceptsReturn = True
        TextBox1.Font = New Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox1.Location = New Point(467, 59)
        TextBox1.Margin = New Padding(3, 4, 3, 4)
        TextBox1.MaxLength = 100
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(329, 132)
        TextBox1.TabIndex = 2
        TextBox1.Text = "XXXXXXXX　XXXXXX　XXXXXX" & vbCrLf & "　"
        TextBox1.TextAlign = HorizontalAlignment.Center
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column7, Column1, Column2, Column3, Column4, Column5, Column6})
        DataGridView1.Location = New Point(34, 267)
        DataGridView1.Margin = New Padding(3, 4, 3, 4)
        DataGridView1.MultiSelect = False
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.RowTemplate.Height = 25
        DataGridView1.Size = New Size(1110, 521)
        DataGridView1.TabIndex = 5
        ' 
        ' Column7
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.LightSteelBlue
        DataGridViewCellStyle1.Font = New Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle1.Padding = New Padding(15, 0, 0, 0)
        Column7.DefaultCellStyle = DataGridViewCellStyle1
        Column7.HeaderText = " ﾓｰﾄﾞ"
        Column7.MaxInputLength = 99
        Column7.MinimumWidth = 6
        Column7.Name = "Column7"
        Column7.ReadOnly = True
        Column7.SortMode = DataGridViewColumnSortMode.NotSortable
        Column7.Width = 40
        ' 
        ' Column1
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.White
        DataGridViewCellStyle2.Font = New Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Column1.DefaultCellStyle = DataGridViewCellStyle2
        Column1.HeaderText = "工事名"
        Column1.MaxInputLength = 56
        Column1.MinimumWidth = 6
        Column1.Name = "Column1"
        Column1.Resizable = DataGridViewTriState.False
        Column1.SortMode = DataGridViewColumnSortMode.NotSortable
        Column1.Width = 362
        ' 
        ' Column2
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Font = New Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle3.Format = "N1"
        Column2.DefaultCellStyle = DataGridViewCellStyle3
        Column2.HeaderText = "数量"
        Column2.MinimumWidth = 6
        Column2.Name = "Column2"
        Column2.SortMode = DataGridViewColumnSortMode.NotSortable
        Column2.Width = 45
        ' 
        ' Column3
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Column3.DefaultCellStyle = DataGridViewCellStyle4
        Column3.FillWeight = 90F
        Column3.HeaderText = "単位"
        Column3.MinimumWidth = 6
        Column3.Name = "Column3"
        Column3.Resizable = DataGridViewTriState.True
        Column3.SortMode = DataGridViewColumnSortMode.NotSortable
        Column3.Width = 70
        ' 
        ' Column4
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Font = New Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle5.Format = "N0"
        Column4.DefaultCellStyle = DataGridViewCellStyle5
        Column4.HeaderText = "単価"
        Column4.MinimumWidth = 6
        Column4.Name = "Column4"
        Column4.SortMode = DataGridViewColumnSortMode.NotSortable
        Column4.Width = 50
        ' 
        ' Column5
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Column5.DefaultCellStyle = DataGridViewCellStyle6
        Column5.HeaderText = "金額"
        Column5.MinimumWidth = 6
        Column5.Name = "Column5"
        Column5.ReadOnly = True
        Column5.Resizable = DataGridViewTriState.False
        Column5.SortMode = DataGridViewColumnSortMode.NotSortable
        Column5.Width = 80
        ' 
        ' Column6
        ' 
        DataGridViewCellStyle7.Font = New Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Column6.DefaultCellStyle = DataGridViewCellStyle7
        Column6.HeaderText = "備考"
        Column6.MaxInputLength = 100
        Column6.MinimumWidth = 6
        Column6.Name = "Column6"
        Column6.SortMode = DataGridViewColumnSortMode.NotSortable
        Column6.Width = 270
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(34, 801)
        Button3.Margin = New Padding(3, 4, 3, 4)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 32)
        Button3.TabIndex = 6
        Button3.Text = "行追加"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(288, 801)
        Button6.Margin = New Padding(3, 4, 3, 4)
        Button6.Name = "Button6"
        Button6.Size = New Size(75, 32)
        Button6.TabIndex = 14
        Button6.Text = "行削除"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Yu Gothic UI", 9F, FontStyle.Underline, GraphicsUnit.Point)
        Label1.Location = New Point(486, 29)
        Label1.Name = "Label1"
        Label1.Size = New Size(96, 20)
        Label1.TabIndex = 0
        Label1.Text = "顧客名の選択"
        ' 
        ' TextBox2
        ' 
        TextBox2.AcceptsReturn = True
        TextBox2.Location = New Point(832, 59)
        TextBox2.Margin = New Padding(3, 4, 3, 4)
        TextBox2.MaxLength = 100
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(308, 132)
        TextBox2.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(830, 31)
        Label2.Name = "Label2"
        Label2.Size = New Size(54, 20)
        Label2.TabIndex = 17
        Label2.Text = "備考欄"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Yu Gothic UI", 20F, FontStyle.Regular, GraphicsUnit.Point)
        Label3.Location = New Point(39, 21)
        Label3.Name = "Label3"
        Label3.Size = New Size(190, 46)
        Label3.TabIndex = 18
        Label3.Text = "見積書作成"
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(54, 152)
        Button4.Margin = New Padding(3, 4, 3, 4)
        Button4.Name = "Button4"
        Button4.Size = New Size(137, 55)
        Button4.TabIndex = 4
        Button4.Text = "見積履歴から作成"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(965, 29)
        ProgressBar1.Margin = New Padding(3, 4, 3, 4)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(153, 20)
        ProgressBar1.TabIndex = 20
        ProgressBar1.Visible = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(34, 233)
        Label4.Name = "Label4"
        Label4.Size = New Size(350, 20)
        Label4.TabIndex = 21
        Label4.Text = "※モード　0：空行　1：明細行　2：数値入力不可行"
        ' 
        ' Button7
        ' 
        Button7.BackColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        Button7.Location = New Point(246, 76)
        Button7.Margin = New Padding(3, 4, 3, 4)
        Button7.Name = "Button7"
        Button7.Size = New Size(126, 47)
        Button7.TabIndex = 100
        Button7.Text = "削除実行"
        Button7.UseVisualStyleBackColor = False
        Button7.Visible = False
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(110, 9)
        TextBox3.Margin = New Padding(3, 4, 3, 4)
        TextBox3.Name = "TextBox3"
        TextBox3.ReadOnly = True
        TextBox3.Size = New Size(79, 27)
        TextBox3.TabIndex = 101
        TextBox3.Text = "0"
        TextBox3.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(25, 13)
        Label5.Name = "Label5"
        Label5.Size = New Size(96, 20)
        Label5.TabIndex = 102
        Label5.Text = "現在の合計："
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(192, 13)
        Label6.Name = "Label6"
        Label6.Size = New Size(84, 20)
        Label6.TabIndex = 103
        Label6.Text = "（課税前）"
        ' 
        ' Panel1
        ' 
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(TextBox3)
        Panel1.Controls.Add(Label6)
        Panel1.Controls.Add(Label5)
        Panel1.Location = New Point(832, 207)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(308, 50)
        Panel1.TabIndex = 104
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(PictureBox1)
        Panel2.Controls.Add(Label7)
        Panel2.Location = New Point(251, 365)
        Panel2.Margin = New Padding(3, 4, 3, 4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(718, 403)
        Panel2.TabIndex = 105
        Panel2.UseWaitCursor = True
        Panel2.Visible = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(279, 161)
        PictureBox1.Margin = New Padding(3, 4, 3, 4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(173, 87)
        PictureBox1.TabIndex = 1
        PictureBox1.TabStop = False
        PictureBox1.UseWaitCursor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(295, 264)
        Label7.Name = "Label7"
        Label7.Size = New Size(143, 20)
        Label7.TabIndex = 0
        Label7.Text = "しばらくお待ちください。"
        Label7.UseWaitCursor = True
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(55, 112)
        Label8.Name = "Label8"
        Label8.Size = New Size(62, 20)
        Label8.TabIndex = 106
        Label8.Text = "請求No."
        ' 
        ' TextBox4
        ' 
        TextBox4.Font = New Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox4.Location = New Point(112, 109)
        TextBox4.Margin = New Padding(3, 4, 3, 4)
        TextBox4.Name = "TextBox4"
        TextBox4.ReadOnly = True
        TextBox4.Size = New Size(45, 27)
        TextBox4.TabIndex = 107
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(246, 147)
        CheckBox1.Margin = New Padding(3, 4, 3, 4)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(155, 24)
        CheckBox1.TabIndex = 109
        CheckBox1.Text = "作成日付を指定する"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Enabled = False
        DateTimePicker1.Location = New Point(246, 173)
        DateTimePicker1.Margin = New Padding(3, 4, 3, 4)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(125, 27)
        DateTimePicker1.TabIndex = 111
        ' 
        ' Panel3
        ' 
        Panel3.BorderStyle = BorderStyle.FixedSingle
        Panel3.Location = New Point(32, 93)
        Panel3.Margin = New Padding(3, 4, 3, 4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(183, 127)
        Panel3.TabIndex = 112
        Panel3.Visible = False
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(246, 43)
        CheckBox2.Margin = New Padding(3, 4, 3, 4)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(156, 24)
        CheckBox2.TabIndex = 113
        CheckBox2.Text = "見積・請求書の削除"
        CheckBox2.UseVisualStyleBackColor = True
        CheckBox2.Visible = False
        ' 
        ' Button8
        ' 
        Button8.Location = New Point(593, 220)
        Button8.Margin = New Padding(3, 4, 3, 4)
        Button8.Name = "Button8"
        Button8.Size = New Size(40, 29)
        Button8.TabIndex = 115
        Button8.Text = "様"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Location = New Point(760, 805)
        CheckBox3.Margin = New Padding(3, 4, 3, 4)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(174, 24)
        CheckBox3.TabIndex = 116
        CheckBox3.Text = "印刷せずに保存だけする"
        CheckBox3.UseVisualStyleBackColor = True
        CheckBox3.Visible = False
        ' 
        ' CheckBox4
        ' 
        CheckBox4.AutoSize = True
        CheckBox4.Location = New Point(115, 804)
        CheckBox4.Margin = New Padding(3, 4, 3, 4)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(168, 24)
        CheckBox4.TabIndex = 117
        CheckBox4.Text = "選択行の前に追加する"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' Button10
        ' 
        Button10.Enabled = False
        Button10.Location = New Point(381, 801)
        Button10.Name = "Button10"
        Button10.Size = New Size(94, 32)
        Button10.TabIndex = 119
        Button10.Text = "出精値引き"
        Button10.UseVisualStyleBackColor = True
        Button10.Visible = False
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(525, 225)
        Label9.Name = "Label9"
        Label9.Size = New Size(66, 20)
        Label9.TabIndex = 121
        Label9.Text = "顧客名に"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(711, 226)
        Label10.Name = "Label10"
        Label10.Size = New Size(83, 20)
        Label10.TabIndex = 122
        Label10.Text = "を追加する。"
        ' 
        ' Button11
        ' 
        Button11.Location = New Point(641, 220)
        Button11.Name = "Button11"
        Button11.Size = New Size(64, 29)
        Button11.TabIndex = 123
        Button11.Text = "御中"
        Button11.UseVisualStyleBackColor = True
        ' 
        ' 見積請求作成修正
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1186, 853)
        Controls.Add(Button11)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(Button10)
        Controls.Add(CheckBox4)
        Controls.Add(CheckBox3)
        Controls.Add(Button8)
        Controls.Add(CheckBox2)
        Controls.Add(DateTimePicker1)
        Controls.Add(Button7)
        Controls.Add(CheckBox1)
        Controls.Add(TextBox4)
        Controls.Add(Label8)
        Controls.Add(Panel2)
        Controls.Add(Label4)
        Controls.Add(ProgressBar1)
        Controls.Add(Button4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(TextBox2)
        Controls.Add(Label1)
        Controls.Add(Button6)
        Controls.Add(Button3)
        Controls.Add(DataGridView1)
        Controls.Add(TextBox1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Panel1)
        Controls.Add(Panel3)
        Location = New Point(500, 300)
        Margin = New Padding(3, 4, 3, 4)
        Name = "見積請求作成修正"
        StartPosition = FormStartPosition.CenterScreen
        Text = "見積・請求"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button3 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label4 As Label
    Friend WithEvents Button7 As Button
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Panel3 As Panel
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Button8 As Button
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Button10 As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Button11 As Button
End Class
