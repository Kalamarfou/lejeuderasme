using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;
using UltimateErasme.MenuState;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme.MenuStates
{
    class SavedPersoMenuState : GameState
    {
        public GraphicsDeviceManager graphics;
        public Game game;
        private static GameState instanceSPMS;
        SpriteBatch spriteBatch;
        SpriteFont font;
        GameObject background;
        GameObject MousePointer;
        static List<ButtonMenu> buttonMenu;
        static List<string> savedFileName;
        public static string directory = "Content\\Sauvegardes";
        public static Dictionary<string, PersoFinal> listePerso = new Dictionary<string, PersoFinal>();

        private SavedPersoMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
        }

        private static void mettreAJourListePersos(Game game) {
            savedFileName = ErasmeFilesDirectoriesUtils.dir(directory);
            float x = 300;
            float y = 250;
            buttonMenu = new List<ButtonMenu>();
            ButtonMenu button;
            foreach (string savedFile in savedFileName)
            {
                savedFile.Replace('_', ' ');
                button = new ButtonMenu(savedFile, Color.DarkBlue, Color.DarkGreen, new Vector2(x, y));
                if(!listePerso.ContainsKey(savedFile))
                    listePerso.Add(savedFile, null);
                buttonMenu.Add(button);
                y += 50;
            }
            if (savedFileName.Count < 5)
            {
                button = new ButtonMenu("Créer nouveau personnage", Color.DarkBlue, Color.DarkGreen, new Vector2(x, y));
                buttonMenu.Add(button);
            }
            button = new ButtonMenu("Annuler", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            buttonMenu.Add(button);
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics)
        {
            if (instanceSPMS == null)
            {
                instanceSPMS = new SavedPersoMenuState(game, graphics);
            }
            mettreAJourListePersos(game);
            return instanceSPMS;
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Backgrounds\decor"));
            MousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));
        }

        public override void UnloadContent()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (ButtonMenu button in buttonMenu)
            {
                if (button.isPressed())
                {
                    if (button.getText().Equals("Créer nouveau personnage"))
                    {
                        MustChangeState(CreatePersoMenuState.getInstance(game, graphics));
                    }
                    else if (button.getText().Equals("Annuler"))
                    {
                        MustChangeState(MainMenuState.getInstance(game, graphics));
                    }
                    else
                    {
                        String fileName = button.getText();
                        fileName.Replace(' ', '_');
                        ErasmeFilesDirectoriesUtils.chargerPerso(listePerso, directory, fileName);
                        PersoFinal persoFinal;
                        listePerso.TryGetValue(fileName, out persoFinal);
                        MustChangeState(new ResumeCreatePerso(game, graphics, persoFinal));
                        Thread.Sleep(300);
                    }
                }
            }
            MousePointer.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
             //init
            game.GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Rectangle viewportRect = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
            viewportRect = new Rectangle(250, 150, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            ErasmeUtils.afficherTexte("SÉLECTION DU PERSONNAGE", game, viewportRect, spriteBatch, font, Color.DarkRed, viewportRect.Y);

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
            Thread.Sleep(300);
            game.currentState = futureState;
            game.currentState.LoadContent();
        }
    }
}
