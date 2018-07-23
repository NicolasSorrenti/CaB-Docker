using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBulls.Models
{
    public class GameTurnViewModel
    {
        public int MyNumberPlayerOne { get; set; }
        public List<int> SuppositionPlayerOne { get; set; }
        public List<int> SuppositionPlayerTwo { get; set; }
        public int ComputersNumber { get; set; }

        public List<int[]> SuppositionCowsAndBullsPlayerOne { set; get; }
        public List<int[]> SuppositionCowsAndBullsPlayerTwo { set; get; }
    }
}
