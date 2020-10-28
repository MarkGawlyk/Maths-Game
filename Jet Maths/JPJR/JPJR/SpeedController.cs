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
    class SpeedController
    {
        int MaxSpeed;
        int TimetoTopSpeed;
        int PixelsPerFrame;
        int FrameCount = 0;
        int GameCountS = 0;
        int CurrentTimeS = 1;
        int MinSpeed;
        public bool End;
        int EndCount;

        int temp;

        public SpeedController(int Maxspeed, int SecsToTopSpeed, int Minspeed)
        {
            End = false;
            MinSpeed = Minspeed;

            MaxSpeed = (int)Maxspeed;

            TimetoTopSpeed = (int)SecsToTopSpeed;
        }

        public void ReSetSC(int maxpeed, int SecsToTopSpeed)
        {
            MaxSpeed = maxpeed;
            TimetoTopSpeed = SecsToTopSpeed;
            PixelsPerFrame = 0;
            CurrentTimeS = 1;
        }

        public bool updateSpeed()
        {
            FrameCount++;
            if (!End)
            {
                if (FrameCount == 60)
                {
                    FrameCount = 0;
                    GameCountS++;
                    CurrentTimeS++;
                }
                if (TimetoTopSpeed <= CurrentTimeS)
                {
                    PixelsPerFrame = MaxSpeed;
                }
                else
                {
                    temp = (TimetoTopSpeed / MaxSpeed);
                    PixelsPerFrame = (CurrentTimeS / (temp));
                    if (PixelsPerFrame <= MinSpeed)
                    {
                        PixelsPerFrame = MinSpeed;
                    }
                    
                }
            }
            else
            {
                EndCount++;
                if (EndCount > 60 && (EndCount % 10 == 1))
                {
                    PixelsPerFrame--;
                }
                if(PixelsPerFrame == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public int GetSpeed()
        {
            return PixelsPerFrame;
        }
        public int GetTimeS()
        {
            return CurrentTimeS;
        }
        public void EndGame()
        {
            End = true;
        }
    }
}
