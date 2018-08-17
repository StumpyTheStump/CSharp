using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CharacterCreatorFourm
{
    public partial class SpriteSheetForm : Form
    {

        public SpriteSheet SpriteSheet { get; private set; }
        Bitmap drawArea;

        public Point CurrentTile { get; private set; } = new Point();

        public string Filename
        {
            get { return (SpriteSheet != null) ? SpriteSheet.Filename : string.Empty; }
        }

        int gridWidth = 16;
        int gridHeight = 16;
        int spacing = 0;

        public SpriteSheetForm()
        {
            InitializeComponent();

            drawArea = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if(dlg.CheckFileExists == true)
                {
                    SpriteSheet = new SpriteSheet(dlg.FileName);
                    drawGrid();
                }
            }
        }

        private void drawGrid()
        {
            pictureBox1.DrawToBitmap(drawArea, pictureBox1.Bounds);

            Graphics g;
            g = Graphics.FromImage(drawArea);

            g.Clear(Color.White);

            if (SpriteSheet == null)
            {
                return;
            }

            g.DrawImage(SpriteSheet.Image, 0, 0);

            Pen pen = new Pen(Brushes.Black);

            int height = pictureBox1.Height;
            int width = pictureBox1.Width;
            for (int y = 0; y < height; y += SpriteSheet.GridHeight + SpriteSheet.Spacing)
            {
                g.DrawLine(pen, 0, y, width, y);
            }

            for (int x = 0; x < width; x += SpriteSheet.GridWidth + SpriteSheet.Spacing)
            {
                g.DrawLine(pen, x, 0, x, height);
            }

            Pen highlight = new Pen(Brushes.Red);
            g.DrawRectangle(highlight, CurrentTile.X * (SpriteSheet.GridWidth + SpriteSheet.Spacing), 
                                       CurrentTile.Y * (SpriteSheet.GridHeight + SpriteSheet.Spacing),
                                       SpriteSheet.GridWidth + SpriteSheet.Spacing, 
                                       SpriteSheet.GridHeight + SpriteSheet.Spacing);

            g.Dispose();

            pictureBox1.Image = drawArea;
        }

        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(textBoxWidth.Text, out width) == true)
            {
                SpriteSheet.GridHeight = width;
                drawGrid();
            }

            textBoxWidth.Text = SpriteSheet.GridWidth.ToString();

        }

        private void textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if(int.TryParse(textBoxHeight.Text, out height) == true)
            {
                SpriteSheet.GridHeight = height;
                drawGrid();
            }

            textBoxHeight.Text = SpriteSheet.GridHeight.ToString();

        }

        private void textBoxSpacing_TextChanged(object sender, EventArgs e)
        {
            int spacing;
            if(int.TryParse(textBoxSpacing.Text, out spacing) == true)
            {
                SpriteSheet.Spacing = spacing;
                drawGrid();
            }

            textBoxSpacing.Text = SpriteSheet.Spacing.ToString();
        }

        
        private void SpriteSheetForm_Shown(object sender, EventArgs e)
        {
            drawGrid();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (SpriteSheet == null)
                return;

            if(e.GetType() == typeof(MouseEventArgs))
            {
                MouseEventArgs mouse = e as MouseEventArgs;
                CurrentTile = new Point( mouse.X / (SpriteSheet.GridWidth + SpriteSheet.Spacing), mouse.Y / (SpriteSheet.GridHeight + SpriteSheet.Spacing));
                drawGrid();
            }
        }
    }
}
