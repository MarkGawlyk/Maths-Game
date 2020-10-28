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
using System.Diagnostics;

namespace JPJR
{
    class SplashScreen
    {
        Texture2D LogoT;
        Texture2D PandaT;
        Texture2D BearT;
        Rectangle LogoR;
        Rectangle PandaR;
        Rectangle BearR;
        SpriteFont Text;
        SpriteBatch sBatch;
        int Count;
        bool GoingUp;
        bool CanContinue;
        double BobSpeed;

        public SplashScreen(ref SpriteBatch isBatch, ContentManager Content)
        {
            BobSpeed = 0;
            GoingUp = true;
            sBatch = isBatch;
            LogoT = Content.Load<Texture2D>("SplashLogo");
            PandaT = Content.Load<Texture2D>("Panda");
            BearT = Content.Load<Texture2D>("Bear");
            Text = Content.Load<SpriteFont>("Score");
            CanContinue = false;
            BearR = new Rectangle(100, 400, 280, 420);
            PandaR = new Rectangle(1540, 400, 280, 420);
            LogoR = new Rectangle(560, 400, 800, 350);
        }

        public void DrawSplash()
        {
            if (CanContinue)
            {
                sBatch.DrawString(Text, "Press Enter to continue...", new Vector2(670, 1000), Color.Black);
            }
            sBatch.Draw(LogoT, LogoR, Color.White);
            sBatch.Draw(PandaT, PandaR, Color.White);
            sBatch.Draw(BearT, BearR, Color.White);
        }

        public bool UpdateSplash()
        {

            if (GoingUp)
            {
                BobSpeed += 0.2;
                if(BobSpeed >= 4)
                {
                    GoingUp = false;
                }
            }
            else
            {
                BobSpeed -= 0.2;
                if (BobSpeed <= -4)
                {
                    GoingUp = true;
                }
            }

            BearR.Y += (int)BobSpeed;
            PandaR.Y += (int)BobSpeed;

            Count++;
            if (Count > (60 * 1))
            {
                CanContinue = true;
            }
            if (CanContinue)
            {
                KeyboardState KeyDown = Keyboard.GetState();
                if (KeyDown.IsKeyDown(Keys.Enter))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
