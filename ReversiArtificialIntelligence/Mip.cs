using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence
{//1
    /// <summary>
    /// This player calculates the move that guarentees the most allied discs on the next turn
    /// </summary>
    public class Mip : IReversiPlayer
    {//2
        private const int MINIMAX_DEPTH = 4;
        public Point NextMove(Disc[,] board, Disc playerColor)
        {//3
            Point bestMove= new Point(0,0);
            int bestScore = int.MinValue;
            Point DRCorner = new Point(7, 7);
            bool isDRfull =false;
            Point URCorner = new Point(7, 0);
            bool isURfull =false;
            Point DLCorner = new Point(0, 7);
            bool isDLfull =false;
            Point ULCorner = new Point(0, 0);
            bool isULfull =false;
            bool shouldDoDumm = true;

            if (ReversiGame.IsValidMove(board,DRCorner,playerColor))
            {//
                bestMove = DRCorner;
                shouldDoDumm = false;
                isDRfull = true;
            }//
            else if (ReversiGame.IsValidMove(board, URCorner, playerColor))
            {//
                bestMove = URCorner;
                shouldDoDumm = false;
                isURfull = true;
            }//
            else if (ReversiGame.IsValidMove(board, DLCorner, playerColor))
            {//
                bestMove = DLCorner;
                shouldDoDumm = false;
                isDLfull = true;
            }//
            else if (ReversiGame.IsValidMove(board, ULCorner, playerColor))
            {//
                bestMove = ULCorner;
                shouldDoDumm = false;
                isULfull = true;
            }//
            else
             {//3.5
                 if (true)
                 {//3.55
                     Point Side = new Point(0, 0);
                     bool shouldBreak = false;
                     int other = 0;
                     for (int i = 0; i < 4; i++)
                     {
                         if ((i == 0)||(i==2))
                         {
                             other = 0;  
                         }
                         else if((i==1)||(i==3))
                         {
                             other = 7;
                         }
                         for (int pm = 0; pm < 8; pm++)
                         {
                             if ((i == 1) || (i == 0))
                             {
                                 Side = new Point(pm, other);
                             }
                             else
                             {
                                 Side = new Point(other, pm);
                             }
                             if (ReversiGame.IsValidMove(board, Side, playerColor))
                             {
                                 bestMove = Side;
                                 shouldBreak = true;
                                 break;
                             }
                         }
                         if (shouldBreak)
                         {
                             break;
                         }
                     }
                 }//3.55
                 else
                 {//3.555
                     shouldDoDumm = true;
                 }//3.555 
             }//3.5
                if(shouldDoDumm)
                {//4
                   return Minimax(board, playerColor, MINIMAX_DEPTH).Item2;

                    /// <summary>
                    /// The minimax algorithm
                    /// </summary>
                    /// <param name="board">Target board</param>
                    /// <param name="playerColor">Current player</param>
                    /// <param name="maxDepth">Maximim recursion depth</param>
                    /// <returns>The score and point of the best minimax play</returns>
                }//4
            return bestMove;
            }//3

        private Tuple<int, Point> Minimax(Disc[,] board, Disc playerColor, int maxDepth)
        {//2.5
            if (maxDepth == 0)
                return new Tuple<int, Point>(ReversiGame.Score(board, playerColor), null);
            Point bestMove = null;
            int bestScore = int.MinValue;
            foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
            {
                int score = -Minimax(
                    ReversiGame.PlayTurn(board, p, playerColor),
                    playerColor.Reversed(),
                    maxDepth - 1).Item1;
                if (score > bestScore)
                {
                    bestMove = p;
                    bestScore = score;
                }
            }
            if (bestMove == null)
                bestScore = ReversiGame.Score(board, playerColor);
            return new Tuple<int, Point>(bestScore, bestMove);
        }//2.5

        }//2
}//1
