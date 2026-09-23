Imports MySqlConnector

Public Class 顧客一覧取得

    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
    End Sub

    Public Sub New(ByRef client As String)
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.clientNameLocal = client
    End Sub

    Dim clientNameLocal As String
    Dim clientNoLocal As Integer
    'Dim flagLocal As Integer

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' リストボックスに項目を追加する

        Dim sql As String = "SELECT * FROM client_master ORDER BY clientNo"

        Try
            '2024/8/29 saruwatari Mod Start ====================================================
            'Using Conn As New MySqlConnection(Builder.ToString)
            Using Conn As New MySqlConnection(メインメニュー.Builder.ToString)
                '2024/8/29 saruwatari Mod End ==================================================

                Conn.Open()
                Using cmd As New MySqlCommand(sql, Conn)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        While (reader.Read())

                            Dim clientName As String

                            If reader.GetInt16(0) = 0 Then
                                clientNoLocal = 0
                            Else
                                clientNoLocal = reader.GetInt16(0)
                            End If

                            If reader.GetString(1) Is Nothing Then
                                clientName = ""
                            Else
                                clientName = reader.GetString(1)
                            End If

                            Me.ListBox1.Items.Add(clientName)

                        End While
                    End Using
                End Using
            End Using

        Catch sqlE As MySqlConnector.MySqlException
            MessageBox.Show("データベース接続エラー：" & vbCrLf & "システム管理者へ連絡して下さい。", "予期せぬエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Dispose()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.ToString & "：" & vbCrLf & "システム管理者へ連絡して下さい。", "予期せぬエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Dispose()
            Exit Sub
        End Try

        ListBox1.SelectedIndex = 0

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clientNameLocal = Me.ListBox1.SelectedItem
        Me.Close()
    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Dispose()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
    End Sub

    Public ReadOnly Property ClientName() As String
        Get
            Return clientNameLocal
        End Get
        'Set(ByVal Value As String)
        '    clientLocal = Value
        'End Set
    End Property

    Public ReadOnly Property ClientNo As Integer
        Get
            Return clientNoLocal
        End Get
    End Property

End Class