using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Super_Coil_Protoctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Coil_Protector__Home_Copy_
{
    public class Level1 : Level
    {
        public Level1(Texture2D block)
        {
            blockArray = new Blocks[]
            {
                new Blocks(new Rectangle(0, 200, 300, 30), block),
                new Blocks(new Rectangle(0, 620, 650, 30), block),
                new Blocks(new Rectangle(0, Game1.window_height - 30, Game1.window_width, 30), block),
                new Blocks(new Rectangle(0, 850, 850, 30), block),
                new Blocks(new Rectangle(0, 390, 450, 30), block),
                new Blocks(new Rectangle(Game1.window_width - 850, 850, 900, 30), block),
                new Blocks(new Rectangle(Game1.window_width - 650, 620, 650, 30), block),
                new Blocks(new Rectangle(Game1.window_width - 450, 390, 450, 30), block),
                new Blocks(new Rectangle(Game1.window_width - 300, 200, 300, 30), block),
                new Blocks(new Rectangle(Game1.window_width / 2 - 200, 390, 400, 30), block),
                new Blocks(new Rectangle(Game1.window_width / 2 - 150, 620, 300, 30), block)
            };

            Vector2[] coilSpawns = new Vector2[]  
            {
                new Vector2(100, 150),
                new Vector2(310, 570),
                new Vector2(500, Game1.window_height - 80),
                new Vector2(420, 800),
                new Vector2(190, 350),
                new Vector2(Game1.window_width - 100, 150),
                new Vector2(Game1.window_width - 310, 570),
                new Vector2(Game1.window_width - 500, Game1.window_height - 80),
                new Vector2(Game1.window_width - 420, 800),
                new Vector2(Game1.window_width - 190, 350),
                new Vector2(Game1.window_width / 2, 350),
                new Vector2(Game1.window_width / 2, 580)
            };
            
            enemySpawnList = new Vector2[] {
                 new Vector2(0, 100), 
                 new Vector2(0, 520), 
                 new Vector2(0, 750), 
                 new Vector2(0, 290),
                 new Vector2(Game1.window_width,750), 
                 new Vector2(Game1.window_width,520), 
                 new Vector2(Game1.window_width,350),
                 new Vector2(Game1.window_width,200)
            };

            int[] block2Array = new int[] {
                Game1.window_width/2 - 75,
                Game1.window_width/2 + 75
            };

            // Block 1.
            blockArray[0].addDrops(400, false);

            // Block 2.
            blockArray[1].addJumps(450, true);
            blockArray[1].addJumps(649, false);
            blockArray[1].addDrops(750, false);

            // Block 3.
            blockArray[2].addJumps(Game1.window_width / 2 - 75, true);
            blockArray[2].addJumps(Game1.window_width/2 + 75, false);

            // Block 4.
            blockArray[3].addJumps(650, true);
            blockArray[3].addJumps(849, false);
            blockArray[3].addDrops(950, false);

            // Block 5.
            blockArray[4].addJumps(300, true);
            blockArray[4].addJumps(449, false);
            blockArray[4].addDrops(550, false);

            // Block 6.
            blockArray[5].addJumps(175, false);
            blockArray[5].addJumps(1, true);
            blockArray[5].addDrops(-100, true);

            // Block 7.
            blockArray[6].addJumps(175, false);
            blockArray[6].addJumps(1, true);
            blockArray[6].addDrops(-100, true);

            // Block 8.
            blockArray[7].addJumps(125, false);
            blockArray[7].addJumps(1, true);
            blockArray[7].addDrops(-100, true);

            // Block 9.
            blockArray[8].addDrops(-100, true);

            // Block 10.
            blockArray[9].addJumps(1, false);
            blockArray[9].addJumps(400, true);
            blockArray[9].addJumps(0, true);
            blockArray[9].addJumps(399, false);
            blockArray[9].addDrops(-100, true);
            blockArray[9].addDrops(500, false);

            // Block 11.
            blockArray[10].addJumps(0, true);
            blockArray[10].addJumps(300, true);
            blockArray[10].addJumps(1, false);
            blockArray[10].addJumps(299, false);
            blockArray[10].addDrops(-100, true);
            blockArray[10].addDrops(400, false);            

            coil = new Coil(new Rectangle(-200, 100, 25, 43), coilSpawns);
            machine = new Machine(new Rectangle(Game1.window_width / 2 - 50, Game1.window_height - 273, 83, 243));
        }

        public override void setBackground(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            background = content.Load<Texture2D>("background.png");
            base.setBackground(content);
        }
    }
}
