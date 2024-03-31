using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiMacro : IMacro
{
    public Keyword keyword { get; private set; }
    private List<IMacro> macros = new();

    public MultiMacro(Keyword keyword)
    {
        this.keyword = keyword;
    }

    public void Add(IMacro macro) => macros.Add(macro);

    public string UseMacro()
    {
        string result = $"{keyword}:";
        if(macros.Count > 0)
        {
            result += macros.Count > 1 ? "\n" : " ";
            for (int i = 0; i < macros.Count; i++)
            {
                result += $"{macros[i].UseMacro()}\n";
            }
        }

        return result;
    }
}
