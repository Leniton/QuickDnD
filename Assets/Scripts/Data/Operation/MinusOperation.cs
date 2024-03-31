
public class MinusOperation : ITaggedOperation
{
    public string key => "-";

    public int GetValue(int a, int b) => a - b;
}
