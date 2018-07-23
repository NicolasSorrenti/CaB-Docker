using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CowsAndBulls.Game
{
    public class PlayerPerson : IPlayer
    {

        public int MyNumber { set; get; }
        public int Guess { set; get; }
        public List<int> Supposition { set; get; }
        public List<int[]> SuppositionCowsAndBulls { set; get; }

        public char[] MyNumberCharList { get; set; }
        
        /*
        PlayerPerson(int myNumber, int guess)
        {
            MyNumber = myNumber;
            Guess = guess;
            Supposition.Add(guess);
        }
        */
        public PlayerPerson(int myNumber)
        {
            MyNumber = myNumber;
            Guess = 0;
            Supposition = new List<int>();
            SuppositionCowsAndBulls = new List<int[]>();
        }
    }
}
