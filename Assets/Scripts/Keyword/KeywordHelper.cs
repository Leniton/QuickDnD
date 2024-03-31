using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeywordHelper
{
    public static Dictionary<int, KeywordInString> GetKeywordsFromDictionary(string text)
    {
        var keywords = new Dictionary<int, KeywordInString>();

        List<Table> tables = KeywordDictionary.Tables;
        for(int i=0; i<tables.Count; i++)
        {
            var subKeywords = GetKeywords(text, tables[i]);
            foreach (var result in subKeywords)
            {
                keywords[result.Key] = result.Value;
            }
        }

        return keywords;
    }
    public static Dictionary<int, KeywordInString> GetKeywords(string text, Table table)
    {
        var keywords = new Dictionary<int, KeywordInString>();
        int currentIndex = 0;

        while (currentIndex < text.Length)
        {
            int codeIndex = text.IndexOf(table.code.StartCode, currentIndex);
            if (codeIndex < 0) return keywords;
            int id;
            if (!ValidKey(text, table.code, codeIndex, out id)) continue;
            //print($"id is {id}");
            if (id > 0 || id < table.elements.Count)
            {
                Keyword keyword = table.elements[id];
                if (keyword != null)
                {
                    string code = $"{table.code.StartCode}{id}{table.code.EndCode}";
                    keywords[codeIndex] = new(code, keyword);
                }
            }

            currentIndex++;
        }

        return keywords;
    }


    private static bool ValidKey(string value, StringCode code, int startIndex, out int index)
    {
        index = -1;
        if (startIndex < 0 || value.Length - startIndex < 3) return false;
        int id = value.IndexOf(code.EndCode, startIndex);
        if (id < 0) return false;
        string keyString = value.Substring(startIndex + 1, id - startIndex - 1);
        //print($"keystring is {keyString}");
        if (!int.TryParse(keyString, out index)) index = -1;
        return true;
    }
}

public struct KeywordInString
{
    public string code;
    public Keyword keyword;
    public KeywordInString(string _code, Keyword _keyword)
    {
        code = _code;
        keyword = _keyword;
    }
}