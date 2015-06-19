using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class T2 : Turrets
    {
        public T2(Rectangle playerRectangle, Texture2D texture, Texture2D projText, Texture2D healthText)
        {
            spriteRectangle = playerRectangle;
            spriteTexture = texture;
            fireDelay = 0;
            fireRate = 10;
            damage = 5;
            radius = 600;
            health = 800;
            cost = 150;
            projectileTexture = projText;
            maxHealth = health;

            projectiles = new List<Projectiles>();
            healthTexture = healthText;
        }
    }
}
