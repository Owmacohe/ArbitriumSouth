using System;
using Random = UnityEngine.Random;

public abstract class RandomChar
{
    public static Char Get()
    {
        return Get(true, true, true, true);
    }

    public static Char Get(bool lower, bool upper, bool numeric, bool special)
    {
        string temp = "";

        if (lower)
        {
            temp += "abcdefghijklmnopqrstuvwxyz";
        }
        
        if (upper)
        {
            temp += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
        
        if (numeric)
        {
            temp += "0123456789";
        }
        
        if (special)
        {
            temp += "+[];:,.?";
        }

        return temp[Random.Range(0, temp.Length)];
    }
}