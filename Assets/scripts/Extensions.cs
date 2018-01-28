using System;
using System.Collections.Generic;

public static class Extensions {

    public static Random rand = new Random();

    private static List<char> garbledChars = new List<Char>(new char[] { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','1','2','3','4','5','6','7','8','9','0','`','~','!','@','#','$','%','^','&','*','(',')','[','{',']','}','\\','|',',','"','\'','?'});

    public static T GetRandom<T>(this List<T> list)
    {
        return list[(int)Math.Floor(rand.NextDouble() * list.Count)];
    }

    public static T GetRandom<T>(this T[] array)
    {
        return array[(int)Math.Floor(rand.NextDouble() * array.Length)];
    }

    public static string GarbleString(this string st, float strength, int times)
    {
        if (times <= 0)
        {
            return st;
        }

        var newString = "";

        foreach(var c in st)
        {
            if (rand.NextDouble() < strength)
            {
                newString += garbledChars.GetRandom();
            }
            else
            {
                newString += c;
            }
        }

        return newString.GarbleString(strength * .75f, times - 1);
    }
}
