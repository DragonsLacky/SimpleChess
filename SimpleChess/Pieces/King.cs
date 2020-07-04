using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SimpleChess.Pieces
{
    public class King : ChessPiece
    {
        public King(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { Type = PieceType.KING;}

        public override bool checkValidMove(ChessPosition position, List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            List<ChessPosition> validPositions = getValidMoves(white, black, piecePositions);
            foreach (ChessPosition pos in validPositions)
            {
                if (position == pos)
                {
                    return true;
                }
            }
            return false;
        }


        public override List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            List<ChessPosition> validPositions = new List<ChessPosition>();
            validPositions.Add(new ChessPosition((char)(Position.X + 1), Position.Y + 1));
            validPositions.Add(new ChessPosition((char)(Position.X + 1), Position.Y - 1));
            validPositions.Add(new ChessPosition((char)(Position.X - 1), Position.Y + 1));
            validPositions.Add(new ChessPosition((char)(Position.X - 1), Position.Y - 1));
            validPositions.Add(new ChessPosition(Position.X, Position.Y + 1));
            validPositions.Add(new ChessPosition(Position.X, Position.Y - 1));
            validPositions.Add(new ChessPosition((char)(Position.X + 1), Position.Y));
            validPositions.Add(new ChessPosition((char)(Position.X - 1), Position.Y));

            for (int i = 0; i < validPositions.Count; i++)
            {
                if (validPositions[i].X < 'A' || validPositions[i].X > 'H' || validPositions[i].Y < 1 || validPositions[i].Y > 8)
                {
                    validPositions.RemoveAt(i);
                    i--;
                    continue;
                }

                if(Occupied[validPositions[i].X][validPositions[i].Y].ocupied)
                {
                    validPositions.RemoveAt(i);
                    i--;
                }

            }

            if (this.Color == ChessColor.BLACK)
                foreach (ChessPiece p in white)
                {
                    if (p.Type == PieceType.KING)
                        continue;
                    List<ChessPosition> chessPositions = p.getTakeMoves(white, black, Occupied);
                    foreach (ChessPosition pos in chessPositions)
                    {
                        if (pos == Position)
                            continue;
                        for (int i = 0; i < validPositions.Count; i++)
                        {
                            if (validPositions[i] == pos)
                            {
                                
                                validPositions.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }

            if (this.Color == ChessColor.WHITE)
                foreach (ChessPiece p in black)
                {
                    if (p.Type == PieceType.KING)
                        continue;
                    List<ChessPosition> chessPositions = p.getTakeMoves(white, black, Occupied);
                    foreach (ChessPosition pos in chessPositions)
                    {
                        for (int i = 0; i < validPositions.Count; i++)
                        {
                            if (validPositions[i] == pos)
                            {
                                validPositions.RemoveAt(i);
                            }
                        }
                    }
                }

            return validPositions;
        }

        public override string getType()
        {
            return "King";
        }
        public override bool victoryCondition(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            List<ChessPiece> checkedPieces = getCheckPieces(white, black, Occupied);
            if(CheckMate(white,black,Occupied))
            {
                if(checkedPieces.Count > 1)
                {
                    return true;
                }
                else
                {
                    foreach(ChessPiece piece in checkedPieces)
                    {
                        foreach (ChessPosition pos in getMovesToKing(piece))
                        {
                            foreach(ChessPiece allyPiece in Color == ChessColor.WHITE ? white : black)
                            {
                                foreach (ChessPosition pos1 in allyPiece.getValidMoves(white,black,Occupied))
                                {
                                    if(pos == pos1)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        virtual public bool CheckMate(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> Occupied) 
        {
            List<ChessPosition> availableMoves = getValidMoves(white, black, Occupied);
            if(Check(white, black, Occupied) && availableMoves.Count == 0)
            {
                return true;
            }
            return false;
        }
        public List<ChessPosition> getMovesToKing(ChessPiece piece)
        {
            List<ChessPosition> pieceMoves = new List<ChessPosition>();
            if(piece.Type == PieceType.BISHOP)
            {
                for (int i = piece.Position.X; i <= Position.X ? i < Position.X : i > Position.X; i = i < Position.X ? i + 1 : i - 1)
                {
                    for (int j = piece.Position.Y; j <= Position.Y ? i < Position.Y : i > Position.Y; i = i < Position.Y ? i + 1 : i - 1)
                    {
                        pieceMoves.Add(new ChessPosition((char)i, j));
                    }
                }
            }
            if(piece.Type == PieceType.ROOK)
            {
                if (Position.X == piece.Position.X)
                for (int i = piece.Position.Y; i <= Position.Y ? i < Position.Y : i > Position.Y; i = i < Position.Y ? i + 1 : i - 1)
                {
                        pieceMoves.Add(new ChessPosition(Position.X, i));
                }
                if (Position.Y == piece.Position.Y)
                for (int i = piece.Position.X; i <= Position.X ? i < Position.X : i > Position.X; i = i < Position.X ? i + 1 : i - 1)
                {
                        pieceMoves.Add(new ChessPosition((char)i, Position.Y));

                }
            }
            if(piece.Type == PieceType.KNIGHT || piece.Type == PieceType.PAWN)
            {
                pieceMoves.Add(new ChessPosition(piece.Position.X, piece.Position.Y));
            }
            if(piece.Type == PieceType.QUEEN)
            {
                if (Position.X == piece.Position.X)
                    for (int i = piece.Position.Y; i <= Position.Y ? i < Position.Y : i > Position.Y; i = i < Position.Y ? i + 1 : i - 1)
                    {
                        pieceMoves.Add(new ChessPosition(Position.X, i));
                    }
                if (Position.Y == piece.Position.Y)
                    for (int i = piece.Position.X; i <= Position.X ? i < Position.X : i > Position.X; i = i < Position.X ? i + 1 : i - 1)
                    {
                        pieceMoves.Add(new ChessPosition((char)i, Position.Y));

                    }
                for (int i = piece.Position.X; i <= Position.X ? i < Position.X : i > Position.X; i = i < Position.X ? i + 1 : i - 1)
                {
                    for (int j = piece.Position.Y; j <= Position.Y ? i < Position.Y : i > Position.Y; i = i < Position.Y ? i + 1 : i - 1)
                    {
                        pieceMoves.Add(new ChessPosition((char)i, j));
                    }
                }

            }
            return pieceMoves;
        }
        public List<ChessPiece> getCheckPieces(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            List<ChessPiece> CheckPieces = new List<ChessPiece>();
            if (this.Color == ChessColor.BLACK)
                foreach (ChessPiece p in white)
                {
                    if (p.Type == PieceType.KING)
                        continue;
                    foreach (ChessPosition pos in p.getTakeMoves(white, black, Occupied))
                    {
                        if (pos == Position)
                        {
                            CheckPieces.Add(p);
                        }

                    }
                }

            if (this.Color == ChessColor.WHITE)
                foreach (ChessPiece p in black)
                {
                    if (p.Type == PieceType.KING)
                        continue;
                    List<ChessPosition> chessPositions = p.getTakeMoves(white, black, Occupied);
                    foreach (ChessPosition pos in chessPositions)
                    {
                        if (pos == Position)
                        {
                            CheckPieces.Add(p);
                        }
                    }
                }
            return CheckPieces;
        }
        public bool Check(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            foreach (ChessPiece p in Color == ChessColor.BLACK ? white : black)
            {
                if (p.Type == PieceType.KING)
                    continue;
                List<ChessPosition> chessPositions = p.getTakeMoves(white, black, Occupied);
                foreach (ChessPosition pos in chessPositions)
                {
                    if (pos == Position)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        public List<ChessPosition>  StopCheckMate(ChessPiece piece, List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            List<ChessPosition> validPositions = new List<ChessPosition>();
            if (this.Color == ChessColor.BLACK)
                foreach (ChessPiece p in white)
                {
                    if (p.Type == PieceType.KING)
                        continue;
                    List<ChessPosition> chessPositions = p.getTakeMoves(white, black, Occupied);
                    foreach (ChessPosition pos in chessPositions)
                    {
                        if(pos == Position)
                        {
                            foreach(ChessPosition position in piece.getTakeMoves(white, black, Occupied))
                            {
                                if(p.Position == position)
                                {
                                    validPositions.Add(position);
                                    continue;
                                }
                                else if (position == pos)
                                {
                                    validPositions.Add(position);
                                }
                            }
                        }
                        
                    }
                }

            if (this.Color == ChessColor.WHITE)
                foreach (ChessPiece p in black)
                {
                    if (p.Type == PieceType.KING)
                        continue;
                    List<ChessPosition> chessPositions = p.getTakeMoves(white, black, Occupied);
                    foreach (ChessPosition pos in chessPositions)
                    {
                        if (pos == Position)
                        {
                            foreach (ChessPosition position in piece.getTakeMoves(white, black, Occupied))
                            {
                                if (p.Position == position)
                                {
                                    validPositions.Add(position);
                                    continue;
                                }
                                else if (position == pos)
                                {
                                    validPositions.Add(position);
                                }
                            }
                        }
                    }
                }
            return validPositions;
        }
        public override void MovePiece(char x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}
