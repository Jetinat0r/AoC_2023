//Template Day class
public class Day_15
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_15_Input.txt");

        Day_15_2(lines);
    }

    void Day_15_1(string[] input)
    {
        string[] sequences = input[0].Split(',');

        long total = 0;
        for(int i  = 0; i < sequences.Length; i++)
        {
            long curVal = 0;
            for(int c = 0; c < sequences[i].Length; c++)
            {
                curVal += sequences[i][c];
                curVal *= 17;
                curVal %= 256;
            }

            total += curVal;
        }

        Console.WriteLine(total);
    }

    public class Entry
    {
        public string Key;
        public long Value;

        public Entry(string key, long value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    void Day_15_2(string[] input)
    {
        string[] sequences = input[0].Split(',');

        List<Entry>[] Map = new List<Entry>[256];
        for(int i = 0; i < 256; i++)
        {
            Map[i] = new();
        }


        for (int i = 0; i < sequences.Length; i++)
        {
            long curIndex = 0;
            int c;
            for (c = 0; c < sequences[i].Length; c++)
            {
                if(sequences[i][c] == '-' || sequences[i][c] == '=')
                {
                    break;
                }
                curIndex += sequences[i][c];
                curIndex *= 17;
                curIndex %= 256;
            }

            if (sequences[i][c] == '=')
            {
                Entry? _e = Map[curIndex].Find(x => x.Key == sequences[i][..c]);
                if (_e == null)
                {
                    Map[curIndex].Add(new(sequences[i][..c], long.Parse(sequences[i][(c+1)..])));
                }
                else
                {
                    _e.Value = long.Parse(sequences[i][(c + 1)..]);
                }
            }
            else
            {
                Entry? _e = Map[curIndex].Find(x => x.Key == sequences[i][..c]);
                if(_e != null )
                {
                    Map[curIndex].Remove(_e);
                }
            }
        }

        long total = 0;
        for(int i = 0; i < 256; i++)
        {
            for(int j = 0; j < Map[i].Count; j++)
            {
                total += (i + 1) * (j + 1) * Map[i][j].Value;
            }
        }

        Console.WriteLine(total);
    }
}