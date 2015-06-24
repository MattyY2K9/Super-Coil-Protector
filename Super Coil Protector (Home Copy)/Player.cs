using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Coil_Protoctor
{
    public class Player : SpritesWithHealth
    {
        public enum movementState
        {
            running,
            stood,
            jumping,
            dead
        }

        private int runState;
        private int runDelay;

        // Bullets.
        List<Projectiles> projectiles;
        Texture2D projectileTexture;

        public int score;
        public int money;
        private int playerSpeed;
        MouseState newState;
        private MouseState oldState;
        public bool lookingLeft;
        public bool coilRetrieved;
        public movementState state;

        public Player()
        {
            score = 0;
            money = 500;
            health = 10;
            playerSpeed = 5;
            coilRetrieved = false;
            projectiles = new List<Projectiles>();
        }

        public void setBullets(Texture2D bullet)
        {
            projectileTexture = bullet;
        }

        public void movement(KeyboardState keystate, List<Enemy> enemyList, Blocks[] blockList, Coil coil, SpriteFont font)
        {
            // Updates the actual rectangle.
            actualRectangle = new Rectangle(spriteRectangle.X + 20, spriteRectangle.Y + 10, 40, spriteRectangle.Height - 15);

            // Sets initial velocity.
            velocity.X = 0;
            bool gravityEffect = true;

            // Block interaction.
            for (int i = 0; i < blockList.Length; i++)
            {
                if (blockList[i].collisionDetection(this))
                {
                    velocity.Y = 0;
                    gravityEffect = false;
                }
            }

            if (gravityEffect)
                gravity();

            // Movement.
            if (state != movementState.dead)
            {
                if (keystate.IsKeyDown(Keys.A))
                {
                    state = movementState.running;
                    velocity.X -= playerSpeed;
                    lookingLeft = true;
                    run();
                }
                else if (keystate.IsKeyDown(Keys.D))
                {
                    state = movementState.running;
                    velocity.X += playerSpeed;
                    lookingLeft = false;
                    run();
                }
                else
                    state = movementState.stood;


                if (keystate.IsKeyDown(Keys.Space))
                {
                    if (velocity.Y == 0)
                        velocity.Y = -12;
                }

                if (velocity.Y != 0)
                    state = movementState.jumping;

                // Gun fire.
                newState = Mouse.GetState();
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                    fireGun();
                oldState = newState;
            }

            // Coil interaction.
            if (spriteRectangle.Intersects(coil.spriteRectangle))
            {
                coilRetrieved = true;
                coil.setRectangleTop();
            }

            spriteRectangle.X += (int)velocity.X;
            spriteRectangle.Y += (int)velocity.Y;

            // Clamping.
            if (spriteRectangle.X < 0)
                spriteRectangle.X = 0;
            if (spriteRectangle.X + spriteRectangle.Width > Game1.window_width)
                spriteRectangle.X = Game1.window_width - spriteRectangle.Width;

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].movement(enemyList);
                if (projectiles[i].health <= 0)
                    projectiles.Remove(projectiles[i]);
            }

            foreach (Enemy e in enemyList)
            {
                if (e.collisionRect.Intersects(actualRectangle))
                    health--;
            }

            if (health <= 0)
                state = movementState.dead;
        }

        public void addFunds(int add)
        {
            money += add;
            score += add;
        }

        // Alters the spritesheet to show the running.
        private void run()
        {
            runDelay++;
            if (runDelay < 3)
            {
                runState = 0;
            }
            else if (runDelay < 6)
                runState = 1;
            else
                runState = 2;

            if (runDelay >= 9)
                runDelay = 0;
        }

        void fireGun()
        {
            if (lookingLeft)
                projectiles.Add(new Projectiles(projectileTexture, new Rectangle(spriteRectangle.X, spriteRectangle.Y + 75, 10, 10), 1, 5, true));
            else
                projectiles.Add(new Projectiles(projectileTexture, new Rectangle(spriteRectangle.X + spriteRectangle.Width, spriteRectangle.Y + 75, 10, 10), 1, 5, false));
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            int spritesheetX;
            int spritesheetY;

            if (lookingLeft)
                spritesheetX = 0;
            else
                spritesheetX = 96;

            if (state == movementState.stood)
                spritesheetY = 128 * 4;
            else if (state == movementState.running)
                spritesheetY = 128 * (4 + runState);
            else if (state == movementState.jumping)
                spritesheetY = 128 * 2;
            else
                spritesheetY = 0;

            spriteBatch.Draw(spriteTexture, spriteRectangle, new Rectangle(spritesheetX, spritesheetY, 96, 128), Color.White);

            foreach (Projectiles p in projectiles)
                p.draw(spriteBatch);

            base.draw(spriteBatch);
        }
    }
}
