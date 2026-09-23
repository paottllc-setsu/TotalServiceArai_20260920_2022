Imports DocumentFormat.OpenXml.Drawing

''' <summary>
''' 請求書の端数調整および税抜値引き額の計算を行うモジュール
''' </summary>
''' 
Public Module InvoiceTaxAdjuster

    ''' <summary>
    ''' 税込金額のroundUnit未満の端数を切り落とすために必要な税抜値引き額（負数）を算出します。
    ''' </summary>
    Public Function CalculateDiscount(ByVal baseSubtotal As Decimal, Optional ByVal roundUnit As Decimal = 1000D) As Decimal

        ' 現状の税（表示用ではなくこのロジックでの floor 税）
        Dim tax As Decimal = Decimal.Floor(baseSubtotal * 0.1D)
        Dim baseTotal As Decimal = baseSubtotal + tax

        Dim fraction As Decimal = baseTotal Mod roundUnit
        If fraction = 0D Then
            Return 0D
        End If

        Dim targetTotal As Decimal = baseTotal - fraction

        ' まずは理論上の分母で切り上げで近似
        Dim candidate As Decimal = Decimal.Ceiling(targetTotal / 1.1D)

        ' candidate を下げて targetTotal に一致するものを探す
        While candidate >= 0D
            If candidate + Decimal.Floor(candidate * 0.1D) = targetTotal Then
                Exit While
            End If
            ' 過剰なら 1 円ずつ下げる
            candidate -= 1D
        End While

        If candidate < 0D Then
            ' 見つからなかった場合は安全に 0 を返す（必要なら別戦略）
            Return 0D
        End If

        Return candidate - baseSubtotal
    End Function

End Module


'Public Module InvoiceTaxAdjuster


'    ''' <summary>
'    ''' 税込金額の1,000円未満の端数を切り落とすために必要な税抜値引き額（負数）を算出します。
'    ''' </summary>
'    Public Function CalculateDiscount(ByVal baseSubtotal As Decimal, Optional ByVal roundUnit As Decimal = 1000D) As Decimal

'        Dim tax As Decimal = Math.Floor(baseSubtotal * 0.1D)
'        Dim baseTotal As Decimal = baseSubtotal + tax

'        Dim fraction As Decimal = baseTotal Mod roundUnit
'        If fraction = 0D Then
'            Return 0D
'        End If

'        Dim targetTotal As Decimal = baseTotal - fraction
'        Dim targetSubtotal As Decimal = Math.Ceiling(targetTotal / 1.1D)

'        Return targetSubtotal - baseSubtotal
'    End Function

'End Module