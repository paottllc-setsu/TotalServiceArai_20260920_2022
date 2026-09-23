<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 顧客マスタ一覧
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        DataGridView1 = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToResizeColumns = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Control
        DataGridViewCellStyle1.Font = New Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle1.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2, Column3, Column4, Column5})
        DataGridView1.Location = New Point(25, 94)
        DataGridView1.MultiSelect = False
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridView1.RowTemplate.Height = 25
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(913, 550)
        DataGridView1.TabIndex = 0
        ' 
        ' Column1
        ' 
        Column1.HeaderText = "顧客No"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        Column1.SortMode = DataGridViewColumnSortMode.NotSortable
        Column1.Width = 70
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "顧客名"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        Column2.SortMode = DataGridViewColumnSortMode.NotSortable
        Column2.Width = 250
        ' 
        ' Column3
        ' 
        Column3.HeaderText = "住所"
        Column3.Name = "Column3"
        Column3.ReadOnly = True
        Column3.SortMode = DataGridViewColumnSortMode.NotSortable
        Column3.Width = 350
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "電話番号"
        Column4.Name = "Column4"
        Column4.ReadOnly = True
        Column4.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Column5
        ' 
        Column5.HeaderText = "郵便番号"
        Column5.Name = "Column5"
        Column5.ReadOnly = True
        Column5.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(25, 26)
        Button1.Name = "Button1"
        Button1.Size = New Size(90, 40)
        Button1.TabIndex = 1
        Button1.Text = "変更"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(847, 26)
        Button2.Name = "Button2"
        Button2.Size = New Size(90, 40)
        Button2.TabIndex = 3
        Button2.Text = "削除"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(136, 26)
        Button3.Name = "Button3"
        Button3.Size = New Size(90, 40)
        Button3.TabIndex = 2
        Button3.Text = "追加"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' 顧客マスタ一覧
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(967, 661)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Name = "顧客マスタ一覧"
        Text = "顧客マスター編集"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
End Class
