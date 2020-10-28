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
    class Coin
    {
        int Height = 40;
        int Width = 40;
        int XCoord;
        int YCoord;
        bool Collected = false;
        bool Counted = false;
        Random r = new Random();
        Rectangle CoinR;
        Texture2D CoinTexture;
        
        SpriteBatch spriteBatch;

        public Coin(int x, int y, ref SpriteBatch sBatch, ContentManager Content)
        {
            
            spriteBatch = sBatch;
            CoinTexture = Content.Load<Texture2D>(string.Concat("coin", r.Next(1, 4)));
            CoinR = new Rectangle(x, y, Width, Height);
            XCoord = x;
            YCoord = y;
        }
        public void DrawCoin(int Speed)
        {
            CoinR.X -= Speed;
            spriteBatch.Draw(CoinTexture, CoinR, Color.White);
        }

        public void UpdateCoin(Rectangle CharacterRectangle)
        {
            if (CoinR.Intersects(CharacterRectangle))
            {
                Collected = true;
            }
        }

        public bool getCollected()
        {
            return Collected;
        }

        public void Remove()
        {
            CoinR.Y = -150;
        }
        public void HasBeenCounted()
        {
            Counted = true;
        }
        public bool GetCounted()
        {
            return Counted;
        }
    }
}
