using System.Text;

namespace Pamx.Common.Implementation;

public static class RandomUtil
{
    private static Random Random => Random.Shared;
    
    public static string GenerateId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*_+{}|:<>?,./;'[]▓▒░▐▆▉☰☱☲☳☴☵☶☷►▼◄▬▩▨▧▦▥▤▣▢□■¤ÿòèµ¶™ßÃ®¾ð¥œ⁕(◠‿◠✿)";
        const int length = 16;
        
        var builder = new StringBuilder(length);
        for (var i = 0; i < length; i++)
            builder.Append(chars[Random.Next(chars.Length)]);
        
        return builder.ToString();
    }

    public static int GenerateLsThemeId()
        => Random.Next(100000, 1000000);
}