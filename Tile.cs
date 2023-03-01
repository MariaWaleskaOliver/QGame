using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOliveiraQGame
{
    public class Tile : PictureBox
    {
        public Tile(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int row { get; set; }
        public int col { get; set; }
    }
}
