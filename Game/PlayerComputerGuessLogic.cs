using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBulls.Game
{
    public partial class PlayerComputer : IPlayer
    {
        //Stores every possible figits for the position 
        private List<List<int>> _availableDigits = CreateAvailableDigits();
        private List<List<int>> AvailableDigits { get => _availableDigits; set => _availableDigits = value; }

        //Add GenNumber to GenNumberList - available from GamePlay
        //Also Updates AvailableDigits.
        public void AddGenNumberToList()
        {
            GenNumberList.Add(GenNumber);
            UpdateAvailableDigits();
        }
        //
        //
        ///UNFINISHED///////////
        //
        //Updates all dictionaries based on guesses and count of cows and bulls 
        //Parameters: SuppositionCowsAndBulls and Generated number(GenNumberList)
        //private void UpdateDictionaries(List<List<int>> GenNumberList, List<int[]> listCB)
        private void UpdateAvailableDigits()
        {
            var listCB = SuppositionCowsAndBulls;
            var len = listCB.Count;
            if (len != 0)
            {
                int cows = listCB[len - 1][0];
                int bulls = listCB[len - 1][1];

                if(bulls == 0)
                {
                    if (cows > 0 )
                    {
                        //Remove that 4 digits from all positions
                        for (int index = 0; index < 4; index++)
                        {
                            AvailableDigits[index].Remove(GenNumberList[len - 1][index]);
                        }
                    }
                    else
                    {
                        for(int index = 0; index < 4; index++)
                        {
                            for(int position = 0; position < 4; position++)
                            {
                                AvailableDigits[position].Remove(GenNumberList[len - 1][index]);
                            }
                        }
                    }
                }
                else if(bulls > 0 && cows == 0)
                {
                    for (int index = 0; index < 4; index++)
                    {
                        for (int position = 0; position < 4; position++)
                        {
                            if(index != position) AvailableDigits[position].Remove(GenNumberList[len - 1][index]);
                        }
                    }
                }
                
                //If there is only one digit for the position, remove it from other positions
                for(int index = 0; index < 4; index++)
                {
                    if (AvailableDigits[index].Count == 1)
                    { 
                    /*
                    if (AvailableDigits[index].Count < 4)
                    {
                        throw new Exception(String.Format("Position {0} with lt 4 digits", index));
                        */
                        for (int position = 0; position < 4; position++)
                        {
                            if (position != index) AvailableDigits[position].Remove(AvailableDigits[index][0]);
                        }
                        
                    }
                }
            }
        }

        //If there are less than 4 digit per position
        private int NewSpeCaseGeneration()
        {
            var mainList = new List<List<int>> { };
            for (int a = 0; a < AvailableDigits[0].Count; a++)
            {
                var localList = new List<int>();
                localList.Add(AvailableDigits[0][a]);
                for (int b = 0; b < AvailableDigits[1].Count; b++)
                {
                    if(AvailableDigits[0][a] != AvailableDigits[1][b])
                    {
                        if (localList.Count > 1)
                        {
                            localList.RemoveRange(1, localList.Count - 1);
                            localList.Add(AvailableDigits[1][b]);
                        }
                        //localList[1] = AvailableDigits[1][b];
                        else localList.Add(AvailableDigits[1][b]);
                        for (int c = 0; c < AvailableDigits[2].Count; c++)
                        {
                            if ( AvailableDigits[0][a] != AvailableDigits[2][c] && AvailableDigits[1][b] != AvailableDigits[2][c])
                            {
                                if (localList.Count > 2)
                                {
                                    localList.RemoveRange(2, localList.Count - 2);
                                    localList.Add(AvailableDigits[2][c]);
                                }
                                //localList[2] = AvailableDigits[2][c];
                                else localList.Add(AvailableDigits[2][c]);
                                for (int d = 0; d < AvailableDigits[3].Count; d++)
                                {
                                    if (AvailableDigits[0][a] != AvailableDigits[3][d] && AvailableDigits[1][b] != AvailableDigits[3][d] && AvailableDigits[2][c]!= AvailableDigits[3][d])
                                    {
                                        var tempList = new List<int>(localList);
                                        localList.Add(AvailableDigits[3][d]);
                                        mainList.Add(localList);
                                        localList = new List<int>(tempList);
                                    }
                                    
                                }
                            }
                            
                        }
                    }
                    
                }
            }
            var result = new List<int>();
            foreach (var item in mainList) result.Add(ListToInt(item));
            result = Removeduplicates(result);
            result = RemoveSubmitedNumbers(result);
            //OutOfRangeException
            //GenNumber = IntToList(result[rnd.Next(result.Count)]);
            return result[rnd.Next(result.Count)];
            
        }

        private List<int> IntToList(int integer)
        {
            
            var result =  integer.ToString().Select(x => Convert.ToInt32(x.ToString())).ToList();
            return result;
        }

        private List<int> RemoveSubmitedNumbers(List<int> newList)
        {
            foreach(var item in Supposition)
            {
                if (newList.Contains(item)) newList.Remove(item);
            }
            return newList;
        }

        private List<int> Removeduplicates(List<int> newList)
        {
            return newList.Distinct().ToList();
        }

        //Creates a list of indexes in order to be generate a guessnumber
        private List<int> Lt4DigitsIndecesOrder(List<int> tempList)
        {
            //var tempList = new List<int>();
            var start = tempList[0];
            //tempList.Add(start);
            foreach(var item in tempList)
            {
                if(start != item)
                {
                    if (AvailableDigits[start].Count > AvailableDigits[item].Count) Swap(tempList, start, item);
                }
            }
            for (int item = 0; item < tempList.Count - 2; item++)
            {
                if (AvailableDigits[tempList[item]].Count > AvailableDigits[tempList[item + 1]].Count) return Lt4DigitsIndecesOrder(tempList);
            }
            /*
            for (int index = 1; index < 4; index++)
            {
                if (AvailableDigits[start].Count > AvailableDigits[index].Count) tempList.Insert(tempList.IndexOf(start), index);
                else tempList.Insert(tempList.IndexOf(start) + 1, index);
            }
            */
            return tempList;
        }
        
        //Create AvailableDigits
        private static List<List<int>> CreateAvailableDigits()
        {
            List<List<int>> localList = new List<List<int>> { };
            for (int i = 0; i < 4; i++)
            {
                localList.Add(new List<int> { });
                for (int digit = 0; digit < 10; digit++)
                {
                    if (i == 0)
                    {
                        if (digit != 0) localList[i].Add(digit);
                    }
                    else localList[i].Add(digit);
                }
            }
            return localList;
        }

        //Used in special cases - when some of the positions have lt 4 digits available
        //Takes : list<int> from  LessThan4DigitsGenerateNumber()
        //Returns: generated int number 
        public int GenerateNumberLT4Digits(List<int> indexList)
        {
            
            Dictionary<int, int> localDict = new Dictionary<int, int>();
            
            if (localDict.Count == 4) return ListToInt(DictToList(localDict));
            else
            {
                foreach(var index in indexList)
                {
                    if (GenNumber.Count != 0) break;
                    int random = AvailableDigits[index][rnd.Next(AvailableDigits[index].Count)];
                    if (localDict.ContainsValue(random)) GenerateNumberLT4Digits(indexList);
                    else
                    {
                        localDict[index] = random;
                        //GenerateNumberLT4Digits(indexList);
                    }
                }
            }
            if (GenNumber.Count == 0) GenNumber = DictToList(localDict);
            return ListToInt(GenNumber);
        }


        private List<int> DictToList(Dictionary<int, int> dict)
        {
            var localList = new List<int>();
            for(int i = 0; i < 4; i++) localList.Add(dict[i]);

            return localList;
        }

        public int GenerateNumberV3()
        {

            if (GenNumber.Count == 4)   return ListToInt(GenNumber);
            else
            {
                int random = AvailableDigits[GenNumber.Count][rnd.Next(AvailableDigits[GenNumber.Count].Count)];
                if (GenNumber.Contains(random)) GenerateNumberV3();
                else
                {
                    GenNumber.Add(random);
                    GenerateNumberV3();
                }
            }
            return ListToInt(GenNumber);
        }
        public int GenerateNumberV2()
        {

            while(GenNumber.Count < 4)
            {
                int random = AvailableDigits[GenNumber.Count][rnd.Next(AvailableDigits[GenNumber.Count].Count)];
                if (!GenNumber.Contains(random)) GenNumber.Add(random);
            }
            return ListToInt(GenNumber);
        }



        public int GuessNumberV2()
        {
            GenNumber = new List<int> { };
            int assumption;
            //Checks in AvailableDigits for lt 4 digits for position if so calls GenerateNumberLT4Digits
            //and generate the number in special order - positions with less digits are with high priority
            if (CheckCountDigitsPerPosition())
            {
                //var tempList = new List<int> { 0,1,2,3};
                //assumption = GenerateNumberLT4Digits(Lt4DigitsIndecesOrder(tempList));
                assumption = NewSpeCaseGeneration();
                GenNumber = IntToList(assumption);
            }
            else assumption = GenerateNumberV2();
            //If the assumption already exists function calls itself again
            if (Supposition.Contains(assumption)) GuessNumberV2();
            else
            {
                return assumption;
            }
            return 999999;
        }

        //Check whether count of digits per position in AvailableDigits is less than 4.
        private bool CheckCountDigitsPerPosition()
        {
            for(int position = 0; position < 4; position++)
            {
                if (AvailableDigits[position].Count < 4) return true;
            }
            return false;
        }

        //Swap to items in list according to their index
        static void Swap(IList<int> list, int indexA, int indexB)
        {
            int tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

    }
}
