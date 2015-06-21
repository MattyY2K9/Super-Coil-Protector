using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Super_Coil_Protector__Home_Copy_;

namespace Super_Coil_Protoctor
{
    public class GameRun
    {
        enum state
        {
            normal,
            inbetweenWave,
            reset
        }

        Level level;
        List<Turrets> turretList;
        List<Enemy> enemyList;
        List<Loop> loopList;
        Blocks[] blockList;
        Vector2[] enemySpawnList;
        Coil coil;
        Machine machine;
        Player player;
        KeyboardState keyboardState;

        // Textures.
        Texture2D enemyTexture;
        Texture2D projectileTexture;
        Texture2D turret1;
        Texture2D turret2;
        Texture2D turret3;
        Texture2D loop;
        Texture2D block;
        Texture2D healthTexture;
        Texture2D machineCoil;
        Texture2D machineNoCoil;
        Texture2D Background;     
        Sprite repairSpanner;

        private SpriteFont font;

        int enemyCount;
        int enemyHealth;

        float gameOverTimer;
        int resetCount;

        state gameState;

        // Countdown for inbetween waves.
        int waveCooldown;

        // Stops player buying multiple turrets by accident.
        bool keyUp;  

        public GameRun(Level inLevel, Texture2D[] textures, Sprite inSpanner, SpriteFont inFont)
        {
            player = new Player();
            player.spriteRectangle = new Rectangle(0, 0, 96, 128);
            enemyCount = 1;
            enemyHealth = 5;
            gameState = state.normal;

            waveCooldown = 100;
            resetCount = 0;

            gameOverTimer = 30;

            keyUp = false;

            level = inLevel;
            coil = level.coil;
            machine = level.machine;

            // Initialise lists.
            enemyList = new List<Enemy>();
            turretList = new List<Turrets>();
            loopList = new List<Loop>();
            blockList = level.blockArray;
            enemySpawnList = level.enemySpawnList;

            // Textures.
            enemyTexture = textures[0];
            projectileTexture = textures[1];
            turret1 = textures[2];
            turret2 = textures[3];
            turret3 = textures[4];
            loop = textures[5];
            block = textures[6];
            healthTexture = textures[7];
            machineCoil = textures[8];
            machineNoCoil = textures[9];
            coil.spriteTexture = textures[10];
            player.spriteTexture = textures[11];

            Background = level.background;

            repairSpanner = inSpanner;
            repairSpanner.spriteRectangle.Width = 193;
            repairSpanner.spriteRectangle.Height = 71;
            font = inFont;

            player.setBullets(projectileTexture);            

            Reset();
        }

        public void update()
        {
            keyboardState = Keyboard.GetState();
            player.movement(keyboardState, enemyList, blockList, coil, font);

            //turret rapairs
            foreach (Turrets turret in turretList)
            {
                    if (keyboardState.IsKeyDown(Keys.R) && player.spriteRectangle.Intersects(turret.spriteRectangle))
                    {
                        repairSpanner.spriteRectangle.X = turret.spriteRectangle.X;
                        repairSpanner.spriteRectangle.Y = turret.spriteRectangle.Y - 65;
                        turret.repair();
                    }
                    else
                    {
                        turret.beingRepaired = false;
                    }
            }

            // Places coil back in machine.
            if (player.spriteRectangle.Intersects(machine.spriteRectangle) && player.coilRetrieved)
            {
                machine.coilIn = true;
                player.coilRetrieved = false;
                coil.spriteRectangle.X = -40000;
            }

            if (!machine.coilIn)
                gameOverTimer -= 0.02f;
            else
                gameOverTimer = 30;

            if (gameOverTimer <= 0)
                player.health = 0;

            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                if (enemyList[i].health <= 0)
                    enemyList.Remove(enemyList[i]);
            }

            if (enemyList.Count <= 0)
            {
                gameState = state.inbetweenWave;
            }

            if (!coil.isDown)
            {
                foreach (Turrets t in turretList)
                    t.active = false;
            }

            switch (gameState)
            {
                case state.inbetweenWave:
                    waveCooldown--;
                    if (waveCooldown <= 0)
                    {
                        enemyCount++;
                        enemyHealth += 5;
                        gameState = state.normal;
                        waveCooldown = 600;
                        spawnEnemies();
                    }
                    break;
            }

            // Placing turrets.
            dropTurret();

            // Enemy update.
            foreach (Enemy e in enemyList)
                e.Update(blockList);

