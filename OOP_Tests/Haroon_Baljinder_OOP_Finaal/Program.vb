Imports System

Module Program
    Public Class Bestelling
        Protected strKlantNaam As String
        Protected dblTotaalBedrag As Double
        Public Sub New(strKlantNaam As String, dblTotaalBedrag As Double)
            Me.strKlantNaam = strKlantNaam
            Me.dblTotaalBedrag = dblTotaalBedrag
        End Sub
        Public Sub ToonBestelDetails() 'Toont de basisdetails van een bestelling'
            Console.WriteLine("Klant met naam: " & strKlantNaam)
            Console.WriteLine("Totaal bedrag van bestelling: " & dblTotaalBedrag)
        End Sub
        Public Overridable Function BerekenVerzendkosten() 'Overridable functie zodat we in subklassen dezelfde
            'functie kunnen gebruiken en waardeb aanpassen
            Return 5
        End Function
    End Class
    Public Class SnelleLevering
        Inherits Bestelling
        Private dblSpoedToeslag As Double
        Public Sub New(strKlantNaam As String, dblTotaalBedrag As Double)
            MyBase.New(strKlantNaam, dblTotaalBedrag)
            dblSpoedToeslag = 10
        End Sub
        Public Overrides Function BerekenVerzendKosten() 'Override dus de bereken functie en voegt de spoedtoeslag toe
            Return MyBase.BerekenVerzendkosten() + dblSpoedToeslag
        End Function
    End Class
    Public Class InternationaleBestelling
        Inherits Bestelling
        Private strLand As String
        Public Sub New(strKlantNaam As String, dblTotaalBedrag As Double, strLand As String)
            MyBase.New(strKlantNaam, dblTotaalBedrag)
            Me.strLand = strLand
        End Sub
        Public Function BerekenDouaneKosten()
            Return dblTotaalBedrag * 0.1 ' = 10% van totale bedrag'
        End Function
        Public Overrides Function BerekenVerzendkosten() 'override bereken functie en voegt douankosten toe
            Return MyBase.BerekenVerzendkosten() + BerekenDouaneKosten() 'voorberkende douanekosten
        End Function
        Public Sub ToonDouaneKosten() 'Printen van de douane kosten
            Console.WriteLine("douanekosten zijn: " & BerekenDouaneKosten())
            Console.WriteLine("Totale verzendkosten zijn: " & BerekenVerzendkosten())
        End Sub
    End Class
    Public Class Verzending
        Protected strVerzendType As String
        Public Sub New(strVerzendType As String)
            Me.strVerzendType = strVerzendType
        End Sub
        Public Overridable Function BerekenVerzendKosten()
            Return 3
        End Function
    End Class
    Public Class PakketVerzending
        Inherits Verzending
        Private strPakketGrootte As String
        Public Sub New(strVerzendType As String, strPakketGrootte As String)
            MyBase.New(strVerzendType)
            Me.strPakketGrootte = strPakketGrootte
        End Sub
        Public Overrides Function BerekenVerzendKosten()
            If (strPakketGrootte = "groot") Then
                Return MyBase.BerekenVerzendKosten() + 5
            ElseIf (strPakketGrootte = "middelgroot") Then
                Return MyBase.BerekenVerzendKosten() + 2
            Else
                Return MyBase.BerekenVerzendKosten()
            End If
        End Function
        'Voor het makkelijk te kunnen printen van info over pakketjes'
        Public Overridable Sub ToonPakketDetails()
            Console.WriteLine("Verzendkosten voor pakket met grootte: " & strPakketGrootte & " is: " & BerekenVerzendKosten() & " euro")
        End Sub
    End Class
    Public Class LuchtPost
        Inherits Verzending
        Private dblLuchtTarief As Double
        Public Sub New(strVerzendType As String)
            MyBase.New(strVerzendType)
            dblLuchtTarief = 15
        End Sub
        Public Overrides Function BerekenVerzendKosten()
            Return dblLuchtTarief
        End Function
        'Voor het makkelijk te kunnen printen van info over luchtbezorging'
        Public Overridable Sub ToonLuchtBezorging()
            Console.WriteLine("Verzendkosten voor luchtpakket zijn: " & BerekenVerzendKosten() & " euro")
        End Sub
    End Class
    Sub Main(args As String())
        Dim SnelleLevering As New SnelleLevering("jansen", 100)
        SnelleLevering.ToonBestelDetails()
        Console.WriteLine("Totale verzendkosten zijn: " & SnelleLevering.BerekenVerzendKosten())
        Console.WriteLine("---")
        Dim InternationaleBestelling As New InternationaleBestelling("Martinez", 200, "Frankrijk")
        InternationaleBestelling.ToonBestelDetails()
        InternationaleBestelling.ToonDouaneKosten()
        Console.WriteLine("---")
        Dim PakketVerzending As New PakketVerzending("Pakket", "middelgroot")
        PakketVerzending.ToonPakketDetails()
        Console.WriteLine("---")
        Dim LuchtPost As New LuchtPost("Luchtpost")
        LuchtPost.ToonLuchtBezorging()
        Console.WriteLine("---")
    End Sub
End Module
