using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class Loop : Turrets
    {
        public bool enemyAttached;
        public Enemy AttachedEnemy;
        private Rectangle ropeRect;
        Texture2D rope;

        public Loop(Rectangle rectangle, Texture2D texture, Texture2D ropeTexture)
        {
            spriteRectangle = rectangle;
            spriteTexture = texture;
            enemyAttached = false;
            health = 1;
            cost = 100;
            rope = ropeTexture;
            ropeRect = new Rectangle(spriteRectangle.X + spriteRectangle.Width / 2 - 5, 0, 10, spriteRectangle.Y);
        }

        public void update(List<Enemy> enemyList)
        {
            if (enemyAttached)
            {
                spriteRectangle.Y -= 10;
                AttachedEnemy.spriteRectangle.Y -= 10;
                if (AttachedEnemy.spriteRectangle.Y + AttachedEnemy.spriteRectangle.Height < 0)
                {
                    health = 0;
                    AttachedEnemy.health = 0;
                }

                ropeRect.Height = spriteRectangle.Y;
            }
            else
            {
                foreach (Enemy a in enemyList)
                {
                    if (spriteRectangle.Intersects(a.spriteRectangle) && !a.attached)
                    {
                        AttachedEnemy = a;
                        enemyAttached = true;
                        AttachedEnemy.attached = true;
                        AttachedEnemy.spriteRectangle = spriteRectangle;
                        break;
                    }
                }
            }
        }
        public void drawLoops(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rope, ropeRect, Color.Orange);
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
        }
    }
}
