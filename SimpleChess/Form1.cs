using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleChess.Pieces;

namespace SimpleChess
{
    enum PieceType { KING, QUEEN, PAWN, BISHOP, ROOK, KNIGHT }
    public partial class Form1 : Form
    {
        Dictionary<char, Dictionary<int, PictureBox>> Board;
        Dictionary<PieceType, Dictionary<Color, Image>> PieceImages;
        
        public Form1()
        {
            InitializeComponent();
            Board = new Dictionary<char, Dictionary<int, PictureBox>>();
            PieceImages = new Dictionary<PieceType, Dictionary<Color, Image>>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadImages();
            DrawBoard();
        }

        private void loadImages()
        {
            List<PieceType> types = new List<PieceType>();
            types.Add(PieceType.KING);
            types.Add(PieceType.QUEEN);
            types.Add(PieceType.BISHOP);
            types.Add(PieceType.KNIGHT);
            types.Add(PieceType.ROOK);
            types.Add(PieceType.PAWN);

            foreach (PieceType type in types)
            {
                PieceImages.Add(type, new Dictionary<Color, Image>());
                PieceImages[type].Add(Color.White, null);
                PieceImages[type].Add(Color.Black, null);
            }

            PieceImages[PieceType.KING][Color.White] = Image.FromFile(@"..\..\Images\White_King.png");
            PieceImages[PieceType.KING][Color.Black] = Image.FromFile(@"..\..\Images\Black_King.png");
            PieceImages[PieceType.QUEEN][Color.White] = Image.FromFile(@"..\..\Images\White_Queen.png");
            PieceImages[PieceType.QUEEN][Color.Black] = Image.FromFile(@"..\..\Images\Black_Queen.png");
            PieceImages[PieceType.BISHOP][Color.White] = Image.FromFile(@"..\..\Images\White_Bishop.png");
            PieceImages[PieceType.BISHOP][Color.Black] = Image.FromFile(@"..\..\Images\Black_Bishop.png");
            PieceImages[PieceType.KNIGHT][Color.White] = Image.FromFile(@"..\..\Images\White_Knight.png");
            PieceImages[PieceType.KNIGHT][Color.Black] = Image.FromFile(@"..\..\Images\Black_Knight.png");
            PieceImages[PieceType.ROOK][Color.White] = Image.FromFile(@"..\..\Images\White_Rook.png");
            PieceImages[PieceType.ROOK][Color.Black] = Image.FromFile(@"..\..\Images\Black_Rook.png");
            PieceImages[PieceType.PAWN][Color.White] = Image.FromFile(@"..\..\Images\White_Pawn.png");
            PieceImages[PieceType.PAWN][Color.Black] = Image.FromFile(@"..\..\Images\Black_Pawn.png");

        }



        public void DrawBoard()
        {
            int nextPosX = 40;
            int nextPosY = 80;
            for (int i = 8; i >= 1; i--)
            {
                for (int j = 'A'; j <= 'H'; j++)
                {
                    if(!Board.ContainsKey((char)j))
                    {
                        Board.Add((char)j, new Dictionary<int, PictureBox>());
                    }
                    Board[(char)j].Add(i, new PictureBox { Location = new Point(nextPosX, nextPosY), Size = new Size(75, 75) });
                    Board[(char)j][i].BorderStyle = BorderStyle.Fixed3D;
                    if (j % 2 != 0)
                    {
                        if(i % 2 != 0)
                        {
                            Board[(char)j][i].BackColor = Color.White;
                        }
                        else
                        {
                            Board[(char)j][i].BackColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (i % 2 != 0)
                        {
                            Board[(char)j][i].BackColor = Color.Black;
                        }
                        else
                        {
                            Board[(char)j][i].BackColor = Color.White;
                        }
                    }
                    Controls.Add(Board[(char)j][i]);
                    nextPosX += 75;
                }
                nextPosX = 40;
                nextPosY += 75;
            }
            //board.Add((char)('A'+1),new Dictionary<int, PictureBox>());
            //board['B'].Add(1, new PictureBox { Location = new Point(40, 300), Size = new Size(75, 75) });
            //Controls.Add(board['B'][1]);
            //Controls.Add(chesspiece);
            //board['B'][1].BackColor = Color.White;
            //board['B'][1].BorderStyle = BorderStyle.Fixed3D;
            
            //board['B'][1].BackColor = ;
        }
    }
}
