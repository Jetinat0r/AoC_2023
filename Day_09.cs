//Template Day class
public class Day_09
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_09_Input.txt");

        Day_09_2(lines);
    }

    void Day_09_1(string[] input)
    {
        List<List<long>> _lines = new();
        for(int i  = 0; i < input.Length; i++)
        {
            List<long> _newLine = new();
            string[] _lineStrs = input[i].Split();


            for(int j = 0; j < _lineStrs.Length; j++)
            {
                _newLine.Add(long.Parse(_lineStrs[j]));
            }

            _lines.Add(_newLine);
        }

        long _total = 0;
        for (int i = 0; i < _lines.Count; i++)
        {
            List<List<long>> _sequence = new();
            _sequence.Add(_lines[i]);

            FindPattern(_sequence, 0);

            long _sum = 0;
            for(int j = 0; j < _sequence.Count; j++)
            {
                _sum += _sequence[j][^1];
            }

            _total += _sum;
        }

        Console.WriteLine(_total);
    }

    public void FindPattern(List<List<long>> _sequence, int _index)
    {
        List<long> _newSequence = new();

        bool _allZero = true;
        for(int i = 0; i < _sequence[_index].Count - 1; i++)
        {
            long _newDiff = _sequence[_index][i + 1] - _sequence[_index][i];
            _newSequence.Add(_newDiff);

            if(_newDiff != 0)
            {
                _allZero = false;
            }
        }

        _sequence.Add(_newSequence);

        if (_allZero)
        {
            return;
        }
        else
        {
            FindPattern(_sequence, _index + 1);
        }
    }

    void Day_09_2(string[] input)
    {
        List<List<long>> _lines = new();
        for (int i = 0; i < input.Length; i++)
        {
            List<long> _newLine = new();
            string[] _lineStrs = input[i].Split();


            for (int j = 0; j < _lineStrs.Length; j++)
            {
                _newLine.Add(long.Parse(_lineStrs[j]));
            }

            _lines.Add(_newLine);
        }

        long _total = 0;
        for (int i = 0; i < _lines.Count; i++)
        {
            List<List<long>> _sequence = new();
            _sequence.Add(_lines[i]);

            FindPattern(_sequence, 0);

            long _prevExtension = 0;
            for (int j = _sequence.Count - 2; j >= 0; j--)
            {
                _prevExtension = _sequence[j][0] - _prevExtension;
            }

            _total += _prevExtension;
        }

        Console.WriteLine(_total);
    }
}