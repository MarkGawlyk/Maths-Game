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
    class Player
    {
        int Width = 28 * 3;
        int Height = 42 * 3;
        Texture2D Texture;
        SpriteBatch sBatch;
        Rectangle PlayerR;
        int CooldownCount = 0;
        const int Cooldown = 10;
        int TimeCount = 0;
        int FrameCount = 0;
        int CurrentLevel;
        const int LevelHeight = 195;
        const int PlatformHeight = 150;


        public Player(ref SpriteBatch isBatch, ContentManager content)
        {
            CurrentLevel = 0;
            sBatch = isBatch;
            //Texture = content.Load<Texture2D>("Panda");
            Texture = content.Load<Texture2D>("Monke");
            PlayerR = new Rectangle((1920 / 4) - Width / 2, 1080 - PlatformHeight - Height, Width, Height);
        }

        public void PlayerUpdate()
        {
            if (CooldownCount != Cooldown)
            {
                CooldownCount++;
            }
            FrameCount++;
            if (FrameCount == 60)
            {
                FrameCount = 0;
                TimeCount++;
            }
        }

        public void DrawPlayer()
        {
            sBatch.Draw(Texture, PlayerR, Color.White);
        }

        public void IncreaseLevel()
        {
            if (CurrentLevel != 3 && CooldownCount == Cooldown)
            {
                CurrentLevel++;
                UpdateRectangle();
                CooldownCount = 0;
            }
        }

        public void DecreaseLevel()
        {
            if (CurrentLevel != 0 && CooldownCount == Cooldown)
            {
                CurrentLevel--;
                UpdateRectangle();
                CooldownCount = 0;
            }
        }

        private void UpdateRectangle()
        {
            PlayerR.Y = (1080 - (PlatformHeight) - Height) - CurrentLevel * LevelHeight;
        }

    }
}
