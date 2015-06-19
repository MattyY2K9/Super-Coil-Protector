using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class T1 : Turrets
    {
        public T1(Rectangle playerRectangle, Texture2D texture, Texture2D projText, Texture2D healthText)
        {
            spriteRectangle = playerRectangle;
            spriteTexture = texture;
            fireDelay = 0;
            fireRate = 20;
            damage = 1;
            radius = 1000;
            health = 1000;
            cost = 200;
            projectileTexture = projText;
            maxHealth = health;

            projectiles = new List<Projectiles>();
            healthTexture = healthText;
        }
    }
}
