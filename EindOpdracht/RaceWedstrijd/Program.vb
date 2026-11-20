Imports System
Class Voertuig
    Protected strmerk As String
    Protected intSnelheid As Integer
    Protected intmaxSnelheid As Integer
    Protected intacceleratie As Double
    Protected intCounter As Integer

    Public Sub New(merk As String, Snelheid As Integer, maxSnelheid As Integer, acceleratie As Double)
        Me.strmerk = merk
        Me.intSnelheid = Snelheid
        Me.intmaxSnelheid = maxSnelheid
        Me.intacceleratie = acceleratie
        Me.intCounter = 0
    End Sub
    Public Overridable Function Versnel()
        intacceleratie -= Random.Shared.NextDouble(1, 2)
    End Function
    Public Overridable Function Rem()
        intacceleratie += Random.Shared.NextDouble(1, 2)
    End Function
    Public Sub Rijd(intTijd As Integer)
        intCounter += intmaxSnelheid * intTijd - (intmaxSnelheid / acceleratie) * (1 - Math.Exp(-acceleratie * intTijd)))
    End Sub
End Class

Class Formule1
    Inherits Voertuig
    Private intDownforce As Integer
    Public Sub New(merk As String, Snelheid As Integer, maxSnelheid As Integer, acceleratie As Double, downforce As Integer)
        MyBase.New(merk, Snelheid, maxSnelheid, acceleratie)
        Me.intDownforce = downforce
    End Sub
    Public Overrides Function Versnel()
        intacceleratie -= Random.Shared.NextDouble(1, 2) * (1 + intDownforce / 100)
    End Function
    Public Overrides Function Rem()
        intacceleratie += Random.Shared.NextDouble(1, 2) * (1 + intDownforce / 100)
    End Function
End Class

Class Rally
    Inherits Voertuig
    Private intGrip As Integer
    Public Sub New(merk As String, Snelheid As Integer, maxSnelheid As Integer, acceleratie As Double, grip As Integer)
        MyBase.New(merk, Snelheid, maxSnelheid, acceleratie)
        Me.intGrip = grip
    End Sub
    Public Overrides Function Versnel()
        intacceleratie -= Random.Shared.NextDouble(1, 2) * (1 + intGrip / 100)
    End Function
    Public Overrides Function Rem()
        intacceleratie += Random.Shared.NextDouble(1, 2) * (1 + intGrip / 100)
    End Function
End Class

Module Program

End Module
