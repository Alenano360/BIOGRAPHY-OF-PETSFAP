﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta<.
'     Versión de runtime:4.0.30319.42000
'
'     Los cambios realizados en este archivo pueden causar un comportamiento incorrecto y se perderán si
'     el código vuelve a generarse.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My.Resources
    
    'Esta clase fue generada automáticamente por la clase StronglyTypedResourceBuilder
    'a través de una herramienta como ResGen o Visual Studio.
    'Para agregar o quitar un miembro, modifique el archivo .ResX y vuelva a ejecutar ResGen
    'con la opción /str o vuelva a generar el proyecto de VS.
    '''<summary>
    '''  Clase de recursos con establecimiento inflexible de tipos, para buscar cadenas traducidas, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Friend Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        '''<summary>
        '''  Devuelve la instancia de ResourceManager almacenada en caché usada por esta clase.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("ReportsApplication.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        '''<summary>
        '''  Invalida la propiedad CurrentUICulture del subproceso actual para todas las
        '''  búsquedas de recursos con esta clase de recurso fuertemente tipado.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set(ByVal value As Global.System.Globalization.CultureInfo)
                resourceCulture = value
            End Set
        End Property
    End Module
End Namespace
