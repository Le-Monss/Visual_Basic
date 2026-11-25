Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class wedstrijd
    Protected liDeelnemers As List(Of Voertuig) 'lijst van deelnemers gemaakt uit verschillende objecten van de klasse Voertuig (Formule1 en Rally)
    Protected voWinnaar As Voertuig
    Protected rng As New Random() 'Om randomizers toe te kunnen passen'
    Public tpVerstrekenTijd As TimeSpan 'Om een raceduur bij te houden'

    Protected Sub New()
        liDeelnemers = New List(Of Voertuig)()
    End Sub

    Public Overridable Sub VoegDeelnemerToe(v As Voertuig) 'voegt een nieuwe deelnemer toe als voertuig'
        liDeelnemers.Add(v)
    End Sub

    Public Overridable Function Start() As Voertuig 'gemaakt om later iets mee te doen en "start" te timer'
        tpVerstrekenTijd = TimeSpan.Zero
        Return Nothing
    End Function

    Public Overridable Sub ToonUitslag() 'zelfde redenen'
    End Sub
End Class

Public Class formule1Race
    Inherits wedstrijd
    Private intTurns As Integer

    Public Sub New(turns As Integer)
        MyBase.New()
        Me.intTurns = turns
    End Sub
    Public Overrides Function Start() As Voertuig
        Const finishafstandKm As Double = 305 'lengte van de race'

        Do While liDeelnemers.Max(Function(v) v.intCounter) < finishafstandKm 'zoland lengte niet is gehaald'
            For Each v As Voertuig In liDeelnemers 'gaat elk voertuig zijn "beurt" op in het programma'
                If rng.NextDouble() < 0.75 Then 'bepaalt of ze gaan versnellen of remmen'
                    v.Versnel()
                Else
                    v.Remmen()
                End If

                v.Rijd(intTurns) 'legt een afstand af op basis van snelheid'
            Next
            tpVerstrekenTijd += TimeSpan.FromMinutes(1 / intTurns * 1.5) 'voegt tijd toe per beurt dat er zig afspeeld'
        Loop

        voWinnaar = liDeelnemers.
        OrderByDescending(Function(v) v.intCounter). 'sorteert de voertuigen op afgelegde afstand en pakt de eerste (de winnaar)'
        FirstOrDefault()

        Return voWinnaar
    End Function

    Public Overrides Sub ToonUitslag() 'wa denkgy da dit toont?
        Dim ranking = liDeelnemers.OrderByDescending(Function(v) v.intCounter).ToList()
        'zelfde principe, sorteert op afstand en zet ze in een lijst'
        For i As Integer = 0 To ranking.Count - 1 'print het mooi uit in een soort van ranking'
            Console.WriteLine($"{i + 1}. {ranking(i).strmerk}: {ranking(i).intCounter:F2} km")
        Next
    End Sub
End Class

Public Class rallyRace 'Rally race werkt op zelfde principe als formule1'
    Inherits wedstrijd
    Private dblVertragingFactor As Double

    Public Sub New(ondergrond As String, hoogteverschil As Integer)
        MyBase.New()
        'Gewoon hier dat ipv turns je het ondegrond kiest'
        Select Case ondergrond.Trim().ToLowerInvariant()
            Case "asfalt"
                dblVertragingFactor = 1.0 'verandert u snelheidscalculatie'
            Case "gravel"
                dblVertragingFactor = 1.2
            Case "modder"
                dblVertragingFactor = 1.5
        End Select
    End Sub
    Public Overrides Function Start() As Voertuig
        Const dblfinishafstandKm As Double = 30
        Dim dbltijd As TimeSpan = TimeSpan.Zero

        Do While liDeelnemers.Max(Function(v) v.intCounter) < dblfinishafstandKm
            For Each v As Voertuig In liDeelnemers
                If rng.NextDouble() < 0.75 Then
                    v.Versnel()
                Else
                    v.Remmen()
                End If

                v.Rijd(dblVertragingFactor)
            Next
            tpVerstrekenTijd += TimeSpan.FromMinutes(rng.Next(1, 3))
            tpVerstrekenTijd += TimeSpan.FromSeconds(rng.Next(1, 60))
        Loop

        voWinnaar = liDeelnemers.
        OrderByDescending(Function(v) v.intCounter).
        FirstOrDefault()

        Return voWinnaar
    End Function
    Public Overrides Sub ToonUitslag()
        Dim ranking = liDeelnemers.OrderByDescending(Function(v) v.intCounter).ToList()

        For i As Integer = 0 To ranking.Count - 1
            Console.WriteLine($"{i + 1}. {ranking(i).strmerk}: {ranking(i).intCounter:F2} km")
        Next
    End Sub
