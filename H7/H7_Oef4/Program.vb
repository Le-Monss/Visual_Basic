Imports System
Imports System.Security.Cryptography.X509Certificates

Module Program
    Class Speler
        Protected strNaam As String
        Public Sub New(strNaam As String)
            Me.strNaam = strNaam
        End Sub
        Public Overridable Sub TrapBal(strNaam As String)
            Console.WriteLine(strNaam & " trapt de bal")
        End Sub
        Public Function GetNaam() As String
            Return strNaam
        End Function
    End Class
    Class Verdediger
        Inherits Speler
        Sub New(strNaam As String)
            MyBase.New(strNaam)
        End Sub
        Public Overrides Sub TrapBal(strNaam As String)
            If (Random.Shared.Next(2) = 0) Then
                Console.WriteLine(strNaam & " trapt de bal weg")
            Else
                Console.WriteLine(strNaam & "verliest de bal")
            End If
        End Sub
    End Class
    Class Aanvaller
        Inherits Speler
        Sub New(strNaam As String)
            MyBase.New(strNaam)
        End Sub
        Public Overrides Sub TrapBal(strNaam As String)
            Console.WriteLine(Random.Shared.Next(100))
            If (Random.Shared.Next(100) > 30) Then
                Console.WriteLine(strNaam & " schiet op doel")
            Else
                Console.WriteLine(strNaam & " verliest de bal")
            End If
        End Sub
    End Class

    Class Keeper
        Inherits Speler
        Private dblBetrouwbaarheid As Double
        Sub New(strNaam As String, dblBetrouwbaarheid As Double)
            MyBase.New(strNaam)
            Me.dblBetrouwbaarheid = dblBetrouwbaarheid
        End Sub
        Public Function getBetrouwbaarheid() As Double
            Return dblBetrouwbaarheid
        End Function
        Public Function DoeStrafschop() As Double
            Return dblBetrouwbaarheid + (Random.Shared.NextDouble() * 0.3 - 0.1)
        End Function
    End Class

    Class Trapper
        Inherits Speler
        Private dblBetrouwbaarheid As Double
        Sub New(strNaam As String, dblBetrouwbaarheid As Double)
            MyBase.New(strNaam)
            Me.dblBetrouwbaarheid = dblBetrouwbaarheid
        End Sub
        Public Function getBetrouwbaarheid() As Double
            Return dblBetrouwbaarheid
        End Function
        Public Function DoeStrafschop() As Double
            Return dblBetrouwbaarheid + (Random.Shared.NextDouble() * 0.4 - 0.3)
        End Function
    End Class
    Sub Main(args As String())
        Dim objMbappe As New Trapper("Mbappe", 0.8)
        Dim objMartinez As New Keeper("Martinez", 0.6)
        Dim dblTrapper As Double = objMbappe.DoeStrafschop()
        Dim dblKeeper As Double = objMartinez.DoeStrafschop()
        Console.WriteLine("Trapper: " & objMbappe.GetNaam())
        Console.WriteLine("Keeper: " & objMartinez.GetNaam())
        If dblKeeper > dblTrapper Then
            Console.WriteLine("Keeper redt de strafschop")
        Else
            Console.WriteLine("Trapper scoort een doelpunt")
        End If
    End Sub
End Module