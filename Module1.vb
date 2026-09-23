Imports System

Module Module1

    Sub Main()
        ' 例: 添付の請求書記載の税抜合計 208,500円
        Dim baseSubtotal As Decimal = 208500D

        ' 端数値引き額を算出
        Dim discount As Decimal = CalculateDiscount(baseSubtotal)

        Console.WriteLine($"元の税抜小計: {baseSubtotal:N0} 円")
        Console.WriteLine($"値引き前の税額 (10%切捨て): {Math.Floor(baseSubtotal * 0.1D):N0} 円")
        Console.WriteLine($"値引き前の税込合計: {baseSubtotal + Math.Floor(baseSubtotal * 0.1D):N0} 円")
        Console.WriteLine("--------------------------------------")
        Console.WriteLine($"明細に追加する値引き額: {discount:N0} 円")
        Console.WriteLine("--------------------------------------")

        ' --- 検証: 明細行に値引き行を追加した後の再集計 ---
        Dim newSubtotal As Decimal = baseSubtotal + discount
        Dim newTax As Decimal = Math.Floor(newSubtotal * 0.1D)
        Dim finalTotal As Decimal = newSubtotal + newTax

        Console.WriteLine($"調整後の税抜小計: {newSubtotal:N0} 円")
        Console.WriteLine($"調整後の消費税額: {newTax:N0} 円")
        Console.WriteLine($"最終ご請求金額: {finalTotal:N0} 円")
    End Sub

    ''' <summary>
    ''' 税込金額の1,000円未満の端数を切り落とすために必要な税抜値引き額（負数）を算出します。
    ''' </summary>
    ''' <param name="baseSubtotal">値引き前の税抜小計</param>
    ''' <param name="roundUnit">丸め単位（既定値: 1,000円）</param>
    ''' <returns>明細行に入力する値引き額（端数がない場合は0）</returns>
    Public Function CalculateDiscount(ByVal baseSubtotal As Decimal, Optional ByVal roundUnit As Decimal = 1000D) As Decimal
        ' 1. 通常の消費税額（切り捨て）と税込合計を算出
        Dim tax As Decimal = Math.Floor(baseSubtotal * 0.1D)
        Dim baseTotal As Decimal = baseSubtotal + tax

        ' 2. 1,000円未満の端数を取得 (剰余算)
        Dim fraction As Decimal = baseTotal Mod roundUnit
        If fraction = 0D Then
            Return 0D
        End If

        ' 3. 目標とする税込合計金額（端数を差し引いたキリのいい金額）
        Dim targetTotal As Decimal = baseTotal - fraction

        ' 4. 目標税込額から必要な目標税抜額を逆算（切り上げ処理）
        ' ※ Math.Ceiling で税抜側を切り上げることで、消費税計算時の切捨て後の不足（1円落ち）を防止
        Dim targetSubtotal As Decimal = Math.Ceiling(targetTotal / 1.1D)

        ' 5. 明細行にセットする税抜値引き額（負の値）
        Dim discount As Decimal = targetSubtotal - baseSubtotal

        Return discount
    End Function

End Module