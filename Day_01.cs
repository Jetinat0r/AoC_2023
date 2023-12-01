using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day_01
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_01_Input.txt");

        //Console.WriteLine(GetSubstring("otwo", "two")[0]);
        Day_01_2(lines);
    }

    public void Day_01_1(string[] lines)
    {
        int sum = 0;

        for(int i = 0; i < lines.Length; i++)
        {
            string x = "";
            for(int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] >= '0' && lines[i][j] <= '9')
                {
                    x += lines[i][j];
                    break;
                }
            }

            for (int j = lines[i].Length - 1; j >= 0; j--)
            {
                if (lines[i][j] >= '0' && lines[i][j] <= '9')
                {
                    x += lines[i][j];
                    break;
                }
            }

            sum += int.Parse(x);
        }

        Console.WriteLine(sum);
    }

    public void Day_01_2(string[] lines)
    {
        int sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string x = "";
            int earlyIndex = 999;
            int lateIndex = 0;
            int earlyNum = 0;
            int lateNum = 0;
            int t;

            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] >= '0' && lines[i][j] <= '9')
                {
                    earlyNum = int.Parse(lines[i][j].ToString());
                    earlyIndex = j;
                    break;
                }
            }

            List<int> one = GetSubstring(lines[i], "one");
            List<int> two = GetSubstring(lines[i], "two");
            List<int> three = GetSubstring(lines[i], "three");
            List<int> four = GetSubstring(lines[i], "four");
            List<int> five = GetSubstring(lines[i], "five");
            List<int> six = GetSubstring(lines[i], "six");
            List<int> seven = GetSubstring(lines[i], "seven");
            List<int> eight = GetSubstring(lines[i], "eight");
            List<int> nine = GetSubstring(lines[i], "nine");

            if(one.Count > 0){
                if (one[0] < earlyIndex)
                {
                    earlyNum = 1;
                    earlyIndex = one[0];
                }

                if (one[one.Count - 1] > lateIndex)
                {
                    lateNum = 1;
                    lateIndex = one[one.Count - 1];
                }
            }

            if (two.Count > 0)
            {
                if (two[0] < earlyIndex)
                {
                    earlyNum = 2;
                    earlyIndex = two[0];
                }

                if (two[two.Count - 1] > lateIndex)
                {
                    lateNum = 2;
                    lateIndex = two[two.Count - 1];
                }
            }

            if (three.Count > 0)
            {
                if (three[0] < earlyIndex)
                {
                    earlyNum = 3;
                    earlyIndex = three[0];
                }

                if (three[three.Count - 1] > lateIndex)
                {
                    lateNum = 3;
                    lateIndex = three[three.Count - 1];
                }
            }

            if (four.Count > 0)
            {
                if (four[0] < earlyIndex)
                {
                    earlyNum = 4;
                    earlyIndex = four[0];
                }

                if (four[four.Count - 1] > lateIndex)
                {
                    lateNum = 4;
                    lateIndex = four[four.Count - 1];
                }
            }

            if (five.Count > 0)
            {
                if (five[0] < earlyIndex)
                {
                    earlyNum = 5;
                    earlyIndex = five[0];
                }

                if (five[five.Count - 1] > lateIndex)
                {
                    lateNum = 5;
                    lateIndex = five[five.Count - 1];
                }
            }

            if (six.Count > 0)
            {
                if (six[0] < earlyIndex)
                {
                    earlyNum = 6;
                    earlyIndex = six[0];
                }

                if (six[six.Count - 1] > lateIndex)
                {
                    lateNum = 6;
                    lateIndex = six[six.Count - 1];
                }
            }

            if (seven.Count > 0)
            {
                if (seven[0] < earlyIndex)
                {
                    earlyNum = 7;
                    earlyIndex = seven[0];
                }

                if (seven[seven.Count - 1] > lateIndex)
                {
                    lateNum = 7;
                    lateIndex = seven[seven.Count - 1];
                }
            }

            if (eight.Count > 0)
            {
                if (eight[0] < earlyIndex)
                {
                    earlyNum = 8;
                    earlyIndex = eight[0];
                }

                if (eight[eight.Count - 1] > lateIndex)
                {
                    lateNum = 8;
                    lateIndex = eight[eight.Count - 1];
                }
            }

            if (nine.Count > 0)
            {
                if (nine[0] < earlyIndex)
                {
                    earlyNum = 9;
                    earlyIndex = nine[0];
                }

                if (nine[nine.Count - 1] > lateIndex)
                {
                    lateNum = 9;
                    lateIndex = nine[nine.Count - 1];
                }
            }

            for (int j = lines[i].Length - 1; j >= 0; j--)
            {
                if(j < lateIndex)
                {
                    break;
                }
                if (lines[i][j] >= '0' && lines[i][j] <= '9')
                {
                    lateNum = int.Parse(lines[i][j].ToString());
                    lateIndex = j;
                    break;
                }
            }

            sum += earlyNum * 10 + lateNum;
        }

        Console.WriteLine(sum);
    }

    public List<int> GetSubstring(string s, string b)
    {
        List<int> l = new();
        for (int i = 0; i < s.Length - (b.Length - 1); i++)
        {
            bool isOk = true;
            for(int j = 0; j < b.Length; j++)
            {
                if (s[i+j] != b[j])
                {
                    isOk = false;
                    break;
                }
            }

            if (isOk)
            {
                l.Add(i);
            }
        }

        return l;
    }
}
