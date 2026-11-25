Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class wedstrijd
    Protected deelnemers As List(Of Voertuig)
    Protected winnaar As Voertuig

    Protected Sub New()
        deelnemers = New List(Of Voertuig)()
    End Sub

    Public Overridable Sub VoegDeelnemerToe(v As Voertuig)
        deelnemers.Add(v)
    End Sub

    Public Overridable Function Start() As Voertuig
        Return Nothing
    End Function

    Public Overridable Sub ToonUitslag()
    End Sub
End Class

Public Class formule1Race
    Inherits wedstrijd

    Private ReadOnly turns As Integer

    Public Sub New(turns As Integer)
        MyBase.New()
        Me.turns = turns
    End Sub

    Public Overrides Function Start() As Voertuig
        For Each v As Voertuig In deelnemers
            For i As Integer = 1 To turns
                v.Rijd(1)
            Next
        Next

        winnaar = deelnemers.OrderByDescending(Function(v) v.intCounter).FirstOrDefault()
        Return winnaar
    End Function

    Public Overrides Sub ToonUitslag()
        Dim ranking = deelnemers.OrderByDescending(Function(v) v.intCounter).ToList()

        For i As Integer = 0 To ranking.Count - 1
            Console.WriteLine($"{i + 1}. {ranking(i).strmerk}: {ranking(i).intCounter:F2}")
        Next
    End Sub
End Class

Public Class rallyRace
    Inherits wedstrijd

    Private ReadOnly vertragingFactor As Double

    Public Sub New(ondergrond As String, hoogteverschil As Integer)
        MyBase.New()

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

    Public Overrides Function Start() As Voertuig
        For Each v As Voertuig In deelnemers
            v.Rijd(CInt(Math.Ceiling(10 * vertragingFactor)))
        Next

        winnaar = deelnemers.OrderByDescending(Function(v) v.intCounter).FirstOrDefault()
        Return winnaar
    End Function

    Public Overrides Sub ToonUitslag()
        Dim ranking = deelnemers.OrderByDescending(Function(v) v.intCounter).ToList()

        For i As Integer = 0 To ranking.Count - 1
            Console.WriteLine($"{i + 1}. {ranking(i).strmerk}: {ranking(i).intCounter:F2}")
        Next
    End Sub
End Class

Public Class Voertuig
    Friend strmerk As String
    Friend intSnelheid As Integer
    Friend intmaxSnelheid As Integer
    Friend intacceleratie As Double
    Friend intCounter As Double

    Public Sub New(merk As String, snelheid As Integer, maxSnelheid As Integer, acceleratie As Double)
        Me.strmerk = merk
        Me.intSnelheid = snelheid
        Me.intmaxSnelheid = maxSnelheid
        Me.intacceleratie = acceleratie
        Me.intCounter = 0
    End Sub
    Private Shared ReadOnly rng As New Random()

    Protected Function RandomBetweenOneAndTwo() As Double
        Return 1.0 + rng.NextDouble()
    End Function

    Public Overridable Sub Versnel()
        intacceleratie += RandomBetweenOneAndTwo()
    End Sub

    Public Overridable Sub Remmen()
        intacceleratie = Math.Max(0.1, intacceleratie - RandomBetweenOneAndTwo())
    End Sub

    Public Sub Rijd(tijd As Integer)
        If intacceleratie <= 0 Then
            Throw New InvalidOperationException("Acceleratie moet groter zijn dan nul.")
        End If

        intCounter += intmaxSnelheid * tijd _
            - (intmaxSnelheid / intacceleratie) *
            (1 - Math.Exp(-intacceleratie * tijd))
    End Sub
End Class

Public Class Formule1
    Inherits Voertuig
    Private ReadOnly intDownforce As Integer

    Public Sub New(merk As String, snelheid As Integer, maxSnelheid As Integer,
                   acceleratie As Double, downforce As Integer)
        MyBase.New(merk, snelheid, maxSnelheid, acceleratie)
        Me.intDownforce = downforce
    End Sub

    Public Overrides Sub Versnel()
        intacceleratie += RandomBetweenOneAndTwo() * (1 + intDownforce / 100.0)
    End Sub

    Public Overrides Sub Remmen()
        intacceleratie = Math.Max(
            0.1,
            intacceleratie - RandomBetweenOneAndTwo() * (1 + intDownforce / 100.0)
        )
    End Sub
End Class

Public Class Rally
    Inherits Voertuig

    Private ReadOnly intGrip As Integer

    Public Sub New(merk As String, snelheid As Integer, maxSnelheid As Integer,
                   acceleratie As Double, grip As Integer)
        MyBase.New(merk, snelheid, maxSnelheid, acceleratie)
        Me.intGrip = grip
    End Sub

    Public Overrides Sub Versnel()
        intacceleratie += RandomBetweenOneAndTwo() * (1 + intGrip / 100.0)
    End Sub

    Public Overrides Sub Remmen()
        intacceleratie = Math.Max(
            0.1,
            intacceleratie - RandomBetweenOneAndTwo() * (1 + intGrip / 100.0)
        )
    End Sub
End Class

Public Module Program
    Public Sub Main()
        Console.WriteLine("Kies een race: formule1 of rally")
        Dim keuze As String = Console.ReadLine().Trim().ToLowerInvariant()

        If (keuze = "formule1)") Then
            Dim race As New formule1Race(10)
            race.VoegDeelnemerToe(New Formule1("Ferrari", 200, 350, 3.5, 50))
            race.VoegDeelnemerToe(New Formule1("Mercedes", 210, 340, 3.2, 60))
            race.VoegDeelnemerToe(New Formule1("Red Bull", 220, 330, 3.0, 70))

            Dim winnaar As Voertuig = race.Start()
            Console.WriteLine($"De winnaar is: {winnaar.strmerk}")
            race.ToonUitslag()

        ElseIf (keuze = "rally") Then
            Dim race As New rallyRace("gravel", 100)
            race.VoegDeelnemerToe(New Rally("Subaru", 180, 300, 2.5, 80))
            race.VoegDeelnemerToe(New Rally("Mitsubishi", 190, 310, 2.8, 70))
            race.VoegDeelnemerToe(New Rally("Toyota", 200, 320, 3.0, 60))

            Dim winnaar As Voertuig = race.Start()
            Console.WriteLine($"De winnaar is: {winnaar.strmerk}")
            race.ToonUitslag()

        Else
            Console.WriteLine("Ongeldige invoer. Voer 'formule1' of 'rally' in.")
        End If
        Console.WriteLine("Press Enter to exit...")
        Console.ReadLine()

    End Sub
End Module
