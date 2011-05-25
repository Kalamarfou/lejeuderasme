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
        String titre;
        DescriptionTypes descriptionTypes;
        List<DescriptionTypes> previousTypes;
        List<DescriptionTypes> followingTypes;
        PersoFinal persoFinal;
        
        private CreatePersoMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;

            previousTypes = new List<DescriptionTypes>();
            followingTypes = new List<DescriptionTypes>();
            descriptionTypes = new TypeClasse(game);
            followingTypes.Add(descriptionTypes);
            descriptionTypes = new TypeAlignement(game);
            followingTypes.Add(descriptionTypes);
            descriptionTypes = new TypeDivinite(game);
            followingTypes.Add(descriptionTypes);
            descriptionTypes = new TypePersonnalise(game);
            followingTypes.Add(descriptionTypes);
            descriptionTypes = new CaracteristiquesCreatePerso(game);
            followingTypes.Add(descriptionTypes);
            descriptionTypes = new ResumeCreatePerso(game);
            followingTypes.Add(descriptionTypes);

            descriptionTypes = new TypeRace(game);
            descriptionTypes.remplissageDonneesCreationPerso(out listeButtons, out listeChoix, out descriptions, out choixSelect, out titre);

            persoFinal = PersoFinal.getInstance();
            
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
                        choixSelect = descriptionTypes.getValeurRecommande(persoFinal);
                    }
                    else if (button.getText().Equals("Retour"))
                    {
                        Thread.Sleep(300);
                        if (previousTypes.Count > 0)
                        {
                            followingTypes.Add(descriptionTypes);
                            descriptionTypes = previousTypes.Last();
                            previousTypes.Remove(descriptionTypes);
                            descriptionTypes.remplissageDonneesCreationPerso(out listeButtons, out listeChoix, out descriptions, out choixSelect, out titre);
                            choixSelect = descriptionTypes.getValeurRecommande(persoFinal);
                        }
                    }
                    else if (button.getText().Equals("Suivant")) //Suivant
                    {
                        Thread.Sleep(300);
                        //On met à jour le personnage final
                        descriptionTypes.setValeurRecommande(persoFinal, choixSelect);
                        previousTypes.Add(descriptionTypes);
                        descriptionTypes = followingTypes.First();
                        descriptionTypes.remplissageDonneesCreationPerso(out listeButtons, out listeChoix, out descriptions, out choixSelect, out titre);
                        followingTypes.Remove(descriptionTypes);
                    }
                    else
                    {
                        MustChangeState(MainMenuState.getInstance(game, graphics));
                    }
                }
            }
            if (listeChoix != null)
            {
                foreach (ButtonMenu button in listeChoix)
                {
                    if (button.isPressed())
                    {
                        choixSelect = button.getText();
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
            
            //Affichage de la partie gauche : Personnage
            viewportRect = new Rectangle(0, game.GraphicsDevice.Viewport.Height / 10, 3 * game.GraphicsDevice.Viewport.Width / 8, 8 * game.GraphicsDevice.Viewport.Height / 10);
            descriptionTypes.DrawPersonnage(viewportRect, spriteBatch, personnage);
            
            //Afficher la partie du milieu : Liste des choix
            viewportRect = new Rectangle(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Height / 10, 2 * game.GraphicsDevice.Viewport.Width / 8, 8 * game.GraphicsDevice.Viewport.Height / 10);
            descriptionTypes.DrawChoix(spriteBatch, listeChoix, viewportRect, game, choixSelect, font);
            
            //Afficher la partie de droite : Description
            viewportRect = new Rectangle(5 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Height / 10, 3 * game.GraphicsDevice.Viewport.Width / 8, 8 * game.GraphicsDevice.Viewport.Height / 10);
            descriptionTypes.DrawDescription(choixSelect, viewportRect, spriteBatch, game, descriptions, font);

            //Afficher la partie basse : Boutons
            descriptionTypes.DrawButtons(spriteBatch, listeButtons, font);

            //Afficher la partie haute : Titre
            descriptionTypes.DrawTitle(spriteBatch, game, titre, font);

            spriteBatch.Draw(MousePointer.Sprite, MousePointer.Position, Color.White);
            spriteBatch.End();
        }

        public static float afficherTexte(String texte, Game game, Rectangle viewportRect, SpriteBatch spriteBatch, SpriteFont font, Color color, float debut)
        {
            int tailleRestante = texte.Length;
            int i = 0;
            float y = debut;
            while (i < texte.Length)
            {
                int tailleMax = (viewportRect.Width / 11);
                if (tailleRestante > tailleMax)
                {
                    spriteBatch.DrawString(font, texte.Substring(i, tailleMax), new Vector2(viewportRect.X, y + 20), color);
                    tailleRestante -= tailleMax;
                    i += tailleMax;
                }
                else
                {
                    spriteBatch.DrawString(font, texte.Substring(i, tailleRestante), new Vector2(viewportRect.X, y + 20), color);
                    i += tailleRestante;
                    tailleRestante = 0;
                }
                y += 20;
            }
            return y;
        }

        public override void MustChangeState(GameState futureState)
        {
            Thread.Sleep(300);
            game.currentState = futureState;
        }

    }
}
