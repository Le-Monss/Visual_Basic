Imports System
Module Program
    Public Class Dier
        Protected strKleur As String
        Public Sub New(strkleur As String)
            Me.strKleur = strkleur
        End Sub
        Public Function Eet() As String
            Return "Het dier eet."
        End Function

        Public Function slaap() As String
            Return "Het dier slaapt."
        End Function
    End Class
    Public Class Hond
        Inherits Dier
        Private strBaasje As String
        Public Sub New(strkleur As String, strBaasje As String)
            MyBase.New(strkleur)
            Me.strBaasje = strBaasje
        End Sub
        Public Function Blaft() As String
            Return "De hond van " & strBaasje & " blaft."
        End Function
        Public Function GeeftEigenaar() As String
            Return "De hond heeft een eigenaar: " & strBaasje
        End Function
        Public Function ZetEigenaar(strBaasje As String) As String
            Me.strBaasje = strBaasje
            Return "De eigenaar van de hond is nu: " & strBaasje
        End Function
    End Class

    Public Class Leeuw
        Inherits Dier
        Private strHabitat As String
        Public Sub New(strkleur As String, strHabitat As String)
            MyBase.New(strkleur)
            Me.strHabitat = strHabitat
        End Sub
        Public Function Brult() As String
            Return "De leeuw brult."
        End Function
        Public Function GeeftHabitat() As String
            Return "De leeuw leeft in: " & strHabitat
        End Function
        Public Function ZetHabitat(strHabitat As String) As String
            Me.strHabitat = strHabitat
            Return "De habitat van de leeuw is nu: " & strHabitat
        End Function
    End Class

    Sub Main(args As String())
        Console.WriteLine("Geef de kleur van de hond:")
        Dim strKleur As String = Console.ReadLine()
        Console.WriteLine("Geef de naam van de eigenaar:")
        Dim strBaasje As String = Console.ReadLine()
        Dim hond As New Hond(strKleur, strBaasje)
        Console.WriteLine(hond.Eet())
        Console.WriteLine(hond.Blaft())
        Console.WriteLine(hond.GeeftEigenaar())
        Console.WriteLine("------------")
        Dim leeuw As New Leeuw("Goudkleurig", "Afrikaanse savanne")
        Console.WriteLine(leeuw.Eet())
        Console.WriteLine(leeuw.Brult())
        Console.WriteLine(leeuw.GeeftHabitat())
    End Sub
End Module
