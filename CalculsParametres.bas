Attribute VB_Name = "CalculsParametres"
Global SeuilFz1 As Single
Global SeuilFz2 As Single
Global FichiersAOuvrir As Variant
Global NbFichiers As Integer
Global NOM_FICHIER_TRAITEMENT As Variant
Sub CalculsRJ(ByVal numcol As Integer)

    '/********** Variables d'entrée
    '    numCol numéro de la colonne à traiter
    
    '/**** DECLARATION DES VARIABLES ****/
    
    Dim Masse As Single 'masse du sujet
    Dim nbPoints As Integer 'nb de points du signal enregistré
    Dim freq As Integer 'fréquence d'échantillonnage
    
    Dim dt As Double 'période d'échantillonnage
    
    Dim i As Integer
    Dim j As Integer 'nb de fronts descendants
    Dim k As Integer
    Dim n As Integer
    
    Dim nbsauts  As Integer
    Dim nbFrBas As Integer
    Dim nbFrHauts As Integer
    
    Dim t1(60) As Single 'instants de decollage (F<5N)
    Dim T2(60) As Single ' instants d'atterrissage (F>5N)
                
    'paramètres calculés - ici le nombre de valeurs devrait être calculé
    Dim Tvol(60) As Single
    Dim TvolMoy(60) As Single
    Dim Tcontact(60) As Single
    Dim Ttotal(60) As Single
    Dim PuissBosco(60) As Single
   
    Dim Zmax(60) As Single
    Dim Impulsion(60) As Single
    Dim ForceMoy(60) As Single
    
    'Paramètres moyens
    Dim PuissMean As Single
    Dim ZmaxMean As Single
    Dim ImpulseMean As Single
    Dim ForceMean As Single
    
    '********************************************************************
    
    
    Sheets("data").Select
    
    Masse = Range("A1").Offset(1, numcol - 1).Value
    freq = Range("A1").Offset(4, numcol - 1).Value
    dt = 1 / freq
    
    
    'ActiveCell.Select
    Range("A1").Offset(6, numcol - 1).Select 'test
    Range(Selection, Selection.End(xlDown)).Select
    
    nbPoints = Selection.Rows.Count
    
    ReDim Force(0 To nbPoints + 1) As Single
    ReDim ForceTmp(0 To nbPoints + 1) As Single
    
    i = 1
    For Each cellule In Selection
        Force(i) = cellule.Value: i = i + 1
        ForceTmp(i) = Force(i)
    Next cellule
    
    Call TTSI2.FILTRE(0, 20, freq, nbPoints, Force)
    'nbsauts = 20
        
    k = 1
     
     'Detection des fronts bas
     t1(0) = 0
     j = 1
     i = 1
     While i < nbPoints - 3
        'For i = 1 To nbPoints - 3
        If Force(i) > SeuilFz1 And Force(i + 1) < SeuilFz1 And Force(i + 2) < SeuilFz1 Then
            For k = i To nbPoints - 2 'on affine la recherche du seuil
                 If Force(k) > SeuilFz2 And Force(k + 1) < SeuilFz2 Then
                      t1(j) = k
                      i = k
                      j = j + 1
                      Exit For
                 End If
            Next k
           
        End If
        i = i + 1
        'Next i
     Wend
     
    nbFrBas = j - 1
    'MsgBox ("Nb Fronts Bas" + Str(nbFrBas))
    
    'Detection des fronts hauts
    i = t1(1)
    j = 1
    
    While i < nbPoints - 3
    ' For i = t1(1) To nbPoints - 3  'on cherche à partir du premier front bas
        'ci-dessous on détecte à nouveau un seuil haut puis on va en arrière
        If Force(i) < SeuilFz1 And Force(i + 1) > SeuilFz1 And Force(i + 2) > SeuilFz1 Then
            k = i + 1 'GD attention modification remplacement de k=i par k=i+1
            While Force(k) > SeuilFz2
                T2(j) = k
                k = k - 1
            Wend
            If T2(j) <> T2(j - 1) Then
                j = j + 1
            End If
            
        End If
        i = i + 1
     Wend
     'Next i
     
    nbFrHauts = j - 1
            
    If nbFrBas > nbFrHauts Then
         nbFrBas = nbFrBas - 1 'le nbFrHauts doit être = nbFrBas
         If nbFrBas <> nbFrHauts Then
            MsgBox ("Veuillez régler les seuils : Erreur dans la détection des sauts")
         End If
    End If
    
    'élimination des doublons
    
    ' calcul des différents paramètres
    
    For i = 1 To nbFrBas - 1
         'Tvol(i) = (t2(i) - t1(i) + t2(i + 1) - t1(i + 1)) / 2
         Tvol(i) = (T2(i) - t1(i)) * dt
         Tcontact(i) = (t1(i + 1) - T2(i)) * dt
         ForceMoy(i) = TTSI2.CalculMoyenne(Force, T2(i), t1(i + 1), freq)
         Impulsion(i) = ForceMoy(i) * Tcontact(i)
         Zmax(i) = 1 / 8 * 9.81 * Tvol(i) ^ 2
    Next i
    Tvol(nbFrBas) = (T2(nbFrBas) - t1(nbFrBas)) * dt
    Zmax(nbFrBas) = 1 / 8 * 9.81 * Tvol(nbFrBas) ^ 2
    
    For i = 1 To nbFrBas - 1
        TvolMoy(i) = (Tvol(i) + Tvol(i + 1)) / 2
        Ttotal(i) = TvolMoy(i) + Tcontact(i)
        
        PuissBosco(i) = 9.81 ^ 2 * TvolMoy(i) * Ttotal(i) / (4 * Tcontact(i))
        
    Next i
    
    
    
    Sheets("Results").Select
   
    Range("A1").Select
    ActiveCell.Offset(0, 1).Select
    ActiveCell.Value = "Num saut"
     ActiveCell.Offset(0, 1).Select
    ActiveCell.Value = "Tcontact (s)"
     ActiveCell.Offset(0, 1).Select
    ActiveCell.Value = "Tvol (s)"
     ActiveCell.Offset(0, 1).Select
    ActiveCell.Value = "Force moy (N)"
     ActiveCell.Offset(0, 1).Select
    ActiveCell.Value = "Impulsion (N.s)"
      ActiveCell.Offset(0, 1).Select
    ActiveCell.Value = "Hauteur (m)"
   ActiveCell.Offset(0, 1).Select
    ActiveCell.Value = "Puissance abs (W)"
    ActiveCell.Offset(0, 1).Select
    ActiveCell.Value = "Puissance rel (W)"
    
    
    Range("A1").Select
    ActiveCell.Offset(0, 1).Select
    For i = 1 To nbFrBas
        ActiveCell.Offset(1, 0).Select
        ActiveCell.Value = i
    Next i
    Range("A1").Select
    ActiveCell.Offset(0, 2).Select
    For i = 1 To nbFrBas
        ActiveCell.Offset(1, 0).Select
        ActiveCell.Value = Tcontact(i)
    Next i
    Range("A1").Select
    ActiveCell.Offset(0, 3).Select
    For i = 1 To nbFrBas
        ActiveCell.Offset(1, 0).Select
        ActiveCell.Value = Tvol(i)
    Next i
    Range("A1").Select
    ActiveCell.Offset(0, 4).Select
    For i = 1 To nbFrBas
        ActiveCell.Offset(1, 0).Select
        ActiveCell.Value = ForceMoy(i)
    Next i
    Range("A1").Select
    ActiveCell.Offset(0, 5).Select
    For i = 1 To nbFrBas
        ActiveCell.Offset(1, 0).Select
        ActiveCell.Value = Impulsion(i)
    Next i
    Range("A1").Select
    ActiveCell.Offset(0, 6).Select
    For i = 1 To nbFrBas
        ActiveCell.Offset(1, 0).Select
        ActiveCell.Value = Zmax(i)
    Next i
    Range("A1").Select
    ActiveCell.Offset(0, 7).Select
    For i = 1 To nbFrBas
        ActiveCell.Offset(1, 0).Select
        ActiveCell.Value = PuissBosco(i) * Masse
    Next i
    Range("A1").Select
    ActiveCell.Offset(0, 8).Select
    For i = 1 To nbFrBas
        ActiveCell.Offset(1, 0).Select
        ActiveCell.Value = PuissBosco(i)
    Next i
   
    
 
    frmSeuils.Hide
