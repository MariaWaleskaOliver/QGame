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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //Here is where I add the btns to the imgList
            lblErroMsg.Text = "";
            btn1.ImageList = imageList;
            btn2.ImageList = imageList;
            btn3.ImageList = imageList;
            btn4.ImageList = imageList;
            btn5.ImageList = imageList;
            btn6.ImageList = imageList;
            //Here is where I add the images indexes to the right buttons

            btn2.ImageIndex = 0;
            btn3.ImageIndex = 1;
            btn4.ImageIndex = 3;
            btn5.ImageIndex = 2;
            btn6.ImageIndex = 4;
        }

        private void saveToFile(string fileName, int rows, int colums)
        {
            string fileContent = $"{rows}\n{colums}\n";
            //Here is  where I check for the image type and save it to the file in a for loop that goes row by row and collum by collum  
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    PictureBox item = _pictureBoxList[colums * i + j];
                    if (item.Text.Contains("none"))
                        fileContent += $"{i}\n{j}\n{0}\n";

                    if (item.Text.Contains("parede"))
                        fileContent += $"{i}\n{j}\n{1}\n";

                    if (item.Text.Contains("RedDoorNew"))
                        fileContent += $"{i}\n{j}\n{2}\n";

                    if (item.Text.Contains("greenDoor"))
                        fileContent += $"{i}\n{j}\n{3}\n";

                    if (item.Text.Contains("Box_Red"))
                        fileContent += $"{i}\n{j}\n{4}\n";

                    if (item.Text.Contains("Green_Box"))
                        fileContent += $"{i}\n{j}\n{5}\n";

                }

            }
            StreamWriter streamWritter = new StreamWriter(fileName);
            streamWritter.WriteLine(fileContent);
            streamWritter.Close();
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            int rows = int.Parse(txtRows.Text), colums = int.Parse(txtColumns.Text);
            DialogResult r = saveFileDialog.ShowDialog();
            switch (r)
            {
                case DialogResult.OK:
                    string filename = saveFileDialog.FileName;
                    saveToFile(filename, rows, colums);
                    //here is where I count the number of boxes, doors and walls and show it the msg box 
                    int numberOfWalls = 0, numberOfBoxes = 0, numberOfDoors = 0;
                    foreach (PictureBox item in _pictureBoxList)
                    {
                        if (item.Text.Contains("parede"))
                            numberOfWalls++;
                        if (item.Text.Contains("RedDoorNew"))
                            numberOfDoors++;
                        if (item.Text.Contains("greenDoor"))
                            numberOfDoors++;
                        if (item.Text.Contains("Box_Red"))
                            numberOfBoxes++;
                        if (item.Text.Contains("Green_Box"))
                            numberOfBoxes++;
                    }
                    MessageBox.Show($"File Saved successfully\n Number of wall:{numberOfWalls} \n Number of Boxes :{numberOfBoxes} \n Number of Doors :{ numberOfDoors}");
                    break;
                case DialogResult.Cancel:
                    break;
                default: break;
            }

        }
        
        //here is a event where I check for the buton index in the image list and add the img and set the text with the name of the image
      
        private void btn1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            _buttonIndex = btn.TabIndex;

        }
        private void closeForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            //Here is where a Generate the pictures boxes 
            lblErroMsg.Text = "";
            try
            {
                int numberOfRows = Convert.ToInt32(txtRows.Text), numberOfCollums = Convert.ToInt32(txtColumns.Text);
                Size diameter = new Size(90, 70);
                for (int i = 0; i < numberOfRows; i++)
                {
                    int currentYsize = b + i * diameter.Height;
                    for (int j = 0; j < numberOfCollums; j++)
                    {
                        int currentXsize = a + j * diameter.Width;
                        PictureBox p = new PictureBox
                        {
                            Name = $"PctBox {numberOfRows + numberOfCollums}",
                            Size = diameter,
                            Location = new Point(currentXsize, currentYsize),
                            BorderStyle = BorderStyle.FixedSingle,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Text = "none" + (numberOfCollums * i + j).ToString()
                        };
                        this.Controls.Add(p);

                        _pictureBoxList.Add(p);
                        p.Click += P_Click1;
                     
                    }
                }
            }
            catch (Exception ex)
            {
                lblErroMsg.Text = "Please input a number!";
            }
        }

        private void P_Click1(object sender, EventArgs e)
        {
           
            switch (_buttonIndex)
            {
                case 1:
                    ((PictureBox)sender).Image = null;
                    ((PictureBox)sender).Text = "none" ;
                    break;
                case 2:
                    ((PictureBox)sender).Image = Properties.Resources.wallNew;
                    ((PictureBox)sender).Text = "parede" + _buttonIndex;

                    break;
                case 3:
                    ((PictureBox)sender).Image = Properties.Resources.redDoor;
                    ((PictureBox)sender).Text = "RedDoorNew" + _buttonIndex;
                    break;
                case 4:
                    ((PictureBox)sender).Image = Properties.Resources.greenDoor;
                    ((PictureBox)sender).Text = "greenDoor" + _buttonIndex;
                    break;
                case 5:
                    ((PictureBox)sender).Image = Properties.Resources.redBoxNew;
                    ((PictureBox)sender).Text = "Box_Red" + _buttonIndex;
                    break;
                case 6:
                    ((PictureBox)sender).Image = Properties.Resources.greenboxnew;
                    ((PictureBox)sender).Text = "Green_Box" + _buttonIndex;
                    break;
            }
        }

        //Here are my privates variables 
        private List<PictureBox> _pictureBoxList = new List<PictureBox>();
        private const int a = 290;
        private const int b = 150;
        private int _buttonIndex = 0;
    }
}
