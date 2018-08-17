using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CharacterCreatorFourm
{
    public class SpriteSheet
    {
        public string Path { get; private set; }

        public int GridWidth { get; set; } = 16;
        public int GridHeight { get; set; } = 16;
        public int Spacing { get; set; } = 1;

        public string Filename
        {
            get { return Path.Substring(Path.LastIndexOf('\\')); }
        }

        public Image Image { get; private set; } = null;

        public int GetWidth()
        {
            return 1;
        }

        public int GetHeight()
        {
            return 1;
        }

        public SpriteSheet(string path)
        {
            Path = path;
            Load();
        }

        public void Load()
        {
            Image = Image.FromFile(Path);
        }

        public override string ToString()
        {
            return Filename;
        }
    }
}
