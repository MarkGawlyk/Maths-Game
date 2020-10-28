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
    class AnswerParticle
    {
        Vector2 Position;
        float XSpeed;
        float YSpeed;

        public AnswerParticle(Vector2 Pos, int x, int Total, Random r)
        {
            Position = Pos;

            XSpeed = (float)(3 * (Math.Sin(((2 * Math.PI) / Total) * x)));
            XSpeed += r.Next(-1, 1);
            YSpeed = (float)(3 * (Math.Cos(((2 * Math.PI) / Total) * x)));
            YSpeed += r.Next(-1, 1);
        }

        public void UpdateParticle()
        {
            Position.X += XSpeed;
            Position.Y += YSpeed;

            Position.X -= 8;

        }

        public Vector2 GetPos()
        {
            return Position;
        }
    }
}