End Class

Public Class Voertuig
    Friend strmerk As String 'Friend want we willen dat de merknaam toegankelijk is in de afgeleide klassen Formule1 en Rally en in Sub main
    'Maar ook in een het totaal ander klasse van Weddstrijd'
    Friend dblSnelheid As Double
    Friend intmaxSnelheid As Integer
    Friend intacceleratie As Double
    Friend intCounter As Double
    Protected rng As New Random()
    Friend dblHuidigeSnelheid As Double

    Public Sub New(strmerk As String, dblSnelheid As Double, intmaxSnelheid As Integer, intacceleratie As Double)
        Me.strmerk = strmerk
        Me.dblSnelheid = dblSnelheid
        Me.intmaxSnelheid = intmaxSnelheid
        Me.intacceleratie = intacceleratie
        dblHuidigeSnelheid = dblSnelheid
        Me.intCounter = 0 'aantal afgelegd afstand'
    End Sub

    Public Overridable Sub Versnel()
        dblHuidigeSnelheid += intacceleratie * (0.8 + rng.NextDouble() * 0.4)
        dblHuidigeSnelheid = Math.Min(dblHuidigeSnelheid, intmaxSnelheid) 'dat je niet kan versnellen boven je max snelheid'
    End Sub

    Public Overridable Sub Remmen()
        dblHuidigeSnelheid -= intacceleratie * (0.5 + rng.NextDouble() * 0.5)
        dblHuidigeSnelheid = Math.Max(dblHuidigeSnelheid, 0) 'dat je niet kan remmen onder 0 snelheid'
    End Sub
    Public Sub Rijd(Optional intturns As Integer = 1,
            Optional vertragingFactor As Double = 1.0)
        Const dblurenPerTurn As Double = 1.0 / 60.0
        Dim variation As Double = 0.95 + rng.NextDouble() * 0.1
        'variation voor wat randomness in de races'
        intCounter += dblHuidigeSnelheid * dblurenPerTurn _
                  / intturns / vertragingFactor * variation
    End Sub

End Class

Public Class Formule1
    Inherits Voertuig
    Private intDownforce As Integer 'verandert calculatie van snelheid bij versnellen en remmen'

    Public Sub New(strmerk As String, dblSnelheid As Double, intmaxSnelheid As Integer,
                   intacceleratie As Double, intDownforce As Integer)
        MyBase.New(strmerk, dblSnelheid, intmaxSnelheid, intacceleratie)
        Me.intDownforce = intDownforce
    End Sub

    Public Overrides Sub Versnel()
        dblHuidigeSnelheid += intacceleratie * (1 + intDownforce / 200.0) _
                       * (0.8 + rng.NextDouble() * 0.4)
        dblHuidigeSnelheid = Math.Min(dblHuidigeSnelheid, intmaxSnelheid)
    End Sub
    Public Overrides Sub Remmen()
        Dim factor As Double = 1 + intDownforce / 300.0
        dblHuidigeSnelheid -= intacceleratie * factor * (0.5 + rng.NextDouble() * 0.5)
        dblHuidigeSnelheid = Math.Max(dblHuidigeSnelheid, 0)
    End Sub
