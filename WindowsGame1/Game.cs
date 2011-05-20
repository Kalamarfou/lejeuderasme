using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using UltimateErasme.GameObjects;
using UltimateErasme.ClassesDInternet.Particles;
using UltimateErasme.Collisions;
using UltimateErasme.XP;
using System.Threading;
using UltimateErasme.Network;
using UltimateErasme.Life;
using UltimateErasme.Cinematiques;
using System.Xml.Linq;
using UltimateErasme.InputTesters;
using UltimateErasme.MenuState;

namespace UltimateErasme
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public GameState currentState {get; set;}
        private List<GameState> states; 
        private GraphicsDeviceManager graphics;


        public Game() {

            Content.RootDirectory = "Content";
            Components.Add(new GamerServicesComponent(this));

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            currentState = MainMenuState.getInstance(this, graphics);
            states = new List<GameState>();
            states.Add(currentState);
            states.Add(UltimateErasme.getInstance(this, graphics));
            states.Add(PauseMenuState.getInstance(this, graphics));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            foreach (GameState state in states)
            {
                state.Initialize();
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            currentState.LoadContent();
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (GameState state in states)
            {
                state.UnloadContent();
            }
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
