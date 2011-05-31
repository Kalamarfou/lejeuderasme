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
using UltimateErasme.InputTesters;

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
        GameObject mousePointer;
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
        KeyboardTester keyboardTester;

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
            descriptionTypes = new HistoireCreatePerso(game);
            followingTypes.Add(descriptionTypes);
            descriptionTypes = new ResumeCreatePerso(game);
            followingTypes.Add(descriptionTypes);

            descriptionTypes = new TypeRace(game);
            descriptionTypes.remplissageDonneesCreationPerso(out listeButtons, out listeChoix, out descriptions, out choixSelect, out titre);

            persoFinal = PersoFinal.getInstance();
            keyboardTester = new KeyboardTester();
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
            mousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));

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
                    else if (button.getText().Equals("Suivant"))
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

            descriptionTypes.changeCaracValue();
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

            descriptionTypes.gestionClavier(mousePointer);

            keyboardTester.GetKeyboard();

            if (keyboardTester.test(Keys.F))
                graphics.ToggleFullScreen();

            mousePointer.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
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

            spriteBatch.Draw(mousePointer.Sprite, mousePointer.Position, Color.White);
            spriteBatch.End();
        }

        public static float afficherTexte(String texte, Game game, Rectangle viewportRect, SpriteBatch spriteBatch, SpriteFont font, Color color, float debut)
        {
            String[] mots = texte.Split(' ');
            String motToPrint;
            float y = debut;
            int tailleRestanteLigne = (viewportRect.Width / 11);
            int idDebut = 0, tailleRestanteLigneFinale = 0;

            foreach (String mot in mots)
            {
                if (mot.Equals(mots.Last()))
                    motToPrint = mot;
                else
                    motToPrint = mot + " ";
                int tailleMot = motToPrint.Length;
                
                if (tailleMot < tailleRestanteLigne)
                {
                    spriteBatch.DrawString(font, motToPrint, new Vector2(viewportRect.X + (viewportRect.Width - 11 * tailleRestanteLigne), y), color);
                    tailleRestanteLigne -= tailleMot;
                    idDebut += tailleMot;
                }
                else if (tailleMot < viewportRect.Width / 11)
                {
                    y += 20;
                    spriteBatch.DrawString(font, motToPrint, new Vector2(viewportRect.X, y), color);
                    idDebut += tailleMot;
                    tailleRestanteLigne = viewportRect.Width / 11 - tailleMot;
                }
                else
                {
                    //Mot plus grand qu'une ligne
                    y = afficherCaractParCaract(motToPrint, game, viewportRect.X, viewportRect.X + (viewportRect.Width - 11 * tailleRestanteLigne), y, tailleRestanteLigne, viewportRect.Width / 11, spriteBatch, font, color, out tailleRestanteLigneFinale);
                    tailleRestanteLigne = tailleRestanteLigneFinale;
                    idDebut += tailleMot;
                }
            }
            return y;
        }

        public static float afficherCaractParCaract(String mot, Game game, float xInit, float x, float y, int tailleRestanteLigne, int tailleMax, SpriteBatch spriteBatch, SpriteFont font, Color color, out int tailleRestanteLigneFinale)
        {
            int tailleRestanteAEcrire = mot.Length;
            int i = 0;
            while (i < mot.Length)
            {
                if (tailleRestanteAEcrire > tailleRestanteLigne)
                {
                    spriteBatch.DrawString(font, mot.Substring(i, tailleRestanteLigne), new Vector2(x, y), color);
                    tailleRestanteAEcrire -= tailleRestanteLigne;
                    i += tailleRestanteLigne;
                    y += 20;
                    tailleRestanteLigne = tailleMax;
                    x = xInit;
                }
                else
                {
                    spriteBatch.DrawString(font, mot.Substring(i, tailleRestanteAEcrire), new Vector2(x, y), color);
                    i += tailleRestanteAEcrire;
                    tailleRestanteAEcrire = 0;
                }
            }
            tailleRestanteLigneFinale = tailleRestanteLigne;
            return y;
        }

        public override void MustChangeState(GameState futureState)
        {
            Thread.Sleep(300);
            game.currentState = futureState;
        }

    }
}
