using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class Coil : Pickups
    {
        Vector2[] coilSpawns;

        public Coil(Rectangle rectangle, Vector2[] inCoilSpawns)
        {
            spriteRectangle = rectangle;
            coilSpawns = inCoilSpawns;
        }

        public bool isDown;

        public void coilRespawn()
        {
            //set and load 12 coil spawn positions
            Random rand = new Random();
            int index = rand.Next(0, coilSpawns.Length);
            spriteRectangle.X = (int)coilSpawns[index].X;
            spriteRectangle.Y = (int)coilSpawns[index].Y;
        }


        public void setRectangleTop()
        {
            spriteRectangle.X = 1000;
            spriteRectangle.Y = 0;
        }
    }
}


