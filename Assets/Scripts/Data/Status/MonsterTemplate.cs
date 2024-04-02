using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMonsterTemplate", menuName = "Monster Template")]
public class MonsterTemplate : ScriptableObject, ITemplate
{
    #region consts
    //base
    public const string sHP = "hp";
    public const string sAC = "ac";
    public const string sProf = "prof";
    public const string sStr = "str";
    public const string sDex = "dex";
    public const string sCon = "con";
    public const string sInt = "int";
    public const string sWis = "wis";
    public const string sCha = "cha";

    //subs
    public const string sAtk = "atk";
    public const string sStP = "ST_P";
    public const string sStS = "ST_S";
    #endregion

    private Dictionary<string, IValueReference<int>> stats;
    public Dictionary<string, IValueReference<int>> Stats 
    {
        get
        {
            if (stats == null) SetUp();
            return stats;
        }
        set => Debug.LogWarning("Status is get only"); 
    }

    public List<ReferenceKeyword<IMacro>> Macros { get; set; }

    public string Name;
    [Space]
    public int HP;
    public int AC;
    [Space]
    public int Prof;
    public int Str, Dex, Con, Int, Wis, Cha;

    [SerializeField] private List<Keyword> macros = new();

    public void SetUp()
    {
        stats = new();
        Macros = new();
        //base
        stats[sHP] = new NumberReference(HP);
        stats[sAC] = new NumberReference(AC);
        stats[sProf] = new NumberReference(Prof);
        stats[sStr] = new NumberReference(Str);
        stats[sDex] = new NumberReference(Dex);
        stats[sCon] = new NumberReference(Con);
        stats[sInt] = new NumberReference(Int);
        stats[sWis] = new NumberReference(Wis);
        stats[sCha] = new NumberReference(Cha);

        List<IValueReference<int>> values = new()
        {
            stats[sStr],
            stats[sDex],
            stats[sCon],
            stats[sInt],
            stats[sWis],
            stats[sCha]
        };

        IValueReference<int> highest = ReferenceValueUtil.Highest(values);
        values.Remove(highest);
        IValueReference<int> secondHighest = ReferenceValueUtil.Highest(values);

        stats[sAtk] = new ValueMerger(highest, stats[sProf], Operations.Get("+"));
        stats[sStP] = new ValueMerger(new NumberReference(8), new ValueMerger(highest, stats[sProf], Operations.Get("+")), Operations.Get("+"));
        stats[sStS] = new ValueMerger(new NumberReference(8), new ValueMerger(secondHighest, stats[sProf], Operations.Get("+")), Operations.Get("+"));

        //base macros
        string statString = sStr;
        AddMacro("Str roll", $"1d20 + [{statString}]", stats[statString]);
        statString = sDex;
        AddMacro("Dex roll", $"1d20 + [{statString}]", stats[statString]);
        statString = sCon;
        AddMacro("Con roll", $"1d20 + [{statString}]", stats[statString]);
        statString = sInt;
        AddMacro("Int roll", $"1d20 + [{statString}]", stats[statString]);
        statString = sWis;
        AddMacro("Wis roll", $"1d20 + [{statString}]", stats[statString]);
        statString = sCha;
        AddMacro("Cha roll", $"1d20 + [{statString}]", stats[statString]);

        //general macros
        for (int i = 0; i < macros.Count; i++)
        {
            ReferenceKeyword<IMacro> data = new(macros[i].word, macros[i].description, null, macros[i].color);
            IMacro macro;
            string name = macros[i];
            string[] formulas = macros[i].description.Split('\n');
            if (formulas.Length == 1)
            {
                macro = IMacro.CreateMacro(name, macros[i].description, stats);
            }
            else
            {
                MultiMacro multiMacro = new MultiMacro(macros[i]);
                for (int u = 0; u < formulas.Length; u++)
                {
                    name = "";
                    if(!(StringUtil.IsNumber(formulas[u][0]) || 
                        (formulas[u].ToLower()[0] == 'd' && StringUtil.IsNumber(formulas[u][1]))))
                    {
                        int id = formulas[u].IndexOf(" ") < 0 ? formulas[u].IndexOf(" ") : formulas[u].IndexOf(":");
                        if (id == -1) id = formulas[u].Length;
                        name = formulas[u].Substring(0, id);
                    }

                    multiMacro.Add(IMacro.CreateMacro(name, formulas[u].Remove(0,name.Length), stats));
                }
                macro = multiMacro;
            }
            data.reference = macro;
            Macros.Add(data);
        }
    }

    private void AddMacro(string name, string formula, IValueReference<int> bonus)
    {
        ReferenceKeyword<IMacro> macro = new(name, formula, null);
        Macro roll = new Macro(bonus, macro);
        macro.reference = roll;
        Macros.Add(macro);
    }
}
