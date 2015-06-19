using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class Projectiles : SpritesWithHealth
    {
        public int damage;
        int speed;

        public Projectiles(Texture2D texture, Rectangle position, int inDamage, int inSpeed, bool fireLeft)
        {
            spriteTexture = texture;
            spriteRectangle = position;
            damage = inDamage;
            health = 1;
            if (fireLeft)
                speed = -inSpeed;
            else
                speed = inSpeed;
        }

        public void movement(List<Enemy> enemies)
        {
            // If update method is used move this into it.
            foreach (Enemy a in enemies)
            {
                if (spriteRectangle.Intersects(a.collisionRect))
                {
                    a.TakeDamage(damage);
                    health = 0;
                    break;
                }
            }

            spriteRectangle.X += speed;
            // TODO: Alter the second argument if a windowsize is specified.
            if (spriteRectangle.X < 0 || spriteRectangle.X > 2000)
                health = 0;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
            base.draw(spriteBatch);
        }
    }
}
