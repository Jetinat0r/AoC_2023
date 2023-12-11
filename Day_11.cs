//Template Day class
public class Day_11
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_11_Input.txt");

        Day_11_2(lines);
    }

    public class Galaxy
    {
        public long x;
        public long y;

        public Galaxy(long _x, long _y)
        {
            this.x = _x;
            this.y = _y;
        }

        public long ManhattenDist(Galaxy _other)
        {
            return Math.Abs(_other.x - x) + Math.Abs(_other.y - y);
        }
    }

    public void DebugGalaxies(int _xBounds, int _yBounds, List<Galaxy> _galaxies)
    {
        Galaxy?[,] _printList = new Galaxy?[_xBounds, _yBounds];

        foreach(Galaxy _g in _galaxies)
        {
            _printList[_g.x, _g.y] = _g;
        }

        for(int y = 0; y < _yBounds; y++)
        {
            for(int x = 0; x < _xBounds; x++)
            {
                Console.Write(_printList[x,y] == null ? '.' : '#');
            }
            Console.WriteLine();
        }
    }

    void Day_11_1(string[] input)
    {
        List<Galaxy> _allGalaxies = new List<Galaxy>();

        for(int y = 0; y < input.Length; y++)
        {
            for(int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '#')
                {
                    _allGalaxies.Add(new Galaxy(x, y));
                }
            }
        }


        int _expandedX = 0;
        for(int x = 0; x < input[0].Length + _expandedX; x++)
        {
            if (!_allGalaxies.Any(g => g.x > x))
            {
                break;
            }
            if (!_allGalaxies.Any(g => g.x == x))
            {
                //Expand column
                foreach(Galaxy g in _allGalaxies)
                {
                    if(g.x > x)
                    {
                        g.x += 1;
                    }
                }

                x++;
                _expandedX++;
            }
        }

        int _expandedY = 0;
        for (int y = 0; y < input.Length + _expandedY; y++)
        {
            if (!_allGalaxies.Any(g => g.y > y))
            {
                break;
            }
            if (!_allGalaxies.Any(g => g.y == y))
            {
                //Expand row
                foreach (Galaxy g in _allGalaxies)
                {
                    if (g.y > y)
                    {
                        g.y += 1;
                    }
                }

                y++;
                _expandedY++;
            }
        }

        int _numPairs = 0;
        long _total = 0;
        for(int i = 0; i < _allGalaxies.Count; i++)
        {
            for(int j = i + 1; j < _allGalaxies.Count; j++)
            {
                _numPairs++;

                _total += _allGalaxies[i].ManhattenDist(_allGalaxies[j]);
            }
        }

        //DebugGalaxies(input[0].Length + _expandedX, input.Length + _expandedY, _allGalaxies);
        //Console.WriteLine(input.Length + _expandedY);
        //Console.WriteLine(input[0].Length + _expandedX);
        //Console.WriteLine(_numPairs);
        Console.WriteLine(_total);
    }

    void Day_11_2(string[] input)
    {
        List<Galaxy> _allGalaxies = new List<Galaxy>();
        long _expansionWidth = 1_000_000 - 1;

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '#')
                {
                    _allGalaxies.Add(new Galaxy(x, y));
                }
            }
        }

        long _preExpansionTotal = 0;
        for (int i = 0; i < _allGalaxies.Count; i++)
        {
            for (int j = i + 1; j < _allGalaxies.Count; j++)
            {
                _preExpansionTotal += _allGalaxies[i].ManhattenDist(_allGalaxies[j]);
            }
        }

        Console.WriteLine(_preExpansionTotal);

        long _expandedX = 0;
        for (long x = 0; x < input[0].Length + _expandedX; x++)
        {
            if (!_allGalaxies.Any(g => g.x > x))
            {
                break;
            }
            if (!_allGalaxies.Any(g => g.x == x))
            {
                //Expand column
                foreach (Galaxy g in _allGalaxies)
                {
                    if (g.x > x)
                    {
                        g.x += _expansionWidth;
                    }
                }

                x += _expansionWidth;
                _expandedX += _expansionWidth;
            }
        }

        long _expandedY = 0;
        for (long y = 0; y < input.Length + _expandedY; y++)
        {
            if (!_allGalaxies.Any(g => g.y > y))
            {
                break;
            }
            if (!_allGalaxies.Any(g => g.y == y))
            {
                //Expand row
                foreach (Galaxy g in _allGalaxies)
                {
                    if (g.y > y)
                    {
                        g.y += _expansionWidth;
                    }
                }

                y += _expansionWidth;
                _expandedY += _expansionWidth;
            }
        }

        long _postExpansionTotal = 0;
        for (int i = 0; i < _allGalaxies.Count; i++)
        {
            for (int j = i + 1; j < _allGalaxies.Count; j++)
            {
                _postExpansionTotal += _allGalaxies[i].ManhattenDist(_allGalaxies[j]);
            }
        }

        Console.WriteLine(_postExpansionTotal);
    }
}