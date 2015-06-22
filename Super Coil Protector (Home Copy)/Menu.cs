using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Coil_Protoctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Coil_Protector__Home_Copy_
{
    public class Menu
    {
        private SpriteFont font;

        // Game active.
        bool isGameActive;

        // Texture list to be passed to gameRun.
        Texture2D[] gameTextures;
        Sprite repairSpanner;
        Button playButton;

        Level1 level;

        GameRun gameRun;

        public Menu()
        {
            gameTextures = new Texture2D[12];
            repairSpanner = new Sprite();
        }

        public void loadTextures(ContentManager Content)
        {
            // When adding new textures make sure to increase the length of the gameTextures if needed.
            gameTextures[0] = Content.Load<Texture2D>("enemy.png");
            gameTextures[1] = Content.Load<Texture2D>("laser1.png");
            gameTextures[2] = Content.Load<Texture2D>("turret1.png");
            gameTextures[3] = Content.Load<Texture2D>("turret2.png");
            gameTextures[4] = Content.Load<Texture2D>("turret3.png");
            gameTextures[5] = Content.Load<Texture2D>("loop.png");
            gameTextures[6] = Content.Load<Texture2D>("preview_170.png");
            gameTextures[7] = Content.Load<Texture2D>("healthSquare.png");
            // Coil/Machine.
            gameTextures[8] = Content.Load<Texture2D>("coil in machine 2.png");
            gameTextures[9] = Content.Load<Texture2D>("coil out machine.png");
            gameTextures[10] = Content.Load<Texture2D>("coil.png");
            // Player.
            gameTextures[11] = Content.Load<Texture2D>("player01.png");
            // Spanner.
            repairSpanner.spriteTexture = Content.Load<Texture2D>("repair_spanner.png");

            font = Content.Load<SpriteFont>("spriteFont");

            level = new Level1(gameTextures[6]);
            level.setBackground(Content);

            playButton = new Button("Play", gameTextures[7], new Rectangle(Game1.window_width / 2, Game1.window_height / 2, 100, 50), Color.Yellow);
        }

        public void update(KeyboardState keystate, Game1 game1)
        {
            if (isGameActive)
                gameRun.update();
            else 
            {
                game1.IsMouseVisible = true;
                if (playButton.update())
                {
                    gameRun = new GameRun(level, gameTextures, repairSpanner, font);
                    isGameActive = true;
                    game1.IsMouseVisible = false;
                }
                
            }
        }

        public void draw(SpriteBatch spritebatch)
        {
            if (isGameActive)
                gameRun.draw(spritebatch);
            else
                playButton.draw(spritebatch, font);
        }
    }
}
