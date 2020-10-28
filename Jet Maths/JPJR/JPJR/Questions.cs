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
    class Questions
    {
        string QuestionText;
        int QuestionAnswer;
        int WrongAnswer1, WrongAnswer2;
        Random r = new Random();
        int Topics;
        int SecsToAnswer;
        int FrameCount;
        public bool QuestionAnswered;
        QuestionAnswers[] Answers;
        SpriteBatch sBatch;
        ContentManager Content;
        Texture2D QuestionBG;
        Rectangle QuestionR;
        SpriteFont Font;
        PlayerJetpack JetPack;
        bool Moving;
        int QuestionCount = 0;
        
        int ChooseA;

        public Questions(int TopicCode, ref SpriteBatch isBatch, ContentManager content, ref PlayerJetpack JP)
        {
            JetPack = JP;
            Font = content.Load<SpriteFont>("AnswerFont");
            QuestionR = new Rectangle(720, 5, 480, 120);
            QuestionBG = content.Load<Texture2D>("QuestionBG");
            Content = content;
            sBatch = isBatch;
            Topics = 6;
            Answers = new QuestionAnswers[3];
           
        }

        public void CreateNewQ(int Topic)
        {
            //TOPIC IDs :
            //1 = Addition, 2 = Subtraction, 3 = Multiplication, 4 = Division
            //5 = All Questions, 6 = Addition and Subtraction, 7 = Multiplication Addition and Subtraction
            //
            QuestionCount++;
            Moving = false;
            QuestionAnswered = false;
            FrameCount = 0;
            Topics = Topic;

            if (Topic == 5)
            {
                Topics = r.Next(1, 5);
            }
            else if (Topic == 6)
            {
                Topics = r.Next(1, 3);
            }
            else if(Topic == 7)
            {
                Topics = r.Next(1, 4);
            }
            switch (Topics)
            {
                case 1:
                    CreateAddition();
                    break;
                case 2:
                    CreateSubtraction();
                    break;
                case 3:
                    CreateMultiplication();
                    break;
                case 4:
                    CreateDivision();
                    break;
            }

            ChooseA = r.Next(1, 4);
            if(ChooseA == 1)
            {
                Answers[0] = new QuestionAnswers(ref sBatch, Content, 120, WrongAnswer1.ToString(), false, ref JetPack);
                Answers[1] = new QuestionAnswers(ref sBatch, Content, 480, WrongAnswer2.ToString(), false, ref JetPack);
                Answers[2] = new QuestionAnswers(ref sBatch, Content, 840, QuestionAnswer.ToString(), true, ref JetPack);
            }
            else if(ChooseA == 2)
            {
                Answers[0] = new QuestionAnswers(ref sBatch, Content, 120, WrongAnswer1.ToString(), false, ref JetPack);
                Answers[1] = new QuestionAnswers(ref sBatch, Content, 480, QuestionAnswer.ToString(), true, ref JetPack);
                Answers[2] = new QuestionAnswers(ref sBatch, Content, 840, WrongAnswer2.ToString(), false, ref JetPack);
            }
            else if(ChooseA == 3)
            {
                Answers[0] = new QuestionAnswers(ref sBatch, Content, 120, QuestionAnswer.ToString(), true, ref JetPack);
                Answers[1] = new QuestionAnswers(ref sBatch, Content, 480, WrongAnswer2.ToString(), false, ref JetPack);
                Answers[2] = new QuestionAnswers(ref sBatch, Content, 840, WrongAnswer1.ToString(), false, ref JetPack);
            }

            

        }

        protected void CreateAddition()
        {
            SecsToAnswer = 3;
            int x, y;
            x = r.Next(3+QuestionCount, 10+(QuestionCount));
            y = r.Next(3 + QuestionCount, 10+(QuestionCount));
            QuestionAnswer = x + y;
            QuestionText = string.Concat(x, "+", y, "=");
            WrongAnswer1 = CreateWrongAnswer(QuestionAnswer, null);
            WrongAnswer2 = CreateWrongAnswer(QuestionAnswer, WrongAnswer1);
        }

        protected void CreateSubtraction()
        {
            SecsToAnswer = 3;
            int x, y;
            x = r.Next(7+(2 * QuestionCount), 15+(2* QuestionCount));
            y = r.Next(1 + (2 * QuestionCount), 8 + (2 * QuestionCount));
            QuestionAnswer = x - y;
            QuestionText = string.Concat(x, "-", y, "=");
            WrongAnswer1 = CreateWrongAnswer(QuestionAnswer, null);
            WrongAnswer2 = CreateWrongAnswer(QuestionAnswer, WrongAnswer1);
        }

        protected void CreateMultiplication()
        {
            SecsToAnswer = 4;
            int x, y;
            x = r.Next(7 + (2 * QuestionCount), 15 + (2 * QuestionCount));
            y = r.Next(1 + (2 * QuestionCount), 8 + (2 * QuestionCount));
            QuestionAnswer = x * y;
            QuestionText = string.Concat(x, "x", y, "=");
            WrongAnswer1 = CreateWrongAnswer(QuestionAnswer, null);
            WrongAnswer2 = CreateWrongAnswer(QuestionAnswer, WrongAnswer1);
        }

        protected void CreateDivision()
        {
            SecsToAnswer = 5;
            int x, y;
            x = r.Next(3 + (2 * QuestionCount), 15 + (2 * QuestionCount));
            y = r.Next(1 + (2 * QuestionCount), 8 + (2 * QuestionCount));
            y *= x;
            QuestionAnswer = y / x;
            QuestionText = string.Concat(y, "/", x, "=");
            WrongAnswer1 = CreateWrongAnswer(QuestionAnswer, null);
            WrongAnswer2 = CreateWrongAnswer(QuestionAnswer, WrongAnswer1);
        }

        public void UpdateQuestion()
        {
            FrameCount++;
            if (!QuestionAnswered)
            {
                FrameCount++;
                if(FrameCount > SecsToAnswer * 60)
                {
                    Moving = true;
                }
                if (Moving)
                {
                    Answers[0].UpdateAnswer(8);
                    Answers[1].UpdateAnswer(8);
                    Answers[2].UpdateAnswer(8);
                    if (FrameCount > 800)
                    {
                        QuestionAnswered = true;
                    }
                }
            }
        }

        public void DrawQuestion()
        {
            sBatch.Draw(QuestionBG, QuestionR, Color.White);
            sBatch.DrawString(Font, QuestionText, new Vector2(900, 30), Color.Black);
            Answers[0].DrawAnswer();
            Answers[1].DrawAnswer();
            Answers[2].DrawAnswer();
        }

        private int CreateWrongAnswer(int correct, int? OtherWrongAns)
        {
            int x = r.Next(1, 3);
            int WrongAns = -1;
            bool repeat = false;
            do
            {
                repeat = false;
                switch (x)
                {
                    case 1:
                        if (correct < 10)
                        {
                            WrongAns = (correct + r.Next(-3, 4));
                        }
                        else if (r.Next(0, 2) == 0)
                        {
                            WrongAns = (correct - 10);
                        }
                        else
                        {
                            WrongAns = (correct + 10);
                        }
                        break;

                    case 2:
                        if (correct < 10)
                        {
                            WrongAns = (correct + r.Next(-3, 4));
                        }
                        else
                        {
                            WrongAns = correct + r.Next(-10, 10);
                        }
                        break;
                }

                if (OtherWrongAns == null)
                {
                    repeat = false;
                }
                if (OtherWrongAns == WrongAns || WrongAns == correct)
                {
                    repeat = true;
                }

            } while (repeat == true);

            return WrongAns;
        }

    }
}
