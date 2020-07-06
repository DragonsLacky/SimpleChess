using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleChess.Pieces;

namespace SimpleChess
{
    class MovesNode
    {
        public Dictionary<char,Dictionary<int, positionInfo>> Board;
        public List<ChessPiece> White;
        public List<ChessPiece> Black;
        public Moves move { get; set; }
        public List<MovesNode> Children { get; set; }
        public int Value { get; set; }
        public MovesNode(Dictionary<char, Dictionary<int, positionInfo>> board, List<ChessPiece> white, List<ChessPiece> black, Moves moves)
        {
            Board = board;
            White = white;
            Black = black;
            move = moves;
            Value = value();
            Children = new List<MovesNode>();
        }
        private int value()
        {
            int sum = 0;
            foreach (var posinfo in Board.Values)
            {
                foreach (var p in posinfo.Values)
                {
                    if(p.ocupied)
                    {
                        
                        if (p.piece.Type == PieceType.KING)
                        {
                            if (((King)p.piece).Check(White, Black, Board))
                            {

                                sum = p.piece.Color == ChessColor.WHITE ? (sum - 1000) : (sum + 1000);

                            }
                        }
                        foreach (var moves in p.piece.getValidMoves(White, Black, Board))
                        {
                            switch (p.piece.Type)
                            {
                                case PieceType.PAWN: sum = p.piece.Color == ChessColor.BLACK ? (sum + 1) : (sum + 1); break;
                                case PieceType.ROOK: sum = p.piece.Color == ChessColor.BLACK ? (sum - 3) : (sum + 3); break;
                                case PieceType.KNIGHT: sum = p.piece.Color == ChessColor.BLACK ? (sum - 4) : (sum + 4); break;
                                case PieceType.BISHOP: sum = p.piece.Color == ChessColor.BLACK ? (sum - 5) : (sum + 5); break;
                                case PieceType.QUEEN: sum = p.piece.Color == ChessColor.BLACK ? (sum - 10) : (sum + 10); break;
                                case PieceType.KING: sum = p.piece.Color == ChessColor.BLACK ? (sum - 4) : (sum + 4); break;
                            }
                            if (Board[moves.X][moves.Y].ocupied && Board[moves.X][moves.Y].piece.Color != p.piece.Color)
                            {
                                switch (Board[moves.X][moves.Y].piece.Type)
                                {
                                    case PieceType.PAWN: sum = p.piece.Color == ChessColor.BLACK ? (sum - 30) : (sum + 30); break;
                                    case PieceType.ROOK: sum = p.piece.Color == ChessColor.BLACK ? (sum - 50) : (sum + 50); break;
                                    case PieceType.KNIGHT: sum = p.piece.Color == ChessColor.BLACK ? (sum - 55) : (sum + 55); break;
                                    case PieceType.BISHOP: sum = p.piece.Color == ChessColor.BLACK ? (sum - 70) : (sum + 70); break;
                                    case PieceType.QUEEN: sum = p.piece.Color == ChessColor.BLACK ? (sum - 200) : (sum + 200); break;
                                    case PieceType.KING: sum = p.piece.Color == ChessColor.BLACK ? (sum - 300) : (sum + 300); break;
                                }
                                foreach (var oposingPiece in Board[moves.X][moves.Y].piece.getValidMoves(White, Black, Board))
                                {
                                    if(oposingPiece == p.piece.Position)
                                    {
                                        switch (p.piece.Type)
                                        {
                                            case PieceType.PAWN: sum = p.piece.Color == ChessColor.WHITE ? (sum - 20) : (sum + 20); break;
                                            case PieceType.ROOK: sum = p.piece.Color == ChessColor.WHITE ? (sum - 40) : (sum + 40); break;
                                            case PieceType.KNIGHT: sum = p.piece.Color == ChessColor.WHITE ? (sum - 55) : (sum + 55); break;
                                            case PieceType.BISHOP: sum = p.piece.Color == ChessColor.WHITE ? (sum - 70) : (sum + 70); break;
                                            case PieceType.QUEEN: sum = p.piece.Color == ChessColor.WHITE ? (sum - 200) : (sum + 200); break;
                                            case PieceType.KING: sum = p.piece.Color == ChessColor.WHITE ? (sum - 300) : (sum + 300); break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return sum;
        }
    }
    class MovesTree
    {
        public MovesNode Root { get; set; }
        public int AlphaBeta(int depth, bool maximizingPlayer)
        {
            return AlphaBeta(Root, depth, int.MinValue, int.MaxValue, maximizingPlayer);
        }
        public MovesTree(Dictionary<char, Dictionary<int, positionInfo>> board, List<ChessPiece> white, List<ChessPiece> black, Moves move)
        {
            Root = new MovesNode(board, white, black,null);
        }
        public int AlphaBeta(MovesNode node, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            if (depth == 0)
            {
                return node.Value;
            }
            if(maximizingPlayer)
            {
                int Value = int.MinValue;
                foreach (MovesNode child in node.Children)
                {
                    int cmpVal = AlphaBeta(child, depth - 1, alpha, beta, !maximizingPlayer);
                    Value = Math.Max(Value, cmpVal);
                    alpha = Math.Max(alpha, Value);
                    if (alpha >= beta)
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
                    int cmpVal = AlphaBeta(child, depth - 1, alpha, beta, !maximizingPlayer);
                    Value = Math.Min(Value, cmpVal);
                    beta = Math.Min(beta, Value);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return Value;
            }
        }
    }
}
