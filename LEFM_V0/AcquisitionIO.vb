Imports NationalInstruments
Imports NationalInstruments.UI
Imports NationalInstruments.DAQmx
Module AcquisitionIO
    Public Structure Configuration_Carte
        Dim DeviceName As String
        Dim ChannelName As String
        Dim InputMax As Double
        Dim InputMin As Double
        Dim SamplingRate As Int32
        Dim AIVoltageUnits As Integer
        Dim SampleClockActiveEdge As Integer
        Dim SampleQuantityMode As Integer
    End Structure

    Private myTask As NationalInstruments.DAQmx.Task 'A new task is created when the start button is pressed
    Private reader As AnalogSingleChannelReader

    
    Sub Acquisition1(ByRef donnees() As Double, ByVal pChannel As String, ByVal ratenumeric As Double, minimumValueNumeric As Double, maximumValueNumeric As Double, ByVal durationValueNumeric As Int32)


        Try
            SplashAcquisition.Show()
            ' Create a new task
            myTask = New Task()

            ' Initialize local variables
            Dim sampleRate As Double = ratenumeric
            Dim rangeMin As Double = minimumValueNumeric
            Dim rangeMax As Double = maximumValueNumeric
            Dim duration As Double = durationValueNumeric
            Dim samplesPerChan As Int32 = Convert.ToInt32(sampleRate * duration)
            ReDim donnees(samplesPerChan)

            ' Create a channel
            Dim physicalchannel As String = pChannel

            Dim myChannel As AIChannel
            myChannel = myTask.AIChannels.CreateVoltageChannel(physicalchannel, "", AITerminalConfiguration.Differential, rangeMin, rangeMax, AIVoltageUnits.Volts)

            '   AVANT GD myTask.AIChannels.CreateVoltageChannel(physicalchannel, "",AITerminalConfiguration.Differential CType(-1, AITerminalConfiguration), rangeMin, rangeMax, AIVoltageUnits.Volts)




            ' Configure timing specs 


            myTask.Timing.ConfigureSampleClock("", sampleRate, SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, samplesPerChan)

            ' Verify the task
            myTask.Control(TaskAction.Verify)
            myTask.WaitUntilDone()

            reader = New AnalogSingleChannelReader(myTask.Stream)

            ' Read the data
            Dim data As AnalogWaveform(Of Double) = reader.ReadWaveform(samplesPerChan)


            For i = 0 To data.Samples.Count - 1 '- attention ! à la dimension des tableaux
                donnees(i) = data.Samples(i).Value
            Next

        Catch ex As DaqException
            MessageBox.Show(ex.Message)
        Finally
            myTask.Dispose()
            SplashAcquisition.Close()
        End Try
    End Sub

    Sub Acquisition(ByRef donnees() As Double, ByVal pChannel As String, ByVal ratenumeric As Double, minimumValueNumeric As Double, maximumValueNumeric As Double, ByVal durationValueNumeric As Int32)


        Try
            SplashAcquisition.Show()
            ' Create a new task
            myTask = New Task()

            ' Initialize local variables
            Dim sampleRate As Double = ratenumeric
            Dim rangeMin As Double = minimumValueNumeric
            Dim rangeMax As Double = maximumValueNumeric
            Dim duration As Double = durationValueNumeric
            Dim samplesPerChan As Int32 = Convert.ToInt32(sampleRate * duration)
            ReDim donnees(samplesPerChan)

            ' Create a channel
            Dim physicalchannel As String = pChannel

            Dim myChannel As AIChannel
            myChannel = myTask.AIChannels.CreateVoltageChannel(physicalchannel, "", AITerminalConfiguration.Differential, rangeMin, rangeMax, AIVoltageUnits.Volts)

            '   AVANT GD myTask.AIChannels.CreateVoltageChannel(physicalchannel, "",AITerminalConfiguration.Differential CType(-1, AITerminalConfiguration), rangeMin, rangeMax, AIVoltageUnits.Volts)

            ' Configure timing specs 
            myTask.Timing.ConfigureSampleClock("", sampleRate, SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, samplesPerChan)

            myTask.Start()

            ' Verify the task
            ' myTask.Control(TaskAction.Verify)

            myTask.WaitUntilDone()
            reader = New AnalogSingleChannelReader(myTask.Stream)

            ' Read the data
            Dim data(samplesPerChan - 1) As Double

            data = reader.ReadMultiSample(samplesPerChan)

            '  Dim data As AnalogWavefosamplesPerChan - 1rm(Of Double) = reader.ReadWaveform(samplesPerChan)
            For i = 0 To samplesPerChan - 1 '- attention ! à la dimension des tableaux
                donnees(i) = data(i)
            Next

            'For i = 0 To data.Samples.Count - 1 '- attention ! à la dimension des tableaux
            '    donnees(i) = data.Samples(i).Value
            'Next
            myTask.Stop()
        Catch ex As DaqException
            MessageBox.Show(ex.Message)
        Finally
            'myTask.Dispose()
            SplashAcquisition.Close()
        End Try
    End Sub

End Module
