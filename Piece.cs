using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // important to extract label class for location,size,backcolor
namespace Snake_Game
{
    internal class Piece:PictureBox // snake >>>>>>>>>>>>>>>>
    {

        public Piece(int x, int y)
        {
            Location = new Point(x, y);
            Size = new Size(21, 21);
            BackColor = Color.Red;
            Enabled = false;
            Image = Properties.Resources.Head;
            BackgroundImageLayout = ImageLayout.Tile;
        }
    }
}
