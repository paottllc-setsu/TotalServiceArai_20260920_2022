Imports MySqlConnector

Public Class 顧客マスタ編集

    Dim param As List(Of String)
    Dim mode As Integer
    Dim clientNo As Integer
    Dim messageStr As String

    Public Sub New(mode As Integer, param As List(Of String))

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()
        '呼び出しの後で初期化を追加します。

        Me.param = param
        Me.mode = mode
    End Sub

    Private Sub 顧客マスタ編集_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Select Case mode
            Case 0
                Call SetData()
                Me.Button1.Text = "更新"
                Me.Text = "顧客登録変更"
                messageStr = "更新を完了しました。"
                TextBox1.ReadOnly = True
                TextBox1.TabIndex = 99
                TextBox2.Focus()
            Case 1
                Call SetData()
                Me.Button1.Text = "削除"
                Me.Text = "顧客登録削除"
                messageStr = "削除を完了しました。"
                TextBox1.ReadOnly = True
                TextBox1.TabIndex = 8
                TextBox2.ReadOnly = True
                TextBox2.TabIndex = 9
                TextBox3.ReadOnly = True
                TextBox3.TabIndex = 10
                TextBox4.ReadOnly = True
                TextBox4.TabIndex = 11
                TextBox5.ReadOnly = True
                TextBox5.TabIndex = 12
                Button1.Focus()
            Case 2
                Me.Button1.Text = "追加"
                Me.Text = "顧客登録追加"
                messageStr = "追加を完了しました。"
                TextBox1.Text = "自動入力"
                TextBox1.ReadOnly = True
                TextBox2.Text = ""
                TextBox1.TabIndex = 8
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox2.Focus()
        End Select

    End Sub

    '更新実行
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not DataCheck() Then
            Exit Sub
        End If

        If mode = 1 Then    '削除
            Dim dr As DialogResult = MessageBox.Show("レコード削除します。" & vbCrLf & "よろしいですか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            Select Case dr
                Case 6
                    '何もしない
                Case 7
                    Me.Dispose()
                    Exit Sub
            End Select
        End If

        Dim sql As String = ""

        If mode = 0 Then
            '更新
            sql = "UPDATE client_master SET clientNo = " _
                & CInt(TextBox1.Text) & ", clientName = '" _
                & TextBox2.Text & "', adress = '" _
                & TextBox3.Text & "', telNo = '" _
                & TextBox4.Text & "', postNo = '" _
                & TextBox5.Text & "' WHERE clientNo = " & CInt(TextBox1.Text)

            If TextBox2.Modified Or TextBox3.Modified Or TextBox4.Modified Or TextBox5.Modified Then
                '処理続行
            Else
                MessageBox.Show("内容に変更がありません")
                Exit Sub
            End If

        ElseIf mode = 1 Then
            '削除
            sql = "DELETE FROM client_master WHERE clientNo = " & CInt(TextBox1.Text)

        ElseIf mode = 2 Then
            '追加
            Call GetClientNo()
            sql = "INSERT INTO client_master VALUES(" _
                & clientNo & ", '" _
                & TextBox2.Text & "', '" _
                & TextBox3.Text & "', '" _
                & TextBox4.Text & "', '" _
                & TextBox5.Text & "' )"
        End If

        Try
            '2024/8/29 saruwatari Mod Start =================================================
            'Using conn As New MySqlConnection(Builder.ToString)
            Using conn As New MySqlConnection(メインメニュー.Builder.ToString)
                '2024/8/29 saruwatari Mod End ===============================================
                conn.Open()

                Dim response As Integer

                Using transaction As MySqlTransaction = conn.BeginTransaction

                    Using command As New MySqlCommand(conn, transaction)

                        command.CommandText = sql
                        response = command.ExecuteNonQuery()

                        If response = 1 Then
                            transaction.Commit()
                            MessageBox.Show(messageStr)
                        Else
                            transaction.Rollback()
                            MessageBox.Show("更新異常：" & vbCrLf & "システム管理者にお問い合わせ下さい。")
                        End If
                    End Using

                End Using

            End Using

            Me.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        Finally

        End Try

    End Sub

    'キャンセル
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
    End Sub

    Private Sub GetClientNo()
        Try
            '2024/8/29 saruwatari Mod Start ==================================================
            'Using conn As New MySqlConnection(Builder.ToString)
            Using conn As New MySqlConnection(メインメニュー.Builder.ToString)
                '2024/8/29 saruwatari Mod End ================================================

                conn.Open()

                Dim response As Integer
                Dim sql As String = ""

                Using transaction As MySqlTransaction = conn.BeginTransaction

                    Using command As New MySqlCommand(conn, transaction)

                        sql = "SELECT * FROM control_master FOR UPDATE"
                        command.CommandText = sql
                        Dim reader As MySqlDataReader = command.ExecuteReader()

                        If reader.Read() Then
                            clientNo = reader.GetInt16(2)
                        End If

                        reader.Close()

                        sql = "UPDATE control_master SET clientNo = " & (clientNo + 1) & " WHERE 1=1"
                        command.CommandText = sql
                        response = command.ExecuteNonQuery()

                        If response = 1 Then
                            transaction.Commit()
                            clientNo = clientNo + 1
                        Else
                            transaction.Rollback()
                            MessageBox.Show("コントロールマスタ：更新異常" & vbCrLf & "システム管理者にお問い合わせ下さい。")
                        End If

                    End Using

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show("予期せぬエラー：" & ex.ToString)
        Finally
        End Try

    End Sub

    Private Sub SetData()
        TextBox1.Text = param.Item(0)
        TextBox2.Text = param.Item(1)
        TextBox3.Text = param.Item(2)
        TextBox4.Text = param.Item(3)
        TextBox5.Text = param.Item(4)
    End Sub

    Private Function DataCheck() As Boolean

        Dim hasError As Boolean = True
        TextBox6.Text = ""

        If TextBox2.Text.Length = 0 Then
            TextBox2.Text = ""
            TextBox2.BackColor = Color.MistyRose
            'MessageBox.Show("入力必須です。")
            TextBox6.Text = "顧客名：入力必須です。"
            TextBox6.Visible = True
            TextBox2.Focus()
            hasError = False
        End If

        Return hasError

    End Function

    Private Sub TextBox2_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBox2.Validating
        If TextBox2.Text.Length = 0 Then
            TextBox2.Text = ""
            TextBox2.BackColor = Color.MistyRose
            MessageBox.Show("入力必須です。")
            TextBox2.Focus()
            Exit Sub
        End If
    End Sub

End Class