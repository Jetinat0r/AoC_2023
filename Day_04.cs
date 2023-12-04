//Template Day class
public class Day_04
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_04_Input.txt");

        Day_04_2(lines);
    }

    void Day_04_1(string[] input)
    {
        long sum = 0;
        for(int i = 0; i < input.Length; i++)
        {
            List<int> _winning = new();
            List<int> _has = new();

            bool _reachedHas = false;
            string _strNum = "";
            bool _readingNum = false;
            for(int j = 9; j < input[i].Length; j++)
            {
                if (input[i][j] == ' ')
                {
                    if (_readingNum)
                    {
                        int _num = int.Parse(_strNum);

                        if (!_reachedHas)
                        {
                            _winning.Add(_num);
                        }
                        else
                        {
                            _has.Add(_num);
                        }

                        _strNum = "";
                        _readingNum = false;
                    }
                }
                else if (input[i][j] == '|')
                {
                    _reachedHas = true;
                }
                else if (IsNum(input[i][j]))
                {
                    _readingNum = true;
                    _strNum += input[i][j];
                }
            }
            _has.Add(int.Parse(_strNum));

            bool _hasOne = false;
            long _localSum = 0;
            for(int j = 0; j < _winning.Count; j++)
            {
                for(int k = 0; k < _has.Count; k++)
                {
                    if (_winning[j] == _has[k])
                    {
                        if (!_hasOne)
                        {
                            _localSum = 1;
                            _hasOne = true;
                        }
                        else
                        {
                            _localSum *= 2;
                        }

                        break;
                    }
                }
            }

            sum += _localSum;
        }

        Console.WriteLine(sum);
    }

    void Day_04_2(string[] input)
    {
        List<int> _cardMatches = new List<int>();
        List<long> _cardSums = new List<long>();

        long _ogSum = 0;
        int _startSeach = 0;
        for(int i = 0; i < input[0].Length; i++)
        {
            if (input[0][i] == ':')
            {
                _startSeach = i;
                break;
            }
        }
        for (int i = 0; i < input.Length; i++)
        {
            List<int> _winning = new();
            List<int> _has = new();

            bool _reachedHas = false;
            string _strNum = "";
            bool _readingNum = false;
            for (int j = _startSeach; j < input[i].Length; j++)
            {
                if (input[i][j] == ' ')
                {
                    if (_readingNum)
                    {
                        int _num = int.Parse(_strNum);

                        if (!_reachedHas)
                        {
                            _winning.Add(_num);
                        }
                        else
                        {
                            _has.Add(_num);
                        }

                        _strNum = "";
                        _readingNum = false;
                    }
                }
                else if (input[i][j] == '|')
                {
                    _reachedHas = true;
                }
                else if (IsNum(input[i][j]))
                {
                    _readingNum = true;
                    _strNum += input[i][j];
                }
            }
            _has.Add(int.Parse(_strNum));

            bool _hasOne = false;
            long _localSum = 0;
            int _localMatches = 0;
            for (int j = 0; j < _winning.Count; j++)
            {
                for (int k = 0; k < _has.Count; k++)
                {
                    if (_winning[j] == _has[k])
                    {
                        _localMatches++;

                        if (!_hasOne)
                        {
                            _localSum = 1;
                            _hasOne = true;
                        }
                        else
                        {
                            _localSum *= 2;
                        }

                        break;
                    }
                }
            }

            _ogSum += _localSum;
            _cardSums.Add(_localSum);
            _cardMatches.Add(_localMatches);
        }


        long sum = 0;
        List<int> _copies = new List<int>();
        for(int i = 0; i < input.Length; i++)
        {
            _copies.Add(1);
            //sum += Depths(input.Length, _cardMatches, _cardSums, i);
        }

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = i + 1; j <= i + _cardMatches[i] && j < input.Length; j++)
            {
                _copies[j] += _copies[i];
            }
            //sum += Depths(input.Length, _cardMatches, _cardSums, i);
        }

        for (int i = 0; i < input.Length; i++)
        {
            sum += _copies[i];
            //sum += Depths(input.Length, _cardMatches, _cardSums, i);
        }

        Console.WriteLine(sum);
    }

    public bool IsNum(char c)
    {
        return (c >= '0' && c <= '9');
    }

    public long Depths(int _numCards, List<int> _cardMatches, List<long> _cardSums, int _startCard)
    {
        long _localSum = 0;

        for(int i = _startCard; i < _numCards; i++)
        {
            for (int j = i + 1; j <= i + _cardMatches[_startCard] && j < _numCards; j++)
            {
                _localSum += Depths(_numCards, _cardMatches, _cardSums, j);
            }
        }

        return _localSum + _cardSums[_startCard];
    }
}