using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class T3 : Turrets
    {
        public T3(Rectangle playerRectangle, Texture2D texture, Texture2D projText, Texture2D healthText)
        {
            spriteRectangle = playerRectangle;
            spriteTexture = texture;
            fireDelay = 0;
            fireRate = 60;
            damage = 30;
            radius = 1500;
            health = 1500;
            cost = 300;
            projectileTexture = projText;
            maxHealth = health;

            projectiles = new List<Projectiles>();
            healthTexture = healthText;
        }
    }
}