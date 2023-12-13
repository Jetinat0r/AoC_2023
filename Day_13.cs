//Template Day class
public class Day_13
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_13_Input.txt");

        Day_13_2(lines);
    }

    public int BlockAnalysis(List<string> _block)
    {
        Stack<string> _pastList = new();
        _pastList.Push(_block[0]);

        for (int i = 1; i < _block.Count; i++)
        {
            Stack<string> _tempPastList = new();
            List<string> l = _pastList.ToList();
            l.Reverse();
            foreach (string s in l)
            {
                _tempPastList.Push(s);
            }

            bool _validReflection = true;
            int t = i;
            while (_tempPastList.Count > 0 && t < _block.Count)
            {
                if (_tempPastList.Peek() == _block[t])
                {
                    _tempPastList.Pop();
                }
                else
                {
                    _validReflection = false;
                    break;
                }

                t++;
            }

            if (_validReflection)
            {
                return i;
            }
            else
            {
                _pastList.Push(_block[i]);
            }
        }

        return -1;
    }

    public List<string> TransposeBlock(List<string> _block)
    {
        List<string> _transposedBlock = new();
        for (int x = 0; x < _block[0].Length; x++)
        {
            _transposedBlock.Add("" + _block[0][x]);
        }

        for (int y = 1; y < _block.Count; y++)
        {
            for (int x = 0; x < _block[y].Length; x++)
            {
                _transposedBlock[x] += _block[y][x];
            }
        }

        return _transposedBlock;
    }

    void Day_13_1(string[] input)
    {
        List<List<string>> _blocks = new();
        List<string> _curBlock = new();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != "")
            {
                _curBlock.Add(input[i]);
            }
            else
            {
                _blocks.Add(_curBlock);
                _curBlock = new();
            }
        }
        _blocks.Add(_curBlock);

        long total = 0;
        for(int i = 0; i < _blocks.Count; i++)
        {
            int _rowIndex = BlockAnalysis(_blocks[i]);

            if(_rowIndex > 0)
            {
                total += 100 * _rowIndex;
                continue;
            }

            List<string> _transposedBlock = TransposeBlock(_blocks[i]);

            int _columnIndex = BlockAnalysis(_transposedBlock);

            if(_columnIndex > 0)
            {
                total += _columnIndex;
            }
        }


        Console.WriteLine(total);
    }

    public bool OneOff(string a, string b)
    {
        bool _hasOneDiff = false;
        for(int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
            {
                if (_hasOneDiff)
                {
                    return false;
                }

                _hasOneDiff = true;
            }
        }

        return _hasOneDiff;
    }

    public int SmudgeAnalysis(List<string> _block)
    {
        Stack<string> _pastList = new();
        _pastList.Push(_block[0]);

        for(int i = 1; i < _block.Count; i++)
        {
            Stack<string> _tempPastList = new();
            List<string> l = _pastList.ToList();
            l.Reverse();
            foreach(string s in l)
            {
                _tempPastList.Push(s);
            }

            bool _validReflection = true;
            bool _hasOneOff = false;
            int t = i;
            while(_tempPastList.Count > 0 && t < _block.Count)
            {
                if(_tempPastList.Peek() == _block[t])
                {
                    _tempPastList.Pop();
                }
                else if(OneOff(_tempPastList.Peek(), _block[t]))
                {
                    _tempPastList.Pop();

                    if (_hasOneOff)
                    {
                        _validReflection = false;
                        break;
                    }

                    _hasOneOff = true;
                }
                else
                {
                    _validReflection = false;
                    break;
                }

                t++;
            }

            if(_validReflection && _hasOneOff)
            {
                return i;
            }
            else
            {
                _pastList.Push(_block[i]);
            }
        }

        return -1;
    }

    void Day_13_2(string[] input)
    {
        List<List<string>> _blocks = new();
        List<string> _curBlock = new();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != "")
            {
                _curBlock.Add(input[i]);
            }
            else
            {
                _blocks.Add(_curBlock);
                _curBlock = new();
            }
        }
        _blocks.Add(_curBlock);

        long total = 0;
        for (int i = 0; i < _blocks.Count; i++)
        {
            int _rowIndex = SmudgeAnalysis(_blocks[i]);

            if (_rowIndex > 0)
            {
                total += 100 * _rowIndex;
                continue;
            }

            List<string> _transposedBlock = TransposeBlock(_blocks[i]);

            int _columnIndex = SmudgeAnalysis(_transposedBlock);
            
            if (_columnIndex > 0)
            {
                total += _columnIndex;
            }
        }


        Console.WriteLine(total);
    }
}