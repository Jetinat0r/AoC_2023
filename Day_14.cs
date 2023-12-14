//Template Day class
using System.Diagnostics;

public class Day_14
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_14_Input.txt");

        Day_14_2(lines);
    }

    public enum TileTypes
    {
        Round = 'O',
        Solid = '#',
        Empty = '.'
    }

    public void ShiftUp(TileTypes[,] grid, int x, int y)
    {
        while (y > 0 && grid[x, y - 1] == TileTypes.Empty)
        {
            grid[x, y-1] = TileTypes.Round;
            grid[x, y] = TileTypes.Empty;
            y--;
        }
    }

    public void ShiftLeft(TileTypes[,] grid, int x, int y)
    {
        while (x > 0 && grid[x - 1, y] == TileTypes.Empty)
        {
            grid[x - 1, y] = TileTypes.Round;
            grid[x, y] = TileTypes.Empty;
            x--;
        }
    }

    public void ShiftDown(TileTypes[,] grid, int x, int y, int yBounds)
    {
        while (y < yBounds - 1 && grid[x, y + 1] == TileTypes.Empty)
        {
            grid[x, y + 1] = TileTypes.Round;
            grid[x, y] = TileTypes.Empty;
            y++;
        }
    }

    public void ShiftRight(TileTypes[,] grid, int x, int y, int xBounds)
    {
        while (x < xBounds - 1 && grid[x + 1, y] == TileTypes.Empty)
        {
            grid[x + 1, y] = TileTypes.Round;
            grid[x, y] = TileTypes.Empty;
            x++;
        }
    }

    public void DebugTiles(TileTypes[,] grid, int xBounds, int yBounds)
    {
        for (int y = 0; y < yBounds; y++)
        {
            for (int x = 0; x < xBounds; x++)
            {
                Console.Write((char)grid[x, y]);
            }
            Console.WriteLine();
        }
    }

    public TileTypes[,] CopyTiles(TileTypes[,] grid)
    {
        TileTypes[,] _copy = new TileTypes[grid.GetLength(0), grid.GetLength(1)];
        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                _copy[x, y] = grid[x, y];
            }
        }
        return _copy;
    }

    void Day_14_1(string[] input)
    {
        TileTypes[,] grid = new TileTypes[input[0].Length, input.Length];
        
        for(int y = 0; y < input.Length; y++)
        {
            for(int x = 0; x < input[y].Length; x++)
            {
                grid[x,y] = (TileTypes)input[y][x];
            }
        }

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if(grid[x, y] == TileTypes.Round)
                {
                    ShiftUp(grid, x, y);
                }
            }
        }

        long total = 0;
        for (int y = 0; y < input.Length; y++)
        {
            long rowTotal = 0;
            for (int x = 0; x < input[y].Length; x++)
            {
                if (grid[x, y] == TileTypes.Round)
                {
                    rowTotal++;
                }
            }

            rowTotal *= (input.Length - y);
            total += rowTotal;
        }

        DebugTiles(grid, input[0].Length, input.Length);
        Console.WriteLine(total);
    }

    
    void Day_14_2(string[] input)
    {
        TileTypes[,] grid = new TileTypes[input[0].Length, input.Length];

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                grid[x, y] = (TileTypes)input[y][x];
            }
        }


        
        for (int i = 0; i < 500; i++)
        {
            //North
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftUp(grid, x, y);
                    }
                }
            }

            //West
            for(int x = 0; x < input[0].Length; x++)
            {
                for(int y = 0; y < input.Length; y++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftLeft(grid, x, y);
                    }
                }
            }


            //South
            for (int y = input.Length - 1; y >= 0; y--)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftDown(grid, x, y, input.Length);
                    }
                }
            }

            //East
            for (int x = input[0].Length - 1; x >= 0; x--)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftRight(grid, x, y, input[0].Length);
                    }
                }
            }

            
        }

        long iters = 500;
        Queue<long> _pattern = new();
        while (true)
        {
            //North
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftUp(grid, x, y);
                    }
                }
            }

            //West
            for (int x = 0; x < input[0].Length; x++)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftLeft(grid, x, y);
                    }
                }
            }


            //South
            for (int y = input.Length - 1; y >= 0; y--)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftDown(grid, x, y, input.Length);
                    }
                }
            }

            //East
            for (int x = input[0].Length - 1; x >= 0; x--)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftRight(grid, x, y, input[0].Length);
                    }
                }
            }

            iters++;

            long total = 0;
            for (int y = 0; y < input.Length; y++)
            {
                long rowTotal = 0;
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        rowTotal++;
                    }
                }

                rowTotal *= (input.Length - y);
                total += rowTotal;
            }

            if(_pattern.Count > 10 && _pattern.Peek() == total)
            {
                TileTypes[,] _copyGrid = CopyTiles(grid);
                Queue<long> _copyQ = new(_pattern);
                _copyQ.Dequeue();

                bool isPattern = true;
                while(_copyQ.Count > 0)
                {
                    //North
                    for (int y = 0; y < input.Length; y++)
                    {
                        for (int x = 0; x < input[y].Length; x++)
                        {
                            if (_copyGrid[x, y] == TileTypes.Round)
                            {
                                ShiftUp(_copyGrid, x, y);
                            }
                        }
                    }

                    //West
                    for (int x = 0; x < input[0].Length; x++)
                    {
                        for (int y = 0; y < input.Length; y++)
                        {
                            if (_copyGrid[x, y] == TileTypes.Round)
                            {
                                ShiftLeft(_copyGrid, x, y);
                            }
                        }
                    }


                    //South
                    for (int y = input.Length - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < input[y].Length; x++)
                        {
                            if (_copyGrid[x, y] == TileTypes.Round)
                            {
                                ShiftDown(_copyGrid, x, y, input.Length);
                            }
                        }
                    }

                    //East
                    for (int x = input[0].Length - 1; x >= 0; x--)
                    {
                        for (int y = 0; y < input.Length; y++)
                        {
                            if (_copyGrid[x, y] == TileTypes.Round)
                            {
                                ShiftRight(_copyGrid, x, y, input[0].Length);
                            }
                        }
                    }

                    long subTotal = 0;
                    for (int y = 0; y < input.Length; y++)
                    {
                        long rowTotal = 0;
                        for (int x = 0; x < input[y].Length; x++)
                        {
                            if (_copyGrid[x, y] == TileTypes.Round)
                            {
                                rowTotal++;
                            }
                        }

                        rowTotal *= (input.Length - y);
                        subTotal += rowTotal;
                    }

                    if(_copyQ.Dequeue() != subTotal)
                    {
                        isPattern = false;
                        break;
                    }
                }

                if (isPattern)
                {
                    break;
                }
            }

            _pattern.Enqueue(total);
        }

        long numLeft = (1000000000 - iters) % _pattern.Count;
        Console.WriteLine($"{iters} : {_pattern.Count} : {numLeft}");
        for(int i = 0; i < numLeft; i++)
        {
            //North
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftUp(grid, x, y);
                    }
                }
            }

            //West
            for (int x = 0; x < input[0].Length; x++)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftLeft(grid, x, y);
                    }
                }
            }


            //South
            for (int y = input.Length - 1; y >= 0; y--)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftDown(grid, x, y, input.Length);
                    }
                }
            }

            //East
            for (int x = input[0].Length - 1; x >= 0; x--)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    if (grid[x, y] == TileTypes.Round)
                    {
                        ShiftRight(grid, x, y, input[0].Length);
                    }
                }
            }
        }

        long finalTotal = 0;
        for (int y = 0; y < input.Length; y++)
        {
            long rowTotal = 0;
            for (int x = 0; x < input[y].Length; x++)
            {
                if (grid[x, y] == TileTypes.Round)
                {
                    rowTotal++;
                }
            }

            rowTotal *= (input.Length - y);
            finalTotal += rowTotal;
        }

        Console.WriteLine(finalTotal);
    }
}