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

    public string Name;
    [Space]
    public int HP;
    public int AC;
    [Space]
    public int Prof;
    public int Str, Dex, Con, Int, Wis, Cha;

    public void SetUp()
    {
        stats = new();
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
    }
}
