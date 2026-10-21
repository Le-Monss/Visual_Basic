Imports System
Class radio
    Private liedje1 As String
    Private liedje2 As String
    Private liedje3 As String

    Public Sub New(liedje1 As String, liedje2 As String, liedje3 As String)
        Me.liedje1 = liedje1
        Me.liedje2 = liedje2
        Me.liedje3 = liedje3
    End Sub
    Public Sub SpeelLiedje()
        Console.WriteLine("Ik ga een liedje spelen...")
        Console.WriteLine(SpeelLiedjeRadio())

    End Sub


    Private Function SpeelLiedjeRadio() As String
        Dim liedjes As New List(Of String) From {liedje1, liedje2, liedje3}
        Dim rnd As New Random()
        Dim gekozen As Integer = rnd.Next(0, 3)
        Return liedjes(gekozen)
    End Function

End Class

Module Program
    Sub Main(args As String())
        Console.WriteLine("Geef het eerste liedje:")
        Dim strliedje1 As String = Console.ReadLine()

        Console.WriteLine("Geef het tweede liedje:")
        Dim strliedje2 As String = Console.ReadLine()

        Console.WriteLine("Geef het derde liedje:")
        Dim strliedje3 As String = Console.ReadLine()

        Dim radio As New radio(strliedje1, strliedje2, strliedje3)
        radio.SpeelLiedje()

    End Sub
End Module