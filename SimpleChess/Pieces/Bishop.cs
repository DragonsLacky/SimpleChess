using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChess.Pieces
{
    public class Bishop : ChessPiece
    {
        public Bishop(char x, int y, ChessColor color, PictureBox piece): base(x, y, color, piece) { Type = PieceType.BISHOP; }

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

        public override List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            List<ChessPosition> validPositions = new List<ChessPosition>();
            char xpos = (char)(Position.X + 1);
            int ypos = Position.Y + 1;
            while (true)
            {
                if (ypos <= 8 && ypos >= 1 && xpos <= 'H' && xpos >= 'A')
                {
                    if (piecePositions[xpos][ypos].ocupied && piecePositions[xpos][ypos].piece.Color != Color)
                    {
                        validPositions.Add(new ChessPosition(xpos, ypos));
                        break;
                    }
                    if (piecePositions[xpos][ypos].ocupied)
                    {
                        break;
                    }
                    validPositions.Add(new ChessPosition(xpos, ypos));
                    ypos += 1;
                    xpos = (char)(xpos + 1);
                }
                else break;
            }
            xpos = (char)(Position.X - 1);
            ypos = Position.Y - 1;
            while (true)
            {
                if (ypos <= 8 && ypos >= 1 && xpos <= 'H' && xpos >= 'A')
                {
                    if (piecePositions[xpos][ypos].ocupied && piecePositions[xpos][ypos].piece.Color != Color)
                    {
                        validPositions.Add(new ChessPosition(xpos, ypos));
                        break;
                    }
                    if (piecePositions[xpos][ypos].ocupied)
                    {
                        break;
                    }
                    validPositions.Add(new ChessPosition(xpos, ypos));
                    ypos -= 1;
                    xpos = (char)(xpos - 1);
                }
                else break;
            }
            xpos = (char)(Position.X - 1);
            ypos = Position.Y + 1;
            while (true)
            {
                if (ypos <= 8 && ypos >= 1 && xpos <= 'H' && xpos >= 'A')
                {
                    if (piecePositions[xpos][ypos].ocupied && piecePositions[xpos][ypos].piece.Color != Color)
                    {
                        validPositions.Add(new ChessPosition(xpos, ypos));
                        break;
                    }
                    if (piecePositions[xpos][ypos].ocupied)
                    {
                        break;
                    }
                    validPositions.Add(new ChessPosition(xpos, ypos));
                    ypos += 1;
                    xpos = (char)(xpos - 1);
                }
                else break;
            }
            xpos = (char)(Position.X + 1);
            ypos = Position.Y - 1;
            while (true)
            {
                if (ypos <= 8 && ypos >= 1 && xpos <= 'H' && xpos >= 'A')
                {
                    if (piecePositions[xpos][ypos].ocupied && piecePositions[xpos][ypos].piece.Color != Color)
                    {
                        validPositions.Add(new ChessPosition(xpos, ypos));
                        break;
                    }
                    if (piecePositions[xpos][ypos].ocupied)
                    {
                        break;
                    }
                    validPositions.Add(new ChessPosition(xpos, ypos));
                    ypos -= 1;
                    xpos = (char)(xpos + 1);
                }
                else break;
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
