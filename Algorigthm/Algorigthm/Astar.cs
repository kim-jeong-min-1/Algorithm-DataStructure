using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorigthm
{
    class Astar 
    {

    }

    class Program
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


        }
    }
}
