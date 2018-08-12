Imports NationalInstruments.Analysis.Math
Module Statistiques
    Function Moyenne(ByVal data() As Double)
        Dim moy As Double
        moy = NationalInstruments.Analysis.Math.Statistics.Mean(data)
        Return moy '
    End Function
    Function Maximum(ByVal data() As Double)
        Dim max As Double
        max = NationalInstruments.Analysis.Math.ArrayOperation.GetMax(data)
        Return max '
    End Function
    Function Minimum(ByVal data() As Double)
        Dim min As Double
        min = NationalInstruments.Analysis.Math.ArrayOperation.GetMin(data)
        Return min '
    End Function
    Function indMaximum(ByVal data() As Double)
        Dim imax As Integer
        imax = NationalInstruments.Analysis.Math.ArrayOperation.GetIndexOfMax(data)
        Return imax '
    End Function
    Function AjustementPolynomial(xData() As Double, yData() As Double, numSamples As Integer, order As Integer, ByRef coefficients() As Double, ByRef rSquare As Double)

        Dim weight(numSamples - 1) As Double
        Dim fittedData() As Double
        Dim mse As Double
        For i As Integer = 0 To weight.Length - 1
            weight(i) = 1.0
        Next

        Try
            ' Calculate polynomial fit of the data set {xData, yData}
            fittedData = CurveFit.PolynomialFit(xData, yData, weight, order, PolynomialFitAlgorithm.Svd, coefficients, mse)
            Dim sse As Double
            Dim msError As Double
            NationalInstruments.Analysis.Math.CurveFit.GoodnessOfFit(yData, fittedData, weight, sse, rSquare, msError)

        Catch ex As Exception
            MsgBox("Problème d'ajustement de courbe " + vbCrLf + ex.Message, MsgBoxStyle.OkOnly, "Ajustement Polynomial")

        End Try
        Return fittedData

    End Function
End Module
