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
    class ScoreCount
    {
        float Score = 0;
        SpriteBatch sBatch;
        SpriteFont font1;
        string ScoreToDraw;

        public ScoreCount(SpriteBatch isBatch, ContentManager Content)
        {
            sBatch = isBatch;
            font1 = Content.Load<SpriteFont>("Score");
        }
        public void AddScore(float ScoreToAdd)
        {
            Score += ScoreToAdd;
        }
        public void ResetScore()
        {
            Score = 0;
        }
        public void DrawScore(string BeforeScore, string AfterScore, Vector2 Position)
        {
            ScoreToDraw = string.Concat(BeforeScore, (int)Score, AfterScore);
            sBatch.DrawString(font1, ScoreToDraw, Position, Color.Black);
        }
        public int getScore()
        {
            return (int)Score;
        }
    }
}
