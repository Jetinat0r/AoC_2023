//Template Day class
using System.Diagnostics.CodeAnalysis;

public class Day_03
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_03_Input.txt");

        Day_03_2(lines);
    }

    void Day_03_1(string[] input)
    {
        List<List<Nums>> list = FindNums(input);
        long sum = 0;

        for(int i = 0; i < list.Count; i++)
        {
            for(int j = 0; j < list[i].Count; j++)
            {
                if(IsPart(input, i, list[i][j]))
                {
                    sum += ParseNum(input, i, list[i][j]);
                }
            }
        }

        Console.WriteLine(sum);
    }

    void Day_03_2(string[] input)
    {
        List<List<Nums>> list = FindNums(input);

        long sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '*')
                {
                    sum += IsGear(input, i, j, list);
                }
            }
        }

        Console.WriteLine(sum);
    }

    public struct Nums
    {
        public int index;
        public int length;
    }

    public List<List<Nums>> FindNums(string[] input)
    {
        List<List<Nums>> _all = new List<List<Nums>>();

        for(int i = 0; i < input.Length; i++)
        {
            List<Nums> _rowList = new List<Nums>();
            for(int j = 0; j < input[i].Length; j++)
            {
                if (IsNum(input[i][j]))
                {
                    Nums _newNum = new Nums();
                    _newNum.index = j;

                    int _len = 1;
                    j++;
                    while (j < input[i].Length && IsNum(input[i][j]))
                    {
                        _len++;
                        j++;
                    }

                    _newNum.length = _len;

                    _rowList.Add(_newNum);
                }
            }

            _all.Add(_rowList);
        }

        return _all;
    }

    public bool IsNum(char c)
    {
        return (c >= '0' && c <= '9');
    }

    public bool IsSymbol(char c)
    {
        return (!IsNum(c) && c != '.');
    }

    public bool IsPart(string[] _input, int _row, Nums _val)
    {
        for(int i = 0; i < _val.length; i++)
        {
            if ((_val.index + i) > 0)
            {
                if (_row > 0)
                {
                    //Top left
                    if (IsSymbol(_input[_row - 1][(_val.index + i) - 1]))
                    {
                        return true;
                    }
                }

                //Left
                if (IsSymbol(_input[_row][(_val.index + i) - 1]))
                {
                    return true;
                }

                if (_row < (_input.Length - 1))
                {
                    //Bottom Left
                    if (IsSymbol(_input[_row + 1][(_val.index + i) - 1]))
                    {
                        return true;
                    }
                }
            }

            if (_row > 0)
            {
                //Top
                if (IsSymbol(_input[_row - 1][(_val.index + i)]))
                {
                    return true;
                }
            }

            if (_row < (_input.Length - 1))
            {
                //Bottom
                if (IsSymbol(_input[_row + 1][(_val.index + i)]))
                {
                    return true;
                }
            }

            if ((_val.index + i) < (_input[_row].Length - 1))
            {
                if (_row > 0)
                {
                    //Top left
                    if (IsSymbol(_input[_row - 1][(_val.index + i) + 1]))
                    {
                        return true;
                    }
                }

                //Left
                if (IsSymbol(_input[_row][(_val.index + i) + 1]))
                {
                    return true;
                }

                if (_row < (_input.Length - 1))
                {
                    //Bottom Left
                    if (IsSymbol(_input[_row + 1][(_val.index + i) + 1]))
                    {
                        return true;
                    }
                }
            }
        }
        

        return false;
    }

    public int ParseNum(string[] _input, int _row, Nums _val)
    {
        string num = "";

        for(int i = 0; i < _val.length; i++)
        {
            num += _input[_row][_val.index + i];
        }

        return int.Parse(num);
    }

    public int IsGear(string[] _input, int _row, int _col, List<List<Nums>> _parts)
    {
        List<Nums> _aboveNearby = new List<Nums>();
        List<Nums> _middleNearby = new List<Nums>();
        List<Nums> _belowNearby = new List<Nums>();


        if (_row > 0)
        {
            if (_col > 0)
            {
                //Top Left
                for (int i = 0; i < _parts[_row - 1].Count; i++)
                {
                    if ((_col - 1) >= _parts[_row - 1][i].index && (_col - 1) < _parts[_row - 1][i].index + _parts[_row - 1][i].length)
                    {
                        AddNoReplacement(_aboveNearby, _parts[_row - 1][i]);
                    }
                }
            }

            //Top
            for (int i = 0; i < _parts[_row - 1].Count; i++)
            {
                if ((_col) >= _parts[_row - 1][i].index && (_col) < _parts[_row - 1][i].index + _parts[_row - 1][i].length)
                {
                    AddNoReplacement(_aboveNearby, _parts[_row - 1][i]);
                }
            }

            if (_col < (_input[_row].Length - 1))
            {
                //Top Right
                for (int i = 0; i < _parts[_row - 1].Count; i++)
                {
                    if ((_col + 1) >= _parts[_row - 1][i].index && (_col + 1) < _parts[_row - 1][i].index + _parts[_row - 1][i].length)
                    {
                        AddNoReplacement(_aboveNearby, _parts[_row - 1][i]);
                    }
                }
            }
        }

        //Left
        if(_col > 0)
        {
            for (int i = 0; i < _parts[_row].Count; i++)
            {
                if ((_col - 1) >= _parts[_row][i].index && (_col - 1) < _parts[_row][i].index + _parts[_row][i].length)
                {
                    AddNoReplacement(_middleNearby, _parts[_row][i]);
                }
            }
        }

        //Right
        if (_col < (_input[_row].Length - 1))
        {
            for (int i = 0; i < _parts[_row].Count; i++)
            {
                if ((_col + 1) >= _parts[_row][i].index && (_col + 1) < _parts[_row][i].index + _parts[_row][i].length)
                {
                    AddNoReplacement(_middleNearby, _parts[_row][i]);
                }
            }
        }

        if (_row < _input.Length - 1)
        {
            if (_col > 0)
            {
                //Bottom Left
                for (int i = 0; i < _parts[_row + 1].Count; i++)
                {
                    if ((_col - 1) >= _parts[_row + 1][i].index && (_col - 1) < _parts[_row + 1][i].index + _parts[_row + 1][i].length)
                    {
                        AddNoReplacement(_belowNearby, _parts[_row + 1][i]);
                    }
                }
            }

            //Below
            for (int i = 0; i < _parts[_row + 1].Count; i++)
            {
                if ((_col) >= _parts[_row + 1][i].index && (_col) < _parts[_row + 1][i].index + _parts[_row + 1][i].length)
                {
                    AddNoReplacement(_belowNearby, _parts[_row + 1][i]);
                }
            }

            if (_col < (_input[_row].Length - 1))
            {
                //Bottom Right
                for (int i = 0; i < _parts[_row + 1].Count; i++)
                {
                    if ((_col + 1) >= _parts[_row + 1][i].index && (_col + 1) < _parts[_row + 1][i].index + _parts[_row + 1][i].length)
                    {
                        AddNoReplacement(_belowNearby, _parts[_row + 1][i]);
                    }
                }
            }
        }

        if(_aboveNearby.Count + _middleNearby.Count + _belowNearby.Count == 2)
        {
            int sum = 1;

            foreach(Nums nums in _aboveNearby)
            {
                sum *= ParseNum(_input, _row - 1, nums);
            }
            foreach (Nums nums in _middleNearby)
            {
                sum *= ParseNum(_input, _row, nums);
            }
            foreach (Nums nums in _belowNearby)
            {
                sum *= ParseNum(_input, _row + 1, nums);
            }

            return sum;
        }

        return 0;
    }

    public void AddNoReplacement(List<Nums> _l, Nums n)
    {
        for(int i = 0; i < _l.Count; i++)
        {
            if (_l[i].index == n.index)
            {
                return;
            }
        }

        _l.Add(n);
    }
}