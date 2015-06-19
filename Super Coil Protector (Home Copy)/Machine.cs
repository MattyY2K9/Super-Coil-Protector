using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
namespace Super_Coil_Protoctor
{
    public class Machine : Sprite
    {
        public Machine(Rectangle rectangle)
        {
            spriteRectangle = rectangle;
        }

        public bool coilIn = true;

        public void setMachineTexture(Texture2D texture1, Texture2D texture2)
        {
            if (coilIn)
                spriteTexture = texture1;
            else
                spriteTexture = texture2;
        }
    }
}
