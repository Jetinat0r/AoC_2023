//Template Day class
public class Day_07
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_07_Input.txt");

        Day_07_1(lines);
        Day_07_2(lines);
    }

    public enum HAND_TYPE
    {
        FIVE = 6,
        FOUR = 5,
        FULL = 4,
        THREE = 3,
        TWOP = 2,
        ONEP = 1,
        NONE = 0
    }

    private static bool PART_1 = false;

    public static Dictionary<char, int> CHAR_VALS_1 = new() {
        { 'A', 15 },
        { 'K', 14 },
        { 'Q', 13 },
        { 'J', 12 },
        { 'T', 11 },
        { '9', 10 },
        { '8',  9 },
        { '7',  8 },
        { '6',  7 },
        { '5',  6 },
        { '4',  5 },
        { '3',  4 },
        { '2',  3 },
    };

    public static Dictionary<char, int> CHAR_VALS_2 = new() {
        { 'A', 15 },
        { 'K', 14 },
        { 'Q', 13 },
        { 'T', 11 },
        { '9', 10 },
        { '8',  9 },
        { '7',  8 },
        { '6',  7 },
        { '5',  6 },
        { '4',  5 },
        { '3',  4 },
        { '2',  3 },
        { 'J', 1 },
    };

    public class Hand : IComparable<Hand>
    {
        public string Cards = "";
        public long Bet = 0;
        public HAND_TYPE HandType = HAND_TYPE.NONE;

        public Hand(string _c, long _b)
        {
            Cards = _c;
            Bet = _b;

            Dictionary<char, int> _dict = new();
            for (int i = 0; i < _c.Length; i++)
            {
                if (!_dict.ContainsKey(_c[i]))
                {
                    _dict.Add(_c[i], 1);
                }
                else
                {
                    _dict[_c[i]]++;
                }
            }

            if (PART_1)
            {
                GetHandType_1(_dict);
            }
            else
            {
                GetHandType_2(_dict);
            }
        }

        private void GetHandType_1(Dictionary<char, int> _dict)
        {
            bool _hasPair = false;
            bool _hasThruple = false;
            foreach (int _val in _dict.Values)
            {
                if (_val == 5)
                {
                    HandType = HAND_TYPE.FIVE;
                    return;
                }
                if (_val == 4)
                {
                    HandType = HAND_TYPE.FOUR;
                    return;
                }
                if (_val == 3)
                {
                    if (_hasPair)
                    {
                        HandType = HAND_TYPE.FULL;
                        return;
                    }
                    else
                    {
                        _hasThruple = true;
                    }
                }
                if (_val == 2)
                {
                    if (_hasThruple)
                    {
                        HandType = HAND_TYPE.FULL;
                        return;
                    }
                    if (_hasPair)
                    {
                        HandType = HAND_TYPE.TWOP;
                        return;
                    }

                    _hasPair = true;
                }
            }

            if (_hasThruple)
            {
                HandType = HAND_TYPE.THREE;
                return;
            }
            if (_hasPair)
            {
                HandType = HAND_TYPE.ONEP;
                return;
            }
            HandType = HAND_TYPE.NONE;
        }

        private void GetHandType_2(Dictionary<char, int> _dict)
        {
            bool _hasPair = false;
            bool _hasThruple = false;
            if(!_dict.TryGetValue('J', out int _numJokers)){
                _numJokers = 0;
            }
            foreach (char _key in _dict.Keys)
            {
                int _val = _dict[_key];
                if(_key == 'J')
                {
                    continue;
                }

                if (_val == 5)
                {
                    HandType = HAND_TYPE.FIVE;
                    return;
                }
                if (_val == 4)
                {
                    HandType = HAND_TYPE.FOUR;
                    if(_numJokers == 1)
                    {
                        HandType = HAND_TYPE.FIVE;
                    }
                    return;
                }
                if (_val == 3)
                {
                    if(_numJokers == 2)
                    {
                        HandType = HAND_TYPE.FIVE;
                        return;
                    }
                    if(_numJokers == 1)
                    {
                        HandType = HAND_TYPE.FOUR;
                        return;
                    }

                    if (_hasPair)
                    {
                        HandType = HAND_TYPE.FULL;
                        return;
                    }
                    else
                    {
                        _hasThruple = true;
                    }
                }
                if (_val == 2)
                {
                    if (_hasThruple)
                    {
                        HandType = HAND_TYPE.FULL;
                        return;
                    }
                    if (_hasPair)
                    {
                        HandType = HAND_TYPE.TWOP;
                        return;
                    }

                    if(_numJokers == 3)
                    {
                        HandType = HAND_TYPE.FIVE;
                        return;
                    }
                    if (_numJokers == 2)
                    {
                        HandType = HAND_TYPE.FOUR;
                        return;
                    }
                    if (_numJokers == 1)
                    {
                        _hasThruple = true;
                        continue;
                    }

                    _hasPair = true;
                }
            }

            if (_hasThruple)
            {
                HandType = HAND_TYPE.THREE;
                return;
            }
            if (_hasPair)
            {
                HandType = HAND_TYPE.ONEP;
                return;
            }

            if(_numJokers == 5)
            {
                HandType = HAND_TYPE.FIVE;
                return;
            }
            if (_numJokers == 4)
            {
                HandType = HAND_TYPE.FIVE;
                return;
            }
            if (_numJokers == 3)
            {
                HandType = HAND_TYPE.FOUR;
                return;
            }
            if (_numJokers == 2)
            {
                HandType = HAND_TYPE.THREE;
                return;
            }
            if (_numJokers == 1)
            {
                HandType = HAND_TYPE.ONEP;
                return;
            }
            HandType = HAND_TYPE.NONE;
        }

        public static int CompareHand(Hand a, Hand b)
        {
            if(a.HandType > b.HandType)
            {
                return 1;
            }
            else if(a.HandType < b.HandType)
            {
                return -1;
            }
            else
            {
                for(int i = 0; i < a.Cards.Length; i++)
                {
                    if (PART_1)
                    {
                        if (CHAR_VALS_1[a.Cards[i]] > CHAR_VALS_1[b.Cards[i]])
                        {
                            return 1;
                        }
                        else if (CHAR_VALS_1[a.Cards[i]] < CHAR_VALS_1[b.Cards[i]])
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (CHAR_VALS_2[a.Cards[i]] > CHAR_VALS_2[b.Cards[i]])
                        {
                            return 1;
                        }
                        else if (CHAR_VALS_2[a.Cards[i]] < CHAR_VALS_2[b.Cards[i]])
                        {
                            return -1;
                        }
                    }
                }
            }

            return 0;
        }

        public int CompareTo(Hand? other)
        {
            return CompareHand(this, other);
        }
    }

    void Day_07_1(string[] input)
    {
        PART_1 = true;

        List<Hand> _hands = new();

        for(int i = 0; i < input.Length; i++)
        {
            string[] _line = input[i].Split();
            _hands.Add(new Hand(_line[0], long.Parse(_line[1])));
        }

        _hands.Sort();

        long _total = 0;
        for(int i = 0; i <  _hands.Count; i++)
        {
            _total += ((i + 1) * _hands[i].Bet);
        }

        Console.WriteLine(_total);
    }

    void Day_07_2(string[] input)
    {
        PART_1 = false;

        List<Hand> _hands = new();

        for (int i = 0; i < input.Length; i++)
        {
            string[] _line = input[i].Split();
            _hands.Add(new Hand(_line[0], long.Parse(_line[1])));
        }

        _hands.Sort();

        long _total = 0;
        for (int i = 0; i < _hands.Count; i++)
        {
            _total += ((i + 1) * _hands[i].Bet);
        }

        Console.WriteLine(_total);
    }
}