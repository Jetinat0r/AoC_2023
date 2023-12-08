public static class MathHelpers
{
    public static long GCD(long a, long b)
    {
        if (a == 0)
        {
            return b;
        }

        return GCD(b % a, a);
    }

    public static long GCD(List<long> _list)
    {
        long _result = _list[0];
        for(int i = 1; i < _list.Count; i++)
        {
            _result = GCD(_result, _list[i]);
        }

        return _result;
    }

    public static long LCM(long a, long b)
    {
        return (a * b) / GCD(a, b);
    }

    public static long LCM(List<long> _list)
    {
        long _result = _list[0];

        for(int i = 1; i < _list.Count; i++)
        {
            _result = LCM(_result, _list[i]);
        }

        return _result;
    }
}