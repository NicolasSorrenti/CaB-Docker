using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBulls.Game

{
    public interface IPlayer
    {
        //4 digit number with no duplicates,  chosen by the player in the begining of the game
        int MyNumber { set; get; }
        
        //Stores last guess
        int Guess { set; get; }

     ////Don't use a dictionary for the lists below, because want to save the order of the guesses.
        //Player's guesses for his opponents number. Count of cows and bulls is calculated by GamePlay class.
        List<int> Supposition { set; get; }

        //Stores the count of cows and bulls for every supposition
        List<int[]> SuppositionCowsAndBulls { set; get; }


        //Stores list of digits in MyNumber
        char[] MyNumberCharList { get; set; }



    }
}
