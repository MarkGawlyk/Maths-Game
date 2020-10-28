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
    class PlatformManager
    {
        private const int PlatformWidth = 600;
        private const int PlatformHeight = 100;
        private const int StageLength = 200;
        private const int BufferLength = 50;
        int StartOffset = 500;
        int platformCount = 1;

        private int StageCount = 0;
        string[] StageImageNames = { "Platform1s", "Platform1", "Platform1" };


        Platform[] Stages = new Platform[3];

        public PlatformManager(ref SpriteBatch sBatch, ContentManager Content, int Ycoord, string ImageName)
        {
            StageImageNames[0] = ImageName;
            for (int x = 0; x < 3; x++)
            {
                Stages[x] = new Platform(PlatformWidth, PlatformHeight, 0, -200, (Ycoord - PlatformHeight), StageImageNames[x], ref sBatch, Content);
            }
        }

        public void DrawStage(int Speed)
        {
            Stages[StageCount].IncreaseRectrangleX(Speed);
            for (int x = 0; x < StageLength; x++)
            {
                Stages[StageCount].DrawPlatform();
                Stages[StageCount].IncreaseRectrangleX(PlatformWidth);
            }
            Stages[StageCount].IncreaseRectrangleX(-(StageLength * PlatformWidth));
        }

        public void DrawStage2(int Speed, int Distance)
        {
            if (Distance*20 >= (platformCount * PlatformWidth) + StartOffset)
            {
                Stages[StageCount].IncreaseRectrangleX(PlatformWidth);
                platformCount++;
            }
            Stages[StageCount].IncreaseRectrangleX(Speed);
            for (int x = 0; x < BufferLength; x++)
            {
                Stages[StageCount].DrawPlatform();
                Stages[StageCount].IncreaseRectrangleX(PlatformWidth);
            }
            Stages[StageCount].IncreaseRectrangleX(-(BufferLength * PlatformWidth));
        }

        private void AddNextPlatform()
        {
            //remove first, add new first and move start point to second platform in array.
        }
    }
}
