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
    class Canons
    {
        Texture2D CanonTexture, ProjectileTexture;
        Rectangle CanonR;
        Rectangle ProjectileR;
        SpriteBatch sBatch;
        Random r = new Random();
        bool Fired;
        public bool IsOffScreen;
        int ProjectileSpeed = -1;


        public Canons(ContentManager Content, ref SpriteBatch isBatch)
        {
            sBatch = isBatch;

            CanonTexture = Content.Load<Texture2D>("Shooter");
            CanonR = new Rectangle(-200, 932, 162, 48);

            ProjectileTexture = Content.Load<Texture2D>("Rock");

                ProjectileR = new Rectangle(-200, 124, 90, 90);
        }

        public void CanonUpdate()
        {
            if (ProjectileR.X <= 1350-((ProjectileSpeed - 3)))
            {
                Fired = true;
            }
            if (Fired)
            {
                ProjectileR.Y -= (int)ProjectileSpeed;
            }
            if (IsOffScreen)
            {
                IsOffScreen = false;
            }
            if(CanonR.X < (0 - 300) && CanonR.X > (-600))
            {
                IsOffScreen = true;
            }
        }

        public void CanonDraw(int Speed)
        {
            CanonR.X -= Speed;
            sBatch.Draw(CanonTexture, CanonR, Color.White);

            ProjectileR.X -= Speed;
            sBatch.Draw(ProjectileTexture, ProjectileR, Color.White);

        }

        public void CreateCanon(int speed)
        {
            ProjectileSpeed = r.Next(speed - 2, speed + 6);
            IsOffScreen = false;
            Fired = false;
            int x = (r.Next(1920, 2500));
            CanonR.X = x;
            ProjectileR.X = x + 35;
            ProjectileR.Y = 860;
        }
        public Rectangle getCanonR()
        {
            return CanonR;
        }
        public Rectangle getProjR()
        {
            return ProjectileR;
        }

    }
}
