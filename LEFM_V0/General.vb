Imports System
Imports System.IO
Module General
    Public Sub Avertissement(ByRef guiControl As Object, ByVal message As String)
        Dim saveColor As New Color
        saveColor = Color.FromArgb(guiControl.BackColor.ToArgb)
        guiControl.BackColor = Color.GreenYellow
        MsgBox(message, MsgBoxStyle.OkOnly, "Avertissement")
        guiControl.BackColor = Color.FromArgb(saveColor.ToArgb)

    End Sub
End Module
