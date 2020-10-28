using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JPJR
{
    class MainMenu
    {
        UserInfo user;
        Button HighScores;
        Button Play;
        Button ChangeClass;
        Button ChangePassword;
        Rectangle LogoR;
        Texture2D LogoT;
        SpriteFont font;
        SpriteBatch sBatch;
        int BBuffer;
        
        public MainMenu(UserInfo UI, ref SpriteBatch isBatch, ContentManager Content)
        {
            font = Content.Load<SpriteFont>("TextFont");
            BBuffer = 1;
            LogoT = Content.Load<Texture2D>("SplashLogo");
            LogoR = new Rectangle(100, 100, 400, 175);
            sBatch = isBatch;
            user = UI;
            Play = new Button(ref sBatch, Content, "PlayS", "Play", true, new Rectangle(200, 800, 225, 150));
            HighScores = new Button(ref sBatch, Content, "HighscoresS", "Highscores", false, new Rectangle(600, 800, 225, 150));
            ChangeClass = new Button(ref sBatch, Content, "ChangeClass", "ChangeClassS", false, new Rectangle(1000, 800, 225, 150));
            ChangePassword = new Button(ref sBatch, Content, "ChangePass", "ChangePassS", false, new Rectangle(1400, 800, 225, 150));
        }

        public void DrawMenu()
        {
            sBatch.DrawString(font, string.Concat("Welcome ", user.getFirstName()), new Vector2(600, 100), Color.Black);
            sBatch.Draw(LogoT, LogoR, Color.White);
            Play.drawButton();
            HighScores.drawButton();
            ChangePassword.drawButton();
            ChangeClass.drawButton();

        }
        public bool UpdateMenu()
        {
            Play.UpdateButton();
            HighScores.UpdateButton();
            ChangeClass.UpdateButton();
            ChangePassword.UpdateButton();
            if (BBuffer == 0)
            {
                KeyboardState KeyDown = Keyboard.GetState();
                if (Play.getSelected())
                {
                    if (KeyDown.IsKeyDown(Keys.Right))
                    {
                        Play.setSelected(false);
                        HighScores.setSelected(true);
                        BBuffer++;
                    }
                    else if (KeyDown.IsKeyDown(Keys.Enter))
                    {
                        return true;
                    }
                }
                else if (HighScores.getSelected())
                {
                    if (KeyDown.IsKeyDown(Keys.Left))
                    {
                        HighScores.setSelected(false);
                        Play.setSelected(true);
                        BBuffer++;
                    }
                    else if (KeyDown.IsKeyDown(Keys.Right))
                    {
                        HighScores.setSelected(false);
                        ChangeClass.setSelected(true);
                        BBuffer++;
                    }
                }
                else if (ChangeClass.getSelected())
                {
                    if (KeyDown.IsKeyDown(Keys.Left))
                    {
                        ChangeClass.setSelected(false);
                        HighScores.setSelected(true);
                        BBuffer++;
                    }
                    else if (KeyDown.IsKeyDown(Keys.Right))
                    {
                        ChangeClass.setSelected(false);
                        ChangePassword.setSelected(true);
                        BBuffer++;
                    }
                }
                else if (ChangePassword.getSelected())
                {
                    if (KeyDown.IsKeyDown(Keys.Left))
                    {
                        ChangePassword.setSelected(false);
                        ChangeClass.setSelected(true);
                        BBuffer++;
                    }
                }

                /*else if (KeyDown.IsKeyDown(Keys.Enter))
                {
                    if (Play.getSelected())
                    {
                        return true;
                    }
                    else
                    {
                        //HIGHSCORES
                    }
                }*/
            }
            else if(BBuffer >= 15)
            {
                BBuffer = 0;
            }
            else
            {
                BBuffer++;
            }

            return false;


        }

    }
}
