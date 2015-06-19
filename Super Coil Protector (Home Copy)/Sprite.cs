using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class Sprite
    {
        public Texture2D spriteTexture;
        public Rectangle spriteRectangle;

        public virtual void draw(SpriteBatch spriteBatch)
        {
        }
    }
}