End Sub
Sub CalculsForceIso(ByVal numcol As Integer)
             
    '/*********************************************************
    '/      Calcul des paramètres issus des test CMJ
    '/*********************************************************
    '/  Variables d'entrée :
    '/        numCol : numéro de la colonne traitée
    '/
    '/  Variables de sortie :
    '/
    '/*******************************************************
    '/*** modifié le 1 octobre 2014 par G Dalleau
    '/*******************************************************
    
        Dim i As Integer
        Dim TFmax As Integer
        Dim T0 As Integer
        Dim Tmp_Fmax As Single
        
        Dim valeursRfd150() As Single
     
        Dim Fmax As Single
        Dim RFDmax As Double
        
        Dim nbPoints As Integer 'nb de points du signal enregistré
        Dim BodyMass As Single 'masse du sujet
        Dim dt As Double 'période d'échantillonnage
        Dim freq As Integer
        '********************************************************************
        
        Sheets("data").Select
        BodyMass = Range("A1").Offset(1, numcol - 1).Value

        freq = Range("A1").Offset(4, numcol - 1).Value
        dt = 1 / freq
        
        
        'Récupération des données de force
        ActiveSheet.Name = "data"
        'mode de sélection à changer probablement
        Range("A1").Offset(11, numcol - 1).Select 'A vérifier
        Range(Selection, Selection.End(xlDown)).Select
        
        nbPoints = Selection.Rows.Count
        
        ReDim Force(nbPoints + 1) As Single
        ReDim ForceTmp(nbPoints + 1) As Single
        
        Force(0) = 0
        i = 1
        For Each cellule In Selection
            Force(i) = cellule.Value: i = i + 1
        Next cellule
        
        'filtre median sur 1001 points (1 seconde)
        For i = 0 To 499
            ForceTmp(i) = Force(i)
        Next
        For i = 500 To nbPoints - 500
            ForceTmp(i) = TTSI2.CalculMoyenne(Force(), i - 500, i + 500, 1001)
        Next
        For i = nbPoints - 499 To nbPoints
            ForceTmp(i) = Force(i)
        Next
        
        'recherche de la valeur max
        Fmax = 0
        For i = 0 To nbPoints
                If ForceTmp(i) > Fmax Then
                    Fmax = ForceTmp(i)
                    T_Fmax = i
                End If
        Next i
        
        'filtrage de la forcce à 20Hz
        For i = 0 To nbPoints
            ForceTmp(i) = Force(i)
        Next i
        Call TTSI2.FILTRE(0, 20, 1000, nbPoints, ForceTmp)
        
        'recherche du début de la montée en force
        i = T_Fmax
        
        Do While ForceTmp(i) > 10 'seuil à 10N
                i = i - 1
        Loop
        T0 = i
        
        
        ReDim valeursRfd150(nbPoints + 1)
        For i = 0 To nbPoints
            valeursRfd150(i) = ForceTmp(i)
        Next
        
        Call TTSI2.CalculDerivee(valeursRfd150, 0.15, nbPoints, freq)
       
        'Recherche du max en RFD
        RFDmax = 0
        For i = 0 To T_Fmax
            If valeursRfd150(i) > RFDmax Then
                RFDmax = valeursRfd150(i)
            End If
        Next
            
        T_Fmax = (T_Fmax - T0) * dt
       'Affichge des résultats
        
            Sheets("Results").Select
            
            
            Range("A2").Select
            ActiveCell.Value = "Force max (N)"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Temps pour Fmax (s)"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "RFD150ms"
            
            Range("A1").Offset(1, numcol).Select
            ActiveCell.Value = Fmax
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T_Fmax
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = RFDmax
         
