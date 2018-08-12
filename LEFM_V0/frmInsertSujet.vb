Public Class frmInsertSujet

    Private Sub btnFermerListe_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnValiderListe_Click(sender As Object, e As EventArgs) Handles btnValiderListe.Click
        'ajoute un sujet dans la liste de sujets 
        Dim it As New ListViewItem
        it.Text = mtxtNom.Text
        'it.SubItems.Add(mtxtNom.Text)
        it.SubItems.Add(mtxtPrenom.Text)
        it.SubItems.Add(CStr(numMasseListe.Value))
        it.SubItems.Add(CStr(numBancListe.Value))
        it.SubItems.Add(CStr(numPiedListe.Value))
        it.SubItems.Add(CStr(numAngle1Liste.Value))
        it.SubItems.Add(CStr(numAngle2Liste.Value))
        it.SubItems.Add("0")
        'frmPrincipale.lstSujets.Items.Add(it)
        'MsgBox(CStr(frmPrincipale.lstSujets.SelectedIndices(0)))
        frmPrincipale.lstSujets.Items.Insert(frmPrincipale.lstSujets.SelectedIndices(0), it)
    End Sub

    Private Sub btnAnnulerListe_Click(sender As Object, e As EventArgs) Handles btnAnnulerListe.Click
        mtxtNom.Text = ""
        'it.SubItems.Add(mtxtNom.Text)
        mtxtPrenom.Text = ""
        numMasseListe.Value = 0
        numBancListe.Value = 0
        numPiedListe.Value = 0
        numAngle1Liste.Value = 0
        numAngle2Liste.Value = 0
    End Sub
End Class