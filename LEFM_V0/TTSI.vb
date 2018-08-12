Imports NationalInstruments.Analysis
Imports NationalInstruments.Analysis.Math
Imports NationalInstruments.Analysis.Dsp
Imports NationalInstruments.Analysis.Dsp.SignalProcessing
Module TTSI
    'Public Enumeration IntegrationMethod

    Function Filtrage_PasseBande(ByVal lowFreq As Double, ByVal highFreq As Double, ByVal order As Integer, ByVal freq As Double, ByVal nPoints As Integer, ByVal signal() As Double) As Double()
        'Pour éviter les effets de bord
        Dim filteredData(nPoints + 200) As Double
        Dim signalTmp(nPoints + 200) As Double

        For i = 0 To 99
            signalTmp(i) = signal(0)
        Next
        For i = 100 To nPoints + 99
            signalTmp(i) = signal(i - 100)
        Next
        For i = nPoints + 100 To nPoints + 199
            signalTmp(i) = signal(nPoints - 1)
        Next

        'BandPass filtering
        Dim newFilter As New ButterworthBandpassFilter(order, freq, lowFreq, highFreq)
        filteredData = newFilter.FilterData(signalTmp)
        For i = 0 To nPoints - 1
            filteredData(i) = filteredData(i + 100)
        Next
        ReDim Preserve filteredData(nPoints)
        Return filteredData

    End Function
    Function Filtrage_PasseBas(ByVal lowFreq As Double, ByVal order As Integer, ByVal freq As Double, ByVal nPoints As Integer, ByVal signal() As Double) As Double()
        'REM_GD Pour éviter les effets de bord, on ajoute 200 points au début et à la fin du signal
        Dim filteredData(nPoints + 200) As Double
        Dim signalTmp(nPoints + 200) As Double

        For i = 0 To 99
            signalTmp(i) = signal(0)
        Next
        For i = 100 To nPoints + 99
            signalTmp(i) = signal(i - 100)
        Next
        For i = nPoints + 100 To nPoints + 199
            signalTmp(i) = signal(nPoints - 1)
        Next

        'LowPass filtering
        Dim newFilter As New ButterworthLowpassFilter(order, freq, lowFreq)
        filteredData = newFilter.FilterData(signalTmp)

        ''On n'inverse de la signal car il n'y a pas de déphasage

        For i = 0 To nPoints - 1 'on tronque les points ajoutés
            filteredData(i) = filteredData(i + 100)
        Next
        ReDim Preserve filteredData(nPoints)
        Return filteredData

    End Function
    Function Filtrage_PasseHaut(ByVal highFreq As Double, ByVal order As Integer, ByVal freq As Double, ByVal nPoints As Integer, ByVal signal() As Double) As Double()
        'Pour éviter les effets de bord
        Dim filteredData(nPoints + 200) As Double
        Dim signalTmp(nPoints + 200) As Double

        For i = 0 To 99
            signalTmp(i) = signal(0)
        Next
        For i = 100 To nPoints + 99
            signalTmp(i) = signal(i - 100)
        Next
        For i = nPoints + 100 To nPoints + 199
            signalTmp(i) = signal(nPoints - 1)
        Next

        'BandPass filtering
        Dim newFilter As New ButterworthHighpassFilter(order, freq, highFreq)
        filteredData = newFilter.FilterData(signalTmp)
        For i = 0 To nPoints - 1
            filteredData(i) = filteredData(i + 100)
        Next
        ReDim Preserve filteredData(nPoints)
        Return filteredData

    End Function
    Function Integre(ByVal signal() As Double, samplingPeriode As Double, initialValue As Double, finalValue As Double) As Double()
        Dim nPoints As Integer
        nPoints = signal.Count - 1 'Bizarre !!!
        Dim integratedData(nPoints) As Double

        'Integration se fait selon la méthode de Simpson
        integratedData = Integrate(signal, samplingPeriode, initialValue, finalValue)

        'integratedData = Integrate(signal, samplingPeriode, initialValue, finalValue,
        Return integratedData
    End Function
    Function Primitive(ByVal signal() As Double, samplingPeriode As Double, initialcondition As Double) As Double()
        Dim nPoints As Integer
        nPoints = signal.Count - 1
        Dim integratedData(nPoints) As Double

        For i = 0 To nPoints
            integratedData(i) = signal(i)
        Next i
        integratedData(0) = initialcondition
        For i = 1 To nPoints
            integratedData(i) = integratedData(i - 1) + (signal(i) + signal(i - 1)) / 2 * samplingPeriode
        Next i
        Return integratedData

    End Function
    Function Moyenne(signal() As Double, samplingFrequency As Double, initialValue As Integer, finalValue As Integer) As Double
        Dim i As Integer
        Dim ValeurMoyenne As Double
        ValeurMoyenne = (signal(initialValue) + signal(finalValue)) / 2
        For i = initialValue + 1 To finalValue - 1
            ValeurMoyenne = ValeurMoyenne + signal(i)
        Next i
        ValeurMoyenne = ValeurMoyenne / samplingFrequency
        Return ValeurMoyenne
    End Function
    Function derive_delta(ByVal signal() As Double, periode As Double, Npoints As Integer, frequency As Integer) As Double()
        Dim i As Integer
        Dim delta As Integer
        Dim demi_delta As Integer

        'calcul de la periode
        delta = periode * frequency

        'calcul du modulo 2 de delta
        demi_delta = delta \ 2

        Dim tampon(Npoints) As Double
        For i = 0 To Npoints
            tampon(i) = signal(i)
        Next i

        For i = 0 To demi_delta - 1 'parce que le signal commence à l'indice zero
            tampon(i) = 0
        Next
        For i = demi_delta To (Npoints - demi_delta)
            tampon(i) = (signal(i + demi_delta) - signal(i - demi_delta)) / periode
        Next
        For i = (Npoints - demi_delta) To Npoints
            tampon(i) = 0
        Next
        Return tampon
    End Function
End Module
