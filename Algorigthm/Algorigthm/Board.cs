using System;
using System.Collections.Generic;
using System.Text;

namespace Algorigthm
{
    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] tile;
        public int size;

        public enum TileType
        {
            Empty,
            Fill
        }

        public void InitializeBoard(int _size)
        {
            //_size 짝수라면 함수 종료
            if (_size % 2 == 0) return;

            size = _size;
            tile = new TileType[size, size];

            //x나 y가 짝수인 타일들만 비워둔다.
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        tile[x, y] = TileType.Fill;
                    else
                        tile[x, y] = TileType.Empty;
                }
            }

            //Binary Tree 알고리즘을 이용하여 랜덤하게 미로를 생성
            Random rand = new Random();
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0) continue;
                    if (x == size - 2 && y == size - 2) continue;

                    if(y == size - 2)
                    {
                        tile[x + 1, y] = TileType.Empty;
                        continue;
                    }
                    if (x == size - 2)
                    {
                        tile[x, y + 1] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        tile[x + 1, y] = TileType.Empty;
                    }
                    else
                    {
                        tile[x, y + 1] = TileType.Empty;
                    }
                }
            }
        }

        public void DrawBoard()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.ForegroundColor = GetTileColor(tile[x, y]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.White;
                case TileType.Fill:
                    return ConsoleColor.DarkGray;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
