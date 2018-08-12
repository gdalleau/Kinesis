Imports System.Drawing.Drawing2D
Imports System.IO

Imports NationalInstruments
Imports DevExpress.XtraCharts

Public Class frmPrincipale

    'Pour gérer l'initialisation du programme
    Dim initDoc As Xml.XmlDocument
    Dim nomficInit As String 'nom du fichier init
    Dim init_charge As Boolean
    Dim repTravail As String 'répertoire de travail

    ' Pour gérer la configuration
    Dim doc As Xml.XmlDocument
    Dim noeuds As Xml.XmlNodeList
    Dim nomficConfig As String 'nom du fichier de configuration
    Dim config_chargee As Boolean

    'Pour la gestion ....
    Private initializationFlag As Boolean = False
    Private initializationTrial As Boolean = True
    'Private passe As Boolean = True

    Dim SujetSelectionne As Boolean = False
    Dim signal() As Double 'signal acquis par la carte

    Dim ficdata As FichierDonnees

    Dim ficDonnees() As FichierDonnees ' tableau de type FichierDonnees - correspond aux fichiers sélectionnés pour analyse
    Dim essaiFic() As Essai ' tableau de type Essai - correspond aux essais ouverts pour analyse - inclus les indices relevés ou calculés

    'variable utiles pour la calibration
    Dim coeffA As Double
    Dim coeffB As Double
    Dim TensionMoyAvide As Double
    Dim TensionMoyAvecTare As Double
    Dim calibTare As Boolean
    Dim calibVide As Boolean

    'permet de stocker le nombre d'essais par charge
    'cette structure sera associée à à un nbEssaiParTest
    Structure fvCmj
        Dim charge As String
        Dim nbessai As Integer
    End Structure

    'permet de stocker le nb d'essais par test
    'cette structure sera associée à un seul sujet
    Structure nbEssaiParTest
        Dim nbSJ As Integer
        Dim nbCMJ As Integer
        Dim nbK As Integer
        Dim nbFVcmj() As fvCmj 'plus compliqué à faire
        Dim charge As Double
        Dim nEssai As Integer
        Dim nbISO_2J As Integer
        Dim nbISO_D As Integer
        Dim nbISO_G As Integer
        Dim nbRJ As Integer
        Dim nbRJ6sec As Integer
    End Structure

    'Variables utiles pour l'analyse de FVcmj
    Dim ForceMoy() As Double
    Dim VitMoy() As Double
    Dim PuissMoy() As Double
    Dim ChargeMoy() As Double

    Dim VitChecked() As Double
    Dim ForceChecked() As Double
    Dim PuisChecked() As Double
    Dim ChargeChecked() As Double

    Dim CheckedNumber As Integer

    'pour l'ajustement des courbes
    Dim fittedForce() As Double
    Dim fittedPuissance() As Double

    Dim coeff_Force() As Double = Nothing
    Dim coeff_Puissance() As Double = Nothing
    'Dim weight(numSamples - 1) As Double

    Dim order As Integer
    Dim rSquare As Double
    Dim choix As String 'choix du type de test à analyser

    Dim lstNameFiles() As String

    Dim newAnalyse As Boolean 'nouvelle analyse lancée _ pour éviter des calculs de cohérence

    'tableau de couleurs pour affichage homogène
    Dim colors() As Color

    'Définition des constantes - Correspondent aux colonnes sur lesquelles se font les calculs de cohérence

    Const numCritCMJ1 As Integer = 5 'correspond à la puissance (W/kg)
    Const numCritCMJ2 As Integer = 7 'correspond à Zmax (m)

    Const numCritSJ As Integer = 5 'correspond à la puissance (W/kg)

    Const numCritK1 As Integer = 4 'correspond à la puissance (W/kg)
    Const numCritK2 As Integer = 5 'correspond à la raideur (N/m/kg)

    Const numCritISO1 As Integer = 5 'correspond à la Fmax
    Const numCritISO2 As Integer = 6 'correspond à la RFD
    Const numCritISO3 As Integer = 7 'correspond au temps à Fmax

    Const numCritFVcmj1 As Integer = 1 'correspond à charge
    Const numCritFVcmj2 As Integer = 5 'correspond à la puissance

    Const numCritRJ As Integer = 2

    Dim dernierRepertoire As String
    Dim nomFichierSujets As String
    Public Sub New()
        InitializeComponent()
        ReDim colors(12)
        colors(0) = Color.Aquamarine
        colors(1) = Color.DarkOrange
        colors(2) = Color.Blue
        colors(3) = Color.Red
        colors(4) = Color.Orange
        colors(5) = Color.Red
        colors(6) = Color.Blue
        colors(7) = Color.Green
        colors(8) = Color.DarkOrange
        colors(9) = Color.Lavender
        colors(10) = Color.Aquamarine
        colors(11) = Color.Blue
        

        initializationFlag = True
        chrtPrincipal.BeginInit()
        chrtPrincipal.Legend.UseCheckBoxes = True

        AddHandler chrtPrincipal.LegendItemChecked, AddressOf OnLegendItemChecked2

        chrtPrincipal.EndInit()
        initializationFlag = False
    End Sub
    Private Sub Coherence_ISO()
        'Détermination de la cohérence pour les tests isométriques
        Affichage_Labels("ISO")
        Dim i As Integer
        Dim n As Integer
        Dim ind2J As Integer 'nbr d'essais avec les deux jambes
        Dim indD As Integer 'nbr d'essais avec la jambe droite
        Dim indG As Integer 'nbr d'essais avec la jambe gauche
        Dim numTypeIso As Integer 'numéro de la colonne où se trouve le type de test càd 2J, D ou G
        numTypeIso = 4
        If lstIndices.CheckedIndices.Count = 0 Then
            lblCoherenceF2J.Visible = True
            lblCoherenceF2J.ForeColor = Color.Red
            lblCoherenceF2J.Text = "Aucun essai sélectionné !"
            lblCoherenceR2J.Visible = True
            lblCoherenceR2J.ForeColor = Color.Red
            lblCoherenceR2J.Text = "Aucun essai sélectionné !"
        Else 'le nombre d'essais sélectionné est supérieur à zéro
            Dim tab1_2J(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de force max pour les essais à 2J
            Dim tab2_2J(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de RFD pour les essais à 2J
            Dim tab3_2J(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de tps à Fmax pour les essais à 2J
            Dim tab1_D(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de force max pour les essais à D
            Dim tab2_D(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de RFD pour les essais à D
            Dim tab3_D(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de tps à Fmax pour les essais à D
            Dim tab1_G(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de force max pour les essais à G
            Dim tab2_G(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de RFD pour les essais à G
            Dim tab3_G(lstIndices.CheckedIndices.Count - 1) As Double 'tableau des valeurs de tps à Fmax pour les essais à G

            ind2J = 0
            indD = 0
            indG = 0
            For Each it In lstIndices.CheckedItems
                Select Case it.SubItems(numTypeIso).Text
                    Case "2J"
                        tab1_2J(ind2J) = CDbl(it.SubItems(numCritISO1).Text)
                        tab2_2J(ind2J) = CDbl(it.SubItems(numCritISO2).Text)
                        tab3_2J(ind2J) = CDbl(it.SubItems(numCritISO3).Text)
                        ind2J = ind2J + 1
                    Case "D"
                        tab1_D(indD) = CDbl(it.SubItems(numCritISO1).Text)
                        tab2_D(indD) = CDbl(it.SubItems(numCritISO2).Text)
                        tab3_D(indD) = CDbl(it.SubItems(numCritISO3).Text)
                        indD = indD + 1
                    Case "G"
                        tab1_G(indG) = CDbl(it.SubItems(numCritISO1).Text)
                        tab2_G(indG) = CDbl(it.SubItems(numCritISO2).Text)
                        tab3_G(indG) = CDbl(it.SubItems(numCritISO3).Text)
                        indG = indG + 1
                End Select
                            
                i = i + 1
            Next
            ReDim Preserve tab1_2J(ind2J - 1)
            ReDim Preserve tab2_2J(ind2J - 1)
            ReDim Preserve tab3_2J(ind2J - 1)
            ReDim Preserve tab1_D(indD - 1)
            ReDim Preserve tab2_D(indD - 1)
            ReDim Preserve tab3_D(indD - 1)
            ReDim Preserve tab1_G(indG - 1)
            ReDim Preserve tab2_G(indG - 1)
            ReDim Preserve tab3_G(indG - 1)

            Dim moy1_2J As Double 'moyenne de la force
            Dim sd1_2J As Double
            Dim moy2_2J As Double 'moyenne de la RFD
            Dim sd2_2J As Double
            Dim moy3_2J As Double 'moyenne du temps à Fmax

            Dim coeffCoherence1_2J As Double
            Dim coeffCoherence2_2J As Double

            Dim moy1_D As Double 'moyenne de la force
            Dim sd1_D As Double
            Dim moy2_D As Double 'moyenne de la RFD
            Dim sd2_D As Double
            Dim moy3_D As Double 'moyenne du temps à Fmax

            Dim coeffCoherence1_D As Double
            Dim coeffCoherence2_D As Double

            Dim moy1_G As Double 'moyenne de la force
            Dim sd1_G As Double
            Dim moy2_G As Double 'moyenne de la RFD
            Dim sd2_G As Double
            Dim moy3_G As Double 'moyenne du temps à Fmax

            Dim coeffCoherence1_G As Double
            Dim coeffCoherence2_G As Double

            Dim diffF As Double 'différence en force Droite et Gauche
            Dim diffR As Double  'différence en RFD Droite et Gauche
            Dim diffT As Double  'différence en Temps Droite et Gauche

            If ind2J < numMinISO.Value Then
                lblMoyenneF2J.Visible = False
                lblCoherenceF2J.Visible = True
                lblCoherenceF2J.Text = "Nombre d'essais insuffisants"
                'pourquoi ne pas mettre la moyenne quand même
                lblMoyenneR2J.Visible = False
                lblCoherenceR2J.Visible = True
                lblCoherenceR2J.Text = "Nombre d'essais insuffisants"
                lblMoyenneT2J.Visible = False
            Else
                n = ind2J
                moy1_2J = NationalInstruments.Analysis.Math.Statistics.Mean(tab1_2J)
                moy2_2J = NationalInstruments.Analysis.Math.Statistics.Mean(tab2_2J)
                moy3_2J = NationalInstruments.Analysis.Math.Statistics.Mean(tab3_2J)
                lblMoyenneF2J.Visible = True
                lblMoyenneF2J.Text = CStr(Math.Round(moy1_2J, 2))

                lblMoyenneR2J.Visible = True
                lblMoyenneR2J.Text = CStr(Math.Round(moy2_2J, 2))

                lblMoyenneT2J.Visible = True
                lblMoyenneT2J.Text = CStr(Math.Round(moy3_2J, 2))
                If moy1_2J <> 0 Then
                    sd1_2J = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab1_2J) * Math.Sqrt(n / (n - 1))
                    lblCoherenceF2J.Visible = True
                    coeffCoherence1_2J = Math.Round(sd1_2J / moy1_2J * 100, 2)
                    If coeffCoherence1_2J > 5 Then
                        lblCoherenceF2J.Text = CStr(coeffCoherence1_2J) + "%"
                    Else
                        lblCoherenceF2J.Text = "OK"
                    End If
                End If

                'problème calcule l'écart-type de Pearson
                If moy2_2J <> 0 Then
                    sd2_2J = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab2_2J) * Math.Sqrt(n / (n - 1))
                    lblCoherenceR2J.Visible = True
                    coeffCoherence2_2J = Math.Round(sd2_2J / moy2_2J * 100, 2)
                    If coeffCoherence2_2J > 5 Then
                        lblCoherenceR2J.Text = CStr(coeffCoherence2_2J) + "%"
                    Else
                        lblCoherenceR2J.Text = "OK"
                    End If
                End If
            End If
            If indD < numMinISO.Value Then
                lblMoyenneFD.Visible = False
                lblCoherenceFD.Visible = True
                lblCoherenceFD.Text = "Nombre d'essais insuffisants"
                'pourquoi ne pas mettre la moyenne quand même
                lblMoyenneRD.Visible = False
                lblCoherenceRD.Visible = True
                lblCoherenceRD.Text = "Nombre d'essais insuffisants"
                lblMoyenneTD.Visible = False
            Else
                n = indD
                moy1_D = NationalInstruments.Analysis.Math.Statistics.Mean(tab1_D)
                moy2_D = NationalInstruments.Analysis.Math.Statistics.Mean(tab2_D)
                moy3_D = NationalInstruments.Analysis.Math.Statistics.Mean(tab3_D)
                lblMoyenneFD.Visible = True
                lblMoyenneFD.Text = CStr(Math.Round(moy1_D, 2))

                lblMoyenneRD.Visible = True
                lblMoyenneRD.Text = CStr(Math.Round(moy2_D, 2))

                lblMoyenneTD.Visible = True
                lblMoyenneTD.Text = CStr(Math.Round(moy3_D, 2))

                If moy1_D <> 0 Then

                    sd1_D = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab1_D) * Math.Sqrt(n / (n - 1))
                    lblCoherenceFD.Visible = True
                    coeffCoherence1_D = Math.Round(sd1_D / moy1_D * 100, 2)
                    If coeffCoherence1_D > 5 Then
                        lblCoherenceFD.Text = CStr(coeffCoherence1_D) + "%"
                    Else
                        lblCoherenceFD.Text = "OK"
                    End If
                End If

                'problème calcule l'écart-type de Pearson
                If moy2_D <> 0 Then
                    sd2_D = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab2_D) * Math.Sqrt(n / (n - 1))
                    lblCoherenceRD.Visible = True
                    coeffCoherence2_D = Math.Round(sd2_D / moy2_D * 100, 2)
                    If coeffCoherence2_D > 5 Then
                        lblCoherenceRD.Text = CStr(coeffCoherence2_D) + "%"
                    Else
                        lblCoherenceRD.Text = "OK"
                    End If
                End If
            End If
            If indG < numMinISO.Value Then
                lblMoyenneFG.Visible = False
                lblCoherenceFG.Visible = True
                lblCoherenceFG.Text = "Nombre d'essais insuffisants"
                'pourquoi ne pas mettre la moyenne quand même
                lblMoyenneRG.Visible = False
                lblCoherenceRG.Visible = True
                lblCoherenceRG.Text = "Nombre d'essais insuffisants"
                lblMoyenneTG.Visible = False
            Else
                n = indG
                moy1_G = NationalInstruments.Analysis.Math.Statistics.Mean(tab1_G)
                moy2_G = NationalInstruments.Analysis.Math.Statistics.Mean(tab2_G)
                moy3_G = NationalInstruments.Analysis.Math.Statistics.Mean(tab3_G)
                lblMoyenneFG.Visible = True
                lblMoyenneFG.Text = CStr(Math.Round(moy1_G, 2))

                lblMoyenneRG.Visible = True
                lblMoyenneRG.Text = CStr(Math.Round(moy2_G, 2))

                lblMoyenneTG.Visible = True
                lblMoyenneTG.Text = CStr(Math.Round(moy3_G, 2))

                If moy1_G <> 0 Then
                    sd1_G = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab1_G) * Math.Sqrt(n / (n - 1))
                    lblCoherenceFG.Visible = True
                    coeffCoherence1_G = Math.Round(sd1_G / moy1_G * 100, 2)
                    If coeffCoherence1_G > 5 Then
                        lblCoherenceFG.Text = CStr(coeffCoherence1_G) + "%"
                    Else
                        lblCoherenceFG.Text = "OK"
                    End If
                End If

                'problème calcule l'écart-type de Pearson
                If moy2_G <> 0 Then
                    sd2_G = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab2_G) * Math.Sqrt(n / (n - 1))
                    lblCoherenceRG.Visible = True
                    coeffCoherence2_G = Math.Round(sd2_G / moy2_G * 100, 2)
                    If coeffCoherence2_G > 5 Then
                        lblCoherenceRG.Text = CStr(coeffCoherence2_G) + "%"
                    Else
                        lblCoherenceRG.Text = "OK"
                    End If
                End If
            End If
            If moy1_D <> 0 Then
                diffF = Math.Round((moy1_G - moy1_D) / moy1_D * 100, 2)
                lblMoyenneFDiff.Text = CStr(diffF)
                lblMoyenneFDiff.Visible = True
            Else
                lblMoyenneFDiff.Text = "Neant"
                lblMoyenneFDiff.Visible = True
            End If
            If moy2_D <> 0 Then
                diffR = Math.Round((moy2_G - moy2_D) / moy2_D * 100, 2)
                lblMoyenneRDiff.Text = CStr(diffR)
                lblMoyenneRDiff.Visible = True
            Else
                lblMoyenneRDiff.Text = "Neant"
                lblMoyenneRDiff.Visible = True
            End If
            If moy3_D <> 0 Then
                diffT = Math.Round((moy3_G - moy3_D) / moy3_D * 100, 2)
                lblMoyenneTdiff.Text = CStr(diffT)
                lblMoyenneTdiff.Visible = True
            Else
                lblMoyenneTdiff.Text = "Neant"
                lblMoyenneTdiff.Visible = True
            End If
                'Dim n As Integer
                'Dim i As Integer
                'i = 0
                'n = lstIndices.CheckedIndices.Count



                'moy1 = NationalInstruments.Analysis.Math.Statistics.Mean(tab1)
                'moy2 = NationalInstruments.Analysis.Math.Statistics.Mean(tab2)

                'lblVariable1.Visible = True
                'lblVariable1.Text = "Puissance (W/kg)"
                'lblMoyenne1.Visible = True
                'lblMoyenne1.Text = CStr(Math.Round(moy1, 2))

                'lblVariable2.Visible = True
                'lblVariable2.Text = "Zmax (m)"
                'lblMoyenne2.Visible = True
                'lblMoyenne2.Text = CStr(Math.Round(moy2, 2))

                'If lstIndices.CheckedIndices.Count < numMinCMJ.Value Then 'le nombre d'essais est inférieur au nombre minimum
                '    lblMoyenne1.Visible = False
                '    lblMoyenne2.Visible = False
                '    lblCoherence1.Visible = True
                '    lblCoherence2.Visible = False
                '    lblCoherence1.Text = "Nombre d'essais insuffisants"
                'Else 'le nombre d'essais est suffisant et supérieur au nombre minimum

                '    'problème calcule l'écart-type de Pearson
                '    If moy1 <> 0 Then
                '        sd1 = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab1) * Math.Sqrt(n / (n - 1))
                '        lblCoherence1.Visible = True
                '        coeffCoherence = Math.Round(sd1 / moy1 * 100, 2)
                '        If coeffCoherence > 5 Then
                '            lblCoherence1.Text = CStr(coeffCoherence) + "%"
                '        Else
                '            lblCoherence1.Text = "OK"
                '        End If
                '    End If

                '    'problème calcule l'écart-type de Pearson
                '    If moy2 <> 0 Then
                '        sd2 = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab2) * Math.Sqrt(n / (n - 1))
                '        lblCoherence2.Visible = True
                '        coeffCoherence = Math.Round(sd2 / moy2 * 100, 2)
                '        If coeffCoherence > 5 Then
                '            lblCoherence2.Text = CStr(coeffCoherence) + "%"
                '        Else
                '            lblCoherence2.Text = "OK"
                '        End If
                '    End If
                'End If
        End If

    End Sub
    Private Sub Coherence_K()
        '// Calcul de la cohérence pour le CMJ et le SJ
        ' Cette routine est une routine décomposée à partir de l'ancienne
        ' numCritCMJ1 etnumCritCMJ2 sont deux constantes définies 
        ' elles indiquent les colonnens sur lesquelles se fait le calul de cohérence
        Affichage_Labels("K")
        lblMoyenneGr1.Visible = True
        lblCoherenceGr1.Visible = True
        If lstIndices.CheckedIndices.Count = 0 Then
            lblCoherence1.Visible = True
            lblCoherence1.ForeColor = Color.Red
            lblCoherence1.Text = "Aucun essai sélectionné !"
        Else 'le nombre d'essais sélectionné est supérieur à zéro
            Dim tab1(lstIndices.CheckedIndices.Count - 1) As Double
            Dim tab2(lstIndices.CheckedIndices.Count - 1) As Double
            Dim moy1 As Double
            Dim sd1 As Double
            Dim moy2 As Double
            Dim sd2 As Double
            Dim coeffCoherence As Double
            Dim n As Integer
            Dim i As Integer
            i = 0
            n = lstIndices.CheckedIndices.Count

            For Each it In lstIndices.CheckedItems
                tab1(i) = CDbl(it.SubItems(numCritK1).Text) 'cohérence est calulée sur la puissance relative
                tab2(i) = CDbl(it.SubItems(numCritK2).Text) 'cohérence est calulée sur la raideur relative
                i = i + 1
            Next

            moy1 = NationalInstruments.Analysis.Math.Statistics.Mean(tab1)
            moy2 = NationalInstruments.Analysis.Math.Statistics.Mean(tab2)

            lblVariable1.Visible = True
            lblVariable1.Text = "Puissance (W/kg)"
            lblMoyenne1.Visible = True
            lblMoyenne1.Text = CStr(Math.Round(moy1, 2))

            lblVariable2.Visible = True
            lblVariable2.Text = "Raideur (N/m/kg)"
            lblMoyenne2.Visible = True
            lblMoyenne2.Text = CStr(Math.Round(moy2, 2))

            If lstIndices.CheckedIndices.Count < numMinK.Value Then 'le nombre d'essais est inférieur au nombre minimum
                lblMoyenne1.Visible = False
                lblMoyenne2.Visible = False
                lblCoherence1.Visible = True
                lblCoherence2.Visible = False
                lblCoherence1.Text = "Nombre d'essais insuffisants"
            Else 'le nombre d'essais est suffisant et supérieur au nombre minimum

                'problème calcule l'écart-type de Pearson
                If moy1 <> 0 Then
                    sd1 = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab1) * Math.Sqrt(n / (n - 1))
                    lblCoherence1.Visible = True
                    coeffCoherence = Math.Round(sd1 / moy1 * 100, 2)
                    If coeffCoherence > 5 Then
                        lblCoherence1.Text = CStr(coeffCoherence) + "%"
                    Else
                        lblCoherence1.Text = "OK"
                    End If
                End If

                'problème calcule l'écart-type de Pearson
                If moy2 <> 0 Then
                    sd2 = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab2) * Math.Sqrt(n / (n - 1))
                    lblCoherence2.Visible = True
                    coeffCoherence = Math.Round(sd2 / moy2 * 100, 2)
                    If coeffCoherence > 5 Then
                        lblCoherence2.Text = CStr(coeffCoherence) + "%"
                    Else
                        lblCoherence2.Text = "OK"
                    End If
                End If
            End If
        End If

    End Sub
    Private Sub Coherence_RJ()
        '/// plutôt qu'un calcul de cohérence 
        '/// affiche ici des informations imporatntes
        Affichage_Labels("RJ")
        lblMoyenneGr1.Visible = False
        lblCoherenceGr1.Visible = False
    End Sub
    Private Sub Coherence_FVcmj(ByVal c_Force() As Double, ByVal c_Puissance() As Double, ByVal puissance_ajustees() As Double, ByVal vitesses() As Double)
        '/// plutôt qu'un calcul de cohérence 
        '/// affiche ici des informations imporatntes
        Affichage_Labels("FVcmj")
        lblMoyenneGr1.Visible = False
        lblCoherenceGr1.Visible = False

        If lstIndices.CheckedIndices.Count = 0 Then
            lblCoherence1.Visible = True
            lblCoherence1.ForeColor = Color.Red
            lblCoherence1.Text = "Aucun essai sélectionné !"
        Else
            lblVariable1.Visible = True
            lblVariable1.Text = "Force (N) = " + CStr(Math.Round(c_Force(1), 2)) + " * Vitesse + " + CStr(Math.Round(c_Force(0), 2))

            'le nombre d'essais sélectionné est supérieur à zéro
            Dim tab1(lstIndices.CheckedIndices.Count - 1) As Double
            Dim tab2(lstIndices.CheckedIndices.Count - 1) As Double
            Dim imax2 As Integer
            'Dim sd1 As Double
            'Dim moy2 As Double
            'Dim sd2 As Double
            'Dim coeffCoherence As Double
            Dim n As Integer
            Dim i As Integer
            i = 0
            n = lstIndices.CheckedIndices.Count

            For Each it In lstIndices.CheckedItems
                tab1(i) = CDbl(it.SubItems(numCritFVcmj1).Text) 'les valeurs de charge
                tab2(i) = CDbl(it.SubItems(numCritFVcmj2).Text) 'les valeurs de puissance
                i = i + 1
            Next

            imax2 = Statistiques.indMaximum(tab2)

            'moy2 = NationalInstruments.Analysis.Math.Statistics.Mean(tab2)

            'lblVariable1.Visible = True
            'lblVariable1.Text = "Puissance (W/kg)"

            lblMoyenne1.Visible = True
            lblMoyenne1.Text = "Puissance Max de " + CStr(Math.Round(tab2(CInt(imax2)), 2)) + " (W/kg)"
            lblCoherence1.Visible = True
            lblCoherence1.Text = "pour une charge de " + CStr(tab1(CInt(imax2))) + " kg"
            'lblVariable2.Visible = True
            'lblVariable2.Text = "Zmax (m)"
            'lblMoyenne2.Visible = True
            'lblMoyenne2.Text = CStr(Math.Round(moy2, 2))

            'If lstIndices.CheckedIndices.Count < numMinCMJ.Value Then 'le nombre d'essais est inférieur au nombre minimum
            '    lblMoyenne1.Visible = False
            '    lblMoyenne2.Visible = False
            '    lblCoherence1.Visible = True
            '    lblCoherence2.Visible = False
            '    lblCoherence1.Text = "Nombre d'essais insuffisants"
            'Else 'le nombre d'essais est suffisant et supérieur au nombre minimum

            '    'problème calcule l'écart-type de Pearson
            '    If moy1 <> 0 Then
            '        sd1 = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab1) * Math.Sqrt(n / (n - 1))
            '        lblCoherence1.Visible = True
            '        coeffCoherence = Math.Round(sd1 / moy1 * 100, 2)
            '        If coeffCoherence > 5 Then
            '            lblCoherence1.Text = CStr(coeffCoherence) + "%"
            '        Else
            '            lblCoherence1.Text = "OK"
            '        End If
            '    End If

            '    'problème calcule l'écart-type de Pearson
            '    If moy2 <> 0 Then
            '        sd2 = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab2) * Math.Sqrt(n / (n - 1))
            '        lblCoherence2.Visible = True
            '        coeffCoherence = Math.Round(sd2 / moy2 * 100, 2)
            '        If coeffCoherence > 5 Then
            '            lblCoherence2.Text = CStr(coeffCoherence) + "%"
            '        Else
            '            lblCoherence2.Text = "OK"
            '        End If
            '    End If
            'End If
        End If

    End Sub
    Private Sub Coherence_CMJ()
        '// Calcul de la cohérence pour le CMJ et le SJ
        ' Cette routine est une routine décomposée à partir de l'ancienne
        ' numCritCMJ1 etnumCritCMJ2 sont deux constantes définies 
        ' elles indiquent les colonnens sur lesquelles se fait le calul de cohérence
        Affichage_Labels("CMJ")
        lblMoyenneGr1.Visible = True
        lblCoherenceGr1.Visible = True
        If lstIndices.CheckedIndices.Count = 0 Then
            lblCoherence1.Visible = True
            lblCoherence1.ForeColor = Color.Red
            lblCoherence1.Text = "Aucun essai sélectionné !"
        Else 'le nombre d'essais sélectionné est supérieur à zéro
            Dim tab1(lstIndices.CheckedIndices.Count - 1) As Double
            Dim tab2(lstIndices.CheckedIndices.Count - 1) As Double
            Dim moy1 As Double
            Dim sd1 As Double
            Dim moy2 As Double
            Dim sd2 As Double
            Dim coeffCoherence As Double
            Dim n As Integer
            Dim i As Integer
            i = 0
            n = lstIndices.CheckedIndices.Count

            For Each it In lstIndices.CheckedItems
                tab1(i) = CDbl(it.SubItems(numCritCMJ1).Text) 'cohérence est calulée sur la puissance relative
                tab2(i) = CDbl(it.SubItems(numCritCMJ2).Text) 'cohérence est calulée sur la position maximale
                i = i + 1
            Next

            moy1 = NationalInstruments.Analysis.Math.Statistics.Mean(tab1)
            moy2 = NationalInstruments.Analysis.Math.Statistics.Mean(tab2)

            lblVariable1.Visible = True
            lblVariable1.Text = "Puissance (W/kg)"
            lblMoyenne1.Visible = True
            lblMoyenne1.Text = CStr(Math.Round(moy1, 2))

            'Modification pour Senges pas d'affichage de la hauteur
            'lblVariable2.Visible = True
            'lblVariable2.Text = "Zmax (m)"
            'lblMoyenne2.Visible = True
            'lblMoyenne2.Text = CStr(Math.Round(moy2, 2))

            If lstIndices.CheckedIndices.Count < numMinCMJ.Value Then 'le nombre d'essais est inférieur au nombre minimum
                lblMoyenne1.Visible = False
                'lblMoyenne2.Visible = False
                lblCoherence1.Visible = True
                'lblCoherence2.Visible = False
                lblCoherence1.Text = "Nombre d'essais insuffisants"
            Else 'le nombre d'essais est suffisant et supérieur au nombre minimum

                'problème calcule l'écart-type de Pearson
                If moy1 <> 0 Then
                    sd1 = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab1) * Math.Sqrt(n / (n - 1))
                    lblCoherence1.Visible = True
                    coeffCoherence = Math.Round(sd1 / moy1 * 100, 2)
                    If coeffCoherence > 5 Then
                        lblCoherence1.Text = CStr(coeffCoherence) + "%"
                    Else
                        lblCoherence1.Text = "OK"
                    End If
                End If

                'problème calcule l'écart-type de Pearson
                If moy2 <> 0 Then
                    sd2 = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab2) * Math.Sqrt(n / (n - 1))
                    'lblCoherence2.Visible = True
                    coeffCoherence = Math.Round(sd2 / moy2 * 100, 2)
                    If coeffCoherence > 5 Then
                        'lblCoherence2.Text = CStr(coeffCoherence) + "%"
                    Else
                        'lblCoherence2.Text = "OK"
                    End If
                End If
            End If
        End If

    End Sub
    Private Sub lstTests_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTests.SelectedIndexChanged
        Dim nbTest As nbEssaiParTest

        Select Case lstTests.SelectedIndex
            Case 0 'SJ
                AfficherReglage(False, False)
                numDuree.Value = 5
                'il peut y avoir une erreur si un sujet n'est pas sélectionné
                If lstSujets.SelectedItems.Count <> 0 Then
                    nbTest = lstSujets.SelectedItems(0).Tag
                    lblNbEssais.Text = CStr(nbTest.nbSJ)
                End If

            Case 1 'CMJ
                AfficherReglage(False, False)
                numDuree.Value = 5
                If lstSujets.SelectedItems.Count <> 0 Then
                    nbTest = lstSujets.SelectedItems(0).Tag
                    lblNbEssais.Text = CStr(nbTest.nbCMJ)
                End If

            Case 2 'K
                AfficherReglage(False, False)
                numDuree.Value = 5
                If lstSujets.SelectedItems.Count <> 0 Then
                    nbTest = lstSujets.SelectedItems(0).Tag
                    lblNbEssais.Text = CStr(nbTest.nbK)
                End If

            Case 3 'ISO
                AfficherReglage(True, False)
                numDuree.Value = 5
                If lstSujets.SelectedItems.Count <> 0 Then
                    nbTest = lstSujets.SelectedItems(0).Tag

                    If rb2pieds.Checked Then
                        lblNbEssais.Text = CStr(nbTest.nbISO_2J)
                    ElseIf rbPiedD.Checked Then
                        lblNbEssais.Text = CStr(nbTest.nbISO_D)
                    ElseIf rbPiedG.Checked Then
                        lblNbEssais.Text = CStr(nbTest.nbISO_G)
                    End If
                End If

            Case 4 'FVcmj'
                AfficherReglage(False, True)
                numDuree.Value = 5
                If lstSujets.SelectedItems.Count <> 0 Then
                    nbTest = lstSujets.SelectedItems(0).Tag
                    Dim dernierFV As New fvCmj
                    If (nbTest.nbFVcmj Is Nothing) Then
                        'dernierFV = nbTest.nbFVcmj(nbTest.nbFVcmj.Count)
                        lblNbEssais.Text = CStr(0)
                        ' MsgBox("pas d'essai")
                    Else
                        lblNbEssais.Text = CStr(nbTest.nbFVcmj.Count) + " charges testées"""
                    End If
                End If

            Case 5 'RJ'
                AfficherReglage(False, False)
                numDuree.Value = 30
            Case 6 'RJ6sec'
                AfficherReglage(False, False)
                numDuree.Value = 6
            Case Else
                AfficherReglage(False, False)
                If lstSujets.SelectedItems.Count <> 0 Then
                    lblNbEssais.Text = CStr(0)
                End If
        End Select

    End Sub
    Private Sub AfficherReglage(ByVal afficheISO As Boolean, ByVal afficheFV As Boolean)
        'Cette routine affiche ou pas certains élements de la page Acquisition
        'en fonction du type de test choisi
        Try
            lblBanc.Visible = afficheISO
            numBanc.Visible = afficheISO
            lblPieds.Visible = afficheISO
            numPieds.Visible = afficheISO
            rb2pieds.Visible = afficheISO
            rb2pieds.Checked = afficheISO
            rbPiedD.Visible = afficheISO
            rbPiedG.Visible = afficheISO
            chkCouche.Visible = afficheISO
            lblAngle1.Visible = afficheFV
            numAngle1.Visible = afficheFV
            lblAngle2.Visible = afficheFV
            numAngle2.Visible = afficheFV
            lblCharge.Visible = afficheFV
            numCharge.Visible = afficheFV
            chkSaut.Visible = afficheFV
            chkBlesse.Checked = False
            cmbxBlessures.Visible = chkBlesse.Checked
        Catch ex As Exception
            MsgBox("Problème Affichage" + vbCrLf + ex.Message, MsgBoxStyle.OkOnly, "Affichage des Reglages")
        End Try

    End Sub
    Private Sub Charge_Config(nomFichier As String)
        If nomFichier <> "" Then
            lblNomConfig.Text = nomFichier
            doc.Load(nomFichier)
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Calibration")
            unNoeud = noeuds.Item(0)
            lblDateModif.Text = unNoeud.InnerText
            config_chargee = True
            Affiche_Blessure(config_chargee)
            Lire_Repertoire(config_chargee)
            Lire_Coefficients(config_chargee)
            Lire_Seuils(config_chargee)
            Lire_Parametres_Carte(config_chargee)
            Lire_Ordre_Reg(config_chargee)
            Lire_NbEssais(config_chargee)
        Else
            config_chargee = False
        End If
    End Sub
    Private Sub Charge_Init(nomFichier As String)
        If nomFichier <> "" Then
            'lblNomConfig.Text = nomFichier
            initDoc.Load(nomFichier) 'une erreur est possible si le fichier n'existe pas
            'la vérification doit se faire en amont

            Dim unNoeud As Xml.XmlNode
            noeuds = initDoc.GetElementsByTagName("FichierConfig")
            unNoeud = noeuds.Item(0)
            nomficConfig = unNoeud.InnerText '

            'If nomficConfig = "" Then

            'End If
            noeuds = initDoc.GetElementsByTagName("Repertoire")
            unNoeud = noeuds.Item(0)
            repTravail = unNoeud.InnerText

            init_charge = True


        Else
            init_charge = False
        End If
    End Sub
    Private Sub Ecrire_Config(nomFichier As String)
        If nomFichier <> "" Then
            lblNomConfig.Text = nomFichier

            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Calibration")
            unNoeud = noeuds.Item(0)
            unNoeud.InnerText = lblDateModif.Text
            config_chargee = True
            'Affiche_Blessure(config_chargee)
            Ecrire_Repertoire(config_chargee)
            Ecrire_Coefficients(config_chargee)
            Ecrire_Seuils(config_chargee)
            Ecrire_Parametres_Carte(config_chargee)
            Ecrire_Ordre_Reg(config_chargee)
            Ecrire_NbEssais(config_chargee)
        Else
            config_chargee = False
        End If
    End Sub
    Private Sub Ecrire_Init(tmpNoeud As String, tmpValeur As String)
        'pas de vérification faite ici

        If init_charge Then
            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = initDoc.GetElementsByTagName(tmpNoeud)
            unNoeud = noeuds.Item(0)
            unNoeud.InnerText = tmpValeur
            initDoc.Save(nomficInit)
        Else
            init_charge = False
        End If
    End Sub
    Private Sub Affichage_Labels(typeLbl As String)
        Select Case typeLbl
            Case "CMJ", "SJ"
                lblVariable1.Visible = True
                lblVariable2.Visible = False
                lblMoyenne1.Visible = True
                lblMoyenne2.Visible = False
                lblCoherence1.Visible = True
                lblCoherence2.Visible = False
                lblMoyenneGr1.Visible = True
                lblCoherenceGr1.Visible = True
                grbxOrdre.Visible = False
                lblTypeCourbe_Analyse.Visible = False
                cbxTypeCourbe_Analyse.Visible = False
            Case "RJ"
                lblVariable1.Visible = False
                lblVariable2.Visible = False
                lblMoyenne1.Visible = False
                lblMoyenne2.Visible = False
                lblCoherence1.Visible = False
                lblCoherence2.Visible = False
                lblMoyenneGr1.Visible = False
                lblCoherenceGr1.Visible = False
                grbxOrdre.Visible = False
                lblTypeCourbe_Analyse.Visible = False
                cbxTypeCourbe_Analyse.Visible = False
            Case "ISO"
                lblVariable1.Visible = False
                lblVariable2.Visible = False
                lblMoyenne1.Visible = False
                lblMoyenne2.Visible = False
                lblCoherence1.Visible = False
                lblCoherence2.Visible = False
                lblMoyenneGr1.Visible = False
                lblCoherenceGr1.Visible = False
                grbxOrdre.Visible = False
                lblTypeCourbe_Analyse.Visible = True
                cbxTypeCourbe_Analyse.Visible = True

            Case "FVcmj", "FVsj"
                lblVariable1.Visible = True
                lblVariable2.Visible = False
                lblMoyenne1.Visible = True
                lblMoyenne2.Visible = False
                lblCoherence1.Visible = True
                lblCoherence2.Visible = False
                lblMoyenneGr1.Visible = False
                lblCoherenceGr1.Visible = False
                grbxOrdre.Visible = True
                lblTypeCourbe_Analyse.Visible = False
                cbxTypeCourbe_Analyse.Visible = False
        End Select

    End Sub
    Private Sub Ajout_Blessure(ByVal config As Boolean)
        If config And txtBlessure.Text <> "" Then
            Dim unNoeud As Xml.XmlNode

            noeuds = doc.GetElementsByTagName("Blessures")
            unNoeud = noeuds.Item(noeuds.Count - 1)

            '' Pour ajouter un child au noeud
            Dim n1 As Xml.XmlNode
            n1 = doc.CreateNode(Xml.XmlNodeType.Element, "type", "")
            n1.InnerText = txtBlessure.Text
            unNoeud.AppendChild(n1)

        End If
    End Sub
   
    Private Sub Affiche_Blessure(ByVal config As Boolean)
        If config Then
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("type")
            lstBlessures.Items.Clear()
            For Each unNoeud In noeuds
                lstBlessures.Items.Add(unNoeud.InnerText)
            Next
        End If
    End Sub
    Private Sub Lire_Repertoire(ByVal cconfig)
        If config_chargee Then

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Repertoire")
            unNoeud = noeuds.Item(0)
            txtRepertoire.Text = unNoeud.InnerText

        End If
    End Sub
    Private Sub Ecrire_Repertoire(ByVal cconfig)
        If config_chargee Then

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Repertoire")
            unNoeud = noeuds.Item(0)
            unNoeud.InnerText = txtRepertoire.Text

        End If
    End Sub
    Private Sub Lire_Coefficients(ByVal config As Boolean)
        If config_chargee Then

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Calibration")
            unNoeud = noeuds.Item(0)
            txtCoeffA_Config.Text = unNoeud.Attributes("coeffA").Value
            txtCoeffB_Config.Text = unNoeud.Attributes("coeffB").Value
            coeffA = CDbl(txtCoeffA_Config.Text)
            coeffB = CDbl(txtCoeffB_Config.Text)
        End If
    End Sub
    Private Sub Ecrire_Coefficients(ByVal config As Boolean)
        If config_chargee Then

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Calibration")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("coeffA").Value = txtCoeffA_Config.Text
            unNoeud.Attributes("coeffB").Value = txtCoeffB_Config.Text
            Dim date_modif As Date
            date_modif = Now()
            unNoeud.InnerText = CStr(date_modif)

        End If
    End Sub
    Private Sub Lire_Ordre_Reg(ByVal config As Boolean)
        If config_chargee Then

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("FVcmj")
            unNoeud = noeuds.Item(0)
            numOrdreReg.Value = CInt(unNoeud.Attributes("ordre_reg").Value)

        End If
    End Sub
    Private Sub Ecrire_Ordre_Reg(ByVal config As Boolean)
        If config_chargee Then
            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("FVcmj")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("ordre_reg").Value = CStr(numOrdreReg.Value)

        End If
    End Sub
    Private Sub Lire_Seuils(ByVal config As Boolean)
        If config_chargee Then

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Seuils")
            unNoeud = noeuds.Item(0)
            numSeuilFz1.Value = CInt(unNoeud.Attributes("Fz1").Value)
            numSeuilFz2.Value = CInt(unNoeud.Attributes("Fz2").Value)

        End If
    End Sub
    Private Sub Ecrire_Seuils(ByVal config As Boolean)
        If config_chargee Then
            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Seuils")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("Fz1").Value = CStr(numSeuilFz1.Value)
            unNoeud.Attributes("Fz2").Value = CStr(numSeuilFz2.Value)

        End If
    End Sub
    Private Sub Lire_Parametres_Carte(ByVal config As Boolean)
        If config_chargee Then

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Carte")
            unNoeud = noeuds.Item(0)
            txtNomCarte.Text = unNoeud.InnerText           
            txtNomVoie.Text = unNoeud.Attributes("NomVoie").Value
            
        End If
    End Sub
    Private Sub Ecrire_Parametres_Carte(ByVal config As Boolean)
        If config_chargee Then

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("Carte")
            unNoeud = noeuds.Item(0)
            unNoeud.InnerText = txtNomCarte.Text          
            unNoeud.Attributes("NomVoie").Value = txtNomVoie.Text


        End If
    End Sub
  
    Private Sub rb2pieds_CheckedChanged(sender As Object, e As EventArgs) Handles rb2pieds.CheckedChanged
        Dim nbTest As nbEssaiParTest
        If rb2pieds.Checked And lstSujets.SelectedItems.Count <> 0 Then
            nbTest = lstSujets.SelectedItems(0).Tag
            lblNbEssais.Text = CStr(nbTest.nbISO_2J)
        End If
    End Sub

    Private Sub chkBlesse_CheckedChanged(sender As Object, e As EventArgs) Handles chkBlesse.CheckedChanged
        cmbxBlessures.Visible = chkBlesse.Checked
    End Sub

    Private Sub btnPoidsSujet_Click(sender As Object, e As EventArgs) Handles btnPoidsSujet.Click

        'Vérification si un sujet a été sélectionné à mettre aussi
        'Dim itSujet As New ListViewItem
        If lstSujets.CheckedIndices.Count <> 0 Or lstSujets.SelectedIndices.Count <> 0 Then
            SujetSelectionne = True 'un sujet a été sélectionné
        Else
            Avertissement(lstSujets, "Vous devez sélectionner un sujet")
            Exit Sub 'on sort de la procédure car il faut sélectionner un sujet
        End If


        'Mesure et calcul de la masse
        Try
            'Dim message, titre, defaultValue As String
            ' Dim maValeur As Object
            'Dim massemesuree As Double
            Dim msgRep As MsgBoxResult
            Dim frequence As Double
            Dim duree As Double
            Dim TensionMoy As Double
            Dim Masse As Double
            '****************************************************
            'Remplacer par une procédure d'acquisition
            'Calul d 'une moyenne
            '   créer cette fonction massemesuree = mesuremasse()
            '   massemesuree = general.moyenne_vecteur(signal) / 9.81
            '   lblMasseMesuree.Text = CStr(Math.Round(massemesuree, 1)) + " kg"
            '********************

            frequence = 1000
            duree = 3
            '/ Mesure : Acquisition
            Dim nbPoints As Double
            nbPoints = duree * frequence 'l'aquisition se fait sur 3 secondes
            Dim donnees(nbPoints) As Double
            Acquisition(donnees, txtNomCarte.Text + "/" + txtNomVoie.Text, frequence, -10, 10, duree)
            calibVide = True
            TensionMoy = NationalInstruments.Analysis.Math.Statistics.Mean(donnees)

            Masse = (coeffA * TensionMoy + coeffB) / 9.81

            msgRep = MsgBox("La masse mesurée est: " + Format(Masse, "###0.000") + " kg." + vbCrLf + "Voulez-vous garder cette valeur comme masse du sujet?", MsgBoxStyle.YesNo, "Mesure du poids du sujet")
            If msgRep = 6 Then
                Dim lstSelectedItem As ListViewItem.ListViewSubItem
                lblMasseMesuree.Text = Format(Masse, "###0.000") + " kg"
                lstSelectedItem = lstSujets.SelectedItems(0).SubItems(2)
                lstSelectedItem.Text = Format(Masse, "###0.000")
            End If


        Catch ex As Exception
            MsgBox("Problème de mesure du poids" + vbCrLf + ex.Message, MsgBoxStyle.OkOnly, sender.text)

        End Try


    End Sub

    Private Sub btnMesure_Click(sender As Object, e As EventArgs) Handles btnMesure.Click
        Dim i As Integer
        Try
            '/ Vérification avant de lancer la mesure
            If lstSujets.SelectedItems.Count = 0 Then
                Avertissement(lstSujets, "Vous devez sélectionner un sujet")
                Exit Sub 'on sort de la procédure car il faut sélectionner un sujet
            End If
            If lstTests.Text = Nothing Then
                Avertissement(lstTests, "Vous devez sélectionner un test")
                Exit Sub 'on sort de la procédure car il faut sélectionner un test
            End If

            '/ Mesure : Acquisition
            'REM_GD       Un soucis à traiter
            '       Comment avoir la liste des cartes installées
            '       Comment avoir le nom de la voie à traiter
            '       Peut-être faire passer en constante le "Dev1/Ai0" et l'intégrer au fichier de configuration
            '       idem pour les valeurs min et les valeurs max
            
            Dim nbPoints As Double
            nbPoints = numDuree.Value * numFreq.Value
            ReDim signal(nbPoints)

            btnMesure.Enabled = False
            Acquisition(signal, txtNomCarte.Text + "/" + txtNomVoie.Text, numFreq.Value, -10, 10, numDuree.Value)
            btnMesure.Enabled = True

            
            '/ Affichage avec DevExpress
            ' Create the first line series and add points to it. 
            Dim serieForce As New DevExpress.XtraCharts.Series("Force", DevExpress.XtraCharts.ViewType.Line)
            Dim donnees(nbPoints) As Double
            Dim temps(nbPoints) As Double

            For i = 0 To nbPoints - 1
                donnees(i) = coeffA * signal(i) + coeffB
                signal(i) = donnees(i)
                temps(i) = i / numFreq.Value
                serieForce.Points.Add(New DevExpress.XtraCharts.SeriesPoint(temps(i), Math.Round(donnees(i), 2)))
            Next i


            'Cast the chart's diagram to the XYDiagram type, to access its axes. 
            Dim diagram As New DevExpress.XtraCharts.XYDiagram
            diagram.Assign(chartForce.Diagram)
            'diagram.AxisY.CrosshairAxisLabelOptions.
            ' Create a constant line. 
            Dim WeightLine As New DevExpress.XtraCharts.ConstantLine("Poids")
            If diagram.AxisY.ConstantLines.Count <> 0 Then
                diagram.AxisY.ConstantLines.RemoveAt(0)
            End If
            diagram.AxisY.ConstantLines.Add(WeightLine)
            WeightLine.AxisValue = CDbl((lblMasseMesuree.Text.Substring(0, Len(lblMasseMesuree.Text) - 3) * 9.81)) 'utiliser la masse courante
            WeightLine.Visible = True
            WeightLine.ShowBehind = False

            WeightLine.Title.Visible = True
            WeightLine.Title.Text = "Ligne du poids"
            WeightLine.Title.TextColor = Color.Red
            WeightLine.Title.Antialiasing = False
            WeightLine.Title.Font = New Font("Tahoma", 10, FontStyle.Regular)
            WeightLine.Title.ShowBelowLine = False

            '' Customize the appearance of the constant line. 
            WeightLine.Color = Color.Red
            WeightLine.LineStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            WeightLine.LineStyle.Thickness = 2

            chartForce.Diagram.Assign(diagram) '

            ' Add the series to the chart. 
            chartForce.Series("Force").Assign(serieForce)

            '/ Fin : affichage DevExpress
            If lstTests.SelectedItem.ToString = "ISO" Then
                lblTypeCourbe.Visible = True
                cmbxTypeCourbe.Visible = True
                cmbxTypeCourbe.SelectedIndex = 0
            Else
                lblTypeCourbe.Visible = False
                cmbxTypeCourbe.Visible = False

            End If
            btnMesure.Enabled = False
            btnSauvegarder.Enabled = True
            btnSupprimer.Enabled = True

            grBxPrevisualisation.Visible = True
        Catch ex As Exception
            MsgBox("Problème d'acquisition" + vbCrLf + ex.Message, MsgBoxStyle.OkOnly, sender.text)
        End Try
    End Sub

    Private Sub frmPrincipale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        calibVide = False
        calibTare = False
        btnDeselectionner.Tag = False

        '
        initDoc = New Xml.XmlDocument 'initDoc est une variable globale de type XmlDocument

        nomficInit = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Init_Kinesis")
        nomficInit = nomficInit + "\Kinesis_init.xml"
        MsgBox(nomficInit)

        Charge_Init(nomficInit) 'cette routine va charger deux variables nomficConfig et repTravail

        '------------ si les dossiers et fichiers n'existent pas -----------------------
        If Not File.Exists(nomficConfig) Then 'vérifie si le fichier de configuration existe
            'si non, charge la config par défaut
            nomficConfig = Application.StartupPath + "\Config.xml"
            'MsgBox(nomficConfig)
        End If
        If Not Directory.Exists(repTravail) Then  'vérifie si le répertoire de travail existe
            'si non, définit le répertoire de travail comme étant le dossier MesDocuments
            repTravail = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            'MsgBox(nomficConfig)
        End If
        '--------------------------------------------------------------------


        doc = New Xml.XmlDocument 'pour stocker les éléments de configuration

        Dim cheminProvisoire As String

        '/ ici charger avec le fichier configuration 
        'cheminProvisoire = My.Computer.FileSystem.SpecialDirectories.Desktop 'Application.StartupPath
        cheminProvisoire = repTravail
        'cheminProvisoire = Application.StartupPath
        'nomficConfig = nomficConfig '"Config.xml" 'cheminProvisoire + "\Config.xml"
        Charge_Config(nomficConfig)
        MsgBox("Le fichier de configuration chargé est : " + nomficConfig, MsgBoxStyle.OkOnly, "Fichier de configuration chargé ")
        'frmIndConfig.Show()

        lblCalibration.Text = "Coeff A = " + CStr(coeffA) + " / CoeffB = " + CStr(coeffB) + vbCrLf + "Dernière calibration le " + lblDateModif.Text
        'appel à une procédure temporaire pour charger des données
        btnSauvegarder.Enabled = False
        cmbxBlessures.Items.Clear()
        For Each it In lstBlessures.Items
            cmbxBlessures.Items.Add(it.ToString)
        Next
       
        If Not (cheminProvisoire) Like "*Kinesis" And Not Directory.Exists(cheminProvisoire + "\Kinesis") Then
            'exécuté si le chemin provisoire ne comprends pas le dossier Kinesis ou s'il n'existe pas 
            'celà doit se produire lors de la première exécution

            Directory.CreateDirectory(cheminProvisoire + "\Kinesis")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\SJ")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\CMJ")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\FVcmj")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\ISO")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\K")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\RJ")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\RJ6sec")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\RESULTATS")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\LISTES_SUJETS")
            Directory.CreateDirectory(cheminProvisoire + "\Kinesis\CONFIGS")
            'Pour la suite pb : envoie une exception si le fichier existe déjà
            My.Computer.FileSystem.CopyFile("Config.xml", cheminProvisoire + "\Kinesis\CONFIGS\Config.xml")
            'Pour la suite pb : envoie une exception si le fichier existe déjà
            My.Computer.FileSystem.CopyFile("MySubjects.csv", cheminProvisoire + "\Kinesis\LISTES_SUJETS\MesSujets.csv", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
    Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            cheminProvisoire = cheminProvisoire + "\Kinesis"

            Ecrire_Init("FichierConfig", cheminProvisoire + "\Kinesis\CONFIGS\Config.xml")
        End If

        txtRepertoire.Text = cheminProvisoire
        Ecrire_Repertoire(True) 'attention important de faire dans cet ordre : txtRepertoire modifié, puis Ecrire_Repertoire
        Ecrire_Init("Repertoire", cheminProvisoire)

        seuilFz1 = numSeuilFz1.Value
        seuilFz2 = numSeuilFz2.Value
        lblOrdrePoly.ForeColor = Color.Black
        dernierRepertoire = txtRepertoire.Text
    End Sub

    Private Sub tabAcqui_Click(sender As Object, e As EventArgs) Handles tabAcqui.Click
        'appel à une procédure temporaire pour charger des données
        btnSauvegarder.Enabled = False
        btnSupprimer.Enabled = False
        cmbxBlessures.Items.Clear()
        For Each it In lstBlessures.Items
            cmbxBlessures.Items.Add(it.ToString)
        Next
        coeffA = CDbl(txtCoeffA_Config.Text)
        coeffB=CDbl(txtCoeffB_Config.Text)
        lblCalibration.Text = "Coeff A = " + CStr(coeffA) + " / CoeffB = " + CStr(coeffB) + vbCrLf + "Dernière calibration le " + lblDateModif.Text

    End Sub

    Private Sub btnAjoutSujets_Click(sender As Object, e As EventArgs) Handles btnAjoutSujets.Click
        Dim itListe As New ListViewItem
        'Dim i As Integer
        For Each it In lstSujets.Items
            lstSujets.Items.Remove(it)
        Next
        ' Dim nomFichierSujets As String
        OpenFileDialog1.InitialDirectory = txtRepertoire.Text + "\LISTES_SUJETS"
        OpenFileDialog1.Filter = "Fichier sujets (*.csv)|.csv"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.DefaultExt = ".csv"
        OpenFileDialog1.FileName = "*.csv"

        Dim dr As DialogResult
        dr = OpenFileDialog1.ShowDialog()
        If dr = DialogResult.Cancel Then
            MsgBox("Vous n'avez pas sélectionné un fichier de sujets")
            Exit Sub
        End If
        nomFichierSujets = OpenFileDialog1.FileName

        'Dim path As String = "C:\Users\Dalleau\Desktop\MySubjects.csv"
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(nomFichierSujets)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(";")
            Dim currentRow As String()
            currentRow = MyReader.ReadFields() 'lit la première ligne pour rien
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()

                    Dim sujet As ListViewItem
                    Dim nbTests As nbEssaiParTest

                    sujet = lstSujets.Items.Add(currentRow(0))
                    sujet.SubItems.Add(currentRow(1))
                    sujet.SubItems.Add(currentRow(2))
                    sujet.SubItems.Add(currentRow(3))
                    sujet.SubItems.Add(currentRow(4))
                    sujet.SubItems.Add(currentRow(5))
                    sujet.SubItems.Add(currentRow(6))
                    sujet.SubItems.Add(currentRow(7))

                    nbTests.nbCMJ = 0
                    nbTests.nbISO_2J = 0
                    nbTests.nbISO_D = 0
                    nbTests.nbISO_G = 0
                    nbTests.nbK = 0
                    nbTests.nbRJ = 0
                    nbTests.nbRJ6sec = 0

                    sujet.Tag = nbTests

                    grBxSujets.Visible = True
                    grbxAcquisition.Visible = True

                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Private Sub lstSujets_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstSujets.ColumnClick
        Me.lstSujets.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lstSujets_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstSujets.ItemCheck
        'Fait en sorte que si la ligne est déjà sélectionnée, la case à cocher reste cochée
        If lstSujets.Items(e.Index).Selected Then
            e.NewValue = CheckState.Checked
        End If
    End Sub

    Private Sub lstSujets_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lstSujets.ItemChecked
        If e.Item.Checked Then
            e.Item.Selected = True
        End If
    End Sub

    Private Sub lstSujets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSujets.SelectedIndexChanged
        Dim i As Integer
        Dim nbTest As nbEssaiParTest
        Try
            For i = 0 To lstSujets.Items.Count - 1
                If lstSujets.Items.Item(i).Selected = False Then
                    'on est sur l'item sélectionné
                    lstSujets.Items.Item(i).Checked = False
                Else
                    'on n'est pas sur l'item sélectionné
                    lstSujets.Items.Item(i).Checked = True
                    'lstSujets.Items.Item(i).BackColor = Color.CadetBlue
                    'Gestion de l'affichage du nombre d'essais pour un test donné
                    Select Case lstTests.SelectedIndex
                        Case 0 'SJ
                            nbTest = lstSujets.SelectedItems(0).Tag
                            lblNbEssais.Text = CStr(nbTest.nbSJ)
                        Case 1 'CMJ
                            nbTest = lstSujets.SelectedItems(0).Tag
                            lblNbEssais.Text = CStr(nbTest.nbCMJ)
                        Case 2 'K
                            nbTest = lstSujets.SelectedItems(0).Tag
                            lblNbEssais.Text = CStr(nbTest.nbK)
                        Case 3 'ISO
                            nbTest = lstSujets.SelectedItems(0).Tag

                            If rb2pieds.Checked Then
                                lblNbEssais.Text = CStr(nbTest.nbISO_2J)
                            ElseIf rbPiedD.Checked Then
                                lblNbEssais.Text = CStr(nbTest.nbISO_D)
                            ElseIf rbPiedG.Checked Then
                                lblNbEssais.Text = CStr(nbTest.nbISO_G)
                            End If


                            'lblNbEssais.Text = CStr(lstSujets.SelectedItems(0).Tag)
                        Case 4 'FVcmj'
                            nbTest = lstSujets.SelectedItems(0).Tag
                            Dim dernierFV As New fvCmj
                            If (nbTest.nbFVcmj Is Nothing) Then
                                'dernierFV = nbTest.nbFVcmj(nbTest.nbFVcmj.Count)
                                lblNbEssais.Text = CStr(0)
                                ' MsgBox("pas d'essai")
                            Else
                                lblNbEssais.Text = CStr(nbTest.nbFVcmj.Count) + " charges testées"""
                            End If

                        Case Else
                            lblNbEssais.Text = CStr(0)
                    End Select


                    If Not (lstSujets.Items.Item(i).SubItems(2).Text = "entrer la masse") Then
                        lblMasseMesuree.Text = lstSujets.Items.Item(i).SubItems(2).Text + " kg"
                    End If
                    If lstSujets.Items.Item(i).SubItems.Item(4).Text <> "" Then 'affichage des réglages de banc et de pied
                        ' si ces colonnes ne sont pas vides on configure les paramètres
                        numBanc.Value = CInt(lstSujets.Items.Item(i).SubItems.Item(3).Text)
                        numPieds.Value = CInt(lstSujets.Items.Item(i).SubItems.Item(4).Text)
                    End If
                    If lstSujets.Items.Item(i).SubItems.Item(4).Text <> "" Then 'affichage des réglages de angle1 et de angle2 
                        ' si ces colonnes ne sont pas vides on configure les paramètres
                        numAngle1.Value = CInt(lstSujets.Items.Item(i).SubItems.Item(5).Text)
                        numAngle2.Value = CInt(lstSujets.Items.Item(i).SubItems.Item(6).Text)
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox("Problème de sélection des sujets dans la liste - Acquisition : " + ex.Message, MsgBoxStyle.OkOnly, sender.text)
        End Try


    End Sub



    Private Sub btnSauvegarder_Click(sender As Object, e As EventArgs) Handles btnSauvegarder.Click

        'recherche de fichier
        Try
            Dim nomFichier As String
            Dim repertoire As String
            Dim essaiTmp As Essai

            'SaveFileDialog1.InitialDirectory = txtRepertoire.Text
            'SaveFileDialog1.FileName = lstSujets.SelectedItems(0).Text + ".txt"
            'SaveFileDialog1.ShowDialog()
            'nomFichier = SaveFileDialog1.FileName
            ''attention"
            ''ici tester la validité de la réponse
            '' apparemment ce n'est pas utile
            'If nomFichier = "" Then
            '    Exit Sub
            'End If
            'test si la masse est bien saisie '

            'REM_DG faire ici quelque chose de plus universel
            'REM_DG attention ici on ne vérifie pas si le fichier existe déjà ou pas.
            'Dim listItemTmp As ListViewItem
            'listItemTmp = lstSujets.SelectedItems(0)
            'listItemTmp.Tag = listItemTmp.Tag + 1

            'REM_GD le tag conserve le numéro d'essai pour le test concerné
            'partie très complexe; à simplifier si possible
            'REM_GD

            Dim strMode As String
            Dim moyPuis As Double
            Dim d1 As DateTime = DateTime.Now
            Dim strDateHeure As String
            strDateHeure = Format(Now(), "dd-MM-yyyy-hh-m-s")

            'MsgBox(CStr(d1.ToLongTimeString()))
            Select Case lstTests.SelectedItem.ToString
                Case "ISO"
                    repertoire = txtRepertoire.Text + "\ISO\"

                    Dim nbTest As nbEssaiParTest

                    If rb2pieds.Checked Then
                        nbTest = lstSujets.SelectedItems(0).Tag
                        nbTest.nbISO_2J = nbTest.nbISO_2J + 1
                        lstSujets.SelectedItems(0).Tag = nbTest
                        lblNbEssais.Text = Str(nbTest.nbISO_2J)
                        strMode = "_ISO_2J_"
                        nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + CStr(nbTest.nbISO_2J) + ".txt"
                        ficdata.typeISO = "2J"
                    ElseIf rbPiedD.Checked Then
                        nbTest = lstSujets.SelectedItems(0).Tag
                        nbTest.nbISO_D = nbTest.nbISO_D + 1
                        lstSujets.SelectedItems(0).Tag = nbTest
                        lblNbEssais.Text = Str(nbTest.nbISO_D)
                        strMode = "_ISO_D_"
                        nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + CStr(nbTest.nbISO_D) + ".txt"
                        ficdata.typeISO = "D"
                    ElseIf rbPiedG.Checked Then
                        nbTest = lstSujets.SelectedItems(0).Tag
                        nbTest.nbISO_G = nbTest.nbISO_G + 1
                        lstSujets.SelectedItems(0).Tag = nbTest
                        lblNbEssais.Text = Str(nbTest.nbISO_G)
                        strMode = "_ISO_G_"
                        'Etre sûr qu'au moins un mode est coché
                        nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + CStr(nbTest.nbISO_G) + ".txt"
                        ficdata.typeISO = "G"
                    End If

                Case "SJ"
                    repertoire = txtRepertoire.Text + "\SJ\"
                    'nomFichier = repertoire + lstSujets.SelectedItems(0).Text + "SJ.txt"
                    Dim nbTest As nbEssaiParTest
                    nbTest = lstSujets.SelectedItems(0).Tag
                    nbTest.nbSJ = nbTest.nbSJ + 1
                    lstSujets.SelectedItems(0).Tag = nbTest
                    lblNbEssais.Text = Str(nbTest.nbSJ)
                    strMode = "_SJ_"
                    nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + CStr(nbTest.nbSJ) + ".txt"

                Case "CMJ"
                    repertoire = txtRepertoire.Text + "\CMJ\"
                    'nomFichier = repertoire + lstSujets.SelectedItems(0).Text + "CMJ.txt"
                    Dim nbTest As nbEssaiParTest
                    nbTest = lstSujets.SelectedItems(0).Tag
                    nbTest.nbCMJ = nbTest.nbCMJ + 1
                    lstSujets.SelectedItems(0).Tag = nbTest
                    lblNbEssais.Text = Str(nbTest.nbCMJ)
                    strMode = "_CMJ_"
                    nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + CStr(nbTest.nbCMJ) + ".txt"

                Case "FVcmj"
                    repertoire = txtRepertoire.Text + "\FVcmj\"
                    'nomFichier = repertoire + lstSujets.SelectedItems(0).Text + "FVcmj.txt"
                    Dim nbTest As nbEssaiParTest
                    Dim chargeTestee As String
                    Dim numEssaiTest As String

                    nbTest = lstSujets.SelectedItems(0).Tag
                    If IsNothing(nbTest.nbFVcmj) Then
                        Dim tCharge As fvCmj
                        tCharge.charge = CStr(numCharge.Value)
                        tCharge.nbessai = 1
                        'ReDim nbTest.nbFVcmj as new list(of fvCmj)
                        ReDim Preserve nbTest.nbFVcmj(0)
                        nbTest.nbFVcmj(0) = tCharge
                        chargeTestee = CStr(numCharge.Value)
                        numEssaiTest = 1
                    Else
                        Dim chargeTrouvee As Boolean
                        Dim k As Integer
                        k = 0
                        For Each tCharge As fvCmj In nbTest.nbFVcmj
                            chargeTrouvee = tCharge.charge.Equals(CStr(numCharge.Value))
                            If chargeTrouvee Then
                                tCharge.nbessai = tCharge.nbessai + 1
                                nbTest.nbFVcmj(k) = tCharge
                                'MsgBox("Found " & CStr(nbTest.nbFVcmj(k).nbessai))
                                chargeTestee = CStr(numCharge.Value)
                                numEssaiTest = tCharge.nbessai
                                Exit For
                            End If
                            k = k + 1
                        Next
                        If Not chargeTrouvee Then
                            ReDim Preserve nbTest.nbFVcmj(nbTest.nbFVcmj.Count)
                            Dim tCharge As fvCmj
                            tCharge.charge = CStr(numCharge.Value)
                            tCharge.nbessai = 1
                            nbTest.nbFVcmj(nbTest.nbFVcmj.Count - 1) = tCharge
                            chargeTestee = CStr(numCharge.Value)
                            numEssaiTest = tCharge.nbessai
                            'MsgBox("Not Found " & CStr(nbTest.nbFVcmj(nbTest.nbFVcmj.Count - 1).nbessai))
                        End If
                    End If

                    lstSujets.SelectedItems(0).Tag = nbTest
                    lblNbEssais.Text = Str(numEssaiTest) + " à la charge de " + chargeTestee + " kg" 'REM_GD attention ici à vérifier
                    strMode = "_FVcmj_" + chargeTestee + "_"
                    If chkSaut.Checked Then
                        strMode = strMode + "_avec_"
                    Else
                        strMode = strMode + "_sans_"
                    End If


                    ' A trouver
                    nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + numEssaiTest + ".txt"

                Case "K"
                    repertoire = txtRepertoire.Text + "\K\"
                    'nomFichier = repertoire + lstSujets.SelectedItems(0).Text + "K.txt"
                    Dim nbTest As nbEssaiParTest
                    nbTest = lstSujets.SelectedItems(0).Tag
                    nbTest.nbK = nbTest.nbK + 1
                    lstSujets.SelectedItems(0).Tag = nbTest
                    lblNbEssais.Text = Str(nbTest.nbK)
                    strMode = "_K_"
                    nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + CStr(nbTest.nbK) + ".txt"

                Case "RJ"
                    repertoire = txtRepertoire.Text + "\RJ\"
                    'nomFichier = repertoire + lstSujets.SelectedItems(0).Text + "RJ.txt"
                    Dim nbTest As nbEssaiParTest
                    nbTest = lstSujets.SelectedItems(0).Tag
                    nbTest.nbRJ = nbTest.nbRJ + 1
                    lstSujets.SelectedItems(0).Tag = nbTest
                    lblNbEssais.Text = Str(nbTest.nbRJ)
                    strMode = "_RJ_"
                    nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + CStr(nbTest.nbRJ) + ".txt"

                Case "RJ6sec"
                    'MsgBox(lstSujets.SelectedItems(0).SubItems(7).Text)


                    repertoire = txtRepertoire.Text + "\RJ6sec\"
                    'nomFichier = repertoire + lstSujets.SelectedItems(0).Text + "RJ.txt"
                    Dim nbTest As nbEssaiParTest
                    nbTest = lstSujets.SelectedItems(0).Tag
                    moyPuis = CDbl(lstSujets.SelectedItems(0).SubItems(7).Text) * nbTest.nbRJ6sec
                    nbTest.nbRJ6sec = nbTest.nbRJ6sec + 1
                    lstSujets.SelectedItems(0).Tag = nbTest
                    lblNbEssais.Text = Str(nbTest.nbRJ6sec)
                    'MsgBox(lblNbEssais.Text)
                    strMode = "_RJ6sec_"
                    nomFichier = repertoire + lstSujets.SelectedItems(0).Text + strMode + strDateHeure + "_" + CStr(nbTest.nbRJ6sec) + ".txt"
            End Select

            'on complète le fichierdonnees
            ficdata.nomSujet = lstSujets.SelectedItems(0).SubItems(0).Text
            ficdata.masse = CDbl(lstSujets.SelectedItems(0).SubItems(2).Text)
            ficdata.typetest = lstTests.SelectedItem.ToString
            If chkBlesse.Checked Then
                ficdata.nom_blessure = cmbxBlessures.SelectedItem.ToString
            Else
                ficdata.nom_blessure = "neant"
            End If

            ficdata.fs = numFreq.Value
            ficdata.sig = signal
            ficdata.nbpoints = numFreq.Value * numDuree.Value
            ficdata.typecourbe = cmbxTypeCourbe.SelectedIndex + 1
            ficdata.pied = numPieds.Value

            ficdata.banc = numBanc.Value
            ficdata.charge = numCharge.Value
            ficdata.reglage1 = numAngle1.Value
            ficdata.reglage2 = numAngle2.Value
            If chkCouche.Checked Then
                ficdata.position = "Couche"
            Else
                ficdata.position = "Debout"
            End If

            If chkSaut.Checked Then
                ficdata.saut_realise = "Avec saut"
            Else
                ficdata.saut_realise = "Sans saut"
            End If
            
            If lstTests.SelectedItem.ToString = "RJ6sec" Then
                '##### simulation #####
                'Dim pstring As String
                'pstring = InputBox("valeur de P à RJ6")

                ''mettre la ligne précédente
                'Dim pTmp As Double
                'pTmp = CDbl(pstring)
                '#####  fin de la simulation ######
                'Dim ftmp As String = "C:\Users\Dalleau\Desktop\LEFM\RJ6sec\Etalon_RJ6sec_20130905_1.txt"
                'Dim pTmp As Double
                'LireFichier(ficdata, ftmp) 'simulation d'une acquisition 
                'initialise_Essai(ficdata, essaiTmp)
                'initialise_Essai_Test(essaiTmp, "RJ")

                'CalculsRJ(essaiTmp)
                ''essaiTmp.indices(4).valeur = InputBox("valeur de P à RJ6")
                'pTmp = CDbl(essaiTmp.indices(4).valeur)

                '############### A TESTER à la place de la simulation  ####################
                Dim pTmp As Double
                initialise_Essai(ficdata, essaiTmp)
                initialise_Essai_Test(essaiTmp, "RJ")
                CalculsRJ(essaiTmp)
                pTmp = CDbl(essaiTmp.indices(4).valeur)

                '############### Fin A TESTER à la place de la simulation  ################

                If CDbl(lblNbEssais.Text) <> 0 Then
                    moyPuis = moyPuis + pTmp
                    moyPuis = moyPuis / (CDbl(lblNbEssais.Text)) 'nbre d'essais réalisé à RJ6sec
                End If

                lstSujets.SelectedItems(0).SubItems(7).Text = CStr(moyPuis)
               
            End If
            ' On calcule une moyenne des essais


            ficdata.puissRJ6sec = CDbl(lstSujets.SelectedItems(0).SubItems(7).Text)
            EcrireFichier(ficdata, nomFichier)
            btnMesure.Enabled = True
            btnSauvegarder.Enabled = False
            btnSupprimer.Enabled = False
        Catch ex As Exception
            MsgBox("Erreur de sauvegarde - Bouton Sauvegarder")
        End Try

    End Sub

    Private Sub btnRepertoire_Click(sender As Object, e As EventArgs) Handles btnRepertoire.Click

        FolderBrowserDialog1.ShowDialog()
        txtRepertoire.Text = FolderBrowserDialog1.SelectedPath

    End Sub

    Private Sub tabSortir_Click(sender As Object, e As EventArgs) Handles tabSortir.Click
        If lstSujets.Items.Count <> 0 Then
            'Propose la sauvegarde du fichier de sujets en sortant
            'Dim rep As MsgBoxResult
            'MODIF à la demande de Senges
            'Dim rep = MessageBox.Show("Voulez-vous sauvegarder la liste de vos sujets ?", "Sauvegarde de la liste", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            ' rep = MsgBox(rep)
            'If rep = DialogResult.Yes And lstSujets.Items.Count <> 0 Then
            Dim i As Integer
            Dim j As Integer
            ' Dim dr As DialogResult
            'Dim nomFichierSujets As String
            Dim tmpString As String
            Dim it As ListViewItem
            Try
                'SaveFileDialog1.InitialDirectory = txtRepertoire.Text + "\LISTES_SUJETS"
                'SaveFileDialog1.FilterIndex = 1
                'SaveFileDialog1.DefaultExt = ".csv"
                'SaveFileDialog1.FileName = "*.csv"
                'SaveFileDialog1.Filter = "Fichier sujets (*.csv)|.csv"

                'dr = SaveFileDialog1.ShowDialog()
                'If dr = DialogResult.Cancel Then
                '    MsgBox("Vous n'avez pas sauvegardé la lise des sujets")
                '    Exit Sub
                'End If
                'nomFichierSujets = SaveFileDialog1.FileName
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter(nomFichierSujets, False) 'si le fichier existe déjà, il est écrasé
                file.WriteLine("Nom;Prenom;Masse;Banc;Pied;Angle1;Angle2;PuissRJ6")
                For i = 0 To lstSujets.Items.Count - 1
                    it = lstSujets.Items(i)
                    tmpString = it.Text '
                    For j = 1 To it.SubItems.Count - 1
                        tmpString = tmpString + ";" + it.SubItems(j).Text
                    Next
                    file.WriteLine(tmpString)
                Next
                file.Close()

            Catch ex As Microsoft.VisualBasic.
                        FileIO.MalformedLineException
                MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
            End Try
            'End If
        End If

        End
    End Sub

    Private Sub btnAccueilAcquisition_Click(sender As Object, e As EventArgs) Handles btnAccueilAcquisition.Click
        tabAppli.SelectTab("tabAcqui")
    End Sub

    Private Sub tabAppli_Click(sender As Object, e As EventArgs) Handles tabAppli.Click
        Select tabAppli.SelectedTab.Name
            Case "tabSortir"
                If lstSujets.Items.Count <> 0 Then
                    'Propose la sauvegarde du fichier de sujets en sortant

                    'Dim rep = MessageBox.Show("Voulez-vous sauvegarder la liste de vos sujets ?", "Sauvegarde de la liste", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    ' rep = MsgBox(rep)
                    'If rep = DialogResult.Yes And lstSujets.Items.Count <> 0 Then
                    Dim i As Integer
                    Dim j As Integer
                    'Dim dr As DialogResult
                    'Dim nomFichierSujets As String
                    Dim tmpString As String
                    Dim it As ListViewItem
                    Try
                        'MODIF à la demande de Jean
                        'SaveFileDialog1.InitialDirectory = txtRepertoire.Text + "\LISTES_SUJETS"
                        'SaveFileDialog1.FilterIndex = 1
                        'SaveFileDialog1.DefaultExt = ".csv"
                        'SaveFileDialog1.FileName = "*.csv"
                        'SaveFileDialog1.Filter = "Fichier sujets (*.csv)|.csv"

                        'dr = SaveFileDialog1.ShowDialog()
                        'If dr = DialogResult.Cancel Then
                        '    MsgBox("Vous n'avez pas sauvegardé la lise des sujets")
                        '    Exit Sub
                        'End If
                        'nomFichierSujets = SaveFileDialog1.FileName
                        Dim file As System.IO.StreamWriter

                        file = My.Computer.FileSystem.OpenTextFileWriter(nomFichierSujets, False) 'si le fichier existe déjà, il est écrasé
                        file.WriteLine("Nom;Prenom;Masse;Banc;Pied;Angle1;Angle2;PuissRJ6")
                        For i = 0 To lstSujets.Items.Count - 1
                            it = lstSujets.Items(i)
                            tmpString = it.Text '
                            For j = 1 To it.SubItems.Count - 1
                                tmpString = tmpString + ";" + it.SubItems(j).Text
                            Next
                            file.WriteLine(tmpString)
                        Next
                        file.Close()

                    Catch ex As Microsoft.VisualBasic.
                                FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                    End Try
                    'End If
                End If


                End 'Ferme l'application
            Case "tabAcqui"
                'appel à une procédure temporaire pour charger des données
                btnSauvegarder.Enabled = False

                btnSupprimer.Enabled = False
                cmbxBlessures.Items.Clear()
                For Each it In lstBlessures.Items
                    cmbxBlessures.Items.Add(it.ToString)
                Next
                coeffA = CDbl(txtCoeffA_Config.Text)
                coeffB = CDbl(txtCoeffB_Config.Text)
                lblCalibration.Text = "Coeff A = " + CStr(coeffA) + " / Coeff B = " + CStr(coeffB) + vbCrLf + "Dernière calibration le " + lblDateModif.Text
            Case "tabAnalyse"
                'ancienne version 24/07/2015 chrtPrincipal.Series.Clear()
            Case Else

        End Select
    End Sub





    Private Sub btnAvide_Click(sender As Object, e As EventArgs) Handles btnAvide.Click
        'Mesure à vide

        Dim frequence As Integer
        Dim duree As Integer

        'btnCalibrer.Enabled = False

        Try
            frequence = 1000
            duree = 3
            '/ Mesure : Acquisition
            Dim nbPoints As Double
            nbPoints = duree * frequence 'l'aquisition se fait sur 3 secondes
            Dim donnees(nbPoints) As Double
            Acquisition(donnees, txtNomCarte.Text + "/" + txtNomVoie.Text, frequence, -10, 10, duree)
            calibVide = True
            TensionMoyAvide = NationalInstruments.Analysis.Math.Statistics.Mean(donnees)

            If calibVide And calibTare Then
                calibVide = False
                calibTare = False
                coeffA = numTare.Value * 9.81 / (TensionMoyAvecTare - TensionMoyAvide)
                coeffB = -coeffA * TensionMoyAvide
                coeffA = Math.Round(coeffA, 3)
                coeffB = Math.Round(coeffB, 3)

                Dim msgRep As MsgBoxResult
                Dim msgRep1 As MsgBoxResult
                msgRep = MsgBox("Voulez-vous conserver cette calibration ?" + vbCrLf + " CoeffA = " + Format(coeffA, "###0.000") + vbCrLf + " CoeffB = " + Format(coeffB, "###0.000") + "", MsgBoxStyle.YesNo, "Calibration")

                If msgRep = MsgBoxResult.Yes Then   ' User chose Yes.
                    ' Perform some action.
                    txtCoeffA_Config.Text = CStr(coeffA)
                    txtCoeffB_Config.Text = CStr(coeffB)
                    Dim date_modif As Date
                    date_modif = Now()
                    lblDateModif.Text = CStr(date_modif)
                    Ecrire_Coefficients(config_chargee)
                    
                    ' mise à jour de la date
                    lblCalibration.Text = "Coeff A = " + Format(coeffA, "###0.000") + " / CoeffB = " + Format(coeffB, "###0.000") + vbCrLf + "Dernière calibration le " + lblDateModif.Text

                    msgRep1 = MsgBox("Voulez-vous enregistrer cette configuration ?", MsgBoxStyle.YesNo, "Configuration")
                    If msgRep1 = MsgBoxResult.Ok Then
                        Ecrire_Config(nomficConfig)
                    End If
                Else
                    ' Perform some other action.
                    ' REM_GD que faire ?
                End If

            End If
        Catch ex As Exception
            MsgBox("Problème de calibration à vide " + vbCrLf + ex.Message + vbCrLf, MsgBoxStyle.OkOnly, sender.text)
        End Try
    End Sub

    Private Sub btnTare_Click(sender As Object, e As EventArgs) Handles btnTare.Click

        'Mesure avec une tare 

        Dim frequence As Integer
        Dim duree As Integer

        Try
            frequence = 1000
            duree = 3
            '/ Mesure : Acquisition

            Dim nbPoints As Double
            nbPoints = duree * frequence 'l'aquisition se fait sur 2 secondes
            Dim donnees(nbPoints) As Double
            'Acquisition(donnees, txtNomCarte.Text + "/Ai0", frequence, -10, 10, duree)
            Acquisition(donnees, txtNomCarte.Text + "/" + txtNomVoie.Text, frequence, -10, 10, duree)
            'For i = 0 To donnees.Length - 1
            '    donnees(i) = donnees(i) + 100
            'Next
            calibTare = True
            TensionMoyAvecTare = NationalInstruments.Analysis.Math.Statistics.Mean(donnees)

            If calibVide And calibTare Then
                calibVide = False
                calibTare = False
                coeffA = numTare.Value * 9.81 / (TensionMoyAvecTare - TensionMoyAvide)
                coeffB = -coeffA * TensionMoyAvide
                coeffA = Math.Round(coeffA, 3)
                coeffB = Math.Round(coeffB, 3)

                Dim msgRep As MsgBoxResult
                Dim msgRep1 As MsgBoxResult

                msgRep = MsgBox("Calibration réussie ! " + vbCrLf + " CoeffA = " + Format(coeffA, "###0.000") + vbCrLf + " CoeffB = " + Format(coeffB, "###0.000") + "", MsgBoxStyle.YesNo, "Calibration")

                If msgRep = MsgBoxResult.Yes Then   ' User chose Yes.
                    ' Perform some action.
                    txtCoeffA_Config.Text = CStr(coeffA)
                    txtCoeffB_Config.Text = CStr(coeffB)
                    Dim date_modif As Date
                    date_modif = Now()
                    lblDateModif.Text = CStr(date_modif)
                    Ecrire_Coefficients(config_chargee)

                    ' mise à jour de la date
                    lblCalibration.Text = "Coeff A = " + Format(coeffA, "###0.000") + " / CoeffB = " + Format(coeffB, "###0.000") + vbCrLf + "Dernière calibration le " + lblDateModif.Text
                    msgRep1 = MsgBox("Voulez-vous enregistrer cette configuration ?", MsgBoxStyle.YesNo, "Configuration")
                    If msgRep1 = MsgBoxResult.Ok Then
                        Ecrire_Config(nomficConfig)
                    End If
                Else
                    ' Perform some other action.
                    ' REM_GD que faire ?
                End If


            End If
        Catch ex As Exception
            MsgBox("Problème de calibration avec une tare " + vbCrLf + ex.Message, MsgBoxStyle.OkOnly, sender.text)
        End Try


    End Sub

    Private Sub txtRepertoire_TextChanged(sender As Object, e As EventArgs) Handles txtRepertoire.TextChanged

    End Sub

    Private Sub btnChoix_Click(sender As Object, e As EventArgs) Handles btnChoix.Click
        'on reprend la culture francaise pour separateur de decimal=virgule
        'Thread.CurrentThread.CurrentCulture = New CultureInfo("fr-FR", True)

        Try
            OpenFileDialog1.InitialDirectory = dernierRepertoire
            OpenFileDialog1.Title = "Choisissez les fichiers à analyser, svp "
            OpenFileDialog1.Multiselect = True

            OpenFileDialog1.Filter = "Fichier d'acquisition (*.txt)|.txt"
            OpenFileDialog1.FilterIndex = 1
            OpenFileDialog1.DefaultExt = ".txt"
            OpenFileDialog1.FileName = "*.txt"
            'OpenFileDialog1.ShowDialog()
            Dim dr As DialogResult
            dr = OpenFileDialog1.ShowDialog()
            If dr = DialogResult.Cancel Then
                MsgBox("Vous n'avez pas sélectionné un fichier de sujets")
                Exit Sub
            End If
            Dim nbFichiers As Integer
            nbFichiers = OpenFileDialog1.FileNames.Count - 1

            ''REM_GD tester si la liste des fichiers n'est pas vide
            'ReDim ficDonnees(nbFichiers) 'As FichierDonnees
            'ReDim essaiFic(nbFichiers) 'As Essai

            Dim nomf As String
            'REM_GD pour n'afficher que les noms de fichier
            Dim folderPath As String
            Dim fileName As String
            Dim testFile As System.IO.FileInfo
            Dim i As Integer
            i = 0
            chkLstFiles1.Items.Clear()
            clstFichAnalyse.Items.Clear()

            ' ReDim lstNameFiles(OpenFileDialog1.FileNames.Count - 1)
            For Each nomf In OpenFileDialog1.FileNames
                'REM_GD pour n'afficher que les noms de fichier
                testFile = My.Computer.FileSystem.GetFileInfo(nomf)
                folderPath = testFile.DirectoryName
                fileName = testFile.Name
                'lstNameFiles(i) = nomf
                'MsgBox(lstNameFiles(i))
                chkLstFiles1.Items.Add(fileName)
                chkLstFiles1.SetItemChecked(i, True)

                clstFichAnalyse.Items.Add(nomf)
                clstFichAnalyse.SetItemChecked(i, True)
                'temp LireFichier(ficDonnees(i), nomf)
                'REM_GD je peux initialiser ici la fichier variable essaiFic
                'tmp initialise_Essai(ficDonnees(i), essaiFic(i))
                i += 1
            Next

            'REM_GD affiche le nom du sujet et sa masse
            ' ne prend en compte que le premier sujet
            Dim fileReader As System.IO.StreamReader
            fileReader = My.Computer.FileSystem.OpenTextFileReader(clstFichAnalyse.Items(0))
            Dim stringReader As String
            stringReader = fileReader.ReadLine()
            If stringReader = "#NEW#" Then
                stringReader = fileReader.ReadLine() 'nouveau format de fichier
                lblSujet.Text = stringReader
            Else
                lblSujet.Text = stringReader 'ancien format de fichier
            End If
            stringReader = fileReader.ReadLine()
            lblMasse.Text = stringReader
            fileReader.Close()

            btnAnalyse.Enabled = True
            dernierRepertoire = folderPath 'pour conserver le dernier répertoire analysé

            ' layoutPanel.Visible = False
            ' grbxSujet.Visible = False
            'grbxIndices.Visible = False

        Catch ex As Exception
            MsgBox("Problème de choix des fichiers : " + ex.Message, MsgBoxStyle.OkOnly, "Analyse")
        End Try


    End Sub

    Private Sub btnAnalyse_Click(sender As Object, e As EventArgs) Handles btnAnalyse.Click
        Dim it As String
        Dim i As Integer = 0
        Dim c() As String 'stocke pour chaque fichier son type
        ' Dim StartTime As Double, EndTime As Double, elapsed_time As Double
        'Dim startTime As Date
        'Dim runLength As Global.System.TimeSpan
        'Dim millisecs As Integer
        'startTime = Now
        Try
            newAnalyse = True
           
            'Dim stopWatch As New Stopwatch()
            'stopWatch.Start()

            
            'REM_GD
            'Dim timePerParse As Stopwatch

            If clstFichAnalyse.CheckedIndices.Count <> 0 Then
                ReDim c(clstFichAnalyse.CheckedIndices.Count - 1)
                ' *************  Vérification des fichiers sélectionnés ******************
                ' REM_GD 
                'Il faut une cohérence : tous les fichiers doivent être du même type
                ' On sort immediatement si deux types successifs ne sont pas identiques
                'REM_GD on peut aussi afficher les fichiers et leur type associé


                For Each it In clstFichAnalyse.CheckedItems
                    If clstFichAnalyse.GetItemText(it) <> Nothing Then
                        btnAnalyse.Enabled = True
                        btnChoix.Enabled = True
                        clstFichAnalyse.Enabled = True

                        If Strings.Right(clstFichAnalyse.GetItemText(it), 4) <> ".txt" Then
                            MsgBox("Vous devez choisir des fichiers d'extension '.txt' !", MsgBoxStyle.OkOnly, "Avertissement")
                            Exit Sub
                        End If

                        If InStr(1, clstFichAnalyse.GetItemText(it), "_CMJ_", CompareMethod.Text) Then
                            'choix = "CMJ"
                            c(i) = "CMJ"
                            If i > 0 Then
                                If c(i - 1) <> "CMJ" Then
                                    MsgBox("Vous devez choisir des fichiers issus d'un même type de test!!", MsgBoxStyle.OkOnly, "Avertissement")
                                    Exit Sub
                                End If
                            End If
                        ElseIf InStr(1, clstFichAnalyse.GetItemText(it), "_SJ_", CompareMethod.Text) Then
                            'choix = "SJ"
                            c(i) = "SJ"
                            If i > 0 Then
                                If c(i - 1) <> "SJ" Then
                                    MsgBox("Vous devez choisir des fichiers issus d'un même type de test!!", MsgBoxStyle.OkOnly, "Avertissement")
                                    Exit Sub
                                End If
                            End If
                        ElseIf InStr(1, clstFichAnalyse.GetItemText(it), "_K_", CompareMethod.Text) Then
                            'choix = "K"
                            c(i) = "K"
                            If i > 0 Then
                                If c(i - 1) <> "K" Then
                                    MsgBox("Vous devez choisir des fichiers issus d'un même type de test !", MsgBoxStyle.OkOnly, "Avertissement")
                                    Exit Sub
                                End If
                            End If
                        ElseIf InStr(1, clstFichAnalyse.GetItemText(it), "_ISO_", CompareMethod.Text) Then
                            'REM_GD ici, le label ISO est différent de l'ancienne version
                            'choix = "ISO"
                            c(i) = "ISO"
                            If i > 0 Then
                                If c(i - 1) <> "ISO" Then
                                    MsgBox("Vous devez choisir des fichiers issus d'un même type de test !", MsgBoxStyle.OkOnly, "Avertissement")
                                    Exit Sub
                                End If
                            End If
                        ElseIf InStr(1, clstFichAnalyse.GetItemText(it), "_RJ_", CompareMethod.Text) Then
                            'choix = "RJ"
                            c(i) = "RJ"
                            If i > 0 Then
                                If c(i - 1) <> "RJ" Then
                                    MsgBox("Vous devez choisir des fichiers issus d'un même type de test !", MsgBoxStyle.OkOnly, "Avertissement")
                                    Exit Sub
                                End If
                            End If
                        ElseIf InStr(1, clstFichAnalyse.GetItemText(it), "_RJ6sec_", CompareMethod.Text) Then
                            'choix = "RJ6sec"
                            c(i) = "RJ6sec"
                            If i > 0 Then
                                If c(i - 1) <> "RJ6sec" Then
                                    MsgBox("Vous devez choisir des fichiers issus d'un même type de test !", MsgBoxStyle.OkOnly, "Avertissement")
                                    Exit Sub
                                End If
                            End If
                        ElseIf InStr(1, clstFichAnalyse.GetItemText(it), "_FVcmj_", CompareMethod.Text) Then
                            'choix = "FVCMJ"
                            c(i) = "FVcmj"
                            If i > 0 Then
                                If c(i - 1) <> "FVcmj" Then
                                    MsgBox("Vous devez choisir des fichiers issus d'un même type de test!!", MsgBoxStyle.OkOnly, "Avertissement")
                                    Exit Sub
                                End If
                            End If
                        ElseIf InStr(1, clstFichAnalyse.GetItemText(it), "_FVsj_", CompareMethod.Text) Then
                            'choix = "FVSJ"
                            c(i) = "FVsj"
                            If i > 0 Then
                                If c(i - 1) <> "FVsj" Then
                                    MsgBox("Vous devez choisir des fichiers issus d'un même type de test!!", MsgBoxStyle.OkOnly, "Avertissement")
                                    Exit Sub
                                End If
                            End If
                        Else 'fichier sélectionné inconnu
                            MsgBox("Fichier de données non reconnu par l'application", MsgBoxStyle.OkOnly, "Analyse")
                        End If
                    Else
                        MsgBox("Pas de nom de fichier sélectionné", MsgBoxStyle.OkOnly, "Avertissement")
                        Exit Sub
                    End If
                    choix = c(i)
                    i += 1
                Next


            Else
                MsgBox("Choisissez un fichier à analyser avant de cliquer sur le bouton analyse!", MsgBoxStyle.OkOnly, "Avertissement")
                Exit Sub
            End If
            ' Windows.Forms.Cursor.Current = Cursors.WaitCursor

            ' *************  Analyse proprement dite des fichiers sélectionnés ******************
            ' Obtient la collection de contrôles du formulaire courant.
            ' Dim analyseControls As Control.ControlCollection = tabAnalyse.Controls
            'specifie l'emplacement d'origine pour les controles à créer
            'Dim location As New Point(10, groupeChoixAnalyse.Location.Y + groupeChoixAnalyse.Height + 10)

            '/// Les données sont remplies quand on a fini de sélectionner les fichiers cf 'btnChoix_Click' 
            
            ''/// TRES IMPORTANT     REM_GD Initialise ici les données
            'nbFic = clstFichAnalyse.CheckedItems.Count
            ReDim ficDonnees(clstFichAnalyse.CheckedItems.Count - 1) 'As FichierDonnees
            ReDim essaiFic(clstFichAnalyse.CheckedItems.Count - 1) 'As Essai
            For i = 0 To clstFichAnalyse.CheckedItems.Count - 1
                LireFichier(ficDonnees(i), clstFichAnalyse.CheckedItems(i).ToString)
                initialise_Essai(ficDonnees(i), essaiFic(i))
            Next

            'ancienne version 24/07/15  Dim diagram As XYDiagram = CType(chrtPrincipal.Diagram, XYDiagram)
            Dim diagram As XYDiagram = TryCast(chrtPrincipal.Diagram, XYDiagram)
            If diagram Is Nothing Then
                MsgBox("Erreur de conversion TryCast")
            Else
                diagram.Panes.Clear()
                diagram.SecondaryAxesY.Clear()
            End If

            'End If
            lstIndices.Clear()
            btnRJ.Visible = False

            'On ajoute 3 panneaux supplémentaires
            ' Ces panneaux sont ajoutés ici à tort car  ce n'est valable que pour CMJ et SJ
            diagram.Panes.Add(New XYDiagramPane("Vitesse"))
            diagram.Panes.Add(New XYDiagramPane("Position"))
            diagram.Panes.Add(New XYDiagramPane("Puissance"))

            Select Case choix
                '#################                 CMJ            ################################################
                Case "CMJ"
                    '*** Mesure du temps ****
                    'timePerParse = Stopwatch.StartNew()

                    Dim typeTest As String
                    typeTest = "CMJ"
                    chrtPrincipal.Visible = False
                    layoutPanel.BringToFront()
                    If chrtPrincipal.Series.Count <> 1 Then
                        chrtPrincipal.Series.Clear()
                        chrtPrincipal.Series.Add(New Series)
                        'chrtPrincipal.
                    End If

                    ''On ajoute 3 panneaux supplémentaires
                    'diagram.Panes.Add(New XYDiagramPane("Vitesse"))
                    'diagram.Panes.Add(New XYDiagramPane("Position"))
                    'diagram.Panes.Add(New XYDiagramPane("Puissance"))

                    '************** REM_GD la partie ci-dessous pourrait être associée à initialise _Essai_Test
                    'ou faire une fonction pour modifier les éléments du contrôle lstIndices.


                    lstIndices.Columns.Add("Essai")
                    lstIndices.Columns.Add("Test/Date")
                    lstIndices.Columns.Add("Fichier")
                    lstIndices.Columns.Add("Blessure")
                    lstIndices.Columns.Add("Tv")
                    lstIndices.Columns.Add("Puiss (W/kg)")
                    lstIndices.Columns.Add("Puiss max (W)")
                    lstIndices.Columns.Add("Zmax (cm)")
                    lstIndices.Columns.Add("He (cm)")

                    lstIndices.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(2).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(5).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(6).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(7).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(8).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(1).Width = 0 'Pour masquer la date
                    lstIndices.Columns(3).Width = 0 'pour masquer blessure
                    lstIndices.Columns(4).Width = 0 'pour masquer le Tv

                    '************** Fin REM_GD                
                    'Traitement de ce type de saut
                    Dim k As Integer
                    Dim nbFic As Integer 'correspond au nombre de fichiers ouverts
                    'nbFic = essaiFic.Count
                    nbFic = clstFichAnalyse.CheckedItems.Count

                    ' MsgBox("nb de fichiers " + CStr(nbFic))
                    For k = 0 To nbFic - 1
                        '
                        initialise_Essai_Test(essaiFic(k), typeTest)
                        'le traitement se fait ici
                        calculCMJSJ(essaiFic(k))
                    Next

                    Dim series_Signaux(nbFic * 4 - 1) As Series

                    For k = 0 To nbFic - 1
                        '/// création des series
                        'création de la serie pour la force d'indice k
                        series_Signaux(k) = New Series("Essai" + CStr(k + 1), ViewType.Line)
                        'création de la serie pour la vitesse d'indice k+n
                        series_Signaux(k + nbFic) = New Series("Vitesse" + CStr(k), ViewType.Line)
                        'création de la serie pour la position d'indice k+n
                        series_Signaux(k + 2 * nbFic) = New Series("Position" + CStr(k), ViewType.Line)
                        'création de la serie pour la puissance d'indice k+n
                        series_Signaux(k + 3 * nbFic) = New Series("Puissance" + CStr(k), ViewType.Line)

                        '////Ajout des points aux series
                        For i = 0 To essaiFic(k).fic.nbpoints - 1 Step 5 'pour sous échantilloner en divisant par 5
                            'ajout des points pour la serie de force
                            series_Signaux(k).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), Math.Round(essaiFic(k).Signaux.Force(i), 2)))
                            'ajout de points pour la serie de vitesse
                            series_Signaux(k + nbFic).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), Math.Round(essaiFic(k).Signaux.Vitesse(i), 2)))
                            'ajout de points pour la serie de position
                            series_Signaux(k + 2 * nbFic).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), Math.Round(essaiFic(k).Signaux.Position(i), 3)))
                            'ajout de points pour la serie de puissance
                            series_Signaux(k + 3 * nbFic).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), Math.Round(essaiFic(k).Signaux.Puissance(i), 2)))
                        Next


                        '///Ajout des series au "chart control"
                        'REM_GD ATTENTION les series sont ajoutées dans l'ordre des fichiers force, vitesse, position, puissance d'un même fichier
                        chrtPrincipal.Series.AddRange(New Series() {series_Signaux(k), series_Signaux(k + nbFic), series_Signaux(k + 2 * nbFic), series_Signaux(k + 3 * nbFic)})
                        series_Signaux(k).CheckedInLegend = True
                        series_Signaux(k + nbFic).CheckedInLegend = True
                        series_Signaux(k + 2 * nbFic).CheckedInLegend = True
                        series_Signaux(k + 3 * nbFic).CheckedInLegend = True
                        series_Signaux(k + nbFic).ShowInLegend = False
                        series_Signaux(k + 2 * nbFic).ShowInLegend = False
                        series_Signaux(k + 3 * nbFic).ShowInLegend = False
                    Next

                    chrtPrincipal.Series.RemoveAt(0)

                    Dim view As XYDiagramSeriesViewBase
                    '*********** Affichage de la force ****************************************
                    For k = 0 To nbFic - 1
                        'CType(series_Signaux(k).View, LineSeriesView).Pane = diagram.DefaultPane
                        'REM_GD ici on répète un peu pour rien cet affichage du titre
                        view = CType(series_Signaux(k).View, XYDiagramSeriesViewBase)
                        view.Pane = diagram.DefaultPane
                        view.Color = colors(k)

                    Next
                    view.AxisY.Title.Text = "Force (N)"
                    view.AxisY.Title.Alignment = StringAlignment.Center
                    view.AxisY.Title.Antialiasing = True
                    view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                    '*********** Affichage de la vitesse ****************************************
                    Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True

                    For k = 0 To nbFic - 1
                        ' ''Creation d'un axe secondaire pour la vitesse                        
                        view = CType(series_Signaux(k + nbFic).View, XYDiagramSeriesViewBase)
                        view.Pane = diagram.Panes(0)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "Vitesse (m/s)"
                        'view.AxisY.Title.Alignment = StringAlignment.Center
                        'view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True 'DevExpress.Utils.DefaultBoolean.True 'DevExpress.Utils.DefaultBoolean.True
                    Next



                    '************** Affichage de la position ****************************************
                    axesPosition = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k + 2 * nbFic).View, LineSeriesView).Pane = diagram.Panes(1)
                        ' ''Creation d'un axe secondaire pour la position
                        view = CType(series_Signaux(k + 2 * nbFic).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "Position (m)"
                        'view.AxisY.Title.Alignment = StringAlignment.Center
                        'view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    Next

                    '************** Affichage de la puissance ****************************************
                    axesPosition = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k + 3 * nbFic).View, LineSeriesView).Pane = diagram.Panes(2)
                        ' ''Creation d'un axe secondaire pour la puissance
                        view = CType(series_Signaux(k + 3 * nbFic).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "Puissance (W)"
                        'view.AxisY.Title.Alignment = StringAlignment.Center
                        'view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    Next
                    ' chrtPrincipal.Legend.UseCheckBoxes = True

                    '************** Affichage du temps sur l'axe des X  ****************************************
                    'tempo
                    'Dim panneau As DevExpress.XtraCharts.XYDiagramPaneBase
                    'diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True
                    'panneau = CType(diagram.DefaultPane, DevExpress.XtraCharts.XYDiagramDefaultPane)
                    'diagram.AxisX.SetVisibilityInPane(False, panneau)
                    'For i = 0 To diagram.Panes.Count - 2
                    '    panneau = CType(diagram.Panes(i), DevExpress.XtraCharts.XYDiagramPane)
                    '    diagram.AxisX.SetVisibilityInPane(False, panneau)
                    'Next i
                    'panneau = CType(diagram.Panes(diagram.Panes.Count - 1), DevExpress.XtraCharts.XYDiagramPane)
                    'diagram.AxisX.SetVisibilityInPane(True, panneau)
                    'tempo


                    ''on ne met dans la légende du graphe que les forces
                    'For Each serie In chrtPrincipal.Series
                    '    Dim seria As Series
                    '    seria = CType(serie, Series)
                    '    If seria.Name.First = "E" Then
                    '        seria.ShowInLegend = True
                    '    Else
                    '        seria.ShowInLegend = False
                    '    End If
                    'Next

                    chrtPrincipal.Visible = True

                    'REM_GD Ceci est un test
                    'REM_GD Ne faudrait il pas trier les fichiers ? 
                    'REM_GD Attention à la concordance des indices entre la légende et le tableau des indices

                    Dim aujour As Date = Date.Now
                    Dim aujourdhui As String = aujour.ToShortDateString

                    Dim j As Integer

                    j = 1
                    For Each it In clstFichAnalyse.CheckedItems 'Pour chaque fichier sélectionné
                        Dim trial As ListViewItem
                        trial = lstIndices.Items.Add(CStr(j)) 'on ajoute un numéro d'essai (un trial= un fichier)
                        trial.Checked = True
                        'REM_GD affiche -t-on la date du jour ou la date de la mesure
                        trial.SubItems.Add(essaiFic(j - 1).fic.datemesure)
                        'REM_GD idem ici il vaut mieux mettre une boucle
                        Dim fileName As String
                        Dim testFile As System.IO.FileInfo
                        testFile = My.Computer.FileSystem.GetFileInfo(it.ToString)
                        fileName = testFile.Name
                        trial.SubItems.Add(LSet(fileName, (fileName.Length - 4)))
                        trial.SubItems.Add(essaiFic(j - 1).fic.nom_blessure)
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(0).valeur, 3)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(1).valeur, 2)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(2).valeur, 2)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(3).valeur * 100, 1)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(4).valeur * 100, 1)))
                        j = j + 1
                    Next

                    Coherence_CMJ()
                    tabResultats.Visible = False
                    initializationTrial = False

                    'timePerParse.Stop()
                    'MsgBox("Temps écoulé / traitement CMJ :" + CStr(timePerParse.ElapsedTicks))
                    'on peut à présent exécuter le calcul de cohérence

                    '#############################                 SJ                    ################################################
                Case "SJ"
                    Dim typeTest As String
                    typeTest = "SJ"


                    chrtPrincipal.Visible = False
                    If chrtPrincipal.Series.Count <> 1 Then
                        chrtPrincipal.Series.Clear()
                        chrtPrincipal.Series.Add(New Series)
                    End If

                    layoutPanel.BringToFront()
                    'On ajoute 3 panneaux supplémentaires
                    'diagram.Panes.Add(New XYDiagramPane("Vitesse"))
                    'diagram.Panes.Add(New XYDiagramPane("Position"))
                    'diagram.Panes.Add(New XYDiagramPane("Puissance"))

                    '************** REM_GD la partie ci-dessous pourrait être associée à initialise _Essai_Test
                    'ou faire une fonction pour modifier les éléments du contrôle lstIndices.

                    lstIndices.Columns.Add("Essai")
                    lstIndices.Columns.Add("Test/Date")
                    lstIndices.Columns.Add("Fichier")
                    lstIndices.Columns.Add("Blessure")
                    lstIndices.Columns.Add("Tv")
                    lstIndices.Columns.Add("Puiss (W/kg)")
                    lstIndices.Columns.Add("Puiss max (W)")
                    lstIndices.Columns.Add("Zmax (m)")
                    lstIndices.Columns.Add("He (m)")

                    '************** Fin REM_GD                
                    'Traitement de ce type de saut
                    Dim k As Integer
                    Dim nbFic As Integer 'correspond au nombre de fichiers ouverts
                    nbFic = essaiFic.Count
                    'MsgBox("nb de fichiers " + CStr(nbFic))
                    For k = 0 To nbFic - 1
                        initialise_Essai_Test(essaiFic(k), typeTest)
                        'le traitement se fait ici
                        calculCMJSJ(essaiFic(k))
                    Next

                    'nombre de séries à créer
                    Dim series_Signaux(nbFic * 4 - 1) As Series

                    For k = 0 To nbFic - 1
                        '/// création des series
                        'création de la serie pour la force d'indice k
                        series_Signaux(k) = New Series("Essai" + CStr(k + 1), ViewType.Line)
                        'création de la serie pour la vitesse d'indice k+n
                        series_Signaux(k + nbFic) = New Series("Vitesse" + CStr(k), ViewType.Line)
                        'création de la serie pour la position d'indice k+n
                        series_Signaux(k + 2 * nbFic) = New Series("Position" + CStr(k), ViewType.Line)
                        'création de la serie pour la puissance d'indice k+n
                        series_Signaux(k + 3 * nbFic) = New Series("Puissance" + CStr(k), ViewType.Line)

                        '////Ajout des points aux series
                        For i = 0 To essaiFic(k).fic.nbpoints - 1
                            'ajout des points pour la serie de force
                            series_Signaux(k).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), Math.Round(essaiFic(k).Signaux.Force(i), 2)))
                            'ajout de points pour la serie de vitesse
                            series_Signaux(k + nbFic).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), Math.Round(essaiFic(k).Signaux.Vitesse(i), 2)))
                            'ajout de points pour la serie de position
                            series_Signaux(k + 2 * nbFic).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), Math.Round(essaiFic(k).Signaux.Position(i), 3)))
                            'ajout de points pour la serie de puissance
                            series_Signaux(k + 3 * nbFic).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), Math.Round(essaiFic(k).Signaux.Puissance(i), 2)))
                        Next

                        '///Ajout des series au "chart control"
                        'REM_GD ATTENTION les series sont ajoutées dans l'ordre des fichiers force, vitesse, position, puissance d'un même fichier
                        chrtPrincipal.Series.AddRange(New Series() {series_Signaux(k), series_Signaux(k + nbFic), series_Signaux(k + 2 * nbFic), series_Signaux(k + 3 * nbFic)})
                    Next

                    chrtPrincipal.Series.RemoveAt(0)


                    '*********** Affichage de la force ****************************************
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k).View, LineSeriesView).Pane = diagram.DefaultPane
                        'REM_GD ici on répète un peu pour rien cet affichage du titre
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY.Title.Text = "Force (N)"
                        view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    Next

                    '*********** Affichage de la vitesse ****************************************
                    Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k + nbFic).View, LineSeriesView).Pane = diagram.Panes(0)
                        ' ''Creation d'un axe secondaire pour la vitesse                        
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k + nbFic).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "Vitesse (m/s)"
                        view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    Next

                    '************** Affichage de la position ****************************************
                    axesPosition = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k + 2 * nbFic).View, LineSeriesView).Pane = diagram.Panes(1)
                        ' ''Creation d'un axe secondaire pour la position
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k + 2 * nbFic).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "Position (m)"
                        view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    Next

                    '************** Affichage de la puissance ****************************************
                    axesPosition = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k + 3 * nbFic).View, LineSeriesView).Pane = diagram.Panes(2)
                        ' ''Creation d'un axe secondaire pour la puissance
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k + 3 * nbFic).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "Puissance (W)"
                        view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    Next
                    'chrtPrincipal.Legend.UseCheckBoxes = True

                    '************** Affichage du temps sur l'axe des X  ****************************************
                    Dim panneau As DevExpress.XtraCharts.XYDiagramPaneBase
                    diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True
                    panneau = CType(diagram.DefaultPane, DevExpress.XtraCharts.XYDiagramDefaultPane)
                    diagram.AxisX.SetVisibilityInPane(False, panneau)
                    For i = 0 To diagram.Panes.Count - 2
                        panneau = CType(diagram.Panes(i), DevExpress.XtraCharts.XYDiagramPane)
                        diagram.AxisX.SetVisibilityInPane(False, panneau)
                    Next i
                    panneau = CType(diagram.Panes(diagram.Panes.Count - 1), DevExpress.XtraCharts.XYDiagramPane)

                    diagram.AxisX.SetVisibilityInPane(True, panneau)
                    'diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    diagram.AxisX.Title.Alignment = StringAlignment.Center
                    diagram.AxisX.Title.Text = "X-axis Title"
                    diagram.AxisX.Title.TextColor = Color.Red
                    diagram.AxisX.Title.Antialiasing = True
                    diagram.AxisX.Title.Font = New Font("Tahoma", 14, FontStyle.Bold)
                    diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                    'on ne met dans la légende du graphe que les forces
                    'For Each serie In chrtPrincipal.Series
                    '    Dim seria As Series
                    '    seria = CType(serie, Series)
                    '    If seria.Name.First = "E" Then
                    '        seria.ShowInLegend = True
                    '    Else
                    '        seria.ShowInLegend = False
                    '    End If
                    'Next
                    chrtPrincipal.Visible = True

                    'REM_GD Ceci est un test
                    'REM_GD Ne faudrait il pas trier les fichiers ? 
                    'REM_GD Attention à la concordance des indices entre la légende et le tableau des indices


                    Dim j As Integer
                    j = 1
                    For Each it In clstFichAnalyse.CheckedItems 'Pour chaque fichier sélectionné
                        Dim trial As ListViewItem
                        trial = lstIndices.Items.Add(CStr(j)) 'on ajoute un numéro d'essai (un trial= un fichier)
                        trial.Checked = True
                        'REM_GD affiche -t-on la date du jour ou la date de la mesure
                        trial.SubItems.Add(Format(Now, "dddd mm yyyy"))
                        'REM_GD idem ici il vaut mieux mettre une boucle
                        Dim fileName As String
                        Dim testFile As System.IO.FileInfo
                        testFile = My.Computer.FileSystem.GetFileInfo(it.ToString)
                        fileName = testFile.Name
                        trial.SubItems.Add(LSet(fileName, (fileName.Length - 4)))
                        trial.SubItems.Add(essaiFic(j - 1).fic.nom_blessure)
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(0).valeur, 3)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(1).valeur, 2)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(2).valeur, 2)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(3).valeur, 3)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(4).valeur, 3)))
                        j = j + 1
                    Next
                    tabResultats.Visible = False
                    initializationTrial = False

                    'on peut à présent exécuter le calcul de cohérence

                    '#################                 RJ                         ################################################

                Case "RJ", "RJ6sec"
                    Dim typeTest As String
                    typeTest = "RJ"
                    'chrtPrincipal.Visible = False
                    chrtPrincipal.Visible = False
                    If chrtPrincipal.Series.Count <> 1 Then
                        chrtPrincipal.Series.Clear()
                        chrtPrincipal.Series.Add(New Series)
                    End If

                    layoutPanel.BringToFront()
                    chrtPrincipal.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True

                    diagram.Panes(1).Visible = False
                    diagram.Panes(2).Visible = False
                    diagram.Panes(0).Name = "Puissance (W)"

                    'On ajoute 1 panneau supplémentaire
                    'diagram.Panes.Add(New XYDiagramPane("Puissance (W)"))

                    '************** REM_GD 
                    '           la partie ci-dessous pourrait être associée à initialise _Essai_Test
                    '        ou faire une fonction pour modifier les éléments du contrôle lstIndices.

                    lstIndices.Columns.Add("Essai")
                    lstIndices.Columns.Add("Test/Date")
                    lstIndices.Columns.Add("Fichier")

                    lstIndices.Columns.Add("Tv")
                    lstIndices.Columns.Add("Tv_moy")
                    lstIndices.Columns.Add("Tc")
                    lstIndices.Columns.Add("Tt")
                    lstIndices.Columns.Add("Puiss Bosco")
                    lstIndices.Columns.Add("Indice de confiance")
                    lstIndices.Columns.Add("pente hauteur")
                    lstIndices.Columns.Add("pente puissance")
                    lstIndices.Columns.Add("Zmax (cm)")
                    lstIndices.Columns.Add("Impulsion")
                    lstIndices.Columns.Add("Force Moy")
                    
                    'ancienne position de la pente hauteur
                    'ancienne position de la pente puissance

                    lstIndices.Columns.Add("Puissance 6 sec")

                    'ancienne position de l'indice de confiance
                    lstIndices.Columns.Add("Nb de sauts")
                    lstIndices.Columns.Add("Pmax/Pmin")
                    lstIndices.Columns.Add("Hmax/Hmin")
                    lstIndices.Columns.Add("Blessure")

                    lstIndices.Columns(1).Width = 0
                    lstIndices.Columns(2).Width = 0
                    lstIndices.Columns(3).Width = 0

                    lstIndices.Columns(4).Width = 0
                    lstIndices.Columns(5).Width = 0
                    lstIndices.Columns(6).Width = 0

                    lstIndices.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(7).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(8).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(9).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(10).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)

                    '************** Fin REM_GD 

                    'Traitement de ce type de saut
                    Dim k As Integer
                    Dim nbFic As Integer 'correspond au nombre de fichiers ouverts
                    nbFic = essaiFic.Count
                    'MsgBox("nb de fichiers " + CStr(nbFic))

                    For k = 0 To nbFic - 1
                        initialise_Essai_Test(essaiFic(k), typeTest)
                        '*** le traitement se fait ici ****
                        CalculsRJ(essaiFic(k))
                    Next

                    'nombre de séries à créer
                    Dim series_Signaux(nbFic * 4 - 1) As Series

                    ''////Ajout des points aux series
                    Dim point As New SeriesPoint
                    For k = 0 To nbFic - 1
                        '/// création des series
                        'création de la serie pour la hauteur
                        series_Signaux(k) = New Series("Essai" + CStr(k + 1), ViewType.Bar)
                        'création de la serie pour la puissance
                        series_Signaux(k + nbFic) = New Series("Puissance" + CStr(k + 1), ViewType.Bar)
                        series_Signaux(k + 2 * nbFic) = New Series("Hauteur_ajustée" + CStr(k + 1), ViewType.Line)
                        series_Signaux(k + 3 * nbFic) = New Series("Puissance_ajustée" + CStr(k + 1), ViewType.Line)
                        For i = 0 To essaiFic(k).nbPaquetsIndices - 1
                            '    'ajout des points pour la serie hauteur                          
                            series_Signaux(k).Points.Add(New SeriesPoint(i + 1, Math.Round(essaiFic(k).indices(5).tabValeurs(i) * 100, 3))) 'hauteur en m
                            '    'ajout de points pour la serie puissance
                            series_Signaux(k + nbFic).Points.Add(New SeriesPoint(i + 1, Math.Round(essaiFic(k).indices(4).tabValeurs(i), 2)))
                            series_Signaux(k + nbFic * 2).Points.Add(New SeriesPoint(i + 1, Math.Round(essaiFic(k).indices(8).tabValeurs(i) * 100, 3)))
                            series_Signaux(k + nbFic * 3).Points.Add(New SeriesPoint(i + 1, Math.Round(essaiFic(k).indices(9).tabValeurs(i), 2)))
                        Next
                        chrtPrincipal.Series.AddRange(New Series() {series_Signaux(k), series_Signaux(k + nbFic), series_Signaux(k + nbFic * 2), series_Signaux(k + nbFic * 3)})
                    Next

                    chrtPrincipal.Series.RemoveAt(0)

                    '*********** Affichage de la hauteur ****************************************
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k).View, BarSeriesView).Pane = diagram.DefaultPane
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY.Title.Text = "Hauteur (cm)"
                        view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                        CType(series_Signaux(k + nbFic * 2).View, LineSeriesView).Pane = diagram.DefaultPane
                        view = CType(series_Signaux(k + nbFic * 2).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)

                        ' '' Create a regression line for the Open value level.
                        'Dim myLine As New RegressionLine()
                        ' '' Access the series collection of indicators,
                        ' '' and add the regression line to it.
                        'CType(series_Signaux(k).View, BarSeriesView).Indicators.Add(myLine)
                        ' '' Customize the regression line's appearance.
                        ''myLine.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.DashDot
                        'myLine.LineStyle.Thickness = 2
                        'myLine.Color = CType(series_Signaux(k).View, BarSeriesView).Color 'Color.Crimson

                    Next

                    '*********** Affichage de la puissance ****************************************
                    Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k + nbFic).View, BarSeriesView).Pane = diagram.Panes(0)
                        ' ''Creation d'un axe secondaire pour la vitesse                        
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k + nbFic).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "Puissance (W/kg)"
                        'view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                        'CType(series_Signaux(k + nbFic).View, BarSeriesView).Color = CType(series_Signaux(k).View, BarSeriesView).Color
                        ' Create a regression line for the Open value level.

                        CType(series_Signaux(k + nbFic * 3).View, LineSeriesView).Pane = diagram.Panes(0)
                        view = CType(series_Signaux(k + nbFic * 3).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)

                        'Dim myLine As New RegressionLine()
                        '' Access the series collection of indicators,
                        '' and add the regression line to it.
                        'CType(series_Signaux(k + nbFic).View, BarSeriesView).Indicators.Add(myLine)
                        '' Customize the regression line's appearance.
                        'myLine.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.DashDot
                        'myLine.LineStyle.Thickness = 2
                        'myLine.Color = CType(series_Signaux(k + nbFic).View, BarSeriesView).Color 'Color.Crimson
                    Next

                    For Each serie In chrtPrincipal.Series
                        Dim seria As Series
                        seria = CType(serie, Series)
                        If seria.Name.First = "E" Then
                            seria.ShowInLegend = True
                        Else
                            seria.ShowInLegend = False
                        End If
                    Next
                    ' ******* ajout d'une annotation
                    ' Create a text annotation. 
                    Dim spH(nbFic) As SeriesPoint
                    Dim spP(nbFic) As SeriesPoint
                    Dim annotationH(nbFic) As TextAnnotation
                    Dim annotationP(nbFic) As TextAnnotation
                    Dim position As RelativePosition
                    For i = 0 To nbFic - 1 'Step 4
                        spH(i) = chrtPrincipal.Series(4 * i + 2).Points(2)
                        annotationH(i) = New TextAnnotation("AnnotationH" + CStr(i), "Essai" + CStr(i + 1) + " : Pente = " + CStr(Math.Round(essaiFic(i).indices(10).valeur * 100, 3)))
                        annotationH(i).AnchorPoint = New SeriesPointAnchorPoint(spH(i))
                        'CType(annotationH(i).AnchorPoint, PaneAnchorPoint).Pane = diagram.DefaultPane
                        'CType(annotationH(i).AnchorPoint, PaneAnchorPoint).AxisXCoordinate.AxisValue = 10
                        'CType(annotationH(i).AnchorPoint, PaneAnchorPoint).AxisYCoordinate.AxisValue = 100
                        ''CType(annotationH(i).ShapePosition, FreePosition).DockTarget = diagram.DefaultPane
                        ''CType(annotationH(i).ShapePosition, FreePosition).DockCorner = DockCorner.RightTop
                        annotationH(i).ShapePosition = New RelativePosition()
                        position = TryCast(annotationH(i).ShapePosition, RelativePosition)
                        position.ConnectorLength = 30
                        position.Angle = 270
                        
                        annotationH(i).ConnectorStyle = AnnotationConnectorStyle.Line

                        annotationH(i).RuntimeMoving = True
                        Me.chrtPrincipal.AnnotationRepository.Add(annotationH(i))

                        spP(i) = chrtPrincipal.Series(4 * i + 3).Points(2)
                        annotationP(i) = New TextAnnotation("AnnotationP" + CStr(i), "Essai" + CStr(i + 1) + " : Pente = " + CStr(Math.Round(essaiFic(i).indices(11).valeur, 3)))
                        annotationP(i).AnchorPoint = New SeriesPointAnchorPoint(spP(i))
                        annotationP(i).ShapePosition = New RelativePosition()
                        position = TryCast(annotationP(i).ShapePosition, RelativePosition)
                        position.ConnectorLength = 50
                        position.Angle = 270
                        'annotationP(i).LabelMode = True
                        'annotationP(i).TextColor = chrtPrincipal.Series(4 * i + 3).View.Color
                        'annotationP(i).BackColor = chrtPrincipal.Series(4 * i + 3).View.Color
                        annotationP(i).RuntimeMoving = True
                        Me.chrtPrincipal.AnnotationRepository.Add(annotationP(i))
                    Next

                    'sp(0) = chrtPrincipal.Series(2).Points(2)
                    'sp(1) = chrtPrincipal.Series(3).Points(4)
                    'annotations(0) = New TextAnnotation("Annotation0", "test0")
                    'annotations(1) = New TextAnnotation("Annotation1", "test1")
                    ''annotations(0).BackColor = chrtPrincipal.Series( 2).View.Color
                    ''annotations(1).BackColor = chrtPrincipal.Series( 3).View.Color
                    'annotations(0).AnchorPoint = New SeriesPointAnchorPoint(sp(0))                    
                    'annotations(0).ShapePosition = New RelativePosition()
                    'position = TryCast(annotations(0).ShapePosition, RelativePosition)
                    'position.ConnectorLength = 50
                    'position.Angle = 270
                    'annotations(1).AnchorPoint = New SeriesPointAnchorPoint(sp(1))                   
                    'annotations(1).ShapePosition = New RelativePosition()
                    'position = TryCast(annotations(1).ShapePosition, RelativePosition)
                    'position.ConnectorLength = 50
                    'position.Angle = 270
                    'annotations(0).RuntimeMoving = True
                    'annotations(1).RuntimeMoving = True
                    'Me.chrtPrincipal.AnnotationRepository.Add(annotations(0))
                    'Me.chrtPrincipal.AnnotationRepository.Add(annotations(1))
                    'Next

                    ' Change the text annotation font style to bold.  
                    'If annotation IsNot Nothing Then
                    '    annotation.Font = New Font(annotation.Font.FontFamily, annotation.Font.Size, FontStyle.Bold)
                    'End If

                    ' Specify the text annotation position. 


                    'annotations(0).RuntimeMoving = True

                    '' Add an annotaion to the annotation repository. 
                    'Me.chrtPrincipal.AnnotationRepository.Add(annotations(1))
                    '' Specify the text annotation position. 
                    'annotations(1).AnchorPoint = New SeriesPointAnchorPoint(sp(1))
                    'annotations(1).ShapePosition = New RelativePosition()
                    'position = TryCast(annotations(1).ShapePosition, RelativePosition)
                    'position.ConnectorLength = 50
                    'position.Angle = 270
                    'annotations(1).RuntimeMoving = True

                    '' Add an annotaion to the annotation repository. 
                    'Me.chrtPrincipal.AnnotationRepository.Add(annotations(2))
                    '' Specify the text annotation position. 
                    'annotations(2).AnchorPoint = New SeriesPointAnchorPoint(sp(2))
                    'annotations(2).ShapePosition = New RelativePosition()
                    'position = TryCast(annotations(2).ShapePosition, RelativePosition)
                    'position.ConnectorLength = 50
                    'position.Angle = 270
                    'annotations(2).RuntimeMoving = True

                    ' Add an annotaion to the annotation repository. 

                    chrtPrincipal.Visible = True
                    Dim j As Integer
                    j = 1
                    For Each it In clstFichAnalyse.CheckedItems 'Pour chaque fichier sélectionné
                        Dim trial As ListViewItem
                        trial = lstIndices.Items.Add(CStr(j)) 'on ajoute un numéro de l'essai 
                        trial.Checked = True
                        'REM_GD affiche -t-on la date du jour ou la date de la mesure
                        trial.SubItems.Add(essaiFic(j - 1).fic.datemesure) 'date de mesure
                        Dim fileName As String
                        Dim testFile As System.IO.FileInfo
                        testFile = My.Computer.FileSystem.GetFileInfo(it.ToString)
                        fileName = testFile.Name
                        trial.SubItems.Add(LSet(fileName, (fileName.Length - 4))) 'nom de fichier

                        'REM_GD idem ici il vaut mieux mettre une boucle
                        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(0).valeur)) 'temps de vol
                        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(1).valeur)) 'temps de vol moy
                        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(2).valeur)) 'temps de contact
                        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(3).valeur)) 'temps total
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(4).valeur, 2))) 'Puissance de Bosco
                        Dim nbSauts As Integer
                        nbSauts = essaiFic(j - 1).nbPaquetsIndices
                        Dim tmp_tab(4) As Double
                        If nbSauts > 4 Then
                            'moyenne sur les 5 premiers sauts de la puissance pour calculer l'indice de confiance
                            Dim moy5Sauts As Double

                            Dim l As Integer
                            For l = 0 To 4 'pb il faut qu'il y ait au moins 5 sauts
                                tmp_tab(l) = essaiFic(j - 1).indices(4).tabValeurs(l)
                            Next
                            moy5Sauts = Statistiques.Moyenne(tmp_tab)
                            If ficDonnees(j - 1).puissRJ6sec <> 0 Then
                                trial.SubItems.Add(CStr(Math.Round(moy5Sauts / ficDonnees(j - 1).puissRJ6sec, 3))) 'Indice de confiance
                            Else
                                trial.SubItems.Add("Puiss RJ6 nulle") 'Indice de confiance
                            End If
                        Else
                            trial.SubItems.Add("Nb sauts insuffisant")
                        End If
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(10).valeur * 100, 3))) 'pente de la hauteur 100 parce que la hauteur est affichée en cm
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(11).valeur, 3))) 'pente de la puissance
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(5).valeur * 100, 1))) 'Zmax (cm)
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(6).valeur, 2))) 'Impulsion
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(7).valeur, 2))) 'Force Moy
                        
                        trial.SubItems.Add(CStr(Math.Round(ficDonnees(j - 1).puissRJ6sec, 2))) 'puissance RJ 6 sec


                       


                        trial.SubItems.Add(CStr(nbSauts)) 'nombre de sauts
                        Dim Pmax As Double
                        Dim Pmin As Double
                        ReDim tmp_tab(nbSauts - 1)
                        For l = 0 To nbSauts - 1
                            tmp_tab(l) = essaiFic(j - 1).indices(4).tabValeurs(l)
                        Next
                        Pmax = Statistiques.Maximum(tmp_tab)
                        Pmin = Statistiques.Minimum(tmp_tab)
                        If Pmin <> 0 Then
                            trial.SubItems.Add(CStr(Math.Round(Pmax / Pmin, 2))) 'Pmax/Pmin
                        Else
                            trial.SubItems.Add("Puissance min = 0")
                        End If
                        Dim Hmax As Double
                        Dim Hmin As Double
                        For l = 0 To nbSauts - 1
                            tmp_tab(l) = essaiFic(j - 1).indices(5).tabValeurs(l)
                        Next
                        Hmax = Statistiques.Maximum(tmp_tab)
                        Hmin = Statistiques.Minimum(tmp_tab)
                        If Hmin <> 0 Then
                            trial.SubItems.Add(CStr(Math.Round(Hmax / Hmin, 2))) '"Hmax/Hmin
                        Else
                            trial.SubItems.Add("hauteur min = 0")
                        End If
                        trial.SubItems.Add(essaiFic(j - 1).fic.nom_blessure) 'type de blessure
                        j = j + 1
                    Next
                    btnRJ.Visible = True
                    Coherence_RJ()
                    tabResultats.Visible = False
                    initializationTrial = False

                    '########################################### RJ6sec #################################################################
                    'le 13/08/2015
                    'Case "RJ6sec"
                    '    Dim typeTest As String
                    '    typeTest = "RJ6sec"
                    '    'chrtPrincipal.Visible = False

                    '    chrtPrincipal.Visible = False
                    '    If chrtPrincipal.Series.Count <> 1 Then
                    '        chrtPrincipal.Series.Clear()
                    '        chrtPrincipal.Series.Add(New Series)
                    '    End If

                    '    layoutPanel.BringToFront()
                    '    chrtPrincipal.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False
                    '    'On ajoute 1 panneau supplémentaire
                    '    'diagram.Panes.Add(New XYDiagramPane("Puissance (W)"))

                    '    diagram.Panes(1).Visible = False
                    '    diagram.Panes(2).Visible = False
                    '    diagram.Panes(0).Name = "Puissance (W)"

                    '    '************** REM_GD 
                    '    '           la partie ci-dessous pourrait être associée à initialise _Essai_Test
                    '    '        ou faire une fonction pour modifier les éléments du contrôle lstIndices.

                    '    lstIndices.Columns.Add("Essai")
                    '    lstIndices.Columns.Add("Test/Date")
                    '    lstIndices.Columns.Add("Fichier")
                    '    lstIndices.Columns.Add("Blessure")
                    '    lstIndices.Columns.Add("Tv")
                    '    lstIndices.Columns.Add("Tv_moy")
                    '    lstIndices.Columns.Add("Tc")
                    '    lstIndices.Columns.Add("Tt")
                    '    lstIndices.Columns.Add("Puiss Bosco")
                    '    lstIndices.Columns.Add("Zmax (m)")
                    '    lstIndices.Columns.Add("Impulsion")
                    '    lstIndices.Columns.Add("Force Moy")
                    '    lstIndices.Columns.Add("pente hauteur")
                    '    lstIndices.Columns.Add("pente puissance")

                    '    '************** Fin REM_GD 

                    '    'Traitement de ce type de saut
                    '    Dim k As Integer
                    '    Dim nbFic As Integer 'correspond au nombre de fichiers ouverts
                    '    nbFic = essaiFic.Count
                    '    'MsgBox("nb de fichiers " + CStr(nbFic))

                    '    For k = 0 To nbFic - 1
                    '        initialise_Essai_Test(essaiFic(k), typeTest)
                    '        'le traitement se fait ici
                    '        CalculsRJ(essaiFic(k))
                    '    Next

                    '    'nombre de séries à créer
                    '    Dim series_Signaux(nbFic * 4 - 1) As Series

                    '    ''////Ajout des points aux series
                    '    Dim point As New SeriesPoint
                    '    For k = 0 To nbFic - 1
                    '        '/// création des series
                    '        'création de la serie pour la hauteur
                    '        series_Signaux(k) = New Series("Hauteur" + CStr(k + 1) + " (cm)", ViewType.Bar)
                    '        'création de la serie pour la puissance
                    '        series_Signaux(k + nbFic) = New Series("Puissance" + CStr(k + 1) + " (W)", ViewType.Bar)
                    '        series_Signaux(k + 2 * nbFic) = New Series("Hauteur_ajustée" + CStr(k + 1), ViewType.Line)
                    '        series_Signaux(k + 3 * nbFic) = New Series("Puissance_ajustée" + CStr(k + 1), ViewType.Line)
                    '        For i = 0 To essaiFic(k).nbPaquetsIndices - 1
                    '            '    'ajout des points pour la serie hauteur                          
                    '            series_Signaux(k).Points.Add(New SeriesPoint(i + 1, Math.Round(essaiFic(k).indices(5).tabValeurs(i + 1), 2)))
                    '            '    'ajout de points pour la serie puissance
                    '            series_Signaux(k + nbFic).Points.Add(New SeriesPoint(i + 1, Math.Round(essaiFic(k).indices(4).tabValeurs(i + 1), 2)))
                    '            series_Signaux(k + nbFic * 2).Points.Add(New SeriesPoint(i + 1, Math.Round(essaiFic(k).indices(8).tabValeurs(i + 1), 2)))
                    '            series_Signaux(k + nbFic * 3).Points.Add(New SeriesPoint(i + 1, Math.Round(essaiFic(k).indices(9).tabValeurs(i + 1), 2)))
                    '        Next
                    '        chrtPrincipal.Series.AddRange(New Series() {series_Signaux(k), series_Signaux(k + nbFic), series_Signaux(k + nbFic * 2), series_Signaux(k + nbFic * 3)})
                    '    Next

                    '    chrtPrincipal.Series.RemoveAt(0)

                    '    '*********** Affichage de la hauteur ****************************************
                    '    For k = 0 To nbFic - 1
                    '        CType(series_Signaux(k).View, BarSeriesView).Pane = diagram.DefaultPane
                    '        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k).View, XYDiagramSeriesViewBase)
                    '        view.Color = colors(k)
                    '        view.AxisY.Title.Text = "Hauteur (cm)"
                    '        view.AxisY.Title.Alignment = StringAlignment.Center
                    '        view.AxisY.Title.Antialiasing = True
                    '        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    '        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                    '        CType(series_Signaux(k + nbFic * 2).View, LineSeriesView).Pane = diagram.DefaultPane
                    '        view = CType(series_Signaux(k + nbFic * 2).View, XYDiagramSeriesViewBase)
                    '        'view.AxisY.Title.Text = "Hauteur (cm)"
                    '        'view.AxisY.Title.Alignment = StringAlignment.Center
                    '        'view.AxisY.Title.Antialiasing = True
                    '        'view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    '        'view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    '        '' Create a regression line for the Open value level.
                    '        'Dim myLine As New RegressionLine()
                    '        '' Access the series collection of indicators,
                    '        '' and add the regression line to it.
                    '        'CType(series_Signaux(k).View, BarSeriesView).Indicators.Add(myLine)
                    '        '' Customize the regression line's appearance.
                    '        'myLine.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.DashDot
                    '        'myLine.LineStyle.Thickness = 2
                    '        'myLine.Color = CType(series_Signaux(k).View, BarSeriesView).Color 'Color.Crimson

                    '    Next

                    '    '*********** Affichage de la puissance ****************************************
                    '    Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    '    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    '    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True
                    '    For k = 0 To nbFic - 1
                    '        CType(series_Signaux(k + nbFic).View, BarSeriesView).Pane = diagram.Panes(0)
                    '        ' ''Creation d'un axe secondaire pour la vitesse                        
                    '        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k + nbFic).View, XYDiagramSeriesViewBase)
                    '        view.Color = colors(k)
                    '        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                    '        view.AxisY.Title.Text = "Puissance (W)"
                    '        'view.AxisY.Title.Alignment = StringAlignment.Center
                    '        view.AxisY.Title.Antialiasing = True
                    '        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    '        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    '        'CType(series_Signaux(k + nbFic).View, BarSeriesView).Color = CType(series_Signaux(k).View, BarSeriesView).Color
                    '        ' Create a regression line for the Open value level.

                    '        CType(series_Signaux(k + nbFic * 3).View, LineSeriesView).Pane = diagram.Panes(0)
                    '        view.Color = colors(k)
                    '        view = CType(series_Signaux(k + nbFic * 3).View, XYDiagramSeriesViewBase)
                    '        view.AxisY = diagram.SecondaryAxesY(axesPosition)

                    '        'Dim myLine As New RegressionLine()
                    '        '' Access the series collection of indicators,
                    '        '' and add the regression line to it.
                    '        'CType(series_Signaux(k + nbFic).View, BarSeriesView).Indicators.Add(myLine)
                    '        '' Customize the regression line's appearance.
                    '        'myLine.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.DashDot
                    '        'myLine.LineStyle.Thickness = 2
                    '        'myLine.Color = CType(series_Signaux(k + nbFic).View, BarSeriesView).Color 'Color.Crimson
                    '    Next

                    '    chrtPrincipal.Visible = True
                    '    Dim j As Integer
                    '    j = 1
                    '    For Each it In clstFichAnalyse.CheckedItems 'Pour chaque fichier sélectionné
                    '        Dim trial As ListViewItem
                    '        trial = lstIndices.Items.Add(CStr(j)) 'on ajoute un numéro de fichier)
                    '        trial.Checked = True
                    '        'REM_GD affiche -t-on la date du jour ou la date de la mesure
                    '        trial.SubItems.Add(essaiFic(j - 1).fic.datemesure)
                    '        Dim fileName As String
                    '        Dim testFile As System.IO.FileInfo
                    '        testFile = My.Computer.FileSystem.GetFileInfo(it.ToString)
                    '        fileName = testFile.Name
                    '        trial.SubItems.Add(LSet(fileName, (fileName.Length - 4)))
                    '        trial.SubItems.Add(essaiFic(j - 1).fic.nom_blessure)
                    '        'REM_GD idem ici il vaut mieux mettre une boucle
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(0).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(1).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(2).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(3).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(4).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(5).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(6).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(7).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(10).valeur))
                    '        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(11).valeur))
                    '        j = j + 1
                    '    Next
                    '    tabResultats.Visible = False
                    '    initializationTrial = False

                    '#################                 FVcmj                         ################################################
                Case "FVcmj", "FVsj"
                    Dim typeTest As String
                    typeTest = "FVcmj"

                    chrtPrincipal.Visible = False
                    chrtFVcmj.Visible = False
                    flowLayoutPanel2.BringToFront()
                    'If chrtPrincipal.Series.Count <> 1 Then
                    '    chrtPrincipal.Series.Clear()
                    '    chrtPrincipal.Series.Add(New Series)
                    '    'chrtPrincipal.ToolTipEnabled = True
                    'End If
                    'chrtPrincipal.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False


                    ''On ajoute 1 panneau supplémentaire  

                    'diagram.Panes(1).Visible = False
                    'diagram.Panes(2).Visible = False
                    'diagram.Panes(0).Name = "Puissance"
                    ''diagram.Panes.Add(New XYDiagramPane("Puissance"))
                    diagram = CType(chrtFVcmj.Diagram, XYDiagram)

                    If chrtFVcmj.Series.Count <> 1 Then
                        chrtFVcmj.Series.Clear()
                        chrtFVcmj.Series.Add(New Series)
                        chrtFVcmj.ToolTipEnabled = True
                    End If
                    chrtFVcmj.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False

                    '************** REM_GD la partie ci-dessous pourrait être associée à initialise _Essai_Test
                    'ou faire une fonction pour modifier les éléments du contrôle lstIndices.

                    lstIndices.Columns.Add("Essai")
                    lstIndices.Columns.Add("Charge (kg)")
                    lstIndices.Columns.Add("Test/Date")
                    lstIndices.Columns.Add("Fichier")
                    lstIndices.Columns.Add("Blessure")
                    lstIndices.Columns.Add("Pmoy Spécifique  (W/kg)")
                    lstIndices.Columns.Add("Zmax (cm)")

                    lstIndices.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(5).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(6).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(2).Width = 0
                    lstIndices.Columns(3).Width = 0
                    lstIndices.Columns(4).Width = 0
                    '************** Fin REM_GD     
                    'Traitement de ce type de saut
                    Dim k As Integer
                    Dim nbFic As Integer 'correspond au nombre de fichiers ouverts
                    nbFic = essaiFic.Count
                    ' MsgBox("nb de fichiers " + CStr(nbFic))
                    For k = 0 To nbFic - 1
                        '
                        initialise_Essai_Test(essaiFic(k), typeTest)
                        'le traitement se fait ici
                        calculFVcmj(essaiFic(k))
                    Next

                    ReDim ForceMoy(nbFic - 1)
                    ReDim VitMoy(nbFic - 1)
                    ReDim PuissMoy(nbFic - 1)
                    ReDim ChargeMoy(nbFic - 1)

                    ReDim VitChecked(nbFic - 1)
                    ReDim ForceChecked(nbFic - 1)
                    ReDim PuisChecked(nbFic - 1)

                    ReDim ChargeChecked(nbFic - 1)

                    Dim j As Integer

                    For j = 0 To nbFic - 1
                        VitMoy(j) = essaiFic(j).indices(6).valeur
                        ForceMoy(j) = essaiFic(j).indices(7).valeur
                        PuissMoy(j) = essaiFic(j).indices(10).valeur '10 : puissance pic 12 : puiss moy 'changement pour Jean Senges
                        ChargeMoy(j) = essaiFic(j).fic.charge
                        VitChecked(j) = VitMoy(j)
                        ForceChecked(j) = ForceMoy(j)
                        PuisChecked(j) = PuissMoy(j)
                        ChargeChecked(j) = ChargeMoy(j)
                    Next
                    CheckedNumber = nbFic

                    'calcul des polynômes d'ajustement
                    Dim numSamples As Integer = VitMoy.Length

                    'ajustement force-vitesse
                    order = 1
                    fittedForce = AjustementPolynomial(VitMoy, ForceMoy, numSamples, order, coeff_Force, rSquare)

                    'ajustement Puissance-vitesse
                    order = numOrdreReg.Value
                    'NationalInstruments.Analysis.SignalGeneration.ArbitrarySignal.
                    fittedPuissance = AjustementPolynomial(VitMoy, PuissMoy, numSamples, order, coeff_Puissance, rSquare)
                    'création d'une courbe avec 100 points à partir de l'équation du polynôme
                    Dim valeurMax As Double = VitChecked.Max
                    Dim valeurMin As Double = VitChecked.Min
                    Dim pas As Double
                    pas = (valeurMax - valeurMin) / 100
                    ReDim fittedPuissance(100)
                    Dim axeVitesse(100) As Double
                    For i = 0 To 100
                        axeVitesse(i) = valeurMin + i * pas
                        fittedPuissance(i) = 0
                        For j = 0 To coeff_Puissance.Length - 1
                            fittedPuissance(i) = coeff_Puissance(j) * (axeVitesse(i) ^ j) + fittedPuissance(i)
                        Next j
                    Next i
                    'nombre de séries à créer
                    Dim series_Signaux(3) As Series
                    '/// création des series
                    'REM_GD il serait plus judicieux de mettre dans un autre ordre force avec force ...
                    'création de la serie pour la force
                    series_Signaux(0) = New Series("Force (N)", ViewType.Point)

                    'création de la serie pour la force ajustée
                    series_Signaux(1) = New Series("Force ajustée (N)", ViewType.Spline)
                    'création de la serie pour la puissance
                    series_Signaux(2) = New Series("Puissance (W)", ViewType.Point)
                    'création pour la puissance ajustée
                    series_Signaux(3) = New Series("Puis ajustée (W)", ViewType.Spline)

                    ''////Ajout des points aux series
                    Dim point As New SeriesPoint
                    For k = 0 To nbFic - 1
                        '    'ajout des points pour la serie hauteur 

                        series_Signaux(0).Points.Add(New SeriesPoint(essaiFic(k).indices(6).valeur, essaiFic(k).indices(7).valeur))
                        point = CType(series_Signaux(0).Points.Last, SeriesPoint)
                        point.Tag = essaiFic(k).fic.charge

                        '    'ajout de points pour la serie puissance
                        series_Signaux(1).Points.Add(New SeriesPoint(essaiFic(k).indices(6).valeur, fittedForce(k)))
                        series_Signaux(2).Points.Add(New SeriesPoint(essaiFic(k).indices(6).valeur, essaiFic(k).indices(10).valeur)) 'changement 10 au lieu de 12
                        point = CType(series_Signaux(2).Points.Last, SeriesPoint)
                        point.Tag = essaiFic(k).fic.charge
                        'series_Signaux(3).Points.Add(New SeriesPoint(essaiFic(k).indices(6).valeur, fittedPuissance(k)))

                    Next
                    For k = 0 To fittedPuissance.Length - 1
                        series_Signaux(3).Points.Add(New SeriesPoint(axeVitesse(k), fittedPuissance(k)))
                    Next



                    series_Signaux(0).LabelsVisibility = True

                    'series_Signaux(1).LabelsVisibility = True
                    series_Signaux(2).LabelsVisibility = True
                    'series_Signaux(3).LabelsVisibility = True

                    chrtFVcmj.Series.AddRange(New Series() {series_Signaux(0), series_Signaux(1), series_Signaux(2), series_Signaux(3)})
                    chrtFVcmj.Series.RemoveAt(0)
                    'chrtPrincipal.Series.AddRange(New Series() {series_Signaux(0), series_Signaux(1), series_Signaux(2), series_Signaux(3)})
                    'chrtPrincipal.Series.RemoveAt(0)

                    ''*********** Affichage de la force ****************************************
                    'For k = 0 To nbFic - 1
                    CType(series_Signaux(0).View, PointSeriesView).Pane = diagram.DefaultPane
                    Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(0).View, XYDiagramSeriesViewBase)
                    view.AxisY.Title.Text = "Force (N)"
                    view.AxisY.Title.Alignment = StringAlignment.Center
                    view.AxisY.Title.Antialiasing = True
                    view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                    CType(series_Signaux(1).View, LineSeriesView).Pane = diagram.DefaultPane
                    view = CType(series_Signaux(1).View, XYDiagramSeriesViewBase)
                    'Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(0).View, XYDiagramSeriesViewBase)
                    view.AxisY.Title.Text = "Force (N)"
                    view.AxisY.Title.Alignment = StringAlignment.Center
                    view.AxisY.Title.Antialiasing = True
                    view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True


                    ''*********** Affichage de la puissance ****************************************
                    Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True

                    CType(series_Signaux(2).View, PointSeriesView).Pane = diagram.Panes(0)
                    ' ''Creation d'un axe secondaire pour la vitesse                        
                    view = CType(series_Signaux(2).View, XYDiagramSeriesViewBase)
                    view.AxisY = diagram.SecondaryAxesY(axesPosition)
                    view.AxisY.Title.Text = "Puissance (W)"
                    'view.AxisY.Title.Alignment = StringAlignment.Center
                    view.AxisY.Title.Antialiasing = True
                    view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                    CType(series_Signaux(3).View, LineSeriesView).Pane = diagram.Panes(0)
                    view = CType(series_Signaux(3).View, XYDiagramSeriesViewBase)
                    'view.Color = colors(k)
                    view.AxisY = diagram.SecondaryAxesY(axesPosition)
                    view.AxisY.Title.Text = "Puissance (W)"
                    'view.AxisY.Title.Alignment = StringAlignment.Center
                    view.AxisY.Title.Antialiasing = True
                    view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                    chrtFVcmj.Visible = True

                    'Dim j As Integer
                    j = 1
                    For Each it In clstFichAnalyse.CheckedItems 'Pour chaque fichier sélectionné
                        Dim trial As ListViewItem
                        trial = lstIndices.Items.Add(CStr(j)) 'on ajoute un numéro de fichier)
                        trial.Checked = True
                        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(0).valeur))
                        'REM_GD affiche -t-on la date du jour ou la date de la mesure
                        trial.SubItems.Add(essaiFic(j - 1).fic.datemesure)
                        Dim fileName As String
                        Dim testFile As System.IO.FileInfo
                        testFile = My.Computer.FileSystem.GetFileInfo(it.ToString)
                        fileName = testFile.Name
                        trial.SubItems.Add(LSet(fileName, (fileName.Length - 4)))
                        trial.SubItems.Add(essaiFic(j - 1).fic.nom_blessure)
                        'REM_GD idem ici il vaut mieux mettre une boucle
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(13).valeur, 2)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(2).valeur * 100, 1)))


                        VitMoy(j - 1) = essaiFic(j - 1).indices(6).valeur
                        ForceMoy(j - 1) = essaiFic(j - 1).indices(7).valeur
                        PuissMoy(j - 1) = essaiFic(j - 1).indices(10).valeur '10 au lieu de 12 changement pour Senges
                        j = j + 1
                    Next

                    grbxOrdre.Visible = True
                    numOrdrePoly.Value = numOrdreReg.Value
                    Coherence_FVcmj(coeff_Force, coeff_Puissance, fittedPuissance, axeVitesse)
                    tabResultats.Visible = False
                    initializationTrial = False


                    '#################                 K                             ################################################
                Case "K"
                    Dim typeTest As String
                    typeTest = "K"
                    'chrtPrincipal.Visible = False
                    chrtPrincipal.Visible = False
                    layoutPanel.BringToFront()
                    If chrtPrincipal.Series.Count <> 1 Then
                        chrtPrincipal.Series.Clear()
                        chrtPrincipal.Series.Add(New Series)
                    End If



                    chrtPrincipal.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
                    'On ajoute 1 panneau supplémentaire
                    'diagram.Panes.Add(New XYDiagramPane("Puissance (W)"))
                    diagram.Panes(0).Visible = False
                    diagram.Panes(1).Visible = False
                    diagram.Panes(2).Visible = False
                    'diagram.Panes(0).Name = "Puissance (W)"
                    '************** REM_GD 
                    '           la partie ci-dessous pourrait être associée à initialise _Essai_Test
                    '        ou faire une fonction pour modifier les éléments du contrôle lstIndices.

                    lstIndices.Columns.Add("Essai")
                    lstIndices.Columns.Add("Test/Date")
                    lstIndices.Columns.Add("Fichier")
                    lstIndices.Columns.Add("Blessure")
                    lstIndices.Columns.Add("Puiss (W/kg)")
                    'lstIndices.Columns.Add("Raideur (N/m)")
                    lstIndices.Columns.Add("Raideur (N/m/kg)")
                    'lstIndices.Columns.Add("Zmax (m)")
                    'lstIndices.Columns.Add("Impulsion (N.s)")


                    '************** Fin REM_GD 

                    'Traitement de ce type de saut
                    Dim k As Integer
                    Dim nbFic As Integer 'correspond au nombre de fichiers ouverts
                    nbFic = essaiFic.Count
                    'MsgBox("nb de fichiers " + CStr(nbFic))

                    For k = 0 To nbFic - 1
                        initialise_Essai_Test(essaiFic(k), typeTest)
                        'le traitement se fait ici
                        CalculK(essaiFic(k))
                    Next

                    'nombre de séries à créer
                    Dim series_Signaux(nbFic * 2 - 1) As Series

                    ''////Ajout des points aux series
                    Dim point As New SeriesPoint
                    For k = 0 To nbFic - 1
                        '/// création des series
                        'création de la serie pour la hauteur
                        series_Signaux(k) = New Series("Essai " + CStr(k + 1), ViewType.Bar)
                        'création de la serie pour la puissance
                        series_Signaux(k + nbFic) = New Series("Puissance " + CStr(k + 1) + " (W/kg)", ViewType.Line)

                        For i = 0 To essaiFic(k).nbPaquetsIndices - 1
                            '    'ajout des points pour la serie Raideur                          
                            series_Signaux(k).Points.Add(New SeriesPoint(i + 1, essaiFic(k).indices(2).tabValeurs(i)))
                            '    'ajout de points pour la serie Puissance
                            series_Signaux(k + nbFic).Points.Add(New SeriesPoint(i + 1, essaiFic(k).indices(0).tabValeurs(i)))
                        Next
                        chrtPrincipal.Series.AddRange(New Series() {series_Signaux(k), series_Signaux(k + nbFic)})
                    Next

                    chrtPrincipal.Series.RemoveAt(0)

                    '*********** Affichage de la Raideur ****************************************
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k).View, BarSeriesView).Pane = diagram.DefaultPane
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY.Title.Text = "Raideur (N/m/kg)"
                        view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                    Next

                    '*********** Affichage de la puissance ****************************************
                    Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Far
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = False
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k + nbFic).View, LineSeriesView).Pane = diagram.DefaultPane 'anciennement .Panes(0)
                        ' ''Creation d'un axe secondaire pour la vitesse                        
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k + nbFic).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "Puissance (W/kg)"
                        'view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                    Next
                    For Each serie In chrtPrincipal.Series
                        Dim seria As Series
                        seria = CType(serie, Series)
                        If seria.Name.First = "E" Then
                            seria.ShowInLegend = True
                        Else
                            seria.ShowInLegend = False
                        End If
                    Next
                    chrtPrincipal.Visible = True
                    Dim j As Integer
                    j = 1
                    For Each it In clstFichAnalyse.CheckedItems 'Pour chaque fichier sélectionné
                        Dim trial As ListViewItem
                        trial = lstIndices.Items.Add(CStr(j)) 'on ajoute un numéro de fichier)
                        trial.Checked = True
                        'REM_GD affiche -t-on la date du jour ou la date de la mesure
                        trial.SubItems.Add(essaiFic(j - 1).fic.datemesure)
                        Dim fileName As String
                        Dim testFile As System.IO.FileInfo
                        testFile = My.Computer.FileSystem.GetFileInfo(it.ToString)
                        fileName = testFile.Name
                        trial.SubItems.Add(LSet(fileName, (fileName.Length - 4)))
                        trial.SubItems.Add(essaiFic(j - 1).fic.nom_blessure)
                        'REM_GD idem ici il vaut mieux mettre une boucle
                        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(0).valeur))
                        'trial.SubItems.Add(CStr(essaiFic(j - 1).indices(1).valeur))
                        trial.SubItems.Add(CStr(essaiFic(j - 1).indices(2).valeur))
                        'trial.SubItems.Add(CStr(essaiFic(j - 1).indices(3).valeur))
                        'trial.SubItems.Add(CStr(essaiFic(j - 1).indices(4).valeur))
                        j = j + 1
                    Next
                    Coherence_K()
                    tabResultats.Visible = False
                    initializationTrial = False


                    '######################          ISO                       ######################################

                Case "ISO"
                    Dim typeTest As String
                    typeTest = "ISO"
                    'chrtPrincipal.Visible = False

                    chrtPrincipal.Visible = False
                    layoutPanel.BringToFront()
                    If chrtPrincipal.Series.Count <> 1 Then
                        chrtPrincipal.Series.Clear()
                        chrtPrincipal.Series.Add(New Series)
                    End If



                    chrtPrincipal.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
                    'On ajoute 1 panneau supplémentaire
                    diagram.Panes(1).Visible = False
                    diagram.Panes(2).Visible = False
                    diagram.Panes(0).Name = "RFD"
                    'diagram.Panes.Add(New XYDiagramPane("RFD"))

                    '************** REM_GD 
                    '           la partie ci-dessous pourrait être associée à initialise _Essai_Test
                    '        ou faire une fonction pour modifier les éléments du contrôle lstIndices.

                    lstIndices.Columns.Add("Essai")
                    lstIndices.Columns.Add("Test/Date")
                    lstIndices.Columns.Add("Fichier")
                    lstIndices.Columns.Add("Typologie")
                    lstIndices.Columns.Add("Latéralité")
                    lstIndices.Columns.Add("Fmax (N)")
                    lstIndices.Columns.Add("RFD 150ms (N/s)")
                    lstIndices.Columns.Add("Tps Fmax (s)")
                    lstIndices.Columns.Add("Blessure")

                    lstIndices.Columns(1).Width = 0
                    lstIndices.Columns(2).Width = 0
                    lstIndices.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(3).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(4).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(5).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(6).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                    lstIndices.Columns(7).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)

                    'Traitement de ce type de mesure
                    Dim k As Integer
                    Dim nbFic As Integer 'correspond au nombre de fichiers ouverts
                    nbFic = essaiFic.Count
                    'MsgBox("nb de fichiers " + CStr(nbFic))

                    For k = 0 To nbFic - 1
                        initialise_Essai_Test(essaiFic(k), typeTest)
                        'le traitement se fait ici
                        CalculIso(essaiFic(k))
                    Next

                    'nombre de séries à créer
                    Dim series_Signaux(nbFic * 2 - 1) As Series

                    For k = 0 To nbFic - 1
                        '/// création des series
                        'création de la serie pour la force d'indice k
                        series_Signaux(k) = New Series("Essai" + CStr(k + 1), ViewType.Line)
                        'création de la serie pour la vitesse d'indice k+n
                        series_Signaux(k + nbFic) = New Series("RFD" + CStr(k + 1), ViewType.Line)


                        '////Ajout des points aux series
                        For i = 0 To essaiFic(k).fic.nbpoints - 1
                            'ajout des points pour la serie de force
                            series_Signaux(k).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), essaiFic(k).Signaux.Force(i)))
                            'ajout de points pour la serie de RFD
                            series_Signaux(k + nbFic).Points.Add(New SeriesPoint(essaiFic(k).Signaux.temps(i), essaiFic(k).Signaux.Vitesse(i)))
                        Next

                        '///Ajout des series au "chart control"
                        'REM_GD ATTENTION les series sont ajoutées dans l'ordre des fichiers force, vitesse, position, puissance d'un même fichier
                        chrtPrincipal.Series.AddRange(New Series() {series_Signaux(k), series_Signaux(k + nbFic)})
                    Next

                    chrtPrincipal.Series.RemoveAt(0)

                    '*********** Affichage de la force ****************************************
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k).View, LineSeriesView).Pane = diagram.DefaultPane
                        'REM_GD ici on répète un peu pour rien cet affichage du titre
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY.Title.Text = "Force (N)"
                        view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    Next

                    '*********** Affichage de la RFD ****************************************
                    Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                    diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                    diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True
                    For k = 0 To nbFic - 1
                        CType(series_Signaux(k + nbFic).View, LineSeriesView).Pane = diagram.Panes(0)
                        ' ''Creation d'un axe secondaire pour la vitesse                        
                        Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(k + nbFic).View, XYDiagramSeriesViewBase)
                        view.Color = colors(k)
                        view.AxisY = diagram.SecondaryAxesY(axesPosition)
                        view.AxisY.Title.Text = "RFD (N/s)"
                        view.AxisY.Title.Alignment = StringAlignment.Center
                        view.AxisY.Title.Antialiasing = True
                        view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                        view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                    Next
                    chrtPrincipal.Legend.UseCheckBoxes = True

                    'on ne met dans la légende du graphe que les forces
                    For Each serie In chrtPrincipal.Series
                        Dim seria As Series
                        seria = CType(serie, Series)
                        If seria.Name.First = "E" Then
                            seria.ShowInLegend = True
                        Else
                            seria.ShowInLegend = False
                        End If
                    Next

                    chrtPrincipal.Visible = True

                    Dim aujour As Date = Date.Now
                    Dim aujourdhui As String = aujour.ToShortDateString
                    Dim j As Integer
                    j = 1
                    For Each it In clstFichAnalyse.CheckedItems 'Pour chaque fichier sélectionné
                        Dim trial As ListViewItem
                        trial = lstIndices.Items.Add(CStr(j)) 'on ajoute un essai (un trial= un fichier)
                        trial.Checked = True
                        trial.SubItems.Add(essaiFic(j - 1).fic.datemesure)
                        Dim fileName As String
                        Dim testFile As System.IO.FileInfo
                        testFile = My.Computer.FileSystem.GetFileInfo(it.ToString)
                        fileName = testFile.Name
                        trial.SubItems.Add(LSet(fileName, (fileName.Length - 4)))
                        trial.SubItems.Add(essaiFic(j - 1).indices(3).valeur) 'type de courbe
                        trial.SubItems.Add(ficDonnees(j - 1).typeISO)

                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(0).valeur, 2)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(2).valeur, 3)))
                        trial.SubItems.Add(CStr(Math.Round(essaiFic(j - 1).indices(1).valeur, 2)))
                        trial.SubItems.Add(essaiFic(j - 1).fic.nom_blessure)

                        j = j + 1
                    Next
                    Coherence_ISO()
                    tabResultats.Visible = True
                    initializationTrial = False
                Case Else

            End Select
            ' layoutPanel.Visible = True
            ' grbxSujet.Visible = True
            'grbxIndices.Visible = True
            'Dim startTime As Date
            'Dim runLength As Global.System.TimeSpan
            'Dim millisecs As Integer
            'startTime = Now
            ' runLength.Subtract()
           
            'stopWatch.Stop()

            '' Get the elapsed time as a TimeSpan value. 
            'Dim ts As TimeSpan = stopWatch.Elapsed
            'Dim elapsedTime As String = CStr(ts.Minutes) + " : " + CStr(ts.Seconds) + " : " + CStr(ts.Milliseconds)
            'MsgBox(elapsedTime)

            newAnalyse = False
        Catch ex As Exception
            MsgBox("Problème Bouton d'analyse des fichiers  : " + ex.Message + "/ " + ex.ToString, MsgBoxStyle.OkOnly, "Analyse")
        End Try

        Windows.Forms.Cursor.Current = Cursors.Default

    End Sub

    Private Sub groupeChoixAnalyse_Enter(sender As Object, e As EventArgs) Handles groupeChoixAnalyse.Enter

    End Sub

    Private Sub btnAccueilAnalyse_Click(sender As Object, e As EventArgs) Handles btnAccueilAnalyse.Click
        tabAppli.SelectTab("tabAnalyse")
    End Sub

    Private Sub btnEffacer_Click(sender As Object, e As EventArgs) Handles btnEffacer.Click
        clstFichAnalyse.Items.Clear()
        btnChoix.Enabled = True
        'ancienne version 24/07/2015 chrtPrincipal.Series.Clear()
        lstIndices.Items.Clear()

    End Sub


    Private Sub tabAnalyse_Click(sender As Object, e As EventArgs) Handles tabAnalyse.Click
        'ancienne version 24/07/2015 chrtPrincipal.Series.Clear()
        grbxOrdre.Visible = False

        'chrtPrincipal.Legend.UseCheckBoxes = True

    End Sub
    Private Sub OnLegendItemChecked(ByVal sender As Object, ByVal e As LegendItemCheckedEventArgs)


        If initializationFlag = True Then
            Return
        End If
        initializationFlag = True
        ' MsgBox(e.CheckedElement.ToString)
        Dim checkedSeries As Series = TryCast(e.CheckedElement, Series)
        If checkedSeries Is Nothing Then
            Throw New Exception("Expected series only")
        End If
       
        Dim indice As String
        ' indice = Microsoft.VisualBasic.Strings.Right(checkedSeries.Name, 1)
        indice = Microsoft.VisualBasic.Strings.Right(e.CheckedElement.ToString, 1)
        Dim chrtTmp As ChartControl = TryCast(sender, ChartControl)

        Dim nbMesures As Integer
        nbMesures = chrtTmp.Series.Count
        'MsgBox(CStr(nbEssais))
        Dim nbPanes As Integer
        Dim diagramTmp As XYDiagram = CType(chrtTmp.Diagram, XYDiagram)
        'Dim lgnd As Legend = CType(chrtTmp.Legend, Legend)

        nbPanes = diagramTmp.Panes.Count + 1
        'MsgBox(CStr(nbPanes))
        Dim nbEssais As Integer
        nbEssais = nbMesures / nbPanes
        Dim indexLegend As Integer
        'MsgBox(CStr(saut))

        'REM_GD mettre ici le traitement des courbes plus performant
        Select Case choix
            Case "CMJ", "SJ", "ISO"

        End Select
        If choix = "CMJ" Then 'Or choix = "SJ" Or choix = "ISO" Then
            Dim nbVisibles As Integer
            nbVisibles = 0
            For Each serieTmp As Series In chrtTmp.Series
                If serieTmp.Name.First = "E" And serieTmp.CheckedInLegend Then
                    nbVisibles = nbVisibles + 1
                End If
            Next
            'MsgBox("reste visible : " + CStr(nbVisibles))
            If nbVisibles = 0 Then
                checkedSeries.CheckedInLegend = True
                '    initializationFlag = False
                '    Exit Sub
            Else

                indexLegend = (CInt(indice)) * nbPanes
                For k = indexLegend + 1 To indexLegend + nbPanes - 1
                    'Il ne faut pas rendre invisible la série du panneau principal sinon la légende disparaît
                    chrtTmp.Series.Item(k).Visible = checkedSeries.CheckedInLegend
                Next
            End If
        ElseIf choix = "K" Then
            Dim nbVisibles As Integer
            nbVisibles = 0
            For Each serieTmp As Series In chrtTmp.Series
                If serieTmp.Name.First = "E" And serieTmp.CheckedInLegend Then
                    nbVisibles = nbVisibles + 1
                End If
            Next
            'MsgBox("reste visible : " + CStr(nbVisibles))
            If nbVisibles = 0 Then
                checkedSeries.CheckedInLegend = True
            Else
                nbEssais = nbMesures / 2
                indexLegend = (CInt(indice) - 1) * 2
                'Il ne faut pas rendre invisible la série du panneau principal sinon la légende disparaît
                chrtTmp.Series.Item(indexLegend + 1).Visible = checkedSeries.CheckedInLegend
            End If

        ElseIf choix = "RJ" Then

            Dim nbVisibles As Integer
            nbVisibles = 0
            For Each serieTmp As Series In chrtTmp.Series
                If serieTmp.Name.First = "E" And serieTmp.CheckedInLegend Then
                    nbVisibles = nbVisibles + 1
                End If
            Next
            'MsgBox("reste visible : " + CStr(nbVisibles))
            If nbVisibles = 0 Then
                checkedSeries.CheckedInLegend = True
            Else
                nbEssais = nbMesures / 4
                indexLegend = (CInt(indice) - 1) * 4
                'Il ne faut pas rendre invisible la série du panneau principal sinon la légende disparaît
                chrtTmp.Series.Item(indexLegend + 1).Visible = checkedSeries.CheckedInLegend
                chrtTmp.Series.Item(indexLegend + 2).Visible = checkedSeries.CheckedInLegend
                chrtTmp.Series.Item(indexLegend + 3).Visible = checkedSeries.CheckedInLegend
            End If
        ElseIf choix = "FVcmj" Then

        End If


        initializationFlag = False
    End Sub
    Private Sub OnLegendItemChecked2(ByVal sender As Object, ByVal e As LegendItemCheckedEventArgs)
        'gère de de façon spécifique les cases à cocher de la légende


        If initializationFlag Or newAnalyse Then
            Return
        End If
        initializationFlag = True

        'MsgBox(e.CheckedElement.ToString)
        Dim checkedSeries As Series = TryCast(e.CheckedElement, Series)
        If checkedSeries Is Nothing Then
            Throw New Exception("Expected series only")
        End If

        Dim indice As String
        indice = Microsoft.VisualBasic.Strings.Right(checkedSeries.Name, 1)
        'indice = Microsoft.VisualBasic.Strings.Right(e.CheckedElement.ToString, 1)
        Dim chrtTmp As ChartControl = TryCast(sender, ChartControl)

        Dim nbMesures As Integer
        nbMesures = chrtTmp.Series.Count
        'MsgBox(CStr(nbEssais))
        'Dim nbPanes As Integer
        Dim diagramTmp As XYDiagram = CType(chrtTmp.Diagram, XYDiagram)
      
        'tempo nbPanes = diagramTmp.Panes.Count + 1
        'MsgBox(CStr(nbPanes))
        Dim nbEssais As Integer
        'tempo nbEssais = nbMesures / nbPanes
        Dim indexLegend As Integer
        'MsgBox(CStr(saut))

        'REM_GD mettre ici le traitement des courbes plus performant
        
        If choix = "CMJ" Then 'Or choix = "SJ" Or choix = "ISO" Then
            Dim nbVisibles As Integer
            nbVisibles = 0
            For Each serieTmp As Series In chrtTmp.Series
                If serieTmp.Name.First = "E" And serieTmp.CheckedInLegend Then
                    nbVisibles = nbVisibles + 1
                End If
            Next
            ' MsgBox("reste visible : " + CStr(nbVisibles))
            If nbVisibles = 0 Then
                checkedSeries.CheckedInLegend = True
                initializationFlag = False
                Exit Sub
            Else

                indexLegend = (CInt(indice - 1)) * 4 '4 = nombre de panneaux
                For k = indexLegend + 1 To indexLegend + 3
                    'Il ne faut pas rendre invisible la série du panneau principal sinon la légende disparaît
                    chrtTmp.Series.Item(k).Visible = checkedSeries.CheckedInLegend
                Next
            End If

        ElseIf choix = "ISO" Then
            Dim nbVisibles As Integer
            nbVisibles = 0
            For Each serieTmp As Series In chrtTmp.Series
                If serieTmp.Name.First = "E" And serieTmp.CheckedInLegend Then
                    nbVisibles = nbVisibles + 1
                End If
            Next
            'MsgBox("reste visible : " + CStr(nbVisibles))
            If nbVisibles = 0 Then
                checkedSeries.CheckedInLegend = True
                initializationFlag = False
                Exit Sub
            Else

                indexLegend = (CInt(indice - 1)) * 2
                'Il ne faut pas rendre invisible la série du panneau principal sinon la légende disparaît
                chrtTmp.Series.Item(indexLegend + 1).Visible = checkedSeries.CheckedInLegend

            End If
        ElseIf choix = "K" Then
            Dim nbVisibles As Integer
            nbVisibles = 0
            For Each serieTmp As Series In chrtTmp.Series
                If serieTmp.Name.First = "E" And serieTmp.CheckedInLegend Then
                    nbVisibles = nbVisibles + 1
                End If
            Next
            ' MsgBox("reste visible : " + CStr(nbVisibles))
            If nbVisibles = 0 Then
                checkedSeries.CheckedInLegend = True
            Else
                nbEssais = nbMesures / 2
                indexLegend = (CInt(indice) - 1) * 2
                'Il ne faut pas rendre invisible la série du panneau principal sinon la légende disparaît
                chrtTmp.Series.Item(indexLegend + 1).Visible = checkedSeries.CheckedInLegend
            End If

        ElseIf choix = "RJ" Then

            Dim nbVisibles As Integer
            nbVisibles = 0
            For Each serieTmp As Series In chrtTmp.Series
                If serieTmp.Name.First = "E" And serieTmp.CheckedInLegend Then
                    nbVisibles = nbVisibles + 1
                End If
            Next
            ' MsgBox("reste visible : " + CStr(nbVisibles))
            If nbVisibles = 0 Then
                checkedSeries.CheckedInLegend = True
            Else
                nbEssais = nbMesures / 4
                indexLegend = (CInt(indice) - 1) * 4
                'Il ne faut pas rendre invisible la série du panneau principal sinon la légende disparaît
                chrtTmp.Series.Item(indexLegend + 1).Visible = checkedSeries.CheckedInLegend
                chrtTmp.Series.Item(indexLegend + 2).Visible = checkedSeries.CheckedInLegend
                chrtTmp.Series.Item(indexLegend + 3).Visible = checkedSeries.CheckedInLegend
            End If
        ElseIf choix = "FVcmj" Then

        End If


        initializationFlag = False
    End Sub
    Private Sub tempo()
        If choix = "FVcmj" Then
            'If e.SeriesPoint.Values(0) > 1 Then
            'e.LabelText = e.SeriesPoint.Tag '"Critical value"
            'End If
        End If

    End Sub
    Private Sub lstIndices_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstIndices.ItemCheck
        Try
            If initializationTrial Then
                Return
            Else
                ''REM_GD mettre ici le traitement des courbes plus performant
                'Dim diagram As XYDiagram = CType(chrtPrincipal.Diagram, XYDiagram)
                ''chrtTmp.Series("Force" + CStr(e.Index)).CheckedInLegend = lstIndices.Items(e.Index).Checked
                'diagram.Series("Vitesse" + CStr(e.Index)).Visible = lstIndices.Items(e.Index).Checked
                'diagram.Series("Position" + CStr(e.Index)).Visible = lstIndices.Items(e.Index).Checked
                'diagram.Series("Puissance" + CStr(e.Index)).Visible = lstIndices.Items(e.Index).Checked
                If Not newAnalyse Then
                    ' Coherence(choix) ' MsgBox("Calcul de cohérence essai n° : " + CStr(e.Index + 1))                   
                End If

                'Dim diagram As XYDiagram = CType(chrtPrincipal.Diagram, XYDiagram)
                'Dim chrtTmp As ChartControl = CType(chrtPrincipal, ChartControl)
                'MsgBox(chrtTmp.Series(e.Index).Name)

                'If sender.Equals(lstIndices) Then
                '    chrtTmp.Series(0).CheckedInLegend = e.NewValue
                'End If

            End If
        Catch ex As Exception
            MsgBox("Problème de 'check' des essais dans la liste - ANALYSE : " + ex.Message, MsgBoxStyle.OkOnly, sender.text)
        End Try



    End Sub

    Private Sub lstIndices_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lstIndices.ItemChecked
        Try
            If initializationTrial Then
                Return
            Else



                If Not newAnalyse Then
                    If choix = "FVcmj" Then
                        If lstIndices.CheckedItems.Count > numOrdrePoly.Value Then

                            chrtFVcmj.Visible = False
                            If chrtFVcmj.Series.Count <> 1 Then

                                chrtFVcmj.Series.Clear()
                                chrtFVcmj.Series.Add(New Series)

                            End If
                            Dim diagram As XYDiagram = CType(chrtFVcmj.Diagram, XYDiagram)
                            'diagram.Panes.Clear()
                            diagram.SecondaryAxesY.Clear()

                            '    'On ajoute 1 panneau supplémentaire   

                            ' diagram.Panes.Add(New XYDiagramPane("Puissance"))
                            '    'calcul des polynômes d'ajustement
                            Dim numSamples As Integer = VitMoy.Length
                            ReDim VitChecked(numSamples)
                            ReDim ForceChecked(numSamples)
                            ReDim PuisChecked(numSamples)
                            'Dim nbFic As Integer
                            Dim k As Integer = 0
                            For i = 0 To numSamples - 1
                                If lstIndices.Items(i).Checked Then
                                    VitChecked(k) = VitMoy(i)
                                    ForceChecked(k) = ForceMoy(i)
                                    PuisChecked(k) = PuissMoy(i)
                                    ChargeChecked(k) = ChargeMoy(i)
                                    k = k + 1
                                End If
                            Next

                            'MsgBox(CStr(k))
                            ReDim Preserve VitChecked(k - 1)
                            ReDim Preserve ForceChecked(k - 1)
                            ReDim Preserve PuisChecked(k - 1)

                            CheckedNumber = k
                            '    'ajustement force-vitesse
                            order = 1
                            fittedForce = AjustementPolynomial(VitChecked, ForceChecked, CheckedNumber, order, coeff_Force, rSquare)

                            '    'ajustement Puissance-vitesse
                            order = numOrdrePoly.Value
                            fittedPuissance = AjustementPolynomial(VitChecked, PuisChecked, CheckedNumber, order, coeff_Puissance, rSquare)
                            'création d'une courbe avec 100 points à partir de l'équation du polynôme
                            Dim valeurMax As Double = VitChecked.Max
                            Dim valeurMin As Double = VitChecked.Min
                            Dim pas As Double
                            pas = (valeurMax - valeurMin) / 100
                            ReDim fittedPuissance(100)
                            Dim axeVitesse(100) As Double
                            For i = 0 To 100
                                axeVitesse(i) = valeurMin + i * pas
                                fittedPuissance(i) = 0
                                For j = 0 To coeff_Puissance.Length - 1
                                    fittedPuissance(i) = coeff_Puissance(j) * (axeVitesse(i) ^ j) + fittedPuissance(i)
                                Next j
                            Next i
                            'nombre de séries à créer
                            Dim series_Signaux(3) As Series

                            '/// création des series
                            'REM_GD il serait plus judicieux de mettre dans un autre ordre force avec force ...
                            'création de la serie pour la force
                            series_Signaux(0) = New Series("Force (N)", ViewType.Point)
                            'création de la serie pour la force ajustée
                            series_Signaux(1) = New Series("Force ajustée (N)", ViewType.Spline)
                            'création de la serie pour la puissance
                            series_Signaux(2) = New Series("Puissance (W)", ViewType.Point)
                            'création pour la puissance ajustée
                            series_Signaux(3) = New Series("Puis ajustée (W)", ViewType.Spline)

                            ''////Ajout des points aux series
                            Dim point As New SeriesPoint

                            For i = 0 To CheckedNumber - 1
                                series_Signaux(0).Points.Add(New SeriesPoint(VitChecked(i), ForceChecked(i)))
                                point = CType(series_Signaux(0).Points.Last, SeriesPoint)
                                point.Tag = ChargeChecked(i)
                                series_Signaux(1).Points.Add(New SeriesPoint(VitChecked(i), fittedForce(i)))
                                series_Signaux(2).Points.Add(New SeriesPoint(VitChecked(i), PuisChecked(i)))
                                point = CType(series_Signaux(2).Points.Last, SeriesPoint)
                                point.Tag = ChargeChecked(i)
                                'series_Signaux(3).Points.Add(New SeriesPoint(VitChecked(i), fittedPuissance(i)))
                            Next
                            For k = 0 To fittedPuissance.Length - 1
                                series_Signaux(3).Points.Add(New SeriesPoint(axeVitesse(k), fittedPuissance(k)))
                            Next
                            series_Signaux(0).LabelsVisibility = True
                            'series_Signaux(1).LabelsVisibility = True
                            series_Signaux(2).LabelsVisibility = True
                            'series_Signaux(3).LabelsVisibility = True

                            chrtFVcmj.Series.AddRange(New Series() {series_Signaux(0), series_Signaux(1), series_Signaux(2), series_Signaux(3)})
                            chrtFVcmj.Series.RemoveAt(0)
                            ''*********** Affichage de la force ****************************************

                            CType(series_Signaux(0).View, PointSeriesView).Pane = diagram.DefaultPane
                            Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(0).View, XYDiagramSeriesViewBase)
                            view.AxisY.Title.Text = "Force (N)"
                            view.AxisY.Title.Alignment = StringAlignment.Center
                            view.AxisY.Title.Antialiasing = True
                            view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                            view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                            CType(series_Signaux(1).View, LineSeriesView).Pane = diagram.DefaultPane
                            view = CType(series_Signaux(1).View, XYDiagramSeriesViewBase)
                            'Dim view As XYDiagramSeriesViewBase = CType(series_Signaux(0).View, XYDiagramSeriesViewBase)
                            view.AxisY.Title.Text = "Force (N)"
                            view.AxisY.Title.Alignment = StringAlignment.Center
                            view.AxisY.Title.Antialiasing = True
                            view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                            view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True


                            ''*********** Affichage de la puissance ****************************************
                            Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
                            diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
                            diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True

                            CType(series_Signaux(2).View, PointSeriesView).Pane = diagram.Panes(0)
                            ' ''Creation d'un axe secondaire pour la vitesse                        
                            view = CType(series_Signaux(2).View, XYDiagramSeriesViewBase)
                            view.AxisY = diagram.SecondaryAxesY(axesPosition)
                            view.AxisY.Title.Text = "Puissance (W)"
                            'view.AxisY.Title.Alignment = StringAlignment.Center
                            view.AxisY.Title.Antialiasing = True
                            view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                            view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                            CType(series_Signaux(3).View, LineSeriesView).Pane = diagram.Panes(0)
                            view = CType(series_Signaux(3).View, XYDiagramSeriesViewBase)
                            'view.Color = colors(k)
                            view.AxisY = diagram.SecondaryAxesY(axesPosition)
                            view.AxisY.Title.Text = "Puissance (W)"
                            'view.AxisY.Title.Alignment = StringAlignment.Center
                            view.AxisY.Title.Antialiasing = True
                            view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
                            view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

                            'diagram.AxisX.Alignment = AxisAlignment.Near
                            'lblVariable1.Text = "Bonjour"
                            Coherence_FVcmj(coeff_Force, coeff_Puissance, fittedPuissance, axeVitesse)
                            chrtFVcmj.Visible = True
                        Else
                            e.Item.Checked = True
                        End If
                    ElseIf choix = "CMJ" Then
                        'Dim i As Integer
                        If lstIndices.CheckedIndices.Count <> 0 Then
                            'le calcul ci-dessous peut être déporté
                            Coherence_CMJ()

                        Else
                            'si c'est le dernier élément, on le force à checked=true
                            e.Item.Checked = True
                        End If
                    ElseIf choix = "K" Then
                        'Dim i As Integer
                        If lstIndices.CheckedIndices.Count <> 0 Then
                            'le calcul ci-dessous peut être déporté
                            Coherence_K()

                        Else
                            'si c'est le dernier élément, on le force à checked=true
                            e.Item.Checked = True
                        End If
                    ElseIf choix = "RJ" Then
                        'Dim i As Integer
                        If lstIndices.CheckedIndices.Count <> 0 Then
                            'le calcul ci-dessous peut être déporté
                            Coherence_RJ()
                        Else
                            'si c'est le dernier élément, on le force à checked=true
                            e.Item.Checked = True
                        End If
                    ElseIf choix = "ISO" Then
                        'Dim i As Integer
                        If lstIndices.CheckedIndices.Count <> 0 Then
                            'le calcul ci-dessous peut être déporté
                            Coherence_ISO()
                        Else
                            'si c'est le dernier élément, on le force à checked=true
                            e.Item.Checked = True
                        End If



                        'If Not newAnalyse Then
                        ' Coherence(choix) ' MsgBox("Calcul de cohérence essai n° : " + CStr(e.Index + 1))                   
                    End If
                End If
                End If
        Catch ex As Exception
            MsgBox("Problème de 'ItemChecked' des essais dans la liste - ANALYSE : " + ex.Message, MsgBoxStyle.OkOnly, sender.text)
        End Try



    End Sub

    Private Sub lstIndices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstIndices.SelectedIndexChanged


    End Sub

    Private Sub chrtPrincipal_Click(sender As Object, e As EventArgs) Handles chrtPrincipal.Click

    End Sub

    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) Handles btnEnregistrer.Click

        Dim itListe As New ListViewItem

        Dim nomFichierIndices As String
        Dim repertoire As String
        repertoire = txtRepertoire.Text + "\RESULTATS\"
        SaveFileDialog1.InitialDirectory = repertoire
        SaveFileDialog1.Filter = "Fichier resultats  de l'analyse (*.csv)|.csv"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.DefaultExt = ".csv"
        SaveFileDialog1.FileName = "*.csv"

       
        Dim dr As DialogResult
        dr = SaveFileDialog1.ShowDialog()
        If dr = DialogResult.Cancel Then
            MsgBox("Vous n'avez pas indiqué un fichier de résultats")
            Exit Sub
        End If
        nomFichierIndices = SaveFileDialog1.FileName


        'ici la sauvegarde n'est pas faite de la même manière
        Dim texte As String
        Dim it As ListViewItem
        Dim col As ColumnHeader

        texte = "Sujet;" + "Test" + ";"
        For Each col In lstIndices.Columns
            texte = texte + col.Text + ";"
        Next
        texte = texte + vbCrLf
        For Each it In lstIndices.Items
            If it.Checked Then
                texte = texte + lblSujet.Text + ";" + choix + ";" + it.Text + ";"
                For i = 1 To it.SubItems.Count - 1
                    texte = texte + it.SubItems(i).Text + ";"
                Next
                texte = texte + vbCrLf
            End If            
        Next
        My.Computer.FileSystem.WriteAllText(nomFichierIndices, texte, True) 'ici True ajoute au contenu du fichier sinon le remplace

    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim numSamples As Integer = 6
    '    Dim xData() As Double = {1.378, 1.237, 1.131, 0.964, 0.86, 0.757}
    '    Dim yData() As Double = {2049.574, 2163.591, 2446.426, 2471.203, 2338.695, 2702.242}

    '    Dim specifiedCoefficients() As Double = {1, 1, 1, 1, 1, 1}
    '    Dim coefficients() As Double = Nothing
    '    Dim weight(numSamples - 1) As Double
    '    Dim fittedData() As Double
    '    Dim specifiedOrder() As Integer = {0, 0, 0, 0, 0, 0}
    '    Dim order As Integer
    '    Dim rSquare As Double


    '    order = 1




    '    'Analysis.Math.CurveFit.GoodnessOfFit(yData, fittedData, weight, sse, rSquare, msError)
    '    fittedData = AjustementPolynomial(xData, yData, numSamples, order, coefficients, rSquare)

    'End Sub

    Private Sub btnChargeConfig_Click(sender As Object, e As EventArgs) Handles btnChargeConfig.Click

        OpenFileDialog1.InitialDirectory = txtRepertoire.Text + "\CONFIGS"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Filter = "Fichier de configuration (*.xml)|*.xml|All Files (*.*)|*.*"
        OpenFileDialog1.FilterIndex = 1
        Dim dr As DialogResult
        dr = OpenFileDialog1.ShowDialog()
        If dr <> Windows.Forms.DialogResult.Cancel Then
           nomficConfig = OpenFileDialog1.FileName
            Charge_Config(nomficConfig)
        End If

        
    End Sub

    Private Sub btnEnregistreConfig_Click(sender As Object, e As EventArgs) Handles btnEnregistreConfig.Click

        SaveFileDialog1.InitialDirectory = txtRepertoire.Text + "\CONFIGS"
        SaveFileDialog1.FileName = nomficConfig
        SaveFileDialog1.Filter = "Fichier de configuration (*.xml)|*.xml|All Files (*.*)|*.*"
        SaveFileDialog1.FilterIndex = 1
        Dim dr As DialogResult
        dr = SaveFileDialog1.ShowDialog()
        If dr <> Windows.Forms.DialogResult.Cancel Then
            nomficConfig = SaveFileDialog1.FileName
            Ecrire_Config(nomficConfig)
            If Not (txtRepertoire.Text) Like "*Kinesis" And Not Directory.Exists(txtRepertoire.Text + "\Kinesis") Then
                'exécuté si le chemin provisoire ne comprends pas le dossier Kinesis ou s'il n'existe pas 
                'celà doit se produire lors de la première exécution

                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\SJ")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\CMJ")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\FVcmj")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\ISO")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\K")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\RJ")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\RJ6sec")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\RESULTATS")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\LISTES_SUJETS")
                Directory.CreateDirectory(txtRepertoire.Text + "\Kinesis\CONFIGS")
                'Pour la suite pb : envoie une exception si le fichier existe déjà
                My.Computer.FileSystem.CopyFile("Config.xml", txtRepertoire.Text + "\Kinesis\CONFIGS\Config.xml")
                'Pour la suite pb : envoie une exception si le fichier existe déjà
                My.Computer.FileSystem.CopyFile("MySubjects.csv", txtRepertoire.Text + "\Kinesis\LISTES_SUJETS\MesSujets.csv", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
        Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                txtRepertoire.Text = txtRepertoire.Text + "\Kinesis"
            End If

            Ecrire_Init("FichierConfig", nomficConfig)
            Ecrire_Init("Repertoire", txtRepertoire.Text)
            doc.Save(nomficConfig)
        End If
    End Sub

    Private Sub cmbxBlessures_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxBlessures.SelectedIndexChanged

    End Sub

    Private Sub btnAfficher_Click(sender As Object, e As EventArgs) Handles btnAfficher.Click
        Affiche_Blessure(config_chargee)
    End Sub

    Private Sub btnBlessure_Click(sender As Object, e As EventArgs) Handles btnBlessure.Click
        Ajout_Blessure(config_chargee)
        Affiche_Blessure(config_chargee)
    End Sub

    Private Sub txtBlessure_TextChanged(sender As Object, e As EventArgs) Handles txtBlessure.TextChanged

    End Sub

    Private Sub txtBlessure_Validated(sender As Object, e As EventArgs) Handles txtBlessure.Validated
        Affiche_Blessure(config_chargee)
        Affiche_Blessure(config_chargee)
    End Sub

    Private Sub tabConfig_Click(sender As Object, e As EventArgs) Handles tabConfig.Click

    End Sub

    Private Sub btnAccueilConfiguration_Click(sender As Object, e As EventArgs) Handles btnAccueilConfiguration.Click
        tabAppli.SelectTab("tabConfig")
    End Sub


    Private Sub numOrdrePoly_ValueChanged(sender As Object, e As EventArgs) Handles numOrdrePoly.ValueChanged
        'ajustement de la courbe
        'ajustement Puissance-vitesse
        If Not IsNothing(VitMoy) And (lstIndices.CheckedItems.Count > numOrdrePoly.Value) Then
            order = numOrdrePoly.Value
            ' Dim numSamples As Integer = VitMoy.Length
            fittedPuissance = AjustementPolynomial(VitChecked, PuisChecked, CheckedNumber, order, coeff_Puissance, rSquare)
            'création d'une courbe avec 100 points à partir de l'équation du polynôme
            Dim valeurMax As Double = VitChecked.Max
            Dim valeurMin As Double = VitChecked.Min
            Dim pas As Double
            pas = (valeurMax - valeurMin) / 100
            ReDim fittedPuissance(100)
            Dim axeVitesse(100)
            For i = 0 To 100
                axeVitesse(i) = valeurMin + i * pas
                fittedPuissance(i) = 0
                For j = 0 To coeff_Puissance.Length - 1
                    fittedPuissance(i) = coeff_Puissance(j) * (axeVitesse(i) ^ j) + fittedPuissance(i)
                Next j
            Next i
            lblOrdrePoly.ForeColor = Color.Black
            lblOrdrePoly.Text = "Polynôme d'ordre :"

            chrtFVcmj.Series.RemoveAt(3)
            chrtFVcmj.Series.RemoveAt(2)

            Dim diagram As XYDiagram = CType(chrtFVcmj.Diagram, XYDiagram)
            diagram.Panes.Clear()
            diagram.SecondaryAxesY.Clear()

            '    'On ajoute 1 panneau supplémentaire                    
            diagram.Panes.Add(New XYDiagramPane("Puissance"))

            Dim series_Sig(1) As Series
            series_Sig(0) = New Series("Puissance (W)", ViewType.Point)
            series_Sig(1) = New Series("Puis ajustée (W)", ViewType.Spline)

            Dim point As New SeriesPoint
            For k = 0 To CheckedNumber - 1
                series_Sig(0).Points.Add(New SeriesPoint(VitChecked(k), PuisChecked(k)))
                point = CType(series_Sig(0).Points.Last, SeriesPoint)
                point.Tag = ChargeChecked(k)
                'series_Sig(1).Points.Add(New SeriesPoint(VitChecked(k), fittedPuissance(k)))
            Next
            For k = 0 To fittedPuissance.Length - 1
                series_Sig(1).Points.Add(New SeriesPoint(axeVitesse(k), fittedPuissance(k)))
            Next
            series_Sig(0).LabelsVisibility = True

            chrtFVcmj.Series.AddRange(New Series() {series_Sig(0), series_Sig(1)})

            ''*********** Affichage de la puissance ****************************************
            'REM_GD n'ajoute pas un second axe ici ?
            Dim axesPosition As Integer = diagram.SecondaryAxesY.Add(New SecondaryAxisY())
            diagram.SecondaryAxesY(axesPosition).Alignment = AxisAlignment.Near
            diagram.SecondaryAxesY(axesPosition).GridLines.Visible = True

            CType(series_Sig(0).View, PointSeriesView).Pane = diagram.Panes(0)
            Dim view As XYDiagramSeriesViewBase = CType(series_Sig(0).View, XYDiagramSeriesViewBase)
            ' ''Creation d'un axe secondaire pour la vitesse                        
            view = CType(series_Sig(0).View, XYDiagramSeriesViewBase)
            view.AxisY = diagram.SecondaryAxesY(axesPosition)
            view.AxisY.Title.Text = "Puissance (W)"
            'view.AxisY.Title.Alignment = StringAlignment.Center
            view.AxisY.Title.Antialiasing = True
            view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
            view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            CType(series_Sig(1).View, SplineSeriesView).Pane = diagram.Panes(0)
            view = CType(series_Sig(1).View, XYDiagramSeriesViewBase)
            'view.Color = colors(k)
            view.AxisY = diagram.SecondaryAxesY(axesPosition)
            view.AxisY.Title.Text = "Puissance (W)"
            'view.AxisY.Title.Alignment = StringAlignment.Center
            view.AxisY.Title.Antialiasing = True
            view.AxisY.Title.Font = New Font("Tahoma", 8, FontStyle.Regular)
            view.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
        Else
            lblOrdrePoly.ForeColor = Color.Tomato
            lblOrdrePoly.Text = "Polynôme non calculé"
        End If
    End Sub

    Private Sub lstBlessures_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBlessures.SelectedIndexChanged

    End Sub

    Private Sub chkLstFiles1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chkLstFiles1.ItemCheck





    End Sub


    Private Sub chkLstFiles1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkLstFiles1.SelectedIndexChanged
        'Dim i As Integer

        Try
            'MsgBox(Str(sender.selectedIndex))
            Dim indexSelected As Integer
            indexSelected = sender.selectedIndex
            'Dim itemChecked As Object
            'Const quote As String = """"
            clstFichAnalyse.SetItemChecked(indexSelected, sender.GetItemCheckState(indexSelected))
            ' First show the index and check state of all selected items. 
            'ecked In sender.CheckedIndices
            '' The indexChecked variable contains the index of the item.
            'MessageBox.Show("Index#: " + indexChecked.ToString() + ", is checked. Checked state is:" + _
            '                sender.GetItemCheckState(indexChecked).ToString() + ".")
            'clstFichAnalyse.SetItemChecked(indexChecked, sender.GetItemCheckState(indexChecked))
            'Next

            '' Next show the object title and check state for each item selected. 
            'For Each itemChecked In sender.CheckedItems

            '    ' Use the IndexOf method to get the index of an item.
            '    MessageBox.Show("Item with title: " + quote + clstFichAnalyse.ToString() + quote + _
            '                    ", is checked. Checked state is: " + _
            '                    sender.GetItemCheckState(sender.Items.IndexOf(itemChecked)).ToString() + ".")
            'Next
            'Dim chkitemtmp As CheckedListBox.i
            'clstFichAnalyse.CheckedItemCollection(2) = False

        Catch ex As Exception
            MsgBox("Problème de sélection des fichiers dans la liste - Analyse : " + ex.Message, MsgBoxStyle.OkOnly, sender.text)
        End Try
    End Sub

    Private Sub chkLstFiles1_SelectedValueChanged(sender As Object, e As EventArgs) Handles chkLstFiles1.SelectedValueChanged

    End Sub

    Private Sub btnDeselectionner_Click(sender As Object, e As EventArgs) Handles btnDeselectionner.Click
        newAnalyse = True
        'si btnDeselectionner.Tag = false on déselectionne tout
        

        If btnDeselectionner.Tag Then
            For Each it As ListViewItem In lstIndices.Items
                it.Checked = btnDeselectionner.Tag 'False
            Next
            btnDeselectionner.Text = "Désélectionner"
        Else           
            For Each it As ListViewItem In lstIndices.CheckedItems
                it.Checked = btnDeselectionner.Tag 'False
            Next
            btnDeselectionner.Text = "Sélectionner"
        End If
        btnDeselectionner.Tag = Not btnDeselectionner.Tag
        newAnalyse = False
    End Sub

    

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim nbvis As Integer

        For Each serietmp As Series In chrtPrincipal.Series
            If serietmp.CheckedInLegend Then
                nbvis = nbvis + 1
            End If
        Next
        MsgBox("nb legend : " + CStr(nbvis))
    End Sub



    Private Sub chrtFVcmj_CustomDrawSeriesPoint(sender As Object, e As CustomDrawSeriesPointEventArgs) Handles chrtFVcmj.CustomDrawSeriesPoint
        ' choix = "FVcmj" Then
        'If e.SeriesPoint.Values(0) > 1 Then
        e.LabelText = e.SeriesPoint.Tag '"Critical value"
        'End If
    End Sub

    

    Private Sub btnRJ_CheckedChanged(sender As Object, e As EventArgs) Handles btnRJ.CheckedChanged

        For i = 0 To chrtPrincipal.Series.Count - 4 Step 4
            If chrtPrincipal.Series.Item(i).CheckedInLegend Then
                chrtPrincipal.Series.Item(i).Visible = sender.checked
                chrtPrincipal.Series.Item(i + 1).Visible = sender.checked
            End If          
        Next
        If sender.checked Then
            sender.text = "Masquer les histogrammes"
        Else

            sender.text = "Afficher les histogrammes"
        End If

    End Sub

    Private Sub txtNomCarte_TextChanged(sender As Object, e As EventArgs) Handles txtNomCarte.TextChanged

    End Sub
    Private Sub Coherence(ByVal choix As String)
        'PB de traitement sur tous les indices
        'PB 

        '// Déclaration de variables //
        Dim moy As Double
        Dim sigma As Double
        Dim tab() As Double
        Dim tab2p(0) As Double 'pour ISO
        Dim tabD(0) As Double
        Dim tabG(0) As Double
        Dim tabzcmj() As Double
        Dim it As ListViewItem
        Dim i As Integer
        Dim nbessai_min As Integer 'nbre d'essais corrects minimum par test
        Dim col As Integer 'n° de colonne ou applique le critere de coherence
        Dim col2 As Integer 'n° de colonne pour coherence en K et ISO
        Dim colzcmj As Integer 'n°col de zmax pour CMJ
        'Dim moy2 As Double
        'Dim sigma2 As Double
        'Dim tab2() As Double
        Dim tab22p(0) As Double 'pour ISO
        Dim tab2D(0) As Double
        Dim tab2G(0) As Double
        Dim tab32p(0) As Double
        Dim tab3D(0) As Double
        Dim tab3G(0) As Double
        Dim tab42p(0) As Double
        Dim tab4D(0) As Double
        Dim tab4G(0) As Double
        'Dim moyI2 As Double
        'Dim sigI2 As Double
        'Dim moyID As Double
        'Dim sigID As Double
        'Dim moyIG As Double
        'Dim sigIG As Double
        'Dim moyRFD2 As Double
        'Dim sigRFD2 As Double
        'Dim moyRFDD As Double
        'Dim sigRFDD As Double
        'Dim moyRFDG As Double
        'Dim sigRFDG As Double
        'Dim moyIk2 As Double
        'Dim moyIkD As Double
        'Dim moyIkG As Double
        'Dim moyRFDk2 As Double
        'Dim moyRFDkD As Double
        'Dim moyRFDkG As Double
        Dim iDroit As Integer = 0
        Dim iGauche As Integer = 0
        Dim diffI As Double = 0
        Dim diffRFD As Double = 0

        '// Fin de déclaration des variables

        '// Détermination du nombre d'essais en fonction des tests
        '// bizarre
        Try
            Select Case choix
                Case "CMJ"
                    nbessai_min = numMinCMJ.Value
                    col = numCritCMJ1 'représente quoi ? il faudrait mettre ici en constante
                    colzcmj = 4
                Case "SJ"
                    nbessai_min = numMinSJ.Value
                    col = numCritSJ
                Case "K"
                    nbessai_min = numMinK.Value
                    col = numCritK1
                    col2 = 4
                Case "ISO"
                    nbessai_min = numMinISO.Value
                    col = numCritISO1
                    col2 = 5
                Case "RJ"
                    nbessai_min = numMinRJ.Value
                    col = numCritRJ
                Case "FVcmj"
                    nbessai_min = numMinFV.Value
                    col = numCritFVcmj1
                Case "FVsj"
                    nbessai_min = numMinFV.Value
                    col = numCritFVcmj1
            End Select
            '// fin de détermination du nombre min d'essais

            lblCoherence1.Visible = True
            If lstIndices.CheckedIndices.Count <> 0 Then
                If choix <> "ISO" And choix <> "K" Then 'traitement pour tous les tests sauf ISO et sauf K
                    i = 0
                    ReDim tab(lstIndices.CheckedIndices.Count - 1)
                    ReDim tabzcmj(lstIndices.CheckedIndices.Count - 1) ' A vérifier
                    For Each it In lstIndices.CheckedItems
                        tab(i) = CDbl(it.SubItems.Item(col).Text) 'col represente la colonne de l'indice sur lequel s'applique la coherence
                        'tabzcmj(i) = CDbl(it.SubItems.Item(colzcmj).Text) 'colzcmj represente quoi ?
                        i = i + 1
                    Next
                    MsgBox("traitement sauf ISO et K")
                    moy = NationalInstruments.Analysis.Math.Statistics.Mean(tab)
                    sigma = NationalInstruments.Analysis.Math.Statistics.StandardDeviation(tab)
                    ' pour l'instant moyzCMJ = NationalInstruments.Analysis.Math.Statistics.Mean(tabzcmj)
                ElseIf choix = "K" Then
                    MsgBox("traitement  K")
                Else 'traitement particulier si ISO
                    MsgBox("traitement  ISO ")
                End If
            End If
        Catch ex As Exception
            MsgBox("Problème de calcul de cohérence " + ex.Message, MsgBoxStyle.OkOnly, "analyse")
        End Try


    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles lblMinISO.Click

    End Sub

    Private Sub tabAccueil_Click(sender As Object, e As EventArgs) Handles tabAccueil.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        frmInsertSujet.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmInsertSujet.ShowDialog()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Dim i As Integer
        Dim j As Integer
        Dim dr As DialogResult
        Dim nomFichierSujets As String
        Dim tmpString As String
        Dim it As ListViewItem
        Try
            SaveFileDialog1.InitialDirectory = txtRepertoire.Text + "\LISTES_SUJETS"
            SaveFileDialog1.FilterIndex = 1
            SaveFileDialog1.DefaultExt = ".csv"
            SaveFileDialog1.FileName = "*.csv"
            SaveFileDialog1.Filter = "Fichier sujets (*.csv)|.csv"

            dr = SaveFileDialog1.ShowDialog()
            If dr = DialogResult.Cancel Then
                MsgBox("Vous n'avez pas sauvegardé la lise des sujets")
                Exit Sub
            End If
            nomFichierSujets = SaveFileDialog1.FileName
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(nomFichierSujets, False) 'si le fichier existe déjà, il est écrasé
            file.WriteLine("Nom;Prenom;Masse;Banc;Pied;Angle1;Angle2;PuissRJ6")
            For i = 0 To lstSujets.Items.Count - 1
                it = lstSujets.Items(i)
                tmpString = it.Text '
                For j = 1 To it.SubItems.Count - 1
                    tmpString = tmpString + ";" + it.SubItems(j).Text
                Next
                file.WriteLine(tmpString)
            Next
            file.Close()

        Catch ex As Microsoft.VisualBasic.
                    FileIO.MalformedLineException
            MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
        End Try


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        lstSujets.Items.Remove(lstSujets.SelectedItems(0))
    End Sub

    Private Sub btnSuppBlessure_Click(sender As Object, e As EventArgs) Handles btnSuppBlessure.Click
        If lstBlessures.SelectedItem <> "" Then
            Dim tmpString As String
            Dim unNoeud As Xml.XmlNode
            Dim parentNoeud As Xml.XmlNode

            noeuds = doc.GetElementsByTagName("type")
            unNoeud = noeuds.Item(lstBlessures.SelectedIndex)
            tmpString = unNoeud.InnerText
            parentNoeud = unNoeud.ParentNode
            parentNoeud.RemoveChild(unNoeud)
            lstBlessures.Items.RemoveAt(lstBlessures.SelectedIndex)
            
        End If
    End Sub
    Private Sub Ecrire_NbEssais(ByVal config As Boolean)
        If config_chargee Then
            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("FVcmj")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("ordre_reg").Value = CStr(numOrdreReg.Value)
            unNoeud.Attributes("nbEssais").Value = CStr(numMinFV.Value)

            noeuds = doc.GetElementsByTagName("ISO")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("nbEssais").Value = CStr(numMinISO.Value)

            noeuds = doc.GetElementsByTagName("CMJ")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("nbEssais").Value = CStr(numMinCMJ.Value)

            noeuds = doc.GetElementsByTagName("SJ")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("nbEssais").Value = CStr(numMinSJ.Value)

            noeuds = doc.GetElementsByTagName("K")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("nbEssais").Value = CStr(numMinK.Value)

            noeuds = doc.GetElementsByTagName("RJ")
            unNoeud = noeuds.Item(0)
            unNoeud.Attributes("nbEssais").Value = CStr(numMinRJ.Value)
        End If
    End Sub
    Private Sub Lire_NbEssais(ByVal config As Boolean)
        If config_chargee Then
           

            'la méthode des noeuds
            Dim unNoeud As Xml.XmlNode
            noeuds = doc.GetElementsByTagName("FVcmj")
            unNoeud = noeuds.Item(0)
            numOrdreReg.Value = CInt(unNoeud.Attributes("ordre_reg").Value)
            numMinFV.Value = CInt(unNoeud.Attributes("nbEssais").Value)

            noeuds = doc.GetElementsByTagName("ISO")
            unNoeud = noeuds.Item(0)
            numMinISO.Value = CInt(unNoeud.Attributes("nbEssais").Value)

            noeuds = doc.GetElementsByTagName("CMJ")
            unNoeud = noeuds.Item(0)
            numMinCMJ.Value = CInt(unNoeud.Attributes("nbEssais").Value)

            noeuds = doc.GetElementsByTagName("SJ")
            unNoeud = noeuds.Item(0)
            numMinSJ.Value = CInt(unNoeud.Attributes("nbEssais").Value)

            noeuds = doc.GetElementsByTagName("K")
            unNoeud = noeuds.Item(0)
            numMinK.Value = CInt(unNoeud.Attributes("nbEssais").Value)

            noeuds = doc.GetElementsByTagName("RJ")
            unNoeud = noeuds.Item(0)
            numMinRJ.Value = CInt(unNoeud.Attributes("nbEssais").Value)
        End If
    End Sub

    Private Sub lblVariable1_Click(sender As Object, e As EventArgs) Handles lblVariable1.Click

    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click

    End Sub

    Private Sub numAngle1_ValueChanged(sender As Object, e As EventArgs) Handles numAngle1.ValueChanged

        Dim lstSelectedItem As ListViewItem.ListViewSubItem
        lstSelectedItem = lstSujets.SelectedItems(0).SubItems(5)
        lstSelectedItem.Text = Format(numAngle1.Value, "###0")
    End Sub

    Private Sub numAngle2_ValueChanged(sender As Object, e As EventArgs) Handles numAngle2.ValueChanged
        Dim lstSelectedItem As ListViewItem.ListViewSubItem
        lstSelectedItem = lstSujets.SelectedItems(0).SubItems(6)
        lstSelectedItem.Text = Format(numAngle2.Value, "###0")
    End Sub

    Private Sub numBanc_ValueChanged(sender As Object, e As EventArgs) Handles numBanc.ValueChanged
        Dim lstSelectedItem As ListViewItem.ListViewSubItem
        lstSelectedItem = lstSujets.SelectedItems(0).SubItems(3)
        lstSelectedItem.Text = Format(numBanc.Value, "###0")
    End Sub

    Private Sub numPieds_ValueChanged(sender As Object, e As EventArgs) Handles numPieds.ValueChanged
        Dim lstSelectedItem As ListViewItem.ListViewSubItem
        lstSelectedItem = lstSujets.SelectedItems(0).SubItems(4)
        lstSelectedItem.Text = Format(numPieds.Value, "###0")
    End Sub

    Private Sub numPieds_VisibleChanged(sender As Object, e As EventArgs) Handles numPieds.VisibleChanged
       
    End Sub

    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) Handles btnSupprimer.Click
        btnMesure.Enabled = True
        btnSauvegarder.Enabled = False
        btnSupprimer.Enabled = False
    End Sub

    
    Private Sub cmbxTypeCourbe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxTypeCourbe.SelectedIndexChanged

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs)
        'Dim texte As String

        Dim d1 As DateTime = DateTime.Now

        MsgBox(CStr(d1.ToLongTimeString()))

    End Sub

    Private Sub rbPiedD_CheckedChanged(sender As Object, e As EventArgs) Handles rbPiedD.CheckedChanged
        Dim nbTest As nbEssaiParTest
        If rbPiedD.Checked And lstSujets.SelectedItems.Count <> 0 Then
            nbTest = lstSujets.SelectedItems(0).Tag
            lblNbEssais.Text = CStr(nbTest.nbISO_D)
        End If
    End Sub

    Private Sub rbPiedG_CheckedChanged(sender As Object, e As EventArgs) Handles rbPiedG.CheckedChanged
        Dim nbTest As nbEssaiParTest
        If rbPiedG.Checked And lstSujets.SelectedItems.Count <> 0 Then
            nbTest = lstSujets.SelectedItems(0).Tag
            lblNbEssais.Text = CStr(nbTest.nbISO_G)
        End If
    End Sub

    Private Sub chrtFVcmj_Click(sender As Object, e As EventArgs) Handles chrtFVcmj.Click

    End Sub

End Class
' Implements the manual sorting of items by columns.
Class ListViewItemComparer
    Implements IComparer

    Private col As Integer

    Public Sub New()
        col = 0
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
       Implements IComparer.Compare

        Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
    End Function
End Class

