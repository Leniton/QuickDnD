using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Keyword 
{
    public string word;
    public Color? color = null;
    [Multiline]public string description;
    private string coloredText => color == null ? $"<color=#{ColorUtility.ToHtmlStringRGBA(color.Value)}>{word}</color>" : word;

    public Keyword(string _word, string _description, Color? _color = null)
    {
        word = _word;
        description = _description;
        color = _color;
    }

    public static implicit operator string(Keyword keyword) => keyword.coloredText;

    public override string ToString() => this;
}
