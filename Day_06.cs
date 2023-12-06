//Template Day class
public class Day_06
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_06_Input.txt");

        Day_06_2(lines);
    }

    void Day_06_1(string[] input)
    {
        string[] _badStrTimes = input[0].Split();
        string[] _badStrDistances = input[1].Split();
        
        List<string> _strTimes = new();
        List<string> _strDistances = new();
        for(int i = 1; i < _badStrTimes.Length; i++)
        {
            if (_badStrTimes[i] != "")
            {
                _strTimes.Add(_badStrTimes[i]);
            }
        }
        for (int i = 1; i < _badStrDistances.Length; i++)
        {
            if (_badStrDistances[i] != "")
            {
                _strDistances.Add(_badStrDistances[i]);
            }
        }

        List<ulong> _times = new();
        List<ulong> _distances = new();

        for(int i = 0; i < _strTimes.Count; i++)
        {
            _times.Add(ulong.Parse(_strTimes[i]));
            _distances.Add(ulong.Parse(_strDistances[i]));
        }

        ulong _total = 1;

        for(int i = 0; i < _times.Count; i++)
        {
            ulong _ways = 0;
            for(ulong j = 0; j < _times[i]; j++)
            {
                if(j * (_times[i] - j) > _distances[i])
                {
                    _ways++;
                }
                else
                {
                    if(_ways > 0)
                    {
                        break;
                    }
                }
            }

            _total *= _ways;
        }

        Console.WriteLine(_total);
    }

    void Day_06_2(string[] input)
    {
        string[] _badStrTimes = input[3].Split();
        string[] _badStrDistances = input[4].Split();

        List<string> _strTimes = new();
        List<string> _strDistances = new();
        for (int i = 1; i < _badStrTimes.Length; i++)
        {
            if (_badStrTimes[i] != "")
            {
                _strTimes.Add(_badStrTimes[i]);
            }
        }
        for (int i = 1; i < _badStrDistances.Length; i++)
        {
            if (_badStrDistances[i] != "")
            {
                _strDistances.Add(_badStrDistances[i]);
            }
        }

        List<ulong> _times = new();
        List<ulong> _distances = new();

        for (int i = 0; i < _strTimes.Count; i++)
        {
            _times.Add(ulong.Parse(_strTimes[i]));
            _distances.Add(ulong.Parse(_strDistances[i]));
        }

        ulong _total = _times[0];

        for (int i = 0; i < _times.Count; i++)
        {
            ulong _ways = 0;
            for (ulong j = 0; j < _times[i]; j++)
            {
                if (!(j * (_times[i] - j) > _distances[i]))
                {
                    _ways++;
                }
                else
                {
                    break;
                }
            }

            _total -= (_ways*2);
            _total += 1;
        }

        Console.WriteLine(_total);
    }
}