Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Threading
Imports ClosedXML
Imports ClosedXML.Excel
Imports DocumentFormat.OpenXml.Drawing
Imports Microsoft.Office.Interop.Excel
Imports MySqlConnector
Imports Newtonsoft.Json.Linq
Imports sd = System.Drawing
Imports swf = System.Windows.Forms

Public Class 見積請求作成修正

    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()
    End Sub

    Public Sub New(mode As Integer)
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()
        execMode = mode
    End Sub

    Const templatePath As String = "C:\template\"

    Dim execMode As Integer
    Dim outputPath As String = ""
    Dim pageCount As Integer
    Dim fileName As String
    Dim clientName As String = ""
    Dim clientNo As Integer = 0
    Dim quoteNoStr As String = ""
    Dim invoiceNoStr As String = ""
    Dim invoiceDate As String
    Dim quoteDate As String '2024/9/2 saruwatari Add
    Dim allAmount As Decimal = 0
    Dim isZero As Boolean
    Dim fromQuotationDelete As Boolean
    '見積履歴削除対策でクラス変数として定義
    Dim newQuoteNo As String = ""
    'totalAmount と roundTaxをクラス変数に変更 2025/3/12 pongirasu
    Dim totalAmount As Decimal = 0
    Dim roundTax As Double = 0

    Public dgvDataList As List(Of String)

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        DataGridView1.Rows.Clear()
        DataGridView1.RowHeadersWidth = 45
        TextBox1.Text = ""

        dgvDataList = New List(Of String)

        For i As Integer = 0 To 99
            dgvDataList.Add("")
        Next

        '初期行
        DataGridView1.Rows.Add()
        DataGridView1(0, 0).Value = 1
        DataGridView1(5, 0).ReadOnly = True
        DataGridView1(5, 0).Style.BackColor = sd.Color.AliceBlue

        ' Form_Load 内の初期化箇所（DataGridView1 を設定した直後）に追加
        ' 数量列を整数表示にする
        Try
            DataGridView1.Columns(2).ValueType = GetType(Integer)
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N0"   ' 小数点なし（例: 1）
            DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Catch ex As Exception
            ' デザイン時に列がまだ存在しない場合に備えた保険（例：後で設定する）
        End Try

        'Dim newQuoteNo As String = ""　履歴削除対策でクラス変数に変更
        Me.newQuoteNo = ""
        Dim newInvoiceNo As String = ""
        '新規見積・請求作成時はそれぞれの番号を取得する
        Using Conn As New MySqlConnection(メインメニュー.Builder.ToString)

            Conn.Open()

            Dim sql As String = "SELECT * FROM control_master"
            Dim cmd As New MySqlCommand(sql, Conn)

            Using result As MySqlDataReader = cmd.ExecuteReader()

                While result.Read()
                    Me.newQuoteNo = CStr(result.GetInt16(0) + 1).PadLeft(5, "0")
                    newInvoiceNo = CStr(result.GetInt16(1) + 1).PadLeft(5, "0")
                End While

            End Using
        End Using

        Select Case execMode
            Case 0
                Me.Label3.Text = "見積書作成"
                Me.Label8.Text = "見積No."
                Me.TextBox4.Text = Me.newQuoteNo
                Me.CheckBox2.Visible = False
                Me.CheckBox2.Text = "見積書削除"
            Case 1
                Me.Label3.Text = "請求書作成"
                Me.Label8.Text = "請求No."
                Me.TextBox4.Text = newInvoiceNo
                Me.Button10.Enabled = True
                Me.Button10.Visible = True

            Case 2
                '2024/8/30 saruwatari Add Start ===========================================
                Me.Label1.Visible = False
                Me.Label3.Text = "見積書修正"
                Me.Label3.BorderStyle = BorderStyle.Fixed3D
                Me.Label3.BackColor = sd.Color.LightCyan
                Me.Label8.Text = "見積No."
                Me.BackColor = sd.Color.LightBlue
                Me.Button3.Enabled = False
                Me.Button4.Text = "見積書を選択"
                Me.Button6.Enabled = False
                Me.Button8.Enabled = False
                Me.TextBox1.Enabled = False
                Me.TextBox2.Enabled = False
                Me.CheckBox2.Text = "見積書削除" '2024/9/3 saruwatari Add
                Me.CheckBox2.Visible = True
                Me.CheckBox3.Visible = True
                Me.DataGridView1.Enabled = False
                Me.DataGridView1.Rows.RemoveAt(0) '2024/9/5 saruwatari Add
                '2024/8/30 saruwatari Add End =============================================
            Case 3
                Me.Label1.Visible = False
                Me.Label3.Text = "請求書修正"
                Me.Label3.BorderStyle = BorderStyle.Fixed3D
                Me.Label3.BackColor = sd.Color.LightCyan
                Me.Label8.Text = "請求No."
                Me.BackColor = sd.Color.LightGreen
                Me.Button3.Enabled = False
                Me.Button4.Text = "請求書を選択"
                Me.Button6.Enabled = False
                Me.Button8.Enabled = False
                Me.Button11.Enabled = False
                Me.TextBox1.Enabled = False
                Me.TextBox2.Enabled = False
                Me.Panel3.Visible = True
                Me.CheckBox2.Text = "請求書削除" '2024/9/3 saruwatari Add
                Me.CheckBox2.Visible = True
                Me.CheckBox3.Visible = True
                Me.DataGridView1.Enabled = False
                Me.DataGridView1.Rows.RemoveAt(0) '2024/9/5 saruwatari Add
        End Select

        Call RowNumberShow()

    End Sub


    Private Sub SaveData_CreatePdf(mode As Integer)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '' 現在実行しているアセンブリを取得する
        'Dim assm As Assembly = Assembly.GetExecutingAssembly()
        'Dim reader As StreamReader

        '' アセンブリに埋め込まれているリソース"Sample.resources.hello.txt"のStreamを取得する
        'Using stream As Stream = assm.GetManifestResourceStream("トータルサービスあらい.Book1.xlsx")

        '    'Streamの内容をすべて読み込んで標準出力に表示する
        '    Dim reader As New StreamReader(stream)

        '    Debug.WriteLine(reader.ReadToEnd())

        'End Using
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''
        'Dim xlBook As New Excel.XLWorkbook(templatePath & "Book1.xlsx")
        Dim xlBook As Excel.XLWorkbook

        '===========================================================================
        'Dim totalAmount As Integer = 0
        'Dim totalAmount As Decimal = 0 クラス変数に変更 2025/3/12 pongirasu Delete
        '===========================================================================
        'Dim roundTax As Double = 0　クラス変数に変更 2025/3/12 pongirasu Delete

        '見積番号／請求番号　の取得
        Dim quoteNo As Integer
        Dim invoiceNo As Integer

        Try
            Using Conn As New MySqlConnection(メインメニュー.Builder.ToString)

                Dim transaction As MySqlTransaction = Nothing

                Conn.Open()
                transaction = Conn.BeginTransaction()

                Dim sql As String = "SELECT * FROM control_master FOR UPDATE"
                Dim cmd As New MySqlCommand(sql, Conn, transaction)

                Using result As MySqlDataReader = cmd.ExecuteReader()

                    While result.Read()
                        quoteNo = result.GetInt16(0) + 1
                        invoiceNo = result.GetInt16(1) + 1
                    End While

                End Using
                '見積書修正の場合
                If execMode = 2 Then
                    quoteNo = CInt(Me.TextBox4.Text.Trim)

                    '請求書修正の場合
                ElseIf execMode = 3 Then
                    invoiceNo = CInt(Me.TextBox4.Text.Trim)
                End If

                '見積番号／請求番号 のインクリメント

                Select Case execMode
                    Case 0
                        sql = "UPDATE control_master SET quoteNo = " & CStr(quoteNo)
                    Case 1
                        sql = "UPDATE control_master SET invoiceNo = " & CStr(invoiceNo)
                End Select

                cmd.CommandText = sql

                Dim respons As Integer = cmd.ExecuteNonQuery

                If respons = 1 Then
                    transaction.Commit()
                Else
                    transaction.Rollback()
                End If

                Conn.Close()

            End Using

            Select Case execMode
                Case 0
                    quoteNoStr = CStr(quoteNo).PadLeft(5, "0"c)
                Case 1
                    invoiceNoStr = CStr(invoiceNo).PadLeft(5, "0"c)
                '2024/8/30 saruwatari Add Start =================================================
                Case 2
                    quoteNoStr = CStr(quoteNo).PadLeft(5, "0"c)
                '2024/8/30 saruwatari Add End ===================================================
                Case 3
                    invoiceNoStr = CStr(invoiceNo).PadLeft(5, "0"c)
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim dgvRowCount As Integer = DataGridView1.Rows.Count

        If Not CheckBox3.Checked Then

            '#################
            Me.totalAmount = 0
            '#################

            xlBook = New Excel.XLWorkbook(templatePath & "Book1.xlsx")

            Try
                '----- ClosedXML
                Dim xlSheet As Excel.IXLWorksheet = xlBook.Worksheet("見積書")

                xlSheet.Cell("K5").Value = CStr(メインメニュー.ht.Item("Name"))
                xlSheet.Cell("K6").Value = CStr(メインメニュー.ht.Item("InvoiceNo"))
                xlSheet.Cell("K7").Value = CStr(メインメニュー.ht.Item("PostNo"))
                xlSheet.Cell("K8").Value = CStr(メインメニュー.ht.Item("Address"))
                xlSheet.Cell("K9").Value = CStr(メインメニュー.ht.Item("TelFax"))

                Select Case execMode
                    Case 0
                        xlSheet.Cell("M2").Value = quoteNoStr
                        '見積書では振込先を表示しない
                        xlSheet.Cell("K11").Value = ""
                        xlSheet.Cell("K12").Value = ""
                        xlSheet.Cell("K13").Value = ""
                    Case 1
                        xlSheet.Cell("M2").Value = invoiceNoStr
                    '2024/8/30 saruwatari Add Start ===============================================
                    Case 2
                        xlSheet.Cell("M2").Value = quoteNoStr
                        '見積書では振込先を表示しない
                        xlSheet.Cell("K11").Value = ""
                        xlSheet.Cell("K12").Value = ""
                        xlSheet.Cell("K13").Value = ""
                    '2024/8/30 saruwatari Add End =================================================
                    Case 3
                        xlSheet.Cell("M2").Value = invoiceNoStr
                End Select

                '日付指定の時
                If CheckBox1.Checked Then
                    Me.DateTimePicker1.Enabled = True
                End If

                xlSheet.Cell("M3").Value = Me.DateTimePicker1.Value.ToString("yyyy年MM月dd日")
                quoteDate = Me.DateTimePicker1.Value.ToString("yyyy-MM-dd") '2024/9/2 saruwatari Add
                invoiceDate = Me.DateTimePicker1.Value.ToString("yyyy-MM-dd")
                fileName = Me.DateTimePicker1.Value.ToString("yyyyMMdd_")

                xlSheet.Cell("B1").Value = Me.TextBox1.Text.ToString
                xlSheet.Cell("K17").Value = Me.TextBox2.Text.ToString

                If execMode = 1 Or execMode = 3 Then
                    xlSheet.Cell("G4").Value = "御　請　求　書"
                    xlSheet.Cell("L2").Value = "　請求No"
                    xlSheet.Cell("L3").Value = "　請求日"
                    xlSheet.Cell("B8").Value = "下記のとおり、御請求申し上げます。"
                    xlSheet.Cell("K11").Value = CStr(メインメニュー.ht.Item("BankInfo"))
                    xlSheet.Cell("K12").Value = CStr(メインメニュー.ht.Item("BankName"))
                    xlSheet.Cell("K13").Value = CStr(メインメニュー.ht.Item("BankNo"))
                End If

                For i As Integer = 1 To dgvRowCount

                    Dim excRowNo As Integer
                    '1頁目21行　2頁目以降34行
                    If i <= 21 Then
                        excRowNo = 17
                        pageCount = 1
                        If dgvRowCount <= 20 Then
                            xlSheet.Cell("B37").Value = "合　計"
                        ElseIf dgvRowCount = 21 Then
                            xlSheet.Cell("B73").Value = "合　計"
                            pageCount = 2
                        End If
                    ElseIf i > 21 And i <= 55 Then
                        excRowNo = 19
                        pageCount = 2
                        If dgvRowCount <= 54 Then
                            xlSheet.Cell("B73").Value = "合　計"
                        ElseIf dgvRowCount = 55 Then
                            xlSheet.Cell("B109").Value = "合　計"
                            pageCount = 3
                        End If
                    ElseIf i > 55 And i <= 88 Then
                        excRowNo = 21
                        pageCount = 3
                        xlSheet.Cell("B109").Value = "合　計"
                    Else
                        Exit Sub
                    End If

                    '工事名 数量 単位
                    xlSheet.Cell(i + excRowNo - 1, 2).Value = NothingToEmpty(DataGridView1.Rows(i - 1).Cells(1).Value)
                    xlSheet.Cell(i + excRowNo - 1, 5).Value = NothingToEmpty(DataGridView1.Rows(i - 1).Cells(2).Value)
                    xlSheet.Cell(i + excRowNo - 1, 6).Value = NothingToEmpty(DataGridView1.Rows(i - 1).Cells(3).Value)

                    Dim unitAmount As String = NothingToEmpty(DataGridView1.Rows(i - 1).Cells(4).Value)
                    If Not unitAmount.Equals("") Then
                        '単価
                        xlSheet.Cell(i + excRowNo - 1, 7).Value = CType(unitAmount, Decimal)
                    End If

                    '金額
                    Dim subAmout As String = NothingToEmpty(DataGridView1.Rows(i - 1).Cells(5).Value)

                    If Not subAmout.Equals("") Then
                        '金額
                        xlSheet.Cell(i + excRowNo - 1, 8).Value = CType(subAmout, Decimal)
                    End If

                    '備考
                    xlSheet.Cell(i + excRowNo - 1, 9).Value = NothingToEmpty(DataGridView1.Rows(i - 1).Cells(6).Value)

                    Dim amount As String = NothingToEmpty(DataGridView1.Rows(i - 1).Cells(5).Value)
                    If Not amount.Equals("") Then
                        totalAmount += CType(amount, Decimal)
                    End If

                Next

                Select Case pageCount
                    Case 1
                        xlSheet.Cell("B37").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("C37").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("D37").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Range("E37:I37").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("H37").Value = totalAmount
                    Case 2
                        xlSheet.Cell("B73").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("C73").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("D73").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Range("E73:I73").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("H73").Value = totalAmount
                    Case 3
                        xlSheet.Cell("B109").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("C109").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("D109").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Range("E109:I109").Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                        xlSheet.Cell("H109").Value = totalAmount
                End Select
                xlSheet.Cell("L24").Value = totalAmount
                Dim taxExact As Decimal = totalAmount * 0.1D
                roundTax = Decimal.Floor(taxExact)


                xlSheet.Cell("L26").Value = roundTax
                xlSheet.Cell("L27").Value = roundTax + totalAmount
                xlSheet.Cell("C11").Value = roundTax + totalAmount

                xlBook.SaveAs(templatePath & "Book1000.xlsx")

            Catch ex As Exception
                Debug.WriteLine("ClosedXMLの処理：" & ex.ToString)
                GC.Collect()
                'xlBook = Nothing
                'xlBook.Dispose()
            Finally
                Dim g As Integer = 0
                Dim pb As Boolean = True

                xlBook.Dispose()
                GC.Collect()
                pb = False
            End Try
        Else
            quoteDate = Me.DateTimePicker1.Value.ToString("yyyy-MM-dd")
            invoiceDate = Me.DateTimePicker1.Value.ToString("yyyy-MM-dd")
            fileName = Me.DateTimePicker1.Value.ToString("yyyyMMdd_")
        End If

        '総合計が０ならばエラー
        If totalAmount = 0 Then
            isZero = True
        End If

        'データ登録
        'Dim sqString As String = ""
        Try
            Using Conn As New MySqlConnection(メインメニュー.Builder.ToString)

                Dim transaction As MySqlTransaction = Nothing

                Conn.Open()
                transaction = Conn.BeginTransaction()

                Dim sql As String = ""
                Dim cmd As New MySqlCommand(sql, Conn, transaction)
                Dim deleteString As String
                Dim tax As Double

                Select Case execMode
                    Case 0
                        sql = "INSERT INTO quotation_master VALUES(" & CStr(quoteNo) & ", '" & Me.TextBox1.Text & "', " _
                            & CStr(totalAmount) & ", " & CStr(roundTax) & ", '" & TextBox2.Text _
                            & "', SYSDATE()" & ", " & "SYSDATE() , '" & quoteDate & "')"
                    Case 1
                        sql = "INSERT INTO invoice_master VALUES(" & CStr(invoiceNo) & ", '" & Me.TextBox1.Text & "', " _
                            & CStr(totalAmount) & ", " & CStr(roundTax) & ", '" & invoiceDate & "', " _
                            & "SYSDATE()" & ", " & "SYSDATE(), '" & TextBox2.Text & "')"
                    Case 2
                        deleteString = "DELETE FROM quotation_master WHERE quoteNo = " & CStr(quoteNo)
                        cmd.CommandText = deleteString
                        cmd.ExecuteNonQuery()

                        deleteString = "DELETE FROM quotation_detail WHERE quoteNo = " & CStr(quoteNo)
                        cmd.CommandText = deleteString
                        cmd.ExecuteNonQuery()

                        If CheckBox3.Checked Then
                            totalAmount = Accounting()
                            tax = totalAmount / 10
                            roundTax = Math.Round(tax, MidpointRounding.AwayFromZero)
                        End If

                        sql = "INSERT INTO quotation_master VALUES(" & CStr(quoteNo) & ", '" & Me.TextBox1.Text & "', " _
                            & CStr(totalAmount) & ", " & CStr(roundTax) & ", '" & TextBox2.Text _
                            & "', SYSDATE()" & ", " & "SYSDATE() , '" & quoteDate & "')"
                    Case 3
                        deleteString = "DELETE FROM invoice_master WHERE invoiceNo = " & CStr(invoiceNo)

                        cmd.CommandText = deleteString
                        cmd.ExecuteNonQuery()

                        deleteString = "DELETE FROM invoice_detail WHERE invoiceNo = " & CStr(invoiceNo)
                        cmd.CommandText = deleteString
                        cmd.ExecuteNonQuery()

                        If CheckBox3.Checked Then
                            totalAmount = Accounting()
                            tax = totalAmount / 10
                            roundTax = Math.Round(tax, MidpointRounding.AwayFromZero)
                        End If

                        sql = "INSERT INTO invoice_master VALUES(" & CStr(invoiceNo) & ", '" & Me.TextBox1.Text & "', " _
                            & CStr(totalAmount) & ", " & CStr(roundTax) & ", '" & invoiceDate & "', " _
                            & "SYSDATE()" & ", " & "SYSDATE()" & ", '" & TextBox2.Text & "')"
                End Select

                cmd.CommandText = sql

                Dim respons As Integer = cmd.ExecuteNonQuery

                If respons = 1 Then
                    transaction.Commit()
                Else
                    transaction.Rollback()
                End If

                Conn.Close()

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        For i As Integer = 1 To dgvRowCount

            Try
                Using conn As New MySqlConnection(メインメニュー.Builder.ToString)

                    Dim transaction As MySqlTransaction = Nothing

                    conn.Open()
                    transaction = conn.BeginTransaction()

                    Dim sql As String = ""

                    Select Case execMode
                        Case 0
                            sql = "INSERT INTO quotation_detail VALUES(" & CStr(quoteNo) & ", '"
                        Case 1
                            sql = "INSERT INTO invoice_detail VALUES(" & CStr(invoiceNo) & ", '"
                        Case 2
                            sql = "INSERT INTO quotation_detail VALUES(" & CStr(quoteNo) & ", '"
                        Case 3
                            sql = "INSERT INTO invoice_detail VALUES(" & CStr(invoiceNo) & ", '"
                    End Select

                    sql = sql & NothingToEmpty(DataGridView1.Rows(i - 1).Cells(1).Value) & "' ," _
                            & NothingToNull(DataGridView1.Rows(i - 1).Cells(2).Value) & " , '" _
                            & NothingToEmpty(DataGridView1.Rows(i - 1).Cells(3).Value) & "' ," _
                            & NothingToNull(DataGridView1.Rows(i - 1).Cells(4).Value) & " ," _
                            & NothingToNull(DataGridView1.Rows(i - 1).Cells(5).Value) & " , '" _
                            & NothingToEmpty(DataGridView1.Rows(i - 1).Cells(6).Value) & "', " _
                            & NothingToNull(DataGridView1.Rows(i - 1).Cells(0).Value) & ", " _
                            & i - 1 & " )"

                    Dim cmd As New MySqlCommand(sql, conn, transaction)

                    Dim respons As Integer = cmd.ExecuteNonQuery

                    If respons = 1 Then
                        transaction.Commit()
                    Else
                        transaction.Rollback()
                    End If

                    conn.Close()

                End Using

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

        Next

        '----- Microsoft.Office.Interop.Excel
        Dim _application As Application
        Dim _workbook As Microsoft.Office.Interop.Excel.Workbook
        Dim disposedValue As Boolean = False

        Try
            If Not CheckBox3.Checked Then

                Common.SetDefaultPrinter("Microsoft Print to PDF")

                _application = New Application()
                _workbook = _application.Workbooks.Open(templatePath & "Book1000.xlsx")

                _workbook.Worksheets.Select()

                Select Case execMode
                    Case 0
                        fileName = fileName & quoteNoStr & "_" & Common.GetDate() & "_見積.pdf"
                    Case 1
                        fileName = fileName & invoiceNoStr & "_" & Common.GetDate() & "_請求.pdf"
                    '2024/8/30 saruwatari Add Start =======================================================
                    Case 2
                        fileName = fileName & quoteNoStr & "_" & Common.GetDate() & "_見積.pdf"
                    '2024/8/30 saruwatari Add End =========================================================
                    Case 3
                        fileName = fileName & invoiceNoStr & "_" & Common.GetDate() & "_請求.pdf"
                End Select

                _workbook.ExportAsFixedFormat(
                    XlFixedFormatType.xlTypePDF, outputPath & "\" & fileName, XlFixedFormatQuality.xlQualityMinimum, , , 1, pageCount)

                _workbook.Close()
                Marshal.ReleaseComObject(_workbook)
                _workbook = Nothing
                _application.Quit()
                Marshal.ReleaseComObject(_application)
                _application = Nothing
            Else
                '請求書修正で保存のみ
                Select Case execMode
                    Case 2
                        MessageBox.Show("見積書の修正を保存しました。")
                    Case 3
                        MessageBox.Show("請求書の修正を保存しました。")
                End Select

            End If
            '_workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, outputPath & "\" & fileName, XlFixedFormatQuality.xlQualityMinimum, , , 1, pageCout)

        Catch ex As Exception
            'MessageBox.Show("Microsoft.Office.Interop.Excelの処理：" & ex.ToString)
            Debug.WriteLine("Microsoft.Office.Interop.Excelの処理：" & ex.ToString)
        Finally
            'If Not CheckBox3.Checked And Not _workbook.Equals(vbNull) Then

            '_workbook = Nothing
            'Marshal.ReleaseComObject(_workbook)
            '_workbook.Close()
            'End If

            'If Not CheckBox3.Checked And Not _application.Equals(vbNull) Then
            '_application.Quit()
            'Marshal.ReleaseComObject(_application)
            '_application = Nothing
            'End If
        End Try

        '#########################################################
        'ここからは実質的に必要ない
        '#########################################################
        'If mode = 0 Then
        '    'PDF印刷（印刷結果に赤字でメッセージが入る）
        '    Dim doc As PdfDocument = New PdfDocument()
        '    Try
        '        doc.LoadFromFile(templatePath & "test.pdf")
        '        'doc.PrintSettings.PrinterName = "Microsoft Print to PDF"

        '        '印刷ファイルのパスと名前を設定する
        '        'doc.PrintSettings.PrintToFile("PrintToXps.xps")
        '        doc.Print()
        '    Catch ex As Exception
        '        MessageBox.Show("Spire.Pdfの印刷処理：" & ex.ToString)
        '    Finally
        '        doc.Close()
        '        doc.Dispose()
        '    End Try
        'ElseIf mode = 9 Then
        '    'Excel印刷
        '    Dim xlApp As New Application()
        '    Dim book As Workbook = xlApp.Workbooks.Open(templatePath & "Book1000.xlsx")
        '    Try
        '        book.Worksheets.Select()

        '        'ProgressBar1.Minimum = 0
        '        'ProgressBar1.Maximum = 20
        '        'ProgressBar1.Value = 0

        '        'book.PrintOutEx(1, pageCout, 1, Preview:=False, ActivePrinter:="Microsoft Print to PDF")
        '        'book.PrintOutEx(1, 1, 1, True, "Microsoft Print to PDF")

        '    Catch ex As Exception
        '        Console.WriteLine("Microsoft.Office.Interop.Excelの印刷処理：" & ex.ToString)
        '    Finally
        '        book.Close()
        '        Marshal.ReleaseComObject(book)
        '        book = Nothing
        '    End Try

        'Else
        '    'Exit Sub
        'End If

        'Dim message As String = ""
        'Select Case execMode
        '    Case 0
        '        message = "見積書を出力しました。"
        '    Case 1
        '        message = "請求書を出力しました。"
        'End Select
        'MessageBox.Show(message)

        'Me.Button2.PerformClick()

        'Call RowNoShow()

    End Sub

    'グリッド・セルクリックイベント
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim index As Integer = e.ColumnIndex
        Dim rowIndex As Integer = DataGridView1.CurrentRow.Index

        Try
            'DataGridView1.BeginEdit(False)

            Select Case index

                Case 0
                    '入力モード選択
                    Dim fm As New モード選択
                    fm.StartPosition = FormStartPosition.CenterParent
                    fm.ShowDialog(Me)

                    If fm.getIndex = -1 Then
                        Exit Sub
                    End If

                    DataGridView1.Rows(rowIndex).Cells(0).Value = fm.getIndex
                    DataGridView1.EndEdit()

                    If fm.getIndex = 0 Then
                        For i As Integer = 1 To 6
                            DataGridView1(i, rowIndex).ReadOnly = True
                            DataGridView1(i, rowIndex).Value = ""
                            DataGridView1(i, rowIndex).Style.BackColor = sd.Color.LightGray
                        Next
                    ElseIf fm.getIndex = 1 Then
                        DataGridView1(0, rowIndex).Style.BackColor = sd.Color.LightSteelBlue
                        For i As Integer = 1 To 6
                            DataGridView1(i, rowIndex).ReadOnly = False
                            DataGridView1(i, rowIndex).Style.BackColor = sd.Color.White
                        Next
                        '金額列は入力不可
                        DataGridView1(5, rowIndex).ReadOnly = True
                        DataGridView1(5, rowIndex).Style.BackColor = sd.Color.AliceBlue
                    Else
                        DataGridView1(0, rowIndex).Style.BackColor = sd.Color.LightSteelBlue
                        DataGridView1(1, rowIndex).ReadOnly = False
                        DataGridView1(1, rowIndex).Style.BackColor = sd.Color.White
                        For i As Integer = 2 To 4
                            DataGridView1(i, rowIndex).ReadOnly = True
                            DataGridView1(i, rowIndex).Style.BackColor = sd.Color.LightGray
                            DataGridView1(i, rowIndex).Value = ""
                        Next
                        DataGridView1(5, rowIndex).ReadOnly = True
                        DataGridView1(5, rowIndex).Style.BackColor = sd.Color.LightGray
                        DataGridView1(5, rowIndex).Value = ""
                        DataGridView1(6, rowIndex).ReadOnly = False
                        DataGridView1(6, rowIndex).Style.BackColor = sd.Color.White
                    End If

                Case 1
                    '工事名選択
                    If DataGridView1(0, rowIndex).Value = 1 Or DataGridView1(0, rowIndex).Value = 2 Then

                        Dim fm As New 工事一覧取得(dgvDataList, rowIndex)
                        fm.StartPosition = FormStartPosition.CenterParent
                        fm.ShowDialog(Me)

                        Try
                            'subjectName 0, unitName 1, execMode 2
                            Dim array As String() = StringSplitter(dgvDataList(rowIndex))

                            If array.Length = 3 Then

                                '---------------------------------------------------------------------
                                '工事一覧からの値設定を決定させる為、カレントセルを「モード」に移動
                                DataGridView1.CurrentCell = DataGridView1.Rows(rowIndex).Cells(0)
                                '---------------------------------------------------------------------

                                DataGridView1(1, rowIndex).Value = array(0)
                                DataGridView1(0, rowIndex).Value = array(2)

                                If array(2).Equals("2") Then
                                    DataGridView1.Rows(rowIndex).Cells(2).Value = ""
                                    DataGridView1.Rows(rowIndex).Cells(3).Value = ""
                                    DataGridView1.Rows(rowIndex).Cells(4).Value = ""
                                    DataGridView1.Rows(rowIndex).Cells(5).Value = ""
                                    DataGridView1.Rows(rowIndex).Cells(6).Value = ""
                                    DataGridView1.Rows(rowIndex).Cells(2).ReadOnly = True
                                    DataGridView1.Rows(rowIndex).Cells(3).ReadOnly = True
                                    DataGridView1.Rows(rowIndex).Cells(4).ReadOnly = True
                                    DataGridView1.Rows(rowIndex).Cells(5).ReadOnly = True
                                    DataGridView1.Rows(rowIndex).Cells(2).Style.BackColor = sd.Color.LightGray
                                    DataGridView1.Rows(rowIndex).Cells(3).Style.BackColor = sd.Color.LightGray
                                    DataGridView1.Rows(rowIndex).Cells(4).Style.BackColor = sd.Color.LightGray
                                    DataGridView1.Rows(rowIndex).Cells(5).Style.BackColor = sd.Color.LightGray
                                    '---------------------------------------------------------------------
                                    DataGridView1.CurrentCell = DataGridView1(6, rowIndex)
                                    DataGridView1.BeginEdit(True)
                                    '---------------------------------------------------------------------
                                ElseIf array(2).Equals("1") Then
                                    DataGridView1.Rows(rowIndex).Cells(2).Value = ""
                                    DataGridView1.Rows(rowIndex).Cells(4).Value = ""

                                    DataGridView1.Rows(rowIndex).Cells(6).Value = ""
                                    DataGridView1.Rows(rowIndex).Cells(2).ReadOnly = False
                                    DataGridView1.Rows(rowIndex).Cells(3).ReadOnly = False
                                    DataGridView1.Rows(rowIndex).Cells(4).ReadOnly = False
                                    DataGridView1.Rows(rowIndex).Cells(5).ReadOnly = True
                                    DataGridView1.Rows(rowIndex).Cells(2).Style.BackColor = sd.Color.White
                                    DataGridView1.Rows(rowIndex).Cells(3).Style.BackColor = sd.Color.White
                                    DataGridView1.Rows(rowIndex).Cells(4).Style.BackColor = sd.Color.White
                                    DataGridView1.Rows(rowIndex).Cells(5).Style.BackColor = sd.Color.AliceBlue

                                    DataGridView1.Rows(rowIndex).Cells(3).Value = array(1)
                                    '---------------------------------------------------------------------
                                    DataGridView1.CurrentCell = DataGridView1(2, rowIndex)
                                    DataGridView1.BeginEdit(True)
                                    '---------------------------------------------------------------------
                                Else
                                    DataGridView1.Rows(rowIndex).Cells(3).Value = array(1)
                                End If

                            End If

                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    End If

                Case 3

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        Finally
            'Me.DataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            'DataGridView1.EndEdit()
        End Try

    End Sub

    '顧客リスト選択
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

        Dim index As Integer = 0

        Select Case index
            Case 0
                Dim fm4 As New 顧客一覧取得()
                Dim res As String = fm4.ShowDialog(Me)

                Try
                    Me.TextBox1.Text = fm4.ClientName
                    Me.clientNo = fm4.ClientNo

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
            Case 1

        End Select
    End Sub

    'PDF出力ボタン
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim isEmpty As Boolean = True

        For j As Integer = 0 To DataGridView1.Rows.Count - 1

            For i As Integer = 1 To 6
                If NothingToEmpty(DataGridView1.Rows(j).Cells(i).Value).Length > 0 Then
                    isEmpty = False
                End If
            Next

        Next

        If TextBox1.Text.Length = 0 Then
            MessageBox.Show("宛先の顧客名が空欄です。")
            Exit Sub
        End If

        '入力値がない場合は処理を中断する
        If isEmpty Then
            MessageBox.Show("明細に入力がありません。")
            Exit Sub
        End If

        '入力値が数値でない場合は処理を中断する
        'If isNum Then
        '    MessageBox.Show("数値ではありません。")
        '    Exit Sub
        'End If

        If allAmount = 0 Then
            If execMode = 0 Then
                MessageBox.Show("「見積金額＝０」の見積書は出力できません。")
            ElseIf execMode = 1 Then
                MessageBox.Show("「見積金額＝０」の見積書は出力できません。")
            ElseIf execMode = 2 Then
                MessageBox.Show("「見積金額＝０」の見積書は出力できません。")
            ElseIf execMode = 3 Then
                MessageBox.Show("「請求金額＝０」の請求書は出力できません。")
            End If

            Exit Sub
        End If


        If Not DataGridCheck() Then
            'MessageBox.Show("必須項目が入力されていません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else

            'Common.SetDefaultPrinter("Microsoft Print to PDF")

            If CheckBox3.Checked Then

                Panel2.Visible = True

                Call DoCreatePdf()

                'TextBox4.Text = ""
                'CheckBox3.Checked = False
                Button1.Text = "PDF出力"
            Else
                If SelectFolder() Then
                    Panel2.Visible = True
                    Call DoCreatePdf()
                Else
                    Exit Sub
                End If
            End If
        End If

        Call RowNumberShow()
    End Sub

    '画面クリア
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Form1_Load(sender, e)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        TextBox1.Enabled = True
        TextBox2.Enabled = True

        allAmount = 0

        Label1.Enabled = True
        'Label3.BackColor = Control.DefaultBackColor 2024/9/4 saruwatari Del
        Button1.Text = "PDF出力"
        Button1.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button6.Enabled = True
        Button7.Visible = False
        Button8.Enabled = True
        Button11.Enabled = True

        If execMode = 2 Then
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox4.Text = ""
            Button3.Enabled = False
            Button6.Enabled = False
            Button8.Enabled = False

        ElseIf execMode = 3 Then
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox4.Text = ""
            Button3.Enabled = False
            Button6.Enabled = False
            Button8.Enabled = False
            Button10.Enabled = False
        End If

    End Sub

    '行追加
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.Rows.Count >= 87 Then
            MessageBox.Show("制限超過で追加できません。" & vbCrLf & "（最大行数 87行）")
            Exit Sub
        End If

        If Not DataGridCheck() Then
            'MessageBox.Show("必須項目が入力されていません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'DataGridView1.CurrentCell = DataGridView1(0, 1)
            Exit Sub
        End If

        Dim lastRow = DataGridView1.Rows.Count
        Dim crrentRow = DataGridView1.CurrentCell.RowIndex

        If CheckBox4.Checked Then
            DataGridView1.Rows.Insert(crrentRow)
            DataGridView1(0, crrentRow).Value = 1
            DataGridView1(5, crrentRow).Style.BackColor = Color.AliceBlue
            DataGridView1(5, crrentRow).ReadOnly = True
            DataGridView1.CurrentCell = DataGridView1(0, crrentRow)
        Else
            DataGridView1.Rows.Add()
            DataGridView1(0, lastRow).Value = 1
            DataGridView1(5, lastRow).Style.BackColor = Color.AliceBlue
            DataGridView1(5, lastRow).ReadOnly = True
            DataGridView1.CurrentCell = DataGridView1(0, lastRow)
        End If

        RowNumberShow()
    End Sub

    ''' <summary>
    ''' 読取りボタンを押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    'Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click
    '    ' 入力されていれば、次の処理へ
    '    If Me.tbxReadColumn.Text <> "" AndAlso Me.tbxReadRow.Text <> "" Then
    '        ' Excelファイルを読み取る準備
    '        Dim xlBook As New Excel.XLWorkbook("E:\ブログ\VisualStudio\Excel操作\test.xlsx")
    '        Dim xlSheet As Excel.IXLWorksheet = xlBook.Worksheet("Sheet1")
    '        ' シートの読み取り
    '        Dim result As String = xlSheet.Cell(Me.tbxReadColumn.Text & Me.tbxReadRow.Text).Value.ToString
    '        Console.WriteLine(xlSheet.Cell(2, 2).Value)
    '        ' 読み取った情報を表示
    '        Me.lbReadResult.Text = result
    '    End If
    'End Sub

    '見積履歴から選択
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim index As Integer = 0
        Dim keyNo As Integer

        DataGridView1.Rows.Clear()
        DataGridView1.Rows.Add()

        Select Case execMode
            Case 0
                Dim fm5 As New 見積請求履歴取得(0)
                Dim res As String = fm5.ShowDialog(Me)
                Try
                    If fm5.GetKeyNo = -1 Then
                        DataGridView1.Rows(0).Cells(0).Value = 1
                        Exit Sub
                    Else
                        keyNo = fm5.GetKeyNo
                        '削除時はその見積書番号を設定し、それ以外は新しい見積書番号を設定
                        If Me.CheckBox2.Checked Then
                            Me.TextBox4.Text = CStr(keyNo).PadLeft(5, "0"c)
                        Else

                        End If
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
            Case 1
                Select Case index
                    Case 0

                        Dim fm5 As New 見積請求履歴取得(0)
                        Dim res As String = fm5.ShowDialog(Me)
                        Try
                            If fm5.GetKeyNo = -1 Then
                                DataGridView1.Rows(0).Cells(0).Value = 1
                                Exit Sub
                            Else
                                keyNo = fm5.GetKeyNo
                            End If

                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    Case 1

                End Select
            '2024/8/30 saruwatari Add Start =====================================================
            Case 2
                '見積書修正・削除
                Dim fm5 As New 見積請求履歴取得(2)
                Dim res As String = fm5.ShowDialog(Me)
                Try
                    If fm5.GetKeyNo = -1 Then
                        DataGridView1.Rows(0).Cells(0).Value = 1
                        Exit Sub
                    Else
                        keyNo = fm5.GetKeyNo
                        'Me.CheckBox2.Checked
                        Me.TextBox4.Text = CStr(keyNo).PadLeft(5, "0"c)

                        Dim execDate As String = fm5.GetDateQuoteStr
                        Dim ymd As New Date(execDate.Substring(0, 4), execDate.Substring(5, 2), execDate.Substring(8, 2))
                        DateTimePicker1.Value = ymd
                    End If

                    Me.Label1.Visible = True
                    Me.TextBox1.Enabled = True
                    Me.TextBox2.Enabled = True
                    Me.Button3.Enabled = True
                    Me.Button6.Enabled = True
                    Me.Button8.Enabled = True
                    Me.Button11.Enabled = True
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
            '2024/8/30 saruwatari End ============================================================
            Case 3
                '請求書修正・削除
                Dim fm5 As New 見積請求履歴取得(3)
                Dim res As String = fm5.ShowDialog(Me)
                Try
                    If fm5.GetKeyNo = -1 Then
                        DataGridView1.Rows(0).Cells(0).Value = 1
                        Exit Sub
                    Else
                        keyNo = fm5.GetKeyNo
                        Me.TextBox4.Text = CStr(keyNo).PadLeft(5, "0"c)

                        '初期値はその請求書の日付
                        'Dim execDate As String = fm5.GetDateStr
                        Dim execDate As String = fm5.GetDateInvoiceStr
                        Dim ymd As New Date(execDate.Substring(0, 4), execDate.Substring(5, 2), execDate.Substring(8, 2))
                        DateTimePicker1.Value = ymd
                    End If

                    Me.Label1.Visible = True
                    Me.TextBox1.Enabled = True
                    Me.TextBox2.Enabled = True
                    Me.Button3.Enabled = True
                    Me.Button6.Enabled = True
                    Me.Button8.Enabled = True
                    Me.Button10.Enabled = True
                    Me.Button10.Visible = True
                    Me.Button11.Enabled = True
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
        End Select

        Try
            '2024/8/29 saruwatari Mod Start =========================================
            'Using Conn As New MySqlConnection(Builder.ToString)
            Using Conn As New MySqlConnection(メインメニュー.Builder.ToString)
                '2024/8/29 saruwatari Mod End =======================================

                Conn.Open()

                '履歴マスタ
                Dim sql As String = ""
                Select Case execMode
                    Case 0
                        sql = "SELECT clientName, remarks FROM quotation_master WHERE quoteNo = " & keyNo
                    Case 1
                        sql = "SELECT clientName, remarks FROM quotation_master WHERE quoteNo = " & keyNo
                    '2024/8/30 saruwatari Add Start ===========================================================
                    Case 2
                        sql = "SELECT clientName, remarks FROM quotation_master WHERE quoteNo = " & keyNo
                    '2024/8/30 saruwatari Add End =============================================================
                    Case 3
                        sql = "SELECT clientName, remarks FROM invoice_master WHERE invoiceNo = " & keyNo
                End Select

                Dim cmd As New MySqlCommand(sql, Conn)

                Using result As MySqlDataReader = cmd.ExecuteReader()
                    result.Read()
                    Me.TextBox1.Text = DBNullToEmpty(result, 0)
                    Me.TextBox2.Text = NothingToEmpty(result.GetString(1))
                End Using

                '履歴明細
                Select Case execMode
                    Case 0
                        sql = "SELECT * FROM quotation_detail WHERE quoteNo = " & keyNo & " ORDER BY lineNo"
                    Case 1
                        sql = "SELECT * FROM quotation_detail WHERE quoteNo = " & keyNo & " ORDER BY lineNo"
                    '2024/8/30 saruwatari Add Start ===========================================================
                    Case 2
                        sql = "SELECT * FROM quotation_detail WHERE quoteNo = " & keyNo & " ORDER BY lineNo"
                    '2024/8/30 saruwatari Add End =============================================================
                    Case 3
                        '請求書修正・削除
                        sql = "SELECT * FROM invoice_detail WHERE invoiceNo = " & keyNo & " ORDER BY lineNo"
                End Select

                cmd = New MySqlCommand(sql, Conn)
                allAmount = 0

                Using result As MySqlDataReader = cmd.ExecuteReader()

                    Dim i As Integer = 0
                    While result.Read()

                        DataGridView1.Rows(i).Cells(1).Value = DBNullToEmpty(result, 1)
                        DataGridView1.Rows(i).Cells(2).Value = DBNullToNothing(result, 2, 1)
                        DataGridView1.Rows(i).Cells(3).Value = DBNullToEmpty(result, 3)
                        DataGridView1.Rows(i).Cells(4).Value = DBNullToNothing(result, 4, 1)
                        DataGridView1.Rows(i).Cells(5).Value = DBNullToNothing(result, 5, 1)

                        '==============================================================================================
                        'allAmount += CInt(DBNullToNothing(result, 5))
                        'allAmount += CType(DBNullToNothing(result, 5, 1), Double)
                        allAmount += CType(DBNullToNothing(result, 5, 1), Decimal)
                        '==============================================================================================

                        DataGridView1.Rows(i).Cells(6).Value = DBNullToEmpty(result, 6)

                        Dim mode As Integer = CInt(DBNullToNothing(result, 7, 0))
                        DataGridView1.Rows(i).Cells(0).Value = mode
                        DataGridView1.Rows.Add()

                        Select Case mode
                            Case 0
                                For j As Integer = 1 To 6
                                    DataGridView1.Rows(i).Cells(j).Style.BackColor = sd.Color.LightGray
                                    DataGridView1.Rows(i).Cells(j).ReadOnly = True
                                Next
                            Case 1
                                DataGridView1.Rows(i).Cells(5).Style.BackColor = sd.Color.AliceBlue
                                DataGridView1.Rows(i).Cells(5).ReadOnly = True
                            Case 2
                                For j As Integer = 2 To 5
                                    DataGridView1.Rows(i).Cells(j).Style.BackColor = sd.Color.LightGray
                                    DataGridView1.Rows(i).Cells(j).ReadOnly = True
                                Next

                        End Select

                        i += 1
                    End While

                    DataGridView1.Rows.Remove(DataGridView1.Rows(DataGridView1.Rows.Count - 1))

                End Using

                Conn.Close()

            End Using

            '20240812 =============================================
            'TextBox3.Text = String.Format("{0:#,0}", allAmount)
            TextBox3.Text = allAmount

            Button8.Enabled = True
            Button11.Enabled = True
            DataGridView1.Enabled = True

            Call RowNumberShow()


            ' 値引き行があれば確認する
            If HasDiscountRows() Then
                Dim res As DialogResult = MessageBox.Show("既存の値引き行を削除します。よろしいですか？" & vbCrLf &
                                                          "削除しない場合、値引き額が不整合となる可能性があります。" & vbCrLf &
                                                          "既存の値引き行を削除することをお勧めします。",
                                                          "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If res = DialogResult.Yes Then
                    ' 既存の値引き行を削除して表示を更新（RemoveDiscountRows は合計更新も行う想定）
                    RemoveDiscountRows(True)
                Else
                    ' ユーザが「いいえ」を選んだ場合は以降の処理を継続するか中止するか選べます。
                    ' ここでは継続する（既存の値引きを維持したまま履歴選択等を行う）挙動としています。
                    ' もし「いいえ」の場合に以降の処理を中止したければ、以下の行のコメントを外してください。
                    RemoveDiscountRows(False)
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

        If keyNo > 0 Then
            Me.Button4.Enabled = False
        End If

    End Sub

    'JSONテスト
    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Dim jsonClass As New ImageJason
        jsonClass.WriteJson()
    End Sub

    '行削除
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        If DataGridView1.Rows.Count = 1 Then
            MessageBox.Show("最後の行は削除できません。")
            Exit Sub
        End If

        For Each r In DataGridView1.SelectedRows
            If Not r.IsNewRow Then
                DataGridView1.Rows.Remove(r)
            End If
        Next r

        allAmount = 0
        '現在トータルの更新
        For Each r As DataGridViewRow In DataGridView1.Rows
            'Debug.WriteLine(r.Index)
            Dim currentVal = NothingToEmpty(DataGridView1.Rows(r.Index).Cells(5).Value)

            If currentVal.Trim.ToString.Equals("") Then
                '値は 0 とする
            Else
                '20240812 =============================================
                'allAmount += CInt(currentVal)
                'allAmount += CType(currentVal, Double)
                allAmount += CType(currentVal, Decimal)
            End If
        Next r

        '20240812 =============================================
        'TextBox3.Text = String.Format("{0:#,0}", allAmount)
        TextBox3.Text = allAmount

        RowNumberShow()

    End Sub

    '見積書・請求書　履歴削除
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        If TextBox4.Text.Length = 0 Then

            Select Case execMode
                Case 0
                    MessageBox.Show("見積書が選択されていません。", "警告", Nothing, MessageBoxIcon.Warning)
                '2024/8/30 saruwatari Add Start ====================================================================
                Case 2
                    MessageBox.Show("見積書が選択されていません。", "警告", Nothing, MessageBoxIcon.Warning)
                '2024/8/30 saruwatari Add End ======================================================================
                Case 3
                    MessageBox.Show("請求書が選択されていません。", "警告", Nothing, MessageBoxIcon.Warning)
            End Select

        Else

            Dim message As String = ""
            Select Case execMode
                Case 0
                    message = "この見積書を削除します。" & vbCrLf & "本当によろしいですか？"
                '2024/8/30 saruwatari Add Start ==================================================================
                Case 2
                    message = "この見積書を削除します。" & vbCrLf & "本当によろしいですか？"
                '2024/8/30 saruwatari Add End ====================================================================
                Case 3
                    message = "この請求書を削除します。" & vbCrLf & "本当によろしいですか？"
            End Select

            Dim result As DialogResult = MessageBox.Show(message, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)

            If result = DialogResult.OK Then

                '2024/8/29 saruwatari Mod Start ===============================================
                'Using Conn As New MySqlConnection(Builder.ToString)
                Using Conn As New MySqlConnection(メインメニュー.Builder.ToString)
                    '2024/8/29 saruwatari Mod End =============================================
                    Dim transaction As MySqlTransaction = Nothing

                    Conn.Open()
                    transaction = Conn.BeginTransaction()

                    Dim sql As String = ""

                    Select Case execMode
                        Case 0
                            sql = "DELETE FROM quotation_master WHERE quoteNo = " & CInt(TextBox4.Text.Trim)
                        '2024/8/30 saruwatari Add Start =============================================================
                        Case 2
                            sql = "DELETE FROM quotation_master WHERE quoteNo = " & CInt(TextBox4.Text.Trim)
                        '2024/8/30 saruwatari Add End ===============================================================
                        Case 3
                            sql = "DELETE FROM invoice_master WHERE invoiceNo = " & CInt(TextBox4.Text.Trim)
                    End Select

                    Dim cmd As New MySqlCommand(sql, Conn, transaction)
                    Dim respons1 As Integer = cmd.ExecuteNonQuery

                    Select Case execMode
                        Case 0
                            cmd.CommandText = "DELETE FROM quotation_detail WHERE quoteNo = " & CInt(TextBox4.Text.Trim)
                        '2024/8/30 saruwatari Add Start ============================================================================
                        Case 2
                            cmd.CommandText = "DELETE FROM quotation_detail WHERE quoteNo = " & CInt(TextBox4.Text.Trim)
                        '2024/8/30 saruwatari Add End ==============================================================================
                        Case 3
                            cmd.CommandText = "DELETE FROM invoice_detail WHERE invoiceNo = " & CInt(TextBox4.Text.Trim)
                    End Select

                    Dim respons2 As Integer = cmd.ExecuteNonQuery

                    If respons1 = 1 Then
                        transaction.Commit()
                    Else
                        transaction.Rollback()
                    End If

                    Conn.Close()
                End Using
            Else
                CheckBox2.Checked = False
            End If
        End If

        Button2.PerformClick()

    End Sub

    '「様」ボタン
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Dim message As String = ""
        Dim result As DialogResult = Nothing

        If TextBox1.Text.Contains("様") Or TextBox1.Text.Contains("御中") Then
            result = MessageBox.Show("「様または御中」が入力されています。" + vbCrLf + "追加してもよろしいですか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            If result = DialogResult.OK Then
                TextBox1.Text = TextBox1.Text & "　様"
                Button8.Enabled = False
            Else
                Exit Sub
            End If
        Else
            TextBox1.Text = TextBox1.Text & "　様"
            Button8.Enabled = False
            Button11.Enabled = False
        End If

    End Sub

    '「御中」ボタン
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        Dim message As String = ""
        Dim result As DialogResult = Nothing

        If TextBox1.Text.Contains("御中") Or TextBox1.Text.Contains("様") Then
            result = MessageBox.Show("「御中または様」が入力されています。" + vbCrLf + "追加してもよろしいですか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            If result = DialogResult.OK Then
                TextBox1.Text = TextBox1.Text & "　御中"
                Button8.Enabled = False
            Else
                Exit Sub
            End If
        Else
            TextBox1.Text = TextBox1.Text & "　御中"
            Button8.Enabled = False
            Button11.Enabled = False
        End If

    End Sub

    Private Function NothingToEmpty(cellValue As Object) As String
        Try
            If cellValue Is Nothing OrElse cellValue.Equals("") Then
                Return ""
            Else
                Return cellValue.ToString
            End If
        Catch ex As Exception
            MessageBox.Show("NothingToEmpty:" & vbCrLf & ex.ToString)
        End Try
        Return Nothing
    End Function

    Private Function NothingToNull(cellValue As Object) As String
        Try
            If cellValue Is Nothing OrElse cellValue.Equals("") Then
                Return "null"
            Else
                Return cellValue.ToString
            End If
        Catch ex As Exception
            MessageBox.Show("NothingToNull:" & vbCrLf & ex.ToString)
        End Try
        Return Nothing
    End Function

    Private Function DBNullToEmpty(reader As MySqlDataReader, column As Integer) As String
        Try
            If reader.IsDBNull(column) Then
                Return ""
            Else
                Return reader.GetString(column)
            End If
        Catch ex As Exception
            MessageBox.Show("DBNullToEmpty:" & vbCrLf & ex.ToString)
        End Try
        Return Nothing
    End Function

    'dataType = 0：Integer
    'dataType = 1：Double
    Private Function DBNullToNothing(reader As MySqlDataReader, column As Integer, dataType As Integer) As String
        Try
            If reader.IsDBNull(column) Then
                Return Nothing
            Else
                If dataType = 0 Then
                    Return reader.GetInt32(column)
                ElseIf dataType = 1 Then
                    'Return reader.GetDouble(column)
                    Return reader.GetDecimal(column)
                Else
                    Return reader.GetInt32(column)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show("DBNullToNothing:" & vbCrLf & ex.ToString)
        End Try
        Return Nothing
    End Function

    Private Function SelectFolder()
        'ルートフォルダを指定する
        '最初に選択するフォルダを指定する（ RootFolder以下にあるフォルダである必要がある ）
        'fbd.SelectedPath = gstrApCfgs(genumApCfg.送り状データ格納場所)
        'ユーザーが新しいフォルダを作成できるようにする
        Dim fbd As New FolderBrowserDialog With {
            .Description = "フォルダを指定してください。",
            .RootFolder = Environment.SpecialFolder.Desktop,
            .SelectedPath = "",
            .ShowNewFolderButton = True
        }

        'ダイアログを表示する
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            '選択されたフォルダを表示する
            'Me.Label1.Text = fbd.SelectedPath
            outputPath = fbd.SelectedPath
            Return True
        Else
            'MessageBox.Show("出力先を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
    End Function

    Private Function StringSplitter(value As String) As String()

        Dim array As String() = value.Split("：")
        Return array

    End Function

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

        Dim column As Integer = e.ColumnIndex
        Dim row As Integer = e.RowIndex

        Dim unitAmount As String = NothingToEmpty(DataGridView1.Rows(row).Cells(2).Value)
        Dim quantity As String = NothingToEmpty(DataGridView1.Rows(row).Cells(4).Value)
        'Dim tempTotal As String = NothingToEmpty(DataGridView1.Rows(row).Cells(5).Value)

        'Dim account As Integer = 0
        'Dim account As Double = 0
        Dim account As Decimal

        If (column = 2 Or column = 4 Or column = 5) And IsNumeric(unitAmount) And IsNumeric(quantity) Then

            '現在の合計金額　再計算
            '==============================================================================================
            'account = CInt(quantity) * CInt(unitAmount)
            'account = CType(CType(quantity, Decimal) * CType(unitAmount, Decimal), Decimal)
            'account = CType(quantity, Double) * CType(unitAmount, Double)
            account = CType(quantity, Decimal) * CType(unitAmount, Decimal)


            'DataGridView1.Rows(row).Cells(5).Value = CInt(quantity) * CInt(unitAmount)
            DataGridView1.Rows(row).Cells(5).Value = account
            '==============================================================================================




            allAmount = 0
            For i As Integer = 0 To DataGridView1.RowCount - 1

                If account > 0 Then
                    'If tempTotal.Length > 0 Then
                    If IsNumeric(DataGridView1.Rows(i).Cells(5).Value) Then
                        '==============================================================================================
                        'allAmount += CInt(DataGridView1.Rows(i).Cells(5).Value)
                        'allAmount += CType(DataGridView1.Rows(i).Cells(5).Value, Double)
                        allAmount += CType(DataGridView1.Rows(i).Cells(5).Value, Decimal)
                        '==============================================================================================
                    End If
                    'allAmount += CInt(DataGridView1.Rows(i).Cells(5).Value)
                End If

            Next

            'TextBox3.Text = String.Format("{0:#,0}", allAmount)
            TextBox3.Text = allAmount

        End If
    End Sub

    'CellValidatingイベントハンドラ 
    '====================================================
    '=== 何かと面倒なので中身は無効化しています =========
    '====================================================
    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating

        'Dim dgv As DataGridView = DirectCast(sender, DataGridView)

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        'If e.RowIndex = dgv.NewRowIndex OrElse Not dgv.IsCurrentCellDirty Then
        'Debug.WriteLine(dgv.IsCurrentCellDirty)
        'Exit Sub
        'End If

        'If dgv.Rows(e.RowIndex).Cells(0).Value = 1 Then

        'If (dgv.Columns(e.ColumnIndex).Name = "Column2" Or
        '    dgv.Columns(e.ColumnIndex).Name = "Column4" Or
        '    dgv.Columns(e.ColumnIndex).Name = "Column5" Or
        '    dgv.Columns(e.ColumnIndex).Name = "Column7") _
        '    And Not IsNumeric(e.FormattedValue.ToString()) Then

        '    '列ヘッダに×を表示
        '    dgv.Rows(e.RowIndex).ErrorText = "数値を入力して下さい。"

        '    '入力した値をキャンセルして元に戻すには、次のようにする 
        '    dgv.CancelEdit()
        '    'キャンセルする 
        '    e.Cancel = True
        'End If
        'Else
        'If dgv.Columns(e.ColumnIndex).Name = "Column7" Then
        'And Not IsNumeric(e.FormattedValue.ToString()) Then

        'If Not IsNumeric(e.FormattedValue.ToString()) Then
        '    MessageBox.Show("数値を入れて", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    e.Cancel = True
        'End If

        ''列ヘッダに×を表示
        'dgv.Rows(e.RowIndex).ErrorText = "数値を入力して下さい。"

        '    '入力した値をキャンセルして元に戻すには、次のようにする 
        '    dgv.CancelEdit()
        '    'キャンセルする 
        '    e.Cancel = True
        'End If
        'End If

    End Sub

    'CellValidatedイベントハンドラ ※利用していない
    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated

        Dim dgv As DataGridView = DirectCast(sender, DataGridView)

        If e.ColumnIndex = 5 Then
            allAmount = 0
            For i As Integer = 0 To DataGridView1.RowCount - 1
                If IsNumeric(DataGridView1.Rows(i).Cells(5).Value) Then
                    '20240812 =============================================
                    'allAmount += CInt(DataGridView1.Rows(i).Cells(5).Value)
                    'allAmount += CType(DataGridView1.Rows(i).Cells(5).Value, Double)
                    allAmount += CType(DataGridView1.Rows(i).Cells(5).Value, Decimal)
                End If
            Next

            '20240812 =============================================
            'TextBox3.Text = String.Format("{0:#,0}", allAmount)
            TextBox3.Text = allAmount

        ElseIf e.ColumnIndex = 0 Then
            For i As Integer = 0 To DataGridView1.RowCount - 1
                If NothingToEmpty(DataGridView1.Rows(i).Cells(0).Value).Equals("1") Then
                    DataGridView1.Rows(i).Cells(0).ReadOnly = True
                End If
            Next
        End If

        'エラーテキストを消す
        dgv.Rows(e.RowIndex).ErrorText = Nothing
    End Sub

    '入力値のチェック
    Private Function DataGridCheck() As Boolean

        Dim dataRowCount As Integer = DataGridView1.RowCount
        Dim response As Boolean = True
        'Dim isNG As Boolean
        'Dim currentNotMoved As Boolean = False

        For i As Integer = 0 To dataRowCount - 1

            If DataGridView1.Rows(i).Cells(0).Value = 0 Then
                '0：空行
                If DataGridView1.Rows(i).Cells(0).Value Is Nothing Then
                    DataGridView1(0, i).Style.BackColor = sd.Color.MistyRose
                    response = False
                Else
                    DataGridView1(0, i).Style.BackColor = sd.Color.White
                End If

                DataGridView1(0, i).Style.BackColor = sd.Color.LightSteelBlue

            ElseIf DataGridView1.Rows(i).Cells(0).Value = 1 Then
                '1：明細行
                Dim val As String = NothingToEmpty(DataGridView1.Rows(i).Cells(1).Value)

                'カラム１
                If val.Equals("") Then
                    DataGridView1(1, i).Style.BackColor = sd.Color.MistyRose
                    MessageBox.Show("「工事名」は空欄にできません")
                    'If currentNotMoved = False Then
                    '    'DataGridView1.CurrentCell = DataGridView1(0, DataGridView1.CurrentRow.Index)
                    '    DataGridView1(1, i).Style.BackColor = Color.MistyRose
                    '    currentNotMoved = True
                    'End If
                    response = False
                Else
                    DataGridView1(1, i).Style.BackColor = sd.Color.White
                End If

                'カラム２
                val = NothingToEmpty(DataGridView1.Rows(i).Cells(2).Value)
                If val.Equals("") Then
                    DataGridView1(2, i).Style.BackColor = sd.Color.MistyRose
                    MessageBox.Show("「数量」は空欄にできません")
                    'If currentNotMoved = False Then
                    '    'DataGridView1.CurrentCell = DataGridView1(0, DataGridView1.CurrentRow.Index)
                    '    DataGridView1(2, i).Style.BackColor = Color.MistyRose
                    '    currentNotMoved = True
                    'End If
                    response = False
                ElseIf IsNumeric(val) = True And (CType(val, Decimal) * 100 Mod 10) > 0 Then
                    DataGridView1(2, i).Style.BackColor = sd.Color.MistyRose
                    MessageBox.Show("「数量」の少数桁は１桁で入力して下さい　例：5.3 など")
                    'If currentNotMoved = False Then
                    '    'DataGridView1.CurrentCell = DataGridView1(0, DataGridView1.CurrentRow.Index)
                    '    DataGridView1(2, i).Style.BackColor = Color.MistyRose
                    '    currentNotMoved = True
                    'End If
                    response = False
                ElseIf IsNumeric(val) = False Then
                    DataGridView1(2, i).Style.BackColor = sd.Color.MistyRose
                    MessageBox.Show("「数量」に数値以外の入力はできません")
                    'If currentNotMoved = False Then
                    '    'DataGridView1.CurrentCell = DataGridView1(0, DataGridView1.CurrentRow.Index)
                    '    DataGridView1(2, i).Style.BackColor = Color.MistyRose
                    '    currentNotMoved = True
                    'End If
                    response = False
                Else
                    DataGridView1(2, i).Style.BackColor = sd.Color.White
                End If

                'カラム４
                val = NothingToEmpty(DataGridView1.Rows(i).Cells(4).Value)
                If val.Equals("") Then
                    DataGridView1(4, i).Style.BackColor = sd.Color.MistyRose
                    MessageBox.Show("「単価」は空欄にできません")
                    'If currentNotMoved = False Then
                    '    '
                    '    DataGridView1(4, i).Style.BackColor = Color.MistyRose
                    '    currentNotMoved = True
                    'End If
                    response = False
                ElseIf IsNumeric(val) = True AndAlso (CType(val, Decimal) Mod 10) > 0 Then
                    MessageBox.Show("「単価」は10円単位で入力して下さい")
                    DataGridView1(4, i).Style.BackColor = sd.Color.MistyRose
                    'If currentNotMoved = False Then
                    '    'DataGridView1.CurrentCell = DataGridView1(0, DataGridView1.CurrentRow.Index)
                    '    DataGridView1(4, i).Style.BackColor = Color.MistyRose
                    '    currentNotMoved = True
                    'End If
                    response = False
                ElseIf IsNumeric(val) = False Then
                    DataGridView1(4, i).Style.BackColor = sd.Color.MistyRose
                    MessageBox.Show("「単価」に数値以外の入力はできません")
                    'If currentNotMoved = False Then
                    '    'DataGridView1.CurrentCell = DataGridView1(0, DataGridView1.CurrentRow.Index)
                    '    DataGridView1(4, i).Style.BackColor = Color.MistyRose
                    '    currentNotMoved = True
                    'End If
                    response = False
                Else
                    DataGridView1(4, i).Style.BackColor = sd.Color.White
                End If

                'カラム５ 金額列
                'val = NothingToEmpty(DataGridView1.Rows(i).Cells(5).Value)
                'If val.Equals("") Then
                '    DataGridView1(5, i).Style.BackColor = Color.MistyRose
                '    response = False
                'Else
                '    DataGridView1(5, i).Style.BackColor = Color.White
                '    If IsNumeric(DataGridView1.Rows(i).Cells(5).Value) Then
                '        allAmount += CInt(DataGridView1.Rows(i).Cells(5).Value)
                '    End If

                'End If

                'If DataGridView1.Rows(i).Cells(0).Value Is Nothing Then
                '    DataGridView1(0, i).Style.BackColor = Color.MistyRose
                '    response = False
                'Else
                '    DataGridView1(0, i).Style.BackColor = Color.Yellow
                'End If

                DataGridView1(0, i).Style.BackColor = sd.Color.LightSteelBlue

            Else
                DataGridView1(0, i).Style.BackColor = sd.Color.LightSteelBlue
                'Debug.WriteLine(DataGridView1.Rows(i).Cells(0).Value)
                '2：数値項目入力不可
                'If DataGridView1.Rows(i).Cells(1).Value Is Nothing Then
                '    DataGridView1(1, i).Style.BackColor = Color.MistyRose
                '    response = False
                'Else
                '    DataGridView1(1, i).Style.BackColor = Color.White
                'End If
            End If

        Next

        Call RowNumberShow()
        Return response
    End Function

    Private Function MakeDigitSeparator(NumberDataSourceType As Integer) As String
        Return ""
    End Function


    'Private Sub Form_Click()
    '    Dim xlApp As Object = New Object
    '    Dim xlBook As Object = New Object

    '    xlApp = CreateObject("Excel.Application")
    '    xlBook = xlApp.Workbooks.Open("C:\templateBook1000.xlsx")
    '    xlApp.Application.Visible = True
    '    xlBook.ActiveSheet.PrintPreview
    '    xlApp.Application.Visible = False
    '    xlApp.Quit
    '    xlBook = Nothing
    '    xlApp = Nothing
    'End Sub

    '====================================================
    '=== 何かと面倒なので中身は無効化しています =========
    '====================================================
    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        'Debug.WriteLine(e.ColumnIndex & " : " & e.RowIndex)
        '金額列をReadOnlyにする
        'If DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString.Equals("1") Then
        '    'Debug.WriteLine(e.ColumnIndex & " : " & e.RowIndex)
        '    DataGridView1(5, e.RowIndex).Style.BackColor = Color.Honeydew
        'Else
        '    DataGridView1(5, e.RowIndex).Style.BackColor = Color.AliceBlue
        'End If
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As DataGridViewEditingControlShowingEventArgs) _
        Handles DataGridView1.EditingControlShowing

        '表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
        If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
            Dim dgv As DataGridView = CType(sender, DataGridView)

            '編集のために表示されているコントロールを取得
            Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)
            '次のようにしてもよい
            'Dim tb As TextBox = CType(e.Control, TextBox)

            '列によってIMEのモードを変更する
            If dgv.CurrentCell.OwningColumn.Name = "Column2" _
                Or dgv.CurrentCell.OwningColumn.Name = "Column4" Then
                'Or dgv.CurrentCell.OwningColumn.Name = "Column5" Then
                tb.ImeMode = System.Windows.Forms.ImeMode.Disable
            Else
                'tb.ImeMode = dgv.ImeMode
                tb.ImeMode = System.Windows.Forms.ImeMode.On
            End If
        End If
    End Sub

    ''' <summary>
    ''' 非同期呼出し
    ''' </summary>
    Private Async Sub DoCreatePdf()
        Dim task As Task = Task.Run(
            Sub()
                Call SaveData_CreatePdf(1)
            End Sub
        )
        Await task
        Panel2.Visible = False
        Button2.PerformClick()
    End Sub

    '日付指定チェックボックス
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            DateTimePicker1.Enabled = True
            CheckBox2.Enabled = False
        Else
            DateTimePicker1.Enabled = False
            CheckBox2.Enabled = True
        End If
    End Sub

    '見積書・請求書 削除チェックボックス
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.Checked Then
            Button1.Enabled = False
            Button3.Enabled = False
            Button6.Enabled = False
            Button7.Visible = True
            Label1.Enabled = False
            Label3.BackColor = ColorTranslator.FromHtml("0xFF8000")
            Label3.Text = "見積書削除"

            If execMode = 0 Then
                If Not NoInputCheck() Or TextBox1.Text.Length > 0 Then
                    Dim result As DialogResult = MessageBox.Show("入力内容が失われますが、よろしいですか？" _
                        + vbCrLf + "よければ削除する見積書を選択し、" + vbCrLf + "「削除実行」ボタンを押して下さい。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    If result = DialogResult.Cancel Then
                        Label1.Enabled = True
                        Label3.BackColor = swf.Control.DefaultBackColor
                        Label3.Text = "見積書作成"
                        Button1.Enabled = True
                        Button3.Enabled = True
                        Button6.Enabled = True
                        Button7.Visible = False

                        fromQuotationDelete = True
                        CheckBox2.Checked = False

                        Exit Sub
                    Else
                        For i = 0 To DataGridView1.Rows.Count - 1
                            For j = 0 To 6
                                DataGridView1.Rows(i).Cells(j).Value = ""
                            Next j
                        Next i
                        Button7.Visible = True
                        'fromQuotationDelete = False
                    End If
                End If

                TextBox4.Text = ""
                Button4.Text = "見積書を選択"
                Button4.Enabled = True

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                CheckBox3.Checked = False
                allAmount = 0
                Button1.Text = "PDF出力"
                Button8.Enabled = False

                Dim rows As Integer = DataGridView1.Rows.Count
                For i = 0 To rows - 1
                    DataGridView1.Rows.RemoveAt(0)
                Next i

                '2024/9/2 saruwatari Add Start ====================
            ElseIf execMode = 2 Then
                '見積書削除
                Label3.Text = "見積書削除"
                '2024/9/2 saruwatari Add End ======================

            ElseIf execMode = 3 Then
                '請求書削除
                Label3.Text = "請求書削除"
            End If

            CheckBox1.Enabled = False
            CheckBox1.Checked = False
            DateTimePicker1.Enabled = False
            DataGridView1.Enabled = False
            TextBox1.Enabled = False
            TextBox2.Enabled = False
        Else
            If Not fromQuotationDelete Then

                Button1.Enabled = True
                Button3.Enabled = True
                Button6.Enabled = True
                Button7.Visible = False
                Button8.Enabled = True
                Button11.Enabled = True
                Label1.Enabled = True

                If execMode = 0 Then
                    Label3.BackColor = swf.Control.DefaultBackColor
                    Label3.Text = "見積書作成"
                    Button4.Text = "見積履歴から作成"
                    TextBox4.Text = Me.newQuoteNo

                    'Button2.PerformClick()'2024/9/6 saruwatari Del

                    '2024/9/2 saruwatari Add Start =======================================
                ElseIf execMode = 2 Then
                    Label3.BackColor = ColorTranslator.FromHtml("0xBDB76B")
                    Label3.Text = "見積書修正"
                    'Button2.PerformClick()'2024/9/6 saruwatari Del
                    '2024/9/2 saruwatari Add End =========================================

                ElseIf execMode = 3 Then
                    Label3.BackColor = ColorTranslator.FromHtml("0xBDB76B")
                    Label3.Text = "請求書修正"
                    'Button2.PerformClick()'2024/9/6 saruwatari Del
                End If

                CheckBox1.Enabled = True
                DataGridView1.Enabled = True
                TextBox1.Enabled = True
                TextBox2.Enabled = True

                fromQuotationDelete = False
                Button2.PerformClick() '2024/9/6 saruwatari Add

            End If
        End If
    End Sub

    '印刷だけするチェックボックス
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            Button1.Text = "保存"
        Else
            Button1.Text = "PDF出力"
        End If
    End Sub

    'データグリッドの左端の通し番号を再設定する
    Sub RowNumberShow()
        'DataGridView1の行ヘッダーに行番号を表示する
        Dim k As Integer
        For k = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(k).HeaderCell.Value = (k + 1).ToString()
            DataGridView1.Rows(k).Cells(0).Style.BackColor = sd.Color.LightSteelBlue
        Next k
    End Sub

    'データグリッド再計算
    Function Accounting() As Decimal
        Dim totalAmount As Decimal
        Dim i As Integer
        For i = 0 To DataGridView1.Rows.Count - 1
            If IsNumeric(DataGridView1.Rows(i).Cells(5).Value) Then
                totalAmount += Decimal.Parse(DataGridView1.Rows(i).Cells(5).Value)
            End If
        Next i
        Return totalAmount
    End Function

    Function NoInputCheck() As Boolean

        Dim isEmpty As Boolean = True

        For j As Integer = 0 To DataGridView1.Rows.Count - 1

            For i As Integer = 1 To 6
                If NothingToEmpty(DataGridView1.Rows(j).Cells(i).Value).Length > 0 Then
                    isEmpty = False
                End If
            Next

        Next

        '入力値がない場合は処理を中断する
        If isEmpty Then
            'MessageBox.Show("明細に入力がありません。")
            Return True
        End If

        Return False

    End Function


    Private Sub Button9_Click(sender As Object, e As EventArgs)

        ' 一時的に jar 呼び出しを無効化（安定化のため）
        MessageBox.Show("jar 呼び出しは無効化されています。ClosedXML + Interop のフローを使用します。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return
    End Sub

    '税・値引き調整
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        ' Button10_Click の先頭付近に入れるガード（既存 HasDiscountRows を利用）
        If HasDiscountRows() Then
            ' 既存行を更新したいなら EnsureSingleAdjustmentRow を呼ぶ（例: 再計算して更新）
            ' Dim discount = InvoiceTaxAdjuster.CalculateDiscount(Accounting(), roundUnit)
            ' EnsureSingleAdjustmentRow(discount, "※値引き")
            MessageBox.Show("既に値引き行があります。既存行を更新してください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim roundUnit As Decimal
            If Not メインメニュー.ht Is Nothing AndAlso メインメニュー.ht.ContainsKey("roundUnit") Then
                roundUnit = Convert.ToString(メインメニュー.ht.Item("roundUnit"))
            End If

            Dim discount As Decimal = InvoiceTaxAdjuster.CalculateDiscount(allAmount, roundUnit)

            If discount < 0D Then

                Dim lastRow = DataGridView1.Rows.Count
                DataGridView1.Rows.Add()
                DataGridView1(0, lastRow).Value = 1
                DataGridView1(1, lastRow).Value = "※値引き"
                DataGridView1(1, lastRow).ReadOnly = True
                DataGridView1(1, lastRow).Selected = False
                DataGridView1(2, lastRow).Value = CInt(1)
                DataGridView1(2, lastRow).ReadOnly = True
                DataGridView1(5, lastRow).Style.BackColor = Color.AliceBlue
                DataGridView1(5, lastRow).ReadOnly = True
                DataGridView1(6, lastRow).Value = "調整"
                DataGridView1(6, lastRow).ReadOnly = True

                '税調整値を設定
                DataGridView1(4, lastRow).Value = discount
                DataGridView1(5, lastRow).Value = discount

                '右のセル（列インデックス6）に移動して編集開始（存在チェック）
                If DataGridView1.Columns.Count > 6 Then
                    DataGridView1.CurrentCell = DataGridView1(6, lastRow)
                    DataGridView1.BeginEdit(True)
                Else
                    '右列が無ければ同一行の先頭へ（用途に応じて調整）
                    DataGridView1.CurrentCell = DataGridView1(0, lastRow)
                    DataGridView1.BeginEdit(True)
                End If

                '合計を再計算して表示を更新
                allAmount = Accounting()
                TextBox3.Text = allAmount

            Else
                MessageBox.Show("値引き額は発生しません。")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    ' 値引き行が存在するか判定するユーティリティ
    Private Function HasDiscountRows() As Boolean
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).IsNewRow Then Continue For
            Dim nameCol As String = NothingToEmpty(DataGridView1(1, i).Value)
            Dim remarkCol As String = NothingToEmpty(DataGridView1(6, i).Value)
            If remarkCol = "調整" Then
                Return True
            End If
        Next
        Return False
    End Function

    ' 既存の値引き／調整行（識別子: 品名列に "※値引き" または 備考列に "調整"）をすべて削除する。
    ' 戻り値: 削除が発生したか
    Private Function RemoveDiscountRows(isRemove As Boolean) As Boolean
        Dim removed As Boolean = False

        ' 後ろから走査して安全に削除
        For i As Integer = DataGridView1.Rows.Count - 1 To 0 Step -1
            If DataGridView1.Rows(i).IsNewRow Then Continue For
            Dim nameCol As String = NothingToEmpty(DataGridView1(1, i).Value)
            Dim remarkCol As String = NothingToEmpty(DataGridView1(6, i).Value)
            If remarkCol = "調整" Then

                If isRemove Then
                    DataGridView1.Rows.RemoveAt(i)
                    removed = True
                Else
                    DataGridView1(2, i).ReadOnly = True
                    DataGridView1(6, i).ReadOnly = True
                End If

            End If
        Next

        If removed Then
            allAmount = Accounting()
            TextBox3.Text = allAmount.ToString()
            RowNumberShow()
        End If

        Return removed
    End Function

End Class
