using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChess
{
    class Computer
    {
        public  Dictionary<char,Dictionary<int, positionInfo>> Positions { get; set; }
        public Control Controls { get; set; }
        public Dictionary<PictureBox, ChessPosition> PositionOnBoard { get; set; }
        public Dictionary<PictureBox, ChessPiece> BoardPieces { get; set; }
        public List<ChessPiece> MyPieces { get; set; }
        public List<ChessPiece> EnemyPieces { get; set; }
        public Computer(Dictionary<char, Dictionary<int, positionInfo>> positions, Control controls, Dictionary<PictureBox, ChessPosition> positionOnBoard, Dictionary<PictureBox, ChessPiece> boardPieces, List<ChessPiece> myPieces, List<ChessPiece> enemyPieces)
        {
            Positions = positions;
            Controls = controls;
            PositionOnBoard = positionOnBoard;
            BoardPieces = boardPieces;
            MyPieces = myPieces;
            EnemyPieces = enemyPieces;
        }
        
        public List<ChessPosition> getCurrentBoardValue()
        {
            List<ChessPosition> positions = new List<ChessPosition>();
            
            return positions;
        }
    }
}
