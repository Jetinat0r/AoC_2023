//Template Day class
using System.Collections.Concurrent;

public class Day_12
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_12_Input.txt");

        Day_12_2(lines);
    }

    public List<string> MakePartialPermutations(string line, string midString)
    {
        List<string> result = new();
        if (line[midString.Length] == '?')
        {
            result.Add(midString + '#');
            result.Add(midString + '.');
        }

        return result;
    }

    public List<string> MakePermutations(string line)
    {
        List<string> result = new()
        {
            ""
        };

        for(int i = 0; i < line.Length; i++)
        {
            if (line[i] == '?')
            {
                List<string> temp = new();
                for(int j = 0; j < result.Count; j++)
                {
                    temp.AddRange(MakePartialPermutations(line, result[j]));
                }
                result = temp;
            }
            else
            {
                for (int j = 0; j < result.Count; j++)
                {
                    result[j] += line[i];
                }
            }
        }

        return result;
    }

    /*
    public List<string> MakePermutations(string line, int totalGears)
    {
        int _initialGears = 0;
        int _numQuestions = 0;
        for(int i = 0; i < line.Length; i++)
        {
            if (line[i] == '#')
            {
                _initialGears++;
            }
            else if (line[i] == '?')
            {
                _numQuestions++;
            }
        }

        List<string> _validPermutes = new();
        for(int i = 0; i < line.Length - (totalGears - _initialGears); i++)
        {
            List<string> _newPermutes = new();
            int _questionsLeft = _numQuestions;

            if (line[i] == '?')
            {
                _numQuestions--;

                string a = "";
                string b = "";
                for (int n = 0; n < i; n++)
                {
                    if (line[n] != '?')
                    {
                        a += line[n];
                        b += line[n];
                    }
                    else
                    {
                        a += '.';
                        b += '.';
                    }

                    a += '#';
                    b += '.';
                }
                _newPermutes.Add()

                for(int j = i + 1; j < line.Length; j++)
                {

                }
            }
        }
    }
    */

    public long CheckPermutations(List<string> permutations, List<int> groups)
    {
        long total = 0;
        for(int i = 0; i < permutations.Count; i++)
        {
            int _curGroupIndex = 0;
            int _curGroupCount = 0;
            for(int j = 0; j < permutations[i].Length; j++)
            {
                if (permutations[i][j] == '#')
                {
                    if(_curGroupIndex == groups.Count)
                    {
                        //Increment for simpler checking
                        _curGroupIndex++;
                        break;
                    }


                    _curGroupCount++;

                    if(_curGroupCount > groups[_curGroupIndex])
                    {
                        _curGroupIndex = groups.Count + 1;
                        break;
                    }
                }
                else if(_curGroupCount > 0)
                {
                    if(_curGroupCount < groups[_curGroupIndex])
                    {
                        _curGroupIndex = groups.Count + 1;
                        break;
                    }

                    _curGroupIndex++;
                    _curGroupCount = 0;
                }
            }

            if(_curGroupIndex == groups.Count || ((_curGroupIndex == groups.Count - 1) && (_curGroupCount == groups[^1])))
            {
                total++;
            }
        }

        return total;
    }

    void Day_12_1(string[] input)
    {
        long total = 0;
        for(int i = 0; i < input.Length; i++)
        {
            string[] _splitInput = input[i].Split();
            string[] _splitGroups = _splitInput[1].Split(',');
            List<int> _gearGroups = new();
            for(int j = 0; j < _splitGroups.Length; j++)
            {
                _gearGroups.Add(int.Parse(_splitGroups[j]));
            }

            total += CheckPermutations(MakePermutations(_splitInput[0]), _gearGroups);
        }

        Console.WriteLine(total);
    }

    public class Permutation
    {
        public int GroupCount = 0;
        public int GroupIndex = 0;
        public List<int> Groups;
        public int NeededGears;
        public int NeededGroups;
        public int GuaranteedGroupsLeft;

        public Permutation(List<int> _groups, int _guaranteedGroupsLeft)
        {
            Groups = _groups;
            NeededGears = 0;
            NeededGroups = Groups.Count - 1;
            GuaranteedGroupsLeft = _guaranteedGroupsLeft;
            foreach (int g in Groups)
            {
                NeededGears += g;
            }
        }

        public Permutation(Permutation _other)
        {
            GroupCount = _other.GroupCount;
            GroupIndex = _other.GroupIndex;
            Groups = _other.Groups;
            NeededGears = _other.NeededGears;
            GuaranteedGroupsLeft = _other.GuaranteedGroupsLeft;
        }

        public bool IncrementGears(int _gearsLeft, int _qsLeft, int _groupsLeft)
        {
            if(GroupIndex == Groups.Count)
            {
                return false;
            }

            GroupCount++;
            NeededGears--;

            if (_gearsLeft > NeededGears)
            {
                return false;
            }

            if (!CanMakeIt(_gearsLeft, _qsLeft, _groupsLeft))
            {
                return false;
            }

            return GroupCount <= Groups[GroupIndex];
        }

        public bool CanMakeIt(int _gearsLeft, int _qsLeft, int _groupsLeft)
        {
            return (_gearsLeft + _qsLeft + GuaranteedGroupsLeft >= NeededGroups + NeededGears);
            return (NeededGears <= _gearsLeft + _qsLeft) && ((_qsLeft - (NeededGears - _gearsLeft)) + GuaranteedGroupsLeft > NeededGroups);
        }

        public bool IncrementGroup(int _gearsLeft, int _qsLeft, int _groupsLeft, bool _wasGuaranteed)
        {
            if(GroupCount == 0)
            {
                return true;
            }

            if(GroupCount != Groups[GroupIndex])
            {
                return false;
            }

            GroupIndex++;
            NeededGroups--;
            GroupCount = 0;
            if (_wasGuaranteed)
            {
                GuaranteedGroupsLeft--;
            }

            return CanMakeIt(_gearsLeft, _qsLeft, _groupsLeft);
        }

        public bool CheckEnd()
        {
            return (GroupCount == 0 && GroupIndex == Groups.Count) || ((GroupCount == Groups[GroupIndex]) && (GroupIndex == Groups.Count - 1));
        }
    }

    public long CleverCheckPermutations(string _initialLine, int _expansions, List<int> _initialGroups)
    {
        string _intermediateLine = _initialLine;
        List<int> groups = new List<int>();
        groups.AddRange(_initialGroups);
        for(int i = 0; i < _expansions; i++)
        {
            _intermediateLine += "?" + _initialLine;
            groups.AddRange(_initialGroups);
        }

        string line = "";
        int _gearsLeft = 0;
        int _qsLeft = 0;
        int _groupsLeft = 2;
        bool _shouldAcceptDot = false;
        for (int i = 0; i < _intermediateLine.Length; i++)
        {
            if (_intermediateLine[i] == '#')
            {
                _gearsLeft++;
                _shouldAcceptDot = true;
                line += _intermediateLine[i];
            }
            else if (_intermediateLine[i] == '?')
            {
                _qsLeft++;
                _shouldAcceptDot = true;
                line += _intermediateLine[i];
            }
            else
            {
                if (_shouldAcceptDot)
                {
                    _groupsLeft++;
                    _shouldAcceptDot = false;
                    line += _intermediateLine[i];
                }
            }
        }

        //Cut trailing .s
        if(line[^1] == '.')
        {
            int _cutIndex;
            for (_cutIndex = line.Length - 2; line[_cutIndex] == '.'; _cutIndex--);
            _cutIndex++;
            line = line[.._cutIndex];
        }

        //Console.WriteLine(line);

        List<Permutation> _options = new()
        {
            new(groups, _groupsLeft)
        };

        
        for (int i = 0; i < line.Length; i++)
        {
            ConcurrentBag<Permutation> _newOptions = new();
            //List<Permutation> _newOptions = new();

            if (line[i] == '#')
            {
                _gearsLeft--;
                
                Parallel.ForEach(_options, p =>
                {
                    if (p.IncrementGears(_gearsLeft, _qsLeft, _groupsLeft))
                    {
                        _newOptions.Add(p);
                    }
                });
                
                /*
                foreach(Permutation p in _options)
                {
                    if (p.IncrementGears(_gearsLeft, _qsLeft, _groupsLeft))
                    {
                        _newOptions.Add(p);
                    }
                }
                */
            }
            else if (line[i] == '?')
            {
                _qsLeft--;
                
                Parallel.ForEach(_options, p =>
                {
                    if (p.CanMakeIt(_gearsLeft, _qsLeft, _groupsLeft))
                    {
                        Permutation _newTry = new(p);
                        if (_newTry.IncrementGroup(_gearsLeft, _qsLeft, _groupsLeft, false))
                        {
                            _newOptions.Add(_newTry);
                        }
                    }

                    if (p.IncrementGears(_gearsLeft, _qsLeft, _groupsLeft))
                    {
                        _newOptions.Add(p);
                    }
                });
                /*
                foreach(Permutation p in _options)
                {
                    if (p.CanMakeIt(_gearsLeft, _qsLeft, _groupsLeft))
                    {
                        Permutation _newTry = new(p);
                        if (_newTry.IncrementGroup(_gearsLeft, _qsLeft, _groupsLeft, false))
                        {
                            _newOptions.Add(_newTry);
                        }
                    }

                    if (p.IncrementGears(_gearsLeft, _qsLeft, _groupsLeft))
                    {
                        _newOptions.Add(p);
                    }
                }
                */
            }
            else //line[i] == '.'
            {
                _groupsLeft--;
                /*
                foreach(Permutation p in _options)
                {
                    if (p.IncrementGroup(_gearsLeft, _qsLeft, _groupsLeft, true))
                    {
                        _newOptions.Add(p);
                    }
                }
                */
                Parallel.ForEach(_options, p =>
                {
                    if (p.IncrementGroup(_gearsLeft, _qsLeft, _groupsLeft, true))
                    {
                        _newOptions.Add(p);
                    }
                });
                
            }

            _options = _newOptions.ToList();
        }

        long total = 0;
        foreach(Permutation p in _options)
        {
            if (p.CheckEnd())
            {
                total++;
            }
        }

        return total;
    }

    void Day_12_2(string[] input)
    {
        long total = 0;
        for (int i = 0; i < input.Length; i++)
        {
            string[] _splitInput = input[i].Split();
            string[] _splitGroups = _splitInput[1].Split(',');
            List<int> _gearGroups = new();
            for (int j = 0; j < _splitGroups.Length; j++)
            {
                _gearGroups.Add(int.Parse(_splitGroups[j]));
            }

            total += CleverCheckPermutations(_splitInput[0], 4, _gearGroups);

            Console.WriteLine(i);
        }

        Console.WriteLine(total);
    }
}