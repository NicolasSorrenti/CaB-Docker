using CowsAndBulls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBulls.Game
{
    public class GamePlay
    {
        
        public PlayerPerson PlayerOne { get; set; }
        public PlayerComputer PlayerTwo { get; set; }
        public string Winner { set; get; }

        public int Number { get; set; }
        
        //Constructors
        public GamePlay() { }

        public GamePlay(int personNumber)
        {
            PlayerOne = new PlayerPerson(personNumber);
            PlayerTwo = new PlayerComputer();
            PlayerOne.MyNumberCharList = NumberToCharArray(PlayerOne.MyNumber);
            PlayerTwo.MyNumberCharList = NumberToCharArray(PlayerTwo.MyNumber);
            Winner = "";
        }

        public void Tick(EnterNumberViewModel turn)
        {
            PlayerOne.Guess = turn.Number;
            PlayerOne.Supposition.Add(PlayerOne.Guess);

            PlayerTwo.Guess = PlayerTwo.GuessNumberV2();
            PlayerTwo.Supposition.Add(PlayerTwo.Guess);
            //PlayerTwo.AddGenNumberToList();
            //WhoWon();
        }

        public GameTurnViewModel GetScene()
        {
            var scene = new GameTurnViewModel();
            scene.MyNumberPlayerOne = PlayerOne.MyNumber;
            scene.SuppositionPlayerOne = PlayerOne.Supposition;
            scene.SuppositionPlayerTwo = PlayerTwo.Supposition;
            scene.ComputersNumber = PlayerTwo.MyNumber;
            
            if (PlayerOne.Guess != 0)
            {
                CheckForCowsAndBulls(PlayerOne.Guess, PlayerOne, PlayerTwo);
                scene.SuppositionCowsAndBullsPlayerOne = PlayerOne.SuppositionCowsAndBulls;

                CheckForCowsAndBulls(PlayerTwo.Guess, PlayerTwo, PlayerOne);
                scene.SuppositionCowsAndBullsPlayerTwo = PlayerTwo.SuppositionCowsAndBulls;
                PlayerTwo.AddGenNumberToList();
            }
            //CheckForCowsAndBulls(PlayerTwo.Guess, PlayerTwo, PlayerOne);
            //scene.SuppositionCowsAndBullsPlayerTwo = PlayerTwo.SuppositionCowsAndBulls;
            //PlayerTwo.AddGenNumberToList();
            WhoWon();
            return scene;
        }

        public EndGameViewModel WhoWon()
        {
            var victory = new EndGameViewModel();
            if (CheckForWinner()) victory.Winner = Winner;
            return victory;
        }

        public static char[] NumberToCharArray(int number)
        {
            var digits = number.ToString().ToCharArray(0, 4);
            return digits;
        }

        //Takes two arguments - guess and player that made the guess. Based on them calculates the count of cows and bulls in the number
        //First item folds the cow's count of the suggestion, the second one - bull's count
        public void CheckForCowsAndBulls(int guessNumber, IPlayer player, IPlayer opponent)
        {
            var guess = NumberToCharArray(guessNumber);
            player.SuppositionCowsAndBulls.Add(new int[] { 0,0});
            for(int i = 0; i < guess.Length; i++)
            {
                for (int x = 0; x < opponent.MyNumberCharList.Length; x++)
                {
                    if (guess[i] == opponent.MyNumberCharList[x])
                    {
                        //Increases the count of cows for the guess
                        if (i != x) player.SuppositionCowsAndBulls[(player.SuppositionCowsAndBulls.Count - 1)][0]++;
                        //Increases the count of bulls for the guess
                        else player.SuppositionCowsAndBulls[(player.SuppositionCowsAndBulls.Count - 1)][1]++;
                    }
                }
            }
        }

        public bool CheckForWinner()
        {
            if(PlayerOne.SuppositionCowsAndBulls.Count > 0)
            {
                if (PlayerTwo.SuppositionCowsAndBulls[PlayerTwo.SuppositionCowsAndBulls.Count - 1][1] == 4 &&
                    PlayerOne.SuppositionCowsAndBulls[PlayerOne.SuppositionCowsAndBulls.Count - 1][1] == 4) Winner = "Both of you";
                else if (PlayerTwo.SuppositionCowsAndBulls[PlayerTwo.SuppositionCowsAndBulls.Count - 1][1] == 4) Winner = "Computer";
                else if (PlayerOne.SuppositionCowsAndBulls[PlayerOne.SuppositionCowsAndBulls.Count - 1][1] == 4) Winner = "You";
            }
            if (String.IsNullOrEmpty(Winner)) return false;
            else return true;
            
        }
    }
}
