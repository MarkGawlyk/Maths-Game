using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPJR
{
    class LevelController
    {
        enum LevelState {Coins, Boulder, Question};
        LevelState CurrentState = LevelState.Coins;

        bool CreateCoins;
        bool CreateBoulder;
        bool QuestionMode;

        int CurrentTime = 1;

        public LevelController()
        {
        }

        public void Update()
        {

        }
    }
}
