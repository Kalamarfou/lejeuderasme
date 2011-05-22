using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.MenuState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace UltimateErasme.MenuStates
{
    class OptionsMenuState : GameState
    {
        public GraphicsDeviceManager graphics;
        public Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;

        List<ListeMenu> listeMenu;
        //List<ListeMenu> listeMenuEnCours;
        GameObject background;
        GameObject MousePointer;
        private static OptionsMenuState instanceOMS;

        private OptionsMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;

            listeMenu = new List<ListeMenu>();
            RemplirListeMenu(listeMenu);
            //TODO : Gerer le cas Annuler en vrai
            //listeMenuEnCours = new List<ListeMenu>();
            //listeMenuEnCours = listeMenu.Clone();
        }

        private void RemplirListeMenu(List<ListeMenu> listeMenu)
        {
            List<OptionMenu> options = new List<OptionMenu>();
            ButtonMenu button = new ButtonMenu("Flou", Color.Red, Color.DarkGreen, new Vector2(300, 350));
            OptionMenu option = new OptionMenu("Faible", new Vector2(400, 350), true);
            options.Add(option);
            option = new OptionMenu("Moyen", new Vector2(500, 350), false);
            options.Add(option);
            option = new OptionMenu("Élevé", new Vector2(600, 350), false);
            options.Add(option);
            ListeMenu liste = new ListeMenu(button, options, Color.Red, Color.DarkGreen);
            listeMenu.Add(liste);

            button = new ButtonMenu("Valider", Color.Red, Color.DarkGreen, new Vector2(300, 400));
            liste = new ListeMenu(button, null, Color.Red, Color.DarkGreen);
            listeMenu.Add(liste);

            button = new ButtonMenu("Annuler", Color.Red, Color.DarkGreen, new Vector2(300, 450));
            liste = new ListeMenu(button, null, Color.Red, Color.DarkGreen);
            listeMenu.Add(liste);
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics)
        {
            if (instanceOMS == null)
            {
                instanceOMS = new OptionsMenuState(game, graphics);
            }
            return instanceOMS;
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Menu\titre"));
            MousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));
        }

        public override void UnloadContent()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ButtonMenu button;
            foreach (ListeMenu liste in listeMenu)
            {
                button = liste.titreListe;
                if (button.isPressed())
                {
                    if (button.getText().Equals("Valider"))
                    {
                        //TODO
                        //listeMenu = listeMenuEnCours;
                        MustChangeState(MainMenuState.getInstance(game, graphics));
                    }
                    else if (button.getText().Equals("Annuler"))
                    {
                        //TODO
                        //listeMenuEnCours = listeMenu;
                        MustChangeState(MainMenuState.getInstance(game, graphics));
                    }
                    else
                    {
                        liste.changeOption();
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
            ButtonMenu button;
            foreach (ListeMenu liste in listeMenu)
            {
                button = liste.titreListe;
                if (button.isNear())
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getOnClickColor());
                }
                else
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getColor());
                }
                if (liste.optionsListe != null)
                {
                    foreach (OptionMenu option in liste.optionsListe)
                    {
                        if (option.isSelected)
                        {
                            spriteBatch.DrawString(font, option.option, option.position, liste.onSelectedColor);
                        }
                        else
                        {
                            spriteBatch.DrawString(font, option.option, option.position, liste.color);
                        }
                    }
                }
            }
            spriteBatch.Draw(MousePointer.Sprite, MousePointer.Position, Color.White);
            spriteBatch.End();
        }

        public override void MustChangeState(GameState futureState)
        {
            Thread.Sleep(300);
            game.currentState = futureState;
            //game.currentState.LoadContent();
        }

    }
}