End Sub
Sub CalculsCMJ(ByVal numcol As Integer)
    '/*********************************************************
    '/      Calcul des paramètres issus des test CMJ
    '/*********************************************************
    '/  Variables d'entrée :
    '/        numCol : numéro de la colonne traitée
    '/
    '/  Variables de sortie :
    '/
    '/*******************************************************
    '/*** modifié le 1 octobre 2014 par G Dalleau
    '/*******************************************************
    
    '******* Déclaration des variables *********************
    Dim Masse As Single 'masse totale sujet + charge
    Dim nbPoints As Integer 'nb de points du signal enregistré
    
    Dim Charge As Single 'masse de la charge
    Dim BodyMass As Single 'masse du sujet
    
    Dim dt As Double 'période d'échantillonnage
    
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim n As Integer
    
    Dim t1 As Single 'instants de decollage (F<5N)
    Dim T2 As Single ' instants d'atterrissage (F>5N)
                
    'paramètres calculés
    Dim Tvol As Single
    
    Dim PuissAbsPeak As Single 'puissance brute en W
    Dim PuissRelPeak As Single 'puissance relative en W/kg
    Dim PuissAbsMean As Single 'puissance brute en W
    Dim PuissRelMean As Single 'puissance relative en W/kg
    
    Dim Zmax As Single
    Dim VitEnvol As Single 'vitesse de ddécollage
    Dim VitBosco As Single
    
    Dim Hauteur As Single 'hauteur de saut estimée
    Dim ForceMoy As Single
    Dim VitMoy As Single
    
    Dim PhaseVol As String
    
    '********************************************************************
    Sheets("data").Select

    BodyMass = Range("A1").Offset(1, numcol - 1).Value
    Charge = Range("A1").Offset(6, numcol - 1).Value
    freq = Range("A1").Offset(4, numcol - 1).Value
    dt = 1 / freq
    
    
    'Récupération des données de force
    ActiveSheet.Name = "data"
    'mode de sélection à changer probablement
    Range("A1").Offset(7, numcol - 1).Select 'test
    Range(Selection, Selection.End(xlDown)).Select
    
    nbPoints = Selection.Rows.Count
    
    ReDim Force(0 To nbPoints + 1) As Single
    ReDim ForceTmp(0 To nbPoints + 1) As Single
    ReDim Acceleration(0 To nbPoints + 1) As Single
    ReDim Vitesse(0 To nbPoints + 1) As Single
    ReDim puissance(0 To nbPoints + 1) As Single
    ReDim Position(0 To nbPoints + 1) As Single
    
    
    i = 1
    For Each cellule In Selection
        Force(i) = cellule.Value: i = i + 1
        ForceTmp(i) = Force(i)
    Next cellule
    
    Call TTSI2.FILTRE(0, 20, 1000, nbPoints, Force)
   
    
    'Calcul de la masse sur 400 premiers points après les 10 points
     For i = 11 To 410
        Masse = Masse + Force(i)
    Next i
    Masse = Masse / 400 / 9.81
    'Masse = Charge + BodyMass
    'MsgBox ("Masse totale estimée : " + Str$(Masse))
     
     
    k = 1
     
     'detection du front bas (T& : début de la phase de vol)
     t1 = 0
     j = 1
     For i = 1 To nbPoints - 3
        If Force(i) > SeuilFz1 And Force(i + 1) < SeuilFz1 And Force(i + 2) < SeuilFz1 Then
            n = 0
            For k = i To nbPoints - 2 'on affine la recherche du seuil
                 If Force(k) > SeuilFz2 And Force(k + 1) < SeuilFz2 Then
                      t1 = k 'début du front bas donc de la phase de vol
                      Exit For
                 End If
            Next k
           Exit For
        End If
     Next i
        If t1 = 0 Then
            PhaseVol = "rien"
            MsgBox ("Pas de décollage")
           
        End If
        
    If t1 <> 0 Then
        'traitement de la phase de vol
        'detection du front haut (T2 : fin de la phase de vol)
        i = 1
        j = 1
        For i = t1 + 100 To nbPoints - 3 'on cherche à partir du  front bas
            If Force(i) < SeuilFz2 And Force(i + 1) > SeuilFz2 And Force(i + 2) > SeuilFz2 Then
                T2 = i
                Exit For
            End If
        Next i
 
        'mise à 0 les valeurs de forcce pendant la phase de vol (entre T1 et T2)
        For i = t1 + 1 To T2
            Force(i) = 0
        Next i
    End If
        
    ' detection de l'instant de début de contre mouvement (T0)
    T0 = 0

    For i = 1 To t1 'c'est problématique si le signal n'est pas parfait
        If Force(i) > Masse * 9.81 * 0.95 And Force(i + 20) < Masse * 9.81 * 0.95 And Force(i + 30) < Masse * 9.81 * 0.95 Then
            T0 = i
            Exit For
        End If
    Next i


    ' met les valeurs de départ avant le contre mouvement à la valeur du poids pour éviter la dérive de la vitesse (de i=1 à TO)
    For i = 1 To T0
        Force(i) = Masse * 9.81
    Next i


  
    ' calcul de l'accélération
    For i = 1 To nbPoints
        Acceleration(i) = (Force(i) - Masse * 9.81) / Masse
    Next i
    
    Sheets("acceleration").Activate
    ActiveSheet.Range(Cells(1, numcol), Cells(nbPoints, numcol)).Select
    i = 1
    For Each cellule In Selection
        cellule.Value = Acceleration(i): i = i + 1
        Vitesse(i) = Acceleration(i) 'on stocke l'accélération dans la vitesse pour intégrer
    Next cellule
        
    'calcul de la vitesse
    TTSI2.Primitive Vitesse(), freq, 1, nbPoints 'calcul de la vitesse
    
    VitMoy = 0
    For i = 1 To nbPoints
        VitMoy = VitMoy + Vitesse(i)
    Next i
    VitMoy = VitMoy / nbPoints
  

    'Filtrage Vitesse, 1000, 20 'filtrage à 20Hz
    Call TTSI2.FILTRE(0.1, 20, 1000, nbPoints, Vitesse)
   
    Sheets("vitesse").Select
    ActiveSheet.Range(Cells(1, numcol), Cells(nbPoints, numcol)).Select
    i = 1
    For Each cellule In Selection
        'Vitesse(i) = Vitesse(i) - VitMoy 'correction de la vitesse moyenne car elle doit être nulle
        cellule.Value = Vitesse(i): i = i + 1
        Position(i) = Vitesse(i) 'on stocke la vitesse dans la position pour intégrer
    Next cellule
    
    TTSI2.Primitive Position(), freq, 1, nbPoints 'calcul de la position
    
    Call TTSI2.FILTRE(0.1, 20, 1000, nbPoints, Position)
    
    Sheets("position").Select
    ActiveSheet.Range(Cells(1, numcol), Cells(nbPoints, numcol)).Select
    i = 1
    For Each cellule In Selection
        cellule.Value = Position(i): i = i + 1
    Next cellule
    
    
    For i = 1 To nbPoints
        puissance(i) = Force(i) * Vitesse(i)
    Next i
    
    Sheets("puissance").Select
    ActiveSheet.Range(Cells(1, numcol), Cells(nbPoints, numcol)).Select
    i = 1
    For Each cellule In Selection
        cellule.Value = puissance(i): i = i + 1
    Next cellule
    
    
    '/************************************************************************
    '/******* Extraction de certains paramètres à partir
    '/******* des courbes force, vitesse et puissance
    '/************************************************************************
    
    Dim T_PMax As Integer
    Dim T1_PuissPositive As Integer
    Dim T2_PuissPositive As Integer
    
    Dim PositiveTime As Integer
    
    PuissAbsPeak = 0
    PuissAbsMean = 0
    PuissRelPeak = 0
    PuissRelMean = 0
    
     ' Temps de vol
           Tvol = (T2 - t1) * dt
           
     ' Recherche de Zmax à partir des positions
           Zmax = 0
           For i = t1 + 1 To T2 - 1
               If Position(i) > Zmax Then
                   Zmax = Position(i)
               End If
           Next i
           Zmax = Zmax - Position(t1)
           
    'Calcul de la hauteur à partir de la formule de Bosco
           Hauteur = 1 / 8 * 9.81 * Tvol ^ 2
             
    ' Calcul de la vitesse d'envol à partir de la formule de Bosco
            VitBosco = 9.81 * Tvol / 2
            
    ' Recherche de la vitesse d'envol
           VitEnvol = Vitesse(t1)
          
    ' Recherche des puissances maximales
    ' Elle se fait avant l'envol
    
            For i = 100 To t1 'on commence un peu plus loin pour éviter les effets de bord
                If puissance(i) > PuissAbsPeak Then
                    PuissAbsPeak = puissance(i)
                    T_PMax = i
                End If
            Next i
            PuissRelPeak = PuissAbsPeak / Masse
            
            '/*** A partir de la valeur max de puissance
            ' on calcule la puissance moyenne positive une fois en incrémentant i
            i = T_PMax
            Do While puissance(i) > 0
                    PuissAbsMean = PuissAbsMean + puissance(i)
                    T2_PuissPositive = i 'fin de la puissance positive, ce qui est théoriquement la fin de la phase de vol
                    i = i + 1
                    If i = t1 + 1 Then
                        Exit Do
                    End If
            Loop
            ' on calcule la puissance moyenne positive une fois en décrémentant i
            i = T_PMax - 1
            Do While puissance(i) > 0
                    PuissAbsMean = PuissAbsMean + puissance(i)
                    T1_PuissPositive = i 'début de la puissance positive
                    i = i - 1
            Loop
            PositiveTime = T2_PuissPositive - T1_PuissPositive + 1 'en nombre de points
            PuissAbsMean = PuissAbsMean / PositiveTime
                      
            PositiveTime = PositiveTime * dt 'phase de puissance positive
            
            PuissRelMean = PuissAbsMean / Masse 'puissance moyenne relative à la masse
           
           
        
            Sheets("Results").Select
           
            Range("A2").Select
            ActiveCell.Value = "Front bas"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Front Haut"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Tvol (s)"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Zmax (m)"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Hauteur Bosco"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "VitEnvol"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Vit Bosco"
            
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "T_PMax"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Puissance maximale brute"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Puissance maximale spécifique"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "T1_PuissPositive"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "T2_PuissPositive"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Puissance moyenne brute"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Puissance moyenne spécifique"
            
            
            Range("A1").Offset(1, numcol).Select
            ActiveCell.Value = t1
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T2
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = Tvol
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = Zmax
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = Hauteur
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = VitEnvol
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = VitBosco
          
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T_PMax
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = PuissAbsPeak
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = PuissRelPeak
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T1_PuissPositive
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T2_PuissPositive
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = PuissAbsMean
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = PuissRelMean
        
            frmSeuils.Hide
    
