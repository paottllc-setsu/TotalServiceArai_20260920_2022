Imports ClosedXML
Imports ClosedXML.Excel
Imports DocumentFormat.OpenXml.Office2016.Drawing.Command
Imports MySqlConnector

Public Class 年間請求一覧

    'Const templatePath As String = "C:\template\" 2024/8/29 猿渡 Del

    Dim param As List(Of String)
    Dim mode As Integer
    Dim subjectNo As Integer
    Dim messageStr As String

    Private Sub 年間請求一覧_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Text = GetDate()
    End Sub

    Private Sub Print()

        Dim xlBook As New Excel.XLWorkbook()

        Try
            '----- ClosedXML
            Dim xlSheet = xlBook.Worksheets.Add("Sheet1")

            xlSheet.PageSetup.AddHorizontalPageBreak(30) '改ページ
            xlSheet.PageSetup.AddHorizontalPageBreak(60)
            xlSheet.PageSetup.AddHorizontalPageBreak(90)

            xlSheet.PageSetup.PagesWide = 1 '横幅全体を1ページで印刷
            xlSheet.PageSetup.PageOrientation = XLPageOrientation.Landscape '用紙を横に印刷
            xlSheet.PageSetup.PaperSize = XLPaperSize.A4Paper
            'ヘッダー・フッター
            xlSheet.PageSetup.Header.Left.AddText(GetDate() & "年度　請求一覧")
            xlSheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.PageNumber, XLHFOccurrence.AllPages)
            xlSheet.PageSetup.Footer.Center.AddText(" / ", XLHFOccurrence.AllPages)
            xlSheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.NumberOfPages, XLHFOccurrence.AllPages)

            xlSheet.Column("A").Width = 7
            xlSheet.Column("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            xlSheet.Column("B").Width = 20
            xlSheet.Column("B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            xlSheet.Column("C").Width = 55
            xlSheet.Column("D").Width = 10
            xlSheet.Column("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            xlSheet.Column("D").Style.NumberFormat.SetFormat("#,##0")
            xlSheet.Column("E").Width = 10
            xlSheet.Column("E").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            xlSheet.Column("E").Style.NumberFormat.SetFormat("#,##0")
            xlSheet.Column("F").Width = 10
            xlSheet.Column("F").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            xlSheet.Column("F").Style.NumberFormat.SetFormat("#,##0")

            xlSheet.Cell(1, 1).Value = "請求No"
            xlSheet.Cell(1, 2).Value = "請求日付"
            xlSheet.Cell(1, 3).Value = "請求先名"
            xlSheet.Cell(1, 4).Value = "工事合計"
            xlSheet.Cell(1, 5).Value = "消費税"
            xlSheet.Cell(1, 6).Value = "税込合計"

            xlSheet.Range(1, 1, 1, 6).Style.Fill.BackgroundColor = XLColor.FromArgb(155, 194, 230)

            Dim sql As String = "SELECT * FROM invoice_master WHERE " _
            & "DATE_FORMAT(invoiceDate, '%Y-%m-%d') >= '" & TextBox1.Text & "-01-01' AND " _
            & "DATE_FORMAT(invoiceDate, '%Y-%m-%d') <= '" & TextBox1.Text & "-12-31' " _
            & "ORDER BY invoiceDate, invoiceNo"

            Try
                '2024/8/29 saruwatari Mod Start ================================================
                'Using Conn As New MySqlConnection(Builder.ToString)
                Using Conn As New MySqlConnection(メインメニュー.Builder.ToString)
                    '2024/8/29 saruwatari Mod end ==============================================

                    Conn.Open()
                    Dim exlRowNumber As Integer = 2
                    Dim amount As Integer = 0
                    Dim tax As Integer = 0
                    Dim total As Integer = 0

                    Using cmd As New MySqlCommand(sql, Conn)

                        Using reader As MySqlDataReader = cmd.ExecuteReader()


                            Dim recordCount As Integer = 0
                            While (reader.Read())

                                If recordCount > 0 And recordCount Mod 29 = 0 Then

                                    xlSheet.Cell(exlRowNumber, 1).Value = "請求No"
                                    xlSheet.Cell(exlRowNumber, 2).Value = "請求日付"
                                    xlSheet.Cell(exlRowNumber, 3).Value = "請求先名"
                                    xlSheet.Cell(exlRowNumber, 4).Value = "工事合計"
                                    xlSheet.Cell(exlRowNumber, 5).Value = "消費税"
                                    xlSheet.Cell(exlRowNumber, 6).Value = "税込合計"
                                    xlSheet.Range(exlRowNumber, 1, exlRowNumber, 6).Style.Fill.BackgroundColor = XLColor.FromArgb(155, 194, 230)
                                    exlRowNumber += 1
                                End If

                                'Debug.WriteLine(reader.GetString(1))

                                xlSheet.Cell(exlRowNumber, 1).Value = reader.GetInt16(0)
                                xlSheet.Cell(exlRowNumber, 2).Value = reader.GetString(4)
                                xlSheet.Cell(exlRowNumber, 3).Value = reader.GetString(1).Replace(Environment.NewLine, " ")

                                '2024/8/28 saruwatari Mod Start===========================================================
                                'xlSheet.Cell(exlRowNumber, 4).Value = reader.GetInt32(2)
                                'xlSheet.Cell(exlRowNumber, 5).Value = reader.GetInt32(3)
                                'xlSheet.Cell(exlRowNumber, 6).Value = reader.GetInt32(2) + reader.GetInt32(3)

                                'amount += reader.GetInt32(2)
                                'tax += reader.GetInt32(3)
                                'total = total + reader.GetInt32(2) + reader.GetInt32(3)                             

                                xlSheet.Cell(exlRowNumber, 4).Value = reader.GetDecimal(2)
                                xlSheet.Cell(exlRowNumber, 5).Value = reader.GetDouble(3)
                                xlSheet.Cell(exlRowNumber, 6).Value = reader.GetDecimal(2) + reader.GetDouble(3)

                                amount += reader.GetDecimal(2)
                                tax += reader.GetDouble(3)
                                total = total + reader.GetDecimal(2) + reader.GetDouble(3)
                                '2024/8/28 saruwatari Mod End=============================================================
                                exlRowNumber += 1
                                recordCount += 1
                            End While

                            If recordCount = 0 Then
                                MessageBox.Show("該当する請求履歴はありません。")
                                TextBox1.Focus()
                                Exit Sub
                            End If
                        End Using
                    End Using

                    xlSheet.Cell(exlRowNumber, 3).Value = "合　計"
                    xlSheet.Cell(exlRowNumber, 4).Value = amount
                    xlSheet.Cell(exlRowNumber, 5).Value = tax
                    xlSheet.Cell(exlRowNumber, 6).Value = total
                    xlSheet.Range(exlRowNumber, 1, exlRowNumber, 6).Style.Border.TopBorder = XLBorderStyleValues.Medium

                End Using

                Dim output As String = Common.SelectFolder()

                If output IsNot Nothing Then
                    xlBook.SaveAs(output & "\" & TextBox1.Text & "年度請求一覧.xlsx")
                    MessageBox.Show("「" & TextBox1.Text & "年度請求一覧.xlsx」を出力しました。")
                    xlBook.Dispose()
                    Me.Close()
                Else
                    MessageBox.Show("出力先を選択して下さい。")
                End If

            Catch sqlE As MySqlConnector.MySqlException
                MessageBox.Show("データベース接続エラー：" & vbCrLf & "システム管理者へ連絡して下さい。", "予期せぬエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Dispose()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show(ex.ToString & "：" & vbCrLf & "システム管理者へ連絡して下さい。", "予期せぬエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Dispose()
                Exit Sub
            End Try

            'Me.Dispose()

        Catch ex As Exception
            MessageBox.Show("ClosedXMLの処理：" & ex.ToString)
        Finally
            'Me.Dispose()
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Print()
    End Sub

    Private Function GetDate() As String
        Dim Now As DateTime = DateTime.Now
        Return Now.ToString("yyyy")
    End Function

    Private Sub TextBox1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If TextBox1.Text.Length = 0 Then
            TextBox1.Text = ""
            TextBox1.BackColor = Color.MistyRose
            MessageBox.Show("入力必須です。")
            TextBox1.Focus()
            Exit Sub
        End If
        If Not IsNumeric(TextBox1.Text) Then
            TextBox1.Text = ""
            TextBox1.BackColor = Color.MistyRose
            MessageBox.Show("数値以外は入力できません。")
            TextBox1.Focus()
            Exit Sub
        End If
        If CInt(TextBox1.Text) < 2000 Or CInt(TextBox1.Text) > 9999 Then
            TextBox1.Text = ""
            TextBox1.BackColor = Color.MistyRose
            MessageBox.Show("四桁の有効な西暦を入力して下さい。")
            TextBox1.Focus()
            Exit Sub
        End If
    End Sub

End Class