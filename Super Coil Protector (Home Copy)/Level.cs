using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Super_Coil_Protoctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Coil_Protector__Home_Copy_
{
    public class Level
    {
        public Blocks[] blockArray;
        public Texture2D background;
        public Coil coil;
        public Machine machine;
        public Vector2[] enemySpawnList;

        public virtual void setBackground(ContentManager content)
        {            
        }

        public Blocks[] getBlocks()
        {
            return blockArray;
        }
    }
}
