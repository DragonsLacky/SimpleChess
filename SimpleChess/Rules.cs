using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChess
{
    public partial class Rules : Form
    {
        public Rules()
        {
            InitializeComponent();
        }

        private void Rules_Load(object sender, EventArgs e)
        {
            string victoryCondition = "The goal of the game is to trap the opposing king, untill it cant move anymore";
            string CheckMate = "If you are in a checkmate you are required to either block the piece involved in the checkmate or move the king away if possible";
            string generalExplanation = "White is always first to move and players take turns alternately moving one piece at a time.\n" +
                " Movement is required. If a player´s turn is to move, he is not in check but has no legal moves,\n" +
                " this situation is called “Stalemate” and it ends the game in a draw. Each type of piece has its \n" +
                " own method of movement. A piece may be moved to another position or may capture an opponent´s piece,\n" +
                " replacing on its square (en passant being the only exception). With the exception of the knight,\n" +
                " a piece may not move over or through any of the other pieces. When a king is threatened with capture \n" +
                " (but can protect himself or escape), it´s called check. If a king is in check, then the player must \n" +
                "make a move that eliminates the threat of capture and cannot leave the king in check. Checkmate happens\n" +
                " when a king is placed in check and there is no legal move to escape. Checkmate ends the game and the \n " +
                "side whose king was checkmated looses.";
            label1.Text = victoryCondition;
            label2.Text = CheckMate;
            label3.Text = generalExplanation;
        }
    }
}