End Sub
Sub CalculsFV(ByVal numcol As Integer)
    '/*********************************************************
    '/      Calcul des paramètres issus des test force-vitesse
    '/*********************************************************
    '/  Variables d'entrée :
    '/        numCol : numéro de la colonne traitée
    '/
    '/  Variables de sortie :
    '/
    '/*******************************************************
    '/*** modifié le7 mars 2013 par G Dalleau
    '/*******************************************************
    
    '******* Déclaration des variables *********************


    '/**** DECLARATION DES VARIABLES ****/
    
    Dim Masse As Single 'masse totale sujet + charge
    Dim nbPoints As Integer 'nb de points du signal enregistré
    
    Dim Charge As Single 'masse de la charge
    Dim BodyMass As Single 'masse du sujet
    
    Dim dt As Double 'période d'échantillonnage
    
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim n As Integer
    
    Dim t1 As Single 'instants de decollage (F<5N)
    Dim T2 As Single ' instants d'atterrissage (F>5N)
                
    'paramètres calculés
    Dim Tvol As Single
    
    Dim PuissAbsPeak As Single 'puissance brute en W
    Dim PuissRelPeak As Single 'puissance relative en W/kg
    Dim PuissAbsMean As Single 'puissance brute en W
    Dim PuissRelMean As Single 'puissance relative en W/kg
    
    Dim Zmax As Single
    Dim VitEnvol As Single 'vitesse de ddécollage
    Dim VitBosco As Single
    
    Dim Hauteur As Single 'hauteur de saut estimée
    Dim ForceMoy As Single
    Dim VitMoy As Single
    
    Dim PhaseVol As String
    
    '********************************************************************
    Sheets("data").Select

    BodyMass = Range("A1").Offset(1, numcol - 1).Value
    Charge = Range("A1").Offset(6, numcol - 1).Value
    freq = Range("A1").Offset(4, numcol - 1).Value
    dt = 1 / freq
    
    'MsgBox (CStr(BodyMass))
    
    'Récupération des données de force
    ActiveSheet.Name = "data"
    'mode de sélection à changer probablement
    Range("A1").Offset(7, numcol - 1).Select 'test
    Range(Selection, Selection.End(xlDown)).Select
    
    nbPoints = Selection.Rows.Count
    
    ReDim Force(0 To nbPoints + 1) As Single
    ReDim ForceTmp(0 To nbPoints + 1) As Single
    ReDim Acceleration(0 To nbPoints + 1) As Single
    ReDim Vitesse(0 To nbPoints + 1) As Single
    ReDim puissance(0 To nbPoints + 1) As Single
    ReDim Position(0 To nbPoints + 1) As Single
    
    
    i = 1
    For Each cellule In Selection
        Force(i) = cellule.Value: i = i + 1
        ForceTmp(i) = Force(i)
    Next cellule
    
    '**** ATTENTION : Filtrage ????
    
    
    'Calcul de la masse sur 200 premiers points après les 10 points
     For i = 11 To 210
        Masse = Masse + Force(i)
    Next i
    Masse = Masse / 200 / 9.81
    'Masse = Charge + BodyMass
    'MsgBox ("Masse totale estimée : " + Str$(Masse))
     
     
    k = 1
     
     'detection du front bas
     t1 = 0
     j = 1
     For i = 1 To nbPoints - 3
        If Force(i) > SeuilFz1 And Force(i + 1) < SeuilFz1 And Force(i + 2) < SeuilFz1 Then
            n = 0
            For k = i To nbPoints - 2 'on affine la recherche du seuil
                 If Force(k) > SeuilFz2 And Force(k + 1) < SeuilFz2 Then
                      t1 = k 'début du front bas donc de la phase de vol
                      Exit For
                 End If
            Next k
           Exit For
        End If
     Next i
        If t1 = 0 Then
            PhaseVol = "rien"
            'MsgBox ("Pas de décollage")
           
        End If
           If t1 <> 0 Then
               'traitement de la phase de vol
               'detection du front haut
               i = 1
               j = 1
               For i = t1 + 100 To nbPoints - 3 'on cherche à partir du  front bas
                   If Force(i) < SeuilFz2 And Force(i + 1) > SeuilFz2 And Force(i + 2) > SeuilFz2 Then
                       T2 = i
                       Exit For
                   End If
               Next i
        
          
               For i = t1 + 1 To T2
                   Force(i) = 0
               Next i
           End If
    
    '****** correction de la force par rapport au poids
    ForceMoy = 0
    For i = 1 To nbPoints
        ForceMoy = ForceMoy + Force(i)
     Next i
    ForceMoy = ForceMoy / nbPoints
    'MsgBox (CStr(ForceMoy))
    
    For i = 1 To nbPoints
        Force(i) = Force(i) / ForceMoy * Masse * 9.81
    Next i
   ' '*******
   
    ' calcul de l'accélération
    For i = 1 To nbPoints
        Acceleration(i) = (Force(i) - Masse * 9.81) / Masse
    Next i
    
    Sheets("acceleration").Activate
    ActiveSheet.Range(Cells(1, numcol), Cells(nbPoints, numcol)).Select
    i = 1
    For Each cellule In Selection
        cellule.Value = Acceleration(i): i = i + 1
        Vitesse(i) = Acceleration(i) 'on stocke l'accélération dans la vitesse pour intégrer
    Next cellule
        
    'calcul de la vitesse
    TTSI2.Primitive Vitesse(), freq, 1, nbPoints 'calcul de la vitesse
    
    VitMoy = 0
    For i = 1 To nbPoints
        VitMoy = VitMoy + Vitesse(i)
    Next i
    VitMoy = VitMoy / nbPoints
    'MsgBox ("Vitesse moy sur l'ensemble de l'acquisition" + CStr(VitMoy))
    'il faut aussi corriger la vitesse a priori

    
    'filtrage de la vitesse
    'Filtrage Vitesse, 1000, 20 'filtrage à 20Hz
    Call TTSI2.FILTRE(0.1, 20, 1000, nbPoints, Vitesse)
   
    Sheets("vitesse").Select
    ActiveSheet.Range(Cells(1, numcol), Cells(nbPoints, numcol)).Select
    i = 1
    For Each cellule In Selection
        'Vitesse(i) = Vitesse(i) - VitMoy 'correction de la vitesse moyenne car elle doit être nulle
        cellule.Value = Vitesse(i): i = i + 1
        Position(i) = Vitesse(i) 'on stocke la vitesse dans la position pour intégrer
    Next cellule
    
    TTSI2.Primitive Position(), freq, 1, nbPoints 'calcul de la position
    
    Call TTSI2.FILTRE(0.1, 20, 1000, nbPoints, Position)
    
    Sheets("position").Select
    ActiveSheet.Range(Cells(1, numcol), Cells(nbPoints, numcol)).Select
    i = 1
    For Each cellule In Selection
        cellule.Value = Position(i): i = i + 1
    Next cellule
    
    
    For i = 1 To nbPoints
        puissance(i) = Force(i) * Vitesse(i)
    Next i
    'MsgBox (Str(puissance(100)))
    Sheets("puissance").Select
    ActiveSheet.Range(Cells(1, numcol), Cells(nbPoints, numcol)).Select
    i = 1
    For Each cellule In Selection
        cellule.Value = puissance(i): i = i + 1
    Next cellule
    
    '/************************************************************************
    '/******* Extraction de certains paramètres à partir
    '/******* des courbes force, vitesse et puissance
    '/************************************************************************
    
    Dim T_PMax As Integer
    Dim T1_PuissPositive As Integer
    Dim T2_PuissPositive As Integer
    
    Dim PositiveTime As Integer
    
    PuissAbsPeak = 0
    PuissAbsMean = 0
    PuissRelPeak = 0
    PuissRelMean = 0
    
    If PhaseVol <> "rien" Then
           ' calcul des différents paramètres
           Tvol = (T2 - t1) * dt
           Zmax = 0
           For i = t1 + 1 To T2 - 1
               If Position(i) > Zmax Then
                   Zmax = Position(i)
               End If
           Next i
           VitEnvol = Vitesse(t1)
           VitBosco = 9.81 * Tvol / 2
    
            For i = 100 To t1 'on commence un peu plus loin pour éviter les effets de bord
                If puissance(i) > PuissAbsPeak Then
                    PuissAbsPeak = puissance(i)
                    T_PMax = i
                End If
            Next i
            PuissRelPeak = PuissAbsPeak / BodyMass
            
            
            '/*** A partir de la valeur max de puissance
            ' on calcule la puissance moyenne positive une fois en incrémentant i
            i = T_PMax
            Do While puissance(i) > 0
                    PuissAbsMean = PuissAbsMean + puissance(i)
                    T2_PuissPositive = i 'fin de la puissance positive
                    i = i + 1
                    If i = t1 + 1 Then
                        Exit Do
                    End If
            Loop
            ' on calcule la puissance moyenne positive une fois en décrémentant i
            i = T_PMax - 1
            Do While puissance(i) > 0
                    PuissAbsMean = PuissAbsMean + puissance(i)
                    T1_PuissPositive = i 'début de la puissance positive
                    i = i - 1
            Loop
            PositiveTime = T2_PuissPositive - T1_PuissPositive + 1 'en nombre de points
            PuissAbsMean = PuissAbsMean / PositiveTime
            
            
            VitMoy = 0
            For i = T1_PuissPositive To T2_PuissPositive
                VitMoy = Vitesse(i) + VitMoy
            Next i
            VitMoy = VitMoy / PositiveTime
            
            ForceMoy = 0
            For i = T1_PuissPositive To T2_PuissPositive
                ForceMoy = ForceMoy + Force(i)
             Next i
            ForceMoy = ForceMoy / PositiveTime
            
            PositiveTime = PositiveTime * dt
            
            PuissRelMean = PuissAbsMean / BodyMass
           
            Hauteur = 1 / 8 * 9.81 * Tvol ^ 2
        
            Sheets("Results").Select
           
            Range("A2").Select
            ActiveCell.Value = "Front bas"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Front Haut"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Tvol (s)"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Zmax (m)"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Hauteur Bosco"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "VitEnvol"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Vit Bosco"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Vit Moyenne"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Force Moyenne"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "T_PMax"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Puissance maximale brute"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Puissance maximale sépcifique"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "T1_PuissPositive"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "T2_PuissPositive"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Puissance moyenne brute"
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = "Puissance moyenne sépcifique"
            
            
            Range("A1").Offset(1, numcol).Select
            ActiveCell.Value = t1
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T2
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = Tvol
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = Zmax
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = Hauteur
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = VitEnvol
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = VitBosco
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = VitMoy
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = ForceMoy
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T_PMax
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = PuissAbsPeak
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = PuissRelPeak
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T1_PuissPositive
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = T2_PuissPositive
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = PuissAbsMean
            ActiveCell.Offset(1, 0).Select
            ActiveCell.Value = PuissRelMean
        
            frmSeuils.Hide

