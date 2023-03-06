namespace PharmaMgmtApi.Helpers;

public class FileSizeHelper
{
    public static double ByteToMb(double @byte) => @byte / 1024 / 1024;
}