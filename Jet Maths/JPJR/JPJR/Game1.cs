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
using System.Diagnostics;

namespace JPJR
{
    public enum GameStates { InGame, InQuestion, PauseMenu, MainMenu, GameOver, SplashScreen, Login};

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GameStates CurrentState = GameStates.SplashScreen;
        const int SCREEN_WIDTH = 1920;
        const int SCREEN_HEIGHT = 1080;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int ButtonPressBufferP = 0;
        UserInfo UI;
        bool DrawingObstacles = false;
        bool Drawingcoins = false;
        bool DrawingQuestion = false;

        //TEMP
        Texture2D PauseScreen;
        //TEMP
        Song SongTest1;
        //TEMP

        
        PlatformManager PManager;
        PlatformManager RoofManager;
        PlayerJetpack Jetpack;
        SpeedController Speed;
        ScoreCount DistanceTravelled;
        ScoreCount CoinsCollected;

        LoginScreen LoginS;
        GameOver GameOver1;
        MainMenu mainM;

        SplashScreen SplashS;

        Canons Canons1;
        Coins TestCoins;
        Questions QuestionA;

        DatabaseConnection DBC;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.IsFullScreen = false;
        }
        

      
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            DBC = new DatabaseConnection();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoginS = new LoginScreen(ref spriteBatch, Content, ref DBC, ref UI);

            PManager = new PlatformManager(ref spriteBatch, Content, 1080, "Platform1s");
            RoofManager = new PlatformManager(ref spriteBatch, Content, 100, "Roof1s");
            Jetpack = new PlayerJetpack(ref spriteBatch, Content);
            Speed = new SpeedController(26, 60, 12);
            DistanceTravelled = new ScoreCount(spriteBatch, Content);
            CoinsCollected = new ScoreCount(spriteBatch, Content);

            Canons1 = new Canons(Content, ref spriteBatch);
            TestCoins = new Coins(ref spriteBatch, Content);
            QuestionA = new Questions(1, ref spriteBatch, Content, ref Jetpack);

            //TEMP
            PauseScreen = Content.Load<Texture2D>("Pause1");
            SongTest1 = Content.Load<Song>("BGSong");
            MediaPlayer.Volume = 0.25f;
            MediaPlayer.Play(SongTest1);
            TestCoins.CreateRandomArray();
            Drawingcoins = true;

            SplashS = new SplashScreen(ref spriteBatch, Content);

            // TODO: use this.Content to load your game content here
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            InputManager();
            switch (CurrentState)
            {
                case GameStates.InGame:
                    InGameUpdate();
                    break;
                case GameStates.PauseMenu:
                    PauseMenuUpdate();
                    break;
                case GameStates.MainMenu:
                    MainMenuUpdate();
                    break;
                case GameStates.GameOver:
                    GameOverUpdate();
                    break;
                case GameStates.SplashScreen:
                    SplasScreenUpdate();
                    break;
                case GameStates.Login:
                    LoginUpdate();
                    break;
            }
            
            base.Update(gameTime);
        }

        protected void InGameUpdate()
        {
           
            if (!Jetpack.PlayerUpdate(Canons1.getCanonR(), Canons1.getProjR()))
            {
                Speed.EndGame();
            }

            if (!Jetpack.Alive)
            {
                DrawingQuestion = false;
            }

            if (TestCoins.IsOffScreen && Drawingcoins)
            {
                Drawingcoins = false;
                DrawingObstacles = true;
                Canons1.CreateCanon(Speed.GetSpeed());
            }
            if (Canons1.IsOffScreen && DrawingObstacles)
            {
                DrawingObstacles = false;
                DrawingQuestion = true;
                QuestionA.CreateNewQ(5);
                //TestCoins.CreateRandomArray();
            }
            if(DrawingQuestion && QuestionA.QuestionAnswered)
            {
                DrawingQuestion = false;
                Drawingcoins = true;
                TestCoins.CreateRandomArray();
            }

            if (!Speed.updateSpeed())
            {
                DistanceTravelled.getScore();
                CurrentState = GameStates.GameOver;
                GameOver1 = new GameOver(DistanceTravelled.getScore(), CoinsCollected.getScore(), Jetpack.GetNoOfAnswers(), ref spriteBatch, Content, ref UI);
                UI.SetRecentScore(GameOver1.GetFinalScore());
                UI.SetGamesPlayed(UI.GetGamesPlayed()+ 1);
            }
            Canons1.CanonUpdate();
            if (DrawingQuestion)
            {
                QuestionA.UpdateQuestion();
            }
            TestCoins.UpdateCoins(Jetpack.getRectangle(), ref CoinsCollected, DistanceTravelled.getScore()*20);
            DistanceTravelled.AddScore((float)(Speed.GetSpeed()) * 0.05f);
        }

        protected void InQuestionUpdate()
        {

        }

        protected void PauseMenuUpdate()
        {

        }

        protected void MainMenuUpdate()
        {
            if (mainM.UpdateMenu())
            {
                CurrentState = GameStates.InGame;
            }
        }

        protected void GameOverUpdate()
        {
            GameOver1.GOUpdate();
        }

        protected void SplasScreenUpdate()
        {
            if (SplashS.UpdateSplash() == true)
            {
                CurrentState = GameStates.Login;
            }
        }

        protected void LoginUpdate()
        {
            if (LoginS.UpdateLogin() == "bezzin")
            {
                mainM = new MainMenu(DBC.getUI(), ref spriteBatch, Content);
                CurrentState = GameStates.MainMenu;
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {


            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            switch (CurrentState)
            {
                case GameStates.InGame:
                    InGameDraw();
                    break;
                case GameStates.PauseMenu:
                    PauseMenuDraw();
                    break;
                case GameStates.MainMenu:
                    MainMenuDraw();
                    break;
                case GameStates.GameOver:
                    GameOverDraw();
                    break;
                case GameStates.SplashScreen:
                    SplasScreenDraw();
                    break;
                case GameStates.Login:
                    LoginDraw();
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void InGameDraw()
        {

            GraphicsDevice.Clear(Color.HotPink);

            PManager.DrawStage2(-(Speed.GetSpeed()), DistanceTravelled.getScore());
            //PManager.DrawStage(Speed.GetSpeed());
            RoofManager.DrawStage2(-(Speed.GetSpeed()), DistanceTravelled.getScore());
            TestCoins.DrawCoins(Speed.GetSpeed());
            Jetpack.DrawPlayer();
            DistanceTravelled.DrawScore(null, " M", new Vector2(50, 95));
            CoinsCollected.DrawScore(null, " coins", new Vector2(50, 125));
            Canons1.CanonDraw(Speed.GetSpeed());
            if (DrawingQuestion)
            { 
                QuestionA.DrawQuestion();
            }
        }

        protected void InQuestionDraw()
        {

        }

        protected void PauseMenuDraw()
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Draw(PauseScreen, new Rectangle(0, 0, 1920, 1080), Color.White);
        }

        protected void MainMenuDraw()
        {
            GraphicsDevice.Clear(Color.White);
            mainM.DrawMenu();
        }

        protected void GameOverDraw()
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            GameOver1.GODraw();
        }

        protected void SplasScreenDraw()
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            SplashS.DrawSplash();     
        }

        protected void LoginDraw()
        {
            GraphicsDevice.Clear(Color.White);
            LoginS.DrawLogin();
        }

        void InputManager()
        {
            
            if(ButtonPressBufferP != 0 && ButtonPressBufferP != 20)
            {
                ButtonPressBufferP++;
            }
            else
            {
                ButtonPressBufferP = 0;
            }
            KeyboardState KeyDown = Keyboard.GetState();
            if (KeyDown.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            else if (ButtonPressBufferP == 0 && KeyDown.IsKeyDown(Keys.P) && ((CurrentState == GameStates.InGame) || (CurrentState == GameStates.PauseMenu)))
            {
                ButtonPressBufferP++;
                if (CurrentState == GameStates.PauseMenu)
                {
                    CurrentState = GameStates.InGame;
                }
                else
                {
                    CurrentState = GameStates.PauseMenu;
                }
            }

        }
    }
}