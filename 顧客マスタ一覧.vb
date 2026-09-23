Imports MySqlConnector
Imports ClosedXML

Public Class 顧客マスタ一覧

    Private Sub 顧客マスター編集_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataGridView1.Rows.Clear()
        Call DataGridLoad()

    End Sub

    Private Sub DataGridLoad()
        Try
            '2024/8/29 saruwatari Mod Start =================================================
            'Using Conn As New MySqlConnection(Builder.ToString)
            Using Conn As New MySqlConnection(メインメニュー.Builder.ToString)
                '2024/8/29 saruwatari Mod End ===============================================

                Conn.Open()

                Dim sql As String = "SELECT * FROM client_master"
                Dim command As New MySqlCommand(sql, Conn)

                Using result As MySqlDataReader = command.ExecuteReader

                    Dim row As Integer = 0
                    While result.Read()

                        DataGridView1.Rows.Add()
                        DataGridView1.Rows(row).Cells(0).Value = CStr(result.GetInt16(0)).PadLeft(5, "0"c)
                        DataGridView1.Rows(row).Cells(1).Value = result.GetString(1)
                        DataGridView1.Rows(row).Cells(2).Value = result.GetString(2)
                        DataGridView1.Rows(row).Cells(3).Value = result.GetString(3)
                        DataGridView1.Rows(row).Cells(4).Value = result.GetString(4)

                        row += 1
                    End While

                    DataGridView1.EndEdit()

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    '変更
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim param As List(Of String) = New List(Of String)
        param.Add(DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(0).Value)
        param.Add(DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(1).Value)
        param.Add(DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(2).Value)
        param.Add(DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(3).Value)
        param.Add(DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(4).Value)

        Dim fm As New 顧客マスタ編集(0, param) With {
            .StartPosition = FormStartPosition.CenterParent
        }
        fm.ShowDialog()

        DataGridView1.Rows.Clear()
        Call DataGridLoad()

    End Sub

    '追加
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim param As New List(Of String) From {
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(0).Value,
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(1).Value,
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(2).Value,
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(3).Value,
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(4).Value
        }

        Dim fm As New 顧客マスタ編集(2, param) With {
            .StartPosition = FormStartPosition.CenterParent
        }
        fm.ShowDialog()

        DataGridView1.Rows.Clear()
        Call DataGridLoad()

    End Sub

    '削除
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim param As New List(Of String) From {
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(0).Value,
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(1).Value,
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(2).Value,
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(3).Value,
            DataGridView1.Rows(DataGridView1.SelectedRows(0).Index).Cells(4).Value
        }

        Dim fm As New 顧客マスタ編集(1, param)

        fm.StartPosition = FormStartPosition.CenterParent
        fm.ShowDialog()

        DataGridView1.Rows.Clear()
        Call DataGridLoad()

    End Sub

End Class