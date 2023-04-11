using System.Linq;

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
                Console.Write("사이즈 입력(홀수) : ");

                boardSize = int.Parse(Console.ReadLine());
                Console.Clear();

                board.InitializeBoard(boardSize);
                board.DrawBoard();
            } while (boardSize % 2 == 0);

            Astar astar = new Astar(board);
            astar.FindPath();
            astar.WalkPath();
        }
    }

    public class Astar
    {
        private int[] deltaX = new int[8] { -1, 1, 0, 0, 1, -1, 1, -1 };
        private int[] deltaY = new int[8] { 0, 0, -1, 1, 1, -1, -1, 1 };
        private int[] cost = new int[8] { 10, 10, 10, 10, 14, 14, 14, 14 };
        private int diagonalIndex = 3;

        private List<Node> openList;
        private List<Node> closedList;
        private Queue<Node> finalPathQueue;

        private Node start;
        private Node target;
        private Board board;

        public Astar(Board _board)
        {
            openList = new List<Node>();
            closedList = new List<Node>();

            start = _board.nodes[1, 1];
            target = _board.nodes[_board.size - 2, _board.size - 2];
            board = _board;
        }

        public void WalkPath()
        {
            const int WAIT_TICK = 100;
            int lastTick = 0;

            while (finalPathQueue.Count != 0)
            {
                int curTick = System.Environment.TickCount;

                if (curTick - lastTick < WAIT_TICK)
                    continue;

                Node moveNode = finalPathQueue.Dequeue();

                Console.Clear();
                board.DrawBoard(moveNode, target);
                lastTick = curTick;
            }
        }

        public void FindPath()
        {
            Node curNode;
            openList.Add(start);

            while (true)
            {
                curNode = GetPriorityNode(openList);
                openList.Remove(curNode);
                closedList.Add(curNode);

                if (curNode == target)
                {
                    Queue<Node> tempPathQueue = new Queue<Node>();
                    Node pathCurNode = target;

                    while (pathCurNode != start)
                    {
                        tempPathQueue.Enqueue(pathCurNode);
                        pathCurNode = pathCurNode.parentNode;
                    }
                    tempPathQueue.Enqueue(start);
                    finalPathQueue = new Queue<Node>(tempPathQueue.Reverse());
                    break;
                }

                for (int i = 0; i < cost.Length; i++)
                {
                    var x = curNode.x + deltaX[i];
                    var y = curNode.y + deltaY[i];

                    if (!CheckNode(x, y)) continue;
                    if (i >= diagonalIndex) if (!DiagonalCheck(curNode, x, y)) continue;

                    var neighBorNode = board.nodes[x, y];
                    if (!openList.Contains(neighBorNode))
                    {
                        var g = curNode.G + cost[i];
                        var h = 10 * (Math.Abs(target.x - neighBorNode.x) + Math.Abs(target.y - neighBorNode.y));

                        neighBorNode.SetNode(g, h);
                        neighBorNode.parentNode = curNode;
                        openList.Add(neighBorNode);
                    }
                }
            }
        }

        private bool CheckNode(int x, int y)
        {
            if (x >= board.size - 1 || y >= board.size - 1 || x <= 0 || y <= 0) return false;
            else if (board.nodes[x, y].type == NodeType.Fill) return false;
            else if (closedList.Contains(board.nodes[x, y])) return false;

            return true;
        }

        private bool DiagonalCheck(Node curNode, int x, int y)
        {
            if (board.nodes[curNode.x, y].type == NodeType.Fill &&
                board.nodes[x, curNode.y].type == NodeType.Fill)
                return false;

            return true;
        }

        private Node GetPriorityNode(List<Node> nodes)
        {
            Node _node = null;

            var f = int.MaxValue;
            var h = int.MaxValue;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].F <= f && nodes[i].H < h)
                {
                    _node = nodes[i];
                    f = nodes[i].F;
                    h = nodes[i].H;
                }
            }
            return _node;
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
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    nodes[x, y] = new Node();
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
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
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
        public void DrawBoard(Node curNode = null, Node targetNode = null)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (curNode != null || targetNode != null)
                    {
                        if (curNode.x == x && curNode.y == y)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(CIRCLE);
                            continue;
                        }
                        else if (targetNode.x == x && targetNode.y == y)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(CIRCLE);
                            continue;
                        }
                    }
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

    public class Node
    {
        public int x, y;
        public int F { get { return G + H; } }
        public int G { get; private set; } = 0;
        public int H { get; private set; } = 0;

        public Node parentNode { get; set; } = null;
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
