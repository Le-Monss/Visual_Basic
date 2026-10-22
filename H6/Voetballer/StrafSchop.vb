Imports System
Class voetballer
    Private naam As String
    Private leeftijd As Integer
    Private positie As String
    Private trefzekerheid As Integer
    Private schop_moeillijkheid As Integer
    Public Sub New(naam As String, leeftijd As Integer, positie As String)
        Me.naam = naam
        Me.leeftijd = leeftijd
        Me.positie = positie
        Me.trefzekerheid = Int(100 * Rnd()) + 1
        Me.schop_moeillijkheid = Int(100 * Rnd()) + 1
    End Sub
    Public Sub PrintInfo()
        Console.WriteLine("Naam: " & naam)
        Console.WriteLine("Leeftijd: " & leeftijd)
        Console.WriteLine("Positie: " & positie)
        Console.WriteLine("Trefzekerheid: " & trefzekerheid & "%")
        Console.WriteLine("Schop_Moeilijkheid:" & schop_moeillijkheid & "%")
    End Sub
    Public Sub GeefStrafSchop()
        If trefzekerheid > schop_moeillijkheid Then
            Console.WriteLine("Binnen!")
        Else
            Console.WriteLine("Mislukt")
        End If
    End Sub
End Class
Module Program
    Sub Main(args As String())
        Randomize()
        Dim Voetballer1 As New voetballer("Lionel Messi", 36, "Aanvaller")
        Voetballer1.PrintInfo()
        Voetballer1.GeefStrafSchop()
    End Sub
End Module
