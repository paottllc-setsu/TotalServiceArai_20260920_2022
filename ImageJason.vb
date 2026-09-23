Imports Irony.Parsing
Imports Newtonsoft.Json
Public Class ImageJason

    Public Property Image As ProductImage

    Public Sub WriteJson()

        Dim productInfo As New ImageJason
        Dim image As New ProductImage
        Dim thumbnail As New ProductImageThumbnail

        productInfo.Image = image
        image.OuterWidth = 800
        image.OuterHeight = 600
        image.Title = "15階からの眺望"
        image.Thumbnail = thumbnail
        thumbnail.ImageUrl = "http://www.example.com/image/481989943"
        thumbnail.Height = 125
        thumbnail.Width = 100
        image.Animated = False
        image.IDs = {116, 943, 234, 38793}

        Dim jsonText As String = JsonConvert.SerializeObject(productInfo, Formatting.Indented)

        jsonText = jsonText & JsonConvert.SerializeObject(productInfo, Formatting.Indented)

        Debug.WriteLine(jsonText)

    End Sub

End Class



Public Class ProductImage
    Public Property OuterWidth As Integer
    Public Property OuterHeight As Integer
    Public Property Title As String
    Public Property Thumbnail As ProductImageThumbnail
    Public Property Animated As Boolean
    Public Property IDs As Integer()
End Class

Public Class ProductImageThumbnail
    Public Property ImageUrl As String
    Public Property Height As Integer
    Public Property Width As Integer
End Class

