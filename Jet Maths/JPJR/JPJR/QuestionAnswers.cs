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
    class QuestionAnswers
    {
        Texture2D ABackGround;
        SpriteFont Font;
        Rectangle AnswerR;
        SpriteBatch sBatch;
        AnswerAnimation Animation;
        string answer;
        bool IsCorrect;
        bool HasAnswered;
        int FontX;
        int YCoord;
        PlayerJetpack JetPack;
        ContentManager Content;

        public QuestionAnswers(ref SpriteBatch isBatch, ContentManager content, int Y, string AnswerToDisplay, bool Correct, ref PlayerJetpack JP)
        {
            Content = content;
            JetPack = JP;
            IsCorrect = Correct;
            YCoord = Y;
            answer = AnswerToDisplay;
            sBatch = isBatch;
            ABackGround = content.Load<Texture2D>("AnswerBG");
            Font = content.Load<SpriteFont>("AnswerFont");
            AnswerR = new Rectangle(1700, YCoord, 120, 120);
            FontX = 1800;
        }

        public void DrawAnswer()
        {
            if (!HasAnswered)
            {
                sBatch.Draw(ABackGround, AnswerR, Color.White);

                sBatch.DrawString(Font, answer, new Vector2(AnswerR.X + 20, AnswerR.Y + 30), Color.Black);
            }
            else
            {
                Animation.Update();
            }

        }
        public void UpdateAnswer(int Speed)
        {
            //sBatch.Draw(ABackGround, AnswerR, Color.White);
            //sBatch.DrawString(Font, answer, new Vector2(FontX, YCoord), Color.Black);

            FontX -= Speed;
            AnswerR.X -= Speed;

            if (AnswerR.Intersects(JetPack.getRectangle()))
            {
                HasAnswered = true;
                Animation = new AnswerAnimation(ref sBatch, Content, IsCorrect, AnswerR.Center.X, AnswerR.Center.Y);
                if (!IsCorrect)
                {
                    JetPack.LoseLife();
                }
                else
                {
                    JetPack.AddToCorrectAnswers();
                }
            }

            if (HasAnswered)
            {
                //Animation.Update();
            }
        }
    }
}
