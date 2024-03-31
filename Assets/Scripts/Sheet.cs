using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    [SerializeField] KeywordTextHandler Str;
    [SerializeField] TMP_Text Atk;
    [SerializeField] TMP_Text ST;

    ITaggedOperation sum = new SumOperation();

    Dictionary<string, IValueReference<int>> stats = new();
    const string prof = "prof",str = "str", atk = "atk",st="st";

    StringCode code = new("[", "]");
    List<IMacro> macros = new();

    private void Awake()
    {
        //setting stats
        stats[prof] = new NumberReference(2);
        stats[str] = new NumberReference(3);
        stats[atk] = new ValueMerger(stats[str], stats[prof], sum);
        stats[st] = new ValueMerger(stats[str], stats[prof], sum);

        //showing
        Str.text = $"Str: {stats[str].Value}";
        Atk.text = $"Atk: {stats[atk].Value}";
        ST.text = $"ST: {stats[st].Value}";

        //macros
        MultiMacro multiMacro = new(new("Sword",""));
        multiMacro.Add(CreateMacro("Attack", "1d20 + [str] + [prof]"));
        multiMacro.Add(CreateMacro("Damage", "1d6 + [str]"));
        macros.Add(multiMacro);

        for (int i = 0; i < macros.Count; i++)
        {
            Debug.Log(macros[i].UseMacro());
        }
    }

    private IMacro CreateMacro(string name, string formula)
    {
        Keyword keyword = new Keyword(name, formula);
        IValueReference<int> valueReference;
        formula = formula.Replace(" ", "");

        var references = StringUtil.GetAllValues(formula, code);
        if(references.Count > 0)
        {
            valueReference = GetValueReference(references[0].Item2);
            for (int i = 1; i < references.Count; i++)
            {
                valueReference = new ValueMerger(valueReference, GetValueReference(references[i].Item2), sum);
            }
        }
        else valueReference = new NumberReference(0);
        
        return new Macro(valueReference, keyword);
    }

    private IValueReference<int> GetValueReference(string key) => stats.ContainsKey(key)? stats[key] : new NumberReference(0);
}
public class RollData
{
    public int type, amount;
    public RollData( int type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }
}