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
    class AnswerAnimation
    {
        Texture2D particleT;
        AnswerParticle[] Particles = new AnswerParticle[20];
        SpriteBatch isBatch;
        Vector2 OriginOfParticles;
        int Count = 0;
        int YCoord, XCoord;
        Random r = new Random();

        public AnswerAnimation(ref SpriteBatch sBatch, ContentManager content, bool Correct, int xCoord, int yCoord)
        {
            isBatch = sBatch;
            if (Correct)
            {
                particleT = content.Load<Texture2D>("CorrectParticle");
            }
            else
            {
                particleT = content.Load<Texture2D>("IncorrectParticle");
            }

            for(int x = 0; x < 20; x++)
            {
                Particles[x] = new AnswerParticle(new Vector2(xCoord, yCoord),x, 20, r);
            }

            YCoord = yCoord;
            XCoord = xCoord;
            OriginOfParticles = new Vector2(XCoord, YCoord);
        }

        public void Draw()
        {
            for (int x = 0; x < 20; x++)
            {
                isBatch.Draw(particleT, Particles[x].GetPos(), Color.White);
            }
            
        }

        public void Update()
        {
            for (int x = 0; x < 20; x++)
            {
                Particles[x].UpdateParticle();
            }
            Draw();
        }
    }
}
