Imports System
Class Wielrenner
    Protected strNaam As String
    Protected intLeeftijd As Integer
    Protected strPloeg As String

    Public Sub New(strNaam As String, intLeeftijd As Integer, strPloeg As String)
        Me.strNaam = strNaam
        Me.intLeeftijd = intLeeftijd
        Me.strPloeg = strPloeg
    End Sub

    Public Sub ToonInfo()
        Console.WriteLine("Naam: " & strNaam)
        Console.WriteLine("Leeftijd: " & intLeeftijd)
        Console.WriteLine("Ploeg: " & strPloeg)
    End Sub
End Class
Class Profwielrenner
    Inherits Wielrenner
    Private intAantalOverwinningen As Integer
    Private dblSalaris As Double
    Public Sub New(strNaam As String, intLeeftijd As Integer, strPloeg As String, intAantalOverwinningen As Integer, dblSalaris As Double)
        MyBase.New(strNaam, intLeeftijd, strPloeg)
        Me.intAantalOverwinningen = intAantalOverwinningen
        Me.dblSalaris = dblSalaris
    End Sub
    Public Sub ToonExtras()
        Console.WriteLine("Aantal overwinningen: " & intAantalOverwinningen)
        Console.WriteLine("Salaris: " & dblSalaris)
    End Sub
End Class
Class Amateurwielrenner
    Inherits Wielrenner
    Private intAantalDeelnames As Integer
    Private intAantalOverwinningen As Integer
    Public Sub New(strNaam As String, intLeeftijd As Integer, strPloeg As String, intAantalOverwinningen As Integer, intAantalDeelnames As Integer)
        MyBase.New(strNaam, intLeeftijd, strPloeg)
        Me.intAantalOverwinningen = intAantalOverwinningen
        Me.intAantalDeelnames = intAantalDeelnames
    End Sub
    Public Sub ToonExtras()
        Console.WriteLine("Aantal overwinningen: " & intAantalOverwinningen)
        Console.WriteLine("Aantal deelnames: " & intAantalDeelnames)
    End Sub
End Class
Module Program
    Sub Main(args As String())
        Dim profwielrenner As New Profwielrenner("Jan Janssen", 30, "Team A", 10, 50000)
        Dim amateurwielrenner As New Amateurwielrenner("Piet Pietersen", 25, "Team B", 5, 100)
        Dim wielrenner As New Wielrenner("John Doe", 28, "Team C")
        wielrenner.ToonInfo()
        profwielrenner.ToonInfo()
        profwielrenner.ToonExtras()
        amateurwielrenner.ToonInfo()
        amateurwielrenner.ToonExtras()
    End Sub
End Module
