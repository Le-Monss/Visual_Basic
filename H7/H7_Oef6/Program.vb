Imports System
Public Class Salamander
    Protected intAantalPoten As Integer
    Protected intStaartLengteCm As Integer
    Protected dblLongCapaciteitInLiter As Double
    Public Sub New()
        intAantalPoten = 4
        intStaartLengteCm = 6
        dblLongCapaciteitInLiter = 0.5
    End Sub
    Public Function GetAantalPoten() As Integer
        Return intAantalPoten
    End Function
    Public Function GetCapaciteit() As Double
        Return dblLongCapaciteitInLiter
    End Function
End Class
Public Class Tijger
    Inherits Salamander
    Protected strHaarkleur As String
    Public Sub New()
        MyBase.New()
        intStaartLengteCm = 50
        dblLongCapaciteitInLiter = 2
        strHaarkleur = "Zwart"
    End Sub
    Public Function GetHaarkleur() As String
        Return strHaarkleur
    End Function
End Class
Public Class Mens
    Inherits Tijger
    Protected intAantalBenen As Integer
    Protected intAntalArmen As Integer
    Public Sub New(strHaarkleur As String)
        MyBase.New()
        intAantalPoten = 0
        Me.strHaarkleur = strHaarkleur
        intAantalBenen = 2
        intAntalArmen = 2
    End Sub
    Public Function GeefAantalArmenEnBenen() As Integer
        Return intAantalBenen
    End Function
End Class
Module Program
    Sub Main(args As String())
        Dim objSalamander As New Salamander()
        Dim objTijger As New Tijger()
        Dim objMens As New Mens("Rood")

        Console.WriteLine("De Salamander heeft zoveel poten: " & objSalamander.GetAantalPoten())
        Console.WriteLine("De Tijger heeft zoveel poten: " & objTijger.GetAantalPoten())
        Console.WriteLine("De Mens heeft zoveel poten:  " & objMens.GetAantalPoten())

        Console.WriteLine("De tijger heeft deze kleur van haar: " & objTijger.GetHaarkleur())
        Console.WriteLine("De mens heeft deze kleur van haar: " & objMens.GetHaarkleur())

        Console.WriteLine("De salamander heeft een longcapaciteit (in liter): " & objSalamander.GetCapaciteit())
        Console.WriteLine("De tijger heeft een longcapaciteit (in liter): " & objTijger.GetCapaciteit())
        Console.WriteLine("De mens heeft een longcapaciteit (in liter): " & objMens.GetCapaciteit())
    End Sub
End Module
