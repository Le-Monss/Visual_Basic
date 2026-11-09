Imports System

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
    Sub Main(args As String())
        Console.WriteLine("Geef naam van verdediger: ")
        Dim verdediger As New Verdediger(Console.ReadLine())
        Console.WriteLine("Geef naam van aanvaller: ")
        Dim aanvaller As New Aanvaller(Console.ReadLine())
        verdediger.TrapBal(verdediger.GetNaam())
        aanvaller.TrapBal(aanvaller.GetNaam())
    End Sub
End Module
