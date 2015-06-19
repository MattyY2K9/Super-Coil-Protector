using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class SpritesWithHealth : Sprite
    {
        protected Texture2D healthTexture;

        public int health;
        protected int maxHealth;
        public Vector2 velocity;
        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }

        public void gravity()
        {
            velocity.Y += .3f;
        }
    }
}