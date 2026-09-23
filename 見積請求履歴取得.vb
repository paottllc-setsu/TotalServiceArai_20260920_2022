Imports System.Diagnostics.Eventing
Imports MySqlConnector

Public Class 見積請求履歴取得

    Sub New(mode As Integer)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        execMode = mode
    End Sub

    Dim NoLocal As Integer
    Dim NameLocal As String
    Dim DateQuote As String '2024/9/2 猿渡 Add
    Dim DateInvoice As String
    Dim DateLocal As String
    Private flagLocal As Integer
    Private execMode As Integer

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '2024/9/6 saruwatari Add Start =========================================
        Dim dt As DateTime = DateTime.Today
        Dim str_dt As String
        str_dt = dt.ToString("yyyy")
        '2024/9/6 saruwatari Add End ===========================================

        ' リストボックス1に項目を追加する
        Dim sql As String = ""
        '2024/9/6 saruwatari Mod Start ======================================================================================
        'Select Case execMode
        '    Case 0
        '        sql = "SELECT * FROM quotation_master ORDER BY quoteNo DESC"
        '    Case 3
        '        Me.Text = "請求履歴一覧"
        '        sql = "SELECT * FROM invoice_master ORDER BY invoiceNo DESC"
        'End Select
        Select Case execMode
            Case 0
                sql = "SELECT * FROM quotation_master WHERE SUBSTR(quoteDate, 1, 4) = " + str_dt + " ORDER BY quoteDate DESC"
            Case 1
                sql = "SELECT * FROM quotation_master WHERE SUBSTR(quoteDate, 1, 4) = " + str_dt + " ORDER BY quoteDate DESC"
            Case 2
                Me.Text = "見積履歴一覧"
                sql = "SELECT * FROM quotation_master WHERE SUBSTR(quoteDate, 1, 4) = " + str_dt + " ORDER BY quoteDate DESC"
            Case 3
                Me.Text = "請求履歴一覧"
                sql = "SELECT * FROM invoice_master WHERE SUBSTR(invoiceDate, 1, 4) = " + str_dt + " ORDER BY invoiceNo DESC"
        End Select
        '2024/9/6 saruwatari Mod End ========================================================================================
        Try
            '2024/8/29 saruwatari Mod Start =================================================
            'Using conn As New MySqlConnection(Builder.ToString)
            Using conn As New MySqlConnection(メインメニュー.Builder.ToString)
                '2024/8/29 saruwatari Mod End ===============================================

                conn.Open()
                Using cmd As New MySqlCommand(sql, conn)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        While (reader.Read())

                            If reader.GetInt16(0) = 0 Then
                                NoLocal = Nothing
                            Else
                                NoLocal = reader.GetInt16(0)
                            End If

                            If reader.GetString(1) Is Nothing Then
                                NameLocal = ""
                            Else
                                NameLocal = reader.GetString(1)
                            End If

                            If reader.GetString(4) Is Nothing Then
                                DateInvoice = "9999-99-99"
                            Else
                                DateInvoice = reader.GetString(4)
                            End If

                            '2024/9/2 saruwatari Add Start =====================================
                            If reader.GetString(7) Is Nothing Then
                                DateQuote = "9999-99-99"
                            Else
                                DateQuote = reader.GetString(7)
                            End If
                            '2024/9/2 saruwatari Add End =======================================

                            Select Case reader.GetDateTime(5)
                                Case Nothing
                                    DateLocal = ""
                                Case Else
                                    DateLocal = reader.GetDateTime(6)
                            End Select

                            '2024/9/2 saruwatari Mod Start ========================================================================================================
                            'Me.ListBox1.Items.Add(CStr(NoLocal).PadLeft(5, "0"c) & " : " & DateInvoice & " : " & NameLocal & " : " & DateLocal)
                            Me.ListBox1.Items.Add(CStr(NoLocal).PadLeft(5, "0"c) & " : " & DateInvoice & " : " & NameLocal & " : " & DateLocal & " : " & DateQuote)
                            '2024/9/2 saruwatari Mod End ==========================================================================================================
                        End While

                    End Using
                End Using

                '2024/9/6 saruwatari Add Start =============================================================================================
                ' リストボックス2に項目を追加する
                Select Case execMode
                    Case 0
                        sql = "SELECT SUBSTR(quoteDate, 1, 4) FROM quotation_master GROUP BY SUBSTR(quoteDate, 1, 4) DESC"
                    Case 1
                        sql = "SELECT SUBSTR(quoteDate, 1, 4) FROM quotation_master GROUP BY SUBSTR(quoteDate, 1, 4) DESC"
                    Case 2
                        Me.Text = "見積履歴一覧"
                        sql = "SELECT SUBSTR(quoteDate, 1, 4) FROM quotation_master GROUP BY SUBSTR(quoteDate, 1, 4) DESC"
                    Case 3
                        Me.Text = "請求履歴一覧"
                        sql = "SELECT SUBSTR(invoiceDate, 1, 4) FROM invoice_master GROUP BY SUBSTR(invoiceDate, 1, 4) DESC"
                End Select
                '2024/9/6 saruwatari Add End ===============================================================================================

                Using cmd As New MySqlCommand(sql, conn)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        While (reader.Read())
                            '2024/9/6 saruwatari Mod Start =========================================================================
                            'Select Case reader.GetInt16(0)
                            '    Case Nothing
                            '        DateLocal = ""
                            '    Case Else
                            '        DateLocal = reader.GetInt16(0)
                            'End Select
                            'Me.ListBox2.Items.Add(DateLocal)
                            If execMode = 0 Or execMode = 1 Or execMode = 2 Then

                                Select Case reader.GetString(0)
                                    Case Nothing
                                        DateQuote = ""
                                    Case Else
                                        DateQuote = reader.GetString(0)
                                End Select
                                Me.ListBox2.Items.Add(DateQuote)

                            ElseIf execMode = 3 Then

                                Select Case reader.GetString(0)
                                    Case Nothing
                                        DateInvoice = ""
                                    Case Else
                                        DateInvoice = reader.GetString(0)

                                End Select
                                Me.ListBox2.Items.Add(DateInvoice)
                            End If
                            '2024/9/6 saruwatari Mod End ===========================================================================
                        End While
                    End Using
                End Using
            End Using
            ListBox1.BorderStyle = BorderStyle.Fixed3D
            ListBox1.SelectedIndex = 0
            ListBox2.SelectedIndex = 0

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Me.ListBox1.SelectedItem Is Nothing Then
            MessageBox.Show("候補選択されていません。")
            Exit Sub
        Else
            NameLocal = Me.ListBox1.SelectedItem
            Me.Close()
        End If

    End Sub

    Public ReadOnly Property ClientName() As String
        Get
            Return NameLocal
        End Get
        'Set(ByVal Value As String)
        '    clientLocal = Value
        'End Set
    End Property

    Public Function GetKeyNo() As Integer
        If Me.ListBox1.SelectedItem IsNot Nothing Then
            '2024/9/2 saruwatari Mod Start ========================
            'If NameLocal.Split(" : ").Length = 4 Then
            If NameLocal.Split(" : ").Length = 5 Then
                '2024/9/2 saruwatari Mod End ======================
                Return NameLocal.Split(" : ")(0)
            Else
                '基本、あってはいけない
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function GetDateStr() As String
        If Me.ListBox1.SelectedItem IsNot Nothing Then
            '2024/9/2 saruwatari Mod Start ========================
            'If NameLocal.Split(" : ").Count = 4 Then
            If NameLocal.Split(" : ").Length = 5 Then
                '2024/9/2 saruwatari Mod End ======================
                Return NameLocal.Split(" : ")(3)
            Else
                '基本、あってはいけない
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    '2024/9/2 saruwatari Add Start ======================================
    '見積書作成日を取得
    Public Function GetDateQuoteStr() As String
        If Me.ListBox1.SelectedItem IsNot Nothing Then
            '2024/9/2 saruwatari Mod Start ========================
            'If NameLocal.Split(" : ").Count = 4 Then
            If NameLocal.Split(" : ").Length = 5 Then
                '2024/9/2 saruwatari Mod End ======================
                Return NameLocal.Split(" : ")(4)
            Else
                '基本、あってはいけない
                Return -1
            End If
        Else
            Return -1
        End If
    End Function
    '2024/9/2 saruwatari Add End ========================================

    '請求書作成日を取得   
    Public Function GetDateInvoiceStr() As String
        If Me.ListBox1.SelectedItem IsNot Nothing Then
            '2024/9/2 saruwatari Mod Start ========================
            'If NameLocal.Split(" : ").Length = 4 Then
            If NameLocal.Split(" : ").Length = 5 Then
                '2024/9/2 saruwatari Mod End ======================
                Return NameLocal.Split(" : ")(1)
            Else
                '基本、あってはいけない
                Return -1
            End If
        Else
            Return -1
        End If
    End Function


    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Dispose()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
    End Sub

    Private Sub ListBox2_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox2.MouseClick

        ' リストボックス1の項目を指定の年だけ表示する
        If Me.ListBox2.SelectedItem Is Nothing Then
            MessageBox.Show("候補選択されていません。")
            Exit Sub
            '2024/9/6 saruwatari Mod Start ==========================================
            'Else
            'NameLocal = Me.ListBox1.SelectedItem
            'Me.Close()
        ElseIf execMode = 0 Or execMode = 1 Or execMode = 2 Then
            DateQuote = Me.ListBox2.SelectedItem
        ElseIf execMode = 3 Then
            DateInvoice = Me.ListBox2.SelectedItem
            '2024/9/6 saruwatari Mod End ============================================
        End If

        Dim sql As String = ""
        Select Case execMode
            '2024/8/30 saruwatari Add Start ==========================================================================================
            Case 0
                sql = "SELECT * FROM quotation_master WHERE SUBSTR(quoteDate, 1, 4) = " + DateQuote + " ORDER BY quoteDate DESC"
            Case 1
                sql = "SELECT * FROM quotation_master WHERE SUBSTR(quoteDate, 1, 4) = " + DateQuote + " ORDER BY quoteDate DESC"
            Case 2
                Me.Text = "見積履歴一覧"
                sql = "SELECT * FROM quotation_master WHERE SUBSTR(quoteDate, 1, 4) = " + DateQuote + " ORDER BY quoteDate DESC"
            Case 3
                Me.Text = "請求履歴一覧"
                sql = "SELECT * FROM invoice_master WHERE SUBSTR(invoiceDate, 1, 4) = " + DateInvoice + " ORDER BY invoiceDate DESC"
                '2024/8/30 saruwatari Add End ============================================================================================
        End Select

        ListBox1.Items.Clear()

        Try
            '2024/8/29 saruwatari Mod Start =================================================
            'Using conn As New MySqlConnection(Builder.ToString)
            Using conn As New MySqlConnection(メインメニュー.Builder.ToString)
                '2024/8/29 saruwatari Mod End ===============================================
                conn.Open()
                Using cmd As New MySqlCommand(sql, conn)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        While (reader.Read())

                            If reader.GetInt16(0) = 0 Then
                                NoLocal = Nothing
                            Else
                                NoLocal = reader.GetInt16(0)
                            End If

                            If reader.GetString(1) Is Nothing Then
                                NameLocal = ""
                            Else
                                NameLocal = reader.GetString(1)
                            End If

                            If reader.GetString(4) Is Nothing Then
                                DateInvoice = "9999-99-99"
                            Else
                                DateInvoice = reader.GetString(4)
                            End If

                            '2024/9/2 saruwatari Add Start ============================
                            If reader.GetString(7) Is Nothing Then
                                DateQuote = "9999-99-99"
                            Else
                                DateQuote = reader.GetString(7)
                            End If
                            '2024/9/2 saruwatari Add End ==============================

                            Select Case reader.GetDateTime(5)
                                Case Nothing
                                    DateLocal = ""
                                Case Else
                                    DateLocal = reader.GetDateTime(6)
                            End Select

                            Me.ListBox1.Items.Add(CStr(NoLocal).PadLeft(5, "0"c) & " : " & DateInvoice & " : " & NameLocal & " : " & DateLocal & " : " & DateQuote)

                        End While
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub

End Class