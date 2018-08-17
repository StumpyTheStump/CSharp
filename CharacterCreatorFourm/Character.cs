using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CharacterCreatorFourm
{
    class Character
    {
        public string name;

        public SpriteSheet spriteSheet;

        public Point tileCoordinates = new Point(0, 0);

        public Character(string name, SpriteSheet spriteSheet)
        {
            this.name = name;
            this.spriteSheet = spriteSheet;
        }

        public override string ToString()
        {
            return base.ToString() + "\n\tpath: \t" + spriteSheet.Path + "\n\tname: \t" + name.ToString() + "\n\ttile coordinates: \t" + tileCoordinates.ToString();
        }
    }
}
