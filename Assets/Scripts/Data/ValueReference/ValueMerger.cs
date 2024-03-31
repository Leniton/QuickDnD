
public class ValueMerger : IValueReference<int>
{
    public int Value { get => GetValue(); set => a.Value = value; }
    IValueReference<int> a, b;
    IOperation<int> Operation;

    public ValueMerger(IValueReference<int> _a, IValueReference<int> _b,IOperation<int> operation)
    {
        a = _a;
        b = _b;
        Operation = operation;
    }

    public int GetValue() => Operation.GetValue(a.GetValue(), b.GetValue());
}
