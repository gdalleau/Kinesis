﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Ce code a été généré par un outil.
'     Version du runtime :4.0.30319.34014
'
'     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
'     le code est régénéré.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    'à l'aide d'un outil, tel que ResGen ou Visual Studio.
    'Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    'avec l'option /str ou régénérez votre projet VS.
    '''<summary>
    '''  Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Kinesis.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Remplace la propriété CurrentUICulture du thread actuel pour toutes
        '''  les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Recherche une chaîne localisée semblable à &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        '''&lt;LEFM&gt;
        '''  &lt;Configuration&gt;
        '''    &lt;Calibration coeffA=&quot;252266,991&quot; coeffB=&quot;541,397&quot;&gt;21/06/2015 18:38:04&lt;/Calibration&gt;
        '''    &lt;Blessures&gt;
        '''      &lt;type&gt;Tendinite&lt;/type&gt;
        '''      &lt;type&gt;LCA&lt;/type&gt;
        '''      &lt;type&gt;Fracture&lt;/type&gt;
        '''      &lt;type&gt;Dechirure&lt;/type&gt;
        '''      &lt;type&gt;Entorse&lt;/type&gt;
        '''      &lt;type&gt;dommage&lt;/type&gt;
        '''      &lt;type&gt;totoche&lt;/type&gt;
        '''      &lt;type&gt;titigrosminet&lt;/type&gt;
        '''      &lt;type&gt;enguete&lt;/type&gt;
        '''    &lt;/Blessures&gt;
        '''    &lt;Repertoire&gt;C:\Users\Dalleau\Desktop\LEFM&lt;/Repertoire&gt;
        '''   [le reste de la chaîne a été tronqué]&quot;;.
        '''</summary>
        Friend ReadOnly Property Config() As String
            Get
                Return ResourceManager.GetString("Config", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Recherche une ressource localisée de type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property logo2() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("logo2", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Recherche une ressource localisée de type System.Drawing.Icon semblable à (Icône).
        '''</summary>
        Friend ReadOnly Property logo21() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("logo21", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        '''<summary>
        '''  Recherche une ressource localisée de type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property splash() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("splash", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace
