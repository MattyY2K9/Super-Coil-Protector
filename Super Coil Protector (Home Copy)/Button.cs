using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Coil_Protoctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Coil_Protector__Home_Copy_
{
   public class Button : Sprite
    {
       private string content;
       private Color buttonColour;
       private Color colour;

       public Button(string inContent, Texture2D texture, Rectangle rectangle, Color inColour)
       {
           content = inContent;
           spriteTexture = texture;
           spriteRectangle = rectangle;
           colour = inColour;
           buttonColour = colour;

       }

       public bool update()
       {
           colour = buttonColour;

           MouseState mouse = Mouse.GetState();
           // Mouse.Y is acting strange so adding 15 seems to fix it.
           Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y + 15, 1, 1);
           if (mouseRectangle.Intersects(spriteRectangle))
           {
               colour = new Color(buttonColour.ToVector3() - new Vector3(0.1f, 0.1f, 0.1f));
               if (mouse.LeftButton == ButtonState.Pressed)
                   return true;
           }
           return false;
       }

       public void draw(SpriteBatch spritebatch, SpriteFont font)
       {
           spritebatch.Draw(spriteTexture, spriteRectangle, colour);
           spritebatch.DrawString(font, content, new Vector2(spriteRectangle.X, spriteRectangle.Y + 20), Color.Black);
       }
    }
}
