using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text Prof;
    [SerializeField] TMP_Text Str, Dex, Con, Int, Wis, Cha;
    [SerializeField] TMP_Text Atk, StP, StS;

    [Space, SerializeField] MonsterTemplate template;

    StringCode code = new("[", "]");
    List<IMacro> macros = new();

    private void Awake()
    {
        //setup
        template.SetUp();

        //show
        Name.text = template.name;
        Prof.text = $"Prof. bonus: {template.Stats[MonsterTemplate.sProf].GetValue()}";
        Str.text = $"Str: {template.Stats[MonsterTemplate.sStr].GetValue()}";
        Dex.text = $"Dex: {template.Stats[MonsterTemplate.sDex].GetValue()}";
        Con.text = $"Con: {template.Stats[MonsterTemplate.sCon].GetValue()}";
        Int.text = $"Int: {template.Stats[MonsterTemplate.sInt].GetValue()}";
        Wis.text = $"Wis: {template.Stats[MonsterTemplate.sWis].GetValue()}";
        Cha.text = $"Cha: {template.Stats[MonsterTemplate.sCha].GetValue()}";
        Atk.text = $"Atk: {template.Stats[MonsterTemplate.sAtk].GetValue()}";
        StP.text = $"StP: {template.Stats[MonsterTemplate.sStP].GetValue()}";
        StS.text = $"StS: {template.Stats[MonsterTemplate.sStS].GetValue()}";

        //macros
        /*MultiMacro multiMacro = new(new("Sword",""));
        multiMacro.Add(CreateMacro("Attack", "1d20 + [str] + [prof]"));
        multiMacro.Add(CreateMacro("Damage", "1d6 + [str]"));
        macros.Add(multiMacro);*/

        for (int i = 0; i < template.Macros.Count; i++)
        {
            Debug.Log(template.Macros[i].reference.UseMacro());
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
                valueReference = new ValueMerger(valueReference, GetValueReference(references[i].Item2), Operations.Get("+"));
            }
        }
        else valueReference = new NumberReference(0);
        
        return new Macro(valueReference, keyword);
    }

    private IValueReference<int> GetValueReference(string key) => template.Stats.ContainsKey(key)? template.Stats[key] : new NumberReference(0);
}