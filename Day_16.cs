//Template Day class
public class Day_16
{
    public void Main()
    {
        string[] lines = File.ReadAllLines("Day_16_Input.txt");

        Day_16_2(lines);
    }

    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public class Laser
    {
        public int x;
        public int y;
        public Direction dir;

        public Laser(int _x, int _y, Direction _dir)
        {
            x = _x;
            y = _y;
            dir = _dir;
        }

        public (int, int, Direction) GetKey()
        {
            return (x, y, dir);
        }
    }

    public void DebugEnergizedTiles(bool[,] energizedTiles)
    {
        for (int x = 0; x < energizedTiles.GetLength(0); x++)
        {
            for (int y = 0; y < energizedTiles.GetLength(1); y++)
            {
                Console.Write(energizedTiles[x, y] ? '#' : '.');
            }
            Console.WriteLine();
        }
    }

    public void MoveLaser(string[] input, bool[,] energizedTiles, int xBounds, int yBounds, List<Laser> lasers, Dictionary<(int, int, Direction), char> existingDict)
    {
        Laser curLaser = lasers[0];

        char newTile;
        switch (curLaser.dir)
        {
            case (Direction.LEFT):
                if(curLaser.x == 0)
                {
                    lasers.RemoveAt(0);
                    return;
                }

                curLaser.x--;
                energizedTiles[curLaser.x, curLaser.y] = true;

                newTile = input[curLaser.y][curLaser.x];
                if(newTile == '/')
                {
                    curLaser.dir = Direction.DOWN;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if(newTile == '\\')
                {
                    curLaser.dir = Direction.UP;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if(newTile == '|')
                {
                    curLaser.dir = Direction.DOWN;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }

                    if (!existingDict.ContainsKey((curLaser.x, curLaser.y, Direction.UP)))
                    {
                        lasers.Add(new(curLaser.x, curLaser.y, Direction.UP));
                        existingDict.Add((curLaser.x, curLaser.y, Direction.UP), '.');
                    }
                }


                break;
            case (Direction.RIGHT):
                if (curLaser.x == xBounds - 1)
                {
                    lasers.RemoveAt(0);
                    return;
                }

                curLaser.x++;
                energizedTiles[curLaser.x, curLaser.y] = true;

                newTile = input[curLaser.y][curLaser.x];
                if (newTile == '/')
                {
                    curLaser.dir = Direction.UP;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '\\')
                {
                    curLaser.dir = Direction.DOWN;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '|')
                {
                    curLaser.dir = Direction.UP;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }

                    if (!existingDict.ContainsKey((curLaser.x, curLaser.y, Direction.DOWN)))
                    {
                        lasers.Add(new(curLaser.x, curLaser.y, Direction.DOWN));
                        existingDict.Add((curLaser.x, curLaser.y, Direction.DOWN), '.');
                    }
                }

                break;
            case (Direction.UP):
                if (curLaser.y == 0)
                {
                    lasers.RemoveAt(0);
                    return;
                }

                curLaser.y--;
                energizedTiles[curLaser.x, curLaser.y] = true;

                newTile = input[curLaser.y][curLaser.x];
                if (newTile == '/')
                {
                    curLaser.dir = Direction.RIGHT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '\\')
                {
                    curLaser.dir = Direction.LEFT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '-')
                {
                    curLaser.dir = Direction.RIGHT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }

                    if (!existingDict.ContainsKey((curLaser.x, curLaser.y, Direction.LEFT)))
                    {
                        lasers.Add(new(curLaser.x, curLaser.y, Direction.LEFT));
                        existingDict.Add((curLaser.x, curLaser.y, Direction.LEFT), '.');
                    }
                }

                break;
            case (Direction.DOWN):
                if (curLaser.y == yBounds - 1)
                {
                    lasers.RemoveAt(0);
                    return;
                }

                curLaser.y++;
                energizedTiles[curLaser.x, curLaser.y] = true;

                newTile = input[curLaser.y][curLaser.x];
                if (newTile == '/')
                {
                    curLaser.dir = Direction.LEFT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '\\')
                {
                    curLaser.dir = Direction.RIGHT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '-')
                {
                    curLaser.dir = Direction.LEFT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }

                    if (!existingDict.ContainsKey((curLaser.x, curLaser.y, Direction.RIGHT)))
                    {
                        lasers.Add(new(curLaser.x, curLaser.y, Direction.RIGHT));
                        existingDict.Add((curLaser.x, curLaser.y, Direction.RIGHT), '.');
                    }
                }

