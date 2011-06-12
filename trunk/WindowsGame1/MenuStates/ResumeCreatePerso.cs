using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using UltimateErasme.MenuState;

namespace UltimateErasme.MenuStates
{
    class ResumeCreatePerso : GameState
    {
        Game game;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PersoFinal persoFinal;
        Rectangle rectResume;
        GameObject mousePointer;
        private static GameState instanceRCP;
        SpriteFont font;
        GameObject background;
        GameObject personnage;
        List<ButtonMenu> listeButtons;
        String titre;

        private ResumeCreatePerso(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            this.persoFinal = PersoFinal.getInstance();
            rectResume = new Rectangle(300, 100, 500, 600);

            listeButtons = new List<ButtonMenu>();
            ButtonMenu button = new ButtonMenu("Supprimer ce personnage", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Valider", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            titre = "RÉSUMÉ DE VOTRE PERSONNAGE";
        }

        public ResumeCreatePerso(Game game, GraphicsDeviceManager graphics, PersoFinal persoFinal)
        {
            this.game = game;
            this.graphics = graphics;
            this.persoFinal = persoFinal;
            rectResume = new Rectangle(300, 100, 500, 600);

            listeButtons = new List<ButtonMenu>();
            ButtonMenu button = new ButtonMenu("Supprimer ce personnage", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Valider", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            titre = "RÉSUMÉ DE VOTRE PERSONNAGE";
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics)
        {
            if (instanceRCP == null)
            {
                instanceRCP = new ResumeCreatePerso(game, graphics);
            }
            instanceRCP.LoadContent();
            return instanceRCP;
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
                        if (button.getText().Equals("Supprimer ce personnage"))
                        {
                            persoFinal.persoValide = false;
                            ErasmeFilesDirectoriesUtils.fileDelete(SavedPersoMenuState.directory + "\\" + persoFinal.prenom + "_" + persoFinal.nom + ".xml");
                            SavedPersoMenuState.listePerso.Remove(persoFinal.prenom + "_" + persoFinal.nom);
                            MustChangeState(SavedPersoMenuState.getInstance(game, graphics));
                        }
                        else if (button.getText().Equals("Valider"))
                        {
                            persoFinal.persoValide = true;
                            PersoFinal.setInstance(persoFinal);
                            ErasmeFilesDirectoriesUtils.enregistrerPerso(SavedPersoMenuState.listePerso, persoFinal, SavedPersoMenuState.directory, true);
                            MustChangeState(MainMenuState.getInstance(game, graphics));
                        }
                    }
            }
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
            DrawPersonnage(viewportRect, spriteBatch, personnage);

            //Afficher la partie de droite : Description
            viewportRect = new Rectangle(5 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Height / 10, 3 * game.GraphicsDevice.Viewport.Width / 8, 8 * game.GraphicsDevice.Viewport.Height / 10);
            DrawDescription(viewportRect, spriteBatch, game, font);

            //Afficher la partie basse : Boutons
            DrawButtons(spriteBatch, listeButtons, font);

            //Afficher la partie haute : Titre
            DrawTitle(spriteBatch, game, titre, font);

            spriteBatch.Draw(mousePointer.Sprite, mousePointer.Position, Color.White);
            spriteBatch.End();
        }

        private void DrawDescription(Rectangle viewportRect, SpriteBatch spriteBatch, Game game, SpriteFont font)
        {
            float y = rectResume.Y;
            String texte = "Vous êtes donc un " + persoFinal.race + " avec pour classe : " + persoFinal.classe + ". Vous n'avez de foi qu'en " + persoFinal.divinite + " et vous êtes de personalité " + persoFinal.personnalite + " et d'alignement " + persoFinal.alignement + ". ";
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y);
            texte = "Vous vous appelez : " + persoFinal.prenom + " " + persoFinal.nom + ". Votre âge est : " + persoFinal.age + " ans. Voici votre histoire : " + persoFinal.histoire;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 50);
            texte = "FORCE : " + persoFinal.force;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 50);
            texte = "DEXTÉRITÉ : " + persoFinal.dexterite;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
            texte = "CONSTITUTION : " + persoFinal.constitution;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
            texte = "INTELLIGENCE : " + persoFinal.intelligence;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
            texte = "SAGESSE : " + persoFinal.sagesse;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
            texte = "CHARISME : " + persoFinal.charisme;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
        }

        private void DrawButtons(SpriteBatch spriteBatch, List<ButtonMenu> listeButtons, SpriteFont font)
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

        private void DrawTitle(SpriteBatch spriteBatch, Game game, String titre, SpriteFont font)
        {
            spriteBatch.DrawString(font, titre, new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 20), Color.DarkRed);
        }

        private void DrawPersonnage(Rectangle viewportRect, SpriteBatch spriteBatch, GameObject personnage)
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