End Class

Public Class Rally
    Inherits Voertuig
    Private intGrip As Integer 'verandert ook calculaties'

    Public Sub New(strmerk As String, dblSnelheid As Double, intmaxSnelheid As Integer,
                   intacceleratie As Double, intGrip As Integer)
        MyBase.New(strmerk, dblSnelheid, intmaxSnelheid, intacceleratie)
        Me.intGrip = intGrip
    End Sub
    Public Overrides Sub Versnel()
        Dim factor As Double = 1 + intGrip / 250.0
        dblHuidigeSnelheid += intacceleratie * factor * (0.8 + rng.NextDouble() * 0.4)
        dblHuidigeSnelheid = Math.Min(dblHuidigeSnelheid, intmaxSnelheid)
    End Sub

    Public Overrides Sub Remmen()
        Dim gripReduction As Double = 1 - intGrip / 200.0
        Dim factor As Double = Math.Max(0.2, gripReduction)

        dblHuidigeSnelheid -= intacceleratie * factor * (0.5 + rng.NextDouble() * 0.5)
        dblHuidigeSnelheid = Math.Max(dblHuidigeSnelheid, 0)
    End Sub

End Class

Public Module Program
    Public Sub Main()
        Console.WriteLine("Kies een race: formule1 of rally")
        Dim keuze As String
        Do
            keuze = Console.ReadLine().Trim().ToLowerInvariant() 'kiezen van categorie van race'
            If keuze <> "formule1" AndAlso keuze <> "rally" Then
                Console.WriteLine("Ongeldige invoer. Voer 'formule1' of 'rally' in.")
            End If
        Loop Until keuze = "formule1" OrElse keuze = "rally" 'gaat niet verder tot je een geldige keuze maakt'
        If (keuze = "formule1") Then
            Dim race As New formule1Race(10)
            race.VoegDeelnemerToe(New Formule1("Ferrari", 200, 350, 3.5, 50))
            'merk, snelheid, maxSnelheid, acceleratie, downforce'
            race.VoegDeelnemerToe(New Formule1("Mercedes", 210, 340, 3.2, 60))
            race.VoegDeelnemerToe(New Formule1("Red Bull", 220, 330, 3.0, 70))

            Dim winnaar As Voertuig = race.Start()
            Console.WriteLine($"De winnaar is: {winnaar.strmerk}")
            Console.WriteLine($"Race time: {race.tpVerstrekenTijd.ToString("hh\:mm\:ss")}")
            race.ToonUitslag()

        Else
            Console.WriteLine("Kies een ondergrond: asfalt, gravel, modder")
            Dim ondergrond As String
            Do
                ondergrond = Console.ReadLine().Trim().ToLowerInvariant()
                If Not ondergrond = "asfalt" AndAlso Not ondergrond = "gravel" AndAlso Not ondergrond = "modder" Then
                    Console.WriteLine("Ongeldige ondergrond. Probeer opnieuw.")
                End If
            Loop Until ondergrond = "asfalt" OrElse ondergrond = "gravel" OrElse ondergrond = "modder"

            Dim race As New rallyRace(ondergrond, 100)
            race.VoegDeelnemerToe(New Rally("Subaru", 180, 300, 2.5, 80)) 'merk, snelheid, maxSnelheid, acceleratie, grip'
            race.VoegDeelnemerToe(New Rally("Mitsubishi", 190, 310, 2.8, 70))
            race.VoegDeelnemerToe(New Rally("Toyota", 200, 280, 3.0, 60))

            Dim winnaar As Voertuig = race.Start()
            Console.WriteLine($"De winnaar is: {winnaar.strmerk}")
            Console.WriteLine($"Race time: {race.tpVerstrekenTijd.ToString("hh\:mm\:ss")}")
            race.ToonUitslag()
        End If
    End Sub
End Module
