Imports MySqlConnector

Public Class 工事一覧取得

    Public Sub New(ByRef dataList As List(Of String), ByVal rowIndex As Integer)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.selectVal = dataList
        Me.rowIndex = rowIndex

    End Sub

    Dim selectVal As List(Of String)
    Dim rowIndex As Integer

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' リストボックスに項目を追加する

        Dim sql As String = "SELECT subjectName, unitName, execMode FROM subjects_master ORDER BY subjectNo"
        '2024/8/30 saruwatari Del Starit =====================================================================
        'Dim ConnectionString As String =
        '    "Server=127.0.0.1;" _
        '    + "Port=3306;" _
        '    + "Database=total_service_arai;" _
        '    + "User ID=root;" _
        '    + "Password=;"

        'Try
        '    Using Conn As New MySqlConnection(ConnectionString)
        '2024/8/30 saruwatari Del End =========================================================================
        Try
            Using conn As New MySqlConnection(メインメニュー.Builder.ToString)

                conn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(sql, conn)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        While reader.Read()

                            Dim unitName As String
                            If reader.GetString(1) Is Nothing Then
                                unitName = ""
                            Else
                                unitName = reader.GetString(1)
                            End If

                            Me.ListBox1.Items.Add(reader.GetString(0) & "：" & unitName & "：" & reader.GetInt16(2))

                        End While
                    End Using
                End Using

                Dim Adapter = New MySqlDataAdapter(sql, conn)
                Dim Ds As New DataSet
                Adapter.Fill(Ds)
                conn.Close()

            End Using

            Me.ListBox1.SelectedIndex = 0
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        selectVal(Me.rowIndex) = Me.ListBox1.SelectedItem
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
    End Sub
End Class