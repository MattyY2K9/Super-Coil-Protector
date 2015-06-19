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
        public Coil(Rectangle rectangle)
        {
            spriteRectangle = rectangle;
        }

        public bool isDown;

        public void coilRespawn()
        {
            List<Vector2> coilSpawns;
            coilSpawns = new List<Vector2>();

            //set and load 12 coil spawn positions
            coilSpawns.Add(new Vector2(100, 150));
            coilSpawns.Add(new Vector2(310, 570));
            coilSpawns.Add(new Vector2(500, Game1.window_height - 80));
            coilSpawns.Add(new Vector2(420, 800));
            coilSpawns.Add(new Vector2(190, 350));
            coilSpawns.Add(new Vector2(Game1.window_width - 100, 150));
            coilSpawns.Add(new Vector2(Game1.window_width - 310, 570));
            coilSpawns.Add(new Vector2(Game1.window_width - 500, Game1.window_height - 80));
            coilSpawns.Add(new Vector2(Game1.window_width - 420, 800));
            coilSpawns.Add(new Vector2(Game1.window_width - 190, 350));
            coilSpawns.Add(new Vector2(Game1.window_width / 2, 350));
            coilSpawns.Add(new Vector2(Game1.window_width / 2, 580));
            Random rand = new Random();
            int index = rand.Next(0, 12);
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


