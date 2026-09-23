Imports System
Imports System.Text

Module InvoiceTaxAdjusterTest

    Public Function exec()
        ' テスト対象範囲（必要に応じて調整）
        Dim startVal As Integer = 0
        Dim endVal As Integer = 100000
        Dim rate As Decimal = 0.1D
        Dim roundUnit As Decimal = 1000D

        Dim overCount As Integer = 0
        Dim underCount As Integer = 0
        Dim equalCount As Integer = 0
        Dim sb As New StringBuilder()

        sb.AppendLine("baseSubtotal,baseTaxExact,baseTaxFloor,expectedTarget,discount,finalSubtotal,finalTaxFloor,finalTotal,diff")

        For base As Integer = startVal To endVal
            Dim baseSubtotal As Decimal = base

            ' 基本の税（floor）および期待する targetTotal（元の合計を roundUnit 切り下げ）
            Dim baseTaxExact As Decimal = baseSubtotal * rate
            Dim baseTaxFloor As Decimal = Decimal.Floor(baseTaxExact)
            Dim baseTotal As Decimal = baseSubtotal + baseTaxFloor
            Dim expectedTarget As Decimal = baseTotal - (baseTotal Mod roundUnit)

            ' InvoiceTaxAdjuster を呼び出して値引き額を得る（项目内のモジュールを利用）
            Dim discount As Decimal = InvoiceTaxAdjuster.CalculateDiscount(baseSubtotal, roundUnit)

            ' 値引き後の計算
            Dim finalSubtotal As Decimal = baseSubtotal + discount
            If finalSubtotal < 0D Then finalSubtotal = 0D
            Dim finalTaxFloor As Decimal = Decimal.Floor(finalSubtotal * rate)
            Dim finalTotal As Decimal = finalSubtotal + finalTaxFloor

            Dim diff As Decimal = finalTotal - expectedTarget

            If diff = 0D Then
                equalCount += 1
            Else
                If diff > 0D Then
                    overCount += 1
                Else
                    underCount += 1
                End If
                ' 差がある行のみ出力（CSV形式）
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}", baseSubtotal, baseTaxExact, baseTaxFloor, expectedTarget, discount, finalSubtotal, finalTaxFloor, finalTotal, diff)
                sb.AppendLine()
            End If
        Next

        ' 結果出力
        Debug.WriteLine("検査範囲: {0} ～ {1}", startVal, endVal)
        Debug.WriteLine("一致: {0}, 多い(over): {1}, 少ない(under): {2}", equalCount, overCount, underCount)
        Console.WriteLine()
        Debug.WriteLine("差分があるケース一覧（CSV）:")
        Debug.WriteLine(sb.ToString())

        Debug.WriteLine("完了。Enterで終了します。")
        Console.ReadLine()

        Return 0
    End Function
End Module