                break;
        }
    }

    public void HandleFirstTile(string[] input, bool[,] energizedTiles, int xBounds, int yBounds, List<Laser> lasers, Dictionary<(int, int, Direction), char> existingDict)
    {
        Laser curLaser = lasers[0];

        char newTile;
        switch (curLaser.dir)
        {
            case (Direction.LEFT):
                if (curLaser.x == 0)
                {
                    lasers.RemoveAt(0);
                    return;
                }

                energizedTiles[curLaser.x, curLaser.y] = true;

                newTile = input[curLaser.y][curLaser.x];
                if (newTile == '/')
                {
                    curLaser.dir = Direction.DOWN;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '\\')
                {
                    curLaser.dir = Direction.UP;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '|')
                {
                    curLaser.dir = Direction.DOWN;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }

                    if (!existingDict.ContainsKey((curLaser.x, curLaser.y, Direction.UP)))
                    {
                        lasers.Add(new(curLaser.x, curLaser.y, Direction.UP));
                        existingDict.Add((curLaser.x, curLaser.y, Direction.UP), '.');
                    }
                }


                break;
            case (Direction.RIGHT):
                if (curLaser.x == xBounds - 1)
                {
                    lasers.RemoveAt(0);
                    return;
                }

                energizedTiles[curLaser.x, curLaser.y] = true;

                newTile = input[curLaser.y][curLaser.x];
                if (newTile == '/')
                {
                    curLaser.dir = Direction.UP;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '\\')
                {
                    curLaser.dir = Direction.DOWN;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '|')
                {
                    curLaser.dir = Direction.UP;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }

                    if (!existingDict.ContainsKey((curLaser.x, curLaser.y, Direction.DOWN)))
                    {
                        lasers.Add(new(curLaser.x, curLaser.y, Direction.DOWN));
                        existingDict.Add((curLaser.x, curLaser.y, Direction.DOWN), '.');
                    }
                }

                break;
            case (Direction.UP):
                if (curLaser.y == 0)
                {
                    lasers.RemoveAt(0);
                    return;
                }

                energizedTiles[curLaser.x, curLaser.y] = true;

                newTile = input[curLaser.y][curLaser.x];
                if (newTile == '/')
                {
                    curLaser.dir = Direction.RIGHT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '\\')
                {
                    curLaser.dir = Direction.LEFT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '-')
                {
                    curLaser.dir = Direction.RIGHT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }

                    if (!existingDict.ContainsKey((curLaser.x, curLaser.y, Direction.LEFT)))
                    {
                        lasers.Add(new(curLaser.x, curLaser.y, Direction.LEFT));
                        existingDict.Add((curLaser.x, curLaser.y, Direction.LEFT), '.');
                    }
                }

                break;
            case (Direction.DOWN):
                if (curLaser.y == yBounds - 1)
                {
                    lasers.RemoveAt(0);
                    return;
                }

                energizedTiles[curLaser.x, curLaser.y] = true;

                newTile = input[curLaser.y][curLaser.x];
                if (newTile == '/')
                {
                    curLaser.dir = Direction.LEFT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '\\')
                {
                    curLaser.dir = Direction.RIGHT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }
                }
                else if (newTile == '-')
                {
                    curLaser.dir = Direction.LEFT;
                    if (existingDict.ContainsKey(curLaser.GetKey()))
                    {
                        lasers.RemoveAt(0);
                    }
                    else
                    {
                        existingDict.Add(curLaser.GetKey(), '.');
                    }

                    if (!existingDict.ContainsKey((curLaser.x, curLaser.y, Direction.RIGHT)))
                    {
                        lasers.Add(new(curLaser.x, curLaser.y, Direction.RIGHT));
                        existingDict.Add((curLaser.x, curLaser.y, Direction.RIGHT), '.');
                    }
                }

                break;
        }
    }

    void Day_16_1(string[] input)
    {
        bool[,] energizedTiles = new bool[input[0].Length, input.Length];
        for(int x = 0; x < energizedTiles.GetLength(0); x++)
        {
            for (int y = 0;y < energizedTiles.GetLength(1); y++)
            {
                energizedTiles[x, y] = false;
            }
        }

        Dictionary<(int, int, Direction), char> existingDict = new();
        List<Laser> lasers = new()
        {
            new Laser(0, 0, Direction.RIGHT)
        };
        HandleFirstTile(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);

        while(lasers.Count > 0)
        {
            MoveLaser(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);
        }

        long total = 0;
        for (int x = 0; x < energizedTiles.GetLength(0); x++)
        {
            for (int y = 0; y < energizedTiles.GetLength(1); y++)
            {
                if (energizedTiles[x, y])
                {
                    total++;
                }
            }
        }

        DebugEnergizedTiles(energizedTiles);
        Console.WriteLine(total);
    }

    void Day_16_2(string[] input)
    {
        bool[,] energizedTiles = new bool[input[0].Length, input.Length];

        long bestTotal = 0;
        for (int x = 0; x < energizedTiles.GetLength(0); x++)
        {
            for (int _x = 0; _x < energizedTiles.GetLength(0); _x++)
            {
                for (int _y = 0; _y < energizedTiles.GetLength(1); _y++)
                {
                    energizedTiles[_x, _y] = false;
                }
            }

            Dictionary<(int, int, Direction), char> existingDict = new();
            List<Laser> lasers = new()
            {
                new Laser(x, 0, Direction.DOWN)
            };
            HandleFirstTile(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);

            while (lasers.Count > 0)
            {
                MoveLaser(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);
            }

            long total = 0;
            for (int _x = 0; _x < energizedTiles.GetLength(0); _x++)
            {
                for (int _y = 0; _y < energizedTiles.GetLength(1); _y++)
                {
                    if (energizedTiles[_x, _y])
                    {
                        total++;
                    }
                }
            }

            if(total > bestTotal)
            {
                bestTotal = total;
            }

            ///////////////////////////////////////////////////////////////////////
            for (int _x = 0; _x < energizedTiles.GetLength(0); _x++)
            {
                for (int _y = 0; _y < energizedTiles.GetLength(1); _y++)
                {
                    energizedTiles[_x, _y] = false;
                }
            }

            existingDict = new();
            lasers = new()
            {
                new Laser(x, energizedTiles.GetLength(1) - 1, Direction.UP)
            };
            HandleFirstTile(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);

            while (lasers.Count > 0)
            {
                MoveLaser(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);
            }

            total = 0;
            for (int _x = 0; _x < energizedTiles.GetLength(0); _x++)
            {
                for (int _y = 0; _y < energizedTiles.GetLength(1); _y++)
                {
                    if (energizedTiles[_x, _y])
                    {
                        total++;
                    }
                }
            }

            if (total > bestTotal)
            {
                bestTotal = total;
            }
        }

        for(int y = 0; y <  energizedTiles.GetLength(1); y++)
        {
            for (int _x = 0; _x < energizedTiles.GetLength(0); _x++)
            {
                for (int _y = 0; _y < energizedTiles.GetLength(1); _y++)
                {
                    energizedTiles[_x, _y] = false;
                }
            }

            Dictionary<(int, int, Direction), char> existingDict = new();
            List<Laser> lasers = new()
            {
                new Laser(0, y, Direction.RIGHT)
            };
            HandleFirstTile(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);

            while (lasers.Count > 0)
            {
                MoveLaser(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);
            }

            long total = 0;
            for (int _x = 0; _x < energizedTiles.GetLength(0); _x++)
            {
                for (int _y = 0; _y < energizedTiles.GetLength(1); _y++)
                {
                    if (energizedTiles[_x, _y])
                    {
                        total++;
                    }
                }
            }

            if (total > bestTotal)
            {
                bestTotal = total;
            }

            ///////////////////////////////////////////////////////////////////////
            for (int _x = 0; _x < energizedTiles.GetLength(0); _x++)
            {
                for (int _y = 0; _y < energizedTiles.GetLength(1); _y++)
                {
                    energizedTiles[_x, _y] = false;
                }
            }

            existingDict = new();
            lasers = new()
            {
                new Laser(energizedTiles.GetLength(0) - 1, y, Direction.LEFT)
            };
            HandleFirstTile(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);

            while (lasers.Count > 0)
            {
                MoveLaser(input, energizedTiles, input[0].Length, input.Length, lasers, existingDict);
            }

            total = 0;
            for (int _x = 0; _x < energizedTiles.GetLength(0); _x++)
            {
                for (int _y = 0; _y < energizedTiles.GetLength(1); _y++)
                {
                    if (energizedTiles[_x, _y])
                    {
                        total++;
                    }
                }
            }

            if (total > bestTotal)
            {
                bestTotal = total;
            }
        }

        //DebugEnergizedTiles(energizedTiles);
        Console.WriteLine(bestTotal);
    }
}