            // Turret update.
            for (int i = turretList.Count - 1; i >= 0; i--)
            {
                if (turretList[i].health <= 0)
                    turretList.Remove(turretList[i]);
                else
                    turretList[i].update(enemyList, player, machine.coilIn);
            }

            // Loop update.
            for (int i = loopList.Count - 1; i >= 0; i--)
            {
                if (loopList[i].health > 0)
                    loopList[i].update(enemyList);
                else
                    loopList.Remove(loopList[i]);
            }

            // Updates machine.
            machine.setMachineTexture(machineCoil, machineNoCoil);

            if (player.health <= 0)
                resetCount++;
            if (resetCount > 100)
            {
                Reset();
            }
        }

        private void Reset()
        {
            enemyList.Clear();
            turretList.Clear();
            loopList.Clear();
            player.money = 500;
            player.score = 0;
            coil.spriteRectangle.X = -200;
            player.health = 10;
            enemyHealth = 0;
            enemyCount = 0;
            gameState = state.inbetweenWave;
            resetCount = 0;
            machine.coilIn = true;
            player.state = Player.movementState.stood;
            player.spriteRectangle.X = Game1.window_width / 2 - 50;
            player.spriteRectangle.Y = 100;
        }

        void dropTurret()
        {
            Turrets t = null;
            Loop l = null;
            if (keyboardState.IsKeyDown(Keys.D1))
                t = new T1(player.spriteRectangle, turret1, projectileTexture, healthTexture);
            else if (keyboardState.IsKeyDown(Keys.D2))
                t = new T2(player.spriteRectangle, turret2, projectileTexture, healthTexture);
            else if (keyboardState.IsKeyDown(Keys.D3))
                t = new T3(player.spriteRectangle, turret3, projectileTexture, healthTexture);
            else if (keyboardState.IsKeyDown(Keys.D4))
                l = new Loop(player.spriteRectangle, loop, healthTexture);
            else
                keyUp = true;
            if (t != null && player.health > 0)
            {
                if (player.money >= t.cost && keyUp && player.velocity == Vector2.Zero && keyUp)
                {
                    player.money -= t.cost;
                    turretList.Add(t);
                    keyUp = false;
                }
            }
            else if (l != null && player.health > 0)
            {
                if (player.money >= l.cost && keyUp)
                {
                    player.money -= l.cost;
                    loopList.Add(l);
                    keyUp = false;
                }
            }
        }

        private void spawnEnemies()
        {
            Random rand = new Random();
            for (int i = 0; i < enemyCount; i++)
            {
                int speed = rand.Next(1, 4);
                int spawnPoint = rand.Next(0, 8);
                Enemy a = new Enemy(enemyTexture, machine, player, coil, healthTexture, enemyHealth, 5, speed);
                a.spriteRectangle.X = (int)enemySpawnList[spawnPoint].X;
                a.spriteRectangle.Y = (int)enemySpawnList[spawnPoint].Y;
                a.spriteRectangle.Width = 96;
                a.spriteRectangle.Height = 128;
                enemyList.Add(a);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Rectangle(0, 0, Game1.window_width, Game1.window_height), Color.White);


            if (player.health <= 0)
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(Game1.window_width / 2 - 100, 500), Color.DarkOrange);
            else if (!machine.coilIn && gameOverTimer > 0)
                spriteBatch.DrawString(font, gameOverTimer.ToString(), new Vector2(Game1.window_width / 2 - 100, 500), Color.DarkOrange);

            spriteBatch.Draw(machine.spriteTexture, machine.spriteRectangle, Color.White);

            spriteBatch.DrawString(font, "Score " + player.score.ToString(), new Vector2(200, 0), Color.White);

            player.draw(spriteBatch);

            spriteBatch.Draw(coil.spriteTexture, coil.spriteRectangle, Color.White);

            if (player.coilRetrieved)
                spriteBatch.DrawString(font, "x1", new Vector2(1030, 0), Color.White);

            spriteBatch.DrawString(font, "Money " + player.money.ToString(), new Vector2(Game1.window_width - 500, 0), Color.White);


            foreach (Enemy a in enemyList)
                a.draw(spriteBatch);

            foreach (Loop l in loopList)
                l.drawLoops(spriteBatch);

            foreach (Turrets t in turretList)
            {
                t.draw(spriteBatch);
                if (t.beingRepaired)
                    spriteBatch.Draw(repairSpanner.spriteTexture, repairSpanner.spriteRectangle, Color.White);
            }

            foreach (Blocks b in blockList)
                b.draw(spriteBatch);

        }
    }
}
