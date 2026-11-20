Imports System
<<<<<<< HEAD
Imports System.Linq



Public Class wedstrijd
    Protected deelnemers As List(Of voertuig)
    Protected finishafstand As Double
    Protected naam As String
    Protected datum As DateTime
    Protected winnaar As voertuig

    Protected Sub New()
        deelnemers = New List(Of voertuig)()
    End Sub

    Public Sub New(finishafstand As Integer)
        Me.New()
        Me.finishafstand = finishafstand
    End Sub

    Public Overridable Sub VoegDeelnemerToe(v As voertuig)
    End Sub

    Public Overridable Function Start() As voertuig
        Return Nothing
    End Function

    Public Overridable Sub ToonUitslag()
    End Sub
End Class

Public Class formule1Race
    Inherits wedstrijd

    Private turns As Integer

    Public Sub New(turns As Integer)
        MyBase.New()
        Me.turns = turns
    End Sub

    Public Overrides Sub VoegDeelnemerToe(v As voertuig)
    End Sub

    Public Overrides Function Start() As voertuig
        Return Nothing
    End Function

    Public Overrides Sub ToonUitslag()
    End Sub
End Class

Public Class rallyRace
    Inherits wedstrijd

    Private ondergrond As String
    Private hoogteverschil As Integer
    Private vertragingFactor As Double

    Public Sub New(ondergrond As String, hoogteverschil As Integer)
        MyBase.New()
        Me.ondergrond = ondergrond
        Me.hoogteverschil = hoogteverschil

        Select Case ondergrond.Trim().ToLowerInvariant()
            Case "asfalt"
                vertragingFactor = 1.0
            Case "gravel"
                vertragingFactor = 1.2
            Case "modder"
                vertragingFactor = 1.5
            Case Else
                Throw New ArgumentException("Onbekende ondergrond.", NameOf(ondergrond))
        End Select
    End Sub

    Public Overrides Sub VoegDeelnemerToe(v As voertuig)
    End Sub

    Public Overrides Function Start() As voertuig
        For Each voertuig In deelnemers
            voertuig.intCounter = CInt(Math.Ceiling(voertuig.intCounter * vertragingFactor))
        Next

        winnaar = deelnemers.OrderBy(Function(v) v.intCounter).FirstOrDefault()
        Return winnaar
    End Function

    Public Overrides Sub ToonUitslag()
        Dim ranking = deelnemers.OrderBy(Function(v) v.intCounter).ToList()

        For i As Integer = 0 To ranking.Count - 1
            Console.WriteLine($"{i + 1}. {ranking(i).intCounter}")
        Next
    End Sub
=======
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
>>>>>>> ea13e40313fff850ce105173f6893b29aa4492d1
End Class

Module Program

End Module
