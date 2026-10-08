Imports System

Module Program

    Class Draak
        Public strNaam As String
        Public strOrigin As String
        Public strGender As String
        Public intAge As Integer
        Public strClass As String

        Public Sub New(strNaam As String, strOrigin As String, strGender As String, intAge As Integer, strClass As String)
            Me.strNaam = strNaam
            Me.strOrigin = strOrigin
            Me.strGender = strGender
            Me.intAge = intAge
            Me.strClass = strClass
        End Sub
        Public Overrides Function ToString() As String
            Return $"Naam: {strNaam}" & vbCrLf &
            $"Origin: {strOrigin}" & vbCrLf &
            $"Leeftijd: {intAge}" & vbCrLf &
            $"Gender: {strGender}" & vbCrLf &
            $"Class: {strClass}"
        End Function
    End Class
    Sub Main(args As String())
        Console.WriteLine("Hello Draakgooners!")
        Dim objDraak As New Draak("Draak", "Dragonland", "Male", 100, "Fire")
        Console.WriteLine(objDraak.ToString())
    End Sub
End Module
