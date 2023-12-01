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

        Day_01_1(lines);
    }

    public void Day_01_1(string[] lines)
    {


        for(int i = 0; i < lines.Length; i++)
        {
            Console.WriteLine(lines[i]);
        }
    }

    public void Day_01_2(string[] lines)
    {

    }
}
