using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOliveiraQGame
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            btnDown.Enabled = false; btnUp.Enabled = false; btnLeft.Enabled = false; btnRight.Enabled = false;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //Here I sent the user to the desktop 
            try
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            catch
            {
                ofd.InitialDirectory = Environment.CurrentDirectory;
            }
            //Here i deal with the file opening 
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            // Here a initiaze the components and clear everything in case the user wants to open another game                         

            numOfBoxes = 0;
            isPctBoxSelected = false;
            numberOfMov = 0;
            txtNumberOfMoves.Text = "0";
            currentTile = null;
            panel1.Controls.Clear();
         

            //Here is where I read the file                    
            using (StreamReader streamReader1 = new StreamReader(ofd.OpenFile()))
            {

                while (!streamReader1.EndOfStream)
                {
                    string next = streamReader1.ReadLine();
                    if (!String.IsNullOrEmpty(next))
                    {
                        loadedFile.Add(next);
                    }
                }
                // Here is where I ge the row and column  the I put into a var and remove it from the list 
                row = Convert.ToInt32(loadedFile[0]);
                column = Convert.ToInt32(loadedFile[1]);
                loadedFile.RemoveAt(1);
                loadedFile.RemoveAt(0);
                //Here I create a list of tiles and I declare mt var
                int currentRow = 0;
                int currentColumn = 0;
                _tiles.Clear();
                //List<Tile> _tiles = new List<Tile>();
                Image currentImage = null;
                //Here I go adding the tiles and set the images, adding a tah so i can know the img type and adding 
                //and event to each pct box, in the end I remove the row, colum and img type
               
                while (loadedFile.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0)
                        {
                            currentRow = Convert.ToInt32(loadedFile[i]);
                        }
                        else if (i == 1)
                        {
                            currentColumn = Convert.ToInt32(loadedFile[i]);
                        }
                        else if (i == 2)
                        {
                            switch (loadedFile[i])
                            {
                                case "0":
                                    currentImage = null;
                                    imgType = "None";
                                    break;
                                case "1":
                                    currentImage = Properties.Resources.wallNew;
                                    imgType = "Wall";
                                    break;
                                case "2":
                                    currentImage = Properties.Resources.redDoor;
                                    imgType = "RedDoor";
                                    break;
                                case "3":
                                    currentImage = Properties.Resources.greenDoor;
                                    imgType = "GreenDoor";
                                    break;
                                case "4":
                                    currentImage = Properties.Resources.redBoxNew;
                                    imgType = "RedBox";
                                    numOfBoxes++;
                                    break;
                                case "5":
                                    currentImage = Properties.Resources.greenboxnew;
                                    imgType = "GreenBox";
                                    numOfBoxes++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }                   
                    currentTile = new Tile(currentRow, currentColumn);
                    currentTile.Image = currentImage;
                    currentTile.Tag = imgType;
                    _tiles.Add(currentTile);                    
                    currentTile.Click += CurrentTile_Click;
                    
                    loadedFile.RemoveAt(2);
                    loadedFile.RemoveAt(1);
                    loadedFile.RemoveAt(0);
                }
                //Here I create the pct Boxes
                Size diameter = new Size(90, 70);
                for (int i = 0; i < row; i++)
                {
                    int currentYsize = b + i * diameter.Height;
                    for (int j = 0; j < column; j++)
                    {
                        int currentXsize = a + j * diameter.Width;

                        _tiles[column * i + j].Name = $"{row}{column}";
                        _tiles[column * i + j].Size = diameter;
                        _tiles[column * i + j].Location = new Point(currentXsize, currentYsize);
                        _tiles[column * i + j].BorderStyle = BorderStyle.FixedSingle;
                        _tiles[column * i + j].SizeMode = PictureBoxSizeMode.StretchImage;

                        panel1.Controls.Add(_tiles[column * i + j]);
                    }
                }
            }
            //Here I initialize the # of boxes abd anable the control 
            txtNumberOfRmainingBoxes.Text = Convert.ToString(numOfBoxes);
            btnDown.Enabled = true; btnUp.Enabled = true; btnLeft.Enabled = true; btnRight.Enabled = true;

        }

        private void CurrentTile_Click(object sender, EventArgs e)
        {
            currentTile = (Tile)sender;
            isPctBoxSelected = true;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //firts i check if the control is selected 
            if (!isPctBoxSelected == true)
            {
                MessageBox.Show("Please Choose a image!");
                return;
            }
            ////For all controles I check the next move if it is a empty spot
            ////if so the current place gets null and th tag none then 
            ///I send th image to the next spt and also set the tag with its type



            // code for greenBoxes 
            if (currentTile.Tag == "GreenBox")
            {
                
                if ((string)_tiles[(currentTile.row - 1) * column + currentTile.col].Tag == "None")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    _tiles[(currentTile.row - 1) * column + currentTile.col].Image = Properties.Resources.greenboxnew;
                    _tiles[(currentTile.row - 1) * column + currentTile.col].Tag = "GreenBox";

                }
                else if ((string)_tiles[(currentTile.row - 1) * column + currentTile.col].Tag == "GreenDoor")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    numOfBoxes--;
                }
                else
                {
                    return;
                }
            }
            // code for redBox 
            if (currentTile.Tag == "RedBox")
            {
               
                if ((string)_tiles[(currentTile.row - 1) * column + currentTile.col].Tag == "None")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    _tiles[(currentTile.row - 1) * column + currentTile.col].Image = Properties.Resources.redBoxNew;
                    _tiles[(currentTile.row - 1) * column + currentTile.col].Tag = "RedBox";
                }
                else if ((string)_tiles[(currentTile.row - 1) * column + currentTile.col].Tag == "RedDoor")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    numOfBoxes--;
                }
                else
                {
                    return;
                }
            }
            // Here I update the var states
            isPctBoxSelected = false;
            txtNumberOfRmainingBoxes.Text = Convert.ToString(numOfBoxes);
            numberOfMov++;
            txtNumberOfMoves.Text = Convert.ToString(numberOfMov);
            if (numOfBoxes == 0)
            {
                SetMessage();
            }

        }

        public void SetMessage()
        {
            MessageBox.Show("Congratulation Game Ended");
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (!isPctBoxSelected == true)
            {
                MessageBox.Show("Please Choose a image!");
                return;
            }
            if (currentTile.Tag == "GreenBox")
            {
                if ((string)_tiles[(currentTile.row + 1) * column + currentTile.col].Tag == "None")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    _tiles[(currentTile.row + 1) * column + currentTile.col].Image = Properties.Resources.greenboxnew;
                    _tiles[(currentTile.row + 1) * column + currentTile.col].Tag = "GreenBox";
                }
                else if ((string)_tiles[(currentTile.row + 1) * column + currentTile.col].Tag == "GreenDoor")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    numOfBoxes--;
                }
                else
                {
                    return;
                }
            }
            if (currentTile.Tag == "RedBox")
            {
                if ((string)_tiles[(currentTile.row + 1) * column + currentTile.col].Tag == "None")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    _tiles[(currentTile.row + 1) * column + currentTile.col].Image = Properties.Resources.redBoxNew;
                    _tiles[(currentTile.row + 1) * column + currentTile.col].Tag = "RedBox";
                }
                else if ((string)_tiles[(currentTile.row + 1) * column + currentTile.col].Tag == "RedDoor")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    numOfBoxes--;
                }
                else
                {
                    return;
                }
            }
            isPctBoxSelected = false;
            txtNumberOfRmainingBoxes.Text = Convert.ToString(numOfBoxes);
            numberOfMov++;
            txtNumberOfMoves.Text = Convert.ToString(numberOfMov);
            if (numOfBoxes == 0)
            {
                SetMessage();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (!isPctBoxSelected == true)
            {
                MessageBox.Show("Please Choose a image!");
                return;
            }

            if (currentTile.Tag == "GreenBox")
            {
                if ((string)_tiles[(currentTile.row * column) + currentTile.col - 1].Tag == "None")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    _tiles[(currentTile.row * column) + currentTile.col - 1].Image = Properties.Resources.greenboxnew;
                    _tiles[(currentTile.row * column) + currentTile.col - 1].Tag = "GreenBox";

                }
                else if ((string)_tiles[(currentTile.row * column) + currentTile.col - 1].Tag == "GreenDoor")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    numOfBoxes--;
                }
                else
                {
                    return;
                }

            }
            if (currentTile.Tag == "RedBox")
            {
                if ((string)_tiles[(currentTile.row * column) + currentTile.col - 1].Tag == "None")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    _tiles[(currentTile.row * column) + currentTile.col - 1].Image = Properties.Resources.redBoxNew;
                    _tiles[(currentTile.row * column) + currentTile.col - 1].Tag = "RedBox";

                }
                else if ((string)_tiles[(currentTile.row * column) + currentTile.col - 1].Tag == "RedDoor")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    numOfBoxes--;
                }
                else
                {
                    return;
                }
            }
            isPctBoxSelected = false;
            txtNumberOfRmainingBoxes.Text = Convert.ToString(numOfBoxes);
            numberOfMov++;
            txtNumberOfMoves.Text = Convert.ToString(numberOfMov);
            if (numOfBoxes == 0)
            {
                SetMessage();
            }

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (!isPctBoxSelected == true)
            {
                MessageBox.Show("Please Choose a image!");
                return;
            }
            if (currentTile.Tag == "GreenBox")
            {
                if ((string)_tiles[(currentTile.row * column) + currentTile.col + 1].Tag == "None")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    _tiles[(currentTile.row * column) + currentTile.col + 1].Image = Properties.Resources.greenboxnew;
                    _tiles[(currentTile.row * column) + currentTile.col + 1].Tag = "GreenBox";
                }
                else if ((string)_tiles[(currentTile.row * column) + currentTile.col + 1].Tag == "GreenDoor")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    numOfBoxes--;
                }
                else
                {
                    return;
                }
            }
            if (currentTile.Tag == "RedBox")
            {
                if ((string)_tiles[(currentTile.row * column) + currentTile.col + 1].Tag == "None")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    _tiles[(currentTile.row * column) + currentTile.col + 1].Image = Properties.Resources.redBoxNew;
                    _tiles[(currentTile.row * column) + currentTile.col + 1].Tag = "RedBox";
                }
                else if ((string)_tiles[(currentTile.row * column) + currentTile.col + 1].Tag == "RedDoor")
                {
                    currentTile.Image = null;
                    currentTile.Tag = "None";
                    numOfBoxes--;
                }
                else
                {
                    return;
                }
            }
            isPctBoxSelected = false;
            txtNumberOfRmainingBoxes.Text = Convert.ToString(numOfBoxes);
            numberOfMov++;
            txtNumberOfMoves.Text = Convert.ToString(numberOfMov);
            if (numOfBoxes == 0)
            {
                SetMessage();
            }
        }

        //My private Variables
        Tile currentTile;
        private List<Tile> _tiles = new List<Tile>();
       // private Tile tile;
        private const int a = 29;
        private const int b = 15;
        private int numOfBoxes = 0;
        private string imgType = "";
        private int row = 0, column = 0;
        private bool isPctBoxSelected = false;
        private int numberOfMov = 0;

        private List<string> loadedFile = new List<string>();
    }
}
