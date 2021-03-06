﻿using System;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Coil_Protoctor
{
    public class Enemy : SpritesWithHealth
    {
        enum state
        {
            running,
            stood,
            falling,
            jumping,
            dead
        }

        private Player player;
        private Machine machine;
        private Coil coil;
        private int moveSpeed;
        private int damage;

        // For loops.
        public bool attached;

        // For animation.
        state gameState;
        private int runDelay;
        private int runCount;
        private bool faceLeft;

        // A rectangle that is smaller for the sprite collisions.
        public Rectangle collisionRect;

        /// <summary>
        /// Constructor passing the coil and player to the enemy class
        /// and also setting intial health.
        /// </summary>
        /// <param name="theCoil">the coil</param>
        /// <param name="thePlayer">the player</param>
        /// <param name="setHealth">intital health to set</param>
        public Enemy(Texture2D texture, Machine inMachine, Player thePlayer, Coil inCoil, Texture2D healthText, int setHealth, int setDamage, int inSpeed)
        {
            spriteTexture = texture;
            player = thePlayer;
            machine = inMachine;
            health = setHealth;
            damage = setDamage;
            gameState = state.stood;
            runDelay = 0;
            runCount = 0;
            faceLeft = true;
            maxHealth = health;

            attached = false;

            healthTexture = healthText;

            moveSpeed = inSpeed;

            coil = inCoil;
        }

        /// <summary>
        /// Movement fo the enemy towards the player or coil.
        /// </summary>
        public void Update(Blocks[] blockList)
        {
            // Updates the actual rectangle.
            actualRectangle = new Rectangle(spriteRectangle.X + 15, spriteRectangle.Y + 10, spriteRectangle.Width - 30, spriteRectangle.Height - 15);

            // Updates if enemy is not attached to a noose.
            if (!attached)
            {
                // Update collision Rectangle.
                collisionRect = new Rectangle(spriteRectangle.X + 5, spriteRectangle.Y + 5, spriteRectangle.Width - 5, spriteRectangle.Height);

                bool gravityEffect = true;

                // Coil interaction.
                if (spriteRectangle.Intersects(machine.spriteRectangle) && machine.coilIn)
                {
                    machine.coilIn = false;
                    coil.coilRespawn();
                }

                Blocks currentBlock = null;
                // Block interaction.
                for (int i = 0; i < blockList.Length; i++)
                {
                    if (blockList[i].collisionDetection(this))
                    {
                        velocity.Y = 0;
                        gravityEffect = false;
                        currentBlock = blockList[i];
                    }
                }

                // Directional movement.
                if (!gravityEffect) // Movement in the air
                {
                    int difference = (int)subTarget(currentBlock).X - (spriteRectangle.X + spriteRectangle.Width / 2);
                    if (difference < 5 && difference > -5)
                    {
                        velocity.X = 0;
                        gameState = state.stood;
                    }
                    else if (subTarget(currentBlock).X < spriteRectangle.X)
                    {
                        velocity.X = -moveSpeed;
                        faceLeft = true;
                        run();
                    }
                    else if (subTarget(currentBlock).X > spriteRectangle.X)
                    {
                        velocity.X = moveSpeed;
                        faceLeft = false;
                        run();
                    }
                }
                else
                {
                    if (faceLeft)
                        velocity.X = -moveSpeed;
                    else
                        velocity.X = moveSpeed;
                }

                if (velocity.Y != 0 && gameState != state.jumping)
                    gameState = state.falling;

                // Jumping
                if (jump(currentBlock) && currentBlock != null)
                    velocity.Y -= 12;

                if (gravityEffect)
                    gravity();

                spriteRectangle.X += (int)velocity.X;
                spriteRectangle.Y += (int)velocity.Y;
            }
        }        

        /// <summary>
        /// Returns a target vector of either the players position or the 
        /// coils position depending on coil status.
        /// </summary>
        /// <returns>Target position to move to.</returns>
        private Vector2 getTarget()
        {
            Vector2 target = new Vector2();

            if (machine.coilIn)
            {
                target.X = machine.spriteRectangle.X;
                target.Y = machine.spriteRectangle.Y;
            }
            else
            {
                target.X = player.spriteRectangle.X;
                target.Y = player.spriteRectangle.Y;
            }
            return target;
        }

        // This returns the sub Target of the enemy,
        // for example if the player is above the sub target is the nearest jumping point.
        private Vector2 subTarget(Blocks currentBlock)
        {
            Vector2 target = getTarget();

            if (target.Y < spriteRectangle.Y && currentBlock.jumpPoints.Count > 0)
                    return getPoint(currentBlock, target, currentBlock.jumpPoints);

            if (target.Y > spriteRectangle.Y && currentBlock.dropPoints.Count > 0)
                return getPoint(currentBlock, target, currentBlock.dropPoints);
            
            return target;
        }

        private Vector2 getPoint(Blocks currentBlock, Vector2 target, List<Blocks.pointsOfInterest> points)
        {
            int closest = points[0].position;
            int returnDifference = points[0].position - currentBlock.spriteRectangle.X;
            if (returnDifference < 0)
                returnDifference = 0 - returnDifference;

            for (int i = 0; i < points.Count; i++)
            {
                // Checks if there are any jump points between the player and enemy.
                int position = points[i].position + currentBlock.spriteRectangle.X;
                if (target.X > spriteRectangle.X && position > spriteRectangle.X && position < target.X && !points[i].left)
                    return new Vector2(position, currentBlock.spriteRectangle.Y);
                else if (target.X < spriteRectangle.X && position < spriteRectangle.X && position > target.X && points[i].left)
                    return new Vector2(position, currentBlock.spriteRectangle.Y);

                // If not it will update the closest jump point.
                int difference = position - currentBlock.spriteRectangle.X;
                if (difference < 0)
                    difference = 0 - difference;
                if (difference < returnDifference)
                {
                    returnDifference = difference;
                    closest = points[i].position;
                }
            }
            return new Vector2(currentBlock.spriteRectangle.X + closest, currentBlock.spriteRectangle.Y);
        }

        // Checks whether the enemy should jump.
        private bool jump(Blocks currentBlock)
        {
            if (currentBlock != null)
            {
                if (getTarget().Y < spriteRectangle.Y && velocity.Y == 0)
                {
                    foreach (Blocks.pointsOfInterest i in currentBlock.jumpPoints)
                    {
                        if (subTarget(currentBlock).X == currentBlock.spriteRectangle.X + i.position)
                        {
                            if (spriteRectangle.X <= currentBlock.spriteRectangle.X + i.position && spriteRectangle.X + spriteRectangle.Width >= currentBlock.spriteRectangle.X + i.position)
                            {
                                gameState = state.jumping;
                                faceLeft = i.left;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Allows enemy to take damage.
        /// Checks whether the enemy has died as a result of the damage.
        /// Also adds the damage taken to the players money.
        /// </summary>
        /// <param name="Damage"></param>
        public void TakeDamage(int Damage)
        {
            int tempHealth = health;
            health -= Damage;
            // Makes the money added to player the same amount as the damage given to the enemy.
            if (health > 0)
                tempHealth -= health;
            player.addFunds(tempHealth);
            checkDead();
        }
        /// <summary>
        /// Checks to see if the enemy has died,
        /// if he has it then calls the die method.
        /// </summary>
        private void checkDead()
        {
            if (health <= 0)
            {
                die();
            }
        }
        /// <summary>
        /// IMplements death behaviour.
        /// </summary>
        private void die()
        {
            //death logic
        }

        private void run()
        {
            gameState = state.running;

            runDelay++;

            if (runDelay >= 9)
                runDelay = 0;

            if (runDelay < 3)
                runCount = 0;
            else if (runDelay < 6)
                runCount = 1;
            else
                runCount = 2;
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            int spritesheetX;
            int spritesheetY;

            if (faceLeft)
                spritesheetX = 0;
            else
                spritesheetX = 96;

            if (gameState == state.dead)
                spritesheetY = 0;
            else if (gameState == state.falling)
                spritesheetY = 128 * 2;
            else if (gameState == state.running)
                spritesheetY = 128 * (4 + runCount);
            else
                spritesheetY = 128 * 8;

            float length = spriteRectangle.Width * health / maxHealth;
            spriteBatch.Draw(healthTexture, new Rectangle(spriteRectangle.X, spriteRectangle.Y, spriteRectangle.Width, 5), Color.Red);
            spriteBatch.Draw(healthTexture, new Rectangle(spriteRectangle.X, spriteRectangle.Y, (int)length, 5), Color.Green);

            spriteBatch.Draw(spriteTexture, spriteRectangle, new Rectangle(spritesheetX, spritesheetY, 96, 128), Color.White);
            base.draw(spriteBatch);
        }


    }
}