//Template Day class
public class Day_08
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_08_Input.txt");

        Day_08_2(lines);
    }

    public class Node
    {
        public string Name = "";
        public string Left = "";
        public string Right = "";

        public Node(string _name, string _left, string _right)
        {
            Name = _name;
            Left = _left;
            Right = _right;
        }
    }

    void Day_08_1(string[] input)
    {
        string _instructions = input[0];

        List<Node> _nodes = new List<Node>();
        for(int i = 2; i < input.Length; i++)
        {
            _nodes.Add(new Node(input[i].Substring(0, 3), input[i].Substring(7, 3), input[i].Substring(12, 3)));
        }

        long _steps = 0;
        int _instructionCounter = 0;
        Node _curNode = _nodes.Find(x => x.Name == "AAA")!;
        while(_curNode.Name != "ZZZ")
        {
            _steps++;
            if (_instructions[_instructionCounter] == 'L')
            {
                _curNode = _nodes.Find(x => x.Name == _curNode.Left)!;
            }
            else
            {
                _curNode = _nodes.Find(x => x.Name == _curNode.Right)!;
            }


            _instructionCounter++;
            _instructionCounter %= _instructions.Length;
        }

        Console.WriteLine(_steps);
    }

    void Day_08_2(string[] input)
    {
        string _instructions = input[0];

        List<Node> _nodes = new List<Node>();
        for (int i = 2; i < input.Length; i++)
        {
            _nodes.Add(new Node(input[i].Substring(0, 3), input[i].Substring(7, 3), input[i].Substring(12, 3)));
        }

        long _steps = 0;
        int _instructionCounter = 0;
        List<Node> _curNodes = _nodes.FindAll(x => x.Name[2] == 'A')!;
        int _endNum = _curNodes.Count;

        List<Node> _path = new List<Node>();

        List<(long, int)> _firstZ = new();
        
        for(int n = 0; n < _curNodes.Count; n++)
        {
            _steps = 0;
            _instructionCounter = 0;
            while (true)
            {
                _steps++;
                if (_instructions[_instructionCounter] == 'L')
                {
                    _curNodes[n] = _nodes.Find(x => x.Name == _curNodes[n].Left)!;
                }
                else
                {
                    _curNodes[n] = _nodes.Find(x => x.Name == _curNodes[n].Right)!;
                }

                _instructionCounter++;
                _instructionCounter %= _instructions.Length;

                if (_curNodes[n].Name[2] == 'Z')
                {
                    _firstZ.Add((_steps, _instructionCounter));
                    break;
                }
            }
        }

        List<long> _pathLengths = new();
        for(int i = 0; i < _firstZ.Count; i++)
        {
            //Console.WriteLine(_curNodes[i].Name + " : " + _firstZ[i].Item1 + " : " + _firstZ[i].Item2);
            _pathLengths.Add(_firstZ[i].Item1);
        }

        Console.WriteLine(MathHelpers.LCM(_pathLengths));
        return;

        //Proof that my assumption is true
        List<(long, int)> _secondZ = new();

        for (int n = 0; n < _curNodes.Count; n++)
        {
            _steps = _firstZ[n].Item1;
            _instructionCounter = _firstZ[n].Item2;
            while (true)
            {
                _steps++;
                if (_instructions[_instructionCounter] == 'L')
                {
                    _curNodes[n] = _nodes.Find(x => x.Name == _curNodes[n].Left)!;
                }
                else
                {
                    _curNodes[n] = _nodes.Find(x => x.Name == _curNodes[n].Right)!;
                }

                _instructionCounter++;
                _instructionCounter %= _instructions.Length;

                if (_curNodes[n].Name[2] == 'Z')
                {
                    _secondZ.Add((_steps, _instructionCounter));
                    break;
                }
            }
        }

        Console.WriteLine();
        for (int i = 0; i < _secondZ.Count; i++)
        {
            Console.WriteLine(_curNodes[i].Name + " : " + _secondZ[i].Item1 + " : " + _secondZ[i].Item2);
        }

        List<(long, int)> _thirdZ = new();

        for (int n = 0; n < _curNodes.Count; n++)
        {
            _steps = _secondZ[n].Item1;
            _instructionCounter = _secondZ[n].Item2;
            while (true)
            {
                _steps++;
                if (_instructions[_instructionCounter] == 'L')
                {
                    _curNodes[n] = _nodes.Find(x => x.Name == _curNodes[n].Left)!;
                }
                else
                {
                    _curNodes[n] = _nodes.Find(x => x.Name == _curNodes[n].Right)!;
                }

                _instructionCounter++;
                _instructionCounter %= _instructions.Length;

                if (_curNodes[n].Name[2] == 'Z')
                {
                    _thirdZ.Add((_steps, _instructionCounter));
                    break;
                }
            }
        }

        Console.WriteLine();

        long _total = 1;
        for (int i = 0; i < _thirdZ.Count; i++)
        {
            Console.WriteLine(_curNodes[i].Name + " : " + _thirdZ[i].Item1 + " : " + _thirdZ[i].Item2);
            _total *= _thirdZ[i].Item1;
        }

        Console.WriteLine(_total);
        return;

        while (_curNodes.FindAll(x => x.Name[2] == 'Z')!.Count < _endNum)
        {
            _steps++;
            for(int i = 0; i < _curNodes.Count; i++)
            {
                if (_instructions[_instructionCounter] == 'L')
                {
                    _curNodes[i] = _nodes.Find(x => x.Name == _curNodes[i].Left)!;
                }
                else
                {
                    _curNodes[i] = _nodes.Find(x => x.Name == _curNodes[i].Right)!;
                }
            }


            _instructionCounter++;
            _instructionCounter %= _instructions.Length;
        }

        Console.WriteLine(_steps);
    }
}