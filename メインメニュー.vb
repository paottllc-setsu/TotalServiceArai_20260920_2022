Imports System.IO
Imports System.Text
Imports DocumentFormat.OpenXml.Drawing
Imports DocumentFormat.OpenXml.Office2016.Drawing.Command
Imports DocumentFormat.OpenXml.Spreadsheet
Imports MySqlConnector
Imports Microsoft.Data.SqlClient

Public Class メインメニュー

    Public Shared Builder As MySqlConnectionStringBuilder '2024/8/29 猿渡 Add
    'Public Shared Builder As SqlConnectionStringBuilder
    Public Shared SqlServerConnStr As String
    Public Shared ht As Hashtable


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fm1 As New 見積請求作成修正(0)
        fm1.Left = 50
        fm1.Top = 50
        'fm1.StartPosition = FormStartPosition.Manual
        fm1.StartPosition = FormStartPosition.CenterScreen
        fm1.ShowDialog(Me)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fm1 As New 見積請求作成修正(1)
        fm1.Left = 50
        fm1.Top = 50
        fm1.StartPosition = FormStartPosition.CenterScreen
        fm1.ShowDialog(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim fm1 As New 顧客マスタ一覧
        fm1.Left = 50
        fm1.Top = 50
        fm1.StartPosition = FormStartPosition.CenterScreen
        fm1.ShowDialog(Me)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim fm1 As New 工事マスタ一覧
        fm1.Left = 50
        fm1.Top = 50
        fm1.StartPosition = FormStartPosition.CenterScreen
        fm1.ShowDialog(Me)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim fm1 As New 年間請求一覧
        fm1.StartPosition = FormStartPosition.CenterScreen
        fm1.ShowDialog(Me)
    End Sub

    Private Sub メインメニュー_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = 0
        Me.Top = 0

        '2024/8/29 saruwatari Add Start ========================================================
        'Dim ht As New Hashtable
        ht = New Hashtable
        Dim sr As New StreamReader("C:\template\system.ini", System.Text.Encoding.UTF8)

        While sr.Peek() > -1
            Dim line As String = sr.ReadLine()

            '2024/9/3 setsu Add Start ================
            If "'".Equals(line.Substring(0, 1)) Then
                Continue While
            End If
            '2024/9/3 setsu Add End ==================

            Dim key As String = line.Split("=")(0).Trim
            Dim value As String = line.Split("=")(1).Trim

            If Not ht.ContainsKey(key) Then
                ht(key) = value
            End If
        End While

        Me.Label1.Text = ht.Item("Title")

        Builder = New MySqlConnectionStringBuilder With {
        .Server = ht.Item("Server"),
        .Port = ht.Item("Port"),
        .UserID = ht.Item("UserID"),
        .Password = ht.Item("Password"),
        .Database = ht.Item("Database"),
        .CharacterSet = ht.Item("CharacterSet")
        }
        '2024/8/29 saruwatari Add End =========================================================


        '--- （参考）SQL Server認証（UserID / Password）を使う場合 ---
        'Builder = New SqlConnectionStringBuilder With {
        '     .DataSource = "PAOTT_NOTE_2",
        '     .InitialCatalog = "total_service_arai",
        '     .IntegratedSecurity = False,
        '     .UserID = ht.Item("sa"),
        '     .Password = ht.Item("admin"),
        '     .TrustServerCertificate =
        ' }

        '--- SQL Server接続設定 ---
        'Builder = New SqlConnectionStringBuilder()
        ' 直接接続文字列を渡すことで、Encrypt等の型不整合を完全に回避します
        'Builder.ConnectionString = "Data Source=PAOTT_NOTE_2;Initial Catalog=total_service_arai;User ID=sa;Password=admin;TrustServerCertificate=True;Encrypt=False;"
        SqlServerConnStr = "Data Source=PAOTT_NOTE_2;Initial Catalog=total_service_arai;User ID=sa;Password=admin;TrustServerCertificate=True;Encrypt=False;"

        Debug.Print("---------------> " + SqlServerConnStr)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Common.SQLiteAccess()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim fm1 As New 見積請求作成修正(3)
        fm1.Left = 50
        fm1.Top = 50
        fm1.StartPosition = FormStartPosition.CenterScreen
        fm1.ShowDialog(Me)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'Dim fm1 As New 見積請求作成修正test(2)
        Dim fm1 As New 見積請求作成修正(2)
        fm1.Left = 50
        fm1.Top = 50
        fm1.StartPosition = FormStartPosition.CenterScreen
        fm1.ShowDialog(Me)
    End Sub

    'データベースのバックアップを実施
    Private Sub DataBackUp()

        ' ProcessStartInfoオブジェクトを作成
        Dim psi As New ProcessStartInfo()
        psi.FileName = "C:\template\backup.bat"
        'psi.Verb = "runas" '管理者権限の確認
        psi.UseShellExecute = True
        psi.Arguments = ht.Item("BackUpDir")
        'psi.RedirectStandardOutput = True
        'psi.RedirectStandardError = True

        ' Processオブジェクトを作成し、Startメソッドを呼び出す
        Dim process As New Process()
        process.StartInfo = psi

        ' プロセスの出力を取得する
        'AddHandler process.OutputDataReceived, AddressOf OutputHandler
        'AddHandler process.ErrorDataReceived, AddressOf ErrorHandler

        process.Start()

    End Sub

    '「フォームを閉じる」直前のハンドラ
    Private Sub メインメニュー_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        '2025/2/21 setsu Add Start ================
        Me.DataBackUp()
        '2025/2/21 setsu Add  End  ================
        Debug.Print("<<<< バックアップ実施 >>>>")

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)
        Debug.Print("--------------------------")
        Debug.Print(exec)
    End Sub

End Class