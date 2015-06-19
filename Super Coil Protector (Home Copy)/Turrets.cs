using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class Turrets : SpritesWithHealth
    {
        public List<Projectiles> projectiles;

        protected int damage;
        protected int fireRate;
        protected int radius;
        protected int fireDelay;
        bool fireLeft;
        public bool beingRepaired;

        public bool dislayHealth;

        public int cost;

        // Each Turret has specific projectiles which are assigned upon creation in the GameRun.
        protected Texture2D projectileTexture;

        // If coil is out then turrets are inactive.
        public bool active;

        public void update(List<Enemy> enemyList, Player player, bool inActive)
        {
            // Sets the closest enemy to at least the distance of the radius.
            // The variable will be used to determine which way to shoot a projectile.
            int closest = radius;
            fireLeft = false;

            // Sets if active.
            active = inActive;

            // Lowers the count for the cool down time.
            fireDelay--;

            foreach (Enemy a in enemyList)
            {
                if (sameY(a.spriteRectangle))
                {
                    if (spriteRectangle.X - a.spriteRectangle.X < closest && spriteRectangle.X - a.spriteRectangle.X > 0)
                    {
                        closest = spriteRectangle.X - a.spriteRectangle.X;
                        fireLeft = true;
                    }
                    else if (a.spriteRectangle.X - spriteRectangle.X < closest && a.spriteRectangle.X - spriteRectangle.X >= 0)
                    {
                        closest = a.spriteRectangle.X - spriteRectangle.X;
                        fireLeft = false;
                    }
                }

                if (spriteRectangle.Intersects(a.spriteRectangle))
                    health--;
            }

            if (closest < radius)
            {
                if (fireDelay <= 0 && active)
                {
                    fireDelay = fireRate;

                    if (fireLeft)
                    {
                        Projectiles p = new Projectiles(projectileTexture, new Rectangle(spriteRectangle.X + spriteRectangle.Width / 2, spriteRectangle.Y + spriteRectangle.Height / 2, 20, 11), damage, 5, true);
                        projectiles.Add(p);
                    }
                    else
                    {
                        Projectiles p = new Projectiles(projectileTexture, new Rectangle(spriteRectangle.X + spriteRectangle.Width / 2, spriteRectangle.Y + spriteRectangle.Height / 2, 20, 11), damage, 5, false);
                        projectiles.Add(p);
                    }
                }
            }

            foreach (Projectiles b in projectiles)
            {
                b.movement(enemyList);
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                if (projectiles[i].health <= 0)
                    projectiles.Remove(projectiles[i]);
            }

            if (health <= maxHealth / 2 || spriteRectangle.Intersects(player.spriteRectangle))
                dislayHealth = true;
            else
                dislayHealth = false;
        }

        public void repair()
        {
            if (health < maxHealth)
                health += 3;
            beingRepaired = true;
        }

        bool sameY(Rectangle rectangle)
        {
            if (spriteRectangle.Y + spriteRectangle.Height / 2 > rectangle.Y && spriteRectangle.Y + spriteRectangle.Height / 2 < rectangle.Y + rectangle.Height)
                return true;
            else
                return false;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            foreach (Projectiles p in projectiles)
                p.draw(spriteBatch);

            if (dislayHealth)
            {
                float length = spriteRectangle.Width * health / maxHealth;
                spriteBatch.Draw(healthTexture, new Rectangle(spriteRectangle.X, spriteRectangle.Y, spriteRectangle.Width, 5), Color.Red);
                spriteBatch.Draw(healthTexture, new Rectangle(spriteRectangle.X, spriteRectangle.Y, (int)length, 5), Color.Green);
            }

            int spritesheetX = 0;
            if (!fireLeft)
                spritesheetX = 53;
            spriteBatch.Draw(spriteTexture, new Rectangle(spriteRectangle.X, spriteRectangle.Y + 52, spriteRectangle.Width, spriteRectangle.Height - 52), new Rectangle(spritesheetX, 0, 53, 57), Color.White);

            base.draw(spriteBatch);
        }
    }
}
