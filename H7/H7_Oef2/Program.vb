Imports System

Module Program
    Class Veelhoek
        Protected intAantalZijden As Integer
        Public Sub New(intAantalZijden As Integer)
            Me.intAantalZijden = intAantalZijden
        End Sub
        Public Function GetAantalZijden() As String
            If (intAantalZijden <> 4) Then
                Return "Deze veelhoek heeft: " & intAantalZijden & " zijden"
            Else
                Return "Een vierkant is een veelhoek met 4 zijden"
            End If
        End Function
    End Class
    Class Vierkant
        Inherits Veelhoek
        Public Sub New(intAantalZijden As Integer)
            MyBase.New(4)
        End Sub
    End Class

    Sub Main(args As String())
        Dim vierkant As New Vierkant(4)
        Console.WriteLine(vierkant.GetAantalZijden())
        Console.WriteLine("------------------------------")
        Console.WriteLine("Geef het aantal zijden:")
        Dim intAantalZijden As Integer = Integer.Parse(Console.ReadLine())
        Dim veelhoek As New Veelhoek(intAantalZijden)
        Console.WriteLine(veelhoek.GetAantalZijden())
    End Sub
End Module
