Module AnalysePF
    '********************************************************************************************
    ' Ce module regroupe les analyses relatives aux signaux plateformes lors de tests de saut
    '
    '********************************************************************************************
    Public Structure Indice
        'cet indice est soit une valeur unique ou une valeur moyenne
        'si type_moyenne= true alors il s'agit d'une valeur moyenne
        'sinon il s'agit d'une valeur unique
        Dim type_moyenne As Boolean
        Dim nom As String
        Dim valeur As Double 'valeur moyenne ou unique
        Dim nbValeurs As Integer
        Dim tabValeurs() As Double
    End Structure

    'tableux de données relatives à un fichier de données
    Public Structure meca_signaux
        Dim temps() As Double
        Dim Force() As Double     'signal de force
        Dim Acceleration() As Double    'signal d'accélération
        Dim Vitesse() As Double    'signal de vitesse
        Dim Position() As Double    'signal de position
        Dim Puissance() As Double    'signal de puissance
    End Structure

    Public Structure Essai
        Dim test As String
        Dim fic As FichierDonnees
        Dim Signaux As meca_signaux

        'indices : exemple Pmax, Hmax, ...
        Dim nbIndices As Integer
        Dim nbPaquetsIndices As Integer 'équivalent de nbValeurs dans la structure Indice
        Dim indices() As Indice 'la dimension sera définie par nbIndices et cela dépend du type de test

    End Structure
    
    Public seuilFz1 As Double
    Public seuilFz2 As Double

    

    Sub initialise_Essai(ByRef ficData As FichierDonnees, ByRef essaiData As Essai)
        'initialise essaidata avec ficData
        Dim dt As Double
        dt = 1 / ficData.fs
        essaiData.fic = ficData
        ReDim essaiData.Signaux.temps(ficData.nbpoints - 1)
        ReDim essaiData.Signaux.Acceleration(ficData.nbpoints - 1)
        ReDim essaiData.Signaux.Vitesse(ficData.nbpoints - 1)
        ReDim essaiData.Signaux.Puissance(ficData.nbpoints - 1)
        ReDim essaiData.Signaux.Position(ficData.nbpoints - 1)
        ReDim essaiData.Signaux.Force(ficData.nbpoints - 1)
        For i = 0 To ficData.nbpoints - 1
            essaiData.Signaux.temps(i) = i * dt
        Next
        essaiData.Signaux.Force = ficData.sig
        'essaiData.Signaux.Acceleration = essaiData.Signaux.Force



    End Sub
    Sub initialise_Essai_Test(ByRef essaiData As Essai, ByVal typeTest As String)
        'initialise certaines variables de essaiData en focntion du type de test

        Select Case typeTest
            Case "CMJ"
                essaiData.test = typeTest
                essaiData.nbIndices = 5
                essaiData.nbPaquetsIndices = 1
                ReDim essaiData.indices(4)
                essaiData.indices(0).nom = "Tv"
                essaiData.indices(1).nom = "P (W/kg)"
                essaiData.indices(2).nom = "Pmax(W)"
                essaiData.indices(3).nom = "Zmax(m)"
                essaiData.indices(4).nom = "He(m)"
                
            Case "SJ"
                essaiData.test = typeTest
                essaiData.nbIndices = 5
                essaiData.nbPaquetsIndices = 1
                ReDim essaiData.indices(4)
                essaiData.indices(0).nom = "Tv"
                essaiData.indices(1).nom = "P (W/kg)"
                essaiData.indices(2).nom = "Pmax(W)"
                essaiData.indices(3).nom = "Zmax(m)"
                essaiData.indices(4).nom = "He(m)"

            Case "K"
                essaiData.test = typeTest
                essaiData.nbIndices = 5
                essaiData.nbPaquetsIndices = 1
                ReDim essaiData.indices(4)
                essaiData.indices(0).nom = "P (W/kg)"
                essaiData.indices(1).nom = "K (N/m)"
                essaiData.indices(2).nom = "K (N/m/kg)"
                essaiData.indices(3).nom = "Zmax (m)"
                essaiData.indices(4).nom = "Impulsion (N.s)"
                
            Case "ISO"
                essaiData.test = typeTest
                essaiData.nbIndices = 6
                essaiData.nbPaquetsIndices = 1
                ReDim essaiData.indices(5)
                essaiData.indices(0).nom = "Force max (N)"
                essaiData.indices(1).nom = "Temps pour Fmax (s)"
                essaiData.indices(2).nom = "RFD150ms"
                essaiData.indices(3).nom = "Type de courbe"
                essaiData.indices(4).nom = "Appuis"
                essaiData.indices(5).nom = "Position"

            Case "RJ"
                essaiData.test = typeTest
                essaiData.nbIndices = 12
                essaiData.nbPaquetsIndices = 60
                ReDim essaiData.indices(11)
                essaiData.indices(0).nom = "Tv"
                essaiData.indices(1).nom = "Tv_moy"
                essaiData.indices(2).nom = "Tc"
                essaiData.indices(3).nom = "Tt"
                essaiData.indices(4).nom = "Puiss Bosco"
                essaiData.indices(5).nom = "Zmax (m)"
                essaiData.indices(6).nom = "Impulsion"
                essaiData.indices(7).nom = "Force Moy"
                essaiData.indices(8).nom = "Z_ajustée"
                essaiData.indices(9).nom = "P_ajustée"
                essaiData.indices(10).nom = "pente H"
                essaiData.indices(11).nom = "pente P"
                

            Case "FVcmj"
                essaiData.test = typeTest
                essaiData.nbIndices = 14
                ReDim essaiData.indices(13)
                essaiData.indices(0).nom = "Charge (kg)"
                essaiData.indices(1).nom = "Tv (s)"
                essaiData.indices(2).nom = "Zmax (m)"
                essaiData.indices(3).nom = "Hauteur Bosco (m)"
                essaiData.indices(4).nom = "Vitesse Envol (m/s)"
                essaiData.indices(5).nom = "Vitesse Bosco (m/s)"
                essaiData.indices(6).nom = "Vitesse moy (m/s)"
                essaiData.indices(7).nom = "Force moy (N)"
                essaiData.indices(8).nom = "Temps à Pmax (s)"
                essaiData.indices(9).nom = "Phase positive (s)"
                essaiData.indices(10).nom = "Pmax Brute (W)"
                essaiData.indices(11).nom = "Pmax Spécifique  (W/kg)"
                essaiData.indices(12).nom = "Pmoy Brute (W)"
                essaiData.indices(13).nom = "Pmoy Spécifique  (W/kg)"
        End Select

    End Sub
    Sub calculCMJSJ(ByRef essai As Essai)
        '/*********************************************************
        '/      Calcul des paramètres issus des test CMJ
        '/*********************************************************
        '/  Variables d'entrée :
        '/        essai : une variable de type structure essai
        '/
        '/  Variables de sortie : aucune directement
        '/      les modifications sont apportées à essai
        '/
        '/*******************************************************
        '/*** modifié le 31 mars 2015 par G Dalleau
        '/***  commenté le 08 août 2015
        '/*******************************************************

        '******* Déclaration des variables *********************
        Dim Masse As Single 'masse totale sujet + charge
        Dim nbPoints As Integer 'nb de points du signal enregistré

        'Dim Charge As Single 'masse de la charge
        Dim BodyMass As Single 'masse du sujet

        Dim freq As Double 'fréquence d'échantillonnage
        Dim dt As Double 'période d'échantillonnage

        Dim freq_Filtrage As Double 'fréquence de coupure
        Dim ordreFiltre As Integer 'ordre du filtre de ButterWorth
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer


        Dim T1 As Integer 'instants de decollage (F<seuilFz1)
        Dim T2 As Integer ' instants d'atterrissage (F>seuilFz1)
        Dim T0 As Integer 'début de contre mouvement (T0)

        'paramètres calculés
        Dim Tvol As Single

        Dim PuissAbsPeak As Single 'puissance brute en W
        Dim PuissRelPeak As Single 'puissance relative en W/kg
        Dim PuissAbsMean As Single 'puissance brute en W
        Dim PuissRelMean As Single 'puissance relative en W/kg mars 

        Dim Zmax As Single      ' hauteur max par double intégration
        Dim VitEnvol As Single  'vitesse de décollage
        Dim VitBosco As Single  ' 'vitese de décollage suiant la "méthode de Bosco v=1/2*g*t^2

        Dim Hauteur As Single   'hauteur de saut estimée
        ' Dim ForceMoy As Single
        'Dim VitMoy As Single     '

        Dim PhaseVol As String
        Dim msgTest As String
        '------------------------------------------------------------



        '// Quelques réglages avant le traitement
        BodyMass = essai.fic.masse
        freq = essai.fic.fs
        dt = 1 / freq
        freq_Filtrage = 40
        ordreFiltre = 2

        'Récupération des données de force
        'mode de sélection à changer probablement

        nbPoints = essai.fic.nbpoints

        i = 1

        'Filtrage 
        'REM_GD
        essai.Signaux.Force = Filtrage_PasseBas(freq_Filtrage, ordreFiltre, freq, nbPoints, essai.Signaux.Force)
        'Call TTSI2.FILTRE(0, 20, 1000, nbPoints, Force)


        'Calcul de la masse sur 400 premiers points après les 10 points
        For i = 11 To 410
            Masse = Masse + essai.Signaux.Force(i)
        Next i
        Masse = Masse / 400 / 9.81

        'MsgBox("Masse totale estimée : " + CStr(Masse) + vbCrLf + "Masse du sujet : " + CStr(BodyMass))


        k = 1

        'detection du front bas (T1 : début de la phase de vol)
        T1 = 0
        'REM_GD erreur ici

        j = 1
        For i = 1 To nbPoints - 3
            If essai.Signaux.Force(i) > seuilFz1 And essai.Signaux.Force(i + 1) < seuilFz1 And essai.Signaux.Force(i + 2) < seuilFz1 Then
                For k = i To nbPoints - 2 'on affine la recherche du seuil
                    If essai.Signaux.Force(k) > seuilFz2 And essai.Signaux.Force(k + 1) < seuilFz2 Then
                        T1 = k 'début du front bas donc de la phase de vol
                        Exit For
                    End If
                Next k
                Exit For
            End If
        Next i
        If T1 = 0 Then
            PhaseVol = "rien"
            msgTest = "Pas de décollage"

        End If

        If T1 <> 0 Then
            'traitement de la phase de vol
            'detection du front haut (T2 : fin de la phase de vol)
            For i = T1 + 1 To nbPoints - 3 'on cherche à partir du  front bas
                If essai.Signaux.Force(i) < seuilFz1 And essai.Signaux.Force(i + 1) > seuilFz1 And essai.Signaux.Force(i + 2) > seuilFz1 Then

                    For k = i To T1 Step -1 'on affine la recherche du seuil
                        If essai.Signaux.Force(k) < seuilFz2 And essai.Signaux.Force(k + 1) > seuilFz2 Then
                            T2 = k 'début du front haut donc de la phase de vol
                            Exit For
                        End If
                    Next k
                    Exit For
                End If

                'If essai.Signaux.Force(i) < seuilFz2 And essai.Signaux.Force(i + 1) > seuilFz2 And essai.Signaux.Force(i + 2) > seuilFz2 Then
                '    T2 = i
                '    Exit For
                'End If
            Next i

            'REM_GD attention il faut trouver T2

            'mise à 0 les valeurs de forcce pendant la phase de vol (entre T1 et T2)
            For i = T1 + 1 To T2
                essai.Signaux.Force(i) = 0
            Next i
        End If

        ' detection de l'instant de début de contre mouvement (T0)
        T0 = 0

        For i = 1 To T1 'c'est problématique si le signal n'est pas parfait
            If essai.Signaux.Force(i) > Masse * 9.81 * 0.95 And essai.Signaux.Force(i + 20) < Masse * 9.81 * 0.95 And essai.Signaux.Force(i + 30) < Masse * 9.81 * 0.95 Then
                T0 = i
                Exit For
            End If
        Next i
        'REM_GD on peut mettre une alerte
        ' met les valeurs de départ avant le contre mouvement à la valeur du poids pour éviter la dérive de la vitesse (de i=1 à TO)
        For i = 0 To T0
            essai.Signaux.Force(i) = Masse * 9.81
        Next i

        ' calcul de l'accélération
        For i = 0 To nbPoints - 1
            essai.Signaux.Acceleration(i) = (essai.Signaux.Force(i) - Masse * 9.81) / Masse
        Next i

        i = 1       

        'calcul de la vitesse
        '// TTSI2.Primitive(Vitesse(), freq, 1, nbPoints) 'calcul de la vitesse
        'SPECIAL essai.Signaux.Vitesse = Integre(essai.Signaux.Acceleration, dt, 0, essai.Signaux.Acceleration(nbPoints - 1))
     
        'essai.Signaux.Vitesse = TTSI.Primitive(essai.Signaux.Acceleration, dt, 0)
        essai.Signaux.Vitesse = Integre(essai.Signaux.Acceleration, dt, 0, essai.Signaux.Acceleration(nbPoints - 1))
        

        'VitMoy = 0
        ''calcul de la vitese moyenne
        'For i = 0 To nbPoints - 1
        '    VitMoy = VitMoy + essai.Signaux.Vitesse(i)
        'Next i
        'VitMoy = VitMoy / nbPoints

        'Correction des vitesses en fonction de Vmoy en théorie on doit avoir Vmoy=0
        'For i = 0 To nbPoints - 1
        '    essai.Signaux.Vitesse(i) = essai.Signaux.Vitesse(i) - VitMoy
        'Next i

        'Filtrage Vitesse, 1000, 20 'filtrage à 
        essai.Signaux.Vitesse = Filtrage_PasseBas(freq_Filtrage, ordreFiltre, freq, nbPoints, essai.Signaux.Vitesse)
        'essai.Signaux.Vitesse = TTSI.Filtrage_PasseBande(0.1, freq_Filtrage, ordreFiltre, freq, nbPoints, essai.Signaux.Vitesse)


        'Calcul de la position
        'essai.Signaux.Position = TTSI.Primitive(essai.Signaux.Vitesse, dt, 0)
        essai.Signaux.Position = Integre(essai.Signaux.Vitesse, dt, 0, essai.Signaux.Vitesse(nbPoints - 1))

        'Filtrage de la position
        'essai.Signaux.Position = Filtrage_PasseBas(freq_Filtrage, ordreFiltre, freq, nbPoints, essai.Signaux.Position)
        essai.Signaux.Position = TTSI.Filtrage_PasseBande(0.1, freq_Filtrage, ordreFiltre, freq, nbPoints, essai.Signaux.Position)
        i = 0
       
        For i = 0 To nbPoints - 1
            essai.Signaux.Puissance(i) = essai.Signaux.Force(i) * essai.Signaux.Vitesse(i)
        Next i


        
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
        Tvol = (T2 - T1) * dt

        ' Recherche de Zmax à partir des positions
        Zmax = 0
        For i = T1 + 1 To T2 - 1
            If essai.Signaux.Position(i) > Zmax Then
                Zmax = essai.Signaux.Position(i)
            End If
        Next i
        Zmax = Zmax - essai.Signaux.Position(T1)

        'Calcul de la hauteur à partir de la formule de Bosco
        Hauteur = 1 / 8 * 9.81 * Tvol ^ 2
        'MsgBox(CStr(Hauteur))
        ' Calcul de la vitesse d'envol à partir de la formule de Bosco
        VitBosco = 9.81 * Tvol / 2

        ' Recherche de la vitesse d'envol
        VitEnvol = essai.Signaux.Vitesse(T1)

        ' Recherche des puissances maximales
        ' Elle se fait avant l'envol

        For i = 100 To T1 'on commence un peu plus loin pour éviter les effets de bord
            If essai.Signaux.Puissance(i) > PuissAbsPeak Then
                PuissAbsPeak = essai.Signaux.Puissance(i)
                T_PMax = i
            End If
        Next i
        PuissRelPeak = PuissAbsPeak / Masse

        '/*** A partir de la valeur max de puissance
        ' on calcule la puissance moyenne positive une fois en incrémentant i
        i = T_PMax
        Do While essai.Signaux.Puissance(i) > 0
            PuissAbsMean = PuissAbsMean + essai.Signaux.Puissance(i)
            T2_PuissPositive = i 'fin de la puissance positive, ce qui est théoriquement la fin de la phase de vol
            i = i + 1
            If i = T1 + 1 Then
                Exit Do
            End If
        Loop
        ' on calcule la Puissance moyenne positive une fois en décrémentant i
        i = T_PMax - 1
        Do While essai.Signaux.Puissance(i) > 0
            PuissAbsMean = PuissAbsMean + essai.Signaux.Puissance(i)
            T1_PuissPositive = i 'début de la puissance positive
            i = i - 1
        Loop
        PositiveTime = T2_PuissPositive - T1_PuissPositive + 1 'en nombre de points
        PuissAbsMean = PuissAbsMean / PositiveTime

        PositiveTime = PositiveTime * dt 'phase de puissance positive
        PuissRelMean = PuissAbsMean / Masse 'puissance moyenne relative à la masse

        '// REM_GD ici les paramètres enregistrés sous excel
        essai.indices(0).valeur = Tvol
        essai.indices(1).valeur = PuissRelPeak ' modif à la demande Jean 18/08 PuissRelMean
        essai.indices(2).valeur = PuissAbsPeak ' PuissAbsMean
        essai.indices(3).valeur = Zmax
        essai.indices(4).valeur = Hauteur
        
    End Sub
    Sub calculFVcmj(ByRef essai As Essai)
        '/*********************************************************
        '/      Calcul des paramètres issus des test CMJ
        '/*********************************************************
        '/  Variables d'entrée :
        '/        essai : une variable de type structure essai
        '/
        '/  Variables de sortie :
        '/
        '/*******************************************************
        '/*** modifié le 27 avril 2015 par G Dalleau
        '/*******************************************************
        ':::::::::::::::::::::::::::::::
        '******* Déclaration des variables *********************  
        Dim Masse As Single 'masse totale sujet + charge
        Dim nbPoints As Integer 'nb de points du signal enregistré

        Dim Charge As Single 'masse de la charge
        Dim BodyMass As Single 'masse du sujet

        Dim freq As Double 'fréquence d'échantillonnage
        Dim dt As Double 'période d'échantillonnage

        Dim freq_Filtrage As Double 'fréquence de coupure
        Dim ordreFiltre As Integer 'ordre du filtre de ButterWorth
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim n As Integer

        Dim T1 As Integer 'instants de decollage (F<seuilFz1)
        Dim T2 As Integer ' instants d'atterrissage (F>seuilFz1)
        'Dim T0 As Integer 'début de contre mouvement (T0)

        'paramètres calculés
        Dim Tvol As Single

        Dim PuissAbsPeak As Single 'puissance brute en W
        Dim PuissRelPeak As Single 'puissance relative en W/kg
        Dim PuissAbsMean As Single 'puissance brute en W
        Dim PuissRelMean As Single 'puissance relative en W/kg mars 

        Dim Zmax As Single
        Dim VitEnvol As Single 'vitesse de ddécollage
        Dim VitBosco As Single

        Dim Hauteur As Single 'hauteur de saut estimée
        Dim ForceMoy As Single
        Dim VitMoy As Single

        Dim PhaseVol As String
        Dim msgTest As String
        '------------------------------------------------------------

        '// Quelques réglages avant le traitement
        BodyMass = essai.fic.masse
        Charge = essai.fic.charge
        freq = essai.fic.fs
        dt = 1 / freq
        freq_Filtrage = 20
        ordreFiltre = 5

        'Récupération des données de force
        'mode de sélection à changer probablement

        nbPoints = essai.fic.nbpoints

        '********************************************************************

        i = 1    

        '**** ATTENTION : Filtrage ????

        'Calcul de la masse sur 400 premiers points après les 10 points
        For i = 11 To 410
            Masse = Masse + essai.Signaux.Force(i)
        Next i
        Masse = Masse / 400 / 9.81
        'Masse = Charge + BodyMass
        'MsgBox("Masse totale estimée : " + CStr(Masse))


        k = 1

        'detection du front bas
        t1 = 0
        j = 1
        For i = 1 To nbPoints - 3
            If essai.Signaux.Force(i) > seuilFz1 And essai.Signaux.Force(i + 1) < seuilFz1 And essai.Signaux.Force(i + 2) < seuilFz1 Then
                n = 0
                For k = i To nbPoints - 2 'on affine la recherche du seuil
                    If essai.Signaux.Force(k) > seuilFz2 And essai.Signaux.Force(k + 1) < seuilFz2 Then
                        T1 = k 'début du front bas donc de la phase de vol
                        Exit For
                    End If
                Next k
                Exit For
            End If
        Next i
        If T1 = 0 Then
            PhaseVol = "rien"
            msgTest = "Pas de décollage"

        End If
        If T1 <> 0 Then
            'traitement de la phase de vol
            'detection du front haut (T2 : fin de la phase de vol)
            For i = T1 + 1 To nbPoints - 3 'on cherche à partir du  front bas
                If essai.Signaux.Force(i) < seuilFz1 And essai.Signaux.Force(i + 1) > seuilFz1 And essai.Signaux.Force(i + 2) > seuilFz1 Then

                    For k = i To T1 Step -1 'on affine la recherche du seuil
                        If essai.Signaux.Force(k) < seuilFz2 And essai.Signaux.Force(k + 1) > seuilFz2 Then
                            T2 = k 'début du front haut donc de la phase de vol
                            Exit For
                        End If
                    Next k
                    Exit For
                End If

               
            Next i

            'mise à 0 les valeurs de forcce pendant la phase de vol (entre T1 et T2)
            For i = T1 + 1 To T2
                essai.Signaux.Force(i) = 0
            Next i
        End If

        'REM_GD
        '****** correction de la force par rapport au poids
        '**********  si le sujet reste sur la plateforme immobile avant le saut et après
        '**********  alors l'intégrale de la force doit donner le poids
        ForceMoy = 0
        For i = 0 To nbPoints - 1
            ForceMoy = ForceMoy + essai.Signaux.Force(i)
        Next i
        ForceMoy = ForceMoy / nbPoints
        'MsgBox(CStr(ForceMoy))

        For i = 0 To nbPoints - 1
            essai.Signaux.Force(i) = essai.Signaux.Force(i) / ForceMoy * Masse * 9.81
        Next i
        ' '**********************************

        ' calcul de l'accélération
        For i = 0 To nbPoints - 1
            essai.Signaux.Acceleration(i) = (essai.Signaux.Force(i) - Masse * 9.81) / Masse
        Next i

        i = 1
     

        'calcul de la vitesse
        'TTSI2.Primitive(Vitesse(), freq, 1, nbPoints) 'calcul de la vitesse

        essai.Signaux.Vitesse = Integre(essai.Signaux.Acceleration, dt, 0, essai.Signaux.Acceleration(nbPoints - 1))

        VitMoy = 0
        For i = 0 To nbPoints - 1
            VitMoy = VitMoy + essai.Signaux.Vitesse(i)
        Next i
        VitMoy = VitMoy / nbPoints
        'MsgBox ("Vitesse moy sur l'ensemble de l'acquisition" + CStr(VitMoy))
        'il faut aussi corriger la vitesse a priori


        'filtrage de la vitesse
        'Filtrage Vitesse, 1000, 20 'filtrage à 20Hz
        'Call TTSI2.FILTRE(0.1, 20, 1000, nbPoints, Vitesse)
        essai.Signaux.Vitesse = Filtrage_PasseBas(freq_Filtrage, 5, freq, nbPoints, essai.Signaux.Vitesse)

        'Calcul de la position
        'REM_GD  TTSI2.Primitive(Position(), freq, 1, nbPoints) 'calcul de la position
        essai.Signaux.Position = Integre(essai.Signaux.Vitesse, dt, 0, essai.Signaux.Vitesse(nbPoints - 1))

        essai.Signaux.Position = Filtrage_PasseBas(freq_Filtrage, 5, freq, nbPoints, essai.Signaux.Position)
       
        i = 0

        For i = 0 To nbPoints - 1
            essai.Signaux.Puissance(i) = essai.Signaux.Force(i) * essai.Signaux.Vitesse(i)
        Next i

        

        '/************************************************************************
        '/******* Extraction de certains paramètres à partir
        '/******* des courbes force, vitesse et puissance
        '/************************************************************************

        Dim T_PMax As Integer
        Dim T1_PuissPositive As Integer
        Dim T2_PuissPositive As Integer

        Dim PositiveTime As Double

        PuissAbsPeak = 0
        PuissAbsMean = 0
        PuissRelPeak = 0
        PuissRelMean = 0

        If PhaseVol <> "rien" Then
            ' calcul des différents paramètres
            Tvol = (T2 - t1) * dt
            Zmax = 0
            For i = T1 + 1 To T2 - 1
                If essai.Signaux.Position(i) > Zmax Then
                    Zmax = essai.Signaux.Position(i)
                End If
            Next i
            Zmax = Zmax - essai.Signaux.Position(T1)

            'Calcul de la hauteur à partir de la formule de Bosco
            Hauteur = 1 / 8 * 9.81 * Tvol ^ 2

            ' Calcul de la vitesse d'envol à partir de la formule de Bosco
            VitBosco = 9.81 * Tvol / 2

            ' Recherche de la vitesse d'envol
            VitEnvol = essai.Signaux.Vitesse(T1)

            For i = 100 To T1 'on commence un peu plus loin pour éviter les effets de bord
                If essai.Signaux.Puissance(i) > PuissAbsPeak Then
                    PuissAbsPeak = essai.Signaux.Puissance(i)
                    T_PMax = i
                End If
            Next i
            PuissRelPeak = PuissAbsPeak / BodyMass


            '/*** A partir de la valeur max de puissance
            ' on calcule la puissance moyenne positive une fois en incrémentant i
            i = T_PMax
            Do While essai.Signaux.Puissance(i) > 0
                PuissAbsMean = PuissAbsMean + essai.Signaux.Puissance(i)
                T2_PuissPositive = i 'fin de la puissance positive, ce qui est théoriquement la fin de la phase de vol
                i = i + 1
                If i = T1 + 1 Then
                    Exit Do
                End If
            Loop
            ' on calcule la puissance moyenne positive une fois en décrémentant i
            i = T_PMax - 1
            Do While essai.Signaux.Puissance(i) > 0
                PuissAbsMean = PuissAbsMean + essai.Signaux.Puissance(i)
                T1_PuissPositive = i 'début de la puissance positive
                i = i - 1
            Loop
            PositiveTime = T2_PuissPositive - T1_PuissPositive + 1 'en nombre de points
            PuissAbsMean = PuissAbsMean / PositiveTime

            VitMoy = 0
            For i = T1_PuissPositive To T2_PuissPositive
                VitMoy = essai.Signaux.Vitesse(i) + VitMoy
            Next i
            VitMoy = VitMoy / PositiveTime

            'MsgBox("T1 =" + CStr(T1_PuissPositive) + "/ T2 =" + CStr(T2_PuissPositive))
            ForceMoy = 0
            For i = T1_PuissPositive To T2_PuissPositive
                ForceMoy = ForceMoy + essai.Signaux.Force(i)
            Next i
            ForceMoy = ForceMoy / PositiveTime
            'MsgBox("Force Moyenne = " + CStr(ForceMoy))
            PositiveTime = PositiveTime * dt

            PuissRelMean = PuissAbsMean / BodyMass
           

            '// REM_GD ici les paramètres enregistrés sous excel

            essai.indices(0).valeur = Charge
            essai.indices(1).valeur = Tvol
            essai.indices(2).valeur = Zmax
            essai.indices(3).valeur = Hauteur
            essai.indices(4).valeur = VitEnvol
            essai.indices(5).valeur = VitBosco
            essai.indices(6).valeur = VitMoy
            essai.indices(7).valeur = ForceMoy
            essai.indices(8).valeur = T_PMax
            essai.indices(9).valeur = PositiveTime

            essai.indices(10).valeur = PuissAbsPeak
            essai.indices(11).valeur = PuissRelPeak
            essai.indices(12).valeur = PuissAbsMean
            essai.indices(13).valeur = PuissRelMean


        Else
            'Il n'y a pas de phase de vol 
            'PhaseVol = "rien"

            For i = 100 To nbPoints - 1 'on commence un peu plus loin pour éviter les effets de bord
                If essai.Signaux.Puissance(i) >= PuissAbsPeak Then
                    PuissAbsPeak = essai.Signaux.Puissance(i)
                    T_PMax = i
                End If
            Next i
            PuissRelPeak = PuissAbsPeak / BodyMass

            '/*** A partir de la valeur max de puissance
            ' on calcule la puissance moyenne positive une fois en incrémentant i
            i = T_PMax
            Do While essai.Signaux.Puissance(i) > 0
                PuissAbsMean = PuissAbsMean + essai.Signaux.Puissance(i)
                T2_PuissPositive = i 'fin de la puissance positive
                i = i + 1
                If i = nbPoints Then
                    Exit Do
                End If
            Loop
            ' on calcule la puissance moyenne positive une fois en décrémentant i
            i = T_PMax - 1
            Do While essai.Signaux.Puissance(i) > 0
                PuissAbsMean = PuissAbsMean + essai.Signaux.Puissance(i)
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
                VitMoy = essai.Signaux.Vitesse(i) + VitMoy
            Next i
            VitMoy = VitMoy / PositiveTime

            ForceMoy = 0
            For i = T1_PuissPositive To T2_PuissPositive
                ForceMoy = ForceMoy + essai.Signaux.Force(i)
            Next i
            ForceMoy = ForceMoy / PositiveTime

            PositiveTime = PositiveTime * dt
            PuissRelMean = PuissAbsMean / BodyMass

            '// REM_GD ici les paramètres enregistrés sous excel
            essai.indices(0).valeur = Charge
            essai.indices(1).valeur = -1
            essai.indices(2).valeur = -1
            essai.indices(3).valeur = -1
            essai.indices(4).valeur = -1
            essai.indices(5).valeur = -1
            essai.indices(6).valeur = VitMoy
            essai.indices(7).valeur = ForceMoy
            essai.indices(8).valeur = T_PMax
            essai.indices(9).valeur = PositiveTime
            essai.indices(10).valeur = PuissAbsPeak
            essai.indices(11).valeur = PuissRelPeak
            essai.indices(12).valeur = PuissAbsMean
            essai.indices(13).valeur = PuissRelMean
        End If

    End Sub
    Sub CalculsRJ(ByRef essai As Essai)
        '/*********************************************************
        '/      Calcul des paramètres issus des test RJ
        '/*********************************************************
        '/  Variables d'entrée :
        '/        essai : une variable de type structure essai
        '/
        '/  Variables de sortie :
        '/
        '/*******************************************************
        '/*** modifié le 21 avril 2015 par G Dalleau
        '/*******************************************************


        '******* Déclaration des variables *********************
        ' Dim Masse As Single 'masse totale sujet + charge
        Dim nbPoints As Integer 'nb de points du signal enregistré

        'Dim Charge As Single 'masse de la charge
        Dim BodyMass As Single 'masse du sujet

        Dim freq As Double 'fréquence d'échantillonnage
        Dim dt As Double 'période d'échantillonnage

        Dim freq_Filtrage As Double 'fréquence de coupure
        Dim ordreFiltre As Integer 'ordre du filtre de ButterWorth
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        'Dim n As Integer

        Dim T1(60) As Integer 'instants de decollage (F<seuilFz1)
        Dim T2(60) As Integer ' instants d'atterrissage (F>seuilFz1)
       

        Dim nbsauts As Integer
        Dim nbFrBas As Integer
        Dim nbFrHauts As Integer


        'REM_GD paramètres calculés - ici le nombre 60 de valeurs devrait être calculé
        Dim Tvol(60) As Double
        Dim TvolMoy(60) As Double
        Dim Tcontact(60) As Double
        Dim Ttotal(60) As Double
        Dim PuissBosco(60) As Double
        Dim Zmax(60) As Double
        Dim Impulsion(60) As Double
        Dim ForceMoy(60) As Double


        '********************************************************************

        '// Quelques réglages avant le traitement
        BodyMass = essai.fic.masse
        freq = essai.fic.fs
        dt = 1 / freq
        freq_Filtrage = 10 'avant c'était 20 sur LEFM.xls
        ordreFiltre = 2
        nbPoints = essai.fic.nbpoints

        'Filtrage de la force
        essai.Signaux.Force = Filtrage_PasseBas(freq_Filtrage, ordreFiltre, freq, nbPoints, essai.Signaux.Force)
        'Call TTSI2.FILTRE(0, 20, 1000, nbPoints, Force)

        k = 1

        'Detection des fronts bas
        T1(0) = 0
        j = 1
        i = 1
        While i < nbPoints - 3
            'For i = 1 To nbPoints - 3
            If essai.Signaux.Force(i) > seuilFz1 And essai.Signaux.Force(i + 1) < seuilFz1 And essai.Signaux.Force(i + 2) < seuilFz1 Then
                For k = i To nbPoints - 2 'on affine la recherche du seuil
                    If essai.Signaux.Force(k) > seuilFz2 And essai.Signaux.Force(k + 1) < seuilFz2 Then
                        T1(j) = k
                        i = k
                        j = j + 1
                        Exit For
                    End If
                Next k

            End If
            i = i + 1

        End While

        nbFrBas = j - 1
       
        'Detection des fronts hauts
        i = T1(1)
        j = 1

        While i < nbPoints - 3           
            'ci-dessous on détecte à nouveau un seuil haut puis on va en arrière
            If essai.Signaux.Force(i) < seuilFz1 And essai.Signaux.Force(i + 1) > seuilFz1 And essai.Signaux.Force(i + 2) > seuilFz1 Then
                k = i + 1 'GD attention modification remplacement de k=i par k=i+1
                While essai.Signaux.Force(k) > seuilFz2
                    T2(j) = k
                    k = k - 1
                End While
                If T2(j) <> T2(j - 1) Then
                    j = j + 1
                End If

            End If
            i = i + 1
        End While

        nbFrHauts = j - 1

        If nbFrBas > nbFrHauts Then
            nbFrBas = nbFrBas - 1 'le nbFrHauts doit être = nbFrBas
            If nbFrBas <> nbFrHauts Then
                MsgBox("Veuillez régler les seuils : Erreur dans la détection des sauts")
            End If
        End If

        ' calcul des différents paramètres
        For i = 1 To nbFrBas - 1           
            Tvol(i) = (T2(i) - T1(i)) * dt
            Tcontact(i) = (T1(i + 1) - T2(i)) * dt
            'GD ici calcul de la moyenne
            ForceMoy(i) = TTSI.Moyenne(essai.Signaux.Force, freq, T2(i), T1(i + 1))
            Impulsion(i) = ForceMoy(i) * Tcontact(i)
            Zmax(i) = 1 / 8 * 9.81 * Tvol(i) ^ 2
        Next i
        Tvol(nbFrBas) = (T2(nbFrBas) - T1(nbFrBas)) * dt
        Zmax(nbFrBas) = 1 / 8 * 9.81 * Tvol(nbFrBas) ^ 2

        For i = 1 To nbFrBas - 1
            TvolMoy(i) = (Tvol(i) + Tvol(i + 1)) / 2
            Ttotal(i) = TvolMoy(i) + Tcontact(i)
            PuissBosco(i) = 9.81 ^ 2 * TvolMoy(i) * Ttotal(i) / (4 * Tcontact(i))
        Next i

        nbsauts = nbFrBas - 1
        essai.nbPaquetsIndices = nbsauts

        For i = 0 To essai.indices.Count - 1
            ReDim essai.indices(i).tabValeurs(nbsauts)
            'REM_GD on pourrait ajouter ici les valeurs moyennes
        Next
        '// REM_GD ici les paramètres enregistrés sous excel
        ReDim Preserve essai.indices(0).tabValeurs(nbsauts - 1) 'attention cela commence à l'indice "zéro" et finit à "nbsauts"
        ReDim Preserve essai.indices(1).tabValeurs(nbsauts - 1)
        ReDim Preserve essai.indices(2).tabValeurs(nbsauts - 1)
        ReDim Preserve essai.indices(3).tabValeurs(nbsauts - 1)
        ReDim Preserve essai.indices(4).tabValeurs(nbsauts - 1)
        ReDim Preserve essai.indices(5).tabValeurs(nbsauts - 1)
        ReDim Preserve essai.indices(6).tabValeurs(nbsauts - 1)
        ReDim Preserve essai.indices(7).tabValeurs(nbsauts - 1)
        ReDim Preserve essai.indices(8).tabValeurs(nbsauts - 1)
        ReDim Preserve essai.indices(9).tabValeurs(nbsauts - 1)

        For i = 0 To nbsauts - 1
            'REM_GD Attention ici, il faut respeter l'ordre des paramètres
            'le 12/08/2015 j'ai ici modifié l'indice des tableaux à droite en i+1
            'cf avec le calculK
            essai.indices(0).tabValeurs(i) = Tvol(i + 1)
            essai.indices(1).tabValeurs(i) = TvolMoy(i + 1)
            essai.indices(2).tabValeurs(i) = Tcontact(i + 1)
            essai.indices(3).tabValeurs(i) = Ttotal(i + 1)
            essai.indices(4).tabValeurs(i) = PuissBosco(i + 1)
            essai.indices(5).tabValeurs(i) = Zmax(i + 1)
            essai.indices(6).tabValeurs(i) = Impulsion(i + 1)
            essai.indices(7).tabValeurs(i) = ForceMoy(i + 1)
        Next
        'ajustement des courbes
        '               ajustement de la hauteur
        Dim numSamples As Integer = nbsauts
        Dim numerosaut(nbsauts - 1) As Double
        Dim zMax0(nbsauts - 1) As Double
        Dim zAjustee(nbsauts - 1) As Double
        Dim Puiss0(nbsauts - 1) As Double
        Dim PuissAjustee(nbsauts - 1) As Double

        'ici à vérifier l'ajustement
        For i = 0 To nbsauts - 1
            numerosaut(i) = i 'GD à vérifier ici s'il faut mettre i ou i+1
            zMax0(i) = Zmax(i + 1)
            Puiss0(i) = PuissBosco(i + 1)
        Next
        Dim coeffHauteur() As Double = Nothing
        Dim rSquareHauteur As Double
        Dim coeffPuissance() As Double = Nothing
        Dim rSquarePuissance As Double

        zAjustee = AjustementPolynomial(numerosaut, zMax0, numSamples, 1, coeffHauteur, rSquareHauteur)
        PuissAjustee = AjustementPolynomial(numerosaut, Puiss0, numSamples, 1, coeffPuissance, rSquarePuissance)

        'REM_GD manque le calcul de la puissance ajustée
        'modifié ici aussi
        For i = 0 To nbsauts - 1
            essai.indices(8).tabValeurs(i) = zAjustee(i)
            essai.indices(9).tabValeurs(i) = PuissAjustee(i) 'mettre ici la puissance
        Next
        '               
        For i = 0 To essai.indices.Count - 1
            essai.indices(i).valeur = Statistiques.Moyenne(essai.indices(i).tabValeurs)
            'REM_GD on pourrait ajouter ici les valeurs moyennes
        Next
        essai.indices(10).valeur = coeffHauteur(1)
        essai.indices(11).valeur = coeffPuissance(1)

        'REM_GD à quoi ça sert ?

        Dim Pmaxi As Double
        Dim Pmini As Double
        Pmaxi = Puiss0(0)
        Pmini = Puiss0(0)
        For i = 1 To Puiss0.Length - 1
            If Puiss0(i) > Pmaxi Then
                Pmaxi = Puiss0(i)
            End If
            If Puiss0(i) < Pmini Then
                Pmini = Puiss0(i)
            End If
        Next

        Dim Hmaxi As Double
        Dim Hmini As Double
        Hmaxi = Puiss0(0)
        Hmini = Puiss0(0)
        For i = 1 To zMax0.Length - 1
            If zMax0(i) > Hmaxi Then
                Hmaxi = zMax0(i)
            End If
            If zMax0(i) < Hmini Then
                Hmini = zMax0(i)
            End If
        Next

    End Sub
    Sub CalculK(ByRef essai As Essai)
        '/********** Variables d'entrée
        '    numCol numéro de la colonne à traiter

        '/**** DECLARATION DES VARIABLES ****/


        '::::::
        'Dim Masse As Single 'masse totale sujet + charge
        Dim nbPoints As Integer 'nb de points du signal enregistré

        'Dim Charge As Single 'masse de la charge
        Dim BodyMass As Single 'masse du sujet

        Dim freq As Double 'fréquence d'échantillonnage
        Dim dt As Double 'période d'échantillonnage

        Dim freq_Filtrage As Double 'fréquence de coupure
        Dim ordreFiltre As Integer 'ordre du filtre de ButterWorth
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim n As Integer

        Dim T1(60) As Integer 'instants de decollage (F<seuilFz1)
        Dim T2(60) As Integer ' instants d'atterrissage (F>seuilFz1)


        Dim nbsauts As Integer
        ' Dim nbsautK As Integer
        Dim nbFrBas As Integer
        Dim nbFrHauts As Integer


        'REM_GD paramètres calculés - ici le nombre 60 de valeurs devrait être calculé
        Dim Tvol(60) As Double
        Dim TvolMoy(60) As Double
        Dim Tcontact(60) As Double
        Dim Ttotal(60) As Double
        Dim PuissBosco(60) As Double
        Dim Zmax(60) As Double
        Dim Impulsion(60) As Double
        Dim ForceMoy(60) As Double
        Dim PuissK(60) As Double
        Dim RaideurK(60) As Double
        Dim RaideurKabs(60) As Double

        'Paramètres moyens
        Dim PuissMean As Single
        Dim KMeanAbs As Single
        Dim KMeanRel As Single
        Dim ZmaxMean As Single
        Dim ImpulseMean As Single
        '********************************************************************

        '// Quelques réglages avant le traitement
        BodyMass = essai.fic.masse
        freq = essai.fic.fs
        dt = 1 / freq
        freq_Filtrage = 20
        ordreFiltre = 5
        nbPoints = essai.fic.nbpoints



        '********************************************************************

        'QUESTION : Faut-il filtrer ?
        'Call TTSI2.FILTRE(0, 20, freq, nbPoints, Force)
        'nbsautK = 20

        k = 1

        'Detection des fronts bas
        T1(0) = 0
        j = 1
        For i = 1 To nbPoints - 3
            If essai.Signaux.Force(i) > seuilFz1 And essai.Signaux.Force(i + 1) < seuilFz1 And essai.Signaux.Force(i + 2) < seuilFz1 Then
                n = 0
                For k = i To nbPoints - 2 'on affine la recherche du seuil
                    If essai.Signaux.Force(k) > seuilFz2 And essai.Signaux.Force(k + 1) < seuilFz2 Then
                        T1(j) = k
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

        For i = T1(1) To nbPoints - 3  'on cherche à partir du premier front bas
            'ci-dessous on détecte à nouveau un seuil haut puis on va en arrière
            If essai.Signaux.Force(i) < seuilFz1 And essai.Signaux.Force(i + 1) > seuilFz1 And essai.Signaux.Force(i + 2) > seuilFz1 Then
                k = i + 1 'GD attention modification remplacement de k=i par k=i+1
                While essai.Signaux.Force(k) > seuilFz2
                    T2(j) = k
                    k = k - 1
                End While
                j = j + 1

            End If
        Next i

        nbFrHauts = j - 1

        If nbFrBas > nbFrHauts Then
            nbFrBas = nbFrBas - 1 'le nbFrHauts doit être = nbFrBas
            If nbFrBas <> nbFrHauts Then
                MsgBox("Veuillez régler les seuils : Erreur dans la détection des sauts")
            End If
        End If

        ' calcul des différents paramètres

        For i = 1 To nbFrBas - 1
            'Tvol(i) = (t2(i) - T1(i) + t2(i + 1) - T1(i + 1)) / 2
            Tvol(i) = (T2(i) - T1(i)) * dt
            Tcontact(i) = (T1(i + 1) - T2(i)) * dt
            Impulsion(i) = TTSI.Moyenne(essai.Signaux.Force, freq, T2(i), T1(i + 1))
            Zmax(i) = 1 / 8 * 9.81 * Tvol(i) ^ 2
        Next i
        Tvol(nbFrBas) = (T2(nbFrBas) - T1(nbFrBas)) * dt
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
        KMeanAbs = KMeanRel * BodyMass

        For j = 1 To nbFrBas - 1
            PuissMean = PuissMean + PuissK(j)
        Next j
        PuissMean = PuissMean / (nbFrBas - 1)

        For j = 1 To nbFrBas - 1
            ImpulseMean = ImpulseMean + Impulsion(j)
        Next j
        ImpulseMean = ImpulseMean / (nbFrBas - 1)

        nbsauts = nbFrBas
        For i = 0 To essai.indices.Count - 1
            ReDim essai.indices(i).tabValeurs(nbsauts - 1)
            'REM_GD on pourrait ajouter ici les valeurs moyennes
        Next
        '// REM_GD ici les paramètres enregistrés sous excel
        For i = 0 To nbsauts - 1
            'REM_GD Attention ici, il faut respeter l'ordre des paramètres
            essai.indices(0).tabValeurs(i) = PuissK(i + 1)
            essai.indices(1).tabValeurs(i) = RaideurK(i + 1) * BodyMass
            essai.indices(2).tabValeurs(i) = RaideurK(i + 1)
            essai.indices(3).tabValeurs(i) = Zmax(i + 1)
            essai.indices(4).tabValeurs(i) = Impulsion(i + 1)
        Next
        '// REM_GD ici les paramètres enregistrés sous excel
        essai.indices(0).valeur = PuissMean
        essai.indices(1).valeur = KMeanAbs
        essai.indices(2).valeur = KMeanRel
        essai.indices(3).valeur = ZmaxMean
        essai.indices(4).valeur = ImpulseMean

        essai.nbPaquetsIndices = nbsauts - 1

    End Sub
    Sub CalculIso(ByRef essai As Essai)

        '******* Déclaration des variables *********************

        Dim nbPoints As Integer 'nb de points du signal enregistré

        'Dim Charge As Single 'masse de la charge
        Dim BodyMass As Single 'masse du sujet

        Dim freq As Double 'fréquence d'échantillonnage
        Dim dt As Double 'période d'échantillonnage

        Dim freq_Filtrage As Double 'fréquence de coupure
        Dim ordreFiltre As Integer 'ordre du filtre de ButterWorth

        Dim i As Integer
        Dim iFmax As Integer
        Dim T0 As Integer
        Dim T_Fmax As Double

        Dim valeursRfd150() As Double

        Dim Fmax As Double
        Dim RFDmax As Double

        '********************************************************************

        '// Quelques réglages avant le traitement
        BodyMass = essai.fic.masse
        freq = essai.fic.fs
        dt = 1 / freq
        freq_Filtrage = 20
        ordreFiltre = 5

        'Récupération des données de force
        'mode de sélection à changer probablement

        nbPoints = essai.fic.nbpoints

        Dim ForceTmp(nbPoints) As Double


        'filtre median sur 1001 points (1 seconde)
        For i = 0 To 500
            ForceTmp(i) = essai.Signaux.Force(i)
        Next
        For i = 501 To nbPoints - 501
            ForceTmp(i) = TTSI.Moyenne(essai.Signaux.Force, freq, i - 500, i + 500)
        Next
        For i = nbPoints - 501 To nbPoints - 1
            ForceTmp(i) = essai.Signaux.Force(i)
        Next

        'recherche de la valeur max
        Fmax = 0
        For i = 0 To nbPoints - 501
            If ForceTmp(i) > Fmax Then
                Fmax = ForceTmp(i)
                iFmax = i
            End If
        Next i

        'filtrage de la force à 20Hz
        'essai.Signaux.Force = Filtrage_PasseBas(freq_Filtrage, ordreFiltre, freq, nbPoints, ForceTmp)

        'recherche du début de la montée en force
        i = iFmax
        'REM_GD problème si le signal est décalé ; pas de ligne zéro au début
        Do While ForceTmp(i) > 10 'seuil à 10N
            i = i - 1
        Loop
        T0 = i


        ReDim valeursRfd150(nbPoints)
        For i = 0 To nbPoints - 1
            valeursRfd150(i) = essai.Signaux.Force(i)
        Next

        'Calcul la dérivée
        valeursRfd150 = TTSI.derive_delta(valeursRfd150, 0.15, nbPoints, freq)
        essai.Signaux.Vitesse = valeursRfd150

        'Recherche du max en RFD
        RFDmax = 0
        For i = 0 To iFmax
            If valeursRfd150(i) > RFDmax Then
                RFDmax = valeursRfd150(i)
            End If
        Next
        T_Fmax = (iFmax - T0) * dt

        
        '// REM_GD ici les paramètres enregistrés sous excel
        essai.indices(0).valeur = Fmax
        essai.indices(1).valeur = T_Fmax
        essai.indices(2).valeur = RFDmax
        'récupère le type de courbe
        essai.indices(3).valeur = essai.fic.typecourbe

    End Sub
End Module
