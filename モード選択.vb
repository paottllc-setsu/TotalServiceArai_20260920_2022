Public Class モード選択

    Dim index As Integer = -1

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ListBox1.Items.Add("空行")
        Me.ListBox1.Items.Add("明細行")
        Me.ListBox1.Items.Add("数値入力不可行")
        Me.ListBox1.SelectedIndex = 1
    End Sub


    Private Sub ListBox1_Click(sender As Object, e As EventArgs) Handles ListBox1.Click
        index = ListBox1.SelectedIndex
        Me.Close()
    End Sub

    Public ReadOnly Property getIndex As Integer
        Get
            Return index
        End Get
    End Property

End Class