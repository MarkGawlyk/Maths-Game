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
    class Particle
    {
        float xspeed;
        float yspeed;
        bool Bouncing = false;
        Vector2 location;
        Random rand = new Random();
        int HitFloorCount = 0;

        public Particle(int x, int y)
        {
            xspeed = rand.Next(-6, -2);
            xspeed += (float)rand.NextDouble();
            yspeed = rand.Next(14, 22);
            yspeed += (float)rand.NextDouble();
            location = new Vector2(x, y);
        }

        public void ParticleUpdate()
        {
            if ((location.Y + yspeed) > 980)
            {
                Bouncing = true;
                yspeed = -yspeed;
                yspeed /= 2;
                HitFloorCount = 1;
                xspeed -= 1;
            }

            if (Bouncing)
            {
                HitFloorCount++;
                if (HitFloorCount % 3 == 0)
                {
                    yspeed++;
                }
                if (yspeed == 1)
                {
                    Bouncing = false;
                }
            }

            location.Y += yspeed;
            location.X += xspeed;

        }

        public Vector2 GetLocation()
        {
            return location;
        }
    }
}

