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
    class GameOver
    {
        int TotalScore;
        int FinalD;
        int FinalC;
        int Count;
        SpriteBatch sBatch;
        SpriteFont font1;
        Texture2D Coin;  

        public GameOver(int FinalDistance, int FinalCoins, int CorrectAnswers, ref SpriteBatch isBatch, ContentManager content, ref UserInfo UI)
        {
            Count = 0;
            TotalScore = (int)((FinalDistance*0.05) * (1 + (FinalCoins * 0.1)));
            TotalScore = (int)(TotalScore * (1 + (CorrectAnswers * 0.1)));
            UI.SetRecentScore(TotalScore);
            FinalD = FinalDistance;
            FinalC = FinalCoins;
            font1 = content.Load<SpriteFont>("Score");
            sBatch = isBatch;
        }
        public void GOUpdate()
        {
            if (Count < 100)
            {
                Count++;
            }
        }

        public void GODraw()
        {
            sBatch.DrawString(font1, string.Concat("Distance: ", (int)((FinalD) - ((FinalD/100.0) * Count))), new Vector2(50, 100), Color.Black);
            sBatch.DrawString(font1, string.Concat("Coins: ", (int)((FinalC) - ((FinalC / 100.0) * Count))), new Vector2(50, 300), Color.Black);
            sBatch.DrawString(font1, string.Concat("Total Score: ", (int)((TotalScore / 100.0) * Count)), new Vector2(50, 500), Color.Black);
        }
        public int GetFinalScore()
        {
            return TotalScore;
        }
    }
}
