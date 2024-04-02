
using System.Collections.Generic;

public interface IMacro
{
    public string UseMacro();


    public static StringCode code = new("[", "]");

    public static IMacro CreateMacro(string name, string formula, Dictionary<string, IValueReference<int>> stats)
    {
        Keyword keyword = new Keyword(name, formula);
        IValueReference<int> valueReference;
        formula = formula.Replace(" ", "");

        var references = StringUtil.GetAllValues(formula, code);
        if (references.Count > 0)
        {
            valueReference = GetValueReference(references[0].Item2, stats);
            for (int i = 1; i < references.Count; i++)
            {
                valueReference = new ValueMerger(valueReference, GetValueReference(references[i].Item2, stats), Operations.Get("+"));
            }
        }
        else valueReference = new NumberReference(0);

        return new Macro(valueReference, keyword);
    }
    public static IValueReference<int> GetValueReference(string key, Dictionary<string, IValueReference<int>> template)
        => template.ContainsKey(key) ? template[key] : new NumberReference(0);
}
