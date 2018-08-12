<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInsertSujet
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInsertSujet))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.mtxtNom = New System.Windows.Forms.MaskedTextBox()
        Me.mtxtPrenom = New System.Windows.Forms.MaskedTextBox()
        Me.numMasseListe = New System.Windows.Forms.NumericUpDown()
        Me.numBancListe = New System.Windows.Forms.NumericUpDown()
        Me.numPiedListe = New System.Windows.Forms.NumericUpDown()
        Me.numAngle1Liste = New System.Windows.Forms.NumericUpDown()
        Me.numAngle2Liste = New System.Windows.Forms.NumericUpDown()
        Me.btnValiderListe = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAnnulerListe = New DevExpress.XtraEditors.SimpleButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.numMasseListe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numBancListe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numPiedListe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numAngle1Liste, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numAngle2Liste, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'mtxtNom
        '
        resources.ApplyResources(Me.mtxtNom, "mtxtNom")
        Me.mtxtNom.Name = "mtxtNom"
        '
        'mtxtPrenom
        '
        resources.ApplyResources(Me.mtxtPrenom, "mtxtPrenom")
        Me.mtxtPrenom.Name = "mtxtPrenom"
        '
        'numMasseListe
        '
        resources.ApplyResources(Me.numMasseListe, "numMasseListe")
        Me.numMasseListe.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.numMasseListe.Name = "numMasseListe"
        '
        'numBancListe
        '
        resources.ApplyResources(Me.numBancListe, "numBancListe")
        Me.numBancListe.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.numBancListe.Name = "numBancListe"
        Me.numBancListe.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'numPiedListe
        '
        resources.ApplyResources(Me.numPiedListe, "numPiedListe")
        Me.numPiedListe.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.numPiedListe.Name = "numPiedListe"
        Me.numPiedListe.Value = New Decimal(New Integer() {12, 0, 0, 0})
        '
        'numAngle1Liste
        '
        resources.ApplyResources(Me.numAngle1Liste, "numAngle1Liste")
        Me.numAngle1Liste.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numAngle1Liste.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numAngle1Liste.Name = "numAngle1Liste"
        Me.numAngle1Liste.Value = New Decimal(New Integer() {90, 0, 0, 0})
        '
        'numAngle2Liste
        '
        resources.ApplyResources(Me.numAngle2Liste, "numAngle2Liste")
        Me.numAngle2Liste.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numAngle2Liste.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numAngle2Liste.Name = "numAngle2Liste"
        Me.numAngle2Liste.Value = New Decimal(New Integer() {90, 0, 0, 0})
        '
        'btnValiderListe
        '
        Me.btnValiderListe.Image = CType(resources.GetObject("btnValiderListe.Image"), System.Drawing.Image)
        Me.btnValiderListe.ImageLocation = DevExpress.XtraEditors.ImageLocation.BottomCenter
        resources.ApplyResources(Me.btnValiderListe, "btnValiderListe")
        Me.btnValiderListe.Name = "btnValiderListe"
        '
        'btnAnnulerListe
        '
        Me.btnAnnulerListe.Image = CType(resources.GetObject("btnAnnulerListe.Image"), System.Drawing.Image)
        Me.btnAnnulerListe.ImageLocation = DevExpress.XtraEditors.ImageLocation.BottomCenter
        resources.ApplyResources(Me.btnAnnulerListe, "btnAnnulerListe")
        Me.btnAnnulerListe.Name = "btnAnnulerListe"
        '
        'frmInsertSujet
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnAnnulerListe)
        Me.Controls.Add(Me.btnValiderListe)
        Me.Controls.Add(Me.numAngle2Liste)
        Me.Controls.Add(Me.numAngle1Liste)
        Me.Controls.Add(Me.numPiedListe)
        Me.Controls.Add(Me.numBancListe)
        Me.Controls.Add(Me.numMasseListe)
        Me.Controls.Add(Me.mtxtPrenom)
        Me.Controls.Add(Me.mtxtNom)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmInsertSujet"
        CType(Me.numMasseListe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numBancListe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numPiedListe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numAngle1Liste, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numAngle2Liste, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents mtxtNom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtxtPrenom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents numMasseListe As System.Windows.Forms.NumericUpDown
    Friend WithEvents numBancListe As System.Windows.Forms.NumericUpDown
    Friend WithEvents numPiedListe As System.Windows.Forms.NumericUpDown
    Friend WithEvents numAngle1Liste As System.Windows.Forms.NumericUpDown
    Friend WithEvents numAngle2Liste As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnValiderListe As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAnnulerListe As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
