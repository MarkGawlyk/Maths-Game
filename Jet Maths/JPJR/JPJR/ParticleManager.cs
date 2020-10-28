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
    class ParticleManager
    {
        const int NoOfParticles = 1200;
        Particle[] Particles;
        const int NoOfParticleVariations = 10;
        Texture2D[] ParticleVariations;
        SpriteBatch sBatch;
        int CurrentParticle = 1;
        Vector2 StartingPos;

        public ParticleManager(ref SpriteBatch isBatch, ContentManager Content)
        {
            Particles = new Particle[NoOfParticles];
            ParticleVariations = new Texture2D[NoOfParticleVariations];
            sBatch = isBatch;
            for (int x = 0; x < 10; x++)
            {
                ParticleVariations[x] = Content.Load<Texture2D>(string.Concat("Particle", x + 1));
            }

            for (int x = 0; x < NoOfParticles; x++)
            {
                Particles[x] = new Particle(3000, 3000);
            }

            StartingPos.X = (870);
        }

        public void UpdateParticles(int YCoord, bool flying)
        {
            StartingPos.Y = YCoord;
            for (int x = 0; x < 1; x++)
            {
                if (flying)
                {
                    Particles[CurrentParticle - 1] = new Particle((int)StartingPos.X, ((int)StartingPos.Y) + 110);
                }
                else
                {
                    Particles[CurrentParticle - 1] = new Particle((int)StartingPos.X, ((int)StartingPos.Y) + 10000);
                }
                if (CurrentParticle == NoOfParticles)
                {
                    CurrentParticle = 0;
                }
                CurrentParticle++;
            }
            for (int x = 0; x < NoOfParticles; x++)
            {
                Particles[x].ParticleUpdate();
            }
        }

        public void DrawParticles()
        {
            for (int x = 0; x < NoOfParticles; x++)
            {
               sBatch.Draw(ParticleVariations[x % 10], Particles[x].GetLocation(), Color.White);
                //sBatch.Draw(ParticleVariations[x % 10], new Rectangle(((int)Particles[x].GetLocation().X), ((int)Particles[x].GetLocation().Y), 10, 10), null, Color.White, 0, new Vector2(100, 100), SpriteEffects.None, 4); 
            }
        }
    }
}

