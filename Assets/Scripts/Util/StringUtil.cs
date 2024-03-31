using System;
using System.Collections.Generic;

public static class StringUtil
{
    public static Tuple<int,string> GetValue(string value, StringCode code, int startIndex = 0)
    {
        Tuple<int, string> returnValue = new(-1, "");
        int startID = value.IndexOf(code.StartCode, startIndex);
        if (startID < 0 || value.Length - startID < 3) return returnValue;
        int id = value.IndexOf(code.EndCode, startID);
        if (id < 0) return returnValue;
        return new(startID, value.Substring(startID + 1, id - startID - 1));
    }

    public static List<Tuple<int, string>> GetAllValues(string value, StringCode code, int startIndex = 0)
    {
        List<Tuple<int, string>> returnValue = new();
        Tuple<int, string> pair = null;
        do
        {
            pair = GetValue(value, code, startIndex);
            if (pair.Item1 >= 0)
            {
                returnValue.Add(pair);
                startIndex = pair.Item1 + 1;
            }

        } while (pair.Item1 >= 0);

        return returnValue;
    }

    public static bool IsNumber(char c)
    {
        //number chars are between 48 and 57
        int code = c;
        return code > 47 && code < 58;
    }
}
