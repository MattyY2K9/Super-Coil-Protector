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
                new Blocks(new Rectangle(Game1.window_width - 850, 850, 850, 30), block),
                new Blocks(new Rectangle(Game1.window_width - 650, 620, 650, 30), block),
                new Blocks(new Rectangle(Game1.window_width - 450, 390, 450, 30), block),
                new Blocks(new Rectangle(Game1.window_width - 300, 200, 300, 30), block),
                new Blocks(new Rectangle(Game1.window_width / 2 - 100, 390, 200, 30), block),
                new Blocks(new Rectangle(Game1.window_width / 2 - 100, 620, 200, 30), block)
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
