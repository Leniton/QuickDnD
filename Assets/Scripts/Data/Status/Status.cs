using UnityEngine;

public class Status
{
    public string Name;
    [SerializeField] private int Value;

    private IValueReference<int> valueReference;

    public IValueReference<int> ValueReference
    {
        get
        {
            if (valueReference == null) valueReference = new NumberReference(Value);
            else valueReference.Value = Value;
            return valueReference;
        }
        set { valueReference = value; }
    }
}
