using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class KeywordTextHandler : MonoBehaviour
{
    [SerializeField] TMP_Text uiText;
    public TMP_Text Text => uiText;
    StringBuilder sb;
    public string text
    {
        get => uiText.text;
        set
        {
            sb.Clear();
            sb.Append(value);
            List<Table> tables = KeywordDictionary.Tables;
            for (int i = 0; i < tables.Count; i++)
            {
                TestHelper(tables[i]);
            }
            uiText.text = sb.ToString();
        }
    }
    private void Awake()
    {
        if(uiText == null) uiText = GetComponent<TMP_Text>();
        uiText.richText = true;
        sb = new StringBuilder(text);
        text = uiText.text;
    }

    private void TestHelper(Table table)
    {
        var keywords = KeywordHelper.GetKeywords(sb.ToString(), table);
        foreach (var pair in keywords)
        {
            int id = table.elements.IndexOf(pair.Value.keyword);
            //check if inside link
            string value = sb.ToString();
            int codeID = value.IndexOf(pair.Value.code);
            string prefix="", suffix="";
            if (value.IndexOf("<link=",codeID) <= value.IndexOf("</link>", codeID))
            {
                //Debug.Log($"no link recursion on: {pair.Value.keyword}");
                prefix = $"<link=\"{KeywordDictionary.TableIdToLink(table, id)}\">";
                suffix = "</link>";
            }
            sb.Replace(pair.Value.code, $"{prefix}{pair.Value.keyword}{suffix}");
        }
    }

    private bool ValidKey(StringCode code, int startIndex, out int index)
    {
        string value = sb.ToString();
        index = -1;
        if(startIndex < 0 || value.Length - startIndex < 3) return false;
        int id = value.IndexOf(code.EndCode, startIndex);
        if (id < 0) return false;
        string keyString = value.Substring(startIndex + 1, id - startIndex - 1);
        //print($"keystring is {keyString}");
        if (!int.TryParse(keyString, out index)) index = -1;
        return true;
    }
}
