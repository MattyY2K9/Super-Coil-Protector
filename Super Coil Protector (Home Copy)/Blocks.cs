using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class Blocks : Sprite
    {
        public struct pointsOfInterest
        {

            public int position;
            public bool left;

            public pointsOfInterest(int inPosition, bool inLeft)
            {
                position = inPosition;
                left = inLeft;
            }
        }

        public List<pointsOfInterest> jumpPoints;
        public List<pointsOfInterest> dropPoints;

        public Blocks(Rectangle rectangle, Texture2D texture)
        {
            spriteRectangle = rectangle;
            spriteTexture = texture;
            jumpPoints = new List<pointsOfInterest>();
            dropPoints = new List<pointsOfInterest>();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            int count = (int)spriteRectangle.Width / 40;
            int overlap = spriteRectangle.Width - (count * 40);
            for (int i = 0; i < count; i++)
            {
                spriteBatch.Draw(spriteTexture, new Rectangle(spriteRectangle.X + (40 * i), spriteRectangle.Y, 40, 30), new Rectangle(45, 35, 100, 100), Color.White);
                if(i == count - 1)
                    spriteBatch.Draw(spriteTexture, new Rectangle(spriteRectangle.X + (40 * (i + 1)), spriteRectangle.Y,overlap, 30), new Rectangle(45,35,overlap,100),Color.White);
            }
            base.draw(spriteBatch);
        }

        public bool collisionDetection(SpritesWithHealth sprite)
        {
            // This sets a rectangle at the players feet to see if he lands on the block.
            Rectangle intersectsRectangle = new Rectangle(sprite.actualRectangle.X, sprite.actualRectangle.Y + sprite.actualRectangle.Height - 5, sprite.actualRectangle.Width, 5);

            if (spriteRectangle.Intersects(intersectsRectangle))
            {
                sprite.spriteRectangle.Y = spriteRectangle.Y - sprite.spriteRectangle.Height + 10;
                return true;
            }
            return false;
        }

        public void addJumps(int inPosition, bool left)
        {
            jumpPoints.Add(new pointsOfInterest(inPosition, left));
        }

        public void addDrops(int inPosition, bool left)
        {
            dropPoints.Add(new pointsOfInterest(inPosition, left));
        }
    }
}
