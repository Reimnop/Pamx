namespace Pamx.Ls;

public static class LsRandomUtil
{
    private static Random Random => Random.Shared;
    
    public static string GenerateId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*_+{}|:<>?,./;'[]▓▒░▐▆▉☰☱☲☳☴☵☶☷►▼◄▬▩▨▧▦▥▤▣▢□■¤ÿòèµ¶™ßÃ®¾ð¥œ⁕(◠‿◠✿)";
        const int length = 16;
        
        Span<char> buffer = stackalloc char[length];
        for (var i = 0; i < length; i++)
            buffer[i] = chars[Random.Next(chars.Length)];
        
        return new string(buffer);
    }

    public static int GenerateThemeId()
        => Random.Next(100000, 1000000);
}