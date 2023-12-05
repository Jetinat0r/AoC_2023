//Template Day class
public class Day_05
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_05_Input.txt");

        Day_05_2(lines);
    }

    public class Map
    {
        public ulong SrcStart;
        public ulong SrcEnd;
        public ulong DstStart;
        public ulong DstEnd;

        public Map(ulong _ss, ulong _se, ulong _ds, ulong _de)
        {
            SrcStart = _ss;
            SrcEnd = _se;
            DstStart = _ds;
            DstEnd = _de;
        }

        public bool InSrcRange(ulong _src)
        {
            return (_src >= SrcStart && _src <= SrcEnd);
        }

        //_src must be in range
        public ulong RunMap(ulong _src)
        {
            return DstStart + (_src - SrcStart);
        }

        public bool InComplexSrcRange(Range _r)
        {
            return (_r.End >= SrcStart && _r.Start <= SrcEnd);
        }

        //_r must be in range
        public List<Range> RunComplexMap(Range _r, out int _ret)
        {
            List<Range> _splitRanges = new List<Range>();
            ulong _heldStart = _r.Start;
            ulong _heldEnd = _r.End;
            bool _untaintedStart = false;
            bool _untaintedEnd = false;

            if(_r.Start < SrcStart)
            {
                _splitRanges.Add(new Range(_r.Start, SrcStart - 1));
                _heldStart = SrcStart;
                _untaintedStart = true;
            }

            if(_r.End > SrcEnd)
            {
                _splitRanges.Add(new Range(SrcEnd + 1, _r.End));
                _heldEnd = SrcEnd;
                _untaintedEnd = true;
            }

            _splitRanges.Add(new Range(DstStart + (_heldStart - SrcStart), DstStart + (_heldEnd - SrcStart)));

            _ret = 0;
            if (_untaintedStart)
            {
                _ret += 1;
            }
            if (_untaintedEnd)
            {
                _ret += 2;
            }

            return _splitRanges;
        }
    }

    public class Range
    {
        public ulong Start;
        public ulong End;

        public Range(ulong _s, ulong _e)
        {
            Start = _s;
            End = _e;
        }
    }

    void Day_05_1(string[] input)
    {
        int _curLine = 0;

        List<ulong> _seeds = ParseSeeds(input, ref _curLine);
        List<Map> _seedToSoilMaps = ParseMaps(input, ref _curLine);
        List<Map> _soilToFertilizerMaps = ParseMaps(input, ref _curLine);
        List<Map> _fertilizerToWaterMaps = ParseMaps(input, ref _curLine);
        List<Map> _waterToLightMaps = ParseMaps(input, ref _curLine);
        List<Map> _lightToTemperatureMaps = ParseMaps(input, ref _curLine);
        List<Map> _temperatureToHumidityMaps = ParseMaps(input, ref _curLine);
        List<Map> _humidityToLocationMaps = ParseMaps(input, ref _curLine);
        List<ulong> _locations = new();


        for(int i = 0; i < _seeds.Count; i++)
        {
            ulong _val = _seeds[i];
            _val = RunMap(_seedToSoilMaps, _val);
            _val = RunMap(_soilToFertilizerMaps, _val);
            _val = RunMap(_fertilizerToWaterMaps, _val);
            _val = RunMap(_waterToLightMaps, _val);
            _val = RunMap(_lightToTemperatureMaps, _val);
            _val = RunMap(_temperatureToHumidityMaps, _val);
            _val = RunMap(_humidityToLocationMaps, _val);
            _locations.Add(_val);
        }

        ulong _smallest = _locations[0];
        for(int i = 1; i < _locations.Count; i++)
        {
            if (_locations[i] < _smallest)
            {
                _smallest = _locations[i];
            }
        }

        Console.WriteLine(_smallest);
    }

    void Day_05_2(string[] input)
    {
        int _curLine = 0;

        List<Range> _seeds = ParseComplexSeeds(input, ref _curLine);
        List<Map> _seedToSoilMaps = ParseMaps(input, ref _curLine);
        List<Map> _soilToFertilizerMaps = ParseMaps(input, ref _curLine);
        List<Map> _fertilizerToWaterMaps = ParseMaps(input, ref _curLine);
        List<Map> _waterToLightMaps = ParseMaps(input, ref _curLine);
        List<Map> _lightToTemperatureMaps = ParseMaps(input, ref _curLine);
        List<Map> _temperatureToHumidityMaps = ParseMaps(input, ref _curLine);
        List<Map> _humidityToLocationMaps = ParseMaps(input, ref _curLine);
        List<List<Range>> _locations = new();

        List<Range> _val;

        _val = RunComplexMap(_seedToSoilMaps, _seeds);
        _val = RunComplexMap(_soilToFertilizerMaps, _val);
        _val = RunComplexMap(_fertilizerToWaterMaps, _val);
        _val = RunComplexMap(_waterToLightMaps, _val);
        _val = RunComplexMap(_lightToTemperatureMaps, _val);
        _val = RunComplexMap(_temperatureToHumidityMaps, _val);
        _val = RunComplexMap(_humidityToLocationMaps, _val);
        _locations.Add(_val);

        ulong _smallest = _locations[0][0].Start;
        for (int i = 0; i < _locations.Count; i++)
        {
            for(int j = 0; j < _locations[i].Count; j++)
            {
                if (_locations[i][j].Start < _smallest)
                {
                    _smallest = _locations[i][j].Start;
                }
            }
        }

        Console.WriteLine(_smallest);
    }

    public List<ulong> ParseSeeds(string[] input, ref int _curLine)
    {
        List<ulong> _seeds = new();

        string[] _seedLine = input[_curLine].Split();
        for(int i = 1; i < _seedLine.Length; i++)
        {
            _seeds.Add(ulong.Parse(_seedLine[i]));
        }

        _curLine = 2;

        return _seeds;
    }

    public List<Range> ParseComplexSeeds(string[] input, ref int _curLine)
    {
        List<Range> _seeds = new();

        string[] _seedLine = input[_curLine].Split();
        for (int i = 1; i < _seedLine.Length; i += 2)
        {
            ulong _start = ulong.Parse(_seedLine[i]);
            _seeds.Add(new Range(_start, _start + (ulong.Parse(_seedLine[i + 1]) - 1)));
        }

        _curLine = 2;

        return _seeds;
    }

    public Map MakeMap(string _line)
    {
        string[] _splitLine = _line.Split();

        ulong _dstStart = ulong.Parse(_splitLine[0]);
        ulong _srcStart = ulong.Parse(_splitLine[1]);
        ulong _range = ulong.Parse(_splitLine[2]) - 1;

        return new Map(_srcStart, _srcStart + _range, _dstStart, _dstStart + _range);
    }

    public List<Map> ParseMaps(string[] input, ref int _curLine)
    {
        List<Map> _seedToSoilMaps = new();
        _curLine += 1;

        while (_curLine < input.Length && input[_curLine] != "")
        {
            _seedToSoilMaps.Add(MakeMap(input[_curLine]));

            _curLine++;
        }

        _curLine += 1;

        return _seedToSoilMaps;
    }

    public ulong RunMap(List<Map> _maps, ulong _val)
    {
        foreach(Map _map in _maps)
        {
            if (_map.InSrcRange(_val))
            {
                return _map.RunMap(_val);
            }
        }

        return _val;
    }

    public List<Range> RunComplexMap(List<Map> _maps, List<Range> _val)
    {
        List<Range> _newRanges = new();
        

        for(int i = 0; i < _val.Count; i++)
        {
            bool _wasBroken = false;
            for (int j = 0; j < _maps.Count; j++)
            {
                if (_maps[j].InComplexSrcRange(_val[i]))
                {
                    List<Range> _modifiedRanges = _maps[j].RunComplexMap(_val[i], out int _flags);
                    _wasBroken = true;

                    if(_flags == 1)
                    {
                        _val.Add(_modifiedRanges[0]);
                        _newRanges.Add(_modifiedRanges[1]);
                    }
                    else if(_flags == 2)
                    {
                        _val.Add(_modifiedRanges[0]);
                        _newRanges.Add(_modifiedRanges[1]);
                    }
                    else if(_flags == 3)
                    {
                        _val.Add(_modifiedRanges[0]);
                        _val.Add(_modifiedRanges[1]);
                        _newRanges.Add(_modifiedRanges[2]);
                    }
                    else
                    {
                        _newRanges.Add(_modifiedRanges[0]);
                    }

                    break;
                }
            }

            if (!_wasBroken)
            {
                _newRanges.Add(_val[i]);
            }
        }

        return _newRanges;
    }
}