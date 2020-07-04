using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChess
{
    class MovesNode
    {
        public Dictionary<char,Dictionary<int, positionInfo>> Board;
        public List<ChessPiece> White;
        public List<ChessPiece> Black;
        public List<MovesNode> Children;
        public MovesNode(Dictionary<char, Dictionary<int, positionInfo>> board, List<ChessPiece> white, List<ChessPiece> black)
        {
            Board = board;
            White = white;
            Black = black;
        }
        public int ToInt()
        {
            return 0;
        }
    }
    class MovesTree
    {
        public MovesNode Root;
        public int AlphaBeta(int depth, bool maximizingPlayer)
        {
            return AlphaBeta(Root, depth, int.MinValue, int.MaxValue, maximizingPlayer);
        }
        public MovesTree(Dictionary<char, Dictionary<int, positionInfo>> board, List<ChessPiece> white, List<ChessPiece> black)
        {
            Root = new MovesNode(board, white, black);
        }
        private int AlphaBeta(MovesNode node, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            if (depth == 0)
                return node.ToInt();
            if(maximizingPlayer)
            {
                int Value = int.MinValue;
                foreach (MovesNode child in node.Children)
                {
                    Value = Math.Max(Value, AlphaBeta(child, depth - 1, alpha, beta, false));
                    alpha = Math.Max(alpha, Value);
                    if(alpha >= beta)
                    {
                        break;
                    }
                    
                }
                return Value;
            }
            else
            {
                int Value = int.MaxValue;
                foreach (MovesNode child in node.Children)
                {
                    Value = Math.Min(Value, AlphaBeta(child, depth - 1, alpha, beta, true));
                    beta = Math.Min(beta, Value);
                    if(beta <= alpha)
                    {
                        break;
                    }
                }
                return Value;
            }
        }
    }
}
