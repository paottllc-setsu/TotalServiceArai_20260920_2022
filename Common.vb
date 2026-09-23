Imports System.Data.SQLite
Imports System.Diagnostics.Eventing
Imports System.Reflection
Imports System.Text
Imports DocumentFormat.OpenXml.EMMA

Public Class Common

    Shared Function SelectFolder()
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
        If fbd.ShowDialog() = DialogResult.OK Then
            '選択されたフォルダを表示する
            'Me.Label1.Text = fbd.SelectedPath
            Return fbd.SelectedPath
        Else
            'MessageBox.Show("出力先を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End If
    End Function

    Public Shared Sub SQLiteAccess()

        Dim sqlConnectionSb = New SQLiteConnectionStringBuilder("DataSource = total_service_arai.db")
        'Dim sqlConnectionSb = New SQLiteConnectionStringBuilder(GetConnectionString())
        Dim res As New StringBuilder

        Using conection As New SQLiteConnection(sqlConnectionSb.ToString())

            conection.Open()

            Using cmd As New SQLiteCommand(conection)

                'テーブル作成
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS client_master(" +
                    "clientNo INTEGER NOT NULL PRIMARY KEY," +
                    "clientName TEXT NOT NULL," +
                    "adress TEXT NOT NULL," +
                    "telNo TEXT NOT NULL," +
                    "postNo INTEGER NOT NULL)"
                cmd.ExecuteNonQuery()

                'データ追加
                cmd.CommandText = "INSERT INTO `client_master` (`clientNo`, `clientName`, `adress`, `telNo`, `postNo`) VALUES " +
                        "(1, 'フォレストハイツ弘明寺 壱番館 A棟', '横浜市南区弘明寺１－１０', '045-244-0009', '225-0021'), " +
                        "(2, 'ばなくう興業Corp', '伊那市ますみヶ丘　８８８８－２', '0260-99-9999', '876-9999'), " +
                        "(3, 'パオットLLC@', '松本市大手　１－１', '090-4707-9737', '396-0027'), " +
                        "(4, 'メゾン・ド・ロゼ', '松本市巾上９－１３', '0265-95-1675', '390-0817'), " +
                        "(7, 'ライオンズマンション', '中野区', '999-000-4444', '100-0019'), " +
                        "(10, 'BIG MOTOR　本社', '東京都港区赤坂　３', '03-4444-6660', '100-0022'), " +
                        "(11, 'ホンダドリーム　松本南', '松本市中央　８８－２ー５', '0390-45-0000', '777-7777'), " +
                        "(12, 'パオットLLC', '伊那市ますみヶ丘　８８８８－１', '0265-95-1675', '390-0027'), " +
                        "(14, '(株)システムアスカ', '塩尻市', '', '') "

                cmd.ExecuteNonQuery()

                ''データ更新
                cmd.CommandText = "UPDATE client_master SET clientName = 'ばなくう興業・インターナショナル@' WHERE clientNo = 2"
                res.Append(cmd.ExecuteNonQuery().ToString & "件更新" & vbCrLf)

                ''データ取得
                cmd.CommandText = "SELECT * FROM client_master "
                Dim reader = cmd.ExecuteReader()


                While reader.Read()

                    Debug.WriteLine(reader.Item("clientName"))
                    res.Append(reader.Item("clientName") & vbCrLf)

                End While

                MessageBox.Show(res.ToString)

                'MaxAPが300以上のでんこをMaxHPで降順ソート
                '    cmd.CommandText = "SELECT * FROM denco WHERE maxap >= 300 ORDER BY maxhp desc";
                '    Using (var reader = cmd.ExecuteReader())
                '    {
                '        var dump = Reader.DumpQuery();
                '        Console.WriteLine(dump);
                '    }

                'LIKE句比較（ここがLINQとちょっと違う）
                '    cmd.CommandText = "SELECT * FROM denco WHERE name LIKE '新居浜%'";//新居浜～で始まる名前を抽出
                '    Using (var reader = cmd.ExecuteReader())
                '    {
                '        var dump = Reader.DumpQuery();
                '        Console.WriteLine(dump);
                '    }
            End Using
        End Using

    End Sub

    Public Shared Function GetConnectionString() As String

        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        Dim resourceName = "AccessToFilesInProject.Resources.resouce.total_service_arai.db"
        'Dim path As String = "C:\Users\paott\source\repos\販売管理システム\bin\Debug\net7.0-windows"
        Dim path As String = "C:\template"
        Return New SQLiteConnectionStringBuilder() With {
            .DataSource = path & "\total_service_arai.db"
        }.ToString()
    End Function

    ''' <summary>
    ''' 「通常使うプリンタ」に設定する
    ''' </summary>
    ''' <param name="printerName">プリンタ名</param>
    Public Shared Sub SetDefaultPrinter(ByVal printerName As String)
        'WshNetworkオブジェクトを作成する
        Dim t As Type = Type.GetTypeFromProgID("WScript.Network")
        Dim wshNetwork As Object = Activator.CreateInstance(t)
        'SetDefaultPrinterメソッドを呼び出す
        t.InvokeMember("SetDefaultPrinter", System.Reflection.BindingFlags.InvokeMethod,
            Nothing, wshNetwork, New Object() {printerName})
    End Sub

    Public Shared Function GetDate() As String

        Dim Now As DateTime = DateTime.Now
        'fileName = Now.ToString("yyyyMMdd_")
        '日付取得の方法
        'If Not CheckBox1.Checked Then
        '    invoiceDate = Now.ToString("yyyy-MM-dd")
        'End If
        'Return Now.ToString("yyyy年MM月dd日")
        Return Now.ToString("yyyyMMdd-HHmmss")
    End Function

    Public Shared Sub E2p()
        'Workbookインスタンスを作成する
        Dim workbook As Spire.Xls.Workbook = New Spire.Xls.Workbook()

        'サンプルExcel文書をロードする
        workbook.LoadFromFile("C:\template\\Book1000.xlsx")

        '変換時にページに合うようにワークシートを設定する
        'workbook.ConverterSetting.SheetFitToPage = True

        '改頁を保つための設定
        For Each sheet As Spire.Xls.Worksheet In workbook.Worksheets
            sheet.PageSetup.FitToPagesWide = 1
            sheet.PageSetup.FitToPagesTall = 0
        Next

        'PDFに保存する
        workbook.SaveToFile("C:\Temp\\ExcelToPdf.pdf", Spire.Xls.FileFormat.PDF)
    End Sub

End Class
