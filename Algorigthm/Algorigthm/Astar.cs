using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorigthm
{
    public class Program
    {
        static void Main(string[] args)
        {
            int boardSize;
            Board board = new Board();
            do
            {
                boardSize = int.Parse(Console.ReadLine());
                Console.Clear();

                board.InitializeBoard(boardSize);
                board.DrawBoard();
            } while (boardSize % 2 == 0);

            Astar astar = new Astar(board);
        }
    }

    public class Astar
    {
        private List<Node> openList = new List<Node>();
        private List<Node> closedList = new List<Node>();

        private Node start;
        private Node target;

        public Astar(Board board)
        {
            start = board.nodes[board.size - 2, board.size - 2];
        }

    }

    public class Board
    {
        const char CIRCLE = '\u25cf';
        public Node[,] nodes;
        public int size;

        public void InitializeBoard(int _size)
        {
            //_size 짝수라면 함수 종료
            if (_size % 2 == 0) return;

            size = _size;
            nodes = new Node[size, size];

            //노도들을 초기화 하고 x나 y가 짝수인 노드들만 비워둔다.
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    nodes[x, y].x = x;
                    nodes[x, y].y = y;

                    if (x % 2 == 0 || y % 2 == 0)
                        nodes[x, y].type = NodeType.Fill;
                    else
                        nodes[x, y].type = NodeType.Empty;
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

                    if (y == size - 2)
                    {
                        nodes[x + 1, y].type = NodeType.Empty;
                        continue;
                    }
                    if (x == size - 2)
                    {
                        nodes[x, y + 1].type = NodeType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        nodes[x + 1, y].type = NodeType.Empty;
                    }
                    else
                    {
                        nodes[x, y + 1].type = NodeType.Empty;
                    }
                }
            }
        }

        //미로를 그리는 함수
        public void DrawBoard()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.ForegroundColor = GetTileColor(nodes[x, y].type);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
        }

        //노드의 type에 따라 다른 색상을 리턴해주는 함수
        ConsoleColor GetTileColor(NodeType type)
        {
            switch (type)
            {
                case NodeType.Empty:
                    return ConsoleColor.White;
                case NodeType.Fill:
                    return ConsoleColor.DarkGray;
                default:
                    return ConsoleColor.White;
            }
        }
    }

    public struct Node
    {
        public int x, y;
        public int F { get { return G + H; } }
        public int G { get; private set; } 
        public int H { get; private set; } 
        public NodeType type { get; set; }

        public void SetNode(int g, int h)
        {
            G = g;
            H = h;
        }
    }

    public enum NodeType
    {
        Empty,
        Fill
    }
}
