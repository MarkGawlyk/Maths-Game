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
    class Coins
    {
        Coin[] CoinArray1;
        public int InitializeCount = 0;
        int offset = 45;
        public int ArrayX;
        public int CurrentTravelled;
        Random r = new Random();
        SpriteBatch spriteBatch;
        ContentManager Content;
        public bool IsOffScreen = false;
        SoundEffect CoinSound;

        public Coins(ref SpriteBatch sBatch, ContentManager content)
        {
            spriteBatch = sBatch;
            Content = content;
            CreateRandomArray();
            CoinSound = Content.Load<SoundEffect>("Coin");
        }

        public void CreateRandomArray()
        {
            IsOffScreen = false;
            int temp = DateTime.Now.Millisecond % 2;
            ArrayX = CurrentTravelled + r.Next(2000, 3000);
            int tempy = r.Next(110, 750);
            if(temp == 0)
            {
                Create2Line(ArrayX, tempy); //GETRANDOM X and Y, Save X
            }
            else
            {
                Create3Line(ArrayX, tempy);
            }
        }

        public void DrawCoins(int Speed)
        {
            for (int x = 0; x < CoinArray1.Length; x++)
            {
                CoinArray1[x].DrawCoin(Speed);
            }
        }

        public void UpdateCoins(Rectangle CharacterR, ref ScoreCount CoinsCollected, int Travelled)
        {
            CurrentTravelled = Travelled;
            if ((ArrayX + 600 < CurrentTravelled))
            {
                IsOffScreen = true;
            }
            for (int x = 0; x < CoinArray1.Length; x++)
            {
                CoinArray1[x].UpdateCoin(CharacterR);
                if (!CoinArray1[x].GetCounted() && CoinArray1[x].getCollected())
                {
                    CoinSound.Play();
                    CoinArray1[x].Remove();
                    CoinArray1[x].HasBeenCounted();
                    CoinsCollected.AddScore(1.0f);
                }
            }
        }

        private void Create2Line(int TopRightX, int TopRightY)
        {
            // 15 x 2 block
            IsOffScreen = false;
            CoinArray1 = new Coin[30];
            InitializeCount = 0;
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    CoinArray1[InitializeCount] = new Coin(TopRightX + (x * offset) - CurrentTravelled, TopRightY + 40 + (y * offset), ref spriteBatch, Content);
                    InitializeCount++;
                }
            }
            InitializeCount = 0;
        }

        private void Create3Line(int TopRightX, int TopRightY)
        {

            // 15 x 3 block
            IsOffScreen = false;
            CoinArray1 = new Coin[45];
            InitializeCount = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    CoinArray1[InitializeCount] = new Coin(TopRightX + (x * offset) - CurrentTravelled, TopRightY + 40 + (y * offset), ref spriteBatch, Content);
                    InitializeCount++;
                }
            }
            InitializeCount = 0;
        }

        private void CreateLargeBlock()
        {
            // 10 x 5 block
        }
    }
}
