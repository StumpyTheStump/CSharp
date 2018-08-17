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
    public partial class SpriteForm : Form
    {
        SpriteSheet SpriteSheet = null;
        Bitmap drawArea = null;

        List<Layer> layers = new List<Layer>();
        public SpriteForm()
        {
            InitializeComponent();
            drawArea = new Bitmap(pictureBox.Width, pictureBox.Height);
        }

        private void SpriteForm_Activated(object sender, EventArgs e)
        {
            MdiClient parent = Parent as MdiClient;
            if (parent != null)
            {
                foreach (Form child in parent.MdiChildren)
                {
                    if (child.GetType() == typeof(SpriteSheetForm))
                    {
                        SpriteSheetForm sheet = child as SpriteSheetForm;
                        SpriteSheet ss = sheet.SpriteSheet;
                        if (ss != null && !comboBox1.Items.Contains(ss))
                        {
                            comboBox1.Items.Add(ss);
                        }
                    }
                }
            }

            if (SpriteSheet != null)
            {
                comboBox1.SelectedItem = SpriteSheet;
            }
            else if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                SpriteSheet = comboBox1.SelectedItem as SpriteSheet;
            }
        }

        SpriteSheetForm FindSheet()
        {
            MdiClient parent = Parent as MdiClient;
            if(parent != null)
            {
                foreach (Form child in parent.MdiChildren)
                {
                    if(child.GetType() == typeof(SpriteSheetForm))
                        {
                        SpriteSheetForm sheet = child as SpriteSheetForm;
                        if (sheet.SpriteSheet == SpriteSheet)
                            return sheet;
                        }
                }
            }
            return null;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if(SpriteSheet !=null)
            {
                SpriteSheetForm sheet = FindSheet();
                {
                    if (sheet!= null)
                    {
                        Layer layer = new Layer("Unnamed Layer");
                        layer.TileCoordinates = sheet.CurrentTile;
                        layer.Priority = layers.Count + 1;

                        layers.Add(layer);

                        listView1.Items.Add(layer.GetListViewItem());

                        DrawCharacter();
                    }

                }
            }
        }

        private void DrawCharacter()
        {
            Graphics g = Graphics.FromImage(drawArea);
            g.FillRectangle(Brushes.White, 0, 0, drawArea.Width, drawArea.Height);

            Rectangle dest = new Rectangle(0, 0, SpriteSheet.GridWidth << 2, SpriteSheet.GridHeight << 2);

            foreach (Layer layer in layers)
            {
                Rectangle source = new Rectangle(layer.TileCoordinates.X * (SpriteSheet.GridWidth + SpriteSheet.Spacing), layer.TileCoordinates.Y * (SpriteSheet.GridHeight + SpriteSheet.Spacing), SpriteSheet.GridWidth, SpriteSheet.GridHeight);

                g.DrawImage(SpriteSheet.Image, dest, source, GraphicsUnit.Pixel);
            }
            g.Dispose();

            pictureBox.Image = drawArea;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indicies = listView1.SelectedIndices;
            if (indicies.Count <= 0)
                return;
            // Remove the Selected Layer from the layers list
            layers.RemoveAt(indicies[0]);

            // delete and rebuild the list view (with updated priority values)
            listView1.Items.Clear();

            // Renumber Layers
            for (int i = 0; i < layers.Count; ++i)
            {
                layers[i].Priority = i + 1;
                listView1.Items.Add(layers[i].GetListViewItem());
            }

            DrawCharacter();
            
        }

        private void comboBoxSheets_SelectedValueChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            layers.Clear();
            SpriteSheet = comboBox1.SelectedItem as SpriteSheet;

            // Clear the Image
            Graphics g = Graphics.FromImage(drawArea);
            g.FillRectangle(Brushes.White, 0, 0, drawArea.Width, drawArea.Height);
            g.Dispose();
            pictureBox.Image = drawArea;

        }

        private void listViewTiles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            int index = e.Item;
            layers[index].Name = e.Label;
        }
    }
}
