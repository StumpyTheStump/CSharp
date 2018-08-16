using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CharacterCreator
{
    class SpriteSheet
    {
        public string path;

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
            this.path = path;
            Load();
        }

        public void Load()
        {
            Image = Image.FromFile(path);
        }

        public override string ToString()
        {
            return base.ToString() + ": " + path.ToString();
        }
    }
}
