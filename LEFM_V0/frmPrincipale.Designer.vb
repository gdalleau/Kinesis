<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrincipale
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim LineSeriesView1 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim LineSeriesView2 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim XyDiagram2 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim XyDiagramPane1 As DevExpress.XtraCharts.XYDiagramPane = New DevExpress.XtraCharts.XYDiagramPane()
        Dim Series2 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim SeriesPoint1 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("12", New Object() {CType(23.0R, Object)})
        Dim LineSeriesView3 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim Series3 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim LineSeriesView4 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim LineSeriesView5 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim XyDiagram3 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim Series4 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim LineSeriesView6 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim LineSeriesView7 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrincipale))
        Me.tabAppli = New System.Windows.Forms.TabControl()
        Me.tabAccueil = New System.Windows.Forms.TabPage()
        Me.btnAccueilConfiguration = New System.Windows.Forms.Button()
        Me.btnAccueilAnalyse = New System.Windows.Forms.Button()
        Me.btnAccueilAcquisition = New System.Windows.Forms.Button()
        Me.tabAcqui = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAjoutSujets = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.grbxAcquisition = New System.Windows.Forms.GroupBox()
        Me.lblNbEssais = New System.Windows.Forms.Label()
        Me.lblEssais = New System.Windows.Forms.Label()
        Me.chkSaut = New System.Windows.Forms.CheckBox()
        Me.numAngle2 = New System.Windows.Forms.NumericUpDown()
        Me.lblAngle2 = New System.Windows.Forms.Label()
        Me.numAngle1 = New System.Windows.Forms.NumericUpDown()
        Me.lblAngle1 = New System.Windows.Forms.Label()
        Me.lblTypeCourbe = New System.Windows.Forms.Label()
        Me.cmbxTypeCourbe = New System.Windows.Forms.ComboBox()
        Me.btnSupprimer = New System.Windows.Forms.Button()
        Me.cmbxBlessures = New System.Windows.Forms.ComboBox()
        Me.btnSauvegarder = New System.Windows.Forms.Button()
        Me.btnMesure = New System.Windows.Forms.Button()
        Me.numCharge = New System.Windows.Forms.NumericUpDown()
        Me.lblCharge = New System.Windows.Forms.Label()
        Me.chkBlesse = New System.Windows.Forms.CheckBox()
        Me.numPieds = New System.Windows.Forms.NumericUpDown()
        Me.numBanc = New System.Windows.Forms.NumericUpDown()
        Me.chkCouche = New System.Windows.Forms.CheckBox()
        Me.rbPiedG = New System.Windows.Forms.RadioButton()
        Me.rbPiedD = New System.Windows.Forms.RadioButton()
        Me.rb2pieds = New System.Windows.Forms.RadioButton()
        Me.lblPieds = New System.Windows.Forms.Label()
        Me.lblBanc = New System.Windows.Forms.Label()
        Me.numDuree = New System.Windows.Forms.NumericUpDown()
        Me.lblDuree = New System.Windows.Forms.Label()
        Me.numFreq = New System.Windows.Forms.NumericUpDown()
        Me.lblFrequence = New System.Windows.Forms.Label()
        Me.lstTests = New System.Windows.Forms.ListBox()
        Me.lblChoixTest = New System.Windows.Forms.Label()
        Me.grBxPrevisualisation = New System.Windows.Forms.GroupBox()
        Me.chartForce = New DevExpress.XtraCharts.ChartControl()
        Me.grBxSujets = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lstSujets = New System.Windows.Forms.ListView()
        Me.clnNom = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnPrenom = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnMasse = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnBanc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnPieds = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnAngle1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnAngle2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnPuiss = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.grpMesurePoids = New System.Windows.Forms.GroupBox()
        Me.lblMasseMesuree = New System.Windows.Forms.Label()
        Me.btnPoidsSujet = New System.Windows.Forms.Button()
        Me.grBxCalibration = New System.Windows.Forms.GroupBox()
        Me.lblCalibration = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAvide = New System.Windows.Forms.Button()
        Me.btnTare = New System.Windows.Forms.Button()
        Me.numTare = New System.Windows.Forms.NumericUpDown()
        Me.tabAnalyse = New System.Windows.Forms.TabPage()
        Me.flowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.chrtFVcmj = New DevExpress.XtraCharts.ChartControl()
        Me.btnRJ = New DevExpress.XtraEditors.CheckButton()
        Me.layoutPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.chrtPrincipal = New DevExpress.XtraCharts.ChartControl()
        Me.grbxIndices = New System.Windows.Forms.GroupBox()
        Me.tabResultats = New System.Windows.Forms.TabControl()
        Me.tabForceMax = New System.Windows.Forms.TabPage()
        Me.lblCoherenceFG = New System.Windows.Forms.Label()
        Me.lblMoyenneFDiff = New System.Windows.Forms.Label()
        Me.lblMoyenneFG = New System.Windows.Forms.Label()
        Me.lblMoyenneFD = New System.Windows.Forms.Label()
        Me.lblCoherenceFD = New System.Windows.Forms.Label()
        Me.lblCoherenceF2J = New System.Windows.Forms.Label()
        Me.lblMoyenneF2J = New System.Windows.Forms.Label()
        Me.lblMeanFmax = New System.Windows.Forms.Label()
        Me.lblCoherenceFmax = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tabRFD = New System.Windows.Forms.TabPage()
        Me.lblCoherenceRG = New System.Windows.Forms.Label()
        Me.lblMoyenneRDiff = New System.Windows.Forms.Label()
        Me.lblMoyenneRG = New System.Windows.Forms.Label()
        Me.lblMoyenneRD = New System.Windows.Forms.Label()
        Me.lblCoherenceRD = New System.Windows.Forms.Label()
        Me.lblCoherenceR2J = New System.Windows.Forms.Label()
        Me.lblMoyenneR2J = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TFmax = New System.Windows.Forms.TabPage()
        Me.lblMoyenneTdiff = New System.Windows.Forms.Label()
        Me.lblMoyenneTG = New System.Windows.Forms.Label()
        Me.lblMoyenneTD = New System.Windows.Forms.Label()
        Me.lblMoyenneT2J = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblTypeCourbe_Analyse = New System.Windows.Forms.Label()
        Me.cbxTypeCourbe_Analyse = New System.Windows.Forms.ComboBox()
        Me.lblCoherence3 = New System.Windows.Forms.Label()
        Me.lblCoherence2 = New System.Windows.Forms.Label()
        Me.lblMoyenne3 = New System.Windows.Forms.Label()
        Me.lblMoyenne2 = New System.Windows.Forms.Label()
        Me.lblVariable3 = New System.Windows.Forms.Label()
        Me.lblVariable2 = New System.Windows.Forms.Label()
        Me.lblVariable1 = New System.Windows.Forms.Label()
        Me.grbxOrdre = New System.Windows.Forms.GroupBox()
        Me.numOrdrePoly = New System.Windows.Forms.NumericUpDown()
        Me.lblOrdrePoly = New System.Windows.Forms.Label()
        Me.btnDeselectionner = New System.Windows.Forms.Button()
        Me.btnRecapitulatif = New System.Windows.Forms.Button()
        Me.btnEnregistrer = New System.Windows.Forms.Button()
        Me.lblCoherence1 = New System.Windows.Forms.Label()
        Me.lblMoyenne1 = New System.Windows.Forms.Label()
        Me.lblCoherenceGr1 = New System.Windows.Forms.Label()
        Me.lblMoyenneGr1 = New System.Windows.Forms.Label()
        Me.lstIndices = New System.Windows.Forms.ListView()
        Me.grbxSujet = New System.Windows.Forms.GroupBox()
        Me.lblMasse = New System.Windows.Forms.Label()
        Me.lblSujet = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.groupeChoixAnalyse = New System.Windows.Forms.GroupBox()
        Me.chkLstFiles1 = New System.Windows.Forms.CheckedListBox()
        Me.btnEffacer = New System.Windows.Forms.Button()
        Me.btnAnalyse = New System.Windows.Forms.Button()
        Me.clstFichAnalyse = New System.Windows.Forms.CheckedListBox()
        Me.btnChoix = New System.Windows.Forms.Button()
        Me.tabConfig = New System.Windows.Forms.TabPage()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grbNbEssais = New System.Windows.Forms.GroupBox()
        Me.numMinFV = New System.Windows.Forms.NumericUpDown()
        Me.lblMinFV = New System.Windows.Forms.Label()
        Me.numMinRJ = New System.Windows.Forms.NumericUpDown()
        Me.lblMinRJ = New System.Windows.Forms.Label()
        Me.numMinISO = New System.Windows.Forms.NumericUpDown()
        Me.lblMinISO = New System.Windows.Forms.Label()
        Me.numMinK = New System.Windows.Forms.NumericUpDown()
        Me.lblMinK = New System.Windows.Forms.Label()
        Me.numMinSJ = New System.Windows.Forms.NumericUpDown()
        Me.lblMinSJ = New System.Windows.Forms.Label()
        Me.numMinCMJ = New System.Windows.Forms.NumericUpDown()
        Me.lblMinCMJ = New System.Windows.Forms.Label()
        Me.grpBlessures = New System.Windows.Forms.GroupBox()
        Me.btnSuppBlessure = New System.Windows.Forms.Button()
        Me.btnBlessure = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtBlessure = New System.Windows.Forms.TextBox()
        Me.btnAfficher = New System.Windows.Forms.Button()
        Me.lstBlessures = New System.Windows.Forms.ListBox()
        Me.grbAcquisition = New System.Windows.Forms.GroupBox()
        Me.txtNomVoie = New System.Windows.Forms.TextBox()
        Me.lblNomVoie = New System.Windows.Forms.Label()
        Me.txtCoeffB_Config = New System.Windows.Forms.TextBox()
        Me.txtCoeffA_Config = New System.Windows.Forms.TextBox()
        Me.txtNomCarte = New System.Windows.Forms.TextBox()
        Me.lblNomCarte = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblNomConfig = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblDateModif = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grbxPoly = New System.Windows.Forms.GroupBox()
        Me.numOrdreReg = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnEnregistreConfig = New System.Windows.Forms.Button()
        Me.btnChargeConfig = New System.Windows.Forms.Button()
        Me.grpBxSeuils = New System.Windows.Forms.GroupBox()
        Me.numSeuilFz1 = New System.Windows.Forms.NumericUpDown()
        Me.numSeuilFz2 = New System.Windows.Forms.NumericUpDown()
        Me.lblFz1 = New System.Windows.Forms.Label()
        Me.lblFz2 = New System.Windows.Forms.Label()
        Me.lblRepertoire = New System.Windows.Forms.Label()
        Me.txtRepertoire = New System.Windows.Forms.TextBox()
        Me.btnRepertoire = New System.Windows.Forms.Button()
        Me.tabSortir = New System.Windows.Forms.TabPage()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.tabAppli.SuspendLayout()
        Me.tabAccueil.SuspendLayout()
        Me.tabAcqui.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grbxAcquisition.SuspendLayout()
        CType(Me.numAngle2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numAngle1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numPieds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numBanc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numDuree, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numFreq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grBxPrevisualisation.SuspendLayout()
        CType(Me.chartForce, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grBxSujets.SuspendLayout()
        Me.grpMesurePoids.SuspendLayout()
        Me.grBxCalibration.SuspendLayout()
        CType(Me.numTare, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAnalyse.SuspendLayout()
        Me.flowLayoutPanel2.SuspendLayout()
        CType(Me.chrtFVcmj, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagramPane1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutPanel.SuspendLayout()
        CType(Me.chrtPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbxIndices.SuspendLayout()
        Me.tabResultats.SuspendLayout()
        Me.tabForceMax.SuspendLayout()
        Me.tabRFD.SuspendLayout()
        Me.TFmax.SuspendLayout()
        Me.grbxOrdre.SuspendLayout()
        CType(Me.numOrdrePoly, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbxSujet.SuspendLayout()
        Me.groupeChoixAnalyse.SuspendLayout()
        Me.tabConfig.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbNbEssais.SuspendLayout()
        CType(Me.numMinFV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMinRJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMinISO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMinK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMinSJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMinCMJ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBlessures.SuspendLayout()
        Me.grbAcquisition.SuspendLayout()
        Me.grbxPoly.SuspendLayout()
        CType(Me.numOrdreReg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBxSeuils.SuspendLayout()
        CType(Me.numSeuilFz1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numSeuilFz2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabAppli
        '
        Me.tabAppli.Controls.Add(Me.tabAccueil)
        Me.tabAppli.Controls.Add(Me.tabAcqui)
        Me.tabAppli.Controls.Add(Me.tabAnalyse)
        Me.tabAppli.Controls.Add(Me.tabConfig)
        Me.tabAppli.Controls.Add(Me.tabSortir)
        Me.tabAppli.Location = New System.Drawing.Point(12, 19)
        Me.tabAppli.Name = "tabAppli"
        Me.tabAppli.SelectedIndex = 0
        Me.tabAppli.Size = New System.Drawing.Size(1422, 1014)
        Me.tabAppli.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.tabAppli.TabIndex = 0
        '
        'tabAccueil
        '
        Me.tabAccueil.Controls.Add(Me.btnAccueilConfiguration)
        Me.tabAccueil.Controls.Add(Me.btnAccueilAnalyse)
        Me.tabAccueil.Controls.Add(Me.btnAccueilAcquisition)
        Me.tabAccueil.Location = New System.Drawing.Point(4, 25)
        Me.tabAccueil.Name = "tabAccueil"
        Me.tabAccueil.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAccueil.Size = New System.Drawing.Size(1414, 985)
        Me.tabAccueil.TabIndex = 0
        Me.tabAccueil.Text = "Accueil"
        Me.tabAccueil.UseVisualStyleBackColor = True
        '
        'btnAccueilConfiguration
        '
        Me.btnAccueilConfiguration.Location = New System.Drawing.Point(420, 103)
        Me.btnAccueilConfiguration.Name = "btnAccueilConfiguration"
        Me.btnAccueilConfiguration.Size = New System.Drawing.Size(266, 59)
        Me.btnAccueilConfiguration.TabIndex = 2
        Me.btnAccueilConfiguration.Text = "Configuration"
        Me.btnAccueilConfiguration.UseVisualStyleBackColor = True
        '
        'btnAccueilAnalyse
        '
        Me.btnAccueilAnalyse.Location = New System.Drawing.Point(99, 186)
        Me.btnAccueilAnalyse.Name = "btnAccueilAnalyse"
        Me.btnAccueilAnalyse.Size = New System.Drawing.Size(261, 63)
        Me.btnAccueilAnalyse.TabIndex = 1
        Me.btnAccueilAnalyse.Text = "Analyse"
        Me.btnAccueilAnalyse.UseVisualStyleBackColor = True
        '
        'btnAccueilAcquisition
        '
        Me.btnAccueilAcquisition.BackColor = System.Drawing.Color.Transparent
        Me.btnAccueilAcquisition.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnAccueilAcquisition.Location = New System.Drawing.Point(95, 103)
        Me.btnAccueilAcquisition.Name = "btnAccueilAcquisition"
        Me.btnAccueilAcquisition.Size = New System.Drawing.Size(266, 59)
        Me.btnAccueilAcquisition.TabIndex = 0
        Me.btnAccueilAcquisition.Text = "Acquisition"
        Me.btnAccueilAcquisition.UseVisualStyleBackColor = False
        '
        'tabAcqui
        '
        Me.tabAcqui.Controls.Add(Me.GroupBox1)
        Me.tabAcqui.Controls.Add(Me.grbxAcquisition)
        Me.tabAcqui.Controls.Add(Me.grBxPrevisualisation)
        Me.tabAcqui.Controls.Add(Me.grBxSujets)
        Me.tabAcqui.Controls.Add(Me.grBxCalibration)
        Me.tabAcqui.Location = New System.Drawing.Point(4, 25)
        Me.tabAcqui.Name = "tabAcqui"
        Me.tabAcqui.Size = New System.Drawing.Size(1414, 985)
        Me.tabAcqui.TabIndex = 2
        Me.tabAcqui.Text = "Acquisition"
        Me.tabAcqui.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAjoutSujets)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(68, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(535, 118)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Liste"
        '
        'btnAjoutSujets
        '
        Me.btnAjoutSujets.Location = New System.Drawing.Point(35, 38)
        Me.btnAjoutSujets.Name = "btnAjoutSujets"
        Me.btnAjoutSujets.Size = New System.Drawing.Size(250, 39)
        Me.btnAjoutSujets.TabIndex = 9
        Me.btnAjoutSujets.Text = "Importer une liste de sujets"
        Me.btnAjoutSujets.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(303, 38)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(195, 39)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Enregistrer la liste de sujets"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'grbxAcquisition
        '
        Me.grbxAcquisition.Controls.Add(Me.lblNbEssais)
        Me.grbxAcquisition.Controls.Add(Me.lblEssais)
        Me.grbxAcquisition.Controls.Add(Me.chkSaut)
        Me.grbxAcquisition.Controls.Add(Me.numAngle2)
        Me.grbxAcquisition.Controls.Add(Me.lblAngle2)
        Me.grbxAcquisition.Controls.Add(Me.numAngle1)
        Me.grbxAcquisition.Controls.Add(Me.lblAngle1)
        Me.grbxAcquisition.Controls.Add(Me.lblTypeCourbe)
        Me.grbxAcquisition.Controls.Add(Me.cmbxTypeCourbe)
        Me.grbxAcquisition.Controls.Add(Me.btnSupprimer)
        Me.grbxAcquisition.Controls.Add(Me.cmbxBlessures)
        Me.grbxAcquisition.Controls.Add(Me.btnSauvegarder)
        Me.grbxAcquisition.Controls.Add(Me.btnMesure)
        Me.grbxAcquisition.Controls.Add(Me.numCharge)
        Me.grbxAcquisition.Controls.Add(Me.lblCharge)
        Me.grbxAcquisition.Controls.Add(Me.chkBlesse)
        Me.grbxAcquisition.Controls.Add(Me.numPieds)
        Me.grbxAcquisition.Controls.Add(Me.numBanc)
        Me.grbxAcquisition.Controls.Add(Me.chkCouche)
        Me.grbxAcquisition.Controls.Add(Me.rbPiedG)
        Me.grbxAcquisition.Controls.Add(Me.rbPiedD)
        Me.grbxAcquisition.Controls.Add(Me.rb2pieds)
        Me.grbxAcquisition.Controls.Add(Me.lblPieds)
        Me.grbxAcquisition.Controls.Add(Me.lblBanc)
        Me.grbxAcquisition.Controls.Add(Me.numDuree)
        Me.grbxAcquisition.Controls.Add(Me.lblDuree)
        Me.grbxAcquisition.Controls.Add(Me.numFreq)
        Me.grbxAcquisition.Controls.Add(Me.lblFrequence)
        Me.grbxAcquisition.Controls.Add(Me.lstTests)
        Me.grbxAcquisition.Controls.Add(Me.lblChoixTest)
        Me.grbxAcquisition.Location = New System.Drawing.Point(68, 336)
        Me.grbxAcquisition.Name = "grbxAcquisition"
        Me.grbxAcquisition.Size = New System.Drawing.Size(1030, 220)
        Me.grbxAcquisition.TabIndex = 7
        Me.grbxAcquisition.TabStop = False
        Me.grbxAcquisition.Text = "Nouvelle acquisition"
        Me.grbxAcquisition.Visible = False
        '
        'lblNbEssais
        '
        Me.lblNbEssais.AutoSize = True
        Me.lblNbEssais.BackColor = System.Drawing.Color.LightSkyBlue
        Me.lblNbEssais.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblNbEssais.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNbEssais.Location = New System.Drawing.Point(639, 77)
        Me.lblNbEssais.Name = "lblNbEssais"
        Me.lblNbEssais.Size = New System.Drawing.Size(16, 17)
        Me.lblNbEssais.TabIndex = 26
        Me.lblNbEssais.Text = "0"
        '
        'lblEssais
        '
        Me.lblEssais.AutoSize = True
        Me.lblEssais.Location = New System.Drawing.Point(639, 47)
        Me.lblEssais.Name = "lblEssais"
        Me.lblEssais.Size = New System.Drawing.Size(166, 17)
        Me.lblEssais.TabIndex = 25
        Me.lblEssais.Text = "Nombre d'essais réalisés"
        '
        'chkSaut
        '
        Me.chkSaut.AutoSize = True
        Me.chkSaut.Checked = True
        Me.chkSaut.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSaut.Location = New System.Drawing.Point(548, 45)
        Me.chkSaut.Name = "chkSaut"
        Me.chkSaut.Size = New System.Drawing.Size(59, 21)
        Me.chkSaut.TabIndex = 24
        Me.chkSaut.Text = "Saut"
        Me.chkSaut.UseVisualStyleBackColor = True
        Me.chkSaut.Visible = False
        '
        'numAngle2
        '
        Me.numAngle2.Location = New System.Drawing.Point(438, 145)
        Me.numAngle2.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numAngle2.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numAngle2.Name = "numAngle2"
        Me.numAngle2.Size = New System.Drawing.Size(81, 22)
        Me.numAngle2.TabIndex = 23
        Me.numAngle2.Visible = False
        '
        'lblAngle2
        '
        Me.lblAngle2.AutoSize = True
        Me.lblAngle2.Location = New System.Drawing.Point(435, 115)
        Me.lblAngle2.Name = "lblAngle2"
        Me.lblAngle2.Size = New System.Drawing.Size(52, 17)
        Me.lblAngle2.TabIndex = 22
        Me.lblAngle2.Text = "Angle2"
        Me.lblAngle2.Visible = False
        '
        'numAngle1
        '
        Me.numAngle1.Location = New System.Drawing.Point(438, 72)
        Me.numAngle1.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numAngle1.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numAngle1.Name = "numAngle1"
        Me.numAngle1.Size = New System.Drawing.Size(81, 22)
        Me.numAngle1.TabIndex = 21
        Me.numAngle1.Visible = False
        '
        'lblAngle1
        '
        Me.lblAngle1.AutoSize = True
        Me.lblAngle1.Location = New System.Drawing.Point(435, 38)
        Me.lblAngle1.Name = "lblAngle1"
        Me.lblAngle1.Size = New System.Drawing.Size(52, 17)
        Me.lblAngle1.TabIndex = 20
        Me.lblAngle1.Text = "Angle1"
        Me.lblAngle1.Visible = False
        '
        'lblTypeCourbe
        '
        Me.lblTypeCourbe.AutoSize = True
        Me.lblTypeCourbe.Location = New System.Drawing.Point(544, 167)
        Me.lblTypeCourbe.Name = "lblTypeCourbe"
        Me.lblTypeCourbe.Size = New System.Drawing.Size(197, 17)
        Me.lblTypeCourbe.TabIndex = 19
        Me.lblTypeCourbe.Text = "Choisissez le type de courbe :"
        Me.lblTypeCourbe.Visible = False
        '
        'cmbxTypeCourbe
        '
        Me.cmbxTypeCourbe.FormattingEnabled = True
        Me.cmbxTypeCourbe.Items.AddRange(New Object() {"1- pic précoce et regression", "2- pic précoce et plateau", "3-  plateau tardif ", "4- montée progressive et pic tardif", "5- autres"})
        Me.cmbxTypeCourbe.Location = New System.Drawing.Point(547, 190)
        Me.cmbxTypeCourbe.Name = "cmbxTypeCourbe"
        Me.cmbxTypeCourbe.Size = New System.Drawing.Size(228, 24)
        Me.cmbxTypeCourbe.TabIndex = 10
        Me.cmbxTypeCourbe.Visible = False
        '
        'btnSupprimer
        '
        Me.btnSupprimer.Enabled = False
        Me.btnSupprimer.Location = New System.Drawing.Point(887, 141)
        Me.btnSupprimer.Name = "btnSupprimer"
        Me.btnSupprimer.Size = New System.Drawing.Size(116, 37)
        Me.btnSupprimer.TabIndex = 2
        Me.btnSupprimer.Text = "Supprimer"
        Me.btnSupprimer.UseVisualStyleBackColor = True
        '
        'cmbxBlessures
        '
        Me.cmbxBlessures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxBlessures.FormattingEnabled = True
        Me.cmbxBlessures.Items.AddRange(New Object() {"Entorse", "Tendinite", "Dechirure"})
        Me.cmbxBlessures.Location = New System.Drawing.Point(324, 145)
        Me.cmbxBlessures.Name = "cmbxBlessures"
        Me.cmbxBlessures.Size = New System.Drawing.Size(95, 24)
        Me.cmbxBlessures.TabIndex = 18
        Me.cmbxBlessures.Visible = False
        '
        'btnSauvegarder
        '
        Me.btnSauvegarder.Enabled = False
        Me.btnSauvegarder.Location = New System.Drawing.Point(887, 94)
        Me.btnSauvegarder.Name = "btnSauvegarder"
        Me.btnSauvegarder.Size = New System.Drawing.Size(116, 37)
        Me.btnSauvegarder.TabIndex = 1
        Me.btnSauvegarder.Text = "Sauvegarder"
        Me.btnSauvegarder.UseVisualStyleBackColor = True
        '
        'btnMesure
        '
        Me.btnMesure.Location = New System.Drawing.Point(887, 45)
        Me.btnMesure.Name = "btnMesure"
        Me.btnMesure.Size = New System.Drawing.Size(116, 38)
        Me.btnMesure.TabIndex = 8
        Me.btnMesure.Text = "Mesure"
        Me.btnMesure.UseVisualStyleBackColor = True
        '
        'numCharge
        '
        Me.numCharge.DecimalPlaces = 3
        Me.numCharge.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numCharge.InterceptArrowKeys = False
        Me.numCharge.Location = New System.Drawing.Point(193, 146)
        Me.numCharge.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.numCharge.Name = "numCharge"
        Me.numCharge.Size = New System.Drawing.Size(97, 22)
        Me.numCharge.TabIndex = 17
        Me.numCharge.Visible = False
        '
        'lblCharge
        '
        Me.lblCharge.AutoSize = True
        Me.lblCharge.Location = New System.Drawing.Point(191, 114)
        Me.lblCharge.Name = "lblCharge"
        Me.lblCharge.Size = New System.Drawing.Size(83, 17)
        Me.lblCharge.TabIndex = 16
        Me.lblCharge.Text = "Charge (kg)"
        Me.lblCharge.Visible = False
        '
        'chkBlesse
        '
        Me.chkBlesse.AutoSize = True
        Me.chkBlesse.Location = New System.Drawing.Point(322, 114)
        Me.chkBlesse.Name = "chkBlesse"
        Me.chkBlesse.Size = New System.Drawing.Size(72, 21)
        Me.chkBlesse.TabIndex = 14
        Me.chkBlesse.Text = "Blessé"
        Me.chkBlesse.UseVisualStyleBackColor = True
        '
        'numPieds
        '
        Me.numPieds.Location = New System.Drawing.Point(438, 145)
        Me.numPieds.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numPieds.Name = "numPieds"
        Me.numPieds.Size = New System.Drawing.Size(75, 22)
        Me.numPieds.TabIndex = 13
        Me.numPieds.Visible = False
        '
        'numBanc
        '
        Me.numBanc.Location = New System.Drawing.Point(438, 72)
        Me.numBanc.Name = "numBanc"
        Me.numBanc.Size = New System.Drawing.Size(81, 22)
        Me.numBanc.TabIndex = 12
        Me.numBanc.Visible = False
        '
        'chkCouche
        '
        Me.chkCouche.AutoSize = True
        Me.chkCouche.Checked = True
        Me.chkCouche.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCouche.Location = New System.Drawing.Point(550, 141)
        Me.chkCouche.Name = "chkCouche"
        Me.chkCouche.Size = New System.Drawing.Size(78, 21)
        Me.chkCouche.TabIndex = 11
        Me.chkCouche.Text = "Couché"
        Me.chkCouche.UseVisualStyleBackColor = True
        Me.chkCouche.Visible = False
        '
        'rbPiedG
        '
        Me.rbPiedG.AutoSize = True
        Me.rbPiedG.Location = New System.Drawing.Point(550, 105)
        Me.rbPiedG.Name = "rbPiedG"
        Me.rbPiedG.Size = New System.Drawing.Size(72, 21)
        Me.rbPiedG.TabIndex = 10
        Me.rbPiedG.TabStop = True
        Me.rbPiedG.Text = "Pied G"
        Me.rbPiedG.UseVisualStyleBackColor = True
        Me.rbPiedG.Visible = False
        '
        'rbPiedD
        '
        Me.rbPiedD.AutoSize = True
        Me.rbPiedD.Location = New System.Drawing.Point(550, 75)
        Me.rbPiedD.Name = "rbPiedD"
        Me.rbPiedD.Size = New System.Drawing.Size(71, 21)
        Me.rbPiedD.TabIndex = 9
        Me.rbPiedD.TabStop = True
        Me.rbPiedD.Text = "Pied D"
        Me.rbPiedD.UseVisualStyleBackColor = True
        Me.rbPiedD.Visible = False
        '
        'rb2pieds
        '
        Me.rb2pieds.AutoSize = True
        Me.rb2pieds.Location = New System.Drawing.Point(550, 45)
        Me.rb2pieds.Name = "rb2pieds"
        Me.rb2pieds.Size = New System.Drawing.Size(75, 21)
        Me.rb2pieds.TabIndex = 8
        Me.rb2pieds.TabStop = True
        Me.rb2pieds.Text = "2 pieds"
        Me.rb2pieds.UseVisualStyleBackColor = True
        Me.rb2pieds.Visible = False
        '
        'lblPieds
        '
        Me.lblPieds.AutoSize = True
        Me.lblPieds.Location = New System.Drawing.Point(435, 115)
        Me.lblPieds.Name = "lblPieds"
        Me.lblPieds.Size = New System.Drawing.Size(43, 17)
        Me.lblPieds.TabIndex = 7
        Me.lblPieds.Text = "Pieds"
        Me.lblPieds.Visible = False
        '
        'lblBanc
        '
        Me.lblBanc.AutoSize = True
        Me.lblBanc.Location = New System.Drawing.Point(435, 38)
        Me.lblBanc.Name = "lblBanc"
        Me.lblBanc.Size = New System.Drawing.Size(40, 17)
        Me.lblBanc.TabIndex = 6
        Me.lblBanc.Text = "Banc"
        Me.lblBanc.Visible = False
        '
        'numDuree
        '
        Me.numDuree.Location = New System.Drawing.Point(325, 72)
        Me.numDuree.Name = "numDuree"
        Me.numDuree.Size = New System.Drawing.Size(69, 22)
        Me.numDuree.TabIndex = 5
        Me.numDuree.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblDuree
        '
        Me.lblDuree.AutoSize = True
        Me.lblDuree.Location = New System.Drawing.Point(322, 39)
        Me.lblDuree.Name = "lblDuree"
        Me.lblDuree.Size = New System.Drawing.Size(68, 17)
        Me.lblDuree.TabIndex = 4
        Me.lblDuree.Text = "Durée (s)"
        '
        'numFreq
        '
        Me.numFreq.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.numFreq.Location = New System.Drawing.Point(191, 74)
        Me.numFreq.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.numFreq.Name = "numFreq"
        Me.numFreq.Size = New System.Drawing.Size(100, 22)
        Me.numFreq.TabIndex = 3
        Me.numFreq.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'lblFrequence
        '
        Me.lblFrequence.AutoSize = True
        Me.lblFrequence.Location = New System.Drawing.Point(185, 39)
        Me.lblFrequence.Name = "lblFrequence"
        Me.lblFrequence.Size = New System.Drawing.Size(107, 17)
        Me.lblFrequence.TabIndex = 2
        Me.lblFrequence.Text = "Fréquence (Hz)"
        '
        'lstTests
        '
        Me.lstTests.FormattingEnabled = True
        Me.lstTests.ItemHeight = 16
        Me.lstTests.Items.AddRange(New Object() {"SJ", "CMJ", "K", "ISO", "FVcmj", "RJ", "RJ6sec"})
        Me.lstTests.Location = New System.Drawing.Point(39, 72)
        Me.lstTests.Name = "lstTests"
        Me.lstTests.Size = New System.Drawing.Size(103, 116)
        Me.lstTests.TabIndex = 1
        '
        'lblChoixTest
        '
        Me.lblChoixTest.AutoSize = True
        Me.lblChoixTest.Location = New System.Drawing.Point(40, 36)
        Me.lblChoixTest.Name = "lblChoixTest"
        Me.lblChoixTest.Size = New System.Drawing.Size(89, 17)
        Me.lblChoixTest.TabIndex = 0
        Me.lblChoixTest.Text = "Choix du test"
        '
        'grBxPrevisualisation
        '
        Me.grBxPrevisualisation.Controls.Add(Me.chartForce)
        Me.grBxPrevisualisation.Location = New System.Drawing.Point(66, 562)
        Me.grBxPrevisualisation.Name = "grBxPrevisualisation"
        Me.grBxPrevisualisation.Size = New System.Drawing.Size(1032, 408)
        Me.grBxPrevisualisation.TabIndex = 2
        Me.grBxPrevisualisation.TabStop = False
        Me.grBxPrevisualisation.Text = "Prévisualisation"
        Me.grBxPrevisualisation.Visible = False
        '
        'chartForce
        '
        XyDiagram1.AxisX.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        XyDiagram1.AxisX.GridLines.Visible = True
        XyDiagram1.AxisX.Title.Font = New System.Drawing.Font("Tahoma", 10.0!)
        XyDiagram1.AxisX.Title.Text = "Temps (s)"
        XyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.[True]
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.CrosshairAxisLabelOptions.BackColor = System.Drawing.Color.Red
        XyDiagram1.AxisY.Title.Font = New System.Drawing.Font("Tahoma", 10.0!)
        XyDiagram1.AxisY.Title.Text = "Force (N)"
        XyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.[True]
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagram1.DefaultPane.BackColor = System.Drawing.Color.White
        XyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.[True]
        XyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.[True]
        XyDiagram1.EnableAxisXScrolling = True
        XyDiagram1.EnableAxisXZooming = True
        XyDiagram1.EnableAxisYScrolling = True
        XyDiagram1.EnableAxisYZooming = True
        XyDiagram1.ZoomingOptions.UseKeyboard = False
        XyDiagram1.ZoomingOptions.UseTouchDevice = False
        Me.chartForce.Diagram = XyDiagram1
        Me.chartForce.Legend.Visibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartForce.Location = New System.Drawing.Point(42, 35)
        Me.chartForce.Name = "chartForce"
        Series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical
        Series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Series1.Name = "Force"
        LineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Series1.View = LineSeriesView1
        Me.chartForce.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1}
        Me.chartForce.SeriesTemplate.View = LineSeriesView2
        Me.chartForce.Size = New System.Drawing.Size(963, 314)
        Me.chartForce.TabIndex = 1
        '
        'grBxSujets
        '
        Me.grBxSujets.Controls.Add(Me.Button3)
        Me.grBxSujets.Controls.Add(Me.Button2)
        Me.grBxSujets.Controls.Add(Me.lstSujets)
        Me.grBxSujets.Controls.Add(Me.grpMesurePoids)
        Me.grBxSujets.Location = New System.Drawing.Point(66, 157)
        Me.grBxSujets.Name = "grBxSujets"
        Me.grBxSujets.Size = New System.Drawing.Size(1032, 161)
        Me.grBxSujets.TabIndex = 1
        Me.grBxSujets.TabStop = False
        Me.grBxSujets.Text = "Sujets/Athlètes"
        Me.grBxSujets.Visible = False
        '
        'Button3
        '
        Me.Button3.FlatAppearance.BorderSize = 2
        Me.Button3.Location = New System.Drawing.Point(141, 0)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(35, 23)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "-"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatAppearance.BorderSize = 2
        Me.Button2.Location = New System.Drawing.Point(109, 0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(35, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "+"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'lstSujets
        '
        Me.lstSujets.CheckBoxes = True
        Me.lstSujets.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clnNom, Me.clnPrenom, Me.clnMasse, Me.clnBanc, Me.clnPieds, Me.clnAngle1, Me.clnAngle2, Me.clnPuiss})
        Me.lstSujets.FullRowSelect = True
        Me.lstSujets.Location = New System.Drawing.Point(26, 27)
        Me.lstSujets.MultiSelect = False
        Me.lstSujets.Name = "lstSujets"
        Me.lstSujets.Size = New System.Drawing.Size(829, 120)
        Me.lstSujets.TabIndex = 0
        Me.lstSujets.UseCompatibleStateImageBehavior = False
        Me.lstSujets.View = System.Windows.Forms.View.Details
        '
        'clnNom
        '
        Me.clnNom.Text = "    Nom"
        Me.clnNom.Width = 100
        '
        'clnPrenom
        '
        Me.clnPrenom.Text = "Prenom"
        Me.clnPrenom.Width = 93
        '
        'clnMasse
        '
        Me.clnMasse.Text = "Masse"
        '
        'clnBanc
        '
        Me.clnBanc.Text = "Banc"
        '
        'clnPieds
        '
        Me.clnPieds.Text = "Pieds"
        '
        'clnAngle1
        '
        Me.clnAngle1.Text = "Angle1"
        '
        'clnAngle2
        '
        Me.clnAngle2.Text = "Angle2"
        '
        'clnPuiss
        '
        Me.clnPuiss.Text = "Puiss RJ6"
        Me.clnPuiss.Width = 84
        '
        'grpMesurePoids
        '
        Me.grpMesurePoids.Controls.Add(Me.lblMasseMesuree)
        Me.grpMesurePoids.Controls.Add(Me.btnPoidsSujet)
        Me.grpMesurePoids.Location = New System.Drawing.Point(864, 21)
        Me.grpMesurePoids.Name = "grpMesurePoids"
        Me.grpMesurePoids.Size = New System.Drawing.Size(146, 120)
        Me.grpMesurePoids.TabIndex = 6
        Me.grpMesurePoids.TabStop = False
        Me.grpMesurePoids.Text = "Mesure du poids"
        '
        'lblMasseMesuree
        '
        Me.lblMasseMesuree.AutoSize = True
        Me.lblMasseMesuree.Location = New System.Drawing.Point(22, 81)
        Me.lblMasseMesuree.Name = "lblMasseMesuree"
        Me.lblMasseMesuree.Size = New System.Drawing.Size(107, 17)
        Me.lblMasseMesuree.TabIndex = 5
        Me.lblMasseMesuree.Text = "Valeur du poids"
        '
        'btnPoidsSujet
        '
        Me.btnPoidsSujet.Location = New System.Drawing.Point(13, 28)
        Me.btnPoidsSujet.Name = "btnPoidsSujet"
        Me.btnPoidsSujet.Size = New System.Drawing.Size(116, 40)
        Me.btnPoidsSujet.TabIndex = 4
        Me.btnPoidsSujet.Text = "Poids du sujet"
        Me.btnPoidsSujet.UseVisualStyleBackColor = True
        '
        'grBxCalibration
        '
        Me.grBxCalibration.Controls.Add(Me.lblCalibration)
        Me.grBxCalibration.Controls.Add(Me.Label1)
        Me.grBxCalibration.Controls.Add(Me.btnAvide)
        Me.grBxCalibration.Controls.Add(Me.btnTare)
        Me.grBxCalibration.Controls.Add(Me.numTare)
        Me.grBxCalibration.Location = New System.Drawing.Point(609, 33)
        Me.grBxCalibration.Name = "grBxCalibration"
        Me.grBxCalibration.Size = New System.Drawing.Size(489, 118)
        Me.grBxCalibration.TabIndex = 0
        Me.grBxCalibration.TabStop = False
        Me.grBxCalibration.Text = "Calibration"
        '
        'lblCalibration
        '
        Me.lblCalibration.AutoSize = True
        Me.lblCalibration.Location = New System.Drawing.Point(40, 76)
        Me.lblCalibration.Name = "lblCalibration"
        Me.lblCalibration.Size = New System.Drawing.Size(34, 17)
        Me.lblCalibration.TabIndex = 7
        Me.lblCalibration.Text = "vide"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(383, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "(kg)"
        '
        'btnAvide
        '
        Me.btnAvide.Location = New System.Drawing.Point(35, 24)
        Me.btnAvide.Name = "btnAvide"
        Me.btnAvide.Size = New System.Drawing.Size(133, 42)
        Me.btnAvide.TabIndex = 5
        Me.btnAvide.Text = "Calibrer à vide"
        Me.btnAvide.UseVisualStyleBackColor = True
        '
        'btnTare
        '
        Me.btnTare.Location = New System.Drawing.Point(182, 24)
        Me.btnTare.Name = "btnTare"
        Me.btnTare.Size = New System.Drawing.Size(117, 42)
        Me.btnTare.TabIndex = 4
        Me.btnTare.Text = "Calibrer avec charge"
        Me.btnTare.UseVisualStyleBackColor = True
        '
        'numTare
        '
        Me.numTare.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numTare.Location = New System.Drawing.Point(318, 35)
        Me.numTare.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numTare.Name = "numTare"
        Me.numTare.Size = New System.Drawing.Size(59, 22)
        Me.numTare.TabIndex = 3
        Me.numTare.Value = New Decimal(New Integer() {40, 0, 0, 0})
        '
        'tabAnalyse
        '
        Me.tabAnalyse.Controls.Add(Me.flowLayoutPanel2)
        Me.tabAnalyse.Controls.Add(Me.btnRJ)
        Me.tabAnalyse.Controls.Add(Me.layoutPanel)
        Me.tabAnalyse.Controls.Add(Me.grbxIndices)
        Me.tabAnalyse.Controls.Add(Me.grbxSujet)
        Me.tabAnalyse.Controls.Add(Me.groupeChoixAnalyse)
        Me.tabAnalyse.Location = New System.Drawing.Point(4, 25)
        Me.tabAnalyse.Name = "tabAnalyse"
        Me.tabAnalyse.Size = New System.Drawing.Size(1414, 985)
        Me.tabAnalyse.TabIndex = 3
        Me.tabAnalyse.Text = "Analyse"
        Me.tabAnalyse.UseVisualStyleBackColor = True
        '
        'flowLayoutPanel2
        '
        Me.flowLayoutPanel2.Controls.Add(Me.chrtFVcmj)
        Me.flowLayoutPanel2.Location = New System.Drawing.Point(26, 250)
        Me.flowLayoutPanel2.Name = "flowLayoutPanel2"
        Me.flowLayoutPanel2.Size = New System.Drawing.Size(657, 552)
        Me.flowLayoutPanel2.TabIndex = 6
        '
        'chrtFVcmj
        '
        XyDiagram2.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram2.AxisY.Title.Text = "Force (N)"
        XyDiagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.[Default]
        XyDiagram2.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagramPane1.Name = "Puissance"
        XyDiagramPane1.PaneID = 0
        XyDiagram2.Panes.AddRange(New DevExpress.XtraCharts.XYDiagramPane() {XyDiagramPane1})
        Me.chrtFVcmj.Diagram = XyDiagram2
        Me.chrtFVcmj.Location = New System.Drawing.Point(3, 3)
        Me.chrtFVcmj.Name = "chrtFVcmj"
        Series2.Name = "Series 1"
        Series2.Points.AddRange(New DevExpress.XtraCharts.SeriesPoint() {SeriesPoint1})
        Series2.View = LineSeriesView3
        Series3.Name = "Series 2"
        Series3.View = LineSeriesView4
        Me.chrtFVcmj.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series2, Series3}
        Me.chrtFVcmj.SeriesTemplate.View = LineSeriesView5
        Me.chrtFVcmj.Size = New System.Drawing.Size(638, 543)
        Me.chrtFVcmj.TabIndex = 0
        Me.chrtFVcmj.Visible = False
        '
        'btnRJ
        '
        Me.btnRJ.Checked = True
        Me.btnRJ.Location = New System.Drawing.Point(26, 217)
        Me.btnRJ.Name = "btnRJ"
        Me.btnRJ.Size = New System.Drawing.Size(641, 27)
        Me.btnRJ.TabIndex = 9
        Me.btnRJ.Text = "Masquer les histogrammes"
        Me.btnRJ.Visible = False
        '
        'layoutPanel
        '
        Me.layoutPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.layoutPanel.Controls.Add(Me.chrtPrincipal)
        Me.layoutPanel.Location = New System.Drawing.Point(26, 250)
        Me.layoutPanel.Name = "layoutPanel"
        Me.layoutPanel.Size = New System.Drawing.Size(657, 552)
        Me.layoutPanel.TabIndex = 4
        '
        'chrtPrincipal
        '
        XyDiagram3.AxisX.Alignment = DevExpress.XtraCharts.AxisAlignment.Zero
        XyDiagram3.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram3.AxisY.VisibleInPanesSerializable = "-1"
        Me.chrtPrincipal.Diagram = XyDiagram3
        Me.chrtPrincipal.Location = New System.Drawing.Point(3, 3)
        Me.chrtPrincipal.Name = "chrtPrincipal"
        Me.chrtPrincipal.PaletteName = "Marquee"
        Series4.Name = "Series 1"
        LineSeriesView6.LineMarkerOptions.BorderVisible = False
        LineSeriesView6.LineStyle.Thickness = 1
        Series4.View = LineSeriesView6
        Me.chrtPrincipal.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series4}
        Me.chrtPrincipal.SeriesTemplate.View = LineSeriesView7
        Me.chrtPrincipal.Size = New System.Drawing.Size(638, 510)
        Me.chrtPrincipal.TabIndex = 0
        '
        'grbxIndices
        '
        Me.grbxIndices.Controls.Add(Me.tabResultats)
        Me.grbxIndices.Controls.Add(Me.lblCoherence3)
        Me.grbxIndices.Controls.Add(Me.lblCoherence2)
        Me.grbxIndices.Controls.Add(Me.lblMoyenne3)
        Me.grbxIndices.Controls.Add(Me.lblMoyenne2)
        Me.grbxIndices.Controls.Add(Me.lblVariable3)
        Me.grbxIndices.Controls.Add(Me.lblVariable2)
        Me.grbxIndices.Controls.Add(Me.lblVariable1)
        Me.grbxIndices.Controls.Add(Me.grbxOrdre)
        Me.grbxIndices.Controls.Add(Me.btnDeselectionner)
        Me.grbxIndices.Controls.Add(Me.btnRecapitulatif)
        Me.grbxIndices.Controls.Add(Me.btnEnregistrer)
        Me.grbxIndices.Controls.Add(Me.lblCoherence1)
        Me.grbxIndices.Controls.Add(Me.lblMoyenne1)
        Me.grbxIndices.Controls.Add(Me.lblCoherenceGr1)
        Me.grbxIndices.Controls.Add(Me.lblMoyenneGr1)
        Me.grbxIndices.Controls.Add(Me.lstIndices)
        Me.grbxIndices.Location = New System.Drawing.Point(689, 351)
        Me.grbxIndices.Name = "grbxIndices"
        Me.grbxIndices.Size = New System.Drawing.Size(556, 479)
        Me.grbxIndices.TabIndex = 3
        Me.grbxIndices.TabStop = False
        Me.grbxIndices.Text = "Indices"
        '
        'tabResultats
        '
        Me.tabResultats.Controls.Add(Me.tabForceMax)
        Me.tabResultats.Controls.Add(Me.tabRFD)
        Me.tabResultats.Controls.Add(Me.TFmax)
        Me.tabResultats.Location = New System.Drawing.Point(10, 258)
        Me.tabResultats.Name = "tabResultats"
        Me.tabResultats.SelectedIndex = 0
        Me.tabResultats.Size = New System.Drawing.Size(405, 221)
        Me.tabResultats.TabIndex = 20
        Me.tabResultats.Visible = False
        '
        'tabForceMax
        '
        Me.tabForceMax.Controls.Add(Me.lblCoherenceFG)
        Me.tabForceMax.Controls.Add(Me.lblMoyenneFDiff)
        Me.tabForceMax.Controls.Add(Me.lblMoyenneFG)
        Me.tabForceMax.Controls.Add(Me.lblMoyenneFD)
        Me.tabForceMax.Controls.Add(Me.lblCoherenceFD)
        Me.tabForceMax.Controls.Add(Me.lblCoherenceF2J)
        Me.tabForceMax.Controls.Add(Me.lblMoyenneF2J)
        Me.tabForceMax.Controls.Add(Me.lblMeanFmax)
        Me.tabForceMax.Controls.Add(Me.lblCoherenceFmax)
        Me.tabForceMax.Controls.Add(Me.Label13)
        Me.tabForceMax.Controls.Add(Me.Label12)
        Me.tabForceMax.Controls.Add(Me.Label5)
        Me.tabForceMax.Controls.Add(Me.Label4)
        Me.tabForceMax.Location = New System.Drawing.Point(4, 25)
        Me.tabForceMax.Name = "tabForceMax"
        Me.tabForceMax.Padding = New System.Windows.Forms.Padding(3)
        Me.tabForceMax.Size = New System.Drawing.Size(397, 192)
        Me.tabForceMax.TabIndex = 0
        Me.tabForceMax.Text = "Force Max (N)"
        Me.tabForceMax.UseVisualStyleBackColor = True
        '
        'lblCoherenceFG
        '
        Me.lblCoherenceFG.AutoSize = True
        Me.lblCoherenceFG.Location = New System.Drawing.Point(226, 87)
        Me.lblCoherenceFG.Name = "lblCoherenceFG"
        Me.lblCoherenceFG.Size = New System.Drawing.Size(59, 17)
        Me.lblCoherenceFG.TabIndex = 25
        Me.lblCoherenceFG.Text = "Label28"
        Me.lblCoherenceFG.Visible = False
        '
        'lblMoyenneFDiff
        '
        Me.lblMoyenneFDiff.AutoSize = True
        Me.lblMoyenneFDiff.Location = New System.Drawing.Point(303, 50)
        Me.lblMoyenneFDiff.Name = "lblMoyenneFDiff"
        Me.lblMoyenneFDiff.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneFDiff.TabIndex = 24
        Me.lblMoyenneFDiff.Text = "Label27"
        Me.lblMoyenneFDiff.Visible = False
        '
        'lblMoyenneFG
        '
        Me.lblMoyenneFG.AutoSize = True
        Me.lblMoyenneFG.Location = New System.Drawing.Point(225, 50)
        Me.lblMoyenneFG.Name = "lblMoyenneFG"
        Me.lblMoyenneFG.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneFG.TabIndex = 23
        Me.lblMoyenneFG.Text = "Label26"
        Me.lblMoyenneFG.Visible = False
        '
        'lblMoyenneFD
        '
        Me.lblMoyenneFD.AutoSize = True
        Me.lblMoyenneFD.Location = New System.Drawing.Point(160, 50)
        Me.lblMoyenneFD.Name = "lblMoyenneFD"
        Me.lblMoyenneFD.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneFD.TabIndex = 22
        Me.lblMoyenneFD.Text = "Label25"
        Me.lblMoyenneFD.Visible = False
        '
        'lblCoherenceFD
        '
        Me.lblCoherenceFD.AutoSize = True
        Me.lblCoherenceFD.Location = New System.Drawing.Point(161, 87)
        Me.lblCoherenceFD.Name = "lblCoherenceFD"
        Me.lblCoherenceFD.Size = New System.Drawing.Size(59, 17)
        Me.lblCoherenceFD.TabIndex = 21
        Me.lblCoherenceFD.Text = "Label24"
        Me.lblCoherenceFD.Visible = False
        '
        'lblCoherenceF2J
        '
        Me.lblCoherenceF2J.AutoSize = True
        Me.lblCoherenceF2J.Location = New System.Drawing.Point(91, 87)
        Me.lblCoherenceF2J.Name = "lblCoherenceF2J"
        Me.lblCoherenceF2J.Size = New System.Drawing.Size(59, 17)
        Me.lblCoherenceF2J.TabIndex = 20
        Me.lblCoherenceF2J.Text = "Label23"
        Me.lblCoherenceF2J.Visible = False
        '
        'lblMoyenneF2J
        '
        Me.lblMoyenneF2J.AutoSize = True
        Me.lblMoyenneF2J.Location = New System.Drawing.Point(92, 50)
        Me.lblMoyenneF2J.Name = "lblMoyenneF2J"
        Me.lblMoyenneF2J.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneF2J.TabIndex = 19
        Me.lblMoyenneF2J.Text = "Label22"
        Me.lblMoyenneF2J.Visible = False
        '
        'lblMeanFmax
        '
        Me.lblMeanFmax.AutoSize = True
        Me.lblMeanFmax.Location = New System.Drawing.Point(20, 50)
        Me.lblMeanFmax.Name = "lblMeanFmax"
        Me.lblMeanFmax.Size = New System.Drawing.Size(66, 17)
        Me.lblMeanFmax.TabIndex = 5
        Me.lblMeanFmax.Text = "Moyenne"
        '
        'lblCoherenceFmax
        '
        Me.lblCoherenceFmax.AutoSize = True
        Me.lblCoherenceFmax.Location = New System.Drawing.Point(9, 89)
        Me.lblCoherenceFmax.Name = "lblCoherenceFmax"
        Me.lblCoherenceFmax.Size = New System.Drawing.Size(77, 17)
        Me.lblCoherenceFmax.TabIndex = 4
        Me.lblCoherenceFmax.Text = "Cohérence"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(219, 18)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(58, 17)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Gauche"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(161, 18)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 17)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Droite"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(277, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 17)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Diff (G-D) (%)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(96, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "2 jbes"
        '
        'tabRFD
        '
        Me.tabRFD.Controls.Add(Me.lblCoherenceRG)
        Me.tabRFD.Controls.Add(Me.lblMoyenneRDiff)
        Me.tabRFD.Controls.Add(Me.lblMoyenneRG)
        Me.tabRFD.Controls.Add(Me.lblMoyenneRD)
        Me.tabRFD.Controls.Add(Me.lblCoherenceRD)
        Me.tabRFD.Controls.Add(Me.lblCoherenceR2J)
        Me.tabRFD.Controls.Add(Me.lblMoyenneR2J)
        Me.tabRFD.Controls.Add(Me.Label16)
        Me.tabRFD.Controls.Add(Me.Label17)
        Me.tabRFD.Controls.Add(Me.Label18)
        Me.tabRFD.Controls.Add(Me.Label19)
        Me.tabRFD.Controls.Add(Me.Label20)
        Me.tabRFD.Controls.Add(Me.Label21)
        Me.tabRFD.Location = New System.Drawing.Point(4, 25)
        Me.tabRFD.Name = "tabRFD"
        Me.tabRFD.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRFD.Size = New System.Drawing.Size(397, 192)
        Me.tabRFD.TabIndex = 1
        Me.tabRFD.Text = "RFD (N/s)"
        Me.tabRFD.UseVisualStyleBackColor = True
        '
        'lblCoherenceRG
        '
        Me.lblCoherenceRG.AutoSize = True
        Me.lblCoherenceRG.Location = New System.Drawing.Point(226, 87)
        Me.lblCoherenceRG.Name = "lblCoherenceRG"
        Me.lblCoherenceRG.Size = New System.Drawing.Size(59, 17)
        Me.lblCoherenceRG.TabIndex = 18
        Me.lblCoherenceRG.Text = "Label28"
        Me.lblCoherenceRG.Visible = False
        '
        'lblMoyenneRDiff
        '
        Me.lblMoyenneRDiff.AutoSize = True
        Me.lblMoyenneRDiff.Location = New System.Drawing.Point(303, 50)
        Me.lblMoyenneRDiff.Name = "lblMoyenneRDiff"
        Me.lblMoyenneRDiff.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneRDiff.TabIndex = 17
        Me.lblMoyenneRDiff.Text = "Label27"
        Me.lblMoyenneRDiff.Visible = False
        '
        'lblMoyenneRG
        '
        Me.lblMoyenneRG.AutoSize = True
        Me.lblMoyenneRG.Location = New System.Drawing.Point(225, 50)
        Me.lblMoyenneRG.Name = "lblMoyenneRG"
        Me.lblMoyenneRG.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneRG.TabIndex = 16
        Me.lblMoyenneRG.Text = "Label26"
        Me.lblMoyenneRG.Visible = False
        '
        'lblMoyenneRD
        '
        Me.lblMoyenneRD.AutoSize = True
        Me.lblMoyenneRD.Location = New System.Drawing.Point(160, 50)
        Me.lblMoyenneRD.Name = "lblMoyenneRD"
        Me.lblMoyenneRD.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneRD.TabIndex = 15
        Me.lblMoyenneRD.Text = "Label25"
        Me.lblMoyenneRD.Visible = False
        '
        'lblCoherenceRD
        '
        Me.lblCoherenceRD.AutoSize = True
        Me.lblCoherenceRD.Location = New System.Drawing.Point(161, 87)
        Me.lblCoherenceRD.Name = "lblCoherenceRD"
        Me.lblCoherenceRD.Size = New System.Drawing.Size(59, 17)
        Me.lblCoherenceRD.TabIndex = 14
        Me.lblCoherenceRD.Text = "Label24"
        Me.lblCoherenceRD.Visible = False
        '
        'lblCoherenceR2J
        '
        Me.lblCoherenceR2J.AutoSize = True
        Me.lblCoherenceR2J.Location = New System.Drawing.Point(91, 87)
        Me.lblCoherenceR2J.Name = "lblCoherenceR2J"
        Me.lblCoherenceR2J.Size = New System.Drawing.Size(59, 17)
        Me.lblCoherenceR2J.TabIndex = 13
        Me.lblCoherenceR2J.Text = "Label23"
        Me.lblCoherenceR2J.Visible = False
        '
        'lblMoyenneR2J
        '
        Me.lblMoyenneR2J.AutoSize = True
        Me.lblMoyenneR2J.Location = New System.Drawing.Point(92, 50)
        Me.lblMoyenneR2J.Name = "lblMoyenneR2J"
        Me.lblMoyenneR2J.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneR2J.TabIndex = 12
        Me.lblMoyenneR2J.Text = "Label22"
        Me.lblMoyenneR2J.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(20, 50)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(66, 17)
        Me.Label16.TabIndex = 11
        Me.Label16.Text = "Moyenne"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(9, 89)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(77, 17)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "Cohérence"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(219, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 17)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "Gauche"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(161, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(46, 17)
        Me.Label19.TabIndex = 8
        Me.Label19.Text = "Droite"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(277, 18)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(95, 17)
        Me.Label20.TabIndex = 7
        Me.Label20.Text = "Diff (G-D) (%)"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(96, 18)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(46, 17)
        Me.Label21.TabIndex = 6
        Me.Label21.Text = "2 jbes"
        '
        'TFmax
        '
        Me.TFmax.Controls.Add(Me.lblMoyenneTdiff)
        Me.TFmax.Controls.Add(Me.lblMoyenneTG)
        Me.TFmax.Controls.Add(Me.lblMoyenneTD)
        Me.TFmax.Controls.Add(Me.lblMoyenneT2J)
        Me.TFmax.Controls.Add(Me.Label25)
        Me.TFmax.Controls.Add(Me.Label14)
        Me.TFmax.Controls.Add(Me.Label15)
        Me.TFmax.Controls.Add(Me.Label23)
        Me.TFmax.Controls.Add(Me.Label24)
        Me.TFmax.Controls.Add(Me.lblTypeCourbe_Analyse)
        Me.TFmax.Controls.Add(Me.cbxTypeCourbe_Analyse)
        Me.TFmax.Location = New System.Drawing.Point(4, 25)
        Me.TFmax.Name = "TFmax"
        Me.TFmax.Padding = New System.Windows.Forms.Padding(3)
        Me.TFmax.Size = New System.Drawing.Size(397, 192)
        Me.TFmax.TabIndex = 2
        Me.TFmax.Text = "Tps Fmax (s)"
        Me.TFmax.UseVisualStyleBackColor = True
        '
        'lblMoyenneTdiff
        '
        Me.lblMoyenneTdiff.AutoSize = True
        Me.lblMoyenneTdiff.Location = New System.Drawing.Point(303, 50)
        Me.lblMoyenneTdiff.Name = "lblMoyenneTdiff"
        Me.lblMoyenneTdiff.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneTdiff.TabIndex = 31
        Me.lblMoyenneTdiff.Text = "Label27"
        Me.lblMoyenneTdiff.Visible = False
        '
        'lblMoyenneTG
        '
        Me.lblMoyenneTG.AutoSize = True
        Me.lblMoyenneTG.Location = New System.Drawing.Point(225, 50)
        Me.lblMoyenneTG.Name = "lblMoyenneTG"
        Me.lblMoyenneTG.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneTG.TabIndex = 30
        Me.lblMoyenneTG.Text = "Label26"
        Me.lblMoyenneTG.Visible = False
        '
        'lblMoyenneTD
        '
        Me.lblMoyenneTD.AutoSize = True
        Me.lblMoyenneTD.Location = New System.Drawing.Point(160, 50)
        Me.lblMoyenneTD.Name = "lblMoyenneTD"
        Me.lblMoyenneTD.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneTD.TabIndex = 29
        Me.lblMoyenneTD.Text = "Label25"
        Me.lblMoyenneTD.Visible = False
        '
        'lblMoyenneT2J
        '
        Me.lblMoyenneT2J.AutoSize = True
        Me.lblMoyenneT2J.Location = New System.Drawing.Point(92, 50)
        Me.lblMoyenneT2J.Name = "lblMoyenneT2J"
        Me.lblMoyenneT2J.Size = New System.Drawing.Size(59, 17)
        Me.lblMoyenneT2J.TabIndex = 28
        Me.lblMoyenneT2J.Text = "Label22"
        Me.lblMoyenneT2J.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(20, 50)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(66, 17)
        Me.Label25.TabIndex = 27
        Me.Label25.Text = "Moyenne"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(219, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(58, 17)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "Gauche"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(161, 18)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(46, 17)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "Droite"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(277, 18)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(95, 17)
        Me.Label23.TabIndex = 24
        Me.Label23.Text = "Diff (G-D) (%)"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(96, 18)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(46, 17)
        Me.Label24.TabIndex = 23
        Me.Label24.Text = "2 jbes"
        '
        'lblTypeCourbe_Analyse
        '
        Me.lblTypeCourbe_Analyse.AutoSize = True
        Me.lblTypeCourbe_Analyse.Location = New System.Drawing.Point(6, 153)
        Me.lblTypeCourbe_Analyse.Name = "lblTypeCourbe_Analyse"
        Me.lblTypeCourbe_Analyse.Size = New System.Drawing.Size(197, 17)
        Me.lblTypeCourbe_Analyse.TabIndex = 22
        Me.lblTypeCourbe_Analyse.Text = "Choisissez le type de courbe :"
        Me.lblTypeCourbe_Analyse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTypeCourbe_Analyse.Visible = False
        '
        'cbxTypeCourbe_Analyse
        '
        Me.cbxTypeCourbe_Analyse.FormattingEnabled = True
        Me.cbxTypeCourbe_Analyse.Items.AddRange(New Object() {"1- pic précoce et regression", "2- pic précoce et plateau", "3- plateau tardif ", "4- montée progressive et pic tardif", "5- autres"})
        Me.cbxTypeCourbe_Analyse.Location = New System.Drawing.Point(209, 150)
        Me.cbxTypeCourbe_Analyse.Name = "cbxTypeCourbe_Analyse"
        Me.cbxTypeCourbe_Analyse.Size = New System.Drawing.Size(167, 24)
        Me.cbxTypeCourbe_Analyse.TabIndex = 21
        Me.cbxTypeCourbe_Analyse.Visible = False
        '
        'lblCoherence3
        '
        Me.lblCoherence3.AutoSize = True
        Me.lblCoherence3.Location = New System.Drawing.Point(333, 327)
        Me.lblCoherence3.Name = "lblCoherence3"
        Me.lblCoherence3.Size = New System.Drawing.Size(99, 17)
        Me.lblCoherence3.TabIndex = 17
        Me.lblCoherence3.Text = "lblCoherence3"
        Me.lblCoherence3.Visible = False
        '
        'lblCoherence2
        '
        Me.lblCoherence2.AutoSize = True
        Me.lblCoherence2.Location = New System.Drawing.Point(239, 327)
        Me.lblCoherence2.Name = "lblCoherence2"
        Me.lblCoherence2.Size = New System.Drawing.Size(99, 17)
        Me.lblCoherence2.TabIndex = 16
        Me.lblCoherence2.Text = "lblCoherence2"
        Me.lblCoherence2.Visible = False
        '
        'lblMoyenne3
        '
        Me.lblMoyenne3.AutoSize = True
        Me.lblMoyenne3.Location = New System.Drawing.Point(333, 295)
        Me.lblMoyenne3.Name = "lblMoyenne3"
        Me.lblMoyenne3.Size = New System.Drawing.Size(88, 17)
        Me.lblMoyenne3.TabIndex = 14
        Me.lblMoyenne3.Text = "lblMoyenne3"
        Me.lblMoyenne3.Visible = False
        '
        'lblMoyenne2
        '
        Me.lblMoyenne2.AutoSize = True
        Me.lblMoyenne2.Location = New System.Drawing.Point(239, 295)
        Me.lblMoyenne2.Name = "lblMoyenne2"
        Me.lblMoyenne2.Size = New System.Drawing.Size(88, 17)
        Me.lblMoyenne2.TabIndex = 13
        Me.lblMoyenne2.Text = "lblMoyenne2"
        Me.lblMoyenne2.Visible = False
        '
        'lblVariable3
        '
        Me.lblVariable3.AutoSize = True
        Me.lblVariable3.Location = New System.Drawing.Point(333, 263)
        Me.lblVariable3.Name = "lblVariable3"
        Me.lblVariable3.Size = New System.Drawing.Size(82, 17)
        Me.lblVariable3.TabIndex = 11
        Me.lblVariable3.Text = "lblVariable3"
        Me.lblVariable3.Visible = False
        '
        'lblVariable2
        '
        Me.lblVariable2.AutoSize = True
        Me.lblVariable2.Location = New System.Drawing.Point(239, 263)
        Me.lblVariable2.Name = "lblVariable2"
        Me.lblVariable2.Size = New System.Drawing.Size(82, 17)
        Me.lblVariable2.TabIndex = 10
        Me.lblVariable2.Text = "lblVariable2"
        Me.lblVariable2.Visible = False
        '
        'lblVariable1
        '
        Me.lblVariable1.AutoSize = True
        Me.lblVariable1.Location = New System.Drawing.Point(103, 263)
        Me.lblVariable1.Name = "lblVariable1"
        Me.lblVariable1.Size = New System.Drawing.Size(82, 17)
        Me.lblVariable1.TabIndex = 9
        Me.lblVariable1.Text = "lblVariable1"
        Me.lblVariable1.Visible = False
        '
        'grbxOrdre
        '
        Me.grbxOrdre.Controls.Add(Me.numOrdrePoly)
        Me.grbxOrdre.Controls.Add(Me.lblOrdrePoly)
        Me.grbxOrdre.Location = New System.Drawing.Point(8, 377)
        Me.grbxOrdre.Name = "grbxOrdre"
        Me.grbxOrdre.Size = New System.Drawing.Size(252, 68)
        Me.grbxOrdre.TabIndex = 8
        Me.grbxOrdre.TabStop = False
        Me.grbxOrdre.Text = "Ajustement puissance"
        Me.grbxOrdre.Visible = False
        '
        'numOrdrePoly
        '
        Me.numOrdrePoly.Location = New System.Drawing.Point(169, 31)
        Me.numOrdrePoly.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numOrdrePoly.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numOrdrePoly.Name = "numOrdrePoly"
        Me.numOrdrePoly.Size = New System.Drawing.Size(62, 22)
        Me.numOrdrePoly.TabIndex = 1
        Me.numOrdrePoly.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'lblOrdrePoly
        '
        Me.lblOrdrePoly.AutoSize = True
        Me.lblOrdrePoly.Location = New System.Drawing.Point(13, 33)
        Me.lblOrdrePoly.Name = "lblOrdrePoly"
        Me.lblOrdrePoly.Size = New System.Drawing.Size(127, 17)
        Me.lblOrdrePoly.TabIndex = 0
        Me.lblOrdrePoly.Text = "Polynôme d'ordre :"
        '
        'btnDeselectionner
        '
        Me.btnDeselectionner.Location = New System.Drawing.Point(419, 263)
        Me.btnDeselectionner.Name = "btnDeselectionner"
        Me.btnDeselectionner.Size = New System.Drawing.Size(121, 32)
        Me.btnDeselectionner.TabIndex = 7
        Me.btnDeselectionner.Text = "Deselectionner"
        Me.btnDeselectionner.UseVisualStyleBackColor = True
        '
        'btnRecapitulatif
        '
        Me.btnRecapitulatif.Location = New System.Drawing.Point(419, 339)
        Me.btnRecapitulatif.Name = "btnRecapitulatif"
        Me.btnRecapitulatif.Size = New System.Drawing.Size(121, 32)
        Me.btnRecapitulatif.TabIndex = 6
        Me.btnRecapitulatif.Text = "Récapitulatif"
        Me.btnRecapitulatif.UseVisualStyleBackColor = True
        Me.btnRecapitulatif.Visible = False
        '
        'btnEnregistrer
        '
        Me.btnEnregistrer.Location = New System.Drawing.Point(419, 301)
        Me.btnEnregistrer.Name = "btnEnregistrer"
        Me.btnEnregistrer.Size = New System.Drawing.Size(121, 32)
        Me.btnEnregistrer.TabIndex = 5
        Me.btnEnregistrer.Text = "Enregistrer"
        Me.btnEnregistrer.UseVisualStyleBackColor = True
        '
        'lblCoherence1
        '
        Me.lblCoherence1.AutoSize = True
        Me.lblCoherence1.Location = New System.Drawing.Point(103, 327)
        Me.lblCoherence1.Name = "lblCoherence1"
        Me.lblCoherence1.Size = New System.Drawing.Size(99, 17)
        Me.lblCoherence1.TabIndex = 4
        Me.lblCoherence1.Text = "lblCoherence1"
        Me.lblCoherence1.Visible = False
        '
        'lblMoyenne1
        '
        Me.lblMoyenne1.AutoSize = True
        Me.lblMoyenne1.Location = New System.Drawing.Point(103, 295)
        Me.lblMoyenne1.Name = "lblMoyenne1"
        Me.lblMoyenne1.Size = New System.Drawing.Size(88, 17)
        Me.lblMoyenne1.TabIndex = 3
        Me.lblMoyenne1.Text = "lblMoyenne1"
        Me.lblMoyenne1.Visible = False
        '
        'lblCoherenceGr1
        '
        Me.lblCoherenceGr1.AutoSize = True
        Me.lblCoherenceGr1.Location = New System.Drawing.Point(19, 327)
        Me.lblCoherenceGr1.Name = "lblCoherenceGr1"
        Me.lblCoherenceGr1.Size = New System.Drawing.Size(77, 17)
        Me.lblCoherenceGr1.TabIndex = 2
        Me.lblCoherenceGr1.Text = "Cohérence"
        '
        'lblMoyenneGr1
        '
        Me.lblMoyenneGr1.AutoSize = True
        Me.lblMoyenneGr1.Location = New System.Drawing.Point(19, 297)
        Me.lblMoyenneGr1.Name = "lblMoyenneGr1"
        Me.lblMoyenneGr1.Size = New System.Drawing.Size(66, 17)
        Me.lblMoyenneGr1.TabIndex = 1
        Me.lblMoyenneGr1.Text = "Moyenne"
        '
        'lstIndices
        '
        Me.lstIndices.CheckBoxes = True
        Me.lstIndices.GridLines = True
        Me.lstIndices.Location = New System.Drawing.Point(8, 21)
        Me.lstIndices.Name = "lstIndices"
        Me.lstIndices.Size = New System.Drawing.Size(532, 223)
        Me.lstIndices.TabIndex = 0
        Me.lstIndices.UseCompatibleStateImageBehavior = False
        Me.lstIndices.View = System.Windows.Forms.View.Details
        '
        'grbxSujet
        '
        Me.grbxSujet.Controls.Add(Me.lblMasse)
        Me.grbxSujet.Controls.Add(Me.lblSujet)
        Me.grbxSujet.Controls.Add(Me.Label3)
        Me.grbxSujet.Controls.Add(Me.Label2)
        Me.grbxSujet.Location = New System.Drawing.Point(689, 234)
        Me.grbxSujet.Name = "grbxSujet"
        Me.grbxSujet.Size = New System.Drawing.Size(556, 96)
        Me.grbxSujet.TabIndex = 2
        Me.grbxSujet.TabStop = False
        Me.grbxSujet.Text = "Sujet/Athlète"
        '
        'lblMasse
        '
        Me.lblMasse.AutoSize = True
        Me.lblMasse.Location = New System.Drawing.Point(76, 64)
        Me.lblMasse.Name = "lblMasse"
        Me.lblMasse.Size = New System.Drawing.Size(51, 17)
        Me.lblMasse.TabIndex = 3
        Me.lblMasse.Text = "Label5"
        '
        'lblSujet
        '
        Me.lblSujet.AutoSize = True
        Me.lblSujet.Location = New System.Drawing.Point(76, 30)
        Me.lblSujet.Name = "lblSujet"
        Me.lblSujet.Size = New System.Drawing.Size(51, 17)
        Me.lblSujet.TabIndex = 2
        Me.lblSujet.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 17)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Masse"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Nom"
        '
        'groupeChoixAnalyse
        '
        Me.groupeChoixAnalyse.Controls.Add(Me.chkLstFiles1)
        Me.groupeChoixAnalyse.Controls.Add(Me.btnEffacer)
        Me.groupeChoixAnalyse.Controls.Add(Me.btnAnalyse)
        Me.groupeChoixAnalyse.Controls.Add(Me.clstFichAnalyse)
        Me.groupeChoixAnalyse.Controls.Add(Me.btnChoix)
        Me.groupeChoixAnalyse.Location = New System.Drawing.Point(23, 24)
        Me.groupeChoixAnalyse.Name = "groupeChoixAnalyse"
        Me.groupeChoixAnalyse.Size = New System.Drawing.Size(1222, 183)
        Me.groupeChoixAnalyse.TabIndex = 0
        Me.groupeChoixAnalyse.TabStop = False
        Me.groupeChoixAnalyse.Text = "Fichiers à analyser"
        '
        'chkLstFiles1
        '
        Me.chkLstFiles1.CheckOnClick = True
        Me.chkLstFiles1.FormattingEnabled = True
        Me.chkLstFiles1.Location = New System.Drawing.Point(175, 31)
        Me.chkLstFiles1.Name = "chkLstFiles1"
        Me.chkLstFiles1.Size = New System.Drawing.Size(469, 140)
        Me.chkLstFiles1.TabIndex = 4
        '
        'btnEffacer
        '
        Me.btnEffacer.Location = New System.Drawing.Point(666, 111)
        Me.btnEffacer.Name = "btnEffacer"
        Me.btnEffacer.Size = New System.Drawing.Size(540, 60)
        Me.btnEffacer.TabIndex = 3
        Me.btnEffacer.Text = "Effacer l'analyse"
        Me.btnEffacer.UseVisualStyleBackColor = True
        Me.btnEffacer.Visible = False
        '
        'btnAnalyse
        '
        Me.btnAnalyse.Location = New System.Drawing.Point(666, 31)
        Me.btnAnalyse.Name = "btnAnalyse"
        Me.btnAnalyse.Size = New System.Drawing.Size(540, 140)
        Me.btnAnalyse.TabIndex = 2
        Me.btnAnalyse.Text = "Lancer l'analyse"
        Me.btnAnalyse.UseVisualStyleBackColor = True
        '
        'clstFichAnalyse
        '
        Me.clstFichAnalyse.FormattingEnabled = True
        Me.clstFichAnalyse.Location = New System.Drawing.Point(389, 31)
        Me.clstFichAnalyse.Name = "clstFichAnalyse"
        Me.clstFichAnalyse.ScrollAlwaysVisible = True
        Me.clstFichAnalyse.Size = New System.Drawing.Size(255, 140)
        Me.clstFichAnalyse.TabIndex = 1
        '
        'btnChoix
        '
        Me.btnChoix.Location = New System.Drawing.Point(20, 31)
        Me.btnChoix.Name = "btnChoix"
        Me.btnChoix.Size = New System.Drawing.Size(126, 140)
        Me.btnChoix.TabIndex = 0
        Me.btnChoix.Text = "Choix des fichiers à analyser"
        Me.btnChoix.UseVisualStyleBackColor = True
        '
        'tabConfig
        '
        Me.tabConfig.Controls.Add(Me.Label22)
        Me.tabConfig.Controls.Add(Me.PictureBox1)
        Me.tabConfig.Controls.Add(Me.grbNbEssais)
        Me.tabConfig.Controls.Add(Me.grpBlessures)
        Me.tabConfig.Controls.Add(Me.grbAcquisition)
        Me.tabConfig.Controls.Add(Me.lblNomConfig)
        Me.tabConfig.Controls.Add(Me.Label8)
        Me.tabConfig.Controls.Add(Me.lblDateModif)
        Me.tabConfig.Controls.Add(Me.Label7)
        Me.tabConfig.Controls.Add(Me.grbxPoly)
        Me.tabConfig.Controls.Add(Me.btnEnregistreConfig)
        Me.tabConfig.Controls.Add(Me.btnChargeConfig)
        Me.tabConfig.Controls.Add(Me.grpBxSeuils)
        Me.tabConfig.Controls.Add(Me.lblRepertoire)
        Me.tabConfig.Controls.Add(Me.txtRepertoire)
        Me.tabConfig.Controls.Add(Me.btnRepertoire)
        Me.tabConfig.Location = New System.Drawing.Point(4, 25)
        Me.tabConfig.Name = "tabConfig"
        Me.tabConfig.Size = New System.Drawing.Size(1414, 985)
        Me.tabConfig.TabIndex = 4
        Me.tabConfig.Text = "Configuration"
        Me.tabConfig.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(276, 573)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(200, 17)
        Me.Label22.TabIndex = 21
        Me.Label22.Text = "Version du 28/12/2015 ; 16:48"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Kinesis.My.Resources.Resources.logo2
        Me.PictureBox1.Location = New System.Drawing.Point(146, 510)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 90)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 20
        Me.PictureBox1.TabStop = False
        '
        'grbNbEssais
        '
        Me.grbNbEssais.Controls.Add(Me.numMinFV)
        Me.grbNbEssais.Controls.Add(Me.lblMinFV)
        Me.grbNbEssais.Controls.Add(Me.numMinRJ)
        Me.grbNbEssais.Controls.Add(Me.lblMinRJ)
        Me.grbNbEssais.Controls.Add(Me.numMinISO)
        Me.grbNbEssais.Controls.Add(Me.lblMinISO)
        Me.grbNbEssais.Controls.Add(Me.numMinK)
        Me.grbNbEssais.Controls.Add(Me.lblMinK)
        Me.grbNbEssais.Controls.Add(Me.numMinSJ)
        Me.grbNbEssais.Controls.Add(Me.lblMinSJ)
        Me.grbNbEssais.Controls.Add(Me.numMinCMJ)
        Me.grbNbEssais.Controls.Add(Me.lblMinCMJ)
        Me.grbNbEssais.Location = New System.Drawing.Point(527, 487)
        Me.grbNbEssais.Name = "grbNbEssais"
        Me.grbNbEssais.Size = New System.Drawing.Size(444, 234)
        Me.grbNbEssais.TabIndex = 19
        Me.grbNbEssais.TabStop = False
        Me.grbNbEssais.Text = "Nombre d'essais minimum"
        '
        'numMinFV
        '
        Me.numMinFV.Location = New System.Drawing.Point(139, 180)
        Me.numMinFV.Name = "numMinFV"
        Me.numMinFV.Size = New System.Drawing.Size(100, 22)
        Me.numMinFV.TabIndex = 11
        '
        'lblMinFV
        '
        Me.lblMinFV.AutoSize = True
        Me.lblMinFV.Location = New System.Drawing.Point(53, 180)
        Me.lblMinFV.Name = "lblMinFV"
        Me.lblMinFV.Size = New System.Drawing.Size(59, 17)
        Me.lblMinFV.TabIndex = 10
        Me.lblMinFV.Text = "Pour FV"
        '
        'numMinRJ
        '
        Me.numMinRJ.Location = New System.Drawing.Point(139, 152)
        Me.numMinRJ.Name = "numMinRJ"
        Me.numMinRJ.Size = New System.Drawing.Size(100, 22)
        Me.numMinRJ.TabIndex = 9
        '
        'lblMinRJ
        '
        Me.lblMinRJ.AutoSize = True
        Me.lblMinRJ.Location = New System.Drawing.Point(53, 152)
        Me.lblMinRJ.Name = "lblMinRJ"
        Me.lblMinRJ.Size = New System.Drawing.Size(59, 17)
        Me.lblMinRJ.TabIndex = 8
        Me.lblMinRJ.Text = "Pour RJ"
        '
        'numMinISO
        '
        Me.numMinISO.Location = New System.Drawing.Point(139, 124)
        Me.numMinISO.Name = "numMinISO"
        Me.numMinISO.Size = New System.Drawing.Size(100, 22)
        Me.numMinISO.TabIndex = 7
        '
        'lblMinISO
        '
        Me.lblMinISO.AutoSize = True
        Me.lblMinISO.Location = New System.Drawing.Point(53, 124)
        Me.lblMinISO.Name = "lblMinISO"
        Me.lblMinISO.Size = New System.Drawing.Size(65, 17)
        Me.lblMinISO.TabIndex = 6
        Me.lblMinISO.Text = "Pour ISO"
        '
        'numMinK
        '
        Me.numMinK.Location = New System.Drawing.Point(139, 96)
        Me.numMinK.Name = "numMinK"
        Me.numMinK.Size = New System.Drawing.Size(100, 22)
        Me.numMinK.TabIndex = 5
        '
        'lblMinK
        '
        Me.lblMinK.AutoSize = True
        Me.lblMinK.Location = New System.Drawing.Point(53, 96)
        Me.lblMinK.Name = "lblMinK"
        Me.lblMinK.Size = New System.Drawing.Size(51, 17)
        Me.lblMinK.TabIndex = 4
        Me.lblMinK.Text = "Pour K"
        '
        'numMinSJ
        '
        Me.numMinSJ.Location = New System.Drawing.Point(139, 68)
        Me.numMinSJ.Name = "numMinSJ"
        Me.numMinSJ.Size = New System.Drawing.Size(100, 22)
        Me.numMinSJ.TabIndex = 3
        '
        'lblMinSJ
        '
        Me.lblMinSJ.AutoSize = True
        Me.lblMinSJ.Location = New System.Drawing.Point(53, 68)
        Me.lblMinSJ.Name = "lblMinSJ"
        Me.lblMinSJ.Size = New System.Drawing.Size(58, 17)
        Me.lblMinSJ.TabIndex = 2
        Me.lblMinSJ.Text = "Pour SJ"
        '
        'numMinCMJ
        '
        Me.numMinCMJ.Location = New System.Drawing.Point(139, 41)
        Me.numMinCMJ.Name = "numMinCMJ"
        Me.numMinCMJ.Size = New System.Drawing.Size(100, 22)
        Me.numMinCMJ.TabIndex = 1
        '
        'lblMinCMJ
        '
        Me.lblMinCMJ.AutoSize = True
        Me.lblMinCMJ.Location = New System.Drawing.Point(53, 41)
        Me.lblMinCMJ.Name = "lblMinCMJ"
        Me.lblMinCMJ.Size = New System.Drawing.Size(69, 17)
        Me.lblMinCMJ.TabIndex = 0
        Me.lblMinCMJ.Text = "Pour CMJ"
        '
        'grpBlessures
        '
        Me.grpBlessures.Controls.Add(Me.btnSuppBlessure)
        Me.grpBlessures.Controls.Add(Me.btnBlessure)
        Me.grpBlessures.Controls.Add(Me.Label11)
        Me.grpBlessures.Controls.Add(Me.txtBlessure)
        Me.grpBlessures.Controls.Add(Me.btnAfficher)
        Me.grpBlessures.Controls.Add(Me.lstBlessures)
        Me.grpBlessures.Location = New System.Drawing.Point(774, 241)
        Me.grpBlessures.Name = "grpBlessures"
        Me.grpBlessures.Size = New System.Drawing.Size(326, 195)
        Me.grpBlessures.TabIndex = 18
        Me.grpBlessures.TabStop = False
        Me.grpBlessures.Text = "Blessures"
        '
        'btnSuppBlessure
        '
        Me.btnSuppBlessure.Location = New System.Drawing.Point(9, 131)
        Me.btnSuppBlessure.Name = "btnSuppBlessure"
        Me.btnSuppBlessure.Size = New System.Drawing.Size(129, 42)
        Me.btnSuppBlessure.TabIndex = 12
        Me.btnSuppBlessure.Text = "Supprimez blessure"
        Me.btnSuppBlessure.UseVisualStyleBackColor = True
        '
        'btnBlessure
        '
        Me.btnBlessure.Location = New System.Drawing.Point(9, 86)
        Me.btnBlessure.Name = "btnBlessure"
        Me.btnBlessure.Size = New System.Drawing.Size(129, 28)
        Me.btnBlessure.TabIndex = 11
        Me.btnBlessure.Text = "Ajouter blessure"
        Me.btnBlessure.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 26)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(123, 17)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Blessure à ajouter"
        '
        'txtBlessure
        '
        Me.txtBlessure.Location = New System.Drawing.Point(9, 49)
        Me.txtBlessure.Name = "txtBlessure"
        Me.txtBlessure.Size = New System.Drawing.Size(129, 22)
        Me.txtBlessure.TabIndex = 9
        '
        'btnAfficher
        '
        Me.btnAfficher.Location = New System.Drawing.Point(9, 26)
        Me.btnAfficher.Name = "btnAfficher"
        Me.btnAfficher.Size = New System.Drawing.Size(97, 48)
        Me.btnAfficher.TabIndex = 8
        Me.btnAfficher.Text = "Afficher les blessures"
        Me.btnAfficher.UseVisualStyleBackColor = True
        Me.btnAfficher.Visible = False
        '
        'lstBlessures
        '
        Me.lstBlessures.FormattingEnabled = True
        Me.lstBlessures.ItemHeight = 16
        Me.lstBlessures.Location = New System.Drawing.Point(156, 26)
        Me.lstBlessures.Name = "lstBlessures"
        Me.lstBlessures.Size = New System.Drawing.Size(151, 148)
        Me.lstBlessures.TabIndex = 0
        '
        'grbAcquisition
        '
        Me.grbAcquisition.Controls.Add(Me.txtNomVoie)
        Me.grbAcquisition.Controls.Add(Me.lblNomVoie)
        Me.grbAcquisition.Controls.Add(Me.txtCoeffB_Config)
        Me.grbAcquisition.Controls.Add(Me.txtCoeffA_Config)
        Me.grbAcquisition.Controls.Add(Me.txtNomCarte)
        Me.grbAcquisition.Controls.Add(Me.lblNomCarte)
        Me.grbAcquisition.Controls.Add(Me.Label10)
        Me.grbAcquisition.Controls.Add(Me.Label9)
        Me.grbAcquisition.Location = New System.Drawing.Point(138, 241)
        Me.grbAcquisition.Name = "grbAcquisition"
        Me.grbAcquisition.Size = New System.Drawing.Size(344, 196)
        Me.grbAcquisition.TabIndex = 17
        Me.grbAcquisition.TabStop = False
        Me.grbAcquisition.Text = "Acquisition"
        '
        'txtNomVoie
        '
        Me.txtNomVoie.Location = New System.Drawing.Point(145, 75)
        Me.txtNomVoie.Name = "txtNomVoie"
        Me.txtNomVoie.Size = New System.Drawing.Size(90, 22)
        Me.txtNomVoie.TabIndex = 7
        Me.txtNomVoie.Text = "Ai0"
        '
        'lblNomVoie
        '
        Me.lblNomVoie.AutoSize = True
        Me.lblNomVoie.Location = New System.Drawing.Point(24, 74)
        Me.lblNomVoie.Name = "lblNomVoie"
        Me.lblNomVoie.Size = New System.Drawing.Size(102, 17)
        Me.lblNomVoie.TabIndex = 6
        Me.lblNomVoie.Text = "Nom de la voie"
        '
        'txtCoeffB_Config
        '
        Me.txtCoeffB_Config.Location = New System.Drawing.Point(145, 131)
        Me.txtCoeffB_Config.Name = "txtCoeffB_Config"
        Me.txtCoeffB_Config.Size = New System.Drawing.Size(90, 22)
        Me.txtCoeffB_Config.TabIndex = 5
        Me.txtCoeffB_Config.Text = "0"
        '
        'txtCoeffA_Config
        '
        Me.txtCoeffA_Config.Location = New System.Drawing.Point(145, 103)
        Me.txtCoeffA_Config.Name = "txtCoeffA_Config"
        Me.txtCoeffA_Config.Size = New System.Drawing.Size(90, 22)
        Me.txtCoeffA_Config.TabIndex = 4
        Me.txtCoeffA_Config.Text = "1"
        '
        'txtNomCarte
        '
        Me.txtNomCarte.Location = New System.Drawing.Point(145, 48)
        Me.txtNomCarte.Name = "txtNomCarte"
        Me.txtNomCarte.Size = New System.Drawing.Size(90, 22)
        Me.txtNomCarte.TabIndex = 3
        Me.txtNomCarte.Text = "Dev1"
        '
        'lblNomCarte
        '
        Me.lblNomCarte.AutoSize = True
        Me.lblNomCarte.Location = New System.Drawing.Point(24, 47)
        Me.lblNomCarte.Name = "lblNomCarte"
        Me.lblNomCarte.Size = New System.Drawing.Size(108, 17)
        Me.lblNomCarte.TabIndex = 2
        Me.lblNomCarte.Text = "Nom de la carte"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(45, 134)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 17)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Coefficient B"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(45, 103)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 17)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Coefficient A"
        '
        'lblNomConfig
        '
        Me.lblNomConfig.AutoSize = True
        Me.lblNomConfig.Location = New System.Drawing.Point(501, 67)
        Me.lblNomConfig.Name = "lblNomConfig"
        Me.lblNomConfig.Size = New System.Drawing.Size(51, 17)
        Me.lblNomConfig.TabIndex = 16
        Me.lblNomConfig.Text = "Label9"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(291, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(156, 17)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Fichier de configuration"
        '
        'lblDateModif
        '
        Me.lblDateModif.AutoSize = True
        Me.lblDateModif.Location = New System.Drawing.Point(501, 115)
        Me.lblDateModif.Name = "lblDateModif"
        Me.lblDateModif.Size = New System.Drawing.Size(51, 17)
        Me.lblDateModif.TabIndex = 14
        Me.lblDateModif.Text = "Label8"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(288, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(194, 17)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Date de dernière modification"
        '
        'grbxPoly
        '
        Me.grbxPoly.Controls.Add(Me.numOrdreReg)
        Me.grbxPoly.Controls.Add(Me.Label6)
        Me.grbxPoly.Location = New System.Drawing.Point(521, 362)
        Me.grbxPoly.Name = "grbxPoly"
        Me.grbxPoly.Size = New System.Drawing.Size(225, 75)
        Me.grbxPoly.TabIndex = 12
        Me.grbxPoly.TabStop = False
        Me.grbxPoly.Text = "Regression polynomiale"
        '
        'numOrdreReg
        '
        Me.numOrdreReg.Location = New System.Drawing.Point(111, 33)
        Me.numOrdreReg.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numOrdreReg.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numOrdreReg.Name = "numOrdreReg"
        Me.numOrdreReg.Size = New System.Drawing.Size(86, 22)
        Me.numOrdreReg.TabIndex = 1
        Me.numOrdreReg.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(36, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 17)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Ordre"
        '
        'btnEnregistreConfig
        '
        Me.btnEnregistreConfig.Location = New System.Drawing.Point(127, 147)
        Me.btnEnregistreConfig.Name = "btnEnregistreConfig"
        Me.btnEnregistreConfig.Size = New System.Drawing.Size(119, 71)
        Me.btnEnregistreConfig.TabIndex = 11
        Me.btnEnregistreConfig.Text = "Enregistrer la configuration"
        Me.btnEnregistreConfig.UseVisualStyleBackColor = True
        '
        'btnChargeConfig
        '
        Me.btnChargeConfig.Location = New System.Drawing.Point(127, 60)
        Me.btnChargeConfig.Name = "btnChargeConfig"
        Me.btnChargeConfig.Size = New System.Drawing.Size(119, 72)
        Me.btnChargeConfig.TabIndex = 10
        Me.btnChargeConfig.Text = "Charger une configuration"
        Me.btnChargeConfig.UseVisualStyleBackColor = True
        '
        'grpBxSeuils
        '
        Me.grpBxSeuils.Controls.Add(Me.numSeuilFz1)
        Me.grpBxSeuils.Controls.Add(Me.numSeuilFz2)
        Me.grpBxSeuils.Controls.Add(Me.lblFz1)
        Me.grpBxSeuils.Controls.Add(Me.lblFz2)
        Me.grpBxSeuils.Location = New System.Drawing.Point(521, 241)
        Me.grpBxSeuils.Name = "grpBxSeuils"
        Me.grpBxSeuils.Size = New System.Drawing.Size(225, 115)
        Me.grpBxSeuils.TabIndex = 9
        Me.grpBxSeuils.TabStop = False
        Me.grpBxSeuils.Text = "Réglage des seuils "
        '
        'numSeuilFz1
        '
        Me.numSeuilFz1.Location = New System.Drawing.Point(111, 37)
        Me.numSeuilFz1.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.numSeuilFz1.Name = "numSeuilFz1"
        Me.numSeuilFz1.Size = New System.Drawing.Size(86, 22)
        Me.numSeuilFz1.TabIndex = 7
        Me.numSeuilFz1.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'numSeuilFz2
        '
        Me.numSeuilFz2.Location = New System.Drawing.Point(111, 77)
        Me.numSeuilFz2.Maximum = New Decimal(New Integer() {19, 0, 0, 0})
        Me.numSeuilFz2.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numSeuilFz2.Name = "numSeuilFz2"
        Me.numSeuilFz2.Size = New System.Drawing.Size(86, 22)
        Me.numSeuilFz2.TabIndex = 8
        Me.numSeuilFz2.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'lblFz1
        '
        Me.lblFz1.AutoSize = True
        Me.lblFz1.Location = New System.Drawing.Point(15, 37)
        Me.lblFz1.Name = "lblFz1"
        Me.lblFz1.Size = New System.Drawing.Size(90, 17)
        Me.lblFz1.TabIndex = 4
        Me.lblFz1.Text = "Seuil Fz1 (N)"
        '
        'lblFz2
        '
        Me.lblFz2.AutoSize = True
        Me.lblFz2.Location = New System.Drawing.Point(17, 77)
        Me.lblFz2.Name = "lblFz2"
        Me.lblFz2.Size = New System.Drawing.Size(90, 17)
        Me.lblFz2.TabIndex = 6
        Me.lblFz2.Text = "Seuil Fz2 (N)"
        '
        'lblRepertoire
        '
        Me.lblRepertoire.AutoSize = True
        Me.lblRepertoire.Location = New System.Drawing.Point(293, 176)
        Me.lblRepertoire.Name = "lblRepertoire"
        Me.lblRepertoire.Size = New System.Drawing.Size(108, 17)
        Me.lblRepertoire.TabIndex = 3
        Me.lblRepertoire.Text = "Répertoire cible"
        '
        'txtRepertoire
        '
        Me.txtRepertoire.Location = New System.Drawing.Point(296, 196)
        Me.txtRepertoire.Name = "txtRepertoire"
        Me.txtRepertoire.Size = New System.Drawing.Size(450, 22)
        Me.txtRepertoire.TabIndex = 2
        '
        'btnRepertoire
        '
        Me.btnRepertoire.Location = New System.Drawing.Point(774, 184)
        Me.btnRepertoire.Name = "btnRepertoire"
        Me.btnRepertoire.Size = New System.Drawing.Size(117, 34)
        Me.btnRepertoire.TabIndex = 1
        Me.btnRepertoire.Text = "Changer ..."
        Me.btnRepertoire.UseVisualStyleBackColor = True
        '
        'tabSortir
        '
        Me.tabSortir.Location = New System.Drawing.Point(4, 25)
        Me.tabSortir.Name = "tabSortir"
        Me.tabSortir.Size = New System.Drawing.Size(1414, 985)
        Me.tabSortir.TabIndex = 5
        Me.tabSortir.Text = "Quitter"
        Me.tabSortir.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmPrincipale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1328, 880)
        Me.Controls.Add(Me.tabAppli)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPrincipale"
        Me.Text = "KINESIS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tabAppli.ResumeLayout(False)
        Me.tabAccueil.ResumeLayout(False)
        Me.tabAcqui.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.grbxAcquisition.ResumeLayout(False)
        Me.grbxAcquisition.PerformLayout()
        CType(Me.numAngle2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numAngle1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numPieds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numBanc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numDuree, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numFreq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grBxPrevisualisation.ResumeLayout(False)
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartForce, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grBxSujets.ResumeLayout(False)
        Me.grpMesurePoids.ResumeLayout(False)
        Me.grpMesurePoids.PerformLayout()
        Me.grBxCalibration.ResumeLayout(False)
        Me.grBxCalibration.PerformLayout()
        CType(Me.numTare, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAnalyse.ResumeLayout(False)
        Me.flowLayoutPanel2.ResumeLayout(False)
        CType(XyDiagramPane1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chrtFVcmj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutPanel.ResumeLayout(False)
        CType(XyDiagram3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chrtPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbxIndices.ResumeLayout(False)
        Me.grbxIndices.PerformLayout()
        Me.tabResultats.ResumeLayout(False)
        Me.tabForceMax.ResumeLayout(False)
        Me.tabForceMax.PerformLayout()
        Me.tabRFD.ResumeLayout(False)
        Me.tabRFD.PerformLayout()
        Me.TFmax.ResumeLayout(False)
        Me.TFmax.PerformLayout()
        Me.grbxOrdre.ResumeLayout(False)
        Me.grbxOrdre.PerformLayout()
        CType(Me.numOrdrePoly, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbxSujet.ResumeLayout(False)
        Me.grbxSujet.PerformLayout()
        Me.groupeChoixAnalyse.ResumeLayout(False)
        Me.tabConfig.ResumeLayout(False)
        Me.tabConfig.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbNbEssais.ResumeLayout(False)
        Me.grbNbEssais.PerformLayout()
        CType(Me.numMinFV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMinRJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMinISO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMinK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMinSJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMinCMJ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBlessures.ResumeLayout(False)
        Me.grpBlessures.PerformLayout()
        Me.grbAcquisition.ResumeLayout(False)
        Me.grbAcquisition.PerformLayout()
        Me.grbxPoly.ResumeLayout(False)
        Me.grbxPoly.PerformLayout()
        CType(Me.numOrdreReg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBxSeuils.ResumeLayout(False)
        Me.grpBxSeuils.PerformLayout()
        CType(Me.numSeuilFz1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numSeuilFz2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabAppli As System.Windows.Forms.TabControl
    Friend WithEvents tabAccueil As System.Windows.Forms.TabPage
    Friend WithEvents tabAcqui As System.Windows.Forms.TabPage
    Friend WithEvents tabAnalyse As System.Windows.Forms.TabPage
    Friend WithEvents tabConfig As System.Windows.Forms.TabPage
    Friend WithEvents tabSortir As System.Windows.Forms.TabPage
    Friend WithEvents grBxPrevisualisation As System.Windows.Forms.GroupBox
    Friend WithEvents grBxSujets As System.Windows.Forms.GroupBox
    Friend WithEvents grBxCalibration As System.Windows.Forms.GroupBox
    Friend WithEvents lblMasseMesuree As System.Windows.Forms.Label
    Friend WithEvents btnPoidsSujet As System.Windows.Forms.Button
    Friend WithEvents lstSujets As System.Windows.Forms.ListView
    Friend WithEvents clnMasse As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnNom As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnPrenom As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnBanc As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnPieds As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnAngle1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents grpMesurePoids As System.Windows.Forms.GroupBox
    Friend WithEvents grbxAcquisition As System.Windows.Forms.GroupBox
    Friend WithEvents btnMesure As System.Windows.Forms.Button
    Friend WithEvents numPieds As System.Windows.Forms.NumericUpDown
    Friend WithEvents numBanc As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkCouche As System.Windows.Forms.CheckBox
    Friend WithEvents rbPiedG As System.Windows.Forms.RadioButton
    Friend WithEvents rbPiedD As System.Windows.Forms.RadioButton
    Friend WithEvents rb2pieds As System.Windows.Forms.RadioButton
    Friend WithEvents lblPieds As System.Windows.Forms.Label
    Friend WithEvents lblBanc As System.Windows.Forms.Label
    Friend WithEvents numDuree As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblDuree As System.Windows.Forms.Label
    Friend WithEvents numFreq As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblFrequence As System.Windows.Forms.Label
    Friend WithEvents lstTests As System.Windows.Forms.ListBox
    Friend WithEvents lblChoixTest As System.Windows.Forms.Label
    Friend WithEvents btnSupprimer As System.Windows.Forms.Button
    Friend WithEvents btnSauvegarder As System.Windows.Forms.Button
    Friend WithEvents numTare As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkBlesse As System.Windows.Forms.CheckBox
    Friend WithEvents numCharge As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCharge As System.Windows.Forms.Label
    Friend WithEvents btnAjoutSujets As System.Windows.Forms.Button
    Friend WithEvents cmbxBlessures As System.Windows.Forms.ComboBox
    Friend WithEvents clnAngle2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chartForce As DevExpress.XtraCharts.ChartControl
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtRepertoire As System.Windows.Forms.TextBox
    Friend WithEvents btnRepertoire As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnAccueilAcquisition As System.Windows.Forms.Button
    Friend WithEvents lblRepertoire As System.Windows.Forms.Label
    Friend WithEvents btnTare As System.Windows.Forms.Button
    Friend WithEvents btnAvide As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents groupeChoixAnalyse As System.Windows.Forms.GroupBox
    Friend WithEvents btnEffacer As System.Windows.Forms.Button
    Friend WithEvents btnAnalyse As System.Windows.Forms.Button
    Friend WithEvents clstFichAnalyse As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnChoix As System.Windows.Forms.Button
    'Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents grbxIndices As System.Windows.Forms.GroupBox
    Friend WithEvents btnDeselectionner As System.Windows.Forms.Button
    Friend WithEvents btnRecapitulatif As System.Windows.Forms.Button
    Friend WithEvents btnEnregistrer As System.Windows.Forms.Button
    Friend WithEvents lblCoherence1 As System.Windows.Forms.Label
    Friend WithEvents lblMoyenne1 As System.Windows.Forms.Label
    Friend WithEvents lblCoherenceGr1 As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneGr1 As System.Windows.Forms.Label
    Friend WithEvents lstIndices As System.Windows.Forms.ListView
    Friend WithEvents grbxSujet As System.Windows.Forms.GroupBox
    Friend WithEvents lblMasse As System.Windows.Forms.Label
    Friend WithEvents lblSujet As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents layoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents chrtPrincipal As DevExpress.XtraCharts.ChartControl
    Friend WithEvents btnAccueilAnalyse As System.Windows.Forms.Button
    Friend WithEvents numSeuilFz2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents numSeuilFz1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblFz2 As System.Windows.Forms.Label
    Friend WithEvents lblFz1 As System.Windows.Forms.Label
    Friend WithEvents grpBxSeuils As System.Windows.Forms.GroupBox
    Friend WithEvents lblCalibration As System.Windows.Forms.Label
    Friend WithEvents grbxPoly As System.Windows.Forms.GroupBox
    Friend WithEvents numOrdreReg As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnEnregistreConfig As System.Windows.Forms.Button
    Friend WithEvents btnChargeConfig As System.Windows.Forms.Button
    Friend WithEvents lblDateModif As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblNomConfig As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents grbAcquisition As System.Windows.Forms.GroupBox
    Friend WithEvents txtCoeffB_Config As System.Windows.Forms.TextBox
    Friend WithEvents txtCoeffA_Config As System.Windows.Forms.TextBox
    Friend WithEvents txtNomCarte As System.Windows.Forms.TextBox
    Friend WithEvents lblNomCarte As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grpBlessures As System.Windows.Forms.GroupBox
    Friend WithEvents lstBlessures As System.Windows.Forms.ListBox
    Friend WithEvents btnBlessure As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtBlessure As System.Windows.Forms.TextBox
    Friend WithEvents btnAfficher As System.Windows.Forms.Button
    Friend WithEvents btnAccueilConfiguration As System.Windows.Forms.Button
    Friend WithEvents cmbxTypeCourbe As System.Windows.Forms.ComboBox
    Friend WithEvents lblTypeCourbe As System.Windows.Forms.Label
    Friend WithEvents grbxOrdre As System.Windows.Forms.GroupBox
    Friend WithEvents lblOrdrePoly As System.Windows.Forms.Label
    Friend WithEvents numOrdrePoly As System.Windows.Forms.NumericUpDown
    Friend WithEvents numAngle2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblAngle2 As System.Windows.Forms.Label
    Friend WithEvents numAngle1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblAngle1 As System.Windows.Forms.Label
    Friend WithEvents chkSaut As System.Windows.Forms.CheckBox
    Friend WithEvents lblNbEssais As System.Windows.Forms.Label
    Friend WithEvents lblEssais As System.Windows.Forms.Label
    Friend WithEvents clnPuiss As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkLstFiles1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents flowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents chrtFVcmj As DevExpress.XtraCharts.ChartControl
    Friend WithEvents btnRJ As DevExpress.XtraEditors.CheckButton
    Friend WithEvents txtNomVoie As System.Windows.Forms.TextBox
    Friend WithEvents lblNomVoie As System.Windows.Forms.Label
    Friend WithEvents grbNbEssais As System.Windows.Forms.GroupBox
    Friend WithEvents numMinFV As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMinFV As System.Windows.Forms.Label
    Friend WithEvents numMinRJ As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMinRJ As System.Windows.Forms.Label
    Friend WithEvents numMinISO As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMinISO As System.Windows.Forms.Label
    Friend WithEvents numMinK As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMinK As System.Windows.Forms.Label
    Friend WithEvents numMinSJ As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMinSJ As System.Windows.Forms.Label
    Friend WithEvents numMinCMJ As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMinCMJ As System.Windows.Forms.Label
    Friend WithEvents lblCoherence3 As System.Windows.Forms.Label
    Friend WithEvents lblCoherence2 As System.Windows.Forms.Label
    Friend WithEvents lblMoyenne3 As System.Windows.Forms.Label
    Friend WithEvents lblMoyenne2 As System.Windows.Forms.Label
    Friend WithEvents lblVariable3 As System.Windows.Forms.Label
    Friend WithEvents lblVariable2 As System.Windows.Forms.Label
    Friend WithEvents lblVariable1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents btnSuppBlessure As System.Windows.Forms.Button
    Friend WithEvents tabResultats As System.Windows.Forms.TabControl
    Friend WithEvents tabForceMax As System.Windows.Forms.TabPage
    Friend WithEvents tabRFD As System.Windows.Forms.TabPage
    Friend WithEvents lblMeanFmax As System.Windows.Forms.Label
    Friend WithEvents lblCoherenceFmax As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCoherenceRG As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneRDiff As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneRG As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneRD As System.Windows.Forms.Label
    Friend WithEvents lblCoherenceRD As System.Windows.Forms.Label
    Friend WithEvents lblCoherenceR2J As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneR2J As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblCoherenceFG As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneFDiff As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneFG As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneFD As System.Windows.Forms.Label
    Friend WithEvents lblCoherenceFD As System.Windows.Forms.Label
    Friend WithEvents lblCoherenceF2J As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneF2J As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TFmax As System.Windows.Forms.TabPage
    Friend WithEvents lblTypeCourbe_Analyse As System.Windows.Forms.Label
    Friend WithEvents cbxTypeCourbe_Analyse As System.Windows.Forms.ComboBox
    Friend WithEvents lblMoyenneTdiff As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneTG As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneTD As System.Windows.Forms.Label
    Friend WithEvents lblMoyenneT2J As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label


End Class
