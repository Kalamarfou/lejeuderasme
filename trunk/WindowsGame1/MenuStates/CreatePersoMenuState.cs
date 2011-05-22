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
    class Descriptions
    {
        public String titre {get; set;}
        public String description { get; set; }

        public Descriptions(String titre, String description)
        {
            this.titre = titre;
            this.description = description;
        }
    }

    class CreatePersoMenuState : GameState
    {
        public GraphicsDeviceManager graphics;
        public Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;

        GameObject background;
        GameObject MousePointer;
        private static CreatePersoMenuState instanceCPMS;
        List<ButtonMenu> listeButtons;
        List<ButtonMenu> listeChoix;
        Dictionary<String, List<Descriptions>> descriptions;
        GameObject personnage;
        String choixSelect;
        DescriptionTypes descriptionTypes;
        List<DescriptionTypes> previousTypes;


        private CreatePersoMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;

            descriptionTypes = new TypeRace(game);
            descriptionTypes.remplissageDonneesCreationPerso(out listeButtons, out listeChoix, out descriptions, out choixSelect);

            previousTypes = new List<DescriptionTypes>();
            
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics)
        {
            if (instanceCPMS == null)
            {
                instanceCPMS = new CreatePersoMenuState(game, graphics);
            }
            return instanceCPMS;
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

            personnage = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme"));
        }

        public override void UnloadContent()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (ButtonMenu button in listeButtons)
            {
                if (button.isPressed())
                {
                    if (button.getText().Equals("Annuler"))
                    {
                        Thread.Sleep(300);
                        MustChangeState(MainMenuState.getInstance(game, graphics));
                    }
                    else if (button.getText().Equals("Recommandé"))
                    {
                        //TODO
                    }
                    else if (button.getText().Equals("Retour"))
                    {
                        Thread.Sleep(300);
                        if (previousTypes.Count > 0)
                        {
                            descriptionTypes = previousTypes.Last();
                            previousTypes.Remove(descriptionTypes);
                            descriptionTypes.remplissageDonneesCreationPerso(out listeButtons, out listeChoix, out descriptions, out choixSelect);
                        }
                    }
                    else //Suivant
                    {
                        Thread.Sleep(300);
                        previousTypes.Add(descriptionTypes);
                        descriptionTypes = new TypeClasse(game);
                        descriptionTypes.remplissageDonneesCreationPerso(out listeButtons, out listeChoix, out descriptions, out choixSelect);                        
                    }
                }
            }

            foreach (ButtonMenu button in listeChoix)
            {
                if (button.isPressed())
                {
                    choixSelect = button.getText();
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
            
            //Affichage de la partie gauche : Personnage
            viewportRect = new Rectangle(0, 0, 3 * game.GraphicsDevice.Viewport.Width/8, 9 * game.GraphicsDevice.Viewport.Height/10);
            DrawPersonnage(viewportRect, spriteBatch);
            
            //Afficher la partie du milieu : Liste des choix
            viewportRect = new Rectangle(3 * game.GraphicsDevice.Viewport.Width / 8, 0, 2 * game.GraphicsDevice.Viewport.Width / 8, 9 * game.GraphicsDevice.Viewport.Height / 10);
            DrawChoix(spriteBatch);
            
            //Afficher la partie de droite : Description
            viewportRect = new Rectangle(5 * game.GraphicsDevice.Viewport.Width / 8, 0, 3 * game.GraphicsDevice.Viewport.Width / 8, 9 * game.GraphicsDevice.Viewport.Height / 10);
            DrawDescription(choixSelect, viewportRect, spriteBatch);

            //Afficher la partie basse : Boutons
            DrawButtons(spriteBatch);

            spriteBatch.Draw(MousePointer.Sprite, MousePointer.Position, Color.White);
            spriteBatch.End();
        }

        private void DrawButtons(SpriteBatch spriteBatch)
        {
            foreach (ButtonMenu button in listeButtons)
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
        }

        private void DrawDescription(String choix, Rectangle viewportRect, SpriteBatch spriteBatch)
        {
            List<Descriptions> descriptionChoice;
            descriptions.TryGetValue(choix, out descriptionChoice);

            //spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
            float y = 20;
            foreach (Descriptions description in descriptionChoice)
            {
                spriteBatch.DrawString(font, description.titre, new Vector2(viewportRect.X, y), Color.DarkBlue);
                spriteBatch.DrawString(font, description.description, new Vector2(viewportRect.X, y + 20), Color.DarkBlue);
                y += 20;
            }
        }

        private void DrawChoix(SpriteBatch spriteBatch)
        {
            foreach (ButtonMenu button in listeChoix)
            {
                if (button.isNear() || button.getText().Equals(choixSelect))
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getOnClickColor());
                }
                else
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getColor());
                }
            }
        }

        public void DrawPersonnage(Rectangle viewportRect, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(personnage.Sprite, viewportRect, Color.White);
        }

        public override void MustChangeState(GameState futureState)
        {
            Thread.Sleep(300);
            game.currentState = futureState;
        }

    }
}
