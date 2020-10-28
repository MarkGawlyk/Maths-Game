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
    class PlayerJetpack
    {
        SoundEffect DeathSound;
        SoundEffect CorrectSound;
        int TotalCorrectAnswers = 0;
        int Width = 28 * 3;
        int Height = 42 * 3;
        Texture2D AliveTextureP, DeadTextureP, AliveTextureB, DeadTextureB;
        Texture2D HeartT;
        Rectangle HeartR;
        SpriteBatch sBatch;
        Rectangle PlayerR;
        const int LevelHeight = 195;
        const int PlatformHeight = 100;
        public int DeathCount;
        public bool Alive;
        ParticleManager Particles;
        int Lives;
        int AnswerBuffer;
        int CorrectAnswers = 0;
        // TEST
        float Speed = 0; // +ve = fly 
        float FallAcc = 0.9f;
        float FlyAcc = 0.9f;
        int MaxSpeed = 20;
        char CharacterName;
        //TEST


        public PlayerJetpack(ref SpriteBatch isBatch, ContentManager content)
        {
            DeathSound = content.Load<SoundEffect>("Punch");
            CorrectSound = content.Load<SoundEffect>("Applause");

            CharacterName = 'B';
            AnswerBuffer = 0;
            Lives = 3;
            Alive = true;
            sBatch = isBatch;

            HeartT = content.Load<Texture2D>("Heart");
            HeartR = new Rectangle(-599, 20, 15 * 4, 14 * 4);

            AliveTextureP = content.Load<Texture2D>("Panda"); //Panda
            DeadTextureP = content.Load<Texture2D>("DeadPanda");

            AliveTextureB = content.Load<Texture2D>("Bear"); //Bear
            DeadTextureB = content.Load<Texture2D>("DeadBear");

            PlayerR = new Rectangle((900) - (Width / 2), 1080 - PlatformHeight - Height, Width, Height); // x = 1920/4
            DeathCount = 0;
            Particles = new ParticleManager(ref sBatch, content);
        }

        public bool PlayerUpdate(Rectangle Canon, Rectangle Ball)
        {
            if(AnswerBuffer != 0)
            {
                AnswerBuffer++;
            }
            if(AnswerBuffer >= 60)
            {
                AnswerBuffer = 0;
            }

            if(Lives <= 0)
            {
                Kill();
            }
            if (PlayerR.Intersects(Canon) || PlayerR.Intersects(Ball))
            {
                LoseLife();
            }
            InputManager();
            return Alive;
        }

        void InputManager()
        {
            KeyboardState KeyDown = Keyboard.GetState();
            if (Alive && KeyDown.IsKeyDown(Keys.Space))
            {
                PlayerFly();
                Particles.UpdateParticles(PlayerR.Y, true);
            }
            else
            {
                PlayerFall();
                Particles.UpdateParticles(PlayerR.Y, false);
            }
        }

        public void DrawPlayer()
        {
            if (CharacterName == 'P')
            {
                if (Alive)
                {
                    sBatch.Draw(AliveTextureP, PlayerR, Color.White);
                }
                else
                {
                    sBatch.Draw(DeadTextureP, PlayerR, Color.White);
                }
            }
            else
            {
                if (Alive)
                {
                    sBatch.Draw(AliveTextureB, PlayerR, Color.White);
                }
                else
                {
                    sBatch.Draw(DeadTextureB, PlayerR, Color.White);
                }
            }
            Particles.DrawParticles();

            if(Lives == 1 || Lives == 2 || Lives == 3)
            {
                HeartR.X = 10;
                sBatch.Draw(HeartT, HeartR, Color.White);
            }
            if (Lives == 3 || Lives == 2)
            {
                HeartR.X = 10 + (HeartR.Width) + 10;
                sBatch.Draw(HeartT, HeartR, Color.White);
            }
            if (Lives == 3)
            {
                HeartR.X = 10 + (HeartR.Width *2) + 20;
                sBatch.Draw(HeartT, HeartR, Color.White);
            }

        }

        public void PlayerFall()
        {
            if (PlayerR.Y - Speed >= 1080 - PlatformHeight - Height)
            {
                PlayerR.Y = (1080 - PlatformHeight - Height);
                Speed = 0;
            }
            else if (PlayerR.Y - Speed < PlatformHeight)
            {
                PlayerR.Y = PlatformHeight;
                Speed = 0;
            }
            else
            {
                if (Speed - FallAcc < -MaxSpeed)
                {
                    Speed = -MaxSpeed;
                }
                else
                {
                    Speed -= FallAcc;
                }
                PlayerR.Y -= (int)Speed;
            }
        }



        public void PlayerFly()
        {
            if (PlayerR.Y - Speed <= PlatformHeight)
            {
                PlayerR.Y = PlatformHeight;
                Speed = 0;
            }
            else if (PlayerR.Y - Speed > 1080 - PlatformHeight - Height)
            {
                PlayerR.Y = (1080 - PlatformHeight - Height);
                Speed = 0;
            }
            else
            {
                if (Speed + FlyAcc > MaxSpeed)
                {
                    Speed = MaxSpeed;
                }
                else
                {
                    Speed += FlyAcc;
                }
                PlayerR.Y -= (int)Speed;
            }
        }


        public int getYValue()
        {
            return PlayerR.Y;
        }
        public Rectangle getRectangle()
        {
            return PlayerR;
        }

        public bool IsAlive()
        {
            return Alive;
        }

        public void LoseLife()
        {
            if (AnswerBuffer == 0)
            {
                DeathSound.Play();
                Lives -= 1;
            }
            ///
            AnswerBuffer++;
        }

        public void Kill()
        {
            Alive = false;
            PlayerR.Width = Height;
        }

        public void ChangeCharacter(char NewChar)
        {
            if(NewChar == 'B' || NewChar == 'b')
            {
                CharacterName = 'B';
            }
            else
            {
                CharacterName = 'P';
            }
        }

        public void AddToCorrectAnswers()
        {
            if (AnswerBuffer == 0)
            {
                CorrectSound.Play();
                TotalCorrectAnswers += 1;
            }
            ///
            AnswerBuffer++;
            
        }

        public int GetNoOfAnswers()
        {
            return TotalCorrectAnswers;
        }
        public int GetCorrectAnswers()
        {
            return CorrectAnswers;
        }
    }
}
