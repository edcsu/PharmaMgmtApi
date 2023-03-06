namespace PharmaMgmtApi.Helpers;

public class ImageHelper
{
    public static string MakeImageName(string fileName)
    {
        var guid = Guid.NewGuid().ToString();
        return "IMG_" + guid + fileName;
    }
}