Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySqlConnector

Public Class 工事マスタ編集

    Dim param As List(Of String)
    Dim execMode As Integer
    Dim subjectNo As Integer
    Dim messageStr As String

    Public Sub New(mode As Integer, param As List(Of String))

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Me.param = param
        Me.execMode = mode
    End Sub

    Private Sub 工事マスタ編集_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Select Case execMode
            Case 0
                Call SetData()
                Me.Button1.Text = "更新"
                Me.Text = "工事名の更新"
                messageStr = "更新を完了しました。"
                TextBox1.ReadOnly = True
                TextBox5.ReadOnly = True

                Select Case TextBox4.Text
                    Case "1"
                        RadioButton1.Checked = True
                        RadioButton2.Checked = False
                    Case "2"
                        RadioButton2.Checked = True
                        RadioButton1.Checked = False
                End Select

                TextBox2.Focus()
            Case 1
                Call SetData()
                Me.Button1.Text = "削除"
                Me.Text = "工事名の削除"
                messageStr = "削除を完了しました。"
                TextBox1.ReadOnly = True
                TextBox2.ReadOnly = True
                TextBox3.ReadOnly = True
                TextBox4.ReadOnly = True
                TextBox5.ReadOnly = True
                Button1.Focus()
            Case 2
                Me.Button1.Text = "追加"
                Me.Text = "工事名の追加"
                TextBox5.ReadOnly = True
                messageStr = "追加を完了しました。"
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox3.ReadOnly = False
                TextBox4.Text = "1"
                RadioButton1.Checked = True

                TextBox5.Text = ""
                Label7.Visible = True
                TextBox1.Focus()
        End Select

    End Sub

    '更新実行
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not DataCheck() Then
            Exit Sub
        End If

        If execMode = 1 Then    '削除
            Dim dr As DialogResult = MessageBox.Show("レコード削除します。" & vbCrLf & "よろしいですか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            Select Case dr
                Case 6
                    'OK：何もしない
                Case 7
                    Me.Dispose()
                    Exit Sub
            End Select
        End If

        Dim sql As String = ""

        If execMode = 0 Then
            '更新
            sql = "UPDATE subjects_master SET subjectNo = " _
                & CInt(TextBox1.Text) & ", subjectName = '" _
                & TextBox2.Text & "', unitName = '" _
                & TextBox3.Text & "', execMode = " _
                & CInt(TextBox4.Text) _
                & " WHERE subjectNo = " & CInt(TextBox1.Text) _
                & " AND slaveNo = " & CInt(TextBox5.Text)

            If TextBox2.Modified Or TextBox3.Modified Or TextBox4.Modified Then
                '処理続行
            Else
                'Dim result As DialogResult = MessageBox.Show("内容に変更がありませんが更新しますか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                'If result = DialogResult.OK Then

                'End If
                'MessageBox.Show("内容に変更がありません")
                'Exit Sub
            End If

        ElseIf execMode = 1 Then
            '削除
            sql = "DELETE FROM subjects_master WHERE subjectNo = " & CInt(TextBox1.Text) _
                & " AND slaveNo = " & CInt(TextBox5.Text)

        ElseIf execMode = 2 Then
            '追加
            'Call GetClientNo()
            sql = "INSERT INTO subjects_master VALUES(" _
                & CInt(TextBox1.Text) & ", " _
                & CInt(TextBox5.Text) & ", '" _
                & TextBox2.Text & "', '" _
                & TextBox3.Text & "', " _
                & CInt(TextBox4.Text) & ")"
        End If

        Try
            '2024/8/29 saruwatari Mod Start ================================================
            'Using conn As New MySqlConnection(Builder.ToString)
            Using conn As New MySqlConnection(メインメニュー.Builder.ToString)  '一時無効
                '2024/8/29 saruwatari Mod End ==============================================

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

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        Finally
            Me.Dispose()
        End Try

    End Sub

    'キャンセル
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'TextBox1.Text = ""
        'TextBox2.Text = ""
        'TextBox3.Text = ""
        'TextBox4.Text = ""
        'MyBase.Close()
        'Me.Close()
        param = New List(Of String)
        Me.Dispose()
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'e.Cancel = False
    End Sub

    Private Sub SetData()
        TextBox1.Text = param.Item(0)
        TextBox5.Text = param.Item(1)
        TextBox2.Text = param.Item(2)
        TextBox3.Text = param.Item(3)
        TextBox4.Text = param.Item(4)
    End Sub

    'Private Sub TextBox1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating

    '    'If TextBox1.Text.Length = 0 Then
    '    '    TextBox1.Text = ""
    '    '    TextBox1.BackColor = Color.MistyRose
    '    '    MessageBox.Show("入力必須です。")
    '    '    TextBox1.Focus()
    '    '    Exit Sub
    '    'End If
    '    'If Not IsNumeric(TextBox1.Text) Then
    '    '    TextBox1.Text = ""
    '    '    TextBox1.BackColor = Color.MistyRose
    '    '    MessageBox.Show("数値以外は入力できません。")
    '    '    TextBox1.Focus()
    '    '    Exit Sub
    '    'End If
    '    'If CInt(TextBox1.Text) > 999 Then
    '    '    TextBox1.Text = ""
    '    '    TextBox1.BackColor = Color.MistyRose
    '    '    MessageBox.Show("999以上の数値は入力できません。")
    '    '    TextBox1.Focus()
    '    '    Exit Sub
    '    'End If
    'End Sub

    Private Sub TextBox2_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBox2.Validating
        If TextBox2.Text.Length = 0 Then
            TextBox2.Text = ""
            TextBox2.BackColor = Color.MistyRose
            MessageBox.Show("入力必須です。")
            TextBox2.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub TextBox4_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBox4.Validating
        If Not IsNumeric(TextBox4.Text) Then
            TextBox4.Text = ""
            TextBox4.BackColor = Color.MistyRose
            MessageBox.Show("数値以外は入力できません。")
            TextBox4.Focus()
            Exit Sub
        End If
        If CInt(TextBox4.Text) > 2 Then
            TextBox4.Text = ""
            TextBox4.BackColor = Color.MistyRose
            MessageBox.Show("「0：空行」" & vbCrLf & "「1：明細行」" & vbCrLf & "「2：数値入力不可行」" & vbCrLf & "以外の数値は入力できません。")
            TextBox1.Focus()
            Exit Sub
        End If
    End Sub



    Private Sub TextBox5_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBox5.Validating
        'If TextBox5.Text.Length = 0 Then
        '    TextBox5.Text = ""
        '    TextBox5.BackColor = Color.MistyRose
        '    MessageBox.Show("入力必須です。")
        '    TextBox5.Focus()
        '    Exit Sub
        'End If
        'If Not IsNumeric(TextBox5.Text) Then
        '    TextBox5.Text = ""
        '    TextBox5.BackColor = Color.MistyRose
        '    MessageBox.Show("数値以外は入力できません。")
        '    TextBox5.Focus()
        '    Exit Sub
        'End If
        'If CInt(TextBox5.Text) > 999 Then
        '    TextBox5.Text = ""
        '    TextBox5.BackColor = Color.MistyRose
        '    MessageBox.Show("999以上の数値は入力できません。")
        '    TextBox5.Focus()
        '    Exit Sub
        'End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave

        Try
            If execMode = 2 And Not TextBox1.Text.Equals("") And IsNumeric(TextBox1.Text) Then

                Dim res As Integer
                '2024/8/29 saruwatari Mod Start =========================================================
                'Using conn As New MySqlConnection(Builder.ToString)
                Using conn As New MySqlConnection(メインメニュー.Builder.ToString)
                    '2024/8/29 saruwatari Mod End =======================================================

                    conn.Open()

                    Dim sql = "SELECT COUNT(*) FROM subjects_master WHERE subjectNo =" & CInt(TextBox1.Text)

                    Using command As New MySqlCommand(sql, conn)

                        Dim result As Object = command.ExecuteScalar

                        If result = 0 Then
                            res = 0
                        Else
                            conn.Close()
                            sql = "SELECT MAX(slaveNo) FROM subjects_master WHERE subjectNo =" & CInt(TextBox1.Text)
                            conn.Open()

                            Using command_2 As New MySqlCommand(sql, conn)

                                Using result_2 As MySqlDataReader = command_2.ExecuteReader()
                                    While result_2.Read
                                        res = result_2.GetInt16(0) + 1
                                    End While
                                End Using

                            End Using

                        End If

                    End Using

                End Using

                TextBox5.Text = CStr(res)
                TextBox2.Focus()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        Finally

        End Try

    End Sub

    Private Function DataCheck()

        Dim hasError As Boolean = True
        TextBox6.Text = ""

        If TextBox1.Text.Length = 0 Then
            TextBox1.Text = ""
            TextBox1.BackColor = Color.MistyRose
            'MessageBox.Show("入力必須です。")
            TextBox6.Text += "工事No：入力必須です。" & vbCrLf
            TextBox1.Focus()
            hasError = False
        End If
        If Not IsNumeric(TextBox1.Text) Then
            TextBox1.Text = ""
            TextBox1.BackColor = Color.MistyRose
            'MessageBox.Show("数値以外は入力できません。")
            TextBox6.Text += "工事No：数値以外は入力できません。" & vbCrLf
            TextBox1.Focus()
            hasError = False
        End If
        If Not TextBox1.Text.Equals("") AndAlso CInt(TextBox1.Text) > 999 Then
            TextBox1.Text = ""
            TextBox1.BackColor = Color.MistyRose
            'MessageBox.Show("999以上の数値は入力できません。")
            TextBox6.Text += "工事No：999以上の数値は入力できません。" & vbCrLf
            TextBox1.Focus()
            hasError = False
        End If

        If TextBox2.Text.Length = 0 Then
            TextBox2.Text = ""
            TextBox2.BackColor = Color.MistyRose
            'MessageBox.Show("入力必須です。")
            TextBox6.Text += "工事名称：入力必須です。" & vbCrLf
            TextBox2.Focus()
            hasError = False
        End If

        If Not IsNumeric(TextBox4.Text) Then
            TextBox4.Text = ""
            TextBox4.BackColor = Color.MistyRose
            TextBox6.Text += "モード：数値の必須入力です。" & vbCrLf
            TextBox4.Focus()
            hasError = False
        End If
        'If Not TextBox4.Text.Equals("") And (CInt(TextBox4.Text) > 2 Or CInt(TextBox4.Text) = 0) Then
        '    TextBox4.Text = ""
        '    TextBox4.BackColor = Color.MistyRose
        '    'MessageBox.Show("「0：空行」" & vbCrLf & "「1：明細行」" & vbCrLf & "「2：数値入力不可行」" & vbCrLf & "以外の数値は入力できません。")
        '    TextBox6.Text += "モードは　1：明細行　または　2：数値入力不可行　のみ登録可能です。"
        '    TextBox1.Focus()
        '    hasError = False
        'End If

        If Not hasError Then
            TextBox6.Visible = True
        End If

        Return hasError
    End Function

    'ラジオボタン：明細行
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

        If RadioButton1.Checked Then
            RadioButton2.Checked = False
            TextBox4.Text = 1
            TextBox3.ReadOnly = False
        End If
    End Sub

    'ラジオボタン：見出し行
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

        If RadioButton2.Checked Then
            RadioButton1.Checked = False
            TextBox4.Text = 2
            TextBox3.ReadOnly = True
            TextBox3.Text = ""
        End If
    End Sub

    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    'Const WM_CLOSE As Integer = &H10
    'Const WM_SYSCOMMAND As Integer = &H112
    'Const SC_CLOSE As Integer = &HF060

    'Select Case m.Msg
    '    Case WM_SYSCOMMAND
    '        If m.WParam.ToInt32() = SC_CLOSE Then
    '            'Xボタン、コントロールメニューの「閉じる」、
    '            'コントロールボックスのダブルクリック、
    '            'Atl+F4などにより閉じられようとしている
    '            'このときValidatingイベントを発生させない。
    '            Me.AutoValidate = Windows.Forms.AutoValidate.Disable

    '        End If
    '    Case WM_CLOSE
    '        'Application.Exit以外で閉じられようとしている
    '        'このときValidatingイベントを発生させない。
    '        Me.AutoValidate = Windows.Forms.AutoValidate.Disable
    'End Select

    'MyBase.WndProc(m)
    'End Sub

End Class