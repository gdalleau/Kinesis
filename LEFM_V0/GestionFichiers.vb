Imports System
Imports System.IO
Module GestionFichiers

    Public Structure FichierDonnees
        Dim nomSujet As String 'nom du sujet
        Dim masse As Double 'masse du sujet
        Dim datemesure As Date 'date de la mesure
        Dim typetest As String 'type de test effectué
        Dim fs As Double 'frequence d'echantillonnage utilisee
        Dim nbpoints As Integer 'nbre de points du fichier
        Dim sig() As Double 'signal acquis

        'parametres supplementaires
        Dim numEssai As Integer 'numero d'essai
        Dim banc As Integer 'reglage banc pour ISO
        Dim pied As Integer 'reglage de pied p our ISO
        Dim typeISO As String '2 pieds D ou G
        Dim position As String 'couche ou debout pour iso
        Dim typecourbe As Integer '1,2,3 ou inconnu *// voir si cela est opportun
        Dim cmj As Double 'valeur de zmax en CMJ pour RJ *// à enlever certainement
        Dim charge As Double 'valeur de la charge soulevee pour FV
        'Dim etat_blessure As Boolean ' true = le sujet était blessé
        Dim nom_blessure As String 'nom de la blessure si le sujet n'est pas blessé mettre "neant"
        Dim reglage1 As Double 'réglage des angles pour le FVcmj
        Dim reglage2 As Double 'réglage des angles pour le FVcmj
        Dim saut_realise As String
        Dim puissRJ6sec As Double

    End Structure
    Sub LireFichier(ByRef fDonnees As FichierDonnees, ByVal nmFichier As String)
        '***********************************************************************
        ' Procédure pour lire un fichier de données
        ' fDonnees est une structure de type FichierDonnees
        ' nmFichier est le nom complet du fichier à lire
        ' il faudrait peut-être un autre type pour attraper facilement le chemin
        ' de façon à créer aussi le répertoire s'il n'existe pas
        '*** Rem : il faut construire le fDonnees avant appel à cette procédure
        '***********************************************************************



        'Dim rep As MsgBoxResult
        Dim n As Integer
        'Dim strTest As String
        Dim nomfichier As StreamReader = Nothing
        Dim code_fichier As String 'permet de vérifier l'historique du fichier

        If Not File.Exists(nmFichier) Then
            MsgBox("Erreur le fichier n'existe pas", MsgBoxStyle.OkOnly, "Avertissement")
            End
        End If
        Try

            nomfichier = New StreamReader(nmFichier)
            code_fichier = nomfichier.ReadLine

            If code_fichier <> "#NEW#" Then
                'ancienne version de fichier
                ' nomfichier.Close() 'on referme pour recommencer à la première ligne
                'nomfichier = New StreamReader(nmFichier)
                fDonnees.nomSujet = code_fichier 'code_ficher contient en fait la première ligne, soit le nom du sujet
                fDonnees.masse = nomfichier.ReadLine
                fDonnees.datemesure = nomfichier.ReadLine
                fDonnees.typetest = nomfichier.ReadLine
                fDonnees.nom_blessure = "neant"
                fDonnees.fs = nomfichier.ReadLine
                fDonnees.nbpoints = nomfichier.ReadLine
                ReDim fDonnees.sig(fDonnees.nbpoints - 1)

                Select Case fDonnees.typetest
                    Case "ISO"
                        '*** situation où il y a le plus de commentaires à ajouter au fichier
                        fDonnees.banc = nomfichier.ReadLine '** Trouver cette valeur dans le fDonnees
                        fDonnees.pied = nomfichier.ReadLine '*** anciennement numPied
                        fDonnees.typeISO = nomfichier.ReadLine
                        fDonnees.position = nomfichier.ReadLine
                        fDonnees.typecourbe = nomfichier.ReadLine
                    Case "FVcmj", "FVsj"
                        fDonnees.charge = nomfichier.ReadLine ' transformer selon fDonnees

                    Case "RJ"
                        fDonnees.cmj = nomfichier.ReadLine
                End Select
                For n = 0 To (fDonnees.nbpoints - 1)
                    fDonnees.sig(n) = CDbl(nomfichier.ReadLine)
                Next n
                nomfichier.Close()

            Else
                'Nouvelle version de fichier
                'MsgBox("Nouvelle version !!!")
                'nouvelle version qui commmence par #NEW#

                fDonnees.nomSujet = nomfichier.ReadLine
                fDonnees.masse = nomfichier.ReadLine
                fDonnees.datemesure = nomfichier.ReadLine
                fDonnees.typetest = nomfichier.ReadLine
                fDonnees.nom_blessure = nomfichier.ReadLine 'nouveauté
                'MsgBox(fDonnees.nom_blessure)
                fDonnees.fs = nomfichier.ReadLine
                fDonnees.nbpoints = nomfichier.ReadLine
                ReDim fDonnees.sig(fDonnees.nbpoints - 1)

                Select Case fDonnees.typetest
                    Case "ISO"
                        '*** situation où il y a le plus de commentaires à ajouter au fichier
                        fDonnees.banc = nomfichier.ReadLine '** Trouver cette valeur dans le fDonnees
                        fDonnees.pied = nomfichier.ReadLine '*** anciennement numPied
                        fDonnees.typeISO = nomfichier.ReadLine
                        fDonnees.position = nomfichier.ReadLine
                        fDonnees.typecourbe = nomfichier.ReadLine
                    Case "FVcmj", "FVsj"
                        fDonnees.charge = nomfichier.ReadLine ' transformer selon fDonnees
                        fDonnees.reglage1 = nomfichier.ReadLine
                        fDonnees.reglage2 = nomfichier.ReadLine
                        fDonnees.saut_realise = nomfichier.ReadLine
                    Case "RJ"
                        fDonnees.puissRJ6sec = nomfichier.ReadLine

                    Case "RJ6sec"
                        fDonnees.puissRJ6sec = nomfichier.ReadLine
                End Select
                For n = 0 To (fDonnees.nbpoints - 1)
                    fDonnees.sig(n) = CDbl(nomfichier.ReadLine)
                Next n
                nomfichier.Close()
            End If



        Catch ex As Exception
            MsgBox("Problème de lecture des données", MsgBoxStyle.OkOnly, "Avertissement")
        End Try



    End Sub

    Sub EcrireFichier(ByVal fDonnees As FichierDonnees, ByVal nmFichier As String)
        '***********************************************************************
        ' Procédure pour écrire un fichier de données
        ' fDonnees est une structure de type FichierDonnees
        ' nmFichier est le nom complet du fichier à enregister
        ' il faudrait peut-être un autre type pour attraper facilement le chemin
        ' de façon à créer aussi le répertoire s'il n'existe pas
        '*** Rem : il faut construire le fDonnees avant appel à cette procédure
        '***********************************************************************


        'Dim rep As MsgBoxResult
        Dim n As Integer
        'Dim strTest As String
        Dim nomfichier As StreamWriter = Nothing

        If Not File.Exists(nmFichier) Then
            Dim fs As Stream = File.Create(nmFichier)
            fs.Close()
        End If
        Try
            nomfichier = New StreamWriter(nmFichier)
            nomfichier.WriteLine("#NEW#")
            nomfichier.WriteLine(fDonnees.nomSujet)
            nomfichier.WriteLine(fDonnees.masse)
            Dim cejour As Integer = Microsoft.VisualBasic.DateAndTime.Day(Now)
            Dim cemois As Integer = Microsoft.VisualBasic.DateAndTime.Month(Now)
            Dim cetteannee As Integer = Microsoft.VisualBasic.DateAndTime.Year(Now)
            Dim cettedate As String = Str(cejour) + "/" + CStr(cemois) + "/" + CStr(cetteannee)
            nomfichier.WriteLine(cettedate)
            nomfichier.WriteLine(fDonnees.typetest)
            nomfichier.WriteLine(fDonnees.nom_blessure)
            nomfichier.WriteLine(fDonnees.fs)
            nomfichier.WriteLine(fDonnees.nbpoints)

            Select Case fDonnees.typetest
                Case "ISO"
                    '*** situation où il y a le plus de commentaires à ajouter au fichier
                    nomfichier.WriteLine(fDonnees.banc) '** Trouver cette valeur dans le fDonnees
                    nomfichier.WriteLine(fDonnees.pied) '*** anciennement numPied
                    Select Case fDonnees.typeISO
                        Case "2J"
                            nomfichier.WriteLine("2J")
                        Case "D"
                            nomfichier.WriteLine("D")
                        Case "G"
                            nomfichier.WriteLine("G")
                    End Select
                    nomfichier.WriteLine(fDonnees.position)
                    nomfichier.WriteLine(fDonnees.typecourbe)
                Case "FVcmj", "FVsj"
                    nomfichier.WriteLine(fDonnees.charge) ' transformer selon fDonnees
                    nomfichier.WriteLine(fDonnees.reglage1) 'réglage de l'angle 1
                    nomfichier.WriteLine(fDonnees.reglage2) 'réglage de l'angle 2
                    nomfichier.WriteLine(fDonnees.saut_realise)
                Case "RJ"
                    nomfichier.WriteLine(fDonnees.puissRJ6sec) 'valeur de la puissance du RJ6sec de référence ...
                Case "RJ6sec"
                    nomfichier.WriteLine(fDonnees.puissRJ6sec) 'valeur de la puissance du RJ6sec de référence ...
            End Select
            For n = 0 To (fDonnees.nbpoints - 1)
                nomfichier.WriteLine(CStr(fDonnees.sig(n)))
            Next n
            nomfichier.Flush()
            nomfichier.Close()

        Catch ex As Exception
            MsgBox("Problème d'enregistrement des données", MsgBoxStyle.OkOnly, "Avertissement")
        End Try


    End Sub
End Module
