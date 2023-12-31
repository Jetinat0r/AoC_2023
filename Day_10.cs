﻿//Template Day class
public class Day_10
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_10_Input.txt");

        Day_10_2(lines);
    }

    public enum PIPES
    {
        UD = '|',
        LR = '-',
        UR = 'L',
        UL = 'J',
        DL = '7',
        DR = 'F',
        GRND = '.',
        START = 'S'
    }

    public class Tile
    {
        public int x;
        public int y;
        public Tuple<int, int> firstDir;
        public Tuple<int, int> secondDir;
        public bool isGround = false;
        public Tile? finder = null;

        public PIPES type;

        public bool blocksTop = false;
        public bool blocksBottom = false;
        public bool blocksLeft = false;
        public bool blocksRight = false;

        public Tile(int x, int y, char _pipe)
        {
            this.x = x;
            this.y = y;

            type = (PIPES)_pipe;

            switch ((PIPES)_pipe)
            {
                case (PIPES.UD):
                    firstDir = new(0, -1);
                    secondDir = new(0, 1);

                    blocksTop = true;
                    blocksBottom = true;
                    break;
                case (PIPES.LR):
                    firstDir = new(1, 0);
                    secondDir = new(-1, 0);

                    blocksLeft = true;
                    blocksRight = true;
                    break;
                case (PIPES.UR):
                    firstDir = new(0, -1);
                    secondDir = new(1, 0);

                    blocksTop = true;
                    blocksRight = true;
                    break;
                case (PIPES.UL):
                    firstDir = new(0, -1);
                    secondDir = new(-1, 0);

                    blocksTop = true;
                    blocksLeft = true;
                    break;
                case (PIPES.DL):
                    firstDir = new(0, 1);
                    secondDir = new(-1, 0);

                    blocksBottom = true;
                    blocksLeft = true;
                    break;
                case (PIPES.DR):
                    firstDir = new(0, 1);
                    secondDir = new(1, 0);

                    blocksBottom = true;
                    blocksRight = true;
                    break;
                case (PIPES.GRND):
                    firstDir = new(0, 0);
                    secondDir = new(0, 0);
                    isGround = true;
                    break;
                default:
                    firstDir = new(0, 0);
                    secondDir = new(0, 0);
                    break;
            }
        }

        public Tile? GetFirstDir(int xBounds, int yBounds, Tile[,] _allTiles)
        {
            int _newX = x + firstDir.Item1;
            int _newY = y + firstDir.Item2;
            if (InBounds(xBounds, yBounds, _newX, _newY))
            {
                if (_allTiles[_newX, _newY].isGround)
                {
                    return null;
                }

                if(_allTiles[_newX, _newY].GetFirstDirNoSelf(xBounds, yBounds, _allTiles) == this || _allTiles[_newX, _newY].GetSecondDirNoSelf(xBounds, yBounds, _allTiles) == this)
                {
                    return _allTiles[_newX, _newY];
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public Tile? GetFirstDirNoSelf(int xBounds, int yBounds, Tile[,] _allTiles)
        {
            int _newX = x + firstDir.Item1;
            int _newY = y + firstDir.Item2;
            if(InBounds(xBounds, yBounds, _newX, _newY))
            {
                if (_allTiles[_newX, _newY].isGround)
                {
                    return null;
                }

                return _allTiles[_newX, _newY];
            }

            return null;
        }

        public Tile? GetSecondDir(int xBounds, int yBounds, Tile[,] _allTiles)
        {
            int _newX = x + secondDir.Item1;
            int _newY = y + secondDir.Item2;
            if (InBounds(xBounds, yBounds, _newX, _newY))
            {
                if (_allTiles[_newX, _newY].isGround)
                {
                    return null;
                }

                if (_allTiles[_newX, _newY].GetFirstDirNoSelf(xBounds, yBounds, _allTiles) == this || _allTiles[_newX, _newY].GetSecondDirNoSelf(xBounds, yBounds, _allTiles) == this)
                {
                    return _allTiles[_newX, _newY];
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public Tile? GetSecondDirNoSelf(int xBounds, int yBounds, Tile[,] _allTiles)
        {
            int _newX = x + secondDir.Item1;
            int _newY = y + secondDir.Item2;
            if (InBounds(xBounds, yBounds, _newX, _newY))
            {
                if (_allTiles[_newX, _newY].isGround)
                {
                    return null;
                }

                return _allTiles[_newX, _newY];
            }

            return null;
        }
    }

    public bool FindLoop(int xBounds, int yBounds, Tile[,] _allTiles, List<Tile> _curList, List<Tile> _allSearched, ref List<Tile> _successList)
    {
        while (_curList.Count > 0)
        {
            List<Tile> _nextIter = new List<Tile>();

            for(int i = 0; i < _curList.Count; i++)
            {
                Tile? _firstTile = _curList[i].GetFirstDir(xBounds, yBounds, _allTiles);
                Tile? _secondTile = _curList[i].GetSecondDir(xBounds, yBounds, _allTiles);

                if(_firstTile is not null && _firstTile != _curList[i].finder)
                {
                    if (_successList.Contains(_firstTile))
                    {
                        return true;
                    }

                    if (_allSearched.Contains(_firstTile))
                    {
                        return false;
                    }

                    _firstTile.finder = _curList[i];

                    _successList.Add(_firstTile);
                    _allSearched.Add(_firstTile);
                    _nextIter.Add(_firstTile);
                    
                }

                if (_secondTile is not null && _secondTile != _curList[i].finder)
                {
                    if (_successList.Contains(_secondTile))
                    {
                        return true;
                    }

                    if (_allSearched.Contains(_secondTile))
                    {
                        return false;
                    }

                    _secondTile.finder = _curList[i];

                    _successList.Add(_secondTile);
                    _allSearched.Add(_secondTile);
                    _nextIter.Add(_secondTile);
                }
            }

            _curList = _nextIter;
        }

        return false;
    }

    public static bool InBounds(int xBounds, int yBounds, int _xToCheck, int _yToCheck)
    {
        return _xToCheck >= 0 && _yToCheck >= 0 && _xToCheck < xBounds && _yToCheck < yBounds;
    }

    public void DebugTiles(int xBounds, int yBounds, List<Tile> _tiles, string[] input)
    {
        for(int y = 0; y < yBounds; y++)
        {
            for(int x = 0; x < xBounds; x++)
            {
                if(_tiles.Any(t => t.x == x && t.y == y))
                {
                    Console.Write(input[y][x]);
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
        }
    }

    void Day_10_1(string[] input)
    {
        int xBounds = input[0].Length;
        int yBounds = input.Length;

        Tile[,] _allTiles = new Tile[xBounds, yBounds];
        Tile _sTile = new(0, 0, 'S');

        for(int y = 0; y < yBounds; y++)
        {
            for(int x = 0; x < xBounds; x++)
            {
                _allTiles[x, y] = new Tile(x, y, input[y][x]);

                if (input[y][x] == 'S')
                {
                    _sTile = _allTiles[x, y];
                }
            }
        }

        List<Tile> _curList = new();
        List<Tile> _possibleList = new();
        List<Tile> _allSearched = new();
        List<Tile> _successList = new();
        Tile? _upTile = null;
        Tile? _downTile = null;
        Tile? _leftTile = null;
        Tile? _rightTile = null;

        //Left
        if(InBounds(xBounds, yBounds, _sTile.x - 1, _sTile.y) && (_allTiles[_sTile.x - 1, _sTile.y].firstDir.Item1 == 1 || _allTiles[_sTile.x - 1, _sTile.y].secondDir.Item1 == 1))
        {
            _possibleList.Add(_allTiles[_sTile.x - 1, _sTile.y]);
            _leftTile = _allTiles[_sTile.x - 1, _sTile.y];
        }

        //Right
        if (InBounds(xBounds, yBounds, _sTile.x + 1, _sTile.y) && (_allTiles[_sTile.x + 1, _sTile.y].firstDir.Item1 == -1 || _allTiles[_sTile.x + 1, _sTile.y].secondDir.Item1 == -1))
        {
            _possibleList.Add(_allTiles[_sTile.x + 1, _sTile.y]);
            _rightTile = _allTiles[_sTile.x + 1, _sTile.y];
        }

        //Up
        if (InBounds(xBounds, yBounds, _sTile.x, _sTile.y - 1) && (_allTiles[_sTile.x, _sTile.y - 1].firstDir.Item2 == 1 || _allTiles[_sTile.x, _sTile.y - 1].secondDir.Item2 == 1))
        {
            _possibleList.Add(_allTiles[_sTile.x, _sTile.y - 1]);
            _upTile = _allTiles[_sTile.x, _sTile.y - 1];
        }

        //Down
        if (InBounds(xBounds, yBounds, _sTile.x, _sTile.y + 1) && (_allTiles[_sTile.x, _sTile.y + 1].firstDir.Item2 == -1 || _allTiles[_sTile.x, _sTile.y + 1].secondDir.Item2 == -1))
        {
            _possibleList.Add(_allTiles[_sTile.x, _sTile.y + 1]);
            _downTile = _allTiles[_sTile.x, _sTile.y + 1];
        }

        for(int i = 0; i < _possibleList.Count; i++)
        {
            _successList = new();
            _successList.AddRange(_possibleList);
            _allSearched.Add(_possibleList[i]);
            _curList = new()
            {
                _possibleList[i]
            };

            if(FindLoop(xBounds, yBounds, _allTiles, _curList, _allSearched, ref _successList))
            {
                break;
            }
        }

        //Figure out what kind of tile S was and add it to success list
        bool _upSucceed = false;
        bool _downSucceed = false;
        bool _leftSucceed = false;
        bool _rightSucceed = false;

        if (_upTile != null && _successList.Contains(_upTile))
        {
            _upSucceed = true;
        }

        if (_downTile != null && _successList.Contains(_downTile))
        {
            _downSucceed = true;
        }

        if (_leftTile != null && _successList.Contains(_leftTile))
        {
            _leftSucceed = true;
        }

        if (_rightTile != null && _successList.Contains(_rightTile))
        {
            _rightSucceed = true;
        }

        if (_upSucceed)
        {
            if (_downSucceed)
            {
                //UD
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, '|');
            }
            else if (_leftSucceed)
            {
                //UL
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, 'J');
            }
            else
            {
                //UR
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, 'L');
            }
        }
        else if (_downSucceed)
        {
            if (_leftSucceed)
            {
                //DL
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, '7');
            }
            else
            {
                //DR
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, 'F');
            }
        }
        else
        {
            //LR
            _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, '-');
        }

        _successList.Add(_allTiles[_sTile.x, _sTile.y]);

        //We win!
        DebugTiles(xBounds, yBounds, _successList, input);

        //((All success pipes used - pipes that COULD connect to S + 2 that DO connect and make the loop w/ S) / 2 for min dist)
        Console.WriteLine((_successList.Count - _possibleList.Count + 2) / 2);
    }

    void Day_10_2(string[] input)
    {
        int xBounds = input[0].Length;
        int yBounds = input.Length;

        Tile[,] _allTiles = new Tile[xBounds, yBounds];
        Tile _sTile = new(0, 0, 'S');

        for (int y = 0; y < yBounds; y++)
        {
            for (int x = 0; x < xBounds; x++)
            {
                _allTiles[x, y] = new Tile(x, y, input[y][x]);

                if (input[y][x] == 'S')
                {
                    _sTile = _allTiles[x, y];
                }
            }
        }

        List<Tile> _curList = new();
        List<Tile> _possibleList = new();
        List<Tile> _allSearched = new();
        List<Tile> _successList = new();
        Tile? _upTile = null;
        Tile? _downTile = null;
        Tile? _leftTile = null;
        Tile? _rightTile = null;

        //Left
        if (InBounds(xBounds, yBounds, _sTile.x - 1, _sTile.y) && (_allTiles[_sTile.x - 1, _sTile.y].firstDir.Item1 == 1 || _allTiles[_sTile.x - 1, _sTile.y].secondDir.Item1 == 1))
        {
            _possibleList.Add(_allTiles[_sTile.x - 1, _sTile.y]);
            _leftTile = _allTiles[_sTile.x - 1, _sTile.y];
        }

        //Right
        if (InBounds(xBounds, yBounds, _sTile.x + 1, _sTile.y) && (_allTiles[_sTile.x + 1, _sTile.y].firstDir.Item1 == -1 || _allTiles[_sTile.x + 1, _sTile.y].secondDir.Item1 == -1))
        {
            _possibleList.Add(_allTiles[_sTile.x + 1, _sTile.y]);
            _rightTile = _allTiles[_sTile.x + 1, _sTile.y];
        }

        //Up
        if (InBounds(xBounds, yBounds, _sTile.x, _sTile.y - 1) && (_allTiles[_sTile.x, _sTile.y - 1].firstDir.Item2 == 1 || _allTiles[_sTile.x, _sTile.y - 1].secondDir.Item2 == 1))
        {
            _possibleList.Add(_allTiles[_sTile.x, _sTile.y - 1]);
            _upTile = _allTiles[_sTile.x, _sTile.y - 1];
        }

        //Down
        if (InBounds(xBounds, yBounds, _sTile.x, _sTile.y + 1) && (_allTiles[_sTile.x, _sTile.y + 1].firstDir.Item2 == -1 || _allTiles[_sTile.x, _sTile.y + 1].secondDir.Item2 == -1))
        {
            _possibleList.Add(_allTiles[_sTile.x, _sTile.y + 1]);
            _downTile = _allTiles[_sTile.x, _sTile.y + 1];
        }

        for (int i = 0; i < _possibleList.Count; i++)
        {
            _successList = new();
            _successList.AddRange(_possibleList);
            _allSearched.Add(_possibleList[i]);
            _curList = new()
            {
                _possibleList[i]
            };

            if (FindLoop(xBounds, yBounds, _allTiles, _curList, _allSearched, ref _successList))
            {
                break;
            }
        }

        //Figure out what kind of tile S was and add it to success list
        bool _upSucceed = false;
        bool _downSucceed = false;
        bool _leftSucceed = false;
        bool _rightSucceed = false;

        if (_upTile != null && _successList.Contains(_upTile))
        {
            _upSucceed = true;
        }

        if (_downTile != null && _successList.Contains(_downTile))
        {
            _downSucceed = true;
        }

        if (_leftTile != null && _successList.Contains(_leftTile))
        {
            _leftSucceed = true;
        }

        if (_rightTile != null && _successList.Contains(_rightTile))
        {
            _rightSucceed = true;
        }

        if (_upSucceed)
        {
            if (_downSucceed)
            {
                //UD
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, '|');
            }
            else if (_leftSucceed)
            {
                //UL
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, 'J');
            }
            else
            {
                //UR
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, 'L');
            }
        }
        else if (_downSucceed)
        {
            if (_leftSucceed)
            {
                //DL
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, '7');
            }
            else
            {
                //DR
                _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, 'F');
            }
        }
        else
        {
            //LR
            _allTiles[_sTile.x, _sTile.y] = new(_sTile.x, _sTile.y, '-');
        }

        _successList.Add(_allTiles[_sTile.x, _sTile.y]);

        //Uh oh!
        DebugTiles(xBounds, yBounds, _successList, input);

        List<Tile> _insideTiles = new();

        uint _total = 0;
        for(int y = 0; y < yBounds; y++)
        {
            bool _inStructure = false;
            //PIPES _lastPipe;
            for (int x = 0; x < xBounds; x++)
            {
                bool _onWall = _successList.Contains(_allTiles[x, y]);
                if (_onWall)
                {
                    if (_allTiles[x, y].type == PIPES.LR)
                    {
                        continue;
                    }

                    if (_allTiles[x, y].type == PIPES.UD)
                    {
                        _inStructure = !_inStructure;
                        continue;
                    }

                    if(_allTiles[x, y].type == PIPES.UR || _allTiles[x, y].type == PIPES.DR)
                    {
                        PIPES _lastPipe = _allTiles[x, y].type;
                        do
                        {
                            x++;
                        } while (_allTiles[x, y].type != PIPES.UL && _allTiles[x, y].type != PIPES.DL);

                        if(_lastPipe == PIPES.UR)
                        {
                            if(_allTiles[x, y].type == PIPES.UL)
                            {
                                //State doesn't change
                            }
                            else
                            {
                                _inStructure = !_inStructure;
                            }
                        }
                        else
                        {
                            if (_allTiles[x, y].type == PIPES.DL)
                            {
                                //State doesn't change
                            }
                            else
                            {
                                _inStructure = !_inStructure;
                            }
                        }
                        continue;
                    }
                }
                else
                {
                    if (_inStructure)
                    {
                        _total++;
                        _insideTiles.Add(_allTiles[x, y]);
                    }
                }
            }
        }

        Console.WriteLine();
        DebugTiles(xBounds, yBounds, _insideTiles, input);
        Console.WriteLine(_total);
    }
}