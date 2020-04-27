using System;
using System.Collections.Generic;
using System.Text;

namespace RPS_Game
{
    class Round
    {
        private Player winner;

        public string p1Choice { get; set; }
        public string p2Choice { get; set; }
        public Player Winner { get => winner; set => winner = value; }
    }
}