Else

  
    For i = 100 To nbPoints 'on commence un peu plus loin pour éviter les effets de bord
        If puissance(i) >= PuissAbsPeak Then
            PuissAbsPeak = puissance(i)
            T_PMax = i
        End If
    Next i
    PuissRelPeak = PuissAbsPeak / BodyMass
    
    '/*** A partir de la valeur max de puissance
    ' on calcule la puissance moyenne positive une fois en incrémentant i
    i = T_PMax
    Do While puissance(i) > 0
            PuissAbsMean = PuissAbsMean + puissance(i)
            T2_PuissPositive = i 'fin de la puissance positive
            i = i + 1
            If i = nbPoints Then
                Exit Do
            End If
    Loop
    ' on calcule la puissance moyenne positive une fois en décrémentant i
    i = T_PMax - 1
    Do While puissance(i) > 0
            PuissAbsMean = PuissAbsMean + puissance(i)
            T1_PuissPositive = i 'début de la puissance positive
            i = i - 1
            If i = 0 Then
                Exit Do
            End If
    Loop
    PositiveTime = T2_PuissPositive - T1_PuissPositive + 1 'en nombre de points
    PuissAbsMean = PuissAbsMean / PositiveTime
    
    
    VitMoy = 0
    For i = T1_PuissPositive To T2_PuissPositive
        VitMoy = Vitesse(i) + VitMoy
    Next i
    VitMoy = VitMoy / PositiveTime
    
    ForceMoy = 0
    For i = T1_PuissPositive To T2_PuissPositive
        ForceMoy = ForceMoy + Force(i)
     Next i
    ForceMoy = ForceMoy / PositiveTime
    
    PositiveTime = PositiveTime * dt
    PuissRelMean = PuissAbsMean / BodyMass

    Sheets("Results").Select
    
    Range("A2").Select
    ActiveCell.Value = "Front bas"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Front Haut"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Tvol (s)"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Zmax (m)"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Hauteur Bosco"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "VitEnvol"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Vit Bosco"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Vit Moyenne"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Force Moyenne"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "T_PMax"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Puissance maximale brute"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Puissance maximale sépcifique"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "T1_PuissPositive"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "T2_PuissPositive"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Puissance moyenne brute"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Puissance moyenne sépcifique"
    
    
    Range("A1").Offset(1, numcol).Select
    ActiveCell.Value = "pas de saut"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "pas de saut"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "pas de saut"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = Zmax
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "pas de saut"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = VitEnvol
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = VitBosco
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = VitMoy
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = ForceMoy
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = T_PMax
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = PuissAbsPeak
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = PuissRelPeak
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = T1_PuissPositive
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = T2_PuissPositive
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = PuissAbsMean
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = PuissRelMean
End If

    frmSeuils.Hide

