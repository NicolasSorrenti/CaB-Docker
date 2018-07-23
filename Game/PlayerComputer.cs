using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CowsAndBulls.Game
{
    public partial class PlayerComputer : IPlayer
    {

        public int MyNumber { set; get; }
        public int Guess { set; get; }
        //Should be static?
        public List<int> Supposition { set; get; }
        public List<int[]> SuppositionCowsAndBulls { set; get; }
        public char[] MyNumberCharList { get; set; }

        private static List<int> genNumber = new List<int> { };
        public static List<int> GenNumber { get => genNumber; set => genNumber = value; }
        private static List<List<int>> genNumberList = new List<List<int>> { };
        public static List<List<int>> GenNumberList { get => genNumberList; set => genNumberList = value; }
        

        private Random rnd;

        public PlayerComputer()
        {
            rnd = new Random();
            GenNumber = new List<int>();
            Supposition = new List<int> { };
            SuppositionCowsAndBulls = new List<int[]> { };
            MyNumber = GenerateNumberV2();
            GenNumberList = new List<List<int>> { };
            
            
        }

        // Може би е по-добре ГенНъмбър да връща тру и фоус и ЛистТоИнт да обработва листа и да дава числото, за да се хващат по-лесно грешки.
        public int GenerateNumber()
        {
            int random = rnd.Next(0, 10);
            //genNumber.Add(random);
            if (GenNumber.Count == 1 && GenNumber[0] == 0) 
            {
                GenNumber = new List<int> { };
                GenerateNumber();
            }
            else
            {
                if (GenNumber.Count() == 4)
                {
                    //genNumberList.Add(genNumber);
                    return ListToInt(GenNumber);
                }
                else
                {
                    if (GenNumber.Contains<int>(random)) GenerateNumber();
                    else
                    {
                        GenNumber.Add(random);
                        GenerateNumber();
                    }
                }
            }
            //genNumberList.Add(genNumber);
            return ListToInt(GenNumber);
        }

        private int ListToInt(List<int> genNumber)
        {
            return int.Parse(string.Join("", genNumber));
            //
            //Other way to do it
            //
            /*
            int total = 0;
            foreach (int entry in genNumber)
            {
                total = 10 * total + entry;
            }
                return total;
            */
        }

        public int GuessNumber()
        {
            GenNumber = new List<int> { };
            int assumption = GenerateNumber();
            if (Supposition.Contains(assumption)) GuessNumber();
            else
            {
                GenNumberList.Add(GenNumber);
                return assumption;
            }
            return 999999;
        }

    }
}
