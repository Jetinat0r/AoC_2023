//Template Day class
public class Day_02
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_02_Input.txt");

        Day_02_2(lines);
    }

    void Day_02_1(string[] input)
    {
        int _sum = 0;

        for(int i = 0; i < input.Length; i++)
        {
            bool _validGame = true;

            string _games = input[i].Split(':')[1];
            string[] _rounds = _games.Split(";");

            
            for(int j = 0; j < _rounds.Length; j++)
            {
                int _redCount = 0;
                int _greenCount = 0;
                int _blueCount = 0;
                string[] _sets = _rounds[j].Split(',');
                
                for(int k = 0; k < _sets.Length; k++)
                {
                    string[] _vals = _sets[k].Split(" ");

                    if (_vals[2] == "red")
                    {
                        _redCount += int.Parse(_vals[1]);
                    }
                    else if (_vals[2] == "green")
                    {
                        _greenCount += int.Parse(_vals[1]);
                    }
                    else if (_vals[2] == "blue")
                    {
                        _blueCount += int.Parse(_vals[1]);
                    }
                    else
                    {
                        Console.WriteLine("FAILURE");
                    }
                }

                if(_redCount > 12 || _greenCount > 13 || _blueCount > 14)
                {
                    _validGame = false;
                    break;
                }
            }



            if(_validGame)
            {
                _sum += (i + 1);
            }
        }

        Console.WriteLine(_sum);
    }

    void Day_02_2(string[] input)
    {
        ulong _power = 0;

        for (int i = 0; i < input.Length; i++)
        {
            string _games = input[i].Split(':')[1];
            string[] _rounds = _games.Split(";");

            ulong _highestRedCount = 0;
            ulong _highestGreenCount = 0;
            ulong _highestBlueCount = 0;

            for (int j = 0; j < _rounds.Length; j++)
            {
                ulong _redCount = 0;
                ulong _greenCount = 0;
                ulong _blueCount = 0;
                
                string[] _sets = _rounds[j].Split(',');

                for (int k = 0; k < _sets.Length; k++)
                {
                    string[] _vals = _sets[k].Split(" ");

                    if (_vals[2] == "red")
                    {
                        _redCount += ulong.Parse(_vals[1]);
                    }
                    else if (_vals[2] == "green")
                    {
                        _greenCount += ulong.Parse(_vals[1]);
                    }
                    else if (_vals[2] == "blue")
                    {
                        _blueCount += ulong.Parse(_vals[1]);
                    }
                    else
                    {
                        Console.WriteLine("FAILURE");
                    }
                }

                if(_redCount > _highestRedCount)
                {
                    _highestRedCount = _redCount;
                }

                if(_greenCount > _highestGreenCount)
                {
                    _highestGreenCount = _greenCount;
                }

                if(_blueCount > _highestBlueCount)
                {
                    _highestBlueCount = _blueCount;
                }

                
            }

            _power += (_highestRedCount * _highestGreenCount * _highestBlueCount);
        }

        Console.WriteLine(_power);
    }

    public bool GetSubstring(string s, string b)
    {
        List<int> l = new();
        for (int i = 0; i < s.Length - (b.Length - 1); i++)
        {
            bool isOk = true;
            for (int j = 0; j < b.Length; j++)
            {
                if (s[i + j] != b[j])
                {
                    isOk = false;
                    break;
                }
            }

            if (isOk)
            {
                return true;
            }
        }

        return false;
    }
}