End Sub

Sub CalculsRaideur(ByVal numcol As Integer)
    
    '/********** Variables d'entrée
    '    numCol numéro de la colonne à traiter
    
    '/**** DECLARATION DES VARIABLES ****/
    
    Dim Masse As Single 'masse du sujet
    Dim nbPoints As Integer 'nb de points du signal enregistré
    Dim freq As Integer 'fréquence d'échantillonnage
    'Dim Force(5001) As Single
    'Dim ForceTmp(5001) As Single
    
    Dim dt As Double 'période d'échantillonnage
    
    Dim i As Integer
    Dim j As Integer 'nb de fronts descendants
    Dim k As Integer
    Dim n As Integer
    
    Dim nbsautK  As Integer
    Dim nbFrBas As Integer
    Dim nbFrHauts As Integer
    
    Dim t1(20) As Single 'instants de decollage (F<5N)
    Dim T2(20) As Single ' instants d'atterrissage (F>5N)
                
    'paramètres calculés
    Dim Tvol(20) As Single
    Dim TvolMoy(20) As Single
    Dim Tcontact(20) As Single
    Dim Ttotal(20) As Single
    Dim PuissK(20) As Single
    Dim RaideurK(20) As Single
    Dim RaideurKabs(20) As Single
    Dim Zmax(20) As Single
    Dim Impulsion(20) As Single
    
    'Paramètres moyens
    Dim PuissMean As Single
    Dim KMeanAbs As Single
    Dim KMeanRel As Single
    Dim ZmaxMean As Single
    Dim ImpulseMean As Single
    
    '********************************************************************
    
    
    Sheets("data").Select
    
    Masse = Range("A1").Offset(1, numcol - 1).Value
    freq = Range("A1").Offset(4, numcol - 1).Value
    dt = 1 / freq
    
    
    'ActiveCell.Select
    Range("A1").Offset(6, numcol - 1).Select 'test
    Range(Selection, Selection.End(xlDown)).Select
    
    nbPoints = Selection.Rows.Count
    
    ReDim Force(0 To nbPoints + 1) As Single
    ReDim ForceTmp(0 To nbPoints + 1) As Single
    
    i = 1
    For Each cellule In Selection
        Force(i) = cellule.Value: i = i + 1
        ForceTmp(i) = Force(i)
    Next cellule
    
    Call TTSI2.FILTRE(0, 20, freq, nbPoints, Force)
    'nbsautK = 20
        
    k = 1
     
     'Detection des fronts bas
     t1(0) = 0
     j = 1
     For i = 1 To nbPoints - 3
        If Force(i) > SeuilFz1 And Force(i + 1) < SeuilFz1 And Force(i + 2) < SeuilFz1 Then
            n = 0
            For k = i To nbPoints - 2 'on affine la recherche du seuil
                 If Force(k) > SeuilFz2 And Force(k + 1) < SeuilFz2 Then
                      t1(j) = k
                      j = j + 1
                      Exit For
                 End If
            Next k
           
        End If
     Next i
     
    nbFrBas = j - 1
    'MsgBox ("Nb Fronts Bas" + Str(nbFrBas))
    
    'Detection des fronts hauts
    i = 1
    j = 1
    
     For i = t1(1) To nbPoints - 3  'on cherche à partir du premier front bas
        'ci-dessous on détecte à nouveau un seuil haut puis on va en arrière
        If Force(i) < SeuilFz1 And Force(i + 1) > SeuilFz1 And Force(i + 2) > SeuilFz1 Then
            k = i + 1 'GD attention modification remplacement de k=i par k=i+1
            While Force(k) > SeuilFz2
                T2(j) = k
                k = k - 1
            Wend
            j = j + 1
            
        End If
     Next i
     
    nbFrHauts = j - 1
        
    If nbFrBas > nbFrHauts Then
         nbFrBas = nbFrBas - 1 'le nbFrHauts doit être = nbFrBas
         If nbFrBas <> nbFrHauts Then
            MsgBox ("Veuillez régler les seuils : Erreur dans la détection des sauts")
         End If
    End If
    
    ' calcul des différents paramètres
    
    For i = 1 To nbFrBas - 1
         'Tvol(i) = (t2(i) - t1(i) + t2(i + 1) - t1(i + 1)) / 2
         Tvol(i) = (T2(i) - t1(i)) * dt
         Tcontact(i) = (t1(i + 1) - T2(i)) * dt
         Impulsion(i) = TTSI2.CalculMoyenne(Force, T2(i), t1(i + 1), freq)
         Zmax(i) = 1 / 8 * 9.81 * Tvol(i) ^ 2
    Next i
    Tvol(nbFrBas) = (T2(nbFrBas) - t1(nbFrBas)) * dt
    Zmax(nbFrBas) = 1 / 8 * 9.81 * Tvol(nbFrBas) ^ 2
    
    For i = 1 To nbFrBas - 1
        TvolMoy(i) = (Tvol(i) + Tvol(i + 1)) / 2
        Ttotal(i) = TvolMoy(i) + Tcontact(i)
        RaideurK(i) = 3.14159 * Ttotal(i) / (Tcontact(i) ^ 2 * (Ttotal(i) / 3.14159 - Tcontact(i) / 4))
        PuissK(i) = 9.81 ^ 2 * TvolMoy(i) * Ttotal(i) / (4 * Tcontact(i))
        
    Next i
    
    For j = 1 To nbFrBas - 1
        ZmaxMean = ZmaxMean + Zmax(j)
    Next j
    ZmaxMean = ZmaxMean / (nbFrBas - 1)
   
    
   
    For j = 1 To nbFrBas - 1
        KMeanRel = KMeanRel + RaideurK(j)
    Next j
    KMeanRel = KMeanRel / (nbFrBas - 1)
    KMeanAbs = KMeanRel * Masse
    
    For j = 1 To nbFrBas - 1
        PuissMean = PuissMean + PuissK(j)
    Next j
    PuissMean = PuissMean / (nbFrBas - 1)
   
    For j = 1 To nbFrBas - 1
       ImpulseMean = ImpulseMean + Impulsion(j)
    Next j
    ImpulseMean = ImpulseMean / (nbFrBas - 1)

    Sheets("Results").Select
   
    Range("A1").Select
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "P (W/kg)"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "K (N/m)"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "K (N/m/kg)"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Zmax (m)"
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = "Impulsion (N.s)"
    
    Range("A1").Select
    ActiveCell.Offset(1, numcol).Select
    ActiveCell.Value = PuissMean
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = KMeanAbs
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = KMeanRel
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = ZmaxMean
    ActiveCell.Offset(1, 0).Select
    ActiveCell.Value = ImpulseMean
 
    frmSeuils.Hide
 
