﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme.MenuState
{
    class PauseMenuState : GameState
    {
        GraphicsDeviceManager graphics;
        Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;
        List<ButtonMenu> buttonMenu;
        GameObject background;
        GameObject MousePointer;
        private static PauseMenuState instancePMS;

        private PauseMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            buttonMenu = new List<ButtonMenu>();
            ButtonMenu bouton = new ButtonMenu("Continuer", Color.Black, Color.DarkGreen, new Vector2(300, 150));
            buttonMenu.Add(bouton);
            bouton = new ButtonMenu("Quitter", Color.Black, Color.DarkGreen, new Vector2(300, 200));
            buttonMenu.Add(bouton);
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics) {
            if (instancePMS == null)
            {
                instancePMS = new PauseMenuState(game, graphics);
            }
            return instancePMS;
        }
         /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Backgrounds\decor2"));
            MousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (ButtonMenu button in buttonMenu)
            {
                if (button.isPressed())
                {
                    if (button.getText().Equals("Quitter"))
                    {
                        game.Exit();
                    }
                    else
                    {
                        MustChangeState(UltimateErasme.getInstance(game, graphics));
                    }
                }
            }

            MousePointer.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {

            //init
            game.GraphicsDevice.Clear(Color.Red);
            UltimateErasme.getInstance(game, graphics).Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Rectangle viewportRect = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            //spriteBatch.Draw(background.Sprite, viewportRect, Color.White);

            foreach (ButtonMenu button in buttonMenu)
            {
                if (button.isNear())
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getOnClickColor());
                }
                else
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getColor());
                }
            }
            spriteBatch.Draw(MousePointer.Sprite, MousePointer.Position, Color.White);
            spriteBatch.End();
        }

        public override void MustChangeState(GameState futureState)
        {
            game.currentState = futureState;
        }
    }
}