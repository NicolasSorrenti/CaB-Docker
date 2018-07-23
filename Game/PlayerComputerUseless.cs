using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBulls.Game
{
    public partial class PlayerComputer : IPlayer
    {
        //Stores the probability for every possible 
        private Dictionary<int, int> _dictDigitProbability = CreateDictDigitProbability();
        private Dictionary<int, int> DictDigitProbability { get => _dictDigitProbability; set => _dictDigitProbability = value; }

        //Stores all possible positions for that digit if it is available
        private Dictionary<int, Dictionary<int, int>> DictDigitPosition = CreateDictDigitPosition();

        //Stores all discarded numbers
        private int[] _discardedDigits = new int[] { };
        private int[] DiscardedDigits { get => _discardedDigits; set => _discardedDigits = value; }

        //Every internal is the position of the opponent's number(0,1,2,3) and stores digits if some is suggested
        private char[][] _guessedDigits = { new char[] { }, new char[] { }, new char[] { }, new char[] { } };
        private char[][] GuessedDigits { get => _guessedDigits; set => _guessedDigits = value; }

        //First generated / guessed numbers
        //private int[] _stepsArray = new int[] { 1234, 5678, 9012 };


        ///
        //////////UNFINISHED///////////////
        //
        //Guess Logic
        //public int StepsThroughNumberGeneration(int turn, int number, List<int[]> suppositionCowsAndBulls)
        //{
        //Update GuessNumber or use GuessesGenerator. It have to use AvailableDigits
        //if (Supposition.Count == 0) GuessNumber();
        //else {
        //update lists and dicts
        //check count of cows and bulls.
        //Based on previous step generate suggestion
        //}
        //}

        //Suggests any numbers
        // private int GuessesGenerator(List<int[]> suppositionCowsAndBulls){  }

        //Create DictDigitProbability
        private static Dictionary<int, int> CreateDictDigitProbability()
        {
            Dictionary<int, int> localDict = new Dictionary<int, int> { };
            for (int i = 0; i < 10; i++)
            {
                localDict[i] = 0;
            }
            return localDict;
        }



        //Create DictDigitPosition
        private static Dictionary<int, Dictionary<int, int>> CreateDictDigitPosition()
        {
            Dictionary<int, Dictionary<int, int>> localDict = new Dictionary<int, Dictionary<int, int>> { };
            localDict[0] = new Dictionary<int, int> { };
            localDict[0].Add(1, 0);
            localDict[0].Add(2, 0);
            localDict[0].Add(3, 0);

            for (int i = 1; i < 10; i++)
            {
                localDict[i] = new Dictionary<int, int> { };
                for (int j = 0; j <= 3; j++) localDict[i].Add(j, 0);
            }
            return localDict;
        }


        //REWRITE AvailableDigits is not an array, but List of int lists
        private static int[][] CreateAvailableDigits2()
        {
            int[][] localArray = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                if (i == 0) localArray[i] = new int[9];
                else localArray[i] = new int[10];
                for (int digit = 0; digit < 10; digit++)
                {

                    if (i == 0)
                    {
                        if (digit != 0) localArray[i][digit - 1] = digit;
                    }
                    else localArray[i][digit] = digit;
                }
            }
            return localArray;
        }


    }
}