End Sub
Sub FILTRE()
    On Error GoTo Quit
    
    Application.ScreenUpdating = False
    Application.Calculation = xlCalculationManual
    Dim cellule, t(10500) As Double, i As Integer, nb As Integer, fcut As Integer, facqui As Integer
    facqui = InputBox("Fréquence d'échantillonnage (Hz) ?", "Filtre de Butterworth", 1000)
    
    fcut = InputBox("Fréquence de coupure (Hz) ?", "Filtre de Butterworth", 10)
    
    
    
    For Each cellule In Selection
        t(i) = cellule.Value: i = i + 1
    Next cellule
    nb = i
    Filtrage t, facqui, fcut
    i = 0
    For Each cellule In Selection
        cellule.Value = t(i): i = i + 1
    Next cellule
    Application.Calculation = xlCalculationAutomatic
    Application.ScreenUpdating = True
Quit:

End Sub


Sub Filtrage(ChannelData1D() As Double, ByVal FreqEch As Integer, ByVal cutf As Integer)

    '** filtrage passe-bas **
    '* tableau à une dimension
    '* fréquence d'échantillonnage
    '* fréquence de coupure
    
      Dim nbPoints As Long
      nbPoints = UBound(ChannelData1D)
      ReDim tampon(0 To nbPoints) As Single
      Dim i As Long
    
    'Symétrie de 10 pts avant et après
      For i = 10 To nbPoints - 10
         tampon(i) = 1! * ChannelData1D(i)
      Next i
    
      For i = 1 To 10
         tampon(10 - i) = 2! * tampon(10) - tampon(10 + i)
         tampon(nbPoints - 10 + i) = 2! * tampon(nbPoints - 10) - tampon(nbPoints - 10 - i)
      Next i
    
        TTSI2.FBUTTER2 cutf, FreqEch, tampon(), 10, nbPoints - 10
        'FiltrageBUTTER cutf, FreqEch, tampon(), 10, NbPoints - 10
        
      For i = 0 To nbPoints
         ChannelData1D(i) = CSng(tampon(i))
      Next i
    
End Sub




