Imports System
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
End Class

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
    End Sub
End